using UnityEngine;

public class BottomInteractionHandler 
{   
    const float SIDE_ENTER_DEPTH = 0.3f;
    private CharacterMove _characterMove;

    public BottomInteractionHandler(CharacterMove characterMove) {
        _characterMove = characterMove;
    }

    public void Interaction (Collider[] bottomColliders, Vector3 patrentCenter) {
        bool bounce = false;
        
        foreach (var collider in bottomColliders) {
            if (HorisontalCollsion(collider.ClosestPoint(patrentCenter), patrentCenter)) return;

            IJumpOn instance = collider.transform.GetComponentInParent<IJumpOn>();

            if (instance != null) {
                if (instance.DoBounce == true)
                    bounce = true;
                instance.JumpOn(patrentCenter);
            }
        }
        if (bounce)
            _characterMove.Bounce();
    }

    private bool HorisontalCollsion(Vector3 closestPoint, Vector3 origin) {
        var positionDifference = origin - closestPoint;
        var overlapDirection = positionDifference.normalized;

        if (Mathf.Abs(overlapDirection.z) > SIDE_ENTER_DEPTH)
            return true;
        else
            return false;
    }
}
