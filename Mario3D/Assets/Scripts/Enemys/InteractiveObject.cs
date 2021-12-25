using UnityEngine;

public abstract class InteractiveObject : MonoBehaviour
{
    protected abstract void Interaction(StateHandler state, Vector3 pos);

    protected void OnTriggerEnter(Collider other) {
        var character = other.GetComponent<Character>();

        if (character)
            Interaction(character.StateHandler, other.bounds.center);
    }
}
