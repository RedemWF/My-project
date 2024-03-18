using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

namespace Request
{
    /// <summary>
    /// 请求基类
    /// </summary>
    public class BaseRequest : MonoBehaviour
    {
        //请求代码
        protected RequestCode requestCode = RequestCode.None;
        //执行代码
        protected ActionCode actionCode = ActionCode.None;
        protected GameFacade _facade;

        // Use this for initialization
        public virtual void Awake()
        {
            _facade = GameFacade.Instance;
            _facade.AddRequest(actionCode, this);
        }

        /// <summary>
        /// 发送请求
        /// </summary>
        /// <param name="data">请求数据</param>
        protected void SendRequest(string data)
        {
            _facade.SendRequest(requestCode, actionCode, data);
        }

        public virtual void SendRequest() { }
        /// <summary>
        /// 获取响应
        /// </summary>
        /// <param name="data">相应数据</param>
        public virtual void OnResponse(string data) { }

        //删除请求
        public virtual void OnDestroy()
        {
            if (_facade != null)
                _facade.RemoveRequest(actionCode);
        }
    }
}