using UnityEngine;
using MyEnums;

public class OverheadInteractionHandler
{
    private Character _character;
    private Interactor _interactor;
    private BoxCollider _interactCollider;

    private IMoveData _moveData;

    private bool _interactActive;
    private Vector3 _direction;
    private Vector3 _colliderCenter { get => _interactCollider.bounds.center; }

    public OverheadInteractionHandler(Character character, IMoveData moveData, Vector3 direction, float inspectLength, LayerMask inspectLayer, float boxIndent = 1f) {
        _character = character;
        _moveData = moveData;
        _interactCollider = moveData.InteractCollider;
        _direction = direction;
        _interactor = new Interactor(moveData.InteractCollider, Axis.vertical, inspectLength, inspectLayer, boxIndent);
    }

    public void CollisionCheck() {
        var interactions = _interactor.InteractionOverlap(_direction);

        if (_moveData.MovingUp && !_interactActive && interactions.Length > 0) {
         
            _interactActive = true;           
            HitNearestCollider(interactions);

        } else if (interactions.Length == 0) {
            _interactActive = false;
        }
    }

    private void HitNearestCollider(Collider[] overheadColliders) {
        Debug.Log(overheadColliders.Length);
        var nearestCollider = ChooseNearestCollider(overheadColliders, _colliderCenter);

        IBrickHit hitInstance = nearestCollider.GetComponentInParent<IBrickHit>();
        if (!hitInstance.BrickInHitState)
            hitInstance?.BrickHit(_character);
    }

    private Collider ChooseNearestCollider(Collider[] overheadColliders, Vector3 playerCenter) {
        float minValue = -1;
        Collider nearestcollider = null;

        foreach (var collider in overheadColliders) {
            var dif = Mathf.Abs(playerCenter.z - collider.ClosestPoint(playerCenter).z);
            if (minValue < 0 || minValue > dif) {
                minValue = dif;
                nearestcollider = collider;
            }
        }
        return nearestcollider;
    }

    public void OnDrawGizmos(Color color) {
        _interactor.OnDrawGizmos(color);
    }
}


