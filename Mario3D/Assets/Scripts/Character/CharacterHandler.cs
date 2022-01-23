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
    public sealed class CharacterHandler : MonoBehaviour, ICharacterComponents, IMoveInfo, GameInput.IPlayerActions
    {
        private const float ISGROUND_BOX_INDENT = 0.95f;

        [Header("FireballSpawner")]
        [SerializeField] private BallSpawnerData spawnerData;

        [SerializeField] private Transform firePoint, fireballParent;
        [SerializeField] private LayerMask groundLayers;
        private BallSpawner.BallSpawner _ballSpawner;

        [Header("StateHandler")]
        [SerializeField] private StateData juniorState;

        [SerializeField] private StateData middleState;
        [SerializeField] private StateData seniorState;
        [SerializeField] private Transform skinsParent;
        [SerializeField] private int flickerLength;
        [SerializeField] private int unstopLength;
        public Transform SkinsParent => skinsParent;
        public StateSystem StateSystem;

        [Header("InteractionsHandler")]
        [SerializeField] private float isGroundedLength;

        [SerializeField] private float topLength;
        [SerializeField] private float bottomLength;
        [SerializeField] private LayerMask topLayer, bottomLayer, isGroundedLayer;
        private InteractionsHandler _interactionsHandler;
        private Interactor _isGrounded;
        public bool IsGrounded => _isGrounded.InteractionBoxcast(Vector3.down);

        [Header("MoveHandler")]
        [SerializeField] private MoveData moveData;

        private Move _move;
        private GameInput _gameInput;
        public bool MovingUp => MainRigidbody.velocity.y > 0;
        public bool MovingDown => MainRigidbody.velocity.y < 0;
        public CapsuleCollider MainCollider{ get; private set; }
        public Rigidbody MainRigidbody{ get; private set; }
        public Transform MainTransform{ get; private set; }


        private void Awake(){
            MainCollider = GetComponent<CapsuleCollider>();
            MainRigidbody = GetComponent<Rigidbody>();
            MainTransform = GetComponent<Transform>();
            
            StateSystem = new StateSystem(this, juniorState, middleState, seniorState, flickerLength, unstopLength);

            _gameInput = new GameInput();
            _gameInput.Enable();
            _gameInput.Player.SetCallbacks(this);

            _ballSpawner = new BallSpawner.BallSpawner(firePoint, groundLayers, fireballParent, spawnerData);
            _isGrounded = new Interactor(MainCollider, Axis.Vertical, isGroundedLength, isGroundedLayer, ISGROUND_BOX_INDENT, true);
            _move = new Move(this, moveData, MainRigidbody, MainCollider, isGroundedLength, isGroundedLayer);
            _interactionsHandler = new InteractionsHandler(StateSystem, MainCollider, this, _move, topLength,
                topLayer, bottomLength, bottomLayer);
        }

        private void FixedUpdate(){
            if (StateSystem.ExtraState == ExtraState.NormalState)
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
                if (StateSystem.Data.CanShoot)
                    _ballSpawner.Spawn();

                _move.OnExtra(true);
            }

            if (context.phase == InputActionPhase.Canceled)
                _move.OnExtra(false);
        }

        private void OnDrawGizmos(){
            _move?.OnDrawGizmos(Color.cyan);
            _interactionsHandler?.OnDrawGizmos(Color.grey);
        }
    }
}