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

        [Header("Walk")]
        [SerializeField] private float maxWalkSpeed;
        [SerializeField] private float walkForceStep;
        
        [Header("ExtraWalk")]
        [SerializeField] private float maxExtraWalkSpeed;
        [SerializeField] private float extraWalkForceStep;

        [Header("Sitting")]
        [SerializeField] private Vector3 sitColliderSize;
        public float MaxWalkSpeed => maxWalkSpeed;
        public float WalkForceStep => walkForceStep;
        public float MaxJumpForceDuration => maxJumpForceDuration;
        public float AddForceStep => addForceStep;
        public float BouncePower => bouncePower;
        public float MaxExtraWalkSpeed => maxExtraWalkSpeed;
        public float ExtraWalkForceStep => extraWalkForceStep;
    }
}