using UnityEngine;
using UnityEngine.Serialization;

namespace Character.States.Data
{
    [CreateAssetMenu(fileName = "NewState", menuName = "stateData")]
    public class StateData : ScriptableObject
    {


        [SerializeField] private bool _canCrush;
        public bool CanCrush => _canCrush;

        [SerializeField] private bool _canSit;
        public bool CanSit => _canSit;

        [SerializeField] private bool _canShoot;
        public bool CanShoot => _canShoot;

        [SerializeField] private Vector3 _colliderSize;
        public Vector3 ColliderSize => _colliderSize;

        [SerializeField] private Vector3 _sitColliderSize;
        public Vector3 SitColliderSize => _sitColliderSize;

        public GameObject SkinGameObject;
    }
}

