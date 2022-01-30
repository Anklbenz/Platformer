using Interfaces;
using UnityEngine;

namespace Game
{
    public class ScreenActiveElementsController : MonoBehaviour
    {
        private const float BACKWALL_WIDTH = 2f;

        [SerializeField] private UnityEngine.Camera mainCam;
        [SerializeField] private Activator activator;
        [SerializeField] private Deactivator deactivator;
        [SerializeField] private float activatorIndent, deactivatorIndent, viewportRectDepth;
        [SerializeField] private BoxCollider backWall;

        private Vector3 _viewRectCenter, _backWallCenter;
        private BoxCollider _activatorCollider, _deactivatorCollider;
        private float _horizontalOffset, _backWallOffset;

        private void Awake(){
            _activatorCollider = activator.ActiveCollider;
            _deactivatorCollider = deactivator.ActiveCollider;
            activator.ActivateObjectEvent += OnActivate;
            deactivator.DeactivateObjectEvent += OnDeactivate;
            Initialize();
        }

        private void OnDisable(){
            activator.ActivateObjectEvent -= OnActivate;
            deactivator.DeactivateObjectEvent -= OnDeactivate;
        }

        private void FixedUpdate(){
            _viewRectCenter.z = _horizontalOffset + mainCam.transform.position.z;

            _activatorCollider.transform.position = _viewRectCenter;
            _deactivatorCollider.transform.position = _viewRectCenter;

            _backWallCenter = _viewRectCenter;
            _backWallCenter.z -= _backWallOffset;
            backWall.transform.position = _backWallCenter;
        }

        private void Initialize(){
            var camPosition = mainCam.transform.position;
            var distance = CameraToViewportDistance(camPosition);

            var rightTop = mainCam.ViewportToWorldPoint(new Vector3(1, 1, distance)); //1,1 rightTop Corner
            var leftBottom = mainCam.ViewportToWorldPoint(new Vector3(0, 0, distance)); // 0,0 leftBottom Corner

            _viewRectCenter = ActivatorCenterCalculate(rightTop, leftBottom);
            _activatorCollider.size = ColliderSizeCalculate(rightTop, leftBottom, activatorIndent);
            _deactivatorCollider.size = ColliderSizeCalculate(rightTop, leftBottom, deactivatorIndent);

            _horizontalOffset = _viewRectCenter.z - camPosition.z;

            _backWallOffset = (rightTop.z - leftBottom.z + BACKWALL_WIDTH) / 2;
            backWall.size = new Vector3(viewportRectDepth, rightTop.y - leftBottom.y, BACKWALL_WIDTH);
        }

        private Vector3 ColliderSizeCalculate(Vector3 rightTop, Vector3 leftBottom, float indent){
            var width = rightTop.z - leftBottom.z;
            var height = rightTop.y - leftBottom.y;
            width += 2 * indent;

            return new Vector3(viewportRectDepth, height, width);
        }

        private Vector3 ActivatorCenterCalculate(Vector3 rightTop, Vector3 leftBottom){
            var centerX = (rightTop.x + leftBottom.x) / 2;
            var centerY = (rightTop.y + leftBottom.y) / 2;
            var centerZ = (rightTop.z + leftBottom.z) / 2;

            return new Vector3(centerX, centerY, centerZ);
        }

        private float CameraToViewportDistance(Vector3 camPosition){
            var cameraToObject = transform.position - camPosition;
            return -Vector3.Project(cameraToObject, Vector3.left).x;
        }

        private void OnActivate(IScreenActivator instance) => instance.Activate();

        private void OnDeactivate(IScreenDeactivator instance) => instance.Deactivate();

        private void OnDrawGizmos(){
            if (_activatorCollider == null) return;

            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(_viewRectCenter, _activatorCollider.size);
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(_viewRectCenter, _deactivatorCollider.size);
            Gizmos.color = Color.black;
            Gizmos.DrawWireCube(_backWallCenter, backWall.size);
        }
    }
}

