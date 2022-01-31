using System;
using System.Collections.Generic;
using Character.States.Data;
using Enemy;
using Enums;
using Interfaces;
using UnityEngine;

namespace Character.States
{
   public class StateHandler : IStateMethods, IDisposable
   {
      public StateData Data => _stateSwitcher.CurrentStateData;
      public ExtraState ExtraState => _stateSwitcher.ExtraState;
      public IStateMethods StateMethods{ get; }
      public bool IsSitting{ get; private set; }

      private readonly float _unstopStateDropForce;
      private readonly Flicker _flicker;
      private readonly StateSwitcher _stateSwitcher;
      private readonly CharacterResizer _characterResizer;

      public StateHandler(ICharacterComponents components, IReadOnlyList<StateData> stateMap, float unstopStateDropForce,
         int flickerLength, int unstopLength){
         InstantiateSkins(stateMap, components.SkinsParent);
         StateMethods = this;

         _flicker = new Flicker();
         _characterResizer = new CharacterResizer(components.MainCollider, components.MainTransform);

         _stateSwitcher = new StateSwitcher(stateMap, flickerLength, unstopLength);
         _unstopStateDropForce = unstopStateDropForce;

         _stateSwitcher.FlickStartEvent += OnStartFlick;
         _stateSwitcher.FlickStopEvent += OnStopFlick;
         _stateSwitcher.StateEnter += OnStateEnter;
         _stateSwitcher.StateExit += OnStateExit;

         _stateSwitcher.StateSwitch<JuniorState>();
      }

      public void BonusTake(){
         _stateSwitcher.CurrentState.StateUp();
      }

      public void UnstopBonusTake(){
         _stateSwitcher.ExtraStateSwitch(ExtraState.UnstopState);
      }

      public void EnemyTouch(ActiveEnemy sender){
         switch (ExtraState){

            case (ExtraState.NormalState):
               _stateSwitcher.CurrentState.StateDown();
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

      public void SitDown(bool sitInput){
         if (!Data.CanSit) return;

         IsSitting = sitInput;

         _characterResizer.ColliderResize(sitInput ? Data.SitColliderSize : Data.ColliderSize);
      }

      private void InstantiateSkins(IEnumerable<StateData> stateMap, Transform parent){
         foreach (var stateData in stateMap)
            stateData.SkinInstantiate(parent);
      }

      private void OnStateEnter(){
         _characterResizer.ColliderResize(Data.ColliderSize);
         Data.Skin.SetActive(true);
      }

      private void OnStateExit() => Data.Skin.SetActive(false);
      private void OnStartFlick() => _flicker.Play(Data.Skin);
      private void OnStopFlick() => _flicker.Stop();

      public void Dispose(){
         _stateSwitcher.FlickStartEvent -= OnStartFlick;
         _stateSwitcher.FlickStopEvent -= OnStopFlick;
         _stateSwitcher.StateEnter -= OnStateEnter;
         _stateSwitcher.StateExit -= OnStateExit;
      }
   }
}
