using Enums;
using Interfaces;
using UnityEngine;

namespace Game
{
  public class ObjectActivator : MonoBehaviour
    {
        [SerializeField] private UnityEngine.Camera mainCam;
        [SerializeField] private float viewportRectDepth;
        [SerializeField] private float inspectLength;
        [SerializeField] private LayerMask layer;
        [SerializeField] private float indentFromRect;
       
        private InteractingWithRect _frontActivate, _frontDeactivate, _backDeactivate;
        private Vector3 _viewRectSize, _viewRectCenter ;
        private float _horizontalOffset;

        private void Awake(){
           CreateViewportRect();
           _frontActivate = new InteractingWithRect(_viewRectSize, Axis.Horizontal, inspectLength, layer);
           _frontDeactivate = new InteractingWithRect(_viewRectSize, Axis.Horizontal, inspectLength, layer, 1,false, indentFromRect);
         //  _backDeactivate = new InteractingWithRect(_viewRectSize, Axis.Horizontal, inspectLength, layer, 1 ,false, indentFromRect);
        }

        private void Update(){
            var camPosition = mainCam.transform.position;
            _viewRectCenter.z = _horizontalOffset + camPosition.z;
            
           var frontActivate = _frontActivate.InteractionOverlap(Vector3.forward, _viewRectCenter);
           
           if (frontActivate.Length > 0){
               foreach (var col in frontActivate){
                   var enemy = col.GetComponent<IScreenActivatorSensitive>();
                   enemy?.Activate();
               }
           }
           /*var _frontDeactivate.InteractionOverlap(Vector3.forward, _viewRectCenter);
           var  _backDeactivate.InteractionOverlap(Vector3.back, _viewRectCenter);*/
        }

        private void CreateViewportRect(){
            var camPosition = mainCam.transform.position;
            var distance = CameraToViewportDistance(camPosition);

            var rightTop = mainCam.ViewportToWorldPoint(new Vector3(1, 1, distance));
            var leftBottom = mainCam.ViewportToWorldPoint(new Vector3(0, 0, distance));
            
            var width = rightTop.z - leftBottom.z;
            var height = rightTop.y - leftBottom.y;

            _viewRectSize = new Vector3(viewportRectDepth, height, width);

            var centerX = (rightTop.x + leftBottom.x) / 2;
            var centerY = (rightTop.y + leftBottom.y) / 2;
            var centerZ =(rightTop.z + leftBottom.z) / 2;

            _viewRectCenter = new Vector3(centerX, centerY, centerZ);
            _horizontalOffset = _viewRectCenter.z - camPosition.z;
        }
       private float CameraToViewportDistance(Vector3 camPosition){
            var cameraToObject = transform.position - camPosition;
            return -Vector3.Project(cameraToObject, Vector3.left).x;
        }

       private void OnDrawGizmos(){
           if(_viewRectSize == Vector3.zero) return;
           
          _frontActivate.OnDrawGizmos(Color.green);
          _frontDeactivate.OnDrawGizmos(Color.red);
          _backDeactivate.OnDrawGizmos(Color.red);
       }
    }
}
