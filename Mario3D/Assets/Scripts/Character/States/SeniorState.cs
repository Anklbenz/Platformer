using Character.States.Data;
using Enums;
using Interfaces;
using UnityEngine;

namespace Character.States
{
    public class SeniorState : State
    {
        public SeniorState( IStateSwitcher stateSwitcher, StateData data) : base(stateSwitcher,
            data){
        }

        public override void StateDown(){
            StateSwitcher.StateSwitch<JuniorState>();
            StateSwitcher.ExtraStateSwitch(ExtraState.FlickerState);
        }

        public override void StateUp(){
            Debug.Log("Senior -> Max State Reached");
        }
    }
}

