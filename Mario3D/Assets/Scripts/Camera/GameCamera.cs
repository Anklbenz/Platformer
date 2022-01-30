using UnityEngine;

namespace Camera
{
    public sealed class GameCamera : MonoBehaviour
    {
        [SerializeField] private float cameraSpeed = 3f;
        [SerializeField] private Transform target;
        private float posZ;
        private void Awake() {
            transform.position = new Vector3(transform.position.x, transform.position.y, target.position.z -6);

    
            posZ = target.position.z -6;
          
        }

        void LateUpdate() {
            Vector3 pos = target.position;
            pos.x = transform.position.x;
            pos.y = transform.position.y;
            if (pos.z -6 > posZ) {
                posZ = pos.z -6;
            }

            pos.z = posZ;

            transform.position = Vector3.Lerp(transform.position, pos, cameraSpeed * Time.deltaTime);
        }
    }
}
