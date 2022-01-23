using PrefabSripts;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class GameManager : MonoBehaviour
    {
        [Header("UI Draw")]
        [SerializeField] private Text scoreLabel;

        [SerializeField] private Text coinLabel;
        [SerializeField] private Text lifeLabel;
        [SerializeField] private Text timeLabel;
        [SerializeField] private ScoreLabel scoreLabelsPrefab;
        [SerializeField] private Transform scoreLabelParent;

        [Header("BonusSpawner")]
        [SerializeField] private BonusFactory bonusFactory;

        [SerializeField] private Transform bonusParent;

        [Header("Life")]
        [SerializeField] private int lifeCount;

        [Header("TimeSystem")]
        [SerializeField] private int levelTime;

        private ObjectSystem _objectSystem;
        private CoinSystem _coinSystem;
        private ScoreSystem _scoreSystem;
        private LifeSystem _lifeSystem;
        private BonusSpawner _bonusSpawner;
        private TimeSystem _timeSystem;
        private UIDrawer _uiDrawer;


        private void Awake(){
            _objectSystem = new ObjectSystem();
            _scoreSystem = new ScoreSystem(_objectSystem.ScoreNotifies);
            _lifeSystem = new LifeSystem(lifeCount, _scoreSystem);
            _coinSystem = new CoinSystem(_objectSystem.CoinCollectNotifies);
            _timeSystem = new TimeSystem(levelTime);
            _uiDrawer = new UIDrawer(_scoreSystem, _coinSystem, _lifeSystem, _timeSystem, lifeLabel, scoreLabel, coinLabel, timeLabel,
                scoreLabelsPrefab, scoreLabelParent);

            _bonusSpawner = new BonusSpawner(_scoreSystem, _coinSystem, _lifeSystem, _objectSystem.BonusBricksList, bonusFactory,
                bonusParent);
        }
    }
}