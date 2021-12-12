using UnityEngine;
using UnityEngine.UI;
using System;

public class ScoreManager 
{
    private readonly int[] SCORE_LIST = { 100, 200, 400, 500, 800, 1000, 2000, 4000, 5000, 10000 };

    private int _totalScore = 0;
    private Text _totalScoreLabel;
    private ScoreLabelsSpawner _lablesSpawner;

    public ScoreManager(Text totalScoreLabel, ScoreLabel prefab, Transform parent) {
        _lablesSpawner = new ScoreLabelsSpawner(prefab, parent);            
        _totalScoreLabel = totalScoreLabel;
         this.TotalScoreLabelUpdate();
    }
   
    public void AddScore(Vector3 position, int bounceCount) {
        if (bounceCount >= SCORE_LIST.Length - 1)
            bounceCount = SCORE_LIST.Length - 1;

        var score = SCORE_LIST[bounceCount];
        this.TotalScoreUpdate(score);
        _lablesSpawner.LabelSpawn(position, score);
    }

    private void TotalScoreUpdate(int points) {
        _totalScore += points;
        this.TotalScoreLabelUpdate();
    }

    private void TotalScoreLabelUpdate() {
        _totalScoreLabel.text = _totalScore.ToString("D6");
    }
}
