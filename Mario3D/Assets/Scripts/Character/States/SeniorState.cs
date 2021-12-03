using UnityEngine;

public class SeniorState : State
{
    public SeniorState(Character character, IStateSwitcher stateSwitcher) : base(character, stateSwitcher) { }
    public override void Enter() {
        character.BallSpawnerSetActive(true);
        character.CanCrush = true;
        Debug.Log("Senior");
    }

    public override void Exit() {
      
    }

    public override void Hurt() {
        character.PlayFlick();
        stateSwitcher.StateSwitch<JuniorState>();
        Debug.Log("LevelDown Senior -> Middle");
    }

    public override void LevelUp() {
        throw new System.NotImplementedException();
    }
}

