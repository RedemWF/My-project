using Managers;
using UnityEngine;

namespace Player
{
    /// <summary>
    /// 箭支类
    /// </summary>
    public class Arrow : MonoBehaviour
    {
        //飞行速度
        public int speed = 15;
        //箭支类型
        public ArrowInfo arrowInfo;
        //爆炸效果
        public GameObject explosionEffect;
        //箭支刚体
        private Rigidbody rgd;
        //是否为本地
        public bool isLocal = false;
        // Start is called before the first frame update
        void Start()
        {
            //获取箭支刚体
            rgd = GetComponent<Rigidbody>();
            //获取箭支信息类
            arrowInfo = GetComponent<ArrowInfo>();
        }

        // Update is called once per frame
        void Update()
        {
            //箭支移动
            rgd.MovePosition(transform.position + transform.forward * speed * Time.deltaTime);
        }
        /// <summary>
        /// 触发器
        /// </summary>
        /// <param name="other"></param>
        private void OnTriggerEnter(Collider other)
        {
            //判断碰撞到的是否为玩家
            if (other.tag == "Player")
            {
                //播放音效
                GameFacade.Instance.PlayNormalAudio(AudioNames.ShootPerson);
                //是否为本地箭支
                if (isLocal)
                {
                    //是否为本地玩家
                    var playerIsLocal = other.GetComponent<PlayerInfo>().isLocal;
                    if (isLocal != playerIsLocal)
                    {
                        //发送攻击请求造成伤害
                        GameFacade.Instance.SendAttack(Random.Range(10, 20));
                    }
                }
            }
            else
            {
                //播放未击中音效
                GameFacade.Instance.PlayNormalAudio(AudioNames.Miss);
            }
            //播放爆炸效果
            Instantiate(explosionEffect, transform.position, transform.rotation);
            //销毁箭支
            Destroy(gameObject);
        }
    }
}
