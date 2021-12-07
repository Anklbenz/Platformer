using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private PlayerInteractor _interactor;
    [SerializeField] private Character _character;
    private int score;
    private int lifes;
    private int currentChapter;
    private int currentLevel;

    private void Awake() {
       // _interactor.OnBrickHitEvent += BrickHit;        
    }

    private void BrickHit(BrickBox brick) {
        brick.BrickHit(_character);
    }

    private void OnDestroy() {
       // _interactor.OnBrickHitEvent -= BrickHit;

    }


}
