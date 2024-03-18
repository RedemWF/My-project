using UnityEngine;

namespace Cameras
{
    /// <summary>
    /// 相机跟随目标脚本
    /// </summary>
    public class FollowTarget : MonoBehaviour
    {
        public Transform target;
        private float smoothing = 2;
        private Vector3 offset = new Vector3(0f, 31f, -14f);

        // Update is called once per frame
        void Update()
        {
            Vector3 targetPosition = target.position + offset;
            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothing * Time.deltaTime);
            transform.LookAt(target);
        }
    }
}
