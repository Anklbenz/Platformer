using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreLabelSpawner : MonoBehaviour
{
    [SerializeField] private int scoreLabelPoolCount;
    [SerializeField] private ScoreLabel scoreLabelPrefab;
    [SerializeField] private Transform scoreParent;

    private PoolObjects<ScoreLabel> scoreLabelPool;
    private PoolObjects<BrickParticales> brickParticalesPool;
 
    private void Awake() {
        scoreLabelPool = new PoolObjects<ScoreLabel>(scoreLabelPrefab, scoreLabelPoolCount, true, scoreParent);
    }

    public ScoreLabel GetScoreLabel() {
        return scoreLabelPool.GetFreeElement();
    }

    public BrickParticales GetBrickParticales() {
        return brickParticalesPool.GetFreeElement();
    }
}
