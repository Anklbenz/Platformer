/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Character.States.Data;
using Enemy;
using Enums;
using Interfaces;
using UnityEngine;

namespace Character.States
{
    public class StateSystem : IStateSwitcher
    { 
        public ExtraState ExtraState;
        public StateData Data => _currentState.Data;
        private State _currentState;
        private readonly int _unstopLength, _flickerLength;
        private readonly Flicker _flicker;
        private readonly List<State> _mainStateMap;
        private readonly ICharacterComponents _character;

        public StateSystem(ICharacterComponents character, StateData junior, StateData middle, StateData senior, int flickerLength,
            int unstopLength){
            ExtraState = ExtraState.NormalState;
            _character = character;
            _unstopLength = unstopLength;
            _flickerLength = flickerLength;
            _flicker = new Flicker();
            
            _mainStateMap = new List<State>
            {
                new JuniorState(character, this, junior),
                new MiddleState(character, this, middle),
                new SeniorState(character, this, senior)
            };

            StateSwitch<JuniorState>();
        }

        public void Hurt(ActiveEnemy sender){
            switch (ExtraState){
                case (ExtraState.NormalState):
                    _currentState.StateDown();
                    break;
                case (ExtraState.UnstopState):
                    sender.DownHit();
                    break;
                case ExtraState.FlickerState:
                    return;
            }
        }

        public void LevelUp(){
            _currentState.StateUp();
        }
        public async void ExtraStateUnstop(){
            ExtraState = ExtraState.UnstopState;
            Debug.Log("ExtraStateUnstop is start");
            
            await Task.Delay(_unstopLength);
            ExtraState = ExtraState.NormalState;
            Debug.Log("ExtraStateUnstop is end");
        }

        public async void ExtraStateFlicker(){
            ExtraState = ExtraState.FlickerState;
            _flicker.FlickerPlay(Data.Skin);
            
            await Task.Delay(_flickerLength);
            ExtraState = ExtraState.NormalState;
            _flicker.FlickerStop();
        }
        
        public void Resize(){
            var height = _character.MainCollider.height;
            var verticalDifference = Vector3.up * (Data.ColliderSize.y - height) / 2;
            _character.MainTransform.position += verticalDifference;

            _character.MainCollider.height = Data.ColliderSize.y;
            _character.MainCollider.radius = Data.ColliderSize.x / 2;

        }


        public void StateSwitch<T>() where T : State{
            var stateHandler = _mainStateMap.FirstOrDefault(source => source is T);
            if (stateHandler == null) return;

            _currentState?.Exit();
            _currentState = stateHandler;
            _currentState.Enter();
        }

        public bool CompareCurrentState<T>() where T : State{
            var stateHandler = _mainStateMap.FirstOrDefault(source => source is T);
            return _currentState == stateHandler ? true : false;
        }
    }
}*/