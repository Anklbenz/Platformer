using System;
using Enums;
using UnityEngine;
using Object = System.Object;

namespace Character.States.Data
{
    [CreateAssetMenu(fileName = "NewState", menuName = "stateData")]
    public class StateData : ScriptableObject
    {
        [SerializeField] private bool canCrush;
        public bool CanCrush => canCrush;

        [SerializeField] private bool canSit;
        public bool CanSit => canSit;

        [SerializeField] private bool canShoot;
        public bool CanShoot => canShoot;

        [SerializeField] private Vector3 colliderSize;
        public Vector3 ColliderSize => colliderSize;

        [SerializeField] private Vector3 sitColliderSize;
        public Vector3 SitColliderSize => sitColliderSize;

        [SerializeField] private GameObject skinObjectLink;

        public GameObject Skin{ get; private set; }

        public void SkinInstantiate(Transform parent){
            if(Skin != null) return;
            
            Skin = Instantiate(skinObjectLink, parent);
        }
    }
}


