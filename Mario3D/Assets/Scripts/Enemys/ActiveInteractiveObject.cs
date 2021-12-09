using UnityEngine;
using System;

public abstract class ActiveInteractiveObject : InteractiveObject
{
    public event Action<ActiveEnemy> OnEnemyFrontCollisionEvent;

    [SerializeField] protected LayerMask _patrolLayer;
    
    protected int _score;
    public int Score { get; private set;}

    private readonly Vector3 CHECK_PLATFORM_SIZE = new Vector3(0.3f, 0.3f, 0);
    private const float CHECK_DISTANCE = 0.05f;
    private HitCatcher<ActiveEnemy> _hitCatcher;
    private float _checkDistance { get => CHECK_DISTANCE + _collider.size.z / 2; }
    private Vector3 _boxCenter { get => _collider.bounds.center; }
    protected BoxCollider _collider;
    protected Patrol _patrol;

    protected virtual void Awake() {
        _collider = GetComponent<BoxCollider>();
        _patrol = GetComponent<Patrol>();
        this.PatrolInitialize();
    }

    protected void FixedUpdate() {
        this.PatrolCollisionCheck();
    }

    private void PatrolInitialize() {
        _hitCatcher = new HitCatcher<ActiveEnemy>(CHECK_PLATFORM_SIZE, _checkDistance, _patrolLayer);
    }

    private void PatrolCollisionCheck() {
        if (_hitCatcher.CollisionBox(_boxCenter, _patrol.GetDirection(), out ActiveEnemy activeEnemy)) {
            if (!activeEnemy)
                _patrol.DirectionChange();
            else
                OnEnemyFrontCollisionEvent?.Invoke(activeEnemy);
        }
    }

    public abstract void Drop();

    void OnDrawGizmos() {
        if (!_collider) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(_boxCenter + (_patrol.GetDirection() * _checkDistance), CHECK_PLATFORM_SIZE / 2);
    }
}