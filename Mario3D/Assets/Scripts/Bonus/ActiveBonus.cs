using Character.States;
using Enemy;
using Interfaces;
using UnityEngine;

namespace Bonus
{
    public abstract class ActiveBonus : ActiveInteractiveObject
    {
        private bool _isActive = true;

        protected override void Interaction(IStateHandlerInteraction stateHandler, Vector3 pos){
            if (!_isActive) return;

            _isActive = false;
            BonusTake(stateHandler);
            Destroy(gameObject);
        }

        public override void DownHit(float force){
            if (Motor.isActiveAndEnabled)
                Motor.DirectionChange();
        }

        protected abstract void BonusTake(IStateHandlerInteraction stateHandler);
    }
}
