using UnityEngine;

public class OverheadInteractionHandler
{
    private Character _character;
    private Interactor _interactor;
    private BoxCollider _interactCollider;

    private IMoveData _moveData;

    private bool _interactActive;
    private Vector3 _colliderCenter { get => _interactCollider.bounds.center; }

    public OverheadInteractionHandler(Character character, IMoveData moveData, Vector3 direction, float inspectLength, LayerMask inspectLayer, float boxIndent = 1f) {
        _character = character;
        _moveData = moveData;
        _interactCollider = moveData.InteractCollider;
        _interactor = new Interactor(moveData.InteractCollider, direction, inspectLength, inspectLayer, boxIndent);
    }

    public void CollisionCheck() {
        if (_moveData.MovingUp && !_interactActive && _interactor.InteractionOverlap.Length > 0) {
            _interactActive = true;
            Interaction(_interactor.InteractionOverlap);
        } else if (_interactor.InteractionOverlap.Length == 0) {
            _interactActive = false;
        }
    }

    private void Interaction(Collider[] overheadColliders) {
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


