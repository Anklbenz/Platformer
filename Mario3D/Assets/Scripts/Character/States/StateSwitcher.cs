using System.Collections.Generic;
using System;
using System.Linq;
using System.Threading.Tasks;
using Character.States.Data;
using Enemy;
using Enums;
using Interfaces;

namespace Character.States
{
  public class StateSwitcher : IStateSwitcher, IStateSwitchActions
  {
    public event Action<StateData> StateChangedEvent;
    public event Action<ExtraState> ExtraStateChangedEvent;
    
    private ExtraState _extraState;
    private readonly List<State> _stateMap;
    private readonly int _flickerLength, _unstopLength;
    private State _currentState;
    //private StateData CurrentStateData => _currentState.Data;

    public StateSwitcher(StateData junior, StateData middle, StateData senior, int flickerLength, int unstopLength){
      _flickerLength = flickerLength;
      _unstopLength = unstopLength;
      _extraState = ExtraState.NormalState;
      
      _stateMap = new List<State>()
      {
        new JuniorState(this, junior), new MiddleState(this, middle), new SeniorState(this, senior)
      };
    }

    public void StateSwitch<T>() where T : State{
      var state = _stateMap.FirstOrDefault(source => source is T);
      if (state == null) return;
     _currentState = state;

     StateChangedEvent?.Invoke(_currentState.Data);
    }

    public bool CompareCurrentState<T>() where T : State{
      var state = _stateMap.FirstOrDefault(source => source is T);
      return _currentState == state;
    }

    public void ExtraStateSwitch(ExtraState state){
      switch (state){
        case (ExtraState.NormalState):
          break;
        case (ExtraState.FlickerState):
          ExtraSwitcher(ExtraState.FlickerState, _flickerLength);
          break;
        case (ExtraState.UnstopState):
          ExtraSwitcher(ExtraState.UnstopState, _unstopLength);
          break;
      }

      _extraState = state;
      ExtraStateChangedEvent?.Invoke(_extraState);
    }

    public void StateUp(){
      _currentState.StateUp();
    }

    public void EnemyTouch(ActiveEnemy sender){
      switch (_extraState){
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

    private async void ExtraSwitcher(ExtraState state, int length){
      ExtraStateSwitch(state);
      await Task.Delay(length);
      ExtraStateSwitch(ExtraState.NormalState);
    }
  }
}
