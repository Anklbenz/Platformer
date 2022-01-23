using System;
using Character.States;
using Interfaces;
using UnityEngine;

namespace Bonus
{
    public class GrowupBonus : ActiveBonus, IScoreChangeNotify
    {
        private const int SCORE_LIST_ELEMENT = 5;

        public event Action<IScoreChangeNotify, int> ScoreChangeEvent;
        public Vector3 Position => _collider.bounds.center;

        protected override void BounsTake(StateSystem state) {
            state.LevelUp();
            ScoreChangeEvent?.Invoke(this, SCORE_LIST_ELEMENT);
        }
    }
}
