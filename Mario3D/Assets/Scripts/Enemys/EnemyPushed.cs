﻿using System.Collections;
using Character;
using Character.States;
using Enums;
using UnityEngine;

namespace Enemys
{
    public class EnemyPushed : ActiveEnemy
    {
        private const int SCORE_LIST_ELEMENT = 3;
        private const float STATES_TRANSITON_DELAY = 0.15f;

        [Header("EnemyPusher")]
        [SerializeField] private float engageSpeed;

        [SerializeField] private float cooldownTime;
        [SerializeField] private PusherState currentState = PusherState.Walk;
        [SerializeField] private MeshRenderer mainMesh;
        [SerializeField] private MeshRenderer secondaryMesh;

        private float _patrolSpeed;
        private readonly InRowCounter _inRowCounter = new InRowCounter();
        private Coroutine _endOfCooldown;

        protected override void Awake(){
            base.Awake();
            _patrolSpeed = base._motor.GetSpeed();
        }

        protected override void Interaction(StateSystem state, Vector3 pos){
            if (currentState == PusherState.Cooldown){
                SendScore(SCORE_LIST_ELEMENT);
                TransitonInEngage(pos);
                return;
            }

            base.Interaction(state, pos);
        }

        public override void JumpOn(Vector3 senderCenter, int inRowJumpCount){
            if (currentState == PusherState.Cooldown){
                base.SendScore(_inRowCounter.Count);
                TransitonInEngage(senderCenter);
                return;
            }

            this._inRowCounter.Reset();
            base.JumpOn(senderCenter, inRowJumpCount);
            this.ChangeMesh(false);
            StartCoroutine(CooldownDelay());
            _endOfCooldown = StartCoroutine(EndOfCooldown());
        }

        private void TransitonInEngage(Vector3 boundsCenter){
            var dir = boundsCenter.z >= _collider.bounds.center.z ? Direction.Left : Direction.Right;
            StartCoroutine(Engage(dir));
        }

        protected override void OnFrontCollision(RaycastHit hit){
            var activeEnemy = hit.collider.GetComponentInParent<ActiveEnemy>();

            if (currentState == PusherState.Engage && activeEnemy){
                if (activeEnemy is EnemyPushed pushedEnemy && pushedEnemy.currentState == PusherState.Engage){
                    base._motor.DirectionChange();
                }
                else{
                    activeEnemy.Drop();
                    base.SendScore(_inRowCounter.Count);
                    _inRowCounter.Inreace();
                }
            }
            else{
                base._motor.DirectionChange();
            }
        }

        private IEnumerator Engage(Direction dir){
            StopCoroutine(_endOfCooldown);
            base._motor.SetActive(true);
            base._motor.SetDirection(dir);
            base._motor.SetSpeed(engageSpeed);
            currentState = PusherState.Engage;

            yield return new WaitForSeconds(STATES_TRANSITON_DELAY);
            base.DoBounce = true;
            base.DoDamage = true;
        }

        private IEnumerator CooldownDelay(){
            yield return new WaitForSeconds(STATES_TRANSITON_DELAY);
            currentState = PusherState.Cooldown;
        }

        private IEnumerator EndOfCooldown(){
            yield return new WaitForSeconds(cooldownTime);
            base._motor.SetActive(true);
            base._motor.SetSpeed(_patrolSpeed);
            base.DoDamage = true;
            base.DoBounce = true;
            this.ChangeMesh(true);
            currentState = PusherState.Walk;
        }

        private void ChangeMesh(bool primaryMeshIsActive){
            mainMesh.gameObject.SetActive(primaryMeshIsActive);
            secondaryMesh.gameObject.SetActive(!primaryMeshIsActive);
        }
    }
}