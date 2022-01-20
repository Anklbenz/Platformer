using System.Collections.Generic;
using Interfaces;
using UnityEngine;

namespace Game
{
    public sealed class BonusSpawner
    {
        private readonly ScoreSystem _scoreSystem;
        private CoinSystem _coinCounter;
        private LifesSystem _lifesSystem;
        private SpawnFactory _spawnFactory;

        private List<IBonusSpawnNotify> _brickBonusList;
        private Transform _bonusPrefabParent;

        public BonusSpawner(ScoreSystem scoreSystem, CoinSystem coinCounter, LifesSystem lifesSystem,
            List<IBonusSpawnNotify> brickBonusList, SpawnFactory spawnFactory,                   
            Transform bonusPrefabParent) {

            _scoreSystem = scoreSystem;
            _coinCounter = coinCounter;
            _lifesSystem = lifesSystem;
            _brickBonusList = brickBonusList;
            _spawnFactory = spawnFactory;
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
            var inctance = _spawnFactory.SpawnObject(bonus.BonusType, bonus.BonusCreatePoint, _bonusPrefabParent);

            if (inctance is IScoreChangeNotify score)
                _scoreSystem.SubsсribeOnScoreEvent(score);
            if (inctance is ICoinCollectNotify coins)
                _coinCounter.SubscribeColinCollectEvent(coins);
            if (inctance is ILifeIncreaceNotify life)
                _lifesSystem.SubscribeOnIncreaceLifeEvent(life);
        }
    }
}

