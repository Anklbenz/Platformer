using UnityEngine;
using System;

public class PlayerInteractor : MonoBehaviour
{
    [SerializeField] private float _brickDetectionLength;
    [SerializeField] private LayerMask _brickLayerMask;

    [SerializeField] private float _enemyDetectionLength;
    [SerializeField] private LayerMask _enemyLayerMask;

    private BottomInteractionHandler _bottomInteractionHandler;
    private OverheadInteractionHandler _overheadInteractionHandler;

    private Character _character;
    private CharacterMove _characterMove;
    private BoxCollider _collider;
    private RaycastHit _rayHit;
    private bool _isBrickHit { get; set; }

    private Vector3 _colliderCenter { get => _collider.bounds.center; }
    private Vector3 _jumpOnPlatformSize { get => new Vector3(_collider.size.x, _enemyDetectionLength, _collider.size.z * transform.localScale.z); }
    private float _colliderHalfExtentsY { get => (_collider.size.y / 2) * transform.localScale.y; }
    private float _detectionBoxCenter { get => _colliderHalfExtentsY + _enemyDetectionLength / 2; }

    //private bool _rayInteractionVectorUp {
    //    get => Physics.Raycast(_colliderCenter, Vector3.up, out _rayHit, _colliderHalfExtentsY + _brickDetectionLength, _brickLayerMask);
    //}

    //private Collider[] _boxInteractionVectorDown {
    //    get => Physics.OverlapBox(_colliderCenter - new Vector3(0, _colliderHalfExtentsY + _enemyDetectionLength, 0), _jumpOnPlatformSize / 2, Quaternion.identity, _enemyLayerMask);
    //}

    //private Collider[] HitColliders (Vector3 direction, LayerMask layer, out Collider [] hitArray) {
    //   return  Physics.OverlapBox(_colliderCenter +  direction * (_colliderHalfExtentsY + _enemyDetectionLength), _jumpOnPlatformSize / 2, Quaternion.identity, layer);
    //}

    private Collider[] _topIteractions {
        get => Physics.OverlapBox(_colliderCenter + Vector3.up * (_colliderHalfExtentsY + _enemyDetectionLength), _jumpOnPlatformSize / 2, Quaternion.identity, _brickLayerMask);
    }

    private Collider[] _bottomIteractions {
        get => Physics.OverlapBox(_colliderCenter + Vector3.down * (_colliderHalfExtentsY + _enemyDetectionLength), _jumpOnPlatformSize / 2, Quaternion.identity, _enemyLayerMask);
    }

    private void Awake() {
        _collider = GetComponent<BoxCollider>();
        _character = GetComponent<Character>();
        _characterMove = GetComponent<CharacterMove>();
        _bottomInteractionHandler = new BottomInteractionHandler(_characterMove);
        _overheadInteractionHandler = new OverheadInteractionHandler();
    }

    private void FixedUpdate() {
        if (_characterMove.MovingUp && _topIteractions.Length > 0)
            _overheadInteractionHandler.Interaction(_topIteractions, _colliderCenter);
        else
            _isBrickHit = false;

        if (_characterMove.MovingDown && _bottomIteractions.Length > 0)
            _bottomInteractionHandler.Interaction(_bottomIteractions, _colliderCenter);
    }


    private void OnDrawGizmos() {
        if (_collider == null) return;
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(_colliderCenter, _colliderCenter + Vector3.up * (_brickDetectionLength + _colliderHalfExtentsY));

        Gizmos.DrawWireCube(_colliderCenter + Vector3.down * _detectionBoxCenter, _jumpOnPlatformSize);
    }
}



//private RaycastHit[] _boxInteractionVectorDown {
//    get => Physics.BoxCastAll(_colliderCenter, _jumpOnPlatformSize / 2, Vector3.down,
//                    transform.rotation, _detectionBoxDistance, _enemyLayerMask);
//}