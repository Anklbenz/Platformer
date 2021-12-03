using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StateHandler : MonoBehaviour, IStateSwitcher
{
    private Character _character;
    public State CurrentState { get; private set; }
    private List<State> stateMap;

    void Start() {
        _character = GetComponent<Character>();
        stateMap = new List<State>
        {
            new JuniorState(_character, this),
            new MiddleState(_character, this),
            new SeniorState(_character, this)
        };
        CurrentState = stateMap[0];
    }

    public void Hurt() {
        CurrentState.Hurt();
    }

    public void LevelUp() {
        CurrentState.LevelUp();
    }

    public void StateSwitch<T>() where T : State {
        var state = stateMap.FirstOrDefault(source => source is T);
        CurrentState.Exit();
        CurrentState = state;
        CurrentState.Enter();
    }

    public bool CompareCurrentStateWith<T>() where T : State {
        var state = stateMap.FirstOrDefault(source => source is T);
        if (CurrentState == state)
            return true;
        else
            return false;
    }

}
