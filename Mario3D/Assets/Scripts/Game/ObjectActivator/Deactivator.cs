using System;
using Interfaces;
using UnityEngine;
using UnityEngine.UIElements;

namespace Game
{
    [RequireComponent(typeof(BoxCollider))]
    public class Deactivator : MonoBehaviour
    {
        public event Action<IScreenDeactivator> DeactivateObjectEvent;
        public BoxCollider ActiveCollider{ get; private set; }

        private void Awake(){
            ActiveCollider = GetComponent<BoxCollider>();
        }

        private void OnTriggerExit(Collider other){
            var instance = other.GetComponent<IScreenDeactivator>();
            if (instance == null) return;
             
            DeactivateObjectEvent?.Invoke(instance);
        }
    }
}
