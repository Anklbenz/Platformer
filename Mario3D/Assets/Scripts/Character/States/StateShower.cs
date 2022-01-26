using System;
using System.Collections.Generic;
using Character.States.Data;
using Enums;
using Interfaces;
using UnityEngine;

namespace Character.States
{
   public class StateShower: IDisposable
   {
      private readonly ICharacterComponents _character;
      private readonly IStateSwitchActions _stateSwitchActions;
      private readonly Flicker _flicker;
      private StateData _currentData;

      public StateShower(ICharacterComponents character, IStateSwitchActions actions){
         _stateSwitchActions = actions;
         _stateSwitchActions.ExtraStateChangedEvent += OnExtraStateSwitch;
         _stateSwitchActions.StateChangedEvent += OnStateSwitch;

         _flicker = new Flicker();
         _character = character;
      }

      private void OnStateSwitch(StateData data){
         _currentData.Skin.SetActive(false);
         _currentData = data;
         ColliderResize();
         _currentData.Skin.SetActive(true);
      }

      private void OnExtraStateSwitch(ExtraState state){
         _flicker.FlickerStop();
         switch (state){
            case(ExtraState.FlickerState):
               _flicker.FlickerPlay(_currentData.Skin);
               break;
            case(ExtraState.UnstopState):
               //unstop visual
               break;
            case (ExtraState.NormalState):
               break;
          }
      }

      private void ColliderResize(){
         var height = _character.MainCollider.height;
         var verticalDifference = Vector3.up * (_currentData.ColliderSize.y - height) / 2;
         _character.MainTransform.position += verticalDifference;

         _character.MainCollider.height = _currentData.ColliderSize.y;
         _character.MainCollider.radius = _currentData.ColliderSize.x / 2;
      }
      
      public void Dispose(){
         _stateSwitchActions.StateChangedEvent -= OnStateSwitch;
         _stateSwitchActions.ExtraStateChangedEvent -= OnExtraStateSwitch;
      }
   }
}
