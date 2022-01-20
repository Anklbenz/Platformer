using Character.States;
using UnityEngine;

namespace Bricks
{
    public class InvisibleBrick : Brick
    {
        [SerializeField] private Collider _triggerCollider;

        public override void BrickHit(StateSystem state) {
            if (!_isActive) return;

            if (_bonusesCount > 0) {
                base.BonusShow(state);
                _bonusesCount--;
                _brickCollider.enabled = true;
                _triggerCollider.enabled = false;
                _isActive = false;
                _secondaryMesh.SetActive(true);
            }
        }
    }
}
