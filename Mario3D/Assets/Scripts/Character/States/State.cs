using System.Threading.Tasks;
using Character.States.Data;
using Interfaces;
using UnityEngine;

namespace Character.States
{
    public abstract class State
    {
        public StateData Data{ get; private set; }

        protected GameObject skinGameObject;
        public GameObject Skin => skinGameObject;
        protected readonly ICharacterComponets character;
        protected readonly IStateSwitcher stateSwitcher;

        protected State(ICharacterComponets character, IStateSwitcher stateSwitcher, StateData data){
            this.character = character;
            this.stateSwitcher = stateSwitcher;
            this.Data = data;
        }

        public abstract void Enter();
        public abstract void Exit();
        public abstract void StateDown();
        public abstract void StateUp();
    }
}
