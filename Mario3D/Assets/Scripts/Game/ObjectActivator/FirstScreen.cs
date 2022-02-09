using System;
using Interfaces;
using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(BoxCollider))]
    public class FirstScreen : MonoBehaviour
    {
        public event Action<IFirstScreenActivate> ActivateObjectEvent;
        public event Action<IFirstScreenDeactivate> DeactivateObjectEvent;
        public BoxCollider ActiveCollider{ get; private set; }

        [SerializeField] private LayerMask activateLayer;
        [SerializeField] private LayerMask deactivateLayer;

        private void Awake() => ActiveCollider = GetComponent<BoxCollider>();
        
        private void OnTriggerEnter(Collider other){
            if ((activateLayer.value & (1 << other.transform.gameObject.layer)) <= 0) return;
          
            var instance = other.GetComponent<IFirstScreenActivate>();
            if (instance == null) return;
            
            if (!instance.Activated) ActivateObjectEvent?.Invoke(instance);
        }

        private void OnTriggerExit(Collider other){
            var instance = other.GetComponent<IFirstScreenDeactivate>();
            if (instance == null) return;
        
            DeactivateObjectEvent?.Invoke(instance);
        }
    }
}
