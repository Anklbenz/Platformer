using MyEnums;
using System;
using UnityEngine;

public interface IBonusSpawnNotify
{
    event Action<IBonusSpawnNotify> BonusSpawnEvent;
    BonusType BonusType { get; }
    Vector3 BonusCreatePoint { get; }
}