using UnityEngine;

public class JuniorState : State
{
    public JuniorState(IStateSystemHandler character, IStateSwitcher stateSwitcher, StateData data) : base(character, stateSwitcher, data) {
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
        Debug.LogError("GameOver");
    }

    public override void StateUp() {
        stateSwitcher.StateSwitch<MiddleState>();
    }
}

