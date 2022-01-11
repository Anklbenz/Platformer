﻿using System;
using Character.States;
using UnityEngine;

public class LifesUpBonus : ActiveBonus, ILifeIncreaceNotify
{
    public event Action<Vector3> IncreceLifeEvent;
    public Vector3 Position => _collider.bounds.center;

    protected override void BounsTake(StateSystem state) {
       IncreceLifeEvent?.Invoke(Position);
    }
}
