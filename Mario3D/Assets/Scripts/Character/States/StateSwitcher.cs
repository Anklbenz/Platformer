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
    public StateData CurrentStateData => CurrentState.Data;
    public ExtraState ExtraState{ get; private set; }
    public State CurrentState{ get; private set; }
    private readonly List<State> _stateMap;
    private readonly int _flickerLength, _unstopLength;

    public StateSwitcher(IReadOnlyList<StateData> stateData, /*StateData junior, StateData middle, StateData senior,*/ int flickerLength, int unstopLength){
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
      CurrentState = state;
      StateChangedEvent?.Invoke(CurrentState.Data);
    }

    public void ExtraStateSwitch(ExtraState state){
      switch (state){
        case (ExtraState.NormalState):
          break;
        case (ExtraState.FlickerState):
          ExtraStateCountdown(_flickerLength);
          break;
        case (ExtraState.UnstopState):
          ExtraStateCountdown(_unstopLength);
          break;
        default:
          throw new ArgumentOutOfRangeException(nameof(state), state, null);
      }
      ExtraState = state;
      ExtraStateChangedEvent?.Invoke(ExtraState);
    }

    private async void ExtraStateCountdown(int timer){
      await Task.Delay(timer);
      ExtraStateSwitch(ExtraState.NormalState);
    }
  }
}
