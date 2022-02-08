using UnityEngine;

namespace Character
{
    [CreateAssetMenu]
    public class MoveData : ScriptableObject
    {
        [Header("Jump")]
        [SerializeField] private float maxJumpForceDuration;
        [SerializeField] private float addForceStep;
        [SerializeField] private float bouncePower;
        [SerializeField] private float startImpulse;
        [SerializeField] private float additionalGravity; //project gravity -30

        [Header("Walk")]
        [SerializeField] private float maxWalkSpeed;
        [SerializeField] private float walkForceStep;
        
        [Header("ExtraWalk")]
        [SerializeField] private float maxExtraWalkSpeed;
        [SerializeField] private float extraWalkForceStep;

        public float MaxWalkSpeed => maxWalkSpeed;
        public float WalkForceStep => walkForceStep;
        public float MaxJumpForceDuration => maxJumpForceDuration;
        public float AddForceStep => addForceStep;
        public float BouncePower => bouncePower;
        public float MaxExtraWalkSpeed => maxExtraWalkSpeed;
        public float ExtraWalkForceStep => extraWalkForceStep;
        public float StartImpulse => startImpulse;
        public float AdditionalGravity => additionalGravity;
    }
}