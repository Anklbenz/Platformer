using UnityEngine;

public abstract class State 
{
    protected Character character;
    protected readonly IStateSwitcher stateSwitcher;
    protected StateData data;

    protected State (Character character , IStateSwitcher stateSwitcher, StateData data) {
        this.character = character;
        this.stateSwitcher = stateSwitcher;
        this.data = data;
    }

    public abstract void Enter();
    public abstract void Exit();
    public abstract void StateUp();
}
