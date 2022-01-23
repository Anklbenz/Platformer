using PrefabSripts;
using UnityEngine;
using UnityEngine.UI;
using System;

namespace Game
{
    public class UIDrawer: IDisposable
    {
        private readonly Text _coinLabel;
        private readonly Text _lifeLabel;
        private readonly Text _timeLabel;
        private readonly Text _scoreLabel;
        private readonly CoinSystem _coinSystem;
        private readonly LifeSystem _lifeSystem;
        private readonly TimeSystem _timeSystem;
        private readonly ScoreSystem _scoreSystem;
        private readonly LabelsSpawner _labelSpawner;

        public UIDrawer(ScoreSystem scoreSystem, CoinSystem coinSystem, LifeSystem lifeSystem, TimeSystem timeSystem,
            Text lifeLabel, Text scoreLabel, Text coinLabel, Text timeLabel, ScoreLabel prefab, Transform labelParent){
            
            _scoreSystem = scoreSystem;
            _scoreLabel = scoreLabel;

            _coinSystem = coinSystem;
            _coinLabel = coinLabel;

            _lifeSystem = lifeSystem;
            _lifeLabel = lifeLabel;

            _timeSystem = timeSystem;
            _timeLabel = timeLabel;

            _labelSpawner = new LabelsSpawner(prefab, labelParent);

            this.SubscribeOnChangeUIEvents();
            this.ScoreLabelUpdate();
            this.LifeLabelUpdate();
            this.CoinsLabelUpdate();
        }

        private void SubscribeOnChangeUIEvents(){
            _scoreSystem.ScoreChangedEvent += ScoreLabelUpdate;
            _scoreSystem.LabelDrawEvent += LabelsDraw;

            _coinSystem.CoinsCountChanged += CoinsLabelUpdate;

            _lifeSystem.LifeCountChangedEvent += LifeLabelUpdate;
            _lifeSystem.LabelDrawEvent += LabelsDraw;

            _timeSystem.TickEvent += TimerLabelUpdate;
        }

        private void LabelsDraw(Vector3 pos, string text){
            _labelSpawner.LabelSpawn(pos, text);
        }

        private void CoinsLabelUpdate(){
            _coinLabel.text = _coinSystem.TotalCoin.ToString("D2");
        }

        private void ScoreLabelUpdate(){
            _scoreLabel.text = _scoreSystem.TotalScore.ToString("D6");
        }

        private void LifeLabelUpdate(){
            _lifeLabel.text = _lifeSystem.LifeCount.ToString();
        }

        private void TimerLabelUpdate(){
            if (_timeLabel != null)
                _timeLabel.text = _timeSystem.Time.ToString();
        }

        public void Dispose(){
            _scoreSystem.ScoreChangedEvent -= ScoreLabelUpdate;
            _scoreSystem.LabelDrawEvent -= LabelsDraw;

            _coinSystem.CoinsCountChanged -= CoinsLabelUpdate;

            _lifeSystem.LifeCountChangedEvent -= LifeLabelUpdate;
            _lifeSystem.LabelDrawEvent -= LabelsDraw;

            _timeSystem.TickEvent -= TimerLabelUpdate;
        }
    }
}
