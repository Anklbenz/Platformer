using System;
using UnityEngine;
public interface ILifeIncreaceNotify
{
    event Action<Vector3> IncreceLifeEvent;
}