using UnityEngine;

public class RayInteractor : MonoBehaviour
{
    const float SIDE_ENTER_DEPTH = 0.3f;

    [SerializeField] private float _brickDetectionLength;
    [SerializeField] private LayerMask _brickLayerMask;

    [SerializeField] private float _enemyDetectionLength;
    [SerializeField] private LayerMask _enemyLayerMask;
    private Vector3 _jumpOnPlatformSize;
    private float _detectionBoxDistance;
    private float _colliderExtendsAxisY;

    private Character _character;
    private CharacterMove _characterMove;
    private BoxCollider _collider;
    private RaycastHit _rayHit;
    private bool _isBrickHit { get; set; }
    private Vector3 _colliderCenter { get => _collider.bounds.center; }

    private bool _rayInteractionVectorUp {
        get => Physics.Raycast(_colliderCenter, Vector3.up, out _rayHit, _colliderExtendsAxisY + _brickDetectionLength, _brickLayerMask);
    }

    private Collider[] _boxInteractionVectorDown {
        get => Physics.OverlapBox(_colliderCenter - new Vector3(0, _colliderExtendsAxisY + _enemyDetectionLength, 0), _jumpOnPlatformSize / 2, Quaternion.identity, _enemyLayerMask);
    }

    private void Awake() {
        _collider = GetComponent<BoxCollider>();
        _character = GetComponent<Character>();
        _characterMove = GetComponent<CharacterMove>();
        this.UpdateBCastDistancesOnLocalScale();
        _character.OnScaleChangedEvent += UpdateBCastDistancesOnLocalScale;
    }

    private void FixedUpdate() {
        if (_characterMove.MovingUp && _rayInteractionVectorUp)
            BrickHitTest();
        else
            _isBrickHit = false;

        if (_boxInteractionVectorDown.Length > 0 && _characterMove.MovingDown)
            EnemyHitTest();
    }

    private void EnemyHitTest() {
        bool bounce = false;

        foreach (var el in _boxInteractionVectorDown) {
            if (CastBoxHitEnemySide(el.ClosestPoint(_colliderCenter), _colliderCenter)) return;

            ActiveEnemy enemy = el.transform.GetComponentInParent<ActiveEnemy>();

            if (enemy != null) {
                if (enemy.DoBounce == true)
                    bounce = true;
                enemy.JumpOn(_characterMove);
            }
        }
        if (bounce)
            _characterMove.Bounce();
    }

    private void BrickHitTest() {
        if (_isBrickHit) return;

        BrickBox brick = _rayHit.transform.GetComponentInParent<BrickBox>();
        if (brick != null) {
            _isBrickHit = true;
            brick.BrickHit(_character);
        }
    }

    private bool CastBoxHitEnemySide(Vector3 closestPoint, Vector3 origin) {
        var positionDifference = origin - closestPoint;
        var overlapDirection = positionDifference.normalized;

        if (Mathf.Abs(overlapDirection.z) > SIDE_ENTER_DEPTH)
            return true;
        else
            return false;
    }

    public void UpdateBCastDistancesOnLocalScale() {
        _jumpOnPlatformSize = new Vector3(_collider.size.x,
                                          _enemyDetectionLength,
                                          _collider.size.z * transform.localScale.z);

        _colliderExtendsAxisY = (_collider.size.y / 2) * transform.localScale.y;
        _detectionBoxDistance = _colliderExtendsAxisY + _enemyDetectionLength / 2;
    }

    private void OnDisable() {
        _character.OnScaleChangedEvent -= UpdateBCastDistancesOnLocalScale;
    }

    private void OnDrawGizmos() {
        if (_collider == null) return;
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(_colliderCenter, _colliderCenter + Vector3.up * (_brickDetectionLength + _colliderExtendsAxisY));

        Gizmos.DrawWireCube(_colliderCenter + Vector3.down * _detectionBoxDistance, _jumpOnPlatformSize);
    }
}



//private RaycastHit[] _boxInteractionVectorDown {
//    get => Physics.BoxCastAll(_colliderCenter, _jumpOnPlatformSize / 2, Vector3.down,
//                    transform.rotation, _detectionBoxDistance, _enemyLayerMask);
//}