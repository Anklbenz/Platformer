using Enums;
using Interfaces;
using UnityEngine;
//  Application.targetFrameRate = 240;
namespace Character
{
    public sealed class Move : IBounce
    {
        private const float WALL_BOX_INDENT = 0.5f;

        private readonly IMove _move;
        private readonly MoveData _data;
        private readonly Rigidbody _rigidbody;
        private readonly Collider _mainCollider;
        private readonly Interactor _wallContactInteractor;

        private bool _canSlide = false;
        private bool _jumping;
        private bool _jumpInput;
        private float _jumpForceDuration;
        private Vector3 _moveDirection;
        private bool isWallContact => _wallContactInteractor.InteractionBoxcast(_moveDirection);

        public Move(IMove move, MoveData data, Rigidbody rBody, Collider collider, float wallInspectLength,
            LayerMask wallLayer){
            _move = move;
            _data = data;
            _rigidbody = rBody;
            _mainCollider = collider;
            _wallContactInteractor =
                new Interactor(collider, Axis.horisontal, wallInspectLength, wallLayer, WALL_BOX_INDENT);
        }

        public void RecalculateMoving(){

            if (!isWallContact) Walk();

            if (_jumping) Jump();

            if (_move.IsGrounded && _canSlide)
                SideImpulse();

            if (!_move.IsGrounded)
                _canSlide = true;
        }

        // [SerializeField] private Transform _characterTransform;
        private void Walk(){
            float currentBodySpeed = Mathf.Abs(_rigidbody.velocity.z);
            if (currentBodySpeed < _data.MaxWalkSpeed)
                _rigidbody.AddForce(_moveDirection * _data.WalkForceStep, ForceMode.VelocityChange);
            //  Flip(_characterTransform, _moveDirection);
        }

        private void Jump(){
            if (!_jumpInput) return;

            _jumpForceDuration += Time.fixedDeltaTime;

            if (_jumpForceDuration < _data.MaxJumpForceDuration)
                _rigidbody.AddForce(Vector3.up * _data.AddForceStep, ForceMode.VelocityChange);
        }

        public void Bounce(){
            StopVerticalMove();
            _rigidbody.AddForce(Vector3.up * _data.BouncePower, ForceMode.Impulse);
        }

        private void SideImpulse(){
            Vector3 t = new Vector3(0, _rigidbody.velocity.y * -1, 0);
            _rigidbody.velocity += t;
            _canSlide = false;
        }

        private void StopVerticalMove(){
            var velocity = _rigidbody.velocity;
            velocity = new Vector3(velocity.x, 0, velocity.z);
            _rigidbody.velocity = velocity;
        }

        private void Flip(Transform go, Vector3 direction){
            if (direction == Vector3.back)
                go.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
            if (direction == Vector3.forward)
                go.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        }

        public void OnMove(Vector3 movement) => _moveDirection = movement;

        public void OnJump(bool jumpInput){
            if (jumpInput){
                _jumpForceDuration = 0;
                _jumping = false;
            }

            if (jumpInput && _move.IsGrounded)
                _jumping = true;

            _jumpInput = jumpInput;
        }

        public void OnDrawGizmos(Color color){
            if (_mainCollider == null) return;
            _wallContactInteractor.OnDrawGizmos(color);
        }
    }
}
