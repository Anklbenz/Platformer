using Enums;
using Interfaces;
using UnityEngine;

namespace Character.Interaction
{
    public class LegsInteractionsHandler
    {
        private const float SIDE_ENTER_DEPTH = 0.3f;

        private readonly InRowCounter InRowCounter = new InRowCounter();
        private readonly Interactor _interactor;
        private readonly CapsuleCollider _interactCollider;
        private readonly Vector3 _direction;
        private readonly IMove _move;
        private readonly IBounce _bounceInstance;
        
        private bool _collisionDetected;
        private Vector3 _colliderCenter => _interactCollider.bounds.center;
        
        public LegsInteractionsHandler(IMove move, IBounce bounceInstance, Vector3 direction, float inspectLength, LayerMask inspectLayer,
            float boxIndent = 1f){
            _move = move;
            _bounceInstance = bounceInstance;
            _interactCollider = move.MainCollider;
            _direction = direction;
            _interactor = new Interactor(move.MainCollider, Axis.vertical, inspectLength, inspectLayer, boxIndent);
        }

        public void CollisionCheck(){
            var interactions = _interactor.InteractionOverlap(_direction);

            if (_move.MovingDown && !_collisionDetected && interactions.Length > 0){
                _collisionDetected = true;
                Interaction(interactions);
            }
            else if (interactions.Length == 0){
                _collisionDetected = false;
            }

            if (_move.IsGrounded)
                InRowCounter.Reset();
        }

        private void Interaction(Collider[] bottomColliders){
            bool bounce = false;

            foreach (var collider in bottomColliders){
                if (CollisionIsHorisontal(collider.ClosestPoint(_colliderCenter), _colliderCenter)) continue;

                IJumpOn jumpOnInstance = collider.transform.GetComponentInParent<IJumpOn>();

                if (jumpOnInstance == null) continue;
                if (jumpOnInstance.DoBounce) bounce = true;
                jumpOnInstance.JumpOn(_colliderCenter, InRowCounter.Count);
                InRowCounter.Inreace();
            }
            
            if (bounce) this.DoBounce();
        }

        private void DoBounce(){
            _bounceInstance.Bounce();
        }
        
        private bool CollisionIsHorisontal(Vector3 closestPoint, Vector3 origin){
            var positionDifference = origin - closestPoint;
            var overlapDirection = positionDifference.normalized;

            return Mathf.Abs(overlapDirection.z) > SIDE_ENTER_DEPTH;
        }
        public void OnDrawGizmos(Color color){
            _interactor.OnDrawGizmos(color);
        }
    }
}