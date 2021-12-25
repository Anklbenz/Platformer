using UnityEngine;

public class SeniorState : State
{
    public SeniorState(Character character, IStateSwitcher stateSwitcher, StateData data) : base(character, stateSwitcher, data) {}

    public override void Enter() {
        character.UpdateStateData(data);
        Debug.Log("Senior");
    }

    public override void Exit() {
        character.PlayFlick();
        stateSwitcher.StateSwitch<JuniorState>();
        Debug.Log("LevelDown Senior -> junior");
    }

    public override void StateUp() {
        Debug.Log("Senior -> Max State Reached");
    }
}

