using UnityEngine;

public class InteractorHandler : MonoBehaviour
{
    const float OVERHEAD_BOX_INDENT = 0.93f;
    [SerializeField] private float _overInteractionLength;
    [SerializeField] private LayerMask _overInteractionLayer;

    [SerializeField] private float _underInteractionLength;
    [SerializeField] private LayerMask _underInteractionlayer;

    private OverheadInteractionHandler _overInteractionHandler;
    public BottomInteractionHandler _underInteractionHandler;

    private Character _character;
    private IMoveProvider _moveProvider;
    private BoxCollider _playerCollider;

    private bool _overInteractionInProcess = false;
    private bool _underInteractionInProcess = false;

    private Vector3 _overPlatformSize { get => new Vector3(_playerCollider.size.x, _overInteractionLength, _playerCollider.size.z * transform.localScale.z) * OVERHEAD_BOX_INDENT; }
    private Vector3 _underPlatformSize { get => new Vector3(_playerCollider.size.x, _underInteractionLength, _playerCollider.size.z * transform.localScale.z); }

    private Vector3 _colliderCenter { get => _playerCollider.bounds.center; }
    private float _colliderHalfExtentsY { get => (_playerCollider.size.y / 2) * transform.localScale.y; }

    private float _overBoxCenter { get => _colliderHalfExtentsY + _overInteractionLength / 2; }
    private float _underBoxCenter { get => _colliderHalfExtentsY + _underInteractionLength / 2; }


    private Collider[] _topIteractions {
        get => Physics.OverlapBox(_colliderCenter + Vector3.up * _overBoxCenter, _overPlatformSize / 2, Quaternion.identity, _overInteractionLayer);
    }

    private Collider[] _bottomIteractions {
        get => Physics.OverlapBox(_colliderCenter + Vector3.down * _underBoxCenter, _underPlatformSize / 2, Quaternion.identity, _underInteractionlayer);
    }

    private void Awake() {
        _playerCollider = GetComponent<BoxCollider>();
        _character = GetComponent<Character>();
        _moveProvider = GetComponent<IMoveProvider>();
        _underInteractionHandler = new BottomInteractionHandler(_moveProvider);
        _overInteractionHandler = new OverheadInteractionHandler(_character);
    }

    private void FixedUpdate() {
        this.OverInercat();
        this.UnderInercat();

        if (_moveProvider.IsGrounded)
            _underInteractionHandler.JumpInRowCountReset();
    }

    private void OverInercat() {
        if (_moveProvider.MovingUp && !_overInteractionInProcess && _topIteractions.Length > 0) {
            _overInteractionInProcess = true;
            _overInteractionHandler.Interaction(_topIteractions, _colliderCenter);
        } else if (_topIteractions.Length == 0) {
            _overInteractionInProcess = false;
        }
    }

    private void UnderInercat() {
        if (_moveProvider.MovingDown && !_underInteractionInProcess && _bottomIteractions.Length > 0) {
            _underInteractionInProcess = true;
            _underInteractionHandler.Interaction(_bottomIteractions, _colliderCenter);
        } else if (_bottomIteractions.Length == 0) {
            _underInteractionInProcess = false;
        }
    }

    private void OnDrawGizmos() {
        if (_playerCollider == null) return;
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(_colliderCenter, _colliderCenter + Vector3.up * (_overInteractionLength + _colliderHalfExtentsY));

        Gizmos.DrawWireCube(_colliderCenter + Vector3.up * _overBoxCenter, _overPlatformSize);
        Gizmos.DrawWireCube(_colliderCenter + Vector3.down * _underBoxCenter, _underPlatformSize);
    }
}



