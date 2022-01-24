using Bonus;
using Enemy;
using Enums;
using UnityEngine;

namespace Game
{
    [CreateAssetMenu]
    public class BonusFactory : Factory
    {
        [SerializeField] Coin _coinPrefab;
        [SerializeField] GrowupBonus _growupBonusPrefab;
        [SerializeField] Flower _flowerPrefab;
        [SerializeField] LifesUpBonus _lifesUpPrefab;
        [SerializeField] JumpingStar _jumpingStarPrefab;
        [SerializeField] EnemyWalker _enemyWalkerPrefab;

        public InteractiveObject SpawnObject(BonusType bonusType, Vector3 pos, Transform parent){
            switch (bonusType){
                case BonusType.Coin:
                    return Get(_coinPrefab, pos, parent);
                case BonusType.GrowUp:
                    return Get(_growupBonusPrefab, pos, parent);
                case BonusType.Flower:
                    return Get(_flowerPrefab, pos, parent);
                case BonusType.LifeUp:
                    return Get(_lifesUpPrefab, pos, parent);
                case BonusType.JumpStar:
                    return Get(_jumpingStarPrefab, pos, parent);
                case BonusType.Walker:
                    return Get(_enemyWalkerPrefab, pos, parent);
            }

            return null;
        }
        
    }
}
