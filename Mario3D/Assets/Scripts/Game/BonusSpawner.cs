using System.Collections.Generic;
using Interfaces;
using UnityEngine;

namespace Game
{
    public sealed class BonusSpawner
    {
        private readonly ScoreSystem _scoreSystem;
        private readonly CoinSystem _coinCounter;
        private readonly LifeSystem _lifeSystem;
        private readonly BonusFactory _bonusFactory;

        private readonly List<IBonusSpawn> _brickBonusList;
        private readonly Transform _bonusPrefabParent;

        public BonusSpawner(ScoreSystem scoreSystem, CoinSystem coinCounter, LifeSystem lifeSystem, List<IBonusSpawn> brickBonusList,
            BonusFactory bonusFactory, Transform bonusPrefabParent){

            _scoreSystem = scoreSystem;
            _coinCounter = coinCounter;
            _lifeSystem = lifeSystem;
            _brickBonusList = brickBonusList;
            _bonusFactory = bonusFactory;
            _bonusPrefabParent = bonusPrefabParent;
            InitBrickHitEvent();
        }

        private void InitBrickHitEvent(){
            foreach (var brick in _brickBonusList)
                SubscribeBonusSpawnEventHolder(brick);
        }

        private void SubscribeBonusSpawnEventHolder(IBonusSpawn sender){
            sender.BonusSpawnEvent += BonusSpawn;
        }

        private void BonusSpawn(IBonusSpawn sender){
            var instance = _bonusFactory.SpawnObject(sender.BonusType, sender.BonusCreatePoint, _bonusPrefabParent);

            if (instance is IScoreChangeNotify score)
                _scoreSystem.SubscribeOnScoreEvent(score);
            if (instance is ICoinCollectNotify coins)
                _coinCounter.SubscribeColinCollectEvent(coins);
            if (instance is ILifeIncreaseNotify life)
                _lifeSystem.SubscribeOnIncreaseLifeEvent(life);
        }
    }
}