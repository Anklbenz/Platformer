using UnityEngine;

public class OverheadInteractionHandler
{
    private Character _character;

    public OverheadInteractionHandler(Character character) {
        _character = character;
    }

    public void Interaction(Collider[] overheadColliders, Vector3 playerCenter) {
        Debug.Log(overheadColliders.Length);
        var nearestCollider = ChooseNearestCollider(overheadColliders, playerCenter);

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
}


