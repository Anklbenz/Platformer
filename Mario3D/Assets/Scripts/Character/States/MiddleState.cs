using Character.States.Data;
using Interfaces;
using UnityEngine;

namespace Character.States
{
    public class MiddleState : State
    {
        public MiddleState(IStateSystemHandler character, IStateSwitcher stateSwitcher, StateData data) : base(
            character, stateSwitcher, data){
            skinGameObject = Object.Instantiate(Data.SkinGameObject, character.SkinsParent);
            skinGameObject.SetActive(false);
        }

        public override void Enter(){
            skinGameObject.SetActive(true);
            character.MainCollider.height = Data.ColliderSize.y;
            character.MainCollider.radius = Data.ColliderSize.x / 2;
        }

        public override void Exit(){
            skinGameObject.SetActive(false);
        }

        public override void StateUp(){
            stateSwitcher.StateSwitch<SeniorState>();
        }

        public override void StateDown(){
            stateSwitcher.StateSwitch<JuniorState>(); 
            stateSwitcher.HurtExtraState();
        }
    }
}

