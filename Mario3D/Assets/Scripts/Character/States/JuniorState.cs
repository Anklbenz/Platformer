using UnityEngine;

public class JuniorState : State
{
    public JuniorState(Character character, IStateSwitcher stateSwitcher, StateData data) : base(character, stateSwitcher, data) {}    

    public override void Enter() {
        character.UpdateStateData(data);     
        Debug.Log("Junior");
    }

    public override void Exit() {
        //  Debug.LogError("GameOVER");
    }

    public override void StateUp() {        
        stateSwitcher.StateSwitch<MiddleState>();
        Debug.Log("LevelUp Junior -> Middle");
    }
}

