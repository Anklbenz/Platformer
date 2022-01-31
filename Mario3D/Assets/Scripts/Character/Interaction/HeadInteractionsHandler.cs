using Character.States;
using Character.States.Data;
using Enums;
using Interfaces;
using UnityEngine;

namespace Character.Interaction
{
    public class HeadInteractionsHandler
    {
        private readonly IMoveInfo _moveInfo;
        private readonly IStateData _characterData;
        private readonly Interacting _interacting;
        private readonly Collider _interactCollider;
        private readonly Vector3 _direction;
        
        private bool _collisionDetected;
        private Vector3 ColliderCenter => _interactCollider.bounds.center; 

        public HeadInteractionsHandler(IStateData data, Collider collider, IMoveInfo moveInfo, Vector3 direction, float inspectLength,
            LayerMask inspectLayer, float boxIndent = 1f){
            
            _characterData = data;
            _moveInfo = moveInfo;
            _interactCollider = collider;
            _direction = direction;
            _interacting = new Interacting(collider, Axis.Vertical, inspectLength, inspectLayer, boxIndent);
        }

        public void CollisionCheck() {
            var interactions = _interacting.InteractionOverlap(_direction);
    
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
            nearestCollider.GetComponentInParent<IBrickHit>()?.BrickHit(_characterData.Data.CanCrush);
        }
        public void OnDrawGizmos(Color color) {
            _interacting?.OnDrawGizmos(color);
        }
    }
}