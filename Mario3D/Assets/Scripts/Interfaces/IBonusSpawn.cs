using System;
using Enums;
using UnityEngine;

namespace Interfaces
{
    public interface IBonusSpawn
    {
        event Action<IBonusSpawn> BonusSpawnEvent;
        BonusType BonusType { get; }
        Vector3 BonusCreatePoint { get; }
    }
}