using UnityEngine;

public class JuniorState : State
{
    public JuniorState(Character character, IStateSwitcher stateSwitcher) : base(character, stateSwitcher) { }
    
    public override void Enter() {
        character.BallSpawnerSetActive(false);
        character.SetTrasformSize(false);
        character.CanCrush = false;
        Debug.Log("Junior");
    }

    public override void Exit() {
        //throw new System.NotImplementedException();
    }

    public override void Hurt() {
        Debug.LogError("GameOVER");
    }

    public override void LevelUp() {        
        stateSwitcher.StateSwitch<MiddleState>();
        Debug.Log("LevelUp Junior -> Middle");
    }
}

