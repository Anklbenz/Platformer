using System;
using UnityEngine;

namespace Interfaces
{
    public interface IScoreChangeNotify
    {
        event Action<IScoreChangeNotify, int> ScoreNotifyEvent;
        Vector3 Position { get;}
    }
}

