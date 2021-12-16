using UnityEngine;
using MyEnums;

public sealed class BonusSpawner 
{
    private const int COIN_POOL_AMOUNT = 5;

    private Coin _coinPrefab;
    private Mushroom _mushroomPrefab;
    private Flower _flowerPrefab;

    private PoolObjects<Coin> _coinsPool;
    private Transform _bonusPrefabParent;

    public BonusSpawner(Coin coinPrefab, Mushroom muroomPrefab, Flower flowerPreab, Transform bonusPrefabParent) {
        _coinPrefab = coinPrefab;
        _mushroomPrefab = muroomPrefab;
        _flowerPrefab = flowerPreab;
        _bonusPrefabParent = bonusPrefabParent;
        _coinsPool = new PoolObjects<Coin>(_coinPrefab, COIN_POOL_AMOUNT, true, bonusPrefabParent);
    }

    public void Subscribe(IBonus sender) {
        sender.BonusSpawnEvent += BonusSpawn;
    }

    public void UnSubscribe(IBonus sender) {
        sender.BonusSpawnEvent -= BonusSpawn;
    }

    public void BonusSpawn(IBonus bonus) { // Сделать фабрику
        switch (bonus.BonusType) {
            case BonusType.coin:
                var coin = _coinsPool.GetFreeElement();
                coin.transform.position = bonus.BonusCreatePoint;
                break;

            case BonusType.mushroom:
                UnityEngine.Object.Instantiate(_mushroomPrefab.gameObject, bonus.BonusCreatePoint, Quaternion.identity, _bonusPrefabParent);
                break;
            case BonusType.flower:
                UnityEngine.Object.Instantiate(_flowerPrefab, bonus.BonusCreatePoint, Quaternion.identity, _bonusPrefabParent);
                break;
            case BonusType.heathUp:
                break;
        }
    }


}

