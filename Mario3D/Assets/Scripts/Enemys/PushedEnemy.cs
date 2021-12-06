using System.Collections;
using UnityEngine;
using MyEnums;

public class PushedEnemy : ActiveEnemy
{
    const float TRANSITON_DELAY = 0.2f;

    [SerializeField] private float _engageSpeed;
    [SerializeField] private float _cooldownTime;
    [SerializeField] private PusherState _currentState = PusherState.Walk;
    [SerializeField] private LayerMask collisionInEngage;
    [SerializeField] private MeshRenderer _mainMesh;
    [SerializeField] private MeshRenderer _secondaryMesh;
    protected float _patrolSpeed;
    private Coroutine endOfCooldown;

    protected override void Awake() {
        base.Awake();
        _patrolSpeed = base._patrol.GetSpeed();
    }

    protected override void Interaction(Collider other) {
        if (_currentState == PusherState.Cooldown) {
            TransitonToEngage(other.bounds.center);
            return;
        }
        base.Interaction(other);
    }

    public override void JumpOn(CharacterMove other) {
        if (_currentState == PusherState.Cooldown) {
            TransitonToEngage(other.ColliderBoundsCenter);
            return;
        }

        base.JumpOn(other);
        this.ChangeMesh(false);
        StartCoroutine(CooldownDelay());
        endOfCooldown = StartCoroutine(EndOfCooldown());
    }

    private void TransitonToEngage(Vector3 boundsCenter) {
        Direction dir = boundsCenter.z >= _collider.bounds.center.z ? Direction.left : Direction.rigth;
        StartCoroutine(Engage(dir));
    }

    protected override void OnEnemyFrontCollision(ActiveEnemy activeEnemy) {
        if (_currentState == PusherState.Engage) {

            if (activeEnemy is PushedEnemy pushedEnemy && pushedEnemy._currentState == PusherState.Engage)
                base._patrol.DirectionChange();
            else
                activeEnemy.Drop();
        } else {
            base._patrol.DirectionChange();
        }
    }

    IEnumerator Engage(Direction dir) {
        StopCoroutine(endOfCooldown);
        base._patrol.SetActive(true);
        base._patrol.SetDirection(dir);
        base._patrol.SetSpeed(_engageSpeed);       

        yield return new WaitForSeconds(TRANSITON_DELAY);
        _currentState = PusherState.Engage;
        base.DoBounce = true;
        base.DoDamage = true;
    }

    IEnumerator CooldownDelay() {
        yield return new WaitForSeconds(TRANSITON_DELAY);
        _currentState = PusherState.Cooldown;
    }

    IEnumerator EndOfCooldown() {
        yield return new WaitForSeconds(_cooldownTime);
        base._patrol.SetActive(true);
        base._patrol.SetSpeed(_patrolSpeed);
        base.DoDamage = true;
        base.DoBounce = true;
        this.ChangeMesh(true);
        _currentState = PusherState.Walk;
    }

    private void ChangeMesh(bool primaryMeshIsActive) {
        _mainMesh.gameObject.SetActive(primaryMeshIsActive);
        _secondaryMesh.gameObject.SetActive(!primaryMeshIsActive);
    }
}