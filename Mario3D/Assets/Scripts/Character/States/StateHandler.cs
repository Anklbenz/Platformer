using Character.States.Data;
using Interfaces;
using UnityEngine;

namespace Character.States
{
   public class StateHandler
   {
      private readonly StateSwitcher _stateSwitcher;
      private StateShower _stateShower;

      public StateHandler(ICharacterComponents character, StateData juniorState, StateData middleState, StateData seniorState, int flickerLength, int unstopLength){
         var parent = character.SkinsParent;
         
         juniorState.SkinInstantiate(parent);
         middleState.SkinInstantiate(parent);
         seniorState.SkinInstantiate(parent);

         _stateSwitcher = new StateSwitcher(juniorState, middleState, seniorState,  flickerLength, unstopLength);
         _stateShower = new StateShower(character, _stateSwitcher);
      }


   }
}
