using System;
using Character.States;
using Interfaces;
using UnityEngine;

namespace Enemy
{
    public class ActiveEnemy : ActiveInteractiveObject, IJumpOn, IScoreChangeNotify, IActivatorSensitive
    {
        public event Action<IScoreChangeNotify, int> ScoreChangeEvent;

        [Header("ActiveEnemy")]
        [SerializeField] protected float destroyDropTime;

        public bool DoBounce{ get; set; } = true;
        protected bool DoDamage{ get; set; } = true;
        public Vector3 Position => Collider.bounds.center;

        protected override void Interaction(StateSystem state, Vector3 pos){
            if (DoDamage)
                state?.Hurt(this);
        }

        public virtual void JumpOn(Vector3 center, int jumpsInRowCount){
            DoDamage = false;
            DoBounce = false;
            Motor.SetActive(false);

            SendScore(jumpsInRowCount);
        }

        public void Drop(){
            Collider.enabled = false;
            Rigidbody.constraints = RigidbodyConstraints.None;
            Rigidbody.AddForceAtPosition(Vector3.up * 30, Rigidbody.position * UnityEngine.Random.Range(50, 100), ForceMode.Impulse);
            Destroy(gameObject, destroyDropTime);
        }

        protected void SendScore(int jumpsInRowCount = 0){
            ScoreChangeEvent?.Invoke(this, jumpsInRowCount);
        }

        public override void DownHit(){
            SendScore();
            Drop();
        }

        public void TurnOn(){
            Motor.SetActive(true);
            Rigidbody.isKinematic = false;
        }

        public void TurnOff(){
           Motor.SetActive(false);
           Rigidbody.isKinematic = true;
        }
    }
}
