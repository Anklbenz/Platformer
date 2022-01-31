using UnityEngine;

namespace Character.States.Data
{
    [CreateAssetMenu(fileName = "NewState", menuName = "stateData")]

    public class StateData : ScriptableObject
    {
        [SerializeField] private bool canCrush;
        [SerializeField] private bool canShoot;
        [SerializeField] private bool canSit;
        [SerializeField] private Vector3 colliderSize;
        [SerializeField] private Vector3 sitColliderSize; 
        [SerializeField] private GameObject skinPrefab;
        public bool CanCrush => canCrush;

        public bool CanSit => canSit;

        public bool CanShoot => canShoot;

        public Vector3 ColliderSize => colliderSize;
        public Vector3 SitColliderSize => sitColliderSize;

        public GameObject Skin{ get; private set; }

        public void SkinInstantiate(Transform parent){
            if (Skin != null) return;

            Skin = Instantiate(skinPrefab, parent);
            Skin.SetActive(false);
        }
    }
}


