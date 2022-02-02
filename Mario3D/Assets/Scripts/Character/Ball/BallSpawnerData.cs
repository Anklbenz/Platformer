using PrefabSripts;
using UnityEngine;

namespace Character.Ball
{
    [CreateAssetMenu]
    public class BallSpawnerData : ScriptableObject
    {
        [SerializeField] private FireBall _prefab;
        [SerializeField] private float fireDelay;
        [SerializeField] private int bulletCount;
        [SerializeField] private float bulletSpeed;
        [SerializeField] private float maxFlyHeight;
        [SerializeField] private float hitForce;
        public float HitForce => hitForce;
        public FireBall Prefab => _prefab;
        public float FireDelay => fireDelay;
        public int BulletCount => bulletCount;
        public float BulletSpeed => bulletSpeed;
        public float MaxFlyHeight => maxFlyHeight;
    }
}
