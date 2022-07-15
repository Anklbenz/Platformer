using System.Collections.Generic;
using System;
using System.Linq;
using System.Threading.Tasks;
using Character.States.Data;
using Enums;
using Interfaces;

namespace Character.States
{
  public class StateSwitcher : IStateSwitcher
  {
    public event Action FlickStartEvent, FlickStopEvent, StateEnter, StateExit ;
    
    public StateData CurrentStateData => CurrentState.Data;
    public ExtraState ExtraState{ get; private set; }
    public State CurrentState{ get; private set; }
    private readonly List<State> _stateMap;
    private readonly int _flickerLength, _unstopLength;

    public StateSwitcher(IReadOnlyList<StateData> stateData, int flickerLength, int unstopLength){
      ExtraState = ExtraState.NormalState;
      _flickerLength = flickerLength;
      _unstopLength = unstopLength;

      _stateMap = new List<State>()
      {
        new JuniorState(this, stateData[0]), new MiddleState(this, stateData[1]), new SeniorState(this, stateData[2])
      };
    }

    public void StateSwitch<T>() where T : State{
      var state = _stateMap.FirstOrDefault(source => source is T);
      if (state == null) return;

      if (CurrentState != null)
        StateExit?.Invoke();

      CurrentState = state;
      StateEnter?.Invoke();
    }

    public void ExtraStateSwitch(ExtraState state){
      switch (state){
        case (ExtraState.NormalState):
          FlickStopEvent?.Invoke();
          break;
        case (ExtraState.FlickerState):
          ExtraStateCountdown(_flickerLength);
          FlickStartEvent?.Invoke();
          break;
        case (ExtraState.UnstopState):
          ExtraStateCountdown(_unstopLength);
          break;
        default:
          throw new ArgumentOutOfRangeException(nameof(state), state, null);
      }
      ExtraState = state;
   }

    private async void ExtraStateCountdown(int timer){
      await Task.Delay(timer);
      ExtraStateSwitch(ExtraState.NormalState);
    }
  }
}
