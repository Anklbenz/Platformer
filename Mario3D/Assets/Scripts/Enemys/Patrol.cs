using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyEnums;

public sealed class Patrol : MonoBehaviour
{
    [Range(0, 15)] public float _patrolSpeed = 2f;
    [SerializeField] private LayerMask collisionMask;

    private Rigidbody _rb;
    private Collider _collider;
    private bool _isActive = true;

    readonly private Vector3 collisionCheckPlatformSize = new Vector3(0.15f, 0.15f, 0.01f);
    private float collisionCheckDistance;
    private RaycastHit rayHit;
    private Vector3 _dir = Vector3.back;
    public Direction direction;

    public delegate void EnemyOnFrontCollision(ActiveEnemy ae);
    public event EnemyOnFrontCollision OnEnemyFrontCollisionEvent;

    private void Awake() {
        _dir = direction == Direction.left ? Vector3.back : Vector3.forward;
        _rb = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
        collisionCheckDistance = _collider.bounds.size.z / 2 + collisionCheckPlatformSize.z * 2;
    }

    void FixedUpdate() {
        if (_isActive)
            Patrolling();
    }

    public void SetSpeed(float _spd) {
        _patrolSpeed = _spd;
    }

    public void SetActive(bool state) {
        _isActive = state;
    }


    public void SetDirection(Direction d) {
        _dir = d == Direction.left ? Vector3.back : Vector3.forward;
    }

    bool FrontCollision {
        get {
            return Physics.BoxCast(_collider.bounds.center,
                                    collisionCheckPlatformSize,
                                    _dir,
                                    out rayHit,
                                    transform.rotation,
                                    collisionCheckDistance,
                                    collisionMask);
        }
    }

    void Patrolling() {
        _rb.MovePosition(_rb.position + _dir * _patrolSpeed * Time.fixedDeltaTime);

        if (FrontCollision) {
            ActiveEnemy activeEnemy = rayHit.collider.GetComponent<ActiveEnemy>();

            if (!activeEnemy)
                DirectionChange();
            else
                OnEnemyFrontCollisionEvent?.Invoke(activeEnemy);
        }
    }

    public void DirectionChange() {
        _dir = _dir == Vector3.back ? Vector3.forward : Vector3.back;
    }

    void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position + _dir * collisionCheckDistance, collisionCheckPlatformSize * 2);
        // Gizmos.DrawWireCube(transform.position + transform.forward * collisionCheckDistance, collisionCheckPlatformSize * 2);
    }
}
//