using PrefabSripts;
using UnityEngine;
using UnityEngine.UI;
using System;

namespace Game
{
    public class UIDrawer : MonoBehaviour
    {
        [SerializeField] private Text coinLabel;
        [SerializeField] private Text lifeLabel;
        [SerializeField] private Text timeLabel;
        [SerializeField] private Text scoreLabel;
        [SerializeField] private ScoreLabel scoreLabelsPrefab;
        [SerializeField] private Transform scoreLabelParent;
        
        private CoinSystem _coinSystem;
        private LifeSystem _lifeSystem;
        private TimeSystem _timeSystem;
        private ScoreSystem _scoreSystem;

        private LabelsSpawner _labelSpawner;

        public void Initialize(CoinSystem coinSystem, LifeSystem lifeSystem, TimeSystem timeSystem, ScoreSystem scoreSystem){
            _coinSystem = coinSystem;
            _lifeSystem = lifeSystem;
            _timeSystem = timeSystem;
            _scoreSystem = scoreSystem;
            _labelSpawner = new LabelsSpawner(scoreLabelsPrefab, scoreLabelParent);

            SubscribeEvents();
            LabelsRefresh();
        }

        private void SubscribeEvents(){
            _scoreSystem.ScoreChangedEvent += ScoreLabelUpdate;
            _scoreSystem.LabelDrawEvent += LabelsDraw;

            _lifeSystem.LifeCountChangedEvent += LifeLabelUpdate;
            _lifeSystem.LabelDrawEvent += LabelsDraw;

            _coinSystem.CoinsCountChanged += CoinsLabelUpdate;
            _timeSystem.TickEvent += TimerLabelUpdate;
        }

        private void LabelsRefresh(){
            ScoreLabelUpdate();
            LifeLabelUpdate();
            CoinsLabelUpdate();
        }

        private void LabelsDraw(Vector3 pos, string text){
            _labelSpawner.LabelSpawn(pos, text);
        }

        private void CoinsLabelUpdate(){
            coinLabel.text = _coinSystem.TotalCoin.ToString("D2");
        }

        private void ScoreLabelUpdate(){
            scoreLabel.text = _scoreSystem.TotalScore.ToString("D6");
        }

        private void LifeLabelUpdate(){
            lifeLabel.text = _lifeSystem.LifeCount.ToString();
        }

        private void TimerLabelUpdate(){
            if (timeLabel != null)
                timeLabel.text = _timeSystem.Time.ToString();
        }

        private void OnDestroy(){
            _scoreSystem.ScoreChangedEvent -= ScoreLabelUpdate;
            _scoreSystem.LabelDrawEvent -= LabelsDraw;

            _coinSystem.CoinsCountChanged -= CoinsLabelUpdate;

            _lifeSystem.LifeCountChangedEvent -= LifeLabelUpdate;
            _lifeSystem.LabelDrawEvent -= LabelsDraw;

            _timeSystem.TickEvent -= TimerLabelUpdate;
        }
    }
}