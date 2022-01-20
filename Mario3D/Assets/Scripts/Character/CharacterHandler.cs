using System;
using Character.BallSpawner;
using Character.Interaction;
using Character.States;
using Character.States.Data;
using Enums;
using Interfaces;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Character
{
    [RequireComponent(typeof(CapsuleCollider))]
    [RequireComponent(typeof(Rigidbody))]
    public sealed class CharacterHandler : MonoBehaviour, ICharacterComponets, IMoveInfo, GameInput.IPlayerActions
    {
        private const float ISGROUND_BOX_INDENT = 0.95f; 
        
        [Header("FireballSpawner")]
        [SerializeField] private BallSpawnerData _spawnerData;

        [SerializeField] private Transform _firePoint, _fireballParent;
        [SerializeField] private LayerMask _groundLayers;
        private BallSpawner.BallSpawner _ballSpawner;

        [Header("StateHandler")]
        [SerializeField] private StateData _juniorState;

        [SerializeField] private StateData _middleState;
        [SerializeField] private StateData _seniorState;
        [SerializeField] private Transform _skinsParent;
        [SerializeField] private int _hurtTime;
        [SerializeField] private int _unsopableTime;
        public Transform SkinsParent => _skinsParent;
        public StateSystem StateSystem;

        [Header("InteractionsHandler")]
        [SerializeField] private float _isGroundedLength;

        [SerializeField] private float _topLength;
        [SerializeField] private float _bottomLength;
        [SerializeField] private LayerMask _topLayer, _bottomLayer, _isGroundedLayer;
        private InteractionsHandler _interactionsHandler;
        private Interactor _isGrounded;
        public bool IsGrounded => _isGrounded.InteractionBoxcast(Vector3.down);

        [Header("MoveHandler")]
        [SerializeField] private MoveData _moveData;

        private Move _move;
        private GameInput _gameInput;
        public bool MovingUp => MainRigidbody.velocity.y > 0;
        public bool MovingDown => MainRigidbody.velocity.y < 0;
        public CapsuleCollider MainCollider{ get; private set; }
        public Rigidbody MainRigidbody{ get; private set; }


        private void Awake(){
            MainCollider = GetComponent<CapsuleCollider>();
            MainRigidbody = GetComponent<Rigidbody>();
            StateSystem = new StateSystem(this, _juniorState, _middleState, _seniorState, _hurtTime, _unsopableTime);

            _gameInput = new GameInput();
            _gameInput.Enable();
            _gameInput.Player.SetCallbacks(this);

            _ballSpawner = new BallSpawner.BallSpawner(_firePoint, _groundLayers, _fireballParent, _spawnerData);
            _isGrounded = new Interactor(MainCollider, Axis.vertical, _isGroundedLength, _isGroundedLayer, ISGROUND_BOX_INDENT);
            _move = new Move(this, _moveData, MainRigidbody, MainCollider, _isGroundedLength, _isGroundedLayer);
            _interactionsHandler = new InteractionsHandler(StateSystem, MainCollider, this, _move, _topLength,
                _topLayer, _bottomLength, _bottomLayer);
        }

        private void FixedUpdate(){
            if (StateSystem.IsActiveState)
                _interactionsHandler.LegsInteractionsCheck();
            _interactionsHandler.HeadInteractionCheck();
            _move.RecalculateMoving();
        }

        public void OnJump(InputAction.CallbackContext context){
            if (context.phase == InputActionPhase.Performed)
                _move.OnJump(true);

            if (context.phase == InputActionPhase.Canceled)
                _move.OnJump(false);
        }

        public void OnMove(InputAction.CallbackContext context){
            var movement = Vector3.forward * context.ReadValue<float>();
            _move.OnMove(movement);
        }

        public void OnExtra(InputAction.CallbackContext context){
            if (context.phase == InputActionPhase.Performed){
                if (StateSystem.CanShoot) _ballSpawner.Spawn();
                _move.OnExtra(true);
            }
            
            if(context.phase == InputActionPhase.Canceled)
                _move.OnExtra(false);
        }

        private void OnDrawGizmos(){
            _move?.OnDrawGizmos(Color.cyan);
            _interactionsHandler?.OnDrawGizmos(Color.grey);
        }
    }
}