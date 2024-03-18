using System.Collections.Generic;
using Common;
using Request;
using UnityEngine;

namespace Managers
{
    /// <summary>
    /// 请求管理器
    /// </summary>
    public class RequestManager : BaseManager
    {

        public RequestManager(GameFacade facade) : base(facade) { }
        //请求字典
        private Dictionary<ActionCode, BaseRequest> requestDict = new Dictionary<ActionCode, BaseRequest>();
        /// <summary>
        /// 字典中添加请求
        /// </summary>
        /// <param name="actionCode">ActionCode</param>
        /// <param name="request">请求</param>
        public void AddRequest(ActionCode actionCode, BaseRequest request)
        {
            requestDict.Add(actionCode, request);
        }
        /// <summary>
        /// 字典中删除请求
        /// </summary>
        /// <param name="actionCode">ActionCode</param>
        public void RemoveRequest(ActionCode actionCode)
        {
            requestDict.Remove(actionCode);
        }
        /// <summary>
        /// 获得响应
        /// </summary>
        /// <param name="actionCode"></param>
        /// <param name="data">数据</param>
        public void HandleReponse(ActionCode actionCode, string data)
        {
            //根据actionCode获取请求
            BaseRequest request = requestDict.TryGet(actionCode);
            if (request == null)
            {
                Debug.LogWarning("无法得到ActionCode[" + actionCode + "]对应的Request类"); return;
            }
            //获取响应
            request.OnResponse(data);
        }
    }
}
