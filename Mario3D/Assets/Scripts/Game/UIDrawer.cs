using UnityEngine;
using UnityEngine.UI;

public class UIDrawer
{
    private Text _scoreLabel;
    private Text _coinLabel;
    private Text _lifesLabel;
    private LabelsSpawner _lablesSpawner;
    private ScoreSystem _scoreSystem;
    private CoinSystem _coinSystem;
    private LifesSystem _lifesSystem;

    public UIDrawer(ScoreSystem scoreSystem, CoinSystem coinSystem, LifesSystem lifesSystem, Text lifesLabel, Text scoreLabel, Text coinLablel, ScoreLabel prefab, Transform labelParent) {
        _scoreSystem = scoreSystem;
        _scoreLabel = scoreLabel;

        _coinSystem = coinSystem;
        _coinLabel = coinLablel;

        _lifesSystem = lifesSystem;
        _lifesLabel = lifesLabel;

        _lablesSpawner = new LabelsSpawner(prefab, labelParent);

        this.SubscribeOnChangeUIEvents();
        this.ScoreLabelUpdate();
        this.LifesLabelUpdate();
        this.CoinsLabelUpdate();
    }

    private void SubscribeOnChangeUIEvents() {
        _scoreSystem.ScoreChangedEvent += ScoreLabelUpdate;
        _scoreSystem.LabelDrawEvent += LabelsDraw;

        _coinSystem.CoinsCountChanged += CoinsLabelUpdate;

        _lifesSystem.LifeCountChangedEvent += LifesLabelUpdate;
        _lifesSystem.LabelDrawEvent += LabelsDraw;
    }

    private void LabelsDraw(Vector3 pos, string text) {
        _lablesSpawner.LabelSpawn(pos, text);
    }

    private void CoinsLabelUpdate() {
        _coinLabel.text = _coinSystem.TotalCoin.ToString("D2");
    }

    private void ScoreLabelUpdate() {
        _scoreLabel.text = _scoreSystem.TotalScore.ToString("D6");
    }   
    
    private void LifesLabelUpdate() {
        _lifesLabel.text = _lifesSystem.LifesCount.ToString();
    }
}
