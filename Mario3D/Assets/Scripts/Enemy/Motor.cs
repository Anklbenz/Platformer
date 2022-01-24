using Enums;
using UnityEngine;

namespace Enemy
{
    public class Motor : MonoBehaviour
    {
        [Range(0, 15)]
        [SerializeField] private float speed, jumpingPower;
        [SerializeField] private bool jumpingMode;
        [SerializeField] private Direction moveDirection;
        public bool JumpingMode => jumpingMode;

        private Rigidbody _rigidbody;
        private bool _isActive = true;
        private Vector3 _direction = Vector3.back;

        private void Awake(){
            _direction = moveDirection == Direction.Left ? Vector3.back : Vector3.forward;
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void FixedUpdate(){
            if (_isActive)
                Patrolling();
        }

        public void SetSpeed(float spd){
            speed = spd;
        }

        public float GetSpeed(){
            return speed;
        }

        public void SetActive(bool state){
            _isActive = state;
        }

        public void DirectionChange(){
            _direction = _direction == Vector3.back ? Vector3.forward : Vector3.back;
        }

        public void SetDirection(Direction dir){
            _direction = dir == Direction.Left ? Vector3.back : Vector3.forward;
        }

        public Vector3 GetDirection(){
            return _direction;
        }

        private void Patrolling(){
            _rigidbody.MovePosition(_rigidbody.position + _direction * speed * Time.fixedDeltaTime);
        }

        public void Jumping(){
            StopVerticalMove();
            _rigidbody.AddForce(Vector3.up * jumpingPower, ForceMode.Impulse);
        }

        private void StopVerticalMove(){
            var velocity = _rigidbody.velocity;
            velocity = new Vector3(velocity.x, 0, velocity.z);
            _rigidbody.velocity = velocity;
        }
    }
}