using Character.States.Data;
using Interfaces;
using UnityEngine;

namespace Character.States
{
    public class SeniorState : State
    {
        public SeniorState(ICharacterComponents character, IStateSwitcher stateSwitcher, StateData data) : base(character, stateSwitcher,
            data){
        }

        public override void Enter(){
            Data.Skin.SetActive(true);
            StateSwitcher.Resize();
        }

        public override void Exit(){
            Data.Skin.SetActive(false);
        }

        public override void StateDown(){
            StateSwitcher.MainStateSwitch<JuniorState>();
        }

        public override void StateUp(){
            Debug.Log("Senior -> Max State Reached");
        }
    }
}

