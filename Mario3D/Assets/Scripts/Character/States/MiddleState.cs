using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiddleState : State
{

    public MiddleState(Character character, IStateSwitcher stateSwitcher, StateData data) : base(character, stateSwitcher, data) {}

    public override void Enter() {
        character.UpdateStateData(data);
        Debug.Log("Middle");
    }

    public override void Exit() {
        character.PlayFlick();
        stateSwitcher.StateSwitch<JuniorState>();
        Debug.Log("LevelDown Middle -> Junior");
    }

    public override void StateUp() {
        stateSwitcher.StateSwitch<SeniorState>();
        Debug.Log("LevelUp Middle -> Senior");
    }
}

