using UnityEngine;

public sealed class Character : MonoBehaviour
{
    [Header("FireballSpawner")]
    [SerializeField] private BallSpawnerData _spawnerData;
    [SerializeField] private Transform _firePoint, _fireballParent;
    [SerializeField] private LayerMask _groundLayers;


    [Header("StateHandler")]
    [SerializeField] private StateData _juniorState;
    [SerializeField] private StateData _middleState , _seniorState;
    private BoxCollider _mainCollider;


    public bool CanCrush { get; set; }
    private bool CanShoot { get; set; }
    private bool CanSitting { get; set; }

    private Vector3 _sitColliderSize;
    private MeshRenderer _mesh;
    private MeshRenderer _sitMesh;

    
    [SerializeField] private float _immortalityTime;
    public bool ImmortalState { get; set; }

    private BallSpawner _ballSpawner;
    public StateHandler StateHandler;
    private StateTransition _flicker;

    private void Awake() {
        _mainCollider = GetComponent<BoxCollider>();
        _ballSpawner = new BallSpawner(_firePoint, _groundLayers, _fireballParent, _spawnerData);
        StateHandler = new StateHandler(this, _juniorState, _middleState, _seniorState);
        _flicker = GetComponent<StateTransition>();  
    }

    private void Update() {
        if (_ballSpawner.isActive && Input.GetKeyDown(KeyCode.Space))
            _ballSpawner.Spawn();
    }

    public void BallSpawnerSetActive(bool state) {
        _ballSpawner.isActive = state;
    }

    public void UpdateStateData(StateData newData) {
        
        CanCrush = newData.CanCrush;
        CanSitting = newData.CanSit;
        CanShoot = newData.CanShoot;

        _mainCollider.size= newData.ColliderSize;

        _sitColliderSize = newData.SitColliderSize;
       // _mesh = newData.StateMesh;

        BallSpawnerSetActive(CanShoot);
    }

    public bool CompareCurrentStateWith<T>() where T : State {
        return StateHandler.CompareCurrentStateWith<T>();
    }

    public void PlayFlick() {
        _flicker.PlayFlick(_immortalityTime);
    }

    public void PlaySizeTransition() {
        _flicker.PlaySizeTransition(_immortalityTime);
    }
}