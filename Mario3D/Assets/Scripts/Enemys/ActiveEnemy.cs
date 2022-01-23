using System;
using Character.States;
using Interfaces;
using UnityEngine;

namespace Enemys
{
    public class ActiveEnemy : ActiveInteractiveObject, IJumpOn, IScoreChangeNotify
    {
        public event Action<IScoreChangeNotify, int> ScoreChangeEvent;

        [Header("ActiveEnemy")]
        [SerializeField] protected float _timeToDestroyAfterDrop = 0f;

        private Rigidbody _rbody;
        protected bool DoDamage { get; set; } = true;
        public bool DoBounce { get; set; } = true;
        public Vector3 Position { get => _collider.bounds.center; }

        protected override void Awake() {        
            base.Awake();
            _rbody = GetComponent<Rigidbody>();        
        }

        protected override void Interaction(StateSystem state, Vector3 pos) {
            if (DoDamage)
                state?.Hurt(this);
        }

        public virtual void JumpOn(Vector3 center, int InRowJumpCount) {
            DoDamage = false;
            DoBounce = false;
            _motor.SetActive(false);

            SendScore(InRowJumpCount);
        }

        public void Drop() {
            _collider.enabled = false;
            _rbody.constraints = RigidbodyConstraints.None;
            _rbody.AddForceAtPosition(Vector3.up * 30, _rbody.position * UnityEngine.Random.Range(50, 100), ForceMode.Impulse);
            Destroy(gameObject, _timeToDestroyAfterDrop);
        }

        protected void SendScore(int InRowCount) {
            ScoreChangeEvent?.Invoke(this, InRowCount);
        }

        public override void DownHit() {
            SendScore(0);
            Drop();
        }
    }
}
