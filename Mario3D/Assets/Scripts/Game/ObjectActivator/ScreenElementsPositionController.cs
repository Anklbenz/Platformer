using Interfaces;
using UnityEngine;

namespace Game
{
    public class ScreenElementsPositionController
    {
        private const float BACK_WALL_WIDTH = 2f;

        private readonly UnityEngine.Camera _mainCam;
        private readonly BoxCollider _activatorCollider, _deactivateCollider, _backWallCollider;
        
        private FirstScreen _firstScreen;
        private SecondScreen _secondScreen;
        private Vector3 _viewRectCenter, _backWallCenter;
        private float  _horizontalOffset, _backWallOffset;

        public ScreenElementsPositionController(UnityEngine.Camera camera, Vector3 position, BoxCollider activatorCollider,
            BoxCollider deactivateCollider, BoxCollider backWallCollider, Vector2 activatorIndent, Vector2 deactivatorIndent,
            float viewportRectDepth){

            _activatorCollider = activatorCollider;
            _deactivateCollider = deactivateCollider;
            _backWallCollider = backWallCollider;
            _mainCam = camera;

            Initialize(position, activatorIndent, deactivatorIndent, viewportRectDepth);
        }

        private void Initialize(Vector3 activatorPosition, Vector2 activatorIndent, Vector2 deactivatorIndent, float depth){
            var camPosition = _mainCam.transform.position;
            var distance = CameraToViewportDistance(camPosition, activatorPosition);

            var rightTop = _mainCam.ViewportToWorldPoint(new Vector3(1, 1, distance)); //1,1 rightTop Corner
            var leftBottom = _mainCam.ViewportToWorldPoint(new Vector3(0, 0, distance)); // 0,0 leftBottom Corner

            _viewRectCenter = ActivatorCenterCalculate(rightTop, leftBottom);
            _activatorCollider.size = ColliderSizeCalculate(rightTop, leftBottom, depth, activatorIndent.y, activatorIndent.x);
            _deactivateCollider.size = ColliderSizeCalculate(rightTop, leftBottom, depth, deactivatorIndent.y, deactivatorIndent.x);

            _horizontalOffset = _viewRectCenter.z - camPosition.z;

            _backWallOffset = (rightTop.z - leftBottom.z + BACK_WALL_WIDTH) / 2;
            _backWallCollider.size = new Vector3(depth, rightTop.y - leftBottom.y, BACK_WALL_WIDTH);
        }

        private Vector3 ColliderSizeCalculate(Vector3 rightTop, Vector3 leftBottom, float depth, float indentY, float indentZ){
            var width = rightTop.z - leftBottom.z;
            var height = rightTop.y - leftBottom.y;
            width += 2 * indentZ;
            height += 2 * indentY;

            return new Vector3(depth, height, width);
        }

        private Vector3 ActivatorCenterCalculate(Vector3 rightTop, Vector3 leftBottom){
            var centerX = (rightTop.x + leftBottom.x) / 2;
            var centerY = (rightTop.y + leftBottom.y) / 2;
            var centerZ = (rightTop.z + leftBottom.z) / 2;

            return new Vector3(centerX, centerY, centerZ);
        }

        private float CameraToViewportDistance(Vector3 camPosition, Vector3 activatorPosition){
            var cameraToObject = activatorPosition - camPosition;
            return -Vector3.Project(cameraToObject, Vector3.left).x;
        }
        
        public void MoveActiveElements(){
            _viewRectCenter.z = _horizontalOffset + _mainCam.transform.position.z;

            _activatorCollider.transform.position = _viewRectCenter;
            _deactivateCollider.transform.position = _viewRectCenter;

            _backWallCenter = _viewRectCenter;
            _backWallCenter.z -= _backWallOffset;
            _backWallCollider.transform.position = _backWallCenter;
        }


      public void OnDrawGizmos(Color activateColor, Color deactivateColor, Color backWallColor){
            if (_activatorCollider == null) return;

            Gizmos.color = activateColor;
            Gizmos.DrawWireCube(_viewRectCenter, _activatorCollider.size);
            Gizmos.color = deactivateColor;
            Gizmos.DrawWireCube(_viewRectCenter, _deactivateCollider.size);
            Gizmos.color = backWallColor;
            Gizmos.DrawWireCube(_backWallCenter, _backWallCollider.size);
        }
    }
}

