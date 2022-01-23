using Character.States;
using UnityEngine;

namespace Bricks
{
    public class InvisibleBrick : Brick
    {
        [SerializeField] private Collider triggerCollider;

        public override void BrickHit(StateSystem state) {
            if (!IsActive) return;
            if (bonusesCount <= 0) return;
            
            base.BonusShow(state);
            bonusesCount--;
            brickCollider.enabled = true;
            triggerCollider.enabled = false;
            IsActive = false;
            secondaryMesh.SetActive(true);
        }
    }
}
