using System.Collections.Generic;
using Character.Ball;
using Character.Interaction;
using Character.States;
using Character.States.Data;
using Enums;
using Input;
using Interfaces;
using UnityEngine;

namespace Character
{
    [RequireComponent(typeof(BoxCollider))]
    [RequireComponent(typeof(SphereCollider))]
    [RequireComponent(typeof(Rigidbody))]
    public sealed class CharacterHandler : MonoBehaviour, IStateData, ICharacterComponents, IMoveData, IScreenDeactivator
    {
      [Header("BallSpawner")]
        [SerializeField] private BallSpawnerData spawnerData;
        [SerializeField] private Transform firePoint, fireballParent;
        [SerializeField] private LayerMask groundLayer, targetLayer;
       private BallShooter _ballShooter;

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

        [Header("Input")]
        [SerializeField] private InputReader inputReader;

        public bool MovingUp => MainRigidbody.velocity.y > 0;
        public bool MovingDown => MainRigidbody.velocity.y < 0;
        public bool IsGrounded => _interactionsHandler.IsGrounded;
        public bool IsWallContact => _interactionsHandler.IsWall;
        public bool IsSittingState => _stateHandler.IsSitting;
        public Transform SkinsParent => skinsParent;
        public BoxCollider MainCollider{ get; private set; }
        public SphereCollider SecondaryCollider{ get; private set; }
        public Rigidbody MainRigidbody{ get; private set; }
        public Transform MainTransform{ get; private set; }
        public IStateMethods StateMethods => _stateHandler.StateMethods;
        public StateData Data => _stateHandler.Data;
        public Vector3 MoveDirection => _move.MoveDirection;

        private void Awake(){
            MainCollider = GetComponent<BoxCollider>();
            SecondaryCollider = GetComponent<SphereCollider>();
            MainRigidbody = GetComponent<Rigidbody>();
            MainTransform = GetComponent<Transform>();

            _move = new Move(this, moveData, MainRigidbody, MainCollider, isGroundedLength, isGroundedLayer);
            _stateHandler = new StateHandler(this, stateDataList, unstopHitForce, flickerLength, unstopLength);
            _ballShooter = new BallShooter(this, this,  spawnerData, firePoint, fireballParent, groundLayer, targetLayer);
            _interactionsHandler = new InteractionsHandler(this, MainCollider, this, _move, topLength,
                topLayer, bottomLength, bottomLayer, isGroundedLength, isGroundedLayer);
        }

        private void OnEnable(){
            inputReader.JumpEvent += _move.OnJump;
            inputReader.SitDownEvent += _stateHandler.SitDown;
            inputReader.ShootEvent += _ballShooter.Shoot;
            inputReader.ExtraActionEvent += _move.OnExtra;
            inputReader.MoveEvent += _move.OnMove;
            inputReader.MoveEvent += _ballShooter.SetFirePointPosition;
        }

        private void OnDisable(){
            inputReader.JumpEvent -= _move.OnJump;
            inputReader.SitDownEvent -= _stateHandler.SitDown;
            inputReader.ShootEvent -= _ballShooter.Shoot;
            inputReader.ExtraActionEvent -= _move.OnExtra;
            inputReader.MoveEvent -= _move.OnMove;
            inputReader.MoveEvent -= _ballShooter.SetFirePointPosition;
        }

        private void FixedUpdate(){
            if (_stateHandler.ExtraState == ExtraState.NormalState)
                _interactionsHandler.LegsInteractionsCheck();

            _interactionsHandler.HeadInteractionCheck();
            _move.RecalculateMoving();
        }
        
        private void OnDrawGizmos(){
            _interactionsHandler?.OnDrawGizmos(Color.grey);
        }

        public void Deactivate(){
            Debug.LogError("GAME OVER Fall");
        }

    }
}