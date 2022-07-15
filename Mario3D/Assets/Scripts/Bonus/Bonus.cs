using System;
using Character.States;
using Enemy;
using Interfaces;
using UnityEngine;

namespace Bonus
{
    public abstract class Bonus : InteractiveObject, IScoreChangeNotify
    {
        public event Action<IScoreChangeNotify, int> ScoreChangeEvent;
        public Vector3 Position => transform.position;

        private bool _isActive = true;

        protected override void Interaction(IStateMethods stateHandler, Vector3 pos) {
            if (!_isActive) return;

            _isActive = false;
            BonusTake(stateHandler);
            Destroy(gameObject);
        }
        protected abstract void BonusTake(IStateMethods stateHandler);

        protected virtual void SendScore(int scoreListElement) {
            ScoreChangeEvent?.Invoke(this, scoreListElement);
        }
    }
}