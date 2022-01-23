using Character.States.Data;
using Interfaces;
using UnityEngine;

namespace Character.States
{
    public abstract class State
    {
        public StateData Data{ get; protected set; }
        protected readonly IStateSwitcher StateSwitcher;

        protected State(ICharacterComponents character, IStateSwitcher stateSwitcher, StateData data){
            StateSwitcher = stateSwitcher;
            Data = data;
            Data.Skin = Object.Instantiate(Data.SkinObject, character.SkinsParent);
            Data.Skin.SetActive(false);
        }

        public abstract void Enter();
        public abstract void Exit();
        public abstract void StateDown();
        public abstract void StateUp();

    }
}
