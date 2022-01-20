using Enums;
using UnityEngine;

namespace Enemys
{
    [RequireComponent(typeof(Motor))]
    [RequireComponent(typeof(BoxCollider))]
    [RequireComponent(typeof(Rigidbody))]

    public abstract class ActiveInteractiveObject : InteractiveObject
    {
        [SerializeField] protected LayerMask _patrolLayer;
        [SerializeField] private bool _groundCheckActive = false;
        private const float PATROL_BOX_INDENT = 0.5f;//%
        private const float PATROL_CHECK_DISTANCE = 0.05f;
        private Interactor _frontInteractor;
        private Interactor _groundInteractor;
        protected BoxCollider _collider;
        protected Motor _motor;

        protected virtual void Awake() {
            _collider = GetComponent<BoxCollider>();
            _motor = GetComponent<Motor>();
            _frontInteractor = new Interactor(_collider, Axis.horisontal, PATROL_CHECK_DISTANCE, _patrolLayer, PATROL_BOX_INDENT);
            _groundInteractor = new Interactor(_collider, Axis.vertical, PATROL_CHECK_DISTANCE, _patrolLayer, PATROL_BOX_INDENT);
        }

        protected virtual void FixedUpdate() {
            PatrolCollisionCheck();

            if (_groundCheckActive)
                GroundCollisionCheck();
        }

        private void PatrolCollisionCheck() {
            if (_frontInteractor.InteractionBoxcast(_motor.GetDirection(), out RaycastHit obj))
                OnFrontCollision(obj);
        }

        private void GroundCollisionCheck() {
            if (_groundInteractor.InteractionBoxcast(Vector3.down, out RaycastHit obj))
                OnIsGrounded();        
        }

        protected virtual void OnFrontCollision(RaycastHit obj) {
            _motor.DirectionChange();
        }

        protected virtual void OnIsGrounded() {}

        public abstract void DownHit();

        void OnDrawGizmos() {
            if (_frontInteractor == null) return;

            _frontInteractor.OnDrawGizmos(Color.white);
            _groundInteractor.OnDrawGizmos(Color.white);
        }
    }
}