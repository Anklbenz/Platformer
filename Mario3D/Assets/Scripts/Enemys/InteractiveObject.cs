using UnityEngine;

public abstract class InteractiveObject : MonoBehaviour
{
    protected abstract void Interaction(Collider other);

    private void OnCollisionEnter(Collision collision)
    {
        Character character = collision.collider.GetComponent<Character>();
        if (character)
            Interaction(collision.collider);
    }
}
