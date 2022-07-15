using PrefabSripts;
using UnityEngine;

namespace Game
{
    public class LabelsSpawner
    {
        private const int POOL_COUNT = 5;
        private readonly PoolObjects<ScoreLabel> _scoreLabelPool;

        public LabelsSpawner(ScoreLabel scoreLabelPrefab, Transform scoreParent) {
            _scoreLabelPool = new PoolObjects<ScoreLabel>(scoreLabelPrefab, POOL_COUNT, true, scoreParent);
        }

        private ScoreLabel GetScoreLabel() {
            return _scoreLabelPool.GetFreeElement();
        }

        public void LabelSpawn(Vector3 position, string text) {
            var label = GetScoreLabel();
            label.transform.position = position;
            label.Text(text);
        }
    }
}
