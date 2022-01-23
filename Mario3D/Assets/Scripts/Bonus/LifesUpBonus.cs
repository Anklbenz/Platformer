using System;
using Character.States;
using Interfaces;
using UnityEngine;

namespace Bonus
{
    public class LifesUpBonus : ActiveBonus, ILifeIncreaseNotify
    {
        public event Action<Vector3> IncreaseLifeEvent;
        public Vector3 Position => _collider.bounds.center;

        protected override void BounsTake(StateSystem state) {
            IncreaseLifeEvent?.Invoke(Position);
        }
    }
}
