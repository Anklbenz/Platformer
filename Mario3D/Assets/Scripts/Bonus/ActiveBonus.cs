using Character.States;
using Enemy;
using UnityEngine;

namespace Bonus
{
    public abstract class ActiveBonus : ActiveInteractiveObject
    {
        private bool _isActive = true;

        protected override void Interaction(StateSystem state, Vector3 pos){
            if (!_isActive) return;

            _isActive = false;
            BonusTake(state);
            Destroy(gameObject);
        }

        public override void DownHit(){
            if (Motor.isActiveAndEnabled)
                Motor.DirectionChange();
        }

        protected abstract void BonusTake(StateSystem state);
    }
}
