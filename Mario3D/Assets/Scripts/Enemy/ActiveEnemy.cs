using System;
using Interfaces;
using UnityEngine;

namespace Enemy
{
    public class ActiveEnemy : ActiveInteractiveObject, IJumpOn, IScoreChangeNotify, IScreenActivator, IScreenDeactivator
    {
        public event Action<IScoreChangeNotify, int> ScoreChangeEvent;

        [Header("ActiveEnemy")]
        [SerializeField] protected float destroyDropTime;
        
        public bool Activated{ get; set; }
        public bool DoBounce{ get; set; } = true;
        protected bool DoDamage{ get; set; } = true;
        public Vector3 Position => ObjectCollider.bounds.center;
        
        private Vector3 RandomSideDirection =>  (UnityEngine.Random.Range(0, 2) == 0 ? Vector3.left : Vector3.right);

        protected override void Interaction(IStateHandlerInteraction stateHandler, Vector3 pos){
            if (DoDamage)
                stateHandler?.EnemyTouch(this);
        }

        public virtual void JumpOn(Vector3 center, int jumpsInRowCount){
            DoDamage = false;
            DoBounce = false;
            Motor.SetActive(false);

            SendScore(jumpsInRowCount);
        }

        public void Drop(float dropForce){
            var randomPointAtBottom = CalculateBottomRandomPoint();
       
            Rigidbody.constraints = RigidbodyConstraints.None;
            Rigidbody.AddForceAtPosition((Vector3.up + RandomSideDirection) * dropForce, randomPointAtBottom, ForceMode.Impulse);
            ObjectCollider.enabled = false;
            
            Destroy(gameObject, destroyDropTime);
        }

        private Vector3 CalculateBottomRandomPoint(){
            var bounds = ObjectCollider.bounds;
            var randomX = UnityEngine.Random.Range(bounds.min.x, bounds.max.x);
            var randomZ = UnityEngine.Random.Range(bounds.min.z, bounds.max.z);
            
            return new Vector3(randomX, bounds.min.y, randomZ);
        }

        protected void SendScore(int jumpsInRowCount = 0){
            ScoreChangeEvent?.Invoke(this, jumpsInRowCount);
        }

        public override void DownHit(float dropForce){
            SendScore();
            Drop(dropForce);
        }

        public void Standby(){
            Activated = false;
            Motor.SetActive(false);
            Rigidbody.useGravity = false;
        }

        public void Activate(){
            if (Activated) return;
            
            Motor.SetActive(true);
            Rigidbody.useGravity = true;
            Activated = true;
        }

        public void Deactivate(){
            if (Activated)
                gameObject.SetActive(false);
        }
    }
}
