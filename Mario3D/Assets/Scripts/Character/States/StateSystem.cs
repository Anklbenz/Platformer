using System.Collections.Generic;
using System.Linq;

namespace Character.States
{
    public class StateSystem : IStateSwitcher
    {
        public StateData Data { get; private set; }

        private State currentState { get; set; }
        private readonly List<State> _stateMap;

        public StateSystem(IStateSystemHandler character, StateData junior, StateData middle, StateData senior){
            _stateMap = new List<State>
            {
                new JuniorState(character, this, junior),
                new MiddleState(character, this, middle),
                new SeniorState(character, this, senior)
            };

            this.StateSwitch<JuniorState>();
        }

        public void Hurt() {
            currentState.StateDown();
        }

        public void LevelUp() {
            currentState.StateUp();
        }

        public void StateSwitch<T>() where T : State {
            currentState?.Exit();
        
            var state = _stateMap.FirstOrDefault(source => source is T);
            if (state == null) return;
        
            currentState = state;
            Data = state.Data;
            currentState.Enter();
        }

        public bool CompareCurrentStateWith<T>() where T : State {
            var state = _stateMap.FirstOrDefault(source => source is T);

            return currentState == state ? true : false;
        }
    }
}
