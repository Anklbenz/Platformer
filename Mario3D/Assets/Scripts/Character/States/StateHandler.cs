using System;
using System.Collections.Generic;
using Character.States.Data;
using Enemy;
using Enums;
using Interfaces;
using UnityEngine;

namespace Character.States
{
   public class StateHandler : IStateData, IStateHandlerInteraction
   {
      public StateData Data => _stateSwitch.CurrentStateData;
      public ExtraState ExtraState => _stateSwitch.ExtraState;

      private readonly float _unstopStateDropForce;
      private readonly StateSwitcher _stateSwitch;
      private StateVisualizer _stateVisualizer;

      public StateHandler(ICharacterComponents character, IReadOnlyList<StateData> stateMap, float unstopStateDropForce,
         int flickerLength, int unstopLength){
         
         SkinsInstantiate(stateMap, character.SkinsParent);

         _stateSwitch = new StateSwitcher( /*juniorState, middleState, seniorState,*/stateMap, flickerLength, unstopLength);
         _stateVisualizer = new StateVisualizer(character, _stateSwitch);

         _unstopStateDropForce = unstopStateDropForce;
         _stateSwitch.StateSwitch<JuniorState>();
      }

      private void SkinsInstantiate(IEnumerable<StateData> stateMap, Transform parent){
         foreach (var stateData in stateMap)
            stateData.SkinInstantiate(parent);
      }

      public void BonusTake(){
         _stateSwitch.CurrentState.StateUp();
      }

      public void UnstopBonusTake(){
         _stateSwitch.ExtraStateSwitch(ExtraState.UnstopState);
      }

      public void EnemyTouch(ActiveEnemy sender){
         switch (ExtraState){
            case (ExtraState.NormalState):
               _stateSwitch.CurrentState.StateDown();
               break;
            case (ExtraState.UnstopState):
               sender.DownHit(_unstopStateDropForce);
               break;
            case ExtraState.FlickerState:
               return;
            default:
               throw new ArgumentOutOfRangeException();
         }
      }

      private void SkinsInstantiate(){
         
      }
   }
}
