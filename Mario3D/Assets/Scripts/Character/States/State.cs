using UnityEngine;

public abstract class State 
{
    public StateData Data { get; private set; }

    protected GameObject skinGameObject;
    protected readonly IStateSystemHandler character;
    protected readonly IStateSwitcher stateSwitcher;

    protected State (IStateSystemHandler character , IStateSwitcher stateSwitcher, StateData data) {
        this.character = character;
        this.stateSwitcher = stateSwitcher;
        this.Data = data;
    }

    public abstract void Enter();
    public abstract void Exit();
    public abstract void StateDown();
    public abstract void StateUp();
}
