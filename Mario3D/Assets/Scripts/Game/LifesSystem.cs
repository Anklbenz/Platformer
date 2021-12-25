using UnityEngine;
using System;

public class LifesSystem : ILabelDrawer
{
    private const string LIFE_UP_MESSAGE = "1UP";

    public event Action LifeCountChangedEvent;
    public event Action<Vector3, string> LabelDrawEvent;  

    public int LifesCount => _lifesCount;

    private ScoreSystem _scoreSystem;
    private int _lifesCount = 0;

    public LifesSystem(int lifesCountOnStart, ScoreSystem scoreSystem) {
        _lifesCount = lifesCountOnStart;
        _scoreSystem = scoreSystem;
        SubscribeOnIncreaceLifeEvent(scoreSystem);
    }

    ~LifesSystem() {
        _scoreSystem.IncreceLifeEvent -= IncreaceLife;
    }

    public void SubscribeOnIncreaceLifeEvent(ILifeIncreaceNotify sender) {
        sender.IncreceLifeEvent += IncreaceLife;
    }

    private void IncreaceLife(Vector3 pos) {
        _lifesCount++;
        LabelDrawEvent?.Invoke(pos, LIFE_UP_MESSAGE);
        LifeCountChangedEvent?.Invoke();
    }

    private void DecreaceLife() {
        _lifesCount--;
        LifeCountChangedEvent?.Invoke();
    }
}
