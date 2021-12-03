using MyEnums;
using UnityEngine;

public sealed class JumpOnPlatform : MonoBehaviour
{
    public void SetActive(bool state) {
        enabled = state;
    }

    private void OnTriggerEnter(Collider other) {
        if (enabled) {
            if (other.CompareTag("PlayerLegHitPoint")) {
                //GetComponentInParent<ActiveEnemy>().JumpOn(other);
               // this.SetActive(false);
            }
        }
    }

}
