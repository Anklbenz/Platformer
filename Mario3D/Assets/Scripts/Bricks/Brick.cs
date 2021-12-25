﻿using MyEnums;
using System;
using UnityEngine;

public abstract class Brick : MonoBehaviour, IBrickHit, IBonusSpawnNotify
{
    public event Action<IBonusSpawnNotify> BonusSpawnEvent;

    [Range(0, 10)]
    [SerializeField] protected int _bonusesCount;
    [SerializeField] protected BonusType _bonusType;
    [SerializeField] protected GameObject _primaryMesh;
    [SerializeField] protected GameObject _secondaryMesh;
    [SerializeField] protected BoxCollider _brickCollider;
    [SerializeField] protected Transform _bonusCreationPoint;

    protected bool _isActive = true;
    
    public Vector3 BonusCreatePoint =>_bonusCreationPoint.position;
    public BonusType BonusType => _bonusType;

    public abstract void BrickHit(Character character);

    protected void BonusShow(Character character) {
        if (_bonusType == BonusType.none) return;

        if (_bonusType == BonusType.growUp || _bonusType == BonusType.flower) 
            _bonusType = character.CompareCurrentStateWith<JuniorState>() ? BonusType.growUp : BonusType.flower;      

        BonusSpawnEvent?.Invoke(this);
    }
}