using System;
using UnityEngine;
public interface IScoreChangeNotify
{
    event Action<IScoreChangeNotify, int> ScoreNotifyEvent;
    Vector3 Position { get;}
}

