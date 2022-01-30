using System;
using Character.States;
using Interfaces;
using UnityEngine;

namespace Bonus
{
    public class JumpingStar : ActiveBonus, IScoreChangeNotify
    {
        private const int SCORE_LIST_ELEMENT = 5;

        public Vector3 Position => ObjectCollider.bounds.center;
        public event Action<IScoreChangeNotify, int> ScoreChangeEvent;

        protected override void Awake(){
            base.Awake();
            Motor.Jumping();
        }

        protected override void OnIsGrounded(){
           Motor.Jumping();
        }
  
        protected override void BonusTake(IStateHandlerInteraction stateHandler){
            stateHandler.UnstopBonusTake();
            ScoreChangeEvent?.Invoke(this, SCORE_LIST_ELEMENT);
        }

    //    public override void DownHit(float force){}

    }
}



