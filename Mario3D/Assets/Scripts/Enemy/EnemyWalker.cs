using UnityEngine;

namespace Enemy
{
    public sealed class EnemyWalker : ActiveEnemy
    {
        [Header("EnemyWalker")]
        [SerializeField] private float destroyStompTime;
        private Animator _animator;

        protected override void Awake(){
            base.Awake();
            _animator = GetComponent<Animator>();
        }

        public override void JumpOn(Vector3 center, int jumpsInRowCount){
            base.JumpOn(center, jumpsInRowCount);

            _animator.SetTrigger("isStomps");
            Destroy(gameObject, destroyStompTime);
            Destroy(this);
        }
    }
}
