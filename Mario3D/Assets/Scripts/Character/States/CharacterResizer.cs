using Interfaces;
using UnityEngine;

namespace Character.States
{
    public class CharacterResizer
    {
        private readonly Transform _transform;
        private readonly CapsuleCollider _collider;

        public CharacterResizer(CapsuleCollider collider,Transform transform){
            _collider = collider;
            _transform = transform;
        }

        public void ColliderResize(Vector3 newSize){
            _transform.position = CalculateTransformPositionForNewSize(newSize);

            _collider.height = newSize.y;
            _collider.radius = newSize.x / 2;
        }

        private Vector3 CalculateTransformPositionForNewSize(Vector3 newSize){
            var height = _collider.height;
            var verticalDifference = Vector3.up * (newSize.y - height) / 2;
            return _transform.position + verticalDifference;
        }
    }
}
 