public interface IStateSwitcher {
    void StateSwitch<T>() where T : State;


}