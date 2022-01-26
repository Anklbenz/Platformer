﻿using Enums;
using UnityEngine;

namespace Enemy
{
    [RequireComponent(typeof(Motor))]
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(BoxCollider))]

    public abstract class ActiveInteractiveObject : InteractiveObject
    {
        private const float PATROL_BOX_INDENT = 0.5f;//%
        private const float PATROL_CHECK_DISTANCE = 0.05f;
        
        [Header("ActiveInterObject")]
        [SerializeField] protected LayerMask patrolLayer;

        protected Motor Motor;
        protected Rigidbody Rigidbody;
        protected BoxCollider Collider;
       
        private Interactor _frontInteract, _groundInteract;

        protected virtual void Awake() {
            Motor = GetComponent<Motor>();
            Rigidbody = GetComponent<Rigidbody>();        
            Collider = GetComponent<BoxCollider>();
       
            _groundInteract = new Interactor(Collider, Axis.Vertical, PATROL_CHECK_DISTANCE, patrolLayer, PATROL_BOX_INDENT);
            _frontInteract = new Interactor(Collider, Axis.Horizontal, PATROL_CHECK_DISTANCE, patrolLayer, PATROL_BOX_INDENT);
        }

        protected virtual void FixedUpdate() {
            PatrolCollisionCheck();

            if (Motor.JumpingMode)
                GroundCollisionCheck();
        }

        private void PatrolCollisionCheck() {
            if (_frontInteract.InteractionBoxcast(Motor.GetDirection(), out var obj))
                OnFrontCollision(obj);
        }

        private void GroundCollisionCheck() {
            if (_groundInteract.InteractionBoxcast(Vector3.down))
                OnIsGrounded();        
        }

        protected virtual void OnFrontCollision(RaycastHit obj) {
            Motor.DirectionChange();
        }

        protected virtual void OnIsGrounded() {}

        public abstract void DownHit();

        private void OnDrawGizmos() {
            if (_frontInteract == null) return;

            _frontInteract.OnDrawGizmos(Color.white);
            _groundInteract.OnDrawGizmos(Color.white);
        }
    }
}