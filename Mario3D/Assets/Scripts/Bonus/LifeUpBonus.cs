using System;
using Character.States;
using Interfaces;
using UnityEngine;

namespace Bonus
{
    public class LifeUpBonus : ActiveBonus, ILifeIncreaseNotify
    {
        public event Action<Vector3> IncreaseLifeEvent;
        public Vector3 Position => ObjectCollider.bounds.center;

        protected override void BonusTake(IStateHandlerInteraction stateHandler) {
            IncreaseLifeEvent?.Invoke(Position);
        }
    }
}
