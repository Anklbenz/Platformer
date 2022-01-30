using System.Collections;
using System.Collections.Generic;
using Enums;
using UnityEngine;

public class InteractingRect : InteractingBase
{
    private readonly Vector3 _rectSize;
    private Vector3 _rectCenter;
    private float _indentFromRect;

    protected override Vector3 InspectBoxCenter => BoundsCenter + Direction * (ColliderHalfSize + _indentFromRect + InspectLength / 2);
    protected override Vector3 InspectPlatformSize => Vector3.ProjectOnPlane(_rectSize, AxisPlane) * BorderIndent + AxisPlane * InspectLength;
    protected override float ColliderHalfSize => Mathf.Abs(Vector3.Dot(AxisPlane, _rectSize)) / 2;
    protected override Vector3 BoundsCenter => _rectCenter;

    public InteractingRect(Vector3 rectSize, Axis axis, float inspectLength, LayerMask layer, float borderIndent = 1,
        bool ignoreTrigger = false, float indentFromRect =0) :
        base(axis, inspectLength, layer, borderIndent, ignoreTrigger){
        _rectSize = rectSize;
        _indentFromRect = indentFromRect;
    }

    public Collider[] InteractionOverlap(Vector3 direction, Vector3 rectCenter){
        Direction = direction;
        _rectCenter = rectCenter;
        return Physics.OverlapBox(InspectBoxCenter, InspectPlatformSize / 2, Quaternion.identity, Layer, TriggerInteraction);
    }
    
    public void OnDrawGizmos(Color color){
        Gizmos.color = color;
        Gizmos.DrawWireCube(InspectBoxCenter, InspectPlatformSize);
    }
}
