using System;
using Interfaces;
using UnityEngine;

namespace Game
{
    public class LifeSystem : ILabelDrawer
    {
        private const string LIFE_UP_MESSAGE = "1UP";
        public event Action LifeCountChangedEvent;
        public event Action<Vector3, string> LabelDrawEvent;
        public int LifeCount => _lifeCount;
        private readonly ScoreSystem _scoreSystem;
        private int _lifeCount = 0;

        public LifeSystem(int lifeCountOnStart, ScoreSystem scoreSystem){
            _lifeCount = lifeCountOnStart;
            _scoreSystem = scoreSystem;
            SubscribeOnIncreaseLifeEvent(scoreSystem);
        }

        ~LifeSystem(){
            _scoreSystem.IncreaseLifeEvent -= IncreaseLife;
        }

        public void SubscribeOnIncreaseLifeEvent(ILifeIncreaseNotify sender){
            sender.IncreaseLifeEvent += IncreaseLife;
        }

        private void IncreaseLife(Vector3 pos){
            _lifeCount++;
            LabelDrawEvent?.Invoke(pos, LIFE_UP_MESSAGE);
            LifeCountChangedEvent?.Invoke();
        }

        private void DecreaseLife(){
            _lifeCount--;
            LifeCountChangedEvent?.Invoke();
        }
    }
}
