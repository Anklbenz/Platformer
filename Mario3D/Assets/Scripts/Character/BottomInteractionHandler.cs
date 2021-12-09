using UnityEngine;
using System;

public class BottomInteractionHandler
{
    const float SIDE_ENTER_DEPTH = 0.3f;
    public event Action<Vector3, int> OnJumpOnEvent;
    private IMoveProvider _moveProvider;
    private int _jumpInRowCount=0;

    public BottomInteractionHandler(IMoveProvider moveProvider) {
        _moveProvider = moveProvider;
    }

    public void Interaction(Collider[] bottomColliders, Vector3 patrentCenter) {
        bool bounce = false;

        foreach (var collider in bottomColliders) {
            if (IsHorisontalCollsion(collider.ClosestPoint(patrentCenter), patrentCenter)) continue;

            IJumpOn jumpOnInstance = collider.transform.GetComponentInParent<IJumpOn>();

            if (jumpOnInstance != null) {
                if (jumpOnInstance.DoBounce) bounce = true;
                jumpOnInstance.JumpOn(patrentCenter);
                OnJumpOnEvent?.Invoke(collider.transform.position, _jumpInRowCount);
                this.JumpInRowCountInreace();
            }
        }
        if (bounce) this.DoBounce();
    }

    public void JumpInRowCountReset() {
        _jumpInRowCount = 0;
    }

    private void JumpInRowCountInreace() {
        _jumpInRowCount ++;
    }


    private void DoBounce() {
        _moveProvider.Bounce();
    }

    private bool IsHorisontalCollsion(Vector3 closestPoint, Vector3 origin) {
        var positionDifference = origin - closestPoint;
        var overlapDirection = positionDifference.normalized;

        if (Mathf.Abs(overlapDirection.z) > SIDE_ENTER_DEPTH)
            return true;
        else
            return false;
    }
}