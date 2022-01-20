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
        private readonly Interactor _wallContactInteractor;

        private bool _canSlide, _jumping, _jumpInput;
        private float _jumpForceDuration, _maxSpeed, _walkStep;

        private Vector3 _moveDirection;
        private bool isWallContact => _wallContactInteractor.InteractionBoxcast(_moveDirection);
        private Vector3 rbVelocity => _rigidbody.velocity;

        public Move(IMoveInfo moveInfo, MoveData data, Rigidbody rBody, Collider collider, float wallInspectLength, LayerMask wallLayer){
            _moveInfo = moveInfo;
            _data = data;
            _maxSpeed = data.MaxWalkSpeed;
            _walkStep = data.WalkForceStep;
            _rigidbody = rBody;
            _wallContactInteractor = new Interactor(collider, Axis.horisontal, wallInspectLength, wallLayer, WALL_BOX_INDENT);
        }

        public void RecalculateMoving(){

            if (!isWallContact) Walk();

            if (_jumping) Jump();

            if (_moveInfo.IsGrounded && _canSlide)
                SideImpulse();

            if (!_moveInfo.IsGrounded)
                _canSlide = true;
        }

        // [SerializeField] private Transform _characterTransform;
        private void Walk(){
            Debug.Log($"maxSpeed {_maxSpeed}  walkStep { _walkStep}");
            float currentBodySpeed = Mathf.Abs(rbVelocity.z);
            if (currentBodySpeed < _maxSpeed)
                _rigidbody.AddForce(_moveDirection * _walkStep, ForceMode.VelocityChange);
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
            Vector3 t = new Vector3(0, rbVelocity.y * -1, 0);
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
            _wallContactInteractor.OnDrawGizmos(color);
        }
    }
}