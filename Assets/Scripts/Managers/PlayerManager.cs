using System.Collections.Generic;
using Common;
using Model;
using Player;
using Request;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

namespace Managers
{
    /// <summary>
    /// 玩家管理器
    /// </summary>
    public class PlayerManager : BaseManager
    {
        //玩家出生位置
        private Transform playerPositions;
        //当前玩家类型
        private RoleType currentRoleType;

        public UserData UserData { set; get; }
        //玩家模型字典
        private Dictionary<RoleType, RoleData> _roleDatas = new Dictionary<RoleType, RoleData>();
        //当前模型对象
        private GameObject currentRoleGO;
        //get
        public GameObject CurrentRoleGO => currentRoleGO;
        //对方模型对象
        private GameObject remoteRoleGO;
        //玩家异步请求
        private GameObject playerSyncRequset;
        //射箭请求
        private ShootRequest shootRequest;
        //攻击请求
        private AttackRequest attackRequest;

        public PlayerManager(GameFacade facade) : base(facade)
        {
        }
        /// <summary>
        /// 设置当前玩家类型
        /// </summary>
        /// <param name="rt">（玩家/模型）类型</param>
        public void SetCurrentRoleType(RoleType rt)
        {
            currentRoleType = (RoleType)rt;
        }

        public override void Init()
        {
            //获取玩家出生位置对象
            playerPositions = GameObject.Find("PlayerPosition").transform;
            // 添加玩家模型到字典中
            InitRoleDataDict();
            base.Init();
        }


        /// <summary>
        /// 添加玩家模型到字典中
        /// </summary>
        private void InitRoleDataDict()
        {
            //蓝色方
            _roleDatas.Add(RoleType.Blue, new RoleData(RoleType.Blue, "Hunter_BLUE", "Arrow_BLUE", "Explosion_BLUE", playerPositions.Find("Position1")));
            //红色方
            _roleDatas.Add(RoleType.Red, new RoleData(RoleType.Red, "Hunter_RED", "Arrow_RED", "Explosion_RED", playerPositions.Find("Position2")));
        }
        /// <summary>
        /// 生成玩家模型
        /// </summary>
        public void SpawnRoles()
        {
            //遍历字典值
            foreach (RoleData rd in _roleDatas.Values)
            {
                //创建预制体模型
                GameObject go = GameObject.Instantiate(rd.RolePrefab, rd.SpawnPosition, Quaternion.identity);
                //添加玩家标签
                go.tag = "Player";
                //判断是否为当前玩家
                if (rd.RoleType == currentRoleType)
                {
                    //设置前玩家
                    currentRoleGO = go;
                    //添加玩家信息脚本
                    currentRoleGO.GetComponent<PlayerInfo>().isLocal = true;
                }
                else
                {
                    //设置对方玩家对象
                    remoteRoleGO = go;
                }

            }
        }
        /// <summary>
        /// 获取玩家模型类型
        /// </summary>
        /// <param name="rt">模型类型</param>
        /// <returns>模型数据</returns>
        public RoleData GetRoleData(RoleType rt)
        {
            return _roleDatas.TryGet(rt);
        }
        /// <summary>
        /// 添加控制脚本
        /// </summary>
        public void AddControlScript()
        {
            //当前玩家添加控制移动脚本
            currentRoleGO.AddComponent<PlayerMove>();
            //当前玩家添加控制攻击脚本
            var playerAttack = currentRoleGO.AddComponent<PlayerAttack>();
            //获取当前玩家模型类型
            var rt = currentRoleGO.GetComponent<PlayerInfo>().roleType;
            //获取当前玩家模型数据
            var rd = GetRoleData(rt);
            //设置当前玩家箭支模型
            playerAttack.ArrowPrefab = rd.ArrowPrefab;
            //设置玩家攻击脚本中的玩家管理类
            playerAttack.SetPlayerMng(this);
        }
        /// <summary>
        /// 创建异步请求
        /// </summary>
        public void CreateSyncRequest()
        {
            //创建异步请求对象
            playerSyncRequset = new GameObject("PlayerSyncRequest");
            //添加玩家移动请求并设置当前玩家
            playerSyncRequset.AddComponent<MoveRequest>()
                .SetLocalPlayer(currentRoleGO.transform, currentRoleGO.GetComponent<PlayerMove>()).SetRemotePlayer(remoteRoleGO.transform);
            //添加玩家射击请求
            shootRequest = playerSyncRequset.AddComponent<ShootRequest>();
            //设置射击请求玩家管理对象
            shootRequest.PlayerManager = this;
            //添加玩家攻击请求
            attackRequest = playerSyncRequset.AddComponent<AttackRequest>();
        }
        /// <summary>
        /// 射击方法
        /// </summary>
        /// <param name="arrowPrefab">箭支模型</param>
        /// <param name="pos">位置</param>
        /// <param name="rotation">旋转</param>
        public void Shoot(GameObject arrowPrefab, Vector3 pos, Quaternion rotation)
        {
            //播放射击音频
            facade.PlayNormalAudio(AudioNames.Timer);
            //创建箭支模型，并设置为本地
            var arrow = GameObject.Instantiate(arrowPrefab, pos, rotation).GetComponent<Arrow>().isLocal = true;
            //var roleType = arrowPrefab.GetComponent<Arrow>().ArrowInfo.roleType;
            //获取箭支模型类型
            var roleType1 = arrowPrefab.GetComponent<ArrowInfo>().roleType;
            //发送射击请求
            shootRequest.SendRequest(roleType1, pos, rotation.eulerAngles);
        }
        /// <summary>
        /// 对方玩家射击
        /// </summary>
        /// <param name="roleType">箭支类型</param>
        /// <param name="pos">位置</param>
        /// <param name="rotation">旋转</param>
        public void RemoteShoot(RoleType roleType, Vector3 pos, Vector3 rotation)
        {
            //播放射击音频
            facade.PlayNormalAudio(AudioNames.Timer);
            //获取箭支模型
            var arrowPrefab = GetRoleData(roleType).ArrowPrefab;
            //创建模型并获取transform
            var trans = GameObject.Instantiate(arrowPrefab).transform;
            //设置模型位置和旋转
            trans.position = pos;
            trans.eulerAngles = rotation;

        }
        /// <summary>
        /// 发送攻击请求
        /// </summary>
        /// <param name="damage">伤害值</param>
        public void SendAttack(int damage)
        {
            attackRequest.SendRequest(damage);
        }
    }
}
