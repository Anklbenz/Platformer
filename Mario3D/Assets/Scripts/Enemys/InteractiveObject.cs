﻿using Character;
using Character.States;
using UnityEngine;

public abstract class InteractiveObject : MonoBehaviour
{
    protected abstract void Interaction(StateSystem state, Vector3 pos);

    protected void OnTriggerEnter(Collider other) {
        var character = other.GetComponent<CharacterHandler>();

        if (character != null) {
            var state = character.StateSystem;
            Interaction(state, other.bounds.center); 
        }
    }
}
