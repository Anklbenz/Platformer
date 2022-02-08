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
        private float _jumpStartTime, _maxSpeed, _walkStep;
        private Vector3 RbVelocity => _rigidbody.velocity;
        public Vector3 MoveDirection{ get; private set; }
        private bool CanWalk => MoveDirection != Vector3.zero && !_moveData.IsWallContact && !_moveData.IsSittingState;

        public Move(IMoveData moveData, MoveData data, Rigidbody rBody){
            _moveData = moveData;
            _data = data;
            _maxSpeed = data.MaxWalkSpeed;
            _walkStep = data.WalkForceStep;
            _rigidbody = rBody;
        }

        public void RecalculateMoving(){
            if (CanWalk) Walk();

            if (_jumping) Jump();

            if (_moveData.IsGrounded && _canSlide)
                SideImpulse();

            if (!_moveData.IsGrounded)
                _canSlide = true;
            
            if (!_moveData.IsGrounded)
                AdditionalGravityForPlayer();
            
         //  Debug.Log($"z {RbVelocity.z} ");
        }

        private void AdditionalGravityForPlayer(){
            _rigidbody.AddForce(Vector3.down * _data.AdditionalGravity, ForceMode.VelocityChange);
        }

        private void Walk(){
            var currentBodySpeed = Mathf.Abs(RbVelocity.z);

            if (currentBodySpeed <= _maxSpeed)
                _rigidbody.AddForce(MoveDirection * _walkStep, ForceMode.VelocityChange);
           
        }

        private void Jump(){
            if (!_jumpInput) return;

            var force = CalculateJumpForce();
            _rigidbody.AddForce(force, ForceMode.VelocityChange);
        }

        private Vector3 CalculateJumpForce(){
            if (_jumpStartTime == 0){
                _jumpStartTime = Time.realtimeSinceStartup;
                return Vector3.up * _data.StartImpulse;
            }
            var timeDifference = Time.realtimeSinceStartup - _jumpStartTime;
            
            if (timeDifference < _data.MaxJumpForceDuration)
                return  Vector3.up * _data.AddForceStep;
            
            return Vector3.zero;
        }

        public void Bounce(){
            StopVerticalMove();
            _rigidbody.AddForce(Vector3.up * _data.BouncePower, ForceMode.Impulse);
        }

        private void SideImpulse(){
            StopVerticalMove();
            _canSlide = false;
        }

        private void StopVerticalMove(){
            var project = Vector3.ProjectOnPlane(_rigidbody.velocity, Vector3.up);
            _rigidbody.velocity = project;
        }

        public void OnMoveInput(Vector3 movement) => MoveDirection = movement;

        public void OnJumpInput(bool jumpInput){
            if (jumpInput){
                _jumpStartTime = 0;
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