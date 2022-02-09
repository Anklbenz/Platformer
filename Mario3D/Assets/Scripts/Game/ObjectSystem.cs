using System.Collections.Generic;
using Enums;
using Interfaces;
using UnityEngine;

namespace Game
{
    public class ObjectSystem
    {
        public readonly List<IBonusSpawn> BonusBricksList = new List<IBonusSpawn>();
        public readonly List<IScoreChangeNotify> ScoreNotifies = new List<IScoreChangeNotify>();
        public readonly List<ICoinCollectNotify> CoinCollectNotifies = new List<ICoinCollectNotify>();
        public readonly List<IFirstScreenActivate> ScreenActivatorSensitives = new List<IFirstScreenActivate>();

        private readonly List<MonoBehaviour> _listAllObjects = new List<MonoBehaviour>();

        public ObjectSystem() {
            _listAllObjects.AddRange(Object.FindObjectsOfType<MonoBehaviour>());
            SortObjectsOfType();
        }

        private void SortObjectsOfType() {
            foreach (var instance in _listAllObjects) {
                if (instance is IScoreChangeNotify notify)
                    ScoreNotifies.Add(notify);

                if (instance is IBonusSpawn bonus)
                    BonusBricksList.Add(bonus);

                if (instance is ICoinCollectNotify coin)
                    CoinCollectNotifies.Add(coin);

                if (instance is IFirstScreenActivate enemy)
                    //ScreenActivatorSensitives.Add(enemy);
                    enemy.Standby();
              
            }
        }
    }
}
