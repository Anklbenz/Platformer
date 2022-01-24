using Enemy;
using UnityEngine;

namespace PrefabSripts
{
    public sealed class FireBall : MonoBehaviour
    {
        private const float MIN_FLY_HEIGHT = 0.1f;
        private LayerMask _groundLayers;
        private float _bulletSpeed;
        private float _maxFlyHeight;
        private Vector3 _moveDirection;
        private Vector3 _ricochetStartPoint;
        private bool _ricochetHappened = false;

        private Rigidbody _rigidbody;
        private Collider _collider;

        private bool RayVectorDown => Physics.Raycast(_collider.bounds.center, Vector3.down, out var rayHit,
            _collider.bounds.size.y / 2 + MIN_FLY_HEIGHT, _groundLayers);

        private void Awake(){
            _rigidbody = GetComponent<Rigidbody>();
            _collider = GetComponent<Collider>();
        }

        public void Initialize(Vector3 position, float bulletSpeed, float ricochetHeight, Vector3 moveDirection, LayerMask groundLayers){
            gameObject.SetActive(true);
            transform.position = position;
            _bulletSpeed = bulletSpeed;
            _maxFlyHeight = ricochetHeight;
            _moveDirection = moveDirection + Vector3.down; // 45 degrees down
            _groundLayers = groundLayers;
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
            Hit(other);
        }

        private void Hit(Collider other){
            var activeEnemy = other.GetComponentInParent<ActiveEnemy>();
            if (activeEnemy)
                activeEnemy.DownHit();

            Deactivate();
        }

        private void Deactivate(){
            gameObject.SetActive(false);
        }

        private void OnDrawGizmos(){
            if (_collider == null) return;
            Debug.DrawLine(transform.position, transform.position + Vector3.down * (_collider.bounds.size.y / 2 + MIN_FLY_HEIGHT));
        }
    }
}

