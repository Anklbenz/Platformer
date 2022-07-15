using System;
using UnityEngine;

namespace Interfaces
{
    public interface ILifeIncreaseNotify
    {
        event Action<Vector3> IncreaseLifeEvent;
    }
}