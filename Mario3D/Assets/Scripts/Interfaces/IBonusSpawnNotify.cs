using System;
using Enums;
using UnityEngine;

namespace Interfaces
{
    public interface IBonusSpawnNotify
    {
        event Action<IBonusSpawnNotify> BonusSpawnEvent;
        BonusType BonusType { get; }
        Vector3 BonusCreatePoint { get; }
    }
}