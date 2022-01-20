using Enums;
using UnityEngine;

public class Interactor
{
    private readonly float _inspectLength;
    private readonly float _boxIndent;
    private readonly Collider _collider;
    private readonly Vector3 _axisPlane;
    private readonly LayerMask _layer;

    private Vector3 _direction;

    private Vector3 _inspectBoxCenter =>
        _collider.bounds.center + _direction * (_colliderHalfSize + _inspectLength / 2);

    private Vector3 _boundsCenter => _collider.bounds.center;

    private Vector3 _inspectPlatformSize => Vector3.ProjectOnPlane(_collider.bounds.size, _axisPlane) * _boxIndent +
                                            _axisPlane * _inspectLength;

    private float _colliderHalfSize => Mathf.Abs(Vector3.Dot(_axisPlane, _collider.bounds.size)) / 2;

    public Interactor(Collider collider, Axis axis, float inspectLength, LayerMask layer, float boxIndent = 1){
        _axisPlane = axis == Axis.horisontal ? Vector3.forward : Vector3.up;
        _collider = collider;
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
        return Physics.BoxCast(_boundsCenter, _inspectPlatformSize / 2, direction, Quaternion.identity,
            _inspectLength + _colliderHalfSize, _layer);
    }

    public bool InteractionBoxcast(Vector3 direction, out RaycastHit obj){
        _direction = direction;
        return Physics.BoxCast(_boundsCenter, _inspectPlatformSize / 2, direction, out obj,
            Quaternion.identity, _inspectLength + _colliderHalfSize, _layer);
    }

    public void OnDrawGizmos(Color color){
        if (_collider == null) return;
        Gizmos.color = color;
        Gizmos.DrawWireCube(_inspectBoxCenter, _inspectPlatformSize);
    }
}