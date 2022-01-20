using PrefabSripts;
using UnityEngine;

namespace Character.BallSpawner
{
    public sealed class BallSpawner
    { 
        private Transform _firePoint;
        private Transform _fireballParent;
        private LayerMask _groundLayers;
        private FireBall[] _fireBallPool;
        private TimerCustom _timer;
        private BallSpawnerData _data;

        public bool isActive { get; set; } = false;

        public BallSpawner(Transform firePoint, LayerMask groundLayers, Transform fireballParent, BallSpawnerData data) {
            _firePoint = firePoint;
            _fireballParent = fireballParent;
            _groundLayers = groundLayers;

            _data = data;
            _timer = new TimerCustom(_data.FireDelay);
            _fireBallPool = new FireBall[_data.BulletCount];

            InitializePool();
        }   

        private void InitializePool() {
            for (int i = 0; i < _data.BulletCount; i++) {
                _fireBallPool[i] = Object.Instantiate(_data.Prefab, _fireballParent);
                _fireBallPool[i].gameObject.SetActive(false);
            }
        }

        public void Spawn() {
            if (!_timer.IsDone()) return;

            var ball = GetFreeElement();
            ball?.Initialize(_firePoint.position, _data.BulletSpeed, _data.MaxFlyHeight, _firePoint.forward, _groundLayers);           
        }

        private FireBall GetFreeElement() {
            foreach (var ball in _fireBallPool) {
                if (!ball.gameObject.activeInHierarchy)
                    return ball;
            }
            return null;
        }
    }
}

//[SerializeField] private FireBall _prefab;
//[SerializeField] private float _fireDelay;
//[SerializeField] private int _bulletCount;
//[SerializeField] private float _bulletSpeed;
//[SerializeField] private float _maxFlyHeight;