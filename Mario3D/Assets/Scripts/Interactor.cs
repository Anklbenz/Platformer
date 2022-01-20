using Enums;
using UnityEngine;

public class Interactor
{
    private readonly float _inspectLength;
    private readonly float _boxIndent;
    private readonly Collider _objectCollider;
    private readonly Vector3 _plane;
    private readonly LayerMask _layer;

    private Vector3 _direction;

    private Vector3 _inspectBoxCenter =>
        _objectCollider.bounds.center + _direction * (_colliderHalfSize + _inspectLength / 2);

    private Vector3 _inspectPlatformSize => Vector3.ProjectOnPlane(_objectCollider.bounds.size, _plane) * _boxIndent +
                                            _plane * _inspectLength;

    private float _colliderHalfSize => Mathf.Abs(Vector3.Dot(_plane, _objectCollider.bounds.size)) / 2;

    public Interactor(Collider objectCollider, Axis axis, float inspectLength, LayerMask layer, float boxIndent = 1){
        _plane = axis == Axis.horisontal ? Vector3.forward : Vector3.up;
        _objectCollider = objectCollider;
        _inspectLength = inspectLength;
        _boxIndent = boxIndent;
        _layer = layer;
    }

    public Collider[] InteractionOverlap(Vector3 direction){
        _direction = direction;
        return Physics.OverlapBox(_inspectBoxCenter, _inspectPlatformSize / 2, Quaternion.identity, _layer);
    }

    public bool InteractionBoxcast(Vector3 direction){
        _direction = direction;
        return Physics.BoxCast(_objectCollider.bounds.center, _inspectPlatformSize / 2, direction, Quaternion.identity,
            _inspectLength + _colliderHalfSize, _layer);
    }

    public bool InteractionBoxcast(Vector3 direction, out RaycastHit obj){
        _direction = direction;
        return Physics.BoxCast(_objectCollider.bounds.center, _inspectPlatformSize / 2, direction, out obj,
            Quaternion.identity, _inspectLength + _colliderHalfSize, _layer);
    }

    public void OnDrawGizmos(Color color){
        if (_objectCollider == null) return;
        Gizmos.color = color;
        Gizmos.DrawWireCube(_inspectBoxCenter, _inspectPlatformSize);
    }
}
