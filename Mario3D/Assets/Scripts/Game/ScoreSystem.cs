﻿using System;
using System.Collections.Generic;
using Interfaces;
using UnityEngine;

namespace Game
{
    public class ScoreSystem : ILifeIncreaseNotify, ILabelDrawer
    {
        private readonly int[] SCORE_LIST = { 100, 200, 400, 500, 800, 1000, 2000, 4000, 5000, 10000 };
    
        public event Action ScoreChangedEvent;
        public event Action<Vector3> IncreaseLifeEvent;
        public event Action<Vector3, string> LabelDrawEvent; 

        public int TotalScore => _totalScore;

        private int _totalScore = 0;
        private List<IScoreChangeNotify> _scoreNotifies;

        public ScoreSystem(List<IScoreChangeNotify> scoreNotifies) {
            _scoreNotifies = scoreNotifies;
            SubscribeAllActiveScoreNotifies();
        }

        private void SubscribeAllActiveScoreNotifies() {
            foreach (var instance in _scoreNotifies)
                SubsсribeOnScoreEvent(instance);
        }

        public void SubsсribeOnScoreEvent(IScoreChangeNotify sender) {
            sender.ScoreChangeEvent += AddScore;
        }

        public void AddScore(IScoreChangeNotify sender, int bounceCount) {
            var lastScoreElement = SCORE_LIST.Length - 1;
            if (bounceCount >= lastScoreElement) { 
                bounceCount = lastScoreElement;
                IncreaseLifeEvent?.Invoke(sender.Position);
                return;
            }

            var score = SCORE_LIST[bounceCount];
            this.TotalScoreUpdate(score);
            LabelDrawEvent?.Invoke(sender.Position, score.ToString());
        }

        private void TotalScoreUpdate(int points) {
            _totalScore += points;
            ScoreChangedEvent?.Invoke();
        }
    }
}
