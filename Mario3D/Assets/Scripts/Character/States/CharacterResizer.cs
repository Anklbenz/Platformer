using UnityEngine;

namespace Character.States
{
    public class CharacterResizer
    {
        private const float COLLIDERS_INDENT = 0.01f;

        private readonly Transform _transform;
        private readonly BoxCollider _boxCollider;
        private readonly SphereCollider _sphereCollider;

        public CharacterResizer(BoxCollider boxCollider, SphereCollider sphereCollider, Transform transform){
            _transform = transform;
            _boxCollider = boxCollider;
            _sphereCollider = sphereCollider;
        }

        public void ColliderResize(Vector3 newSize){
            _transform.position = CalculateTransformPositionForNewSize(newSize);
            _boxCollider.size = newSize;

            _sphereCollider.center = SphereCenter();
        }

        private Vector3 SphereCenter(){
            var boxCenter = _boxCollider.center;
            var spherePositionY = boxCenter.y - (_boxCollider.size.y / 2) + _sphereCollider.radius - COLLIDERS_INDENT;

            return new Vector3(boxCenter.x, spherePositionY, boxCenter.y);
        }

        private Vector3 CalculateTransformPositionForNewSize(Vector3 newSize){
            var height = _boxCollider.size.y;
            var verticalDifference = Vector3.up * (newSize.y - height) / 2;
            return _transform.position + verticalDifference;
        }
    }
}
 