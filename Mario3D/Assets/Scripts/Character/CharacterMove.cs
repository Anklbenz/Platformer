using UnityEngine;
//  Application.targetFrameRate = 240;
public sealed class CharacterMove : MonoBehaviour
{
    [Header("JumpSettings")]
    [SerializeField] private float _jumpForceDuration = 0.15f; //Time of jump
    [SerializeField] private float _addForceStep = 3.67f;
    [SerializeField] private float _bouncePower = 5f;
    [SerializeField] private LayerMask isGroundedLayerMask;
    [SerializeField] private LayerMask wallCheckLayerMask;
    [Space]
    [Header("Walk")]
    [SerializeField] private float _maxWalkSpeed = 9;
    [SerializeField] private float _walkForceStep = 1.1f;
    [SerializeField] public float minActionSpeed = 0.3f;
    [SerializeField] private Transform characterMesh;

    private Character _charater;

    private Rigidbody _rBody;
    private CapsuleCollider _capsuleCollider;
    private BoxCollider _boxCollider;
    private Vector3 _moveDirection = Vector3.zero;

    private Vector3 _groundCheckPlatformSize;
    private Vector3 _wallContactPlatformSize;
    private float _groundCheckDistance;
    private float _wallCheckDistance;
    private float _boxCastWidth = 0.1f;
    private float _boxCastEdge = 0.95f; //%

    private bool _isJumpButtonPressed = false;
    private bool _isJump = false;
    private bool _canSlide = false;
 
    private float timeLeft;

    public bool MovingUp { get => _rBody.velocity.y > 0; }
    public bool MovingDown { get => _rBody.velocity.y < 0; }
    public Vector3 ColliderBoundsCenter { get => _boxCollider.bounds.center; }

    private bool isGrounded {
        get => Physics.BoxCast(_capsuleCollider.bounds.center, _groundCheckPlatformSize / 2, Vector3.down, out RaycastHit groundHit,
                          transform.rotation, _groundCheckDistance, isGroundedLayerMask);
    }

    private bool isWallContact {
        get => Physics.BoxCast(_boxCollider.bounds.center, _wallContactPlatformSize / 2, _moveDirection, out RaycastHit wallHit,
                         transform.rotation, _wallCheckDistance, wallCheckLayerMask);
    }

    private void Awake() {
        _rBody = GetComponent<Rigidbody>();
        _capsuleCollider = GetComponent<CapsuleCollider>();
        _boxCollider = GetComponent<BoxCollider>();
        _charater = GetComponent<Character>();
        _charater.OnScaleChangedEvent += UpdateCastDistancesOnLocalScale;
        this.UpdateCastDistancesOnLocalScale();
       
    }

    void Update() {
        _moveDirection.z = Input.GetAxis("Horizontal");
        _isJumpButtonPressed = Input.GetKey(KeyCode.W);

        if (Input.GetKeyDown(KeyCode.W) && isGrounded)
            _isJump = true;

        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) {
            timeLeft = 0;
           _isJump = false;
        }

        if (!isGrounded)
            _canSlide = true;
    }

    private void FixedUpdate() {
        //isWallContact для того чтобы персонаж не прилипал к стенам из за AddForce
        if (_moveDirection.z != 0 && !isWallContact)
            Walk();

        if (_isJump)
            Jump();

        if (isGrounded && _canSlide)
            SideImpulse();
    }

    void Walk() {
        float currentBodySpeed = Mathf.Abs(_rBody.velocity.z);
        if (currentBodySpeed < _maxWalkSpeed) {
            _rBody.AddForce(_moveDirection * _walkForceStep, ForceMode.VelocityChange);
            Flip(characterMesh, _moveDirection);
        }
    }

    private void Jump() {
        if (_isJumpButtonPressed) {
            timeLeft += Time.fixedDeltaTime;
            if (timeLeft < _jumpForceDuration)
                _rBody.AddForce(Vector3.up * _addForceStep, ForceMode.VelocityChange);
        }
    }

    public void Bounce() {
        StopVerticalMove();
        _rBody.AddForce(Vector3.up * _bouncePower, ForceMode.Impulse);
    }

    public void StopVerticalMove() {
        _rBody.velocity = new Vector3(_rBody.velocity.x, 0, _rBody.velocity.z);
    }

    void Flip(Transform go, Vector3 direction) {
        if (direction == Vector3.back)
            go.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
        if (direction == Vector3.forward)
            go.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
    }

    void SideImpulse() {
        Vector3 t = new Vector3(0, _rBody.velocity.y * -1, 0);
        _rBody.velocity += t;
        _canSlide = false;
    }

    public void UpdateCastDistancesOnLocalScale() {
        _groundCheckPlatformSize = new Vector3(_boxCollider.size.x,
                                               _boxCastWidth,
                                               _boxCollider.size.z * _boxCastEdge * transform.localScale.z);

        _wallContactPlatformSize = new Vector3(_boxCollider.size.x,
                                               _boxCollider.size.y * _boxCastEdge * transform.localScale.y,
                                               _boxCastWidth);

        _groundCheckDistance = (_capsuleCollider.height / 2) * transform.localScale.y;
        _wallCheckDistance = (_boxCollider.size.z / 2) * transform.localScale.z;
    }

    private void OnDestroy() {
        _charater.OnScaleChangedEvent -= UpdateCastDistancesOnLocalScale;
    }

    void OnDrawGizmos() {
        if (_capsuleCollider == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawLine(_capsuleCollider.bounds.center, _capsuleCollider.bounds.center - transform.up * _groundCheckDistance);
        Gizmos.DrawWireCube(_capsuleCollider.bounds.center - transform.up * _groundCheckDistance, _groundCheckPlatformSize);

        Gizmos.color = Color.magenta;
        Gizmos.DrawWireCube(_boxCollider.bounds.center + _moveDirection * _wallCheckDistance, _wallContactPlatformSize);
    }
}
