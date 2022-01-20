using System;
using UnityEngine;

namespace Interfaces
{
    public interface ILifeIncreaceNotify
    {
        event Action<Vector3> IncreceLifeEvent;
    }
}