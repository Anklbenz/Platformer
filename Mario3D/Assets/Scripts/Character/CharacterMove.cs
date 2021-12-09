using UnityEngine;
//  Application.targetFrameRate = 240;
public sealed class CharacterMove : MonoBehaviour,  IMoveProvider
{
    [Header("JumpSettings")]
    [SerializeField] private float _jumpForceDuration = 0.15f; //Time of jump
    [SerializeField] private float _addForceStep = 3.67f;
    [SerializeField] private float _bouncePower = 5f;
    [SerializeField] private LayerMask _isGroundedLayer;
    [SerializeField] private LayerMask _wallCheckLayer;
    private bool _isJumpButtonPressed = false;
    private bool _isJump = false;
    private float _jumpingTimeLeft;
    [Space]
    [Header("Walk")]
    [SerializeField] private float _maxWalkSpeed = 9;
    [SerializeField] private float _walkForceStep = 1.1f;
    [SerializeField] public float _minActionSpeed = 0.3f;
    [SerializeField] private Transform _characterTransform;
    private bool _canSlide = false;

    private Rigidbody _rBody;
    private CapsuleCollider _capsuleCollider;
    private BoxCollider _boxCollider;
    private Vector3 _moveDirection = Vector3.zero;

    private float _boxCastWidth = 0.1f;
    private const float BOX_INDENT = 0.95f; //% 

    public bool MovingUp { get => _rBody.velocity.y > 0; }
    public bool MovingDown { get => _rBody.velocity.y < 0; }

    private Vector3 _groundCheckPlatformSize { get => new Vector3(_boxCollider.size.x, _boxCastWidth, _boxCollider.size.z * BOX_INDENT * transform.localScale.z); }
    private Vector3 _wallContactPlatformSize { get => new Vector3(_boxCollider.size.x, _boxCollider.size.y * BOX_INDENT * transform.localScale.y, _boxCastWidth); }

    private float _groundCheckDistance { get => (_capsuleCollider.height / 2) * transform.localScale.y; }
    private float _wallCheckDistance { get => (_boxCollider.size.z / 2) * transform.localScale.z; }

    public bool IsGrounded {
        get => Physics.BoxCast(_capsuleCollider.bounds.center, _groundCheckPlatformSize / 2, Vector3.down, out RaycastHit groundHit,
                          transform.rotation, _groundCheckDistance, _isGroundedLayer);
    }

    private bool isWallContact {
        get => Physics.BoxCast(_boxCollider.bounds.center, _wallContactPlatformSize / 2, _moveDirection, out RaycastHit wallHit,
                         transform.rotation, _wallCheckDistance, _wallCheckLayer);
    }

    private void Awake() {
        _rBody = GetComponent<Rigidbody>(); 
        _boxCollider = GetComponent<BoxCollider>();
        _capsuleCollider = GetComponent<CapsuleCollider>();
    }

    private void Update() {
        _moveDirection.z = Input.GetAxis("Horizontal");
        _isJumpButtonPressed = Input.GetKey(KeyCode.W);

        if (Input.GetKeyDown(KeyCode.W) && IsGrounded)
            _isJump = true;

        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) {
            _jumpingTimeLeft = 0;
            _isJump = false;
        }

        if (!IsGrounded)
            _canSlide = true;
    }

    private void FixedUpdate() {
        //isWallContact для того чтобы персонаж не прилипал к стенам из за AddForce
        if (_moveDirection.z != 0 && !isWallContact)
            Walk();

        if (_isJump)
            Jump();

        if (IsGrounded && _canSlide)
            SideImpulse();
    }

    private void Walk() {
        float currentBodySpeed = Mathf.Abs(_rBody.velocity.z);
        if (currentBodySpeed < _maxWalkSpeed) {
            _rBody.AddForce(_moveDirection * _walkForceStep, ForceMode.VelocityChange);
            Flip(_characterTransform, _moveDirection);
        }
    }

    private void Jump() {
        if (_isJumpButtonPressed) {
            _jumpingTimeLeft += Time.fixedDeltaTime;
            if (_jumpingTimeLeft < _jumpForceDuration)
                _rBody.AddForce(Vector3.up * _addForceStep, ForceMode.VelocityChange);
        }
    }

    public void Bounce() {
        StopVerticalMove();
        _rBody.AddForce(Vector3.up * _bouncePower, ForceMode.Impulse);
    
    }

    private void StopVerticalMove() {
        _rBody.velocity = new Vector3(_rBody.velocity.x, 0, _rBody.velocity.z);
    }

    private void Flip(Transform go, Vector3 direction) {
        if (direction == Vector3.back)
            go.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
        if (direction == Vector3.forward)
            go.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
    }

    private void SideImpulse() {
        Vector3 t = new Vector3(0, _rBody.velocity.y * -1, 0);
        _rBody.velocity += t;
        _canSlide = false;
    }

    private void OnDrawGizmos() {
        if (_capsuleCollider == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawLine(_capsuleCollider.bounds.center, _capsuleCollider.bounds.center - transform.up * _groundCheckDistance);
        Gizmos.DrawWireCube(_capsuleCollider.bounds.center - transform.up * _groundCheckDistance, _groundCheckPlatformSize);

        Gizmos.color = Color.magenta;
        Gizmos.DrawWireCube(_boxCollider.bounds.center + _moveDirection * _wallCheckDistance, _wallContactPlatformSize);
    }
}
