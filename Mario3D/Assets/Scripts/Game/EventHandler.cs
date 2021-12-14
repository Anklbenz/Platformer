using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventHandler 
{
    private IScoreReciver _scoreReciver;
    
    public EventHandler(IScoreReciver scoreReciver) {
        _scoreReciver = scoreReciver;
    }

    public void Subsribe(IScoreMessage sender) {
        sender.ScoreEvent += _scoreReciver.AddScore;
    }
    public void UnSubsribe(IScoreMessage sender) {
        sender.ScoreEvent -= _scoreReciver.AddScore;
    }

}
