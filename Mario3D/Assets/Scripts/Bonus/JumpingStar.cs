using System;
using Character.States;
using Interfaces;
using UnityEngine;

namespace Bonus
{
    public class JumpingStar : ActiveBonus, IScoreChangeNotify
    {
        private const int SCORE_LIST_ELEMENT = 5;

        public Vector3 Position => _collider.bounds.center;
        public event Action<IScoreChangeNotify, int> ScoreNotifyEvent;

        protected override void Awake() {
            base.Awake();
            _motor.Bounce();
        }

        protected override void OnIsGrounded() {
            _motor.Bounce();
        }

        protected override void BounsTake(StateSystem state) {
            Debug.Log("Unstopabel state");
            state.UnstobaleExtraState();
            ScoreNotifyEvent?.Invoke(this, SCORE_LIST_ELEMENT);
        }
    }
}



