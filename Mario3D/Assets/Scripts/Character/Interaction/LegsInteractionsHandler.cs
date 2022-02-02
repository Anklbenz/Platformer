using Enums;
using Interfaces;
using UnityEngine;

namespace Character.Interaction
{
    public class LegsInteractionsHandler
    {
        private const float SIDE_ENTER_DEPTH = 0.3f;

        private readonly InRowCounter InRowCounter = new InRowCounter();
        private readonly Interacting _interacting;
        private readonly Collider _interactCollider;
        private readonly Vector3 _direction;
        private readonly IMoveData _moveData;
        private readonly IBounce _bounceInstance;
        
        private bool _collisionDetected;
        private Vector3 _colliderCenter => _interactCollider.bounds.center;
        
        public LegsInteractionsHandler(Collider collider, IMoveData moveData, IBounce bounceInstance, Vector3 direction, float inspectLength, LayerMask inspectLayer,
            float boxIndent = 1f){
            _moveData = moveData;
            _bounceInstance = bounceInstance;
            _interactCollider = collider;
            _direction = direction;
            _interacting = new Interacting(collider, Axis.Vertical, inspectLength, inspectLayer, boxIndent);
        }

        public void CollisionCheck(){
            var interactions = _interacting.InteractionOverlap(_direction);

            if (_moveData.MovingDown && !_collisionDetected && interactions.Length > 0){
                _collisionDetected = true;
                Interaction(interactions);
            }
            else if (interactions.Length == 0){
                _collisionDetected = false;
            }

            if (_moveData.IsGrounded)
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
            _interacting.OnDrawGizmos(color);
        }
    }
}