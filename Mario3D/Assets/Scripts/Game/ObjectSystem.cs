using System.Collections.Generic;
using Interfaces;
using UnityEngine;

namespace Game
{
    public class ObjectSystem
    {
        public List<IBonusSpawn> BonusBricksList = new List<IBonusSpawn>();
        public List<IScoreChangeNotify> ScoreNotifies = new List<IScoreChangeNotify>();
        public List<ICoinCollectNotify> CoinCollectNotifies = new List<ICoinCollectNotify>();

        private List<MonoBehaviour> allObjectsList = new List<MonoBehaviour>();

        public ObjectSystem() {
            allObjectsList.AddRange(Object.FindObjectsOfType<MonoBehaviour>());
            SortObjectsOfType();
        }

        private void SortObjectsOfType() {
            foreach (var instance in allObjectsList) {
                if (instance is IScoreChangeNotify)
                    ScoreNotifies.Add(instance as IScoreChangeNotify);

                if (instance is IBonusSpawn)
                    BonusBricksList.Add(instance as IBonusSpawn);

                if (instance is ICoinCollectNotify)
                    CoinCollectNotifies.Add(instance as ICoinCollectNotify);
            }
        }
    }
}
