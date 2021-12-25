using System.Collections.Generic;
using System.Linq;

public class StateHandler : IStateSwitcher
{
    private Character _character;
    public State CurrentState { get; private set; }
    private List<State> stateMap;

    public StateHandler(Character character, StateData junior, StateData middle, StateData senior) {
        _character = character;

        stateMap = new List<State>
        {
            new JuniorState(_character, this, junior),
            new MiddleState(_character, this, middle),
            new SeniorState(_character, this, senior)
        };
        CurrentState = stateMap[0];
    }

    public void Hurt() {
        CurrentState.Exit();
    }

    public void LevelUp() {
        CurrentState.StateUp();
    }

    public void StateSwitch<T>() where T : State {
        var state = stateMap.FirstOrDefault(source => source is T);
        CurrentState = state;
        CurrentState.Enter();
    }

    public bool CompareCurrentStateWith<T>() where T : State {
        var state = stateMap.FirstOrDefault(source => source is T);

        return CurrentState == state ? true : false;
    }
}
