using UnityEngine;

namespace Player
{
    /// <summary>
    /// 玩家移动脚本
    /// </summary>
    public class PlayerMove : MonoBehaviour
    {
        //移动速度
        private float speed = 3;
        //朝向
        public float forward = 0;
        // 动画器
        private Animator ani;
        // Start is called before the first frame update
        void Start()
        {
            ani = GetComponent<Animator>();
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");
            if (Mathf.Abs(h) > 0 || Mathf.Abs(v) > 0)
            {
                transform.Translate(new Vector3(h, 0, v) * speed * Time.deltaTime, Space.World);
                transform.rotation = Quaternion.LookRotation(new Vector3(h, 0, v));
                float res = Mathf.Max(Mathf.Abs(h), Mathf.Abs(v));
                forward = res;
                ani.SetFloat("Forward", res);
            }
        }
    }
}
