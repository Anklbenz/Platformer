using Enemy;
using Enums;
using Interfaces;
using UnityEngine;

namespace PrefabSripts
{
    public sealed class FireBall : MonoBehaviour, IFirstScreenDeactivate
    {
        private const float MIN_FLY_HEIGHT = 0.3f;
        private const float GROUND_BOX_INDENT = 0.95f;

        private bool _ricochetHappened;
        private LayerMask _groundLayer, _targetLayer;
        private Vector3 _moveDirection, _ricochetStartPoint;
        private float _bulletSpeed, _maxFlyHeight, _hitForce, _angle;

        private Collider _collider;
        private Rigidbody _rigidbody;
        private Interacting _groundInteract;
        private bool GroundCheck => _groundInteract.InteractionBoxcast(Vector3.down);

        private void Awake(){
            _rigidbody = GetComponent<Rigidbody>();
            _collider = GetComponent<Collider>();
        }

        public void Initialize(float bulletSpeed, float maxFlyHeight, float hitForce, float angle, LayerMask groundLayer, LayerMask targetLayer){
            _bulletSpeed = bulletSpeed;
            _maxFlyHeight = maxFlyHeight;
            _hitForce = hitForce;
            _targetLayer = targetLayer;
            _groundLayer = groundLayer;
            _angle = angle;

            _groundInteract = new Interacting(_collider, Axis.Vertical, MIN_FLY_HEIGHT, _groundLayer, GROUND_BOX_INDENT);
        }

        public void Activate(Vector3 position, Vector3 moveDirection){
            gameObject.SetActive(true);
            transform.position = position;
            
            var angle = _angle;
            angle = moveDirection.z > 0 ? angle : -angle;

            _moveDirection = Quaternion.Euler(angle, 0, 0) * moveDirection;
            _ricochetHappened = false;
        }

        private void FixedUpdate(){
            if (!_ricochetHappened && GroundCheck)
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
            _groundInteract.OnDrawGizmos(Color.blue);
        }
    }
}

