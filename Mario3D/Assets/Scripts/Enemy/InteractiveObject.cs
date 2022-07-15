using Character;
using Character.States;
using Interfaces;
using UnityEngine;

namespace Enemy
{
    public abstract class InteractiveObject : MonoBehaviour
    {
        protected abstract void Interaction(IStateMethods stateHandler, Vector3 pos);

        protected void OnTriggerEnter(Collider other) {
            var character = other.GetComponent<CharacterHandler>();
            if (character == null) return;
            
            var state = character.StateMethods;
            Interaction(state, other.bounds.center);
        }
    }
}
