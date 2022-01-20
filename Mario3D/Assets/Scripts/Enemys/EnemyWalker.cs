using UnityEngine;

namespace Enemys
{
    public sealed class EnemyWalker : ActiveEnemy
    {
        [SerializeField] private float destrAfterStomp = 0f;
        private Animator animator;

        protected override void Awake() {
            base.Awake();
            animator = GetComponent<Animator>();
        }

        public override void JumpOn(Vector3 center, int inRowJumpCount) {
            base.JumpOn(center, inRowJumpCount);

            animator.SetTrigger("isStomps");
            Destroy(gameObject, destrAfterStomp);
            Destroy(this);
        }
    }
}
