﻿using System;
using UnityEngine;

public abstract class ActiveBonus : ActiveInteractiveObject
{
    private bool _isActive = true;

    protected override void Interaction(StateHandler state, Vector3 pos) {
        if (!_isActive) return;

        _isActive = false;
        BounsTake(state);
        Destroy(gameObject);
    }

    public override void DownHit() {
        if (_motor.isActiveAndEnabled)
            _motor.DirectionChange();
    }

    protected abstract void BounsTake(StateHandler state);
}