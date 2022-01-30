using PrefabSripts;
using UnityEngine;

namespace Character.BallSpawner
{
    [CreateAssetMenu]
    public class BallSpawnerData : ScriptableObject
    {
        [SerializeField] private FireBall _prefab;
        [SerializeField] private float _fireDelay;
        [SerializeField] private int _bulletCount;
        [SerializeField] private float _bulletSpeed;
        [SerializeField] private float _maxFlyHeight;
        [SerializeField] private float hitForce;
        public FireBall Prefab => _prefab;

        public float FireDelay => _fireDelay;

        public int BulletCount => _bulletCount;

        public float BulletSpeed => _bulletSpeed;

        public float MaxFlyHeight => _maxFlyHeight;

        public float HitForce => hitForce;
    }
}
