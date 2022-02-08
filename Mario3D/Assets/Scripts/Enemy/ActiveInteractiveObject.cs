using Enums;
using UnityEngine;

namespace Enemy
{
    [RequireComponent(typeof(Motor))]
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(BoxCollider))]

    public abstract class ActiveInteractiveObject : InteractiveObject
    {
        private const float PATROL_BOX_INDENT = 0.5f; //%
        private const float PATROL_CHECK_DISTANCE = 0.05f;

        [Header("ActiveInterObject")]
        [SerializeField] protected LayerMask patrolLayer;

        protected bool IsActive;
        protected Motor Motor;
        protected Rigidbody Rigidbody;
        protected BoxCollider ObjectCollider;
        private Interacting _frontInteract, _groundInteract;

        protected virtual void Awake(){
            IsActive = true;
            
            Motor = GetComponent<Motor>();
            Rigidbody = GetComponent<Rigidbody>();
            ObjectCollider = GetComponent<BoxCollider>();

            _groundInteract = new Interacting(ObjectCollider, Axis.Vertical, PATROL_CHECK_DISTANCE, patrolLayer, PATROL_BOX_INDENT, true);
            _frontInteract = new Interacting(ObjectCollider, Axis.Horizontal, PATROL_CHECK_DISTANCE, patrolLayer, PATROL_BOX_INDENT,true);
        }

        protected virtual void FixedUpdate(){
            if(!IsActive) return;
            
            PatrolCollisionCheck();

            if (Motor.JumpingMode)
                GroundCollisionCheck();
        }

        private void PatrolCollisionCheck(){
            if (_frontInteract.InteractionBoxcast(Motor.GetDirection(), out var obj))
                OnFrontCollision(obj);
        }

        private void GroundCollisionCheck(){
            if (_groundInteract.InteractionBoxcast(Vector3.down))
                OnIsGrounded();
        }

        protected virtual void OnFrontCollision(RaycastHit obj){
            Motor.DirectionChange();
        }

        protected virtual void OnIsGrounded(){
        }

        public abstract void DownHit(float dropForce);

        private void OnDrawGizmos(){
            if (_frontInteract == null) return;

            _frontInteract.OnDrawGizmos(Color.red);
            _groundInteract.OnDrawGizmos(Color.yellow);
        }
    }
}