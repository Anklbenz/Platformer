using UnityEngine;
using MyEnums;

public class Interactor
{
    private readonly BoxCollider _objectCollider;
    private Vector3 _direction;
    private readonly Vector3 _inspectPlatformSize;
    private readonly LayerMask _layer;
    private readonly float _inspectLength;
    private readonly float _boxIndent;
    private readonly float _colliderHalfSize;

    private Vector3 _inspectBoxCenter { get => _objectCollider.bounds.center + _direction * (_colliderHalfSize + _inspectLength / 2);  }

    public Interactor(BoxCollider objectCollider, Axis axis, float inspectLength, LayerMask layer, float boxIndent = 1) {
        Vector3 direction = axis == Axis.horisontal ? Vector3.forward : Vector3.up;

        _objectCollider = objectCollider;
        _inspectLength = inspectLength;
        _boxIndent = boxIndent;
        _layer = layer;     
        _inspectPlatformSize = InspectPlatformSize(direction);
        _colliderHalfSize = ColliderHalfSize(direction);
    }

    private float ColliderHalfSize(Vector3 direction) {
        return Mathf.Abs(Vector3.Dot(direction, _objectCollider.size)) / 2;
    }

    private Vector3 InspectPlatformSize(Vector3 direction) {
        return (Vector3.ProjectOnPlane(_objectCollider.bounds.size, direction) * _boxIndent + direction * _inspectLength) ;
    }

    public Collider[] InteractionOverlap(Vector3 direction) {
        _direction = direction;
        return Physics.OverlapBox(_inspectBoxCenter, _inspectPlatformSize / 2, Quaternion.identity, _layer);
    }
    
    public bool InteractionBoxcast(Vector3 direction) {
        _direction = direction;
        return Physics.BoxCast(_inspectBoxCenter, _inspectPlatformSize / 2, direction, Quaternion.identity, 0, _layer);
    }


    public bool InteractionBoxcast(Vector3 direction, out RaycastHit obj) {
        _direction = direction;
        return Physics.BoxCast(_objectCollider.bounds.center, _inspectPlatformSize / 2, direction, out obj, Quaternion.identity, _inspectLength + _colliderHalfSize, _layer);
    }

    public void OnDrawGizmos(Color color) {
        if (_objectCollider == null) return;
        Gizmos.color = color;
        Gizmos.DrawWireCube(_inspectBoxCenter, _inspectPlatformSize);
    }
}