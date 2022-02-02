using Enums;
using Interfaces;
using UnityEngine;
//  Application.targetFrameRate = 240;
namespace Character
{
    public sealed class Move : IBounce
    {
        private readonly IMoveData _moveData;
        private readonly MoveData _data;
        private readonly Rigidbody _rigidbody;

        private bool _canSlide, _jumping, _jumpInput;
        private float _jumpForceDuration, _maxSpeed, _walkStep;
        private Vector3 RbVelocity => _rigidbody.velocity;
        public Vector3 MoveDirection{ get; private set; }

        public Move(IMoveData moveData, MoveData data, Rigidbody rBody, Collider collider, float wallInspectLength, LayerMask wallLayer){
            _moveData = moveData;
            _data = data;
            _maxSpeed = data.MaxWalkSpeed;
            _walkStep = data.WalkForceStep;
            _rigidbody = rBody;
        }

        public void RecalculateMoving(){
            if (!_moveData.IsWallContact && !_moveData.IsSittingState) Walk();

            if (_jumping) Jump();

            if (_moveData.IsGrounded && _canSlide)
                SideImpulse();

            if (!_moveData.IsGrounded)
                _canSlide = true;
        }

        private void Walk(){
            var currentBodySpeed = Mathf.Abs(RbVelocity.z);
            
            if (currentBodySpeed < _maxSpeed)
                _rigidbody.AddForce(MoveDirection * _walkStep, ForceMode.VelocityChange);
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

        public void OnMove(Vector3 movement) => MoveDirection = movement;

        public void OnJump(bool jumpInput){
            if (jumpInput){
                _jumpForceDuration = 0;
                _jumping = false;
            }

            if (jumpInput && _moveData.IsGrounded)
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
        
    }
}