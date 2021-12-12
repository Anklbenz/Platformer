using UnityEngine;

public class Interactor
{
    private BoxCollider _objectCollider;
    private float _inspectLength;
    private Vector3 _direction;
    private float _boxIndent;
    private LayerMask _layer;

    //private float _scale { get => Vector3.Dot(_direction, _localScale); }
    //private Vector3 _inspectBoxCenter { get => _objectCollider.bounds.center + _direction * (_colliderHalfSize * _scale + _inspectLength / 2); }
    private Vector3 _overlapBoxCenter { get => _objectCollider.bounds.center + _direction * (_colliderHalfSize + _inspectLength / 2); }

    private Vector3 _inspectPlatformSize { get => (Vector3.ProjectOnPlane(_objectCollider.bounds.size, _direction) + _direction * _inspectLength) * _boxIndent; }
    private float _colliderHalfSize { get => Mathf.Abs(Vector3.Dot(_direction, _objectCollider.size)) / 2; }

    public Interactor(BoxCollider objectCollider, Vector3 direction, float inspectLength, LayerMask layer, float boxIndent = 1) {
        _objectCollider = objectCollider;
        _direction = direction;
        _inspectLength = inspectLength;
        _boxIndent = boxIndent;
        _layer = layer;
    }

    private Vector3 BoxcastOrigin(Vector3 directon) {
        return _objectCollider.bounds.center + directon * (Mathf.Abs(Vector3.Dot(directon, _objectCollider.size) / 2));
    }

    public Collider[] InteractionOverlap {
        get => Physics.OverlapBox(_overlapBoxCenter, _inspectPlatformSize / 2, Quaternion.identity, _layer);
    }

    public bool InteractionBoxcast(Vector3 direction) {
         return  Physics.BoxCast(BoxcastOrigin(direction), _inspectPlatformSize / 2, Quaternion.identity, 3  , _layer)) ;

    }

    //private Vector3 InpectBoxCenter(Vector3 localScale) {
    //    float _scale = Vector3.Dot(_direction, localScale);
    //    return _objectCollider.bounds.center + _direction * (_colliderHalfSize * _scale + _inspectLength / 2);
    //}

    public void OnDrawGizmos(Color color) {
        if (_objectCollider == null) return;
        Gizmos.color = color;
        Gizmos.DrawWireCube(_overlapBoxCenter, _inspectPlatformSize);
    }
}

