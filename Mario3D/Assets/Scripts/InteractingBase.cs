using UnityEngine;
using Enums;

public abstract class InteractingBase
{
    protected readonly float InspectLength;
    protected readonly float BorderIndent;
    protected readonly Vector3 AxisPlane;
    protected readonly LayerMask Layer;
    protected readonly QueryTriggerInteraction TriggerInteraction;
    protected Vector3 Direction;

    protected abstract Vector3 InspectPlatformSize{ get; }
    protected abstract Vector3 InspectBoxCenter{ get; }
    protected abstract float ColliderHalfSize{ get; }
    protected abstract Vector3 BoundsCenter{ get; }

    protected InteractingBase(Axis axis, float inspectLength, LayerMask layer, float borderIndent, bool ignoreTrigger){

        AxisPlane = axis == Axis.Horizontal ? Vector3.forward : Vector3.up;
        // _collider = collider;
        InspectLength = inspectLength;
        BorderIndent = borderIndent;
        Layer = layer;
        TriggerInteraction = ignoreTrigger == true ? QueryTriggerInteraction.Ignore : QueryTriggerInteraction.UseGlobal;
    }
}