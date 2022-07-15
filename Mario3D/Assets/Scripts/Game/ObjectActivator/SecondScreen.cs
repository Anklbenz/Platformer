using System;
using Interfaces;
using UnityEngine;
using UnityEngine.UIElements;

namespace Game
{
    [RequireComponent(typeof(BoxCollider))]
    public class SecondScreen : MonoBehaviour
    {
        public event Action<ISecondScreenDeactivate> DeactivateObjectEvent;
        public BoxCollider ActiveCollider{ get; private set; }
        [SerializeField] private LayerMask activeLayer;
        private void Awake() => ActiveCollider = GetComponent<BoxCollider>();

        private void OnTriggerExit(Collider other){
            if ((activeLayer.value & (1 << other.transform.gameObject.layer)) <= 0) return;

            var instance = other.GetComponent<ISecondScreenDeactivate>();
            if (instance == null) return;

            DeactivateObjectEvent?.Invoke(instance);
        }
    }
}
