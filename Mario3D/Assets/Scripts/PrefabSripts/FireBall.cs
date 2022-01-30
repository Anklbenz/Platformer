using Enemy;
using Interfaces;
using UnityEngine;

namespace PrefabSripts
{
    public sealed class FireBall : MonoBehaviour, IScreenDeactivator
    {
        private const float MIN_FLY_HEIGHT = 0.1f;
     
        private Vector3 _moveDirection, _ricochetStartPoint;
        private LayerMask _groundLayer, _targetLayer;
        private float _bulletSpeed, _maxFlyHeight, _hitForce;
        private bool _ricochetHappened = false;

        private Rigidbody _rigidbody;
        private Collider _collider;

        private bool RayVectorDown => Physics.Raycast(_collider.bounds.center, Vector3.down, out var rayHit,
            _collider.bounds.size.y / 2 + MIN_FLY_HEIGHT, _groundLayer);

        private void Awake(){
            _rigidbody = GetComponent<Rigidbody>();
            _collider = GetComponent<Collider>();
        }

        public void Initialize(float bulletSpeed, float maxFlyHeight, float hitForce, LayerMask groundLayer, LayerMask targetLayer){
            _bulletSpeed = bulletSpeed;
            _maxFlyHeight = maxFlyHeight;
            _hitForce = hitForce;
            _targetLayer = targetLayer;
            _groundLayer = groundLayer;
        }

        public void Activate(Vector3 position, Vector3 moveDirection){
            gameObject.SetActive(true);
            transform.position = position;
            
            _moveDirection = moveDirection + Vector3.down; // 45 degrees down
            _ricochetHappened = false;
        }

        private void FixedUpdate(){
            if (!_ricochetHappened && RayVectorDown)
                RicochetState();
            Move();
        }

        private void Move(){
            if (_ricochetHappened){
                if (_maxFlyHeight <= Vector3.Distance(_ricochetStartPoint, transform.position)){
                    ChangeVerticalDirection();
                    _ricochetStartPoint = transform.position;
                }
            }
            _rigidbody.velocity = _moveDirection * _bulletSpeed;
        }

        private void RicochetState(){
            ChangeVerticalDirection();
            _ricochetHappened = true;
            _ricochetStartPoint = transform.position;
        }

        private void ChangeVerticalDirection(){
            _moveDirection = new Vector3(_moveDirection.x, _moveDirection.y * -1, _moveDirection.z);
        }

        private void OnTriggerEnter(Collider other){
            if ((_targetLayer.value & (1 << other.transform.gameObject.layer)) > 0)
                Hit(other);
        }

        private void Hit(Collider other){
            var activeEnemy = other.GetComponentInParent<ActiveEnemy>();
            if (activeEnemy)
                activeEnemy.DownHit(_hitForce);
            
            Deactivate();
        }

        public void Deactivate(){
            gameObject.SetActive(false);
        }

        private void OnDrawGizmos(){
            if (_collider == null) return;
            Debug.DrawLine(transform.position, transform.position + Vector3.down * (_collider.bounds.size.y / 2 + MIN_FLY_HEIGHT));
        }
    }
}

