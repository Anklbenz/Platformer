using System;
using Character.States;
using Interfaces;
using UnityEngine;

namespace Bonus
{
    public class GrowupBonus : ActiveBonus, IScoreChangeNotify
    {
        private const int SCORE_LIST_ELEMENT = 5;

        public event Action<IScoreChangeNotify, int> ScoreNotifyEvent;
        public Vector3 Position => _collider.bounds.center;

        protected override void BounsTake(StateSystem state) {
            state.LevelUp();
            ScoreNotifyEvent?.Invoke(this, SCORE_LIST_ELEMENT);
        }
    }
}
