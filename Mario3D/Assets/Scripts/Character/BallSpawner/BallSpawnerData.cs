using UnityEngine;


[CreateAssetMenu]
public class BallSpawnerData : ScriptableObject
{
    [SerializeField] private FireBall _prefab;
    public FireBall Prefab => _prefab;

    [SerializeField] private float _fireDelay;
    public float FireDelay => _fireDelay;

    [SerializeField] private int _bulletCount;
    public int BulletCount => _bulletCount;

    [SerializeField] private float _bulletSpeed;
    public float BulletSpeed => _bulletSpeed;

    [SerializeField] private float _maxFlyHeight;
    public float MaxFlyHeight => _maxFlyHeight;

}
