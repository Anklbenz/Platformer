using Character.States.Data;
using Enums;
using Interfaces;
using UnityEngine;

namespace Character.States
{
    public class MiddleState : State
    {
        public MiddleState(IStateSwitcher stateSwitcher, StateData data) : base(stateSwitcher, data){
        } 

        public override void StateUp(){
            StateSwitcher.StateSwitch<SeniorState>();
        }

        public override void StateDown(){
            StateSwitcher.StateSwitch<JuniorState>();
            StateSwitcher.ExtraStateSwitch(ExtraState.FlickerState);
        }
    }
}
