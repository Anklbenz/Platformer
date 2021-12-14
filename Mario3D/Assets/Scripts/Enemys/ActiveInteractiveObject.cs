using UnityEngine;
using System;
using MyEnums;

public abstract class ActiveInteractiveObject : InteractiveObject
{
    [SerializeField] protected LayerMask _patrolLayer;
    public event Action<ActiveEnemy> OnEnemyFrontCollisionEvent;

    private const float BOX_INDENT = 0.5f;//%
    private const float CHECK_DISTANCE = 0.05f;

    private Interactor _interactor;
    protected BoxCollider _collider;
    protected Patrol _patrol;

    protected virtual void Awake() {
        _collider = GetComponent<BoxCollider>();
        _patrol = GetComponent<Patrol>();
        _interactor = new Interactor(_collider, Axis.horisontal, CHECK_DISTANCE, _patrolLayer, BOX_INDENT);
    }

    protected void FixedUpdate() {
        this.PatrolCollisionCheck();
    }

    private void PatrolCollisionCheck() {
        if (_interactor.InteractionBoxcast(_patrol.GetDirection(), out RaycastHit obj)) {
            ActiveEnemy activeEnemy = obj.collider.GetComponentInParent<ActiveEnemy>();

            if (!activeEnemy)
                _patrol.DirectionChange();
            else
                OnEnemyFrontCollisionEvent?.Invoke(activeEnemy);
        }
    }

    public abstract void DownHit();

    void OnDrawGizmos() {
        if (_interactor == null) return;
        _interactor.OnDrawGizmos(Color.white);
    }
}
