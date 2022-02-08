using PrefabSripts;
using UnityEngine;

namespace Character.Ball
{
    public sealed class BallSpawner
    {
        private readonly BallSpawnerData _data;
        private readonly FireBall[] _fireBallPool;
        private readonly Transform _firePoint;
        private float _nextFire;

        public BallSpawner(BallSpawnerData data, Transform firePoint, Transform fireballParent, LayerMask groundLayer,
            LayerMask targetLayer){
            _data = data;
            _firePoint = firePoint;
            _fireBallPool = new FireBall[_data.BulletCount];

            InitializePool(groundLayer, targetLayer, fireballParent);
        }

        private void InitializePool(LayerMask groundLayer, LayerMask targetLayer, Transform fireballParent){
            for (var i = 0; i < _data.BulletCount; i++){
                _fireBallPool[i] = Object.Instantiate(_data.Prefab, fireballParent);
                _fireBallPool[i].Initialize(_data.BulletSpeed, _data.MaxFlyHeight, _data.HitForce, _data.Angle, groundLayer, targetLayer);
                _fireBallPool[i].gameObject.SetActive(false);
            }
        }

        public void Spawn(){
            if (AllBallsIsFree() || ShootTimer()){
                var ball = GetFreeBall();

                if (ball != null)
                    ball.Activate(_firePoint.position, _firePoint.forward);
            }
        }

        private bool ShootTimer(){
            if (Time.realtimeSinceStartup < _nextFire + _data.FireDelay) return false;
            _nextFire = Time.realtimeSinceStartup;
            return true;
        }

        private bool AllBallsIsFree(){
            foreach (var ball in _fireBallPool){
                if (ball.gameObject.activeInHierarchy)
                    return false;
            }

            return true;
        }

        private FireBall GetFreeBall(){
            foreach (var ball in _fireBallPool){
                if (!ball.gameObject.activeInHierarchy)
                    return ball;
            }

            return null;
        }
    }
}
