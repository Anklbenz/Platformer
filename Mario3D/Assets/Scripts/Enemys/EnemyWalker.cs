﻿using MyEnums;
using UnityEngine;

public sealed class EnemyWalker : ActiveEnemy//MonoBehaviour, ITakeDamage, IDrop
{
    [SerializeField] private float destrAfterStomp = 0f;
    private Animator animator;

    protected override void Awake() {
        base.Awake();
        animator = GetComponent<Animator>();
    }

    public override void JumpOn(CharacterMove other) {
        base.JumpOn(other);
        animator.SetTrigger("isStomps");
        Destroy(gameObject, destrAfterStomp);
    }
}
