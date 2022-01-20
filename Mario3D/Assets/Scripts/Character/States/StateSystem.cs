using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Character.States.Data;
using Enemys;
using Interfaces;
using UnityEngine;

namespace Character.States
{
    public class StateSystem : IStateSwitcher
    {
        public bool IsActiveState => !_immortal && !_touchKills;
        public bool CanCrush => CurrentState.Data.CanCrush;
        public bool CanShoot => CurrentState.Data.CanShoot;
        
        private State CurrentState; //
        private bool _immortal = false;
        private bool _touchKills = false;
        private readonly int _hurtImmortalTime;
        private readonly int _unsopableImmortalTime;
        private readonly List<State> _stateMap;
        private readonly Flicker _flicker;

        public StateSystem(ICharacterComponets character, StateData junior, StateData middle, StateData senior, int hurtImmortalTime, int unsopableImmortalTime){
            
            _hurtImmortalTime = hurtImmortalTime;
            _unsopableImmortalTime = unsopableImmortalTime;
            _flicker = new Flicker();

            _stateMap = new List<State>
            {
                new JuniorState(character, this, junior),
                new MiddleState(character, this, middle),
                new SeniorState(character, this, senior)
            };
            this.StateSwitch<JuniorState>();
        }

        public void Hurt(ActiveEnemy sender){
            if (_touchKills){
               sender.DownHit();
               return;
            }

            if (_immortal) return;

            CurrentState.StateDown();
        }

        public void LevelUp(){
            CurrentState.StateUp();
        }

        public void StateSwitch<T>() where T : State{
            var state = _stateMap.FirstOrDefault(source => source is T);
            if (state == null) return;

            CurrentState?.Exit();
            CurrentState = state;
            CurrentState.Enter();
        }

        public bool CompareCurrentStateWith<T>() where T : State{
            var state = _stateMap.FirstOrDefault(source => source is T);
            return CurrentState == state ? true : false;
        }

        public async void HurtExtraState(){
            _immortal = true;
            _flicker.SetObject(CurrentState.Skin);
            _flicker.FlickerPlay();
            await Task.Delay(_hurtImmortalTime);
            _flicker.FlickerStop();
            _immortal = false;
        }

        public async void UnstobaleExtraState(){
            _immortal = true;
            _touchKills = true;
            //animation changecolor play
            await Task.Delay(_unsopableImmortalTime);
            _immortal = false;
            _touchKills = false;
            Debug.Log("Unstopable state is end");
        }
    }
}