using Character.States;
using Character.States.Data;
using Enums;

namespace Interfaces
{
    public interface IStateSwitcher {
        void StateSwitch<T>() where T : State;

        void ExtraStateSwitch(ExtraState state);

    }
}