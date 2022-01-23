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
        private readonly Collider _interactCollider;
        private readonly IMoveInfo _moveInfo;
        private readonly Vector3 _direction;
        
        private bool _collisionDetected;
        private Vector3 ColliderCenter => _interactCollider.bounds.center; 

        public HeadInteractionsHandler(StateSystem state, Collider collider, IMoveInfo moveInfo, Vector3 direction, float inspectLength,
            LayerMask inspectLayer, float boxIndent = 1f){
            
            _characterState = state;
            _moveInfo = moveInfo;
            _interactCollider = collider;
            _direction = direction;
            _interactor = new Interactor(collider, Axis.Vertical, inspectLength, inspectLayer, boxIndent);
        }

        public void CollisionCheck() {
            var interactions = _interactor.InteractionOverlap(_direction);
    
            if (_moveInfo.MovingUp && !_collisionDetected && interactions.Length > 0) {
                _collisionDetected = true;
                HitNearestCollider(interactions);

            } else if (interactions.Length == 0) {
                _collisionDetected = false;
            }
        }
        private Collider ChooseNearestCollider(Collider[] overheadColliders, Vector3 playerCenter) {
            float minValue = -1;
            Collider nearestCollider = null;

            foreach (var collider in overheadColliders) {
                var dif = Mathf.Abs(playerCenter.z - collider.ClosestPoint(playerCenter).z);
                if (minValue < 0 || minValue > dif) {
                    minValue = dif;
                    nearestCollider = collider;
                }
            }
            return nearestCollider;
        }
        private void HitNearestCollider(Collider[] overheadColliders) {
            var nearestCollider = overheadColliders.Length == 1 ? overheadColliders[0] : ChooseNearestCollider(overheadColliders, ColliderCenter);
            nearestCollider.GetComponentInParent<IBrickHit>()?.BrickHit(_characterState);
        }
        public void OnDrawGizmos(Color color) {
            _interactor?.OnDrawGizmos(color);
        }
    }
}