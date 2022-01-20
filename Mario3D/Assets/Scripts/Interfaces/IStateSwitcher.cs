using Character.States;

namespace Interfaces
{
    public interface IStateSwitcher {
        void StateSwitch<T>() where T : State;
        void HurtExtraState();
    }
}