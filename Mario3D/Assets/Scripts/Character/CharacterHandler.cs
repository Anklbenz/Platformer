using Character.Interaction;
using Character.States;
using UnityEngine;

namespace Character
{
    [RequireComponent (typeof(BoxCollider))]
    public sealed class CharacterHandler : MonoBehaviour, IStateSystemHandler
    {
        [Header("FireballSpawner")]
        [SerializeField] private BallSpawnerData _spawnerData;
        [SerializeField] private Transform _firePoint, _fireballParent;
        [SerializeField] private LayerMask _groundLayers;

        [Header("StateHandler")]
        [SerializeField] private StateData _juniorState;
        [SerializeField] private StateData _middleState;
        [SerializeField] private StateData _seniorState;
        [SerializeField] private Transform _skinsParent;

        [Header("InteractionHandler")]
        [SerializeField] private float _topInteractionLength;
        [SerializeField] private float _bottomInteractionLength;
        [SerializeField] private LayerMask _topInteractionLayer, _bottomInteractionLayer;
   

        public StateSystem StateSystem;
        public BoxCollider MainCollider{ get => _mainCollider; }
        public Transform SkinsParent{ get => _skinsParent; }
        
        private InteractorHandler _interactorHandler;
        private BoxCollider _mainCollider;
        private BallSpawner _ballSpawner;
        private Flicker _flicker;

        private void Start(){
            var moveData = GetComponent<IMoveData>();
            _mainCollider = GetComponent<BoxCollider>();

            _flicker = new Flicker();
            _ballSpawner = new BallSpawner(_firePoint, _groundLayers, _fireballParent, _spawnerData);
            StateSystem = new StateSystem(this, _juniorState, _middleState, _seniorState);
            _interactorHandler = new InteractorHandler(StateSystem, moveData, _topInteractionLength, _topInteractionLayer,
                _bottomInteractionLength, _bottomInteractionLayer);
        }

        private void Update() {
            if (StateSystem.Data.CanShoot && Input.GetKeyDown(KeyCode.Space))
                _ballSpawner.Spawn();
        }

        private void FixedUpdate(){
            _interactorHandler.Interaction();
        }
    }
}