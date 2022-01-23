using Character.States.Data;
using Interfaces;
using UnityEngine;

namespace Character.States
{
    public class MiddleState : State
    {
        public MiddleState(ICharacterComponents character, IStateSwitcher stateSwitcher, StateData data) : base(character, stateSwitcher, data){ }

        public override void Enter(){
            Data.Skin.SetActive(true);
            StateSwitcher.Resize();
        }

        public override void Exit(){
            Data.Skin.SetActive(false);
        }

        public override void StateUp(){
            StateSwitcher.MainStateSwitch<SeniorState>();
        }

        public override void StateDown(){
            StateSwitcher.MainStateSwitch<JuniorState>();
            StateSwitcher.ExtraStateFlicker();
        }
    }
}

