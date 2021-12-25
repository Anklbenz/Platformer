using System;
using UnityEngine;

public abstract class Bonus : InteractiveObject, IScoreChangeNotify
{
    public event Action<IScoreChangeNotify, int> ScoreNotifyEvent;
    public Vector3 Position => transform.position;

    private bool _isActive = true;

    protected override void Interaction(StateHandler state, Vector3 pos) {
        if (!_isActive) return;

        _isActive = false;
        BonusTake(state);
        Destroy(gameObject);
    }
    protected abstract void BonusTake(StateHandler state);

    protected virtual void SendScore(int scoreListElement) {
        ScoreNotifyEvent?.Invoke(this, scoreListElement);
    }
}