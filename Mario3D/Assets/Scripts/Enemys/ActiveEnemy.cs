using UnityEngine;
using MyEnums;
using System;

public class ActiveEnemy : ActiveInteractiveObject
{
    [SerializeField] protected float _timeToDestroyAfterDrop = 0f;
    protected Rigidbody _rbody;
    public bool DoDamage { get; set; } = true;
    public bool DoBounce { get; set; } = true;

    public event Action<ActiveEnemy>  OnEnememyEvent;


    protected override void Awake() {
        base.Awake();
        _rbody = GetComponent<Rigidbody>();
        base._patrol.OnEnemyFrontCollisionEvent += OnEnemyFrontCollision;
    }

    protected override void Interaction(Collider other) {
        if (DoDamage)
            other.GetComponent<StateHandler>()?.Hurt();
    }

    public override void Drop() {
        _collider.enabled = false;
        _rbody.constraints = RigidbodyConstraints.None;
        _rbody.AddForceAtPosition(Vector3.up * 50, _rbody.position * UnityEngine.Random.Range(50, 100), ForceMode.Impulse);
        Destroy(gameObject, _timeToDestroyAfterDrop);
    }

    protected virtual void OnEnemyFrontCollision(ActiveEnemy ae) {
        base._patrol.DirectionChange();
    }

    public virtual void JumpOn(CharacterMove other) {
        DoDamage = false;
        DoBounce = false;
        _patrol.SetActive(false);
    }

    private void OnDisable() {
        base._patrol.OnEnemyFrontCollisionEvent -= OnEnemyFrontCollision;
    }
}
