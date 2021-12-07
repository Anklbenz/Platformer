using UnityEngine;

public class HitCatcher<T> where T : MonoBehaviour
{
    private Vector3 _halfPlatformSize;
    private float _checkDistance;
    private LayerMask _mask;

    public HitCatcher(Vector3 platformSizeExtents, float checkDistance, LayerMask mask) {
        _halfPlatformSize = platformSizeExtents;
        _checkDistance = checkDistance;
        _mask = mask;
    }

    public bool CollisionBox(Vector3 boxCenter, Vector3 direction, out T specialInteractionType) {
        if (Physics.BoxCast(boxCenter, _halfPlatformSize, direction, out RaycastHit _rayHit, Quaternion.identity, _checkDistance, _mask)) {
            specialInteractionType = _rayHit.collider.GetComponentInParent<T>();
            return true;
        } else {
            specialInteractionType = null;
            return false;
        }
    }

    public Collider[] CollisionOverlapBox (Vector3 boxCenter, Vector3 direction, Vector3 plaformSizeExtent ) {
       return  Physics.OverlapBox(boxCenter, plaformSizeExtent, Quaternion.identity, _mask);
    }

}


    //public bool rayCollisionExist(Vector3 boxCenter, Vector3 direction, out Collider hitCollider) {
    //    Physics.Raycast(boxCenter, direction, out RaycastHit _rayHit, _checkDistance, _mask);
    //    hitCollider = _rayHit.collider;
    //    return hitCollider != null;
    //}