using System;
using Character.States.Data;
using Enums;
using Interfaces;
using UnityEngine;

namespace Character.States
{
   public class StateVisualizer: IDisposable
   {
      private readonly ICharacterComponents _character;
      private readonly IStateSwitchActions _stateSwitchActions;
      private readonly Flicker _flicker;
      private StateData _currentData;

      public StateVisualizer(ICharacterComponents character, IStateSwitchActions actions){
         _character = character;
         _stateSwitchActions = actions;
         
         _stateSwitchActions.ExtraStateChangedEvent += OnExtraStateSwitch;
         _stateSwitchActions.StateChangedEvent += OnStateSwitch;

         _flicker = new Flicker();
      }

      private void OnStateSwitch(StateData data){
         if (_currentData != null)
            _currentData.Skin.SetActive(false);

         _currentData = data;
         _currentData.Skin.SetActive(true);
         
         ColliderResize();
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
            default:
               throw new ArgumentOutOfRangeException(nameof(state), state, null);
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
