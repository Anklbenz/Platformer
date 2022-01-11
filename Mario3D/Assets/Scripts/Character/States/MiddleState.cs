﻿using UnityEngine;

public class MiddleState : State
{
    public MiddleState(IStateSystemHandler character, IStateSwitcher stateSwitcher, StateData data) : base(character, stateSwitcher, data) {
        skinGameObject = Object.Instantiate(Data.SkinGameObject, character.SkinsParent);
        skinGameObject.SetActive(false);
    }

    public override void Enter() {
        skinGameObject.SetActive(true);
        character.MainCollider.size = Data.ColliderSize;
    }

    public override void Exit() {
       
        skinGameObject.SetActive(false);
    }

    public override void StateDown() {
        stateSwitcher.StateSwitch<JuniorState>();
        
    }

    public override void StateUp() {
        stateSwitcher.StateSwitch<SeniorState>();
    }
}

