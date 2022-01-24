using System;
using Character.States;
using Interfaces;
using UnityEngine;

namespace Bonus
{
    public class LifesUpBonus : ActiveBonus, ILifeIncreaseNotify
    {
        public event Action<Vector3> IncreaseLifeEvent;
        public Vector3 Position => Collider.bounds.center;

        protected override void BonusTake(StateSystem state) {
            IncreaseLifeEvent?.Invoke(Position);
        }
    }
}
