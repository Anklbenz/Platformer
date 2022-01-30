using System;
using Interfaces;
using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(BoxCollider))]
    public class Activator : MonoBehaviour
    {
        public event Action<IScreenActivator> ActivateObjectEvent;
        public BoxCollider ActiveCollider{ get; private set; }

        private void Awake(){
            ActiveCollider = GetComponent<BoxCollider>();
        }
        
        private void OnTriggerEnter(Collider other){
            var instance = other.GetComponent<IScreenActivator>();
            if (instance == null) return;

            if (!instance.Activated)
                ActivateObjectEvent?.Invoke(instance);
        }
    }
}
