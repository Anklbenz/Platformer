using Character.States;
using Character.States.Data;

namespace Interfaces
{
    public interface IStateSwitcher {
        void MainStateSwitch<T>() where T : State;
  
        void ExtraStateFlicker();
        
        void Resize();
    }
}