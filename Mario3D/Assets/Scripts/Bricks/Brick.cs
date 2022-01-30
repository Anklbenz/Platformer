using System;
using Character.States;
using Enums;
using Interfaces;
using UnityEngine;

namespace Bricks
{
    public abstract class Brick : MonoBehaviour, IBrickHit, IBonusSpawn
    {
        public event Action<IBonusSpawn> BonusSpawnEvent;

        [Range(0, 10)]
        [SerializeField] protected int bonusesCount;
        [SerializeField] protected BonusType bonusType;
        [SerializeField] protected GameObject primaryMesh;
        [SerializeField] protected GameObject secondaryMesh;
        [SerializeField] protected BoxCollider brickCollider;
        [SerializeField] protected Transform bonusCreationPoint;

        protected bool IsActive = true;
    
        public Vector3 BonusCreatePoint =>bonusCreationPoint.position;
        public BonusType BonusType => bonusType;

        public abstract void BrickHit(bool canCrush);

        protected void BonusShow(bool canCrush){
            if (bonusType == BonusType.None) return;

            if (bonusType == BonusType.GrowUp || bonusType == BonusType.Flower)
                bonusType = canCrush ? BonusType.Flower : BonusType.GrowUp;

            BonusSpawnEvent?.Invoke(this);
        }
    }
}
