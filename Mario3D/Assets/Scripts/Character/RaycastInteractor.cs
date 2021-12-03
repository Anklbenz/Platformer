using UnityEngine;

public class RaycastInteractor
{      
    private Vector3 center;
    private Vector3 direction;
    private float rayLength;
    private LayerMask layerMask;
    private RaycastHit _rayHit;   


    public RaycastInteractor(Vector3 center, Vector3 direction, float rayLength, LayerMask layerMask) {
        this.center = center;
        this.direction = direction;
        this.rayLength = rayLength;
        this.layerMask = layerMask;
    }
    
    private bool _interaction {
        get {
            return Physics.Raycast(center, direction, out _rayHit, rayLength, layerMask);
        }
    }

    public bool InteractionCheck<T>(out T targetClass) where T: MonoBehaviour {      
        if (_interaction) {
            targetClass = _rayHit.transform.GetComponentInParent<T>();
            if (targetClass != null)           
                return true;
        }
        targetClass = null;
        return false;
    }

    public void DrawGizmos() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(center, center + direction * rayLength);
    }

}
