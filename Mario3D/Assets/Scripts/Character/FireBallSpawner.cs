using UnityEngine;

public sealed class FireBallSpawner : MonoBehaviour
{
    [SerializeField] private FireBall _prefab;
    [SerializeField] private Transform _firePoint;
    [SerializeField] private float _fireDelay = 1f;
    [SerializeField] private int _bulletCount;
    [SerializeField] private float _bulletSpeed;
    [SerializeField] private float _maxFlyHeight;
    [SerializeField] private LayerMask _groundLayers;
    [SerializeField] private Transform _fireballParent;
    private FireBall[] _fireBallPool;
    private TimerCustom timer;

    public bool isActive { get; set; } = false;

    private void Awake() {
        timer = new TimerCustom(_fireDelay);
        InitializePool();
    }

    private void Update() {
        if (isActive && Input.GetKeyDown(KeyCode.Space))
            Shoot();
    }

    private void InitializePool() {
        _fireBallPool = new FireBall[_bulletCount];

        for (int i = 0; i < _bulletCount; i++) {
            _fireBallPool[i] = Instantiate(_prefab, transform);
            _fireBallPool[i].gameObject.SetActive(false);
            _fireBallPool[i].transform.parent = _fireballParent;
        }
    }

    private void Shoot() {
        if (timer.IsDone()) {
            foreach (var ball in _fireBallPool) {
                if (!ball.gameObject.activeInHierarchy) {
                    ball.Initialize(_firePoint.position, _bulletSpeed, _maxFlyHeight, _firePoint.forward, _groundLayers);
                    ball.gameObject.SetActive(true);
                    return;
                }
            }
        }
    }
}

