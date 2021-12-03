using UnityEngine;

public abstract class InteractiveObject : MonoBehaviour
{
    protected abstract void Interaction(Collider other);
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
            Interaction(collision.collider);
    }
}
