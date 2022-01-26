using System;
using Character.States.Data;
using Enums;

namespace Interfaces
{
   public interface IStateSwitchActions
    {
         event Action<StateData> StateChangedEvent;
         event Action<ExtraState> ExtraStateChangedEvent;
    }
}
