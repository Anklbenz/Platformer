using Bonus;
using Enemys;
using Enums;
using UnityEngine;

namespace Game
{
    [CreateAssetMenu]
    public class SpawnFactory : ScriptableObject
    {
        [SerializeField] Coin _coinPrefab;
        [SerializeField] GrowupBonus _growupBonusPrefab;
        [SerializeField] Flower _flowerPrefab;
        [SerializeField] LifesUpBonus _lifesUpPrefab;
        [SerializeField] JumpingStar _jumpingStarPrefab;
        [SerializeField] EnemyWalker _enemyWalkerPrefab;

        public InteractiveObject SpawnObject(BonusType bonusType, Vector3 pos, Transform _parent) {
            switch (bonusType) {
                case BonusType.coin:
                    return Get(_coinPrefab, pos, _parent);
                case BonusType.growUp:
                    return Get(_growupBonusPrefab, pos, _parent);
                case BonusType.flower:
                    return Get(_flowerPrefab, pos, _parent);
                case BonusType.lifesUp:
                    return Get(_lifesUpPrefab, pos, _parent);
                case BonusType.jumpStar:
                    return Get(_jumpingStarPrefab, pos, _parent);
                case BonusType.walker:
                    return Get(_enemyWalkerPrefab, pos, _parent);
            }
            return null;
        }

        private T Get<T>(T prefab, Vector3 pos, Transform _parent) where T : MonoBehaviour {
            return Object.Instantiate(prefab, pos, Quaternion.identity, _parent);
        }
    }
}
