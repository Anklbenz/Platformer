using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    private readonly int[] SCORE_LIST = { 100, 200, 400, 500, 800, 1000, 2000, 4000, 5000, 10000 };

    [SerializeField] private InteractorHandler _interactionHandler;
    [SerializeField] private ScoreLabelSpawner _laberSpawner;
    [SerializeField] private Text _totalScoreLabel;

    private int _totalScore = 0;

    private void Start() {
        _interactionHandler._underInteractionHandler.OnJumpOnEvent += AddScoresOnJumpOn;
        this.TotalScoreLabelUpdate();
    }

    private void AddScoresOnJumpOn(Vector3 position, int bounceCount) {
        if (bounceCount >= SCORE_LIST.Length - 1)
            bounceCount = SCORE_LIST.Length - 1;

        var score = SCORE_LIST[bounceCount];
        AddTotalScore(score);
        LabelDraw(position, score);
    }

    private void AddTotalScore(int points) {
        _totalScore += points;
        this.TotalScoreLabelUpdate();
    }

    private void TotalScoreLabelUpdate() {
        _totalScoreLabel.text = _totalScore.ToString("D6");
    }

    private void LabelDraw(Vector3 position, int score) {
        var label = _laberSpawner.GetScoreLabel();
        label.transform.position = position;
        label.Text(score.ToString());
    }

    private void OnDestroy() {
        _interactionHandler._underInteractionHandler.OnJumpOnEvent -= AddScoresOnJumpOn;
    }
}
