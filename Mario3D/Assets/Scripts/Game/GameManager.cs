using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private InteractorHandler _playerinteractorHandler;

    [Header("ScoreManager")]
    [SerializeField] private Text _totalScoreLabel;
    [SerializeField] private ScoreLabel _scoreLabelPrefab;
    [SerializeField] private Transform _scoreLabelParent;

    [Header("BonusSpawner")]
    [SerializeField] private Coin _coinPrefab;
    [SerializeField] private Mushroom _mushroomPrefab;
    [SerializeField] private Flower _flowerPrefab;
    [SerializeField] private Transform _bonusParent;

    public EnemySystem EnemySystem = new EnemySystem();
    public ScoreSystem ScoreSystem;
    public BonusSpawner BonusSpawner;
    //particalesSpawner;
      
    private void Awake() {

        ScoreSystem = new ScoreSystem(_totalScoreLabel, _scoreLabelPrefab, _scoreLabelParent);
        EnemySystem.EventNotifySubsribe();
        BonusSpawner = new BonusSpawner(_coinPrefab, _mushroomPrefab, _flowerPrefab, _bonusParent);
    }
}
