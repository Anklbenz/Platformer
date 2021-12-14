using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{   
    [Header("ScoreManager")]
    [SerializeField] private Text _totalScoreLabel;
    [SerializeField] private ScoreLabel _scoreLabelPrefab;
    [SerializeField] private Transform _scoreLabelParent;

    public ScoreManager ScoreManager;   
  
    private void Awake() {
        ScoreManager = new ScoreManager(_totalScoreLabel, _scoreLabelPrefab, _scoreLabelParent);      
    }



}
