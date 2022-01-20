using System;
using Character.States;
using Enemys;
using Interfaces;
using UnityEngine;

namespace Bonus
{
    public abstract class Bonus : InteractiveObject, IScoreChangeNotify
    {
        public event Action<IScoreChangeNotify, int> ScoreNotifyEvent;
        public Vector3 Position => transform.position;

        private bool _isActive = true;

        protected override void Interaction(StateSystem state, Vector3 pos) {
            if (!_isActive) return;

            _isActive = false;
            BonusTake(state);
            Destroy(gameObject);
        }
        protected abstract void BonusTake(StateSystem state);

        protected virtual void SendScore(int scoreListElement) {
            ScoreNotifyEvent?.Invoke(this, scoreListElement);
        }
    }
}