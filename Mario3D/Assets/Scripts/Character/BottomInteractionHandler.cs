using UnityEngine;
using System;

public class BottomInteractionHandler
{
    const float SIDE_ENTER_DEPTH = 0.3f;

    public event Action<Vector3, int> OnJumpOnEvent;
    public InRowCounter InRowCounter = new InRowCounter();

    private bool _interactActive;
    private BoxCollider _interactCollider;

    private IMoveData _moveData;
    private Interactor _interactor;
    private Vector3 _colliderCenter { get => _interactCollider.bounds.center; }


    public BottomInteractionHandler(IMoveData moveData, Vector3 direction, float inspectLength, LayerMask inspectLayer, float boxIndent = 1f) {
        _moveData = moveData;
        _interactCollider = moveData.InteractCollider;
        _interactor = new Interactor(moveData.InteractCollider, direction, inspectLength, inspectLayer, boxIndent);
    }

    public void CollisionCheck() {
        if (_moveData.MovingDown && !_interactActive && _interactor.InteractionOverlap.Length > 0) {
            _interactActive = true;
            Interaction(_interactor.InteractionOverlap);
        } else if (_interactor.InteractionOverlap.Length == 0) {
            _interactActive = false;
        }
    }

    private void Interaction(Collider[] bottomColliders) {
        bool bounce = false;

        foreach (var collider in bottomColliders) {
            if (IsHorisontalCollsion(collider.ClosestPoint(_colliderCenter), _colliderCenter)) continue;

            IJumpOn jumpOnInstance = collider.transform.GetComponentInParent<IJumpOn>();

            if (jumpOnInstance != null) {
                if (jumpOnInstance.DoBounce) bounce = true;

                jumpOnInstance.JumpOn(_colliderCenter);
                OnJumpOnEvent?.Invoke(collider.transform.position, InRowCounter.Count);

                InRowCounter.Inreace();
            }
        }

        if (bounce) this.DoBounce();
    }

    private void DoBounce() {
        _moveData.Bounce();
    }

    private bool IsHorisontalCollsion(Vector3 closestPoint, Vector3 origin) {
        var positionDifference = origin - closestPoint;
        var overlapDirection = positionDifference.normalized;

        if (Mathf.Abs(overlapDirection.z) > SIDE_ENTER_DEPTH)
            return true;
        else
            return false;
    }

    public void OnDrawGizmos(Color color) {
        _interactor.OnDrawGizmos(color);
    }
}