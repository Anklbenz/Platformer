using System;
using System.Collections.Generic;
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

        [SerializeField] private BallSpawnerData spawnerData;
        [SerializeField] private Transform firePoint, fireballParent;
        [SerializeField] private LayerMask groundLayer, targetLayer;
        private BallSpawner.BallSpawner _ballSpawner;

        [Header("StateHandler")]
        [SerializeField] private List<StateData> stateDataList;

        [SerializeField] private Transform skinsParent;
        [SerializeField] private float unstopStateHitForce;
        [SerializeField] private int flickerLength, unstopLength;

        public Transform SkinsParent => skinsParent;
        public StateHandler StateHandler;

        [Header("InteractionsHandler")]
        [SerializeField] private float isGroundedLength;

        [SerializeField] private float topLength, bottomLength;
        [SerializeField] private LayerMask topLayer, bottomLayer, isGroundedLayer;
        private InteractionsHandler _interactionsHandler;
        private Interacting _isGrounded;
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

            StateHandler = new StateHandler(this, stateDataList, unstopStateHitForce, flickerLength, unstopLength);

            _gameInput = new GameInput();
            _gameInput.Enable();
            _gameInput.Player.SetCallbacks(this);

            _ballSpawner = new BallSpawner.BallSpawner(spawnerData, firePoint, fireballParent, groundLayer, targetLayer);
            _isGrounded = new Interacting(MainCollider, Axis.Vertical, isGroundedLength, isGroundedLayer, ISGROUND_BOX_INDENT, true);
            _move = new Move(this, moveData, MainRigidbody, MainCollider, isGroundedLength, isGroundedLayer);
            _interactionsHandler = new InteractionsHandler(StateHandler, MainCollider, this, _move, topLength,
                topLayer, bottomLength, bottomLayer);
        }

        private void FixedUpdate(){
            if (StateHandler.ExtraState == ExtraState.NormalState)
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
                if (StateHandler.Data.CanShoot)
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