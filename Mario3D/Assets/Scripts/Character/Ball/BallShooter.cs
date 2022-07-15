using Interfaces;
using UnityEngine;

namespace Character.Ball
{
    public class BallShooter
    {
        private const float HORIZONTAL_INDENT = 0.1f;

        private readonly BallSpawner _ballSpawner;
        private readonly Transform _firePoint;
        private readonly BoxCollider _collider;
        private readonly IStateData _state;
        private Vector3 _lastDirection, _lastSize;

        public BallShooter(ICharacterComponents character, IStateData state, BallSpawnerData data, Transform firePoint, Transform fireballParent,
            LayerMask groundLayer, LayerMask targetLayer){
            _state = state;
            _firePoint = firePoint;
            _collider = character.MainCollider;
            _ballSpawner = new BallSpawner(data, firePoint, fireballParent, groundLayer, targetLayer);
        }

        public void ShootInput(){
            if (_state.Data.CanShoot)
                _ballSpawner.Spawn();
        }

        public void SetFirePointPosition(Vector3 direction){
            if (direction == Vector3.zero || (direction == _lastDirection && _lastSize == _collider.size)) return;

            var size = _collider.size;
            var center = _collider.center;
            //var y = center.y + (size.y / 4);
             var y = center.y;

            _lastDirection = direction;
            _lastSize = size;

            if (direction == Vector3.back){
                _firePoint.localPosition = new Vector3(center.x, y, center.z - (HORIZONTAL_INDENT + size.z / 2));
                _firePoint.localRotation = Quaternion.Euler(new Vector3(0, 180, 0));
            }

            if (direction == Vector3.forward){
                _firePoint.localPosition = new Vector3(center.x, y, center.z + (HORIZONTAL_INDENT + size.z / 2));
                _firePoint.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
            }
        }
    }
}






