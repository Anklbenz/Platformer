using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private InteractorHandler _playerInteractionHandler;
   
    [Header("ScoreManager")]
    [SerializeField] private Text _totalScoreLabel;
    [SerializeField] private ScoreLabel _scoreLabelPrefab;
    [SerializeField] private Transform _scoreLabelParent;

    private ScoreManager _scoreManager;
     
    private void Start() {
        _scoreManager = new ScoreManager(_totalScoreLabel, _scoreLabelPrefab, _scoreLabelParent);
        _playerInteractionHandler._underInteractionHandler.OnJumpOnEvent += _scoreManager.AddScore;
    }

    private void OnDestroy() {
        _playerInteractionHandler._underInteractionHandler.OnJumpOnEvent -= _scoreManager.AddScore;
    }

}
