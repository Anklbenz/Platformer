using MyEnums;
using UnityEngine;

public sealed class EnemyWalker : ActiveEnemy
{
    [SerializeField] private float destrAfterStomp = 0f;
    private Animator animator;

    protected override void Awake() {
        base.Awake();
        animator = GetComponent<Animator>();
    }

    public override void JumpOn(Vector3 center) {
        base.JumpOn(center);
        animator.SetTrigger("isStomps");
        Destroy(gameObject, destrAfterStomp);
        Destroy(this);
    }
}
