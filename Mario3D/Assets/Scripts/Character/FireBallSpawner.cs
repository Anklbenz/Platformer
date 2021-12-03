using UnityEngine;

public class FireBallSpawner : MonoBehaviour
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
        _fireBallPool = new FireBall[_bulletCount];
        timer = new TimerCustom(_fireDelay);
        for (int i = 0; i < _bulletCount; i++) {
            _fireBallPool[i] = Instantiate(_prefab, transform);
            _fireBallPool[i].gameObject.SetActive(false);
            _fireBallPool[i].transform.parent = _fireballParent;
        }
    }

    void Update() {
        if (isActive && Input.GetKeyDown(KeyCode.Space))
            Shoot();
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

//private void Shoot() {
//    if (Time.time >= nextFire) {
//        foreach (var ball in _fireBallPool) {
//            if (!ball.gameObject.activeInHierarchy) {
//                ball.Initialize(_firePoint.position, _bulletSpeed, _maxFlyHeight, _firePoint.forward, _groundLayers);
//                ball.gameObject.SetActive(true);
//                nextFire = Time.time + _fireDelay;
//                return;
//            }
//        }
//    }
//}