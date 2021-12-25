using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisibleBrick : Brick
{
    [SerializeField] private Collider _triggerCollider;

    public override void BrickHit(Character character) {
        if (!_isActive) return;

        if (_bonusesCount > 0) {
             base.BonusShow(character);
            _bonusesCount--;
            _brickCollider.enabled = true;
            _triggerCollider.enabled = false;
            _isActive = false;
            _secondaryMesh.SetActive(true);
        }
    }
}
