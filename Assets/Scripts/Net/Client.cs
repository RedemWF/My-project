using System;
using System.Net.Sockets;
using Common;
using Managers;
using UnityEngine;

namespace Net
{
    /// <summary>
    /// 客户端管理器
    /// </summary>
    public class Client : BaseManager
    {
        //ip
        private const string IP = "127.0.0.1";
        //端口
        private const int PORT = 6688;

        //客户端Socket
        private Socket clientSocket;
        //消息类
        private Message msg = new Message();
        public Client(GameFacade facade) : base(facade)
        {
        }

        public override void Init()
        {
            base.Init();
            //创建Socket
            clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                //连接服务器
                clientSocket.Connect(IP, PORT);
                //开启连接
                Start();
            }
            catch (Exception e)
            {
                Debug.LogWarning("无法连接到服务器端，请检查您的网络！！" + e);
            }
        }
        /// <summary>
        /// 开启异步连接对话
        /// </summary>
        private void Start()
        {
            clientSocket.BeginReceive(msg.Data, msg.StartIndex, msg.RemainSize, SocketFlags.None, ReceiveCallback, null);
        }
        /// <summary>
        /// 对话回调
        /// </summary>
        /// <param name="ar">IAsyncResult</param>
        private void ReceiveCallback(IAsyncResult ar)
        {
            try
            {
                //判断Socket是否为空
                if (clientSocket == null || clientSocket.Connected == false) return;
                //获取消息数据
                int count = clientSocket.EndReceive(ar);
                //异步读取数据
                msg.ReadMessage(count, OnProcessDataCallback);
                //重新开启对话
                Start();
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
        }
        /// <summary>
        /// 异步获取数据回调
        /// </summary>
        /// <param name="actionCode"></param>
        /// <param name="data"></param>
        private void OnProcessDataCallback(ActionCode actionCode, string data)
        {
            facade.HandleReponse(actionCode, data);
        }
        /// <summary>
        /// 发送请求
        /// </summary>
        /// <param name="requestCode">请求代码</param>
        /// <param name="actionCode">执行代码</param>
        /// <param name="data">数据</param>
        public void SendRequest(RequestCode requestCode, ActionCode actionCode, string data)
        {
            byte[] bytes = Message.PackData(requestCode, actionCode, data);
            clientSocket.Send(bytes);
        }
        /// <summary>
        /// 关闭连接
        /// </summary>
        public override void Destroy()
        {
            base.Destroy();
            try
            {
                clientSocket.Close();
            }
            catch (Exception e)
            {
                Debug.LogWarning("无法关闭跟服务器端的连接！！" + e);
            }
        }
    }
}
