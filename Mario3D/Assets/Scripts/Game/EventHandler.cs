using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventHandler 
{
    private IScoreReciver _scoreReciver;
    
    public EventHandler(IScoreReciver scoreReciver) {
        _scoreReciver = scoreReciver;
    }

    public void Subsribe(IScoreNotify sender) {
        sender.ScoreNotifyEvent += _scoreReciver.AddScore;
    }
    public void UnSubsribe(IScoreNotify sender) {
        sender.ScoreNotifyEvent -= _scoreReciver.AddScore;
    }

}
