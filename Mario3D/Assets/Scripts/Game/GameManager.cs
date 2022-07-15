using UnityEngine;

namespace Game
{
    public class GameManager : MonoBehaviour
    {
        [Header("BonusSpawner")]
        [SerializeField] private BonusFactory bonusFactory;
        [SerializeField] private Transform bonusParent;

        [Header("Life")]
        [SerializeField] private int lifeCount;

        [Header("TimeSystem")]
        [SerializeField] private int levelTime;

        [Header("UI Drawer")]
        [SerializeField] private UIDrawer uiDrawer;

        private CoinSystem _coinSystem;
        private ScoreSystem _scoreSystem;
        private LifeSystem _lifeSystem;
        private TimeSystem _timeSystem;

        private ObjectSystem _objectSystem;
        private BonusSpawner _bonusSpawner;

        private void Awake(){
            _objectSystem = new ObjectSystem();
            _scoreSystem = new ScoreSystem(_objectSystem.ScoreNotifies);
            _lifeSystem = new LifeSystem(lifeCount, _scoreSystem);
            _coinSystem = new CoinSystem(_objectSystem.CoinCollectNotifies);
            _timeSystem = new TimeSystem(levelTime);

            _bonusSpawner = new BonusSpawner(_scoreSystem, _coinSystem, _lifeSystem, _objectSystem.BonusBricksList, bonusFactory,
                bonusParent);
            
            uiDrawer.Initialize(_coinSystem, _lifeSystem,_timeSystem,_scoreSystem);
        }
    }
}