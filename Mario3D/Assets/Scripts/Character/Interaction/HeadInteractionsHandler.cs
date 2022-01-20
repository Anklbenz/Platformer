using Character.States;
using Enums;
using Interfaces;
using UnityEngine;

namespace Character.Interaction
{
    public class HeadInteractionsHandler
    {
        private readonly StateSystem _characterState;
        private readonly Interactor _interactor;
        private readonly CapsuleCollider _interactCollider;
        private readonly IMove _move;
        private readonly Vector3 _direction;
        
        private bool _collisionDetected;
        private Vector3 _colliderCenter => _interactCollider.bounds.center; 

        public HeadInteractionsHandler(StateSystem state, IMove move, Vector3 direction, float inspectLength,
            LayerMask inspectLayer, float boxIndent = 1f){
            
            _characterState = state;
            _move = move;
            _interactCollider = move.MainCollider;
            _direction = direction;
            _interactor = new Interactor(move.MainCollider, Axis.vertical, inspectLength, inspectLayer, boxIndent);
        }

        public void CollisionCheck() {
            var interactions = _interactor.InteractionOverlap(_direction);
    
            if (_move.MovingUp && !_collisionDetected && interactions.Length > 0) {
                _collisionDetected = true;
                HitNearestCollider(interactions);

            } else if (interactions.Length == 0) {
                _collisionDetected = false;
            }
        }
        private Collider ChooseNearestCollider(Collider[] overheadColliders, Vector3 playerCenter) {
            float minValue = -1;
            Collider nearestcollider = null;

            foreach (var collider in overheadColliders) {
                var dif = Mathf.Abs(playerCenter.z - collider.ClosestPoint(playerCenter).z);
                if (minValue < 0 || minValue > dif) {
                    minValue = dif;
                    nearestcollider = collider;
                }
            }
            return nearestcollider;
        }
        private void HitNearestCollider(Collider[] overheadColliders) {
            Collider nearestCollider = overheadColliders.Length == 1 ? overheadColliders[0] : ChooseNearestCollider(overheadColliders, _colliderCenter);
            nearestCollider.GetComponentInParent<IBrickHit>()?.BrickHit(_characterState);
        }
        public void OnDrawGizmos(Color color) {
            _interactor.OnDrawGizmos(color);
        }
    }
}