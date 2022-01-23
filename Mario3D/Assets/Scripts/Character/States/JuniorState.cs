using Character.States.Data;
using Interfaces;
using UnityEngine;

namespace Character.States
{
    public class JuniorState : State
    {
        public JuniorState(ICharacterComponents character, IStateSwitcher stateSwitcher, StateData data) : base(character, stateSwitcher, data){ }

        public override void Enter(){
            Data.Skin.SetActive(true);
            StateSwitcher.Resize();
        }

        public override void Exit() {
            Data.Skin.SetActive(false);
        }

        public override void StateDown() {
            Debug.LogError("GameOver");
        }

        public override void StateUp() {
            StateSwitcher.MainStateSwitch<MiddleState>();
        }
    }
}

