using System;
using UnityEngine;
public interface IScoreNotify
{
    event Action<IScoreNotify, int> ScoreNotifyEvent;
    Vector3 Position { get;}
}

