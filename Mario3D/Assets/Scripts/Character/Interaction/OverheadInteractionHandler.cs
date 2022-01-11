using Character.States;
using MyEnums;
using UnityEngine;

namespace Character.Interaction
{
    public class OverheadInteractionHandler
    {
        private readonly StateSystem _characterState;
        private readonly Interactor _interactor;
        private readonly BoxCollider _interactCollider;
        private readonly IMoveData _moveData;
        private bool _interactActive;
        private readonly Vector3 _direction;
        private Vector3 _colliderCenter { get => _interactCollider.bounds.center; }

        public OverheadInteractionHandler(StateSystem state, IMoveData moveData, Vector3 direction, float inspectLength, LayerMask inspectLayer, float boxIndent = 1f) {
            _characterState = state;
            _moveData = moveData;
            _interactCollider = moveData.InteractCollider;
            _direction = direction;
            _interactor = new Interactor(moveData.InteractCollider, Axis.vertical, inspectLength, inspectLayer, boxIndent);
        }

        public void CollisionCheck() {
            var interactions = _interactor.InteractionOverlap(_direction);
    
            if (_moveData.MovingUp && !_interactActive && interactions.Length > 0) {
                _interactActive = true;
                HitNearestCollider(interactions);

            } else if (interactions.Length == 0) {
                _interactActive = false;
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

