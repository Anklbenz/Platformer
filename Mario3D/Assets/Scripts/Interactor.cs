using Enums;
using UnityEngine;

public class Interactor
{
    private readonly float _inspectLength;
    private readonly float _boxIndent;
    private readonly Collider _collider;
    private readonly Vector3 _axisPlane;
    private readonly LayerMask _layer;
    private readonly QueryTriggerInteraction _triggerInteraction;

    private Vector3 _direction;

    private Vector3 InspectBoxCenter =>  _collider.bounds.center + _direction * (ColliderHalfSize + _inspectLength / 2);

    private Vector3 BoundsCenter => _collider.bounds.center;

    private Vector3 InspectPlatformSize => Vector3.ProjectOnPlane(_collider.bounds.size, _axisPlane) * _boxIndent + _axisPlane * _inspectLength;

    private float ColliderHalfSize => Mathf.Abs(Vector3.Dot(_axisPlane, _collider.bounds.size)) / 2;

    public Interactor(Collider collider, Axis axis, float inspectLength, LayerMask layer, float boxIndent = 1, bool ignoreTrigger = false){
        _axisPlane = axis == Axis.Horizontal ? Vector3.forward : Vector3.up;
        _collider = collider;
        _inspectLength = inspectLength;
        _boxIndent = boxIndent;
        _layer = layer;
        _triggerInteraction = ignoreTrigger == true ? QueryTriggerInteraction.Ignore : QueryTriggerInteraction.UseGlobal;
    }

    public Collider[] InteractionOverlap(Vector3 direction){
        _direction = direction;
        return Physics.OverlapBox(InspectBoxCenter, InspectPlatformSize / 2, Quaternion.identity, _layer, _triggerInteraction);
    }

    public bool InteractionBoxcast(Vector3 direction){
        _direction = direction;
        return Physics.BoxCast(BoundsCenter, InspectPlatformSize / 2, direction, Quaternion.identity,
            _inspectLength + ColliderHalfSize, _layer, _triggerInteraction);
    }

    public bool InteractionBoxcast(Vector3 direction, out RaycastHit obj){
        _direction = direction;
        return Physics.BoxCast(BoundsCenter, InspectPlatformSize / 2, direction, out obj,
            Quaternion.identity, _inspectLength + ColliderHalfSize, _layer, _triggerInteraction);
    }

    public void OnDrawGizmos(Color color){
        if (_collider == null) return;
        Gizmos.color = color;
        Gizmos.DrawWireCube(InspectBoxCenter, InspectPlatformSize);
    }
}