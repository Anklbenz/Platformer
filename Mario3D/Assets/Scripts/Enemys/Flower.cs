
using UnityEngine;

public sealed class Flower : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            Interaction(other);
    }

    private void Interaction(Collider other)
    {
            other.GetComponent<StateHandler>()?.LevelUp();
            Destroy(gameObject);
    }
}
