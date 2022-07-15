using UnityEngine;

namespace Character
{
    [CreateAssetMenu]
    public class MoveData : ScriptableObject
    {
        [SerializeField] private float additionalGravity; //project gravity -10
        [Header("Jump")]
        [SerializeField] private float maxJumpDuration;
        [SerializeField] private float jumpForceStep;
        [SerializeField] private float jumpStartImpulse;
        [SerializeField] private float bouncePower;

        [Header("Walk")]
        [SerializeField] private float maxWalkSpeed;
        [SerializeField] private float walkForceStep;

        [Header("ExtraWalk")]
        [SerializeField] private float extraMaxWalkSpeed;
        [SerializeField] private float extraWalkForceStep;

        public float BouncePower => bouncePower;
        public float MaxWalkSpeed => maxWalkSpeed;
        public float JumpForceStep => jumpForceStep;
        public float WalkForceStep => walkForceStep;
        public float MAXJumpDuration => maxJumpDuration;
        public float JumpStartImpulse => jumpStartImpulse;
        public float AdditionalGravity => additionalGravity;
        public float ExtraMaxWalkSpeed => extraMaxWalkSpeed;
        public float ExtraWalkForceStep => extraWalkForceStep;
    }
}