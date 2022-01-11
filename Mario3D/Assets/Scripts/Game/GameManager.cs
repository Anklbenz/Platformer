using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("UI Draw")]
    [SerializeField] private Text _scoreLabel;
    [SerializeField] private Text _coinLabel;
    [SerializeField] private Text _lifesLabel;
    [SerializeField] private ScoreLabel _scoreLabelsPrefab;
    [SerializeField] private Transform _scoreLabelParent;

    [Header("BonusSpawner")]
    [SerializeField] private SpawnFactory _spawnFactory;
    [SerializeField] private Transform _bonusParent;

    [Header("Lifes")]
    [SerializeField] private int _lifes;

    private ObjectSystem objectSystem;
    private CoinSystem coinSystem;
    private ScoreSystem ScoreSystem;
    private LifesSystem lifesSystem;
    private BonusSpawner BonusSpawner;
    private UIDrawer uiDrawer;

    //particalesSpawner;

    private void Awake() {
        objectSystem = new ObjectSystem();
        coinSystem = new CoinSystem(objectSystem.CoinCollectNotifies);
        ScoreSystem = new ScoreSystem(objectSystem.ScoreNotifies);
        lifesSystem = new LifesSystem(_lifes, ScoreSystem);
        uiDrawer = new UIDrawer(ScoreSystem, coinSystem, lifesSystem, _lifesLabel, _scoreLabel, _coinLabel, _scoreLabelsPrefab, _scoreLabelParent);
        
        BonusSpawner = new BonusSpawner(ScoreSystem, coinSystem, lifesSystem, objectSystem.BonusBricksList, _spawnFactory, _bonusParent);
    }
}