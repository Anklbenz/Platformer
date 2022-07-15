using MyEnums;
using System.Collections.Generic;
using UnityEngine;

public sealed class BonusSpawner
{
    private Coin _coinPrefab;
    private GrowupBonus _growupPrefab;
    private Flower _flowerPrefab;
    private LifesUpBonus _lifesUpPrefab;
    private Transform _bonusPrefabParent;
    private ScoreSystem _scoreSystem;
    private LifesSystem _lifesSystem;
    private CoinSystem _coinCounter;
    private List<IBonusSpawnNotify> _brickBonusList;

    public BonusSpawner(ScoreSystem scoreSystem, CoinSystem coinCounter, LifesSystem lifesSystem,
                        List<IBonusSpawnNotify> brickBonusList,
                        Coin coinPrefab, GrowupBonus muroomPrefab, Flower flowerPreab, LifesUpBonus lifesUpPrefab,
                        Transform bonusPrefabParent) {

        _scoreSystem = scoreSystem;
        _coinCounter = coinCounter;
        _lifesSystem = lifesSystem;
        _brickBonusList = brickBonusList;
        _coinPrefab = coinPrefab;
        _growupPrefab = muroomPrefab;
        _flowerPrefab = flowerPreab;
        _lifesUpPrefab = lifesUpPrefab;
        _bonusPrefabParent = bonusPrefabParent;
        InitBrickHitEvent();
    }

    private void InitBrickHitEvent() {
        foreach (var brick in _brickBonusList)
            SubscribeBonusSpawEventHolder(brick);
    }

    public void SubscribeBonusSpawEventHolder(IBonusSpawnNotify sender) {
        sender.BonusSpawnEvent += BonusSpawn;
    }

    public void BonusSpawn(IBonusSpawnNotify bonus) {

    }

    //public void BonusSpawn(IBonusSpawnNotify bonus) { 
    //    switch (bonus.BonusType) {           
    //        case BonusType.coin:
    //            Coin coin = Get(_coinPrefab, bonus.BonusCreatePoint);
    //            _scoreSystem.SubsсribeOnScoreEvent(coin);
    //            _coinCounter.SubscribeColinCollectEvent(coin);
    //            break;

    //        case BonusType.growUp:
    //            GrowupBonus mushroom = Get(_growupPrefab, bonus.BonusCreatePoint);
    //            _scoreSystem.SubsсribeOnScoreEvent(mushroom);
    //            break;

    //        case BonusType.flower:
    //            Flower flower = Get(_flowerPrefab, bonus.BonusCreatePoint);
    //            _scoreSystem.SubsсribeOnScoreEvent(flower);
    //            break;

    //        case BonusType.lifesUp:
    //            LifesUpBonus lifesUpBonus = Get(_lifesUpPrefab, bonus.BonusCreatePoint);
    //            _lifesSystem.SubscribeOnIncreaceLifeEvent(lifesUpBonus);
    //            break;
    //    }
    //}

    //private T Get<T>(T prefab, Vector3 pos) where T: MonoBehaviour {
    //    return Object.Instantiate(prefab, pos, Quaternion.identity, _bonusPrefabParent);
    //}
}

