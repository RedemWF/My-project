using System;
using Common;
using Player;
using Tools;
using UnityEngine;

namespace Request
{
    /// <summary>
    /// 移动请求
    /// </summary>
    public class MoveRequest : BaseRequest
    {
        //本地玩家trans
        private Transform localPlayerTransform;

        public Transform LocalPlayerTransform { get; set; }
        //本地玩家移动脚本
        private PlayerMove localPlayerMove;
        //对方玩家trans
        private Transform _remotePlayerTrans;
        public Transform RemotePlayerTrans
        {
            get => _remotePlayerTrans;
            set => _remotePlayerTrans = value;
        }
        //对方玩家动画
        private Animator _remotePlayerAni;

        public Animator RemotePlayerAni
        {
            get => _remotePlayerAni;
            set => _remotePlayerAni = value;
        }

        public PlayerMove LocalPlayerMove { get => localPlayerMove; set => localPlayerMove = value; }
        //同步速度
        private int syncRate = 30;
        private bool isRemotePlayer = false;
        private Vector3 position;
        private Vector3 rotation;
        private float forward;

        private static readonly int Forward = Animator.StringToHash("Forward");

        public override void Awake()
        {
            requestCode = RequestCode.Game;
            actionCode = ActionCode.Move;
            base.Awake();
        }

        private void Start()
        {
            InvokeRepeating(nameof(SyncLocalPlayer), 1, 1f / syncRate);
        }
        /// <summary>
        /// 上传同步本地玩家
        /// </summary>
        private void SyncLocalPlayer()
        {
            var eulerAngles = LocalPlayerTransform.eulerAngles;
            var position = LocalPlayerTransform.position;
            SendRequest(position.x,
                position.y,
                position.z,
                eulerAngles.x,
            eulerAngles.y,
                eulerAngles.z,
                LocalPlayerMove.forward
                );
        }

        private void FixedUpdate()
        {
            if (isRemotePlayer)
            {
                SyncRemotePlayer();
                isRemotePlayer = false;
            }
        }
        /// <summary>
        /// 同步对方玩家
        /// </summary>
        private void SyncRemotePlayer()
        {
            RemotePlayerTrans.position = position;
            RemotePlayerTrans.eulerAngles = rotation;
            RemotePlayerAni.SetFloat(Forward, forward);

        }
        /// <summary>
        /// 发送k求
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <param name="rotationX"></param>
        /// <param name="rotationY"></param>
        /// <param name="rotationZ"></param>
        /// <param name="forward"></param>
        private void SendRequest(float x, float y, float z, float rotationX, float rotationY, float rotationZ, float forward)
        {
            string data = $"{x},{y},{z}|{rotationX},{rotationY},{rotationZ}|{forward}";
            base.SendRequest(data);
        }

        public override void OnResponse(string data)
        {
            string[] strs = data.Split("|");
            position = UnityTools.ParseVector3(strs[0]);
            rotation = UnityTools.ParseVector3(strs[1]);
            forward = float.Parse(strs[2]);
            isRemotePlayer = true;
        }


        /// <summary>
        /// 设置本地玩家
        /// </summary>
        /// <param name="localPlayerTransform"></param>
        /// <param name="localPlayerMove"></param>
        /// <returns></returns>
        public MoveRequest SetLocalPlayer(Transform localPlayerTransform, PlayerMove localPlayerMove)
        {
            LocalPlayerTransform = localPlayerTransform;
            LocalPlayerMove = localPlayerMove;
            return this;
        }
        /// <summary>
        /// 设置对方玩家
        /// </summary>
        /// <param name="remotePlayerTransform"></param>
        /// <returns></returns>
        public MoveRequest SetRemotePlayer(Transform remotePlayerTransform)
        {
            RemotePlayerTrans = remotePlayerTransform;
            RemotePlayerAni = remotePlayerTransform.GetComponent<Animator>();
            return this;
        }
    }
}
