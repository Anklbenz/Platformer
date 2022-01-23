using System;
using UnityEngine;

namespace Interfaces
{
    public interface IScoreChangeNotify
    {
        event Action<IScoreChangeNotify, int> ScoreChangeEvent;
        Vector3 Position { get;}
    }
}

