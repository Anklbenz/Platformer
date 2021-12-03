using UnityEngine;

public abstract class State
{
    protected Character character;
    protected readonly IStateSwitcher stateSwitcher;

    protected State (Character character , IStateSwitcher stateSwitcher) {
        this.character = character;
        this.stateSwitcher = stateSwitcher;
    }

    public abstract void Enter();
    public abstract void Exit();
    public abstract void Hurt();
    public abstract void LevelUp();
}
