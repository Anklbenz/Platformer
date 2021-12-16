using MyEnums;
using System;
using UnityEngine;

public interface IBonus
{
    event Action<IBonus> BonusSpawnEvent;
    BonusType BonusType { get; }
    Vector3 BonusCreatePoint { get; }
}