using System.Collections;
using UnityEngine;
using MyEnums;
using System;
using Character.States;

public class EnemyPushed : ActiveEnemy
{
    private const int SCORE_LIST_ELEMENT = 3;
    private const float STATES_TRANSITON_DELAY = 0.2f;

    [Header("EnemyPusher")]
    [SerializeField] private float _engageSpeed;
    [SerializeField] private float _cooldownTime;
    [SerializeField] private PusherState _currentState = PusherState.Walk;
    [SerializeField] private LayerMask collisionInEngage;
    [SerializeField] private MeshRenderer _mainMesh;
    [SerializeField] private MeshRenderer _secondaryMesh;

    private float _patrolSpeed;
    private InRowCounter InRowCounter = new InRowCounter();
    private Coroutine endOfCooldown;

    protected override void Awake() {
        base.Awake();
        _patrolSpeed = base._motor.GetSpeed();
    }

    protected override void Interaction(StateSystem state, Vector3 pos) {
        if (_currentState == PusherState.Cooldown) {
            SendScore(SCORE_LIST_ELEMENT);
            TransitonToEngage(pos);
            return;
        }
        base.Interaction(state, pos);
    }

    public override void JumpOn(Vector3 senderCenter, int inRowJumpCount) {
        if (_currentState == PusherState.Cooldown) {
            base.SendScore(InRowCounter.Count);
            TransitonToEngage(senderCenter);
            return;
        }

        this.InRowCounter.Reset();
        base.JumpOn(senderCenter, inRowJumpCount);
        this.ChangeMesh(false);
        StartCoroutine(CooldownDelay());
        endOfCooldown = StartCoroutine(EndOfCooldown());
    }

    private void TransitonToEngage(Vector3 boundsCenter) {
        Direction dir = boundsCenter.z >= _collider.bounds.center.z ? Direction.left : Direction.rigth;
        StartCoroutine(Engage(dir));
    }

    protected override void OnFrontCollision(RaycastHit hit) {
        var activeEnemy = hit.collider.GetComponentInParent<ActiveEnemy>();

        if (_currentState == PusherState.Engage && activeEnemy) {
            if (activeEnemy is EnemyPushed pushedEnemy && pushedEnemy._currentState == PusherState.Engage) {
                base._motor.DirectionChange();
            } else {
                activeEnemy.Drop();
                base.SendScore(InRowCounter.Count);
                InRowCounter.Inreace();
            }
        } else {
            base._motor.DirectionChange();
        }
    }

    IEnumerator Engage(Direction dir) {
        StopCoroutine(endOfCooldown);
        base._motor.SetActive(true);
        base._motor.SetDirection(dir);
        base._motor.SetSpeed(_engageSpeed);
        _currentState = PusherState.Engage;

        yield return new WaitForSeconds(STATES_TRANSITON_DELAY);
        base.DoBounce = true;
        base.DoDamage = true;
    }

    IEnumerator CooldownDelay() {
        yield return new WaitForSeconds(STATES_TRANSITON_DELAY);
        _currentState = PusherState.Cooldown;
    }

    IEnumerator EndOfCooldown() {
        yield return new WaitForSeconds(_cooldownTime);
        base._motor.SetActive(true);
        base._motor.SetSpeed(_patrolSpeed);
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

//protected override void OnEnemyFrontCollision(ActiveEnemy activeEnemy)