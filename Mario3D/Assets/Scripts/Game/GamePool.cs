using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePool : MonoBehaviour
{
    [SerializeField] private int scoreLabelPoolCount;
    [SerializeField] private ScoreLabel scoreLabelPrefab;
    [SerializeField] private Transform scoreParent;

    [SerializeField] private int brickParticalesPoolCount;
    [SerializeField] private BrickParticales brickParticales;
    [SerializeField] private Transform brickParent;

    private PoolObjects<ScoreLabel> scoreLabelPool;
    private PoolObjects<BrickParticales> brickParticalesPool;
    

    private void Awake() {

        scoreLabelPool = new PoolObjects<ScoreLabel>(scoreLabelPrefab, scoreLabelPoolCount, true, scoreParent);
        brickParticalesPool = new PoolObjects<BrickParticales>(brickParticales, scoreLabelPoolCount, true, brickParent);
    }

    public ScoreLabel GetScoreLabel() {
        return scoreLabelPool.GetFreeElement();
    }

    public BrickParticales GetBrickParticales() {
        return brickParticalesPool.GetFreeElement();
    }
}
