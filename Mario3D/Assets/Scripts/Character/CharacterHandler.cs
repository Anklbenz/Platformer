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
    public sealed class CharacterHandler : MonoBehaviour, IStateData, ICharacterComponents, IMoveInfo, GameInput.IPlayerActions, IScreenDeactivator
    {
        [Header("BallSpawner")]
        [SerializeField] private BallSpawnerData spawnerData;
        [SerializeField] private Transform firePoint, fireballParent;
        [SerializeField] private LayerMask groundLayer, targetLayer;
        private BallSpawner.BallSpawner _ballSpawner;

        [Header("StateHandler")]
        [SerializeField] private List<StateData> stateDataList;
        [SerializeField] private Transform skinsParent;
        [SerializeField] private float unstopHitForce;
        [SerializeField] private int flickerLength, unstopLength;
        private StateHandler _stateHandler;

        [Header("InteractionsHandler")]
        [SerializeField] private float isGroundedLength;
        [SerializeField] private float topLength, bottomLength;
        [SerializeField] private LayerMask topLayer, bottomLayer, isGroundedLayer;
        private InteractionsHandler _interactionsHandler;

        [Header("MoveHandler")]
        [SerializeField] private MoveData moveData;
        private Move _move;

        private GameInput _gameInput;
        public bool MovingUp => MainRigidbody.velocity.y > 0;
        public bool MovingDown => MainRigidbody.velocity.y < 0;
        public bool IsGrounded => _interactionsHandler.IsGrounded;
        public bool IsSittingState => _stateHandler.IsSitting;
        public Transform SkinsParent => skinsParent;
        public CapsuleCollider MainCollider{ get; private set; }
        public Rigidbody MainRigidbody{ get; private set; }
        public Transform MainTransform{ get; private set; }
  
        public IStateMethods StateMethods => _stateHandler.StateMethods;
        public StateData Data => _stateHandler.Data;

        private void Awake(){
            MainCollider = GetComponent<CapsuleCollider>();
            MainRigidbody = GetComponent<Rigidbody>();
            MainTransform = GetComponent<Transform>();
           
            
            _stateHandler = new StateHandler(this, stateDataList, unstopHitForce, flickerLength, unstopLength);

            _gameInput = new GameInput();
            _gameInput.Enable();
            _gameInput.Player.SetCallbacks(this);

            _ballSpawner = new BallSpawner.BallSpawner(spawnerData, firePoint, fireballParent, groundLayer, targetLayer);
            _move = new Move(this, moveData, MainRigidbody, MainCollider, isGroundedLength, isGroundedLayer);

            _interactionsHandler = new InteractionsHandler(this, MainCollider, this, _move, topLength,
                topLayer, bottomLength, bottomLayer, isGroundedLength, isGroundedLayer);
        }

        private void FixedUpdate(){
            if (_stateHandler.ExtraState == ExtraState.NormalState)
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
                if (_stateHandler.Data.CanShoot)
                    _ballSpawner.Spawn();

                _move.OnExtra(true);
            }

            if (context.phase == InputActionPhase.Canceled)
                _move.OnExtra(false);
        }

        public void OnSitDown(InputAction.CallbackContext context){
           if(context.phase == InputActionPhase.Performed)
              _stateHandler.SitDown(true);
           
           if(context.phase == InputActionPhase.Canceled)
               _stateHandler.SitDown(false);
        }

        private void OnDrawGizmos(){
            _move?.OnDrawGizmos(Color.cyan);
            _interactionsHandler?.OnDrawGizmos(Color.grey);
        }

        public void Deactivate(){
            Debug.LogError("GAME OVER Fall");
        }
       
    }
}