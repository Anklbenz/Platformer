using Character.States.Data;
using Interfaces;
using UnityEngine;

namespace Character.States
{
    public abstract class State
    {
        public StateData Data{ get; protected set; }
        protected readonly IStateSwitcher StateSwitcher;

        protected State(IStateSwitcher stateSwitcher, StateData data){
            StateSwitcher = stateSwitcher;
            Data = data;
        }

        public abstract void StateDown();
        public abstract void StateUp();

    }
}
