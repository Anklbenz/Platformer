using System;
using Interfaces;
using UnityEngine;

namespace Game
{
    public class ScreenActiveElementsHandler : MonoBehaviour
    {
        [SerializeField] private UnityEngine.Camera mainCam;
        [SerializeField] private FirstScreen firstScreen;
        [SerializeField] private SecondScreen secondScreen;
        [SerializeField] private BoxCollider backWall;
        [SerializeField] private float viewportRectDepth;
        [SerializeField] private Vector2 firstScreenIndent, secondScreenIndent;

        private ScreenElementsPositionController _positionController;

        private void Awake(){
            var activatorCollider = firstScreen.ActiveCollider;
            var deactivateCollider = secondScreen.ActiveCollider;
            var position = transform.position;
            _positionController = new ScreenElementsPositionController(mainCam, position, activatorCollider, deactivateCollider, backWall,
                firstScreenIndent, secondScreenIndent, viewportRectDepth);
        }

        private void OnEnable(){
            firstScreen.ActivateObjectEvent += OnFirstScreenActivate;
            firstScreen.DeactivateObjectEvent += OnFirstScreenDeactivate;
            secondScreen.DeactivateObjectEvent += OnSecondScreenDeactivate;
        }

        private void OnDisable(){
            firstScreen.ActivateObjectEvent -= OnFirstScreenActivate;
            firstScreen.DeactivateObjectEvent -= OnFirstScreenDeactivate;
            secondScreen.DeactivateObjectEvent -= OnSecondScreenDeactivate;
        }

        private void FixedUpdate(){
            _positionController.MoveActiveElements();
        }

        private void OnFirstScreenActivate(IFirstScreenActivate instance) => instance.Activate();
        private void OnFirstScreenDeactivate(IFirstScreenDeactivate instance) => instance.Deactivate();
        private void OnSecondScreenDeactivate(ISecondScreenDeactivate instance) => instance.Deactivate();

        private void OnDrawGizmos(){
            _positionController?.OnDrawGizmos(Color.green, Color.red, Color.white);
        }
    }
}

