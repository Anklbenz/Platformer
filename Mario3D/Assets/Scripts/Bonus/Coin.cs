using System;
using Character.States;
using Interfaces;
using UnityEngine;

namespace Bonus
{
    public sealed class Coin : Bonus, ICoinCollectNotify
    {
        private const int SCORE_LIST_ELEMENT = 0;
        public event Action CoinCollectNotify;
        [SerializeField] private float _destroyTime;   

        private void Start() {
            this.BonusTake(null);
            Destroy(this.gameObject, _destroyTime);
        }

        protected override void BonusTake(StateSystem state) {
            SendScore(SCORE_LIST_ELEMENT);
            CoinCollectNotify?.Invoke();
        }
    }
}