using Enums;
using UnityEngine;

public class Interacting : InteractingBase
{
    private readonly Collider _collider;

    protected override Vector3 InspectBoxCenter => BoundsCenter + Direction * (ColliderHalfSize + InspectLength / 2);

    protected override Vector3 BoundsCenter => _collider.bounds.center;

    protected override Vector3 InspectPlatformSize => Vector3.ProjectOnPlane(_collider.bounds.size, AxisPlane) * BorderIndent + AxisPlane * InspectLength;

    protected override float ColliderHalfSize => Mathf.Abs(Vector3.Dot(AxisPlane, _collider.bounds.size)) / 2;

    public Interacting(Collider collider, Axis axis, float inspectLength, LayerMask layer, float borderIndent = 1,
        bool ignoreTrigger = false)
        : base(axis, inspectLength, layer, borderIndent, ignoreTrigger){
        _collider = collider;
    }

    public Collider[] InteractionOverlap(Vector3 direction){
        Direction = direction;
        return Physics.OverlapBox(InspectBoxCenter, InspectPlatformSize / 2, Quaternion.identity, Layer, TriggerInteraction);
    }

    public bool InteractionBoxcast(Vector3 direction){
        Direction = direction;
        return Physics.BoxCast(BoundsCenter, InspectPlatformSize / 2, direction, Quaternion.identity,
            InspectLength + ColliderHalfSize, Layer, TriggerInteraction);
    }

    public bool InteractionBoxcast(Vector3 direction, out RaycastHit obj){
        Direction = direction;
        return Physics.BoxCast(BoundsCenter, InspectPlatformSize / 2, direction, out obj,
            Quaternion.identity, InspectLength + ColliderHalfSize, Layer, TriggerInteraction);
    }

    public void OnDrawGizmos(Color color){
        if (_collider == null) return;
        Gizmos.color = color;
        Gizmos.DrawWireCube(InspectBoxCenter, InspectPlatformSize);
    }
}