using Enums;
using Interfaces;
using UnityEngine;
//  Application.targetFrameRate = 240;
namespace Character
{
    public sealed class Move : IBounce
    {
        private const float WALL_BOX_INDENT = 0.5f;

        private readonly IMoveInfo _moveInfo;
        private readonly MoveData _data;
        private readonly Rigidbody _rigidbody;
        private readonly Interacting _wallContactInteracting;

        private bool _canSlide, _jumping, _jumpInput;
        private float _jumpForceDuration, _maxSpeed, _walkStep;

        private Vector3 _moveDirection;
        private Vector3 RbVelocity => _rigidbody.velocity;
        private bool IsWallContact => _wallContactInteracting.InteractionBoxcast(_moveDirection);

        public Move(IMoveInfo moveInfo, MoveData data, Rigidbody rBody, Collider collider, float wallInspectLength, LayerMask wallLayer){
            _moveInfo = moveInfo;
            _data = data;
            _maxSpeed = data.MaxWalkSpeed;
            _walkStep = data.WalkForceStep;
            _rigidbody = rBody;
            _wallContactInteracting = new Interacting(collider, Axis.Horizontal, wallInspectLength, wallLayer, WALL_BOX_INDENT);
        }

        public void RecalculateMoving(){

            if (!IsWallContact) Walk();

            if (_jumping) Jump();

            if (_moveInfo.IsGrounded && _canSlide)
                SideImpulse();

            if (!_moveInfo.IsGrounded)
                _canSlide = true;
        }

        private void Walk(){
            var currentBodySpeed = Mathf.Abs(RbVelocity.z);
            
            if (currentBodySpeed < _maxSpeed)
                _rigidbody.AddForce(_moveDirection * _walkStep, ForceMode.VelocityChange);
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
            var t = new Vector3(0, RbVelocity.y * -1, 0);
            _rigidbody.velocity += t;
            _canSlide = false;
        }

        private void StopVerticalMove(){
            var velocity = _rigidbody.velocity;
            velocity = new Vector3(velocity.x, 0, velocity.z);
            _rigidbody.velocity = velocity;
        }

        public void OnMove(Vector3 movement) => _moveDirection = movement;

        public void OnJump(bool jumpInput){
            if (jumpInput){
                _jumpForceDuration = 0;
                _jumping = false;
            }

            if (jumpInput && _moveInfo.IsGrounded)
                _jumping = true;

            _jumpInput = jumpInput;
        }

        public void OnExtra(bool extraMove){
            if (extraMove){
                _maxSpeed = _data.MaxExtraWalkSpeed;
                _walkStep = _data.ExtraWalkForceStep;
            }
            else{
                _maxSpeed = _data.MaxWalkSpeed;
                _walkStep = _data.WalkForceStep;
            }
        }

        public void OnDrawGizmos(Color color){
            _wallContactInteracting.OnDrawGizmos(color);
        }
    }
}