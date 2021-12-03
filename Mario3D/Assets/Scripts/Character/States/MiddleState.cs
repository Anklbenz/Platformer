using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiddleState : State
{

    public MiddleState(Character character, IStateSwitcher stateSwitcher) : base(character, stateSwitcher) { }
    public override void Enter() {
        character.BallSpawnerSetActive(false);
        character.SetTrasformSize(true);
        character.CanCrush = true;
        Debug.Log("Middle");
    }

    public override void Exit() {
      
    }

    public override void Hurt() {
        character.PlayFlick();
        stateSwitcher.StateSwitch<JuniorState>();
        Debug.Log("LevelDown Middle -> Junior");

    }

    public override void LevelUp() {
        stateSwitcher.StateSwitch<SeniorState>();
        Debug.Log("LevelUp Middle -> Senior");
    }
}

