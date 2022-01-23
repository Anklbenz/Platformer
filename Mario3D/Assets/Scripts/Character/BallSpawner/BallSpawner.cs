using PrefabSripts;
using UnityEngine;

namespace Character.BallSpawner
{
    public sealed class BallSpawner
    { 
        private readonly Transform _firePoint;
        private readonly BallSpawnerData _data;
        private readonly LayerMask _groundLayers;
        private readonly FireBall[] _fireBallPool;
        private readonly Transform _fireballParent;

        private float _nextFire;

        public BallSpawner(Transform firePoint, LayerMask groundLayers, Transform fireballParent, BallSpawnerData data){
            _data = data;
            _firePoint = firePoint;
            _groundLayers = groundLayers;
            _fireballParent = fireballParent;
            _fireBallPool = new FireBall[_data.BulletCount];

            InitializePool();
        }

        private void InitializePool() {
            for (var i = 0; i < _data.BulletCount; i++) {
                _fireBallPool[i] = Object.Instantiate(_data.Prefab, _fireballParent);
                _fireBallPool[i].gameObject.SetActive(false);
            }
        }

        public void Spawn() {
            if (AllBallsIsFree() || Timer()){
                var ball = GetFreeBall();
                
                if(ball!=null)
                    ball.Initialize(_firePoint.position, _data.BulletSpeed, _data.MaxFlyHeight, _firePoint.forward, _groundLayers);
            }
        }
        
        private bool Timer(){
            if (Time.realtimeSinceStartup < _nextFire + _data.FireDelay) return false;
            _nextFire = Time.realtimeSinceStartup;
            return true;
        }

        private bool AllBallsIsFree(){
            foreach (var ball in _fireBallPool) {
                if (ball.gameObject.activeInHierarchy)
                    return false;
            }
            return true;
        }
        
        private FireBall GetFreeBall() {
            foreach (var ball in _fireBallPool) {
                if (!ball.gameObject.activeInHierarchy)
                    return ball;
            }
            return null;
        }
    }
}
