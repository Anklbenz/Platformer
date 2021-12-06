using UnityEngine;

public class HitCatcher 
{
    private Vector3 _halfPlatformSize;
    private float _checkDistance;
    private LayerMask _mask;

    public HitCatcher(Vector3 halfPlatformSize, float checkDistance, LayerMask mask) {
        _halfPlatformSize = halfPlatformSize;
        _checkDistance = checkDistance;
        _mask = mask;
    }

    public bool collisionExist( Vector3 boxCenter, Vector3 direction,out Collider hitCollider) {
        Physics.BoxCast(boxCenter, _halfPlatformSize, direction, out RaycastHit _rayHit, Quaternion.identity, _checkDistance, _mask);
        hitCollider = _rayHit.collider;
        return hitCollider != null;
    }

    public bool rayCollisionExist(Vector3 boxCenter, Vector3 direction, out Collider hitCollider) {
        Physics.Raycast(boxCenter, direction, out RaycastHit _rayHit, _checkDistance, _mask);
        hitCollider = _rayHit.collider;
        return hitCollider != null;
    }

    //private Collider[] _boxInteractionVectorDown {
    //    get => Physics.OverlapBox(_colliderCenter - new Vector3(0, _colliderExtendsAxisY + _enemyDetectionLength, 0), _jumpOnPlatformSize / 2, Quaternion.identity, _enemyLayerMask);
    //}

}


