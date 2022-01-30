using Character.States;
using UnityEngine;

namespace Bricks
{
    public class InvisibleBrick : Brick
    {
        [SerializeField] private Collider triggerCollider;

        public override void BrickHit(bool canCrush) {
            if (!IsActive) return;
            if (bonusesCount <= 0) return;
            
            base.BonusShow(canCrush);
            bonusesCount--;
            brickCollider.enabled = true;
            triggerCollider.enabled = false;
            IsActive = false;
            secondaryMesh.SetActive(true);
        }
    }
}
