using Managers;
using UnityEngine;

namespace Player
{
    /// <summary>
    /// 玩家攻击脚本
    /// </summary>
    public class PlayerAttack : MonoBehaviour
    {
        //动画
        private Animator ani;
        //玩家管理器
        private PlayerManager _playerManager;
        //箭支模型
        [SerializeField] private GameObject arrowPrefab;
        public GameObject ArrowPrefab
        {
            get => arrowPrefab;
            set => arrowPrefab = value;
        }
        //玩家模型左手位置
        private Transform leftHandTrans;
        //偏移
        private Vector3 dir;
        void Start()
        {
            //获取动画器
            ani = GetComponent<Animator>();
            //获取左手位置
            leftHandTrans =
                transform.Find(
                    "Bip001/Bip001 Pelvis/Bip001 Spine/Bip001 Neck/Bip001 L Clavicle/Bip001 L UpperArm/Bip001 L Forearm/Bip001 L Hand");
        }
        /// <summary>
        /// 设置玩家管理器
        /// </summary>
        /// <param name="playerManager"></param>
        public void SetPlayerMng(PlayerManager playerManager)
        {
            _playerManager = playerManager;
        }

        // Update is called once per frame
        void Update()
        {
            //判断动画是否正确
            if (ani.GetCurrentAnimatorStateInfo(0).IsName("Grounded"))
            {
                //左键射击
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    //相机绘制射线
                    var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    //设置碰撞器
                    RaycastHit hit;
                    var isCollider = Physics.Raycast(ray, out hit);
                    if (isCollider)
                    {
                        //目标位置
                        Vector3 targetPoint = hit.point;
                        targetPoint.y = transform.position.y;
                        dir = targetPoint - transform.position;
                        transform.rotation = Quaternion.LookRotation(dir);
                        ani.SetTrigger("Attack");
                        Invoke("Shoot", .5f);
                    }
                }
            }
        }
        /// <summary>
        ///  调用射击方法
        /// </summary>
        private void Shoot()
        {
            _playerManager.Shoot(ArrowPrefab, leftHandTrans.position, Quaternion.LookRotation(dir));
        }
    }
}
