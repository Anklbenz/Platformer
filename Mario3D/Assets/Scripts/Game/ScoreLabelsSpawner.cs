using UnityEngine;

public class ScoreLabelsSpawner
{
    private const int POOL_COUNT = 5;
    private PoolObjects<ScoreLabel> scoreLabelPool;

    public ScoreLabelsSpawner(ScoreLabel scoreLabelPrefab, Transform scoreParent) {
        scoreLabelPool = new PoolObjects<ScoreLabel>(scoreLabelPrefab, POOL_COUNT, true, scoreParent);
    }

    private ScoreLabel GetScoreLabel() {
        return scoreLabelPool.GetFreeElement();
    }

    public void LabelSpawn(Vector3 position, int score) {
        var label = GetScoreLabel();
        label.transform.position = position;
        label.Text(score.ToString());
    }
}
