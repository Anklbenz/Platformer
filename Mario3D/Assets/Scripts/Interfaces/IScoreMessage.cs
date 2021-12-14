using System;
using UnityEngine;
public interface IScoreMessage
{
    event Action<IScoreMessage, int> ScoreEvent;
    Vector3 Position { get;}
}

