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
        public Vector3 Position => ObjectCollider.bounds.center;

        protected override void BonusTake(IStateHandlerInteraction stateHandler) {
            stateHandler.BonusTake();
            ScoreChangeEvent?.Invoke(this, SCORE_LIST_ELEMENT);
        }
    }
}
