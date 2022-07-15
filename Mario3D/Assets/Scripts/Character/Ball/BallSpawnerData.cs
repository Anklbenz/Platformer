using PrefabSripts;
using UnityEngine;

namespace Character.Ball
{
    [CreateAssetMenu]
    public class BallSpawnerData : ScriptableObject
    {
        [SerializeField] private FireBall _prefab;
        [SerializeField] private int bulletCount;
        [SerializeField] private float fireDelay;
        [SerializeField] private float bulletSpeed;
        [SerializeField] private float maxFlyHeight;
        [SerializeField] private float hitForce;
        [SerializeField] private float angle;
        public float Angle => angle;
        public FireBall Prefab => _prefab;
        public float HitForce => hitForce;
        public float FireDelay => fireDelay;
        public int BulletCount => bulletCount;
        public float BulletSpeed => bulletSpeed;
        public float MaxFlyHeight => maxFlyHeight;
    }
}
