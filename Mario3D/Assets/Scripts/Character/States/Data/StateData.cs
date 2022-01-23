using UnityEngine;

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

        [SerializeField] private GameObject skinObject;

        public GameObject SkinObject => skinObject;

        public GameObject Skin{ get; set; }
    }
}


