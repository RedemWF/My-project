using System.Collections;
using System.Collections.Generic;
using Common;
using UIPanel;
using UnityEngine;

namespace Request
{
    /// <summary>
    /// 注册请求
    /// </summary>
    public class RegisterRequest : BaseRequest
    {
        private RegisterPanel registerPanel;
        public override void Awake()
        {
            requestCode = RequestCode.User;
            actionCode = ActionCode.Register;
            registerPanel = GetComponent<RegisterPanel>();
            base.Awake();
        }
        public void SendRequest(string username, string password)
        {
            string data = username + "," + password;
            SendRequest(data);
        }
        public override void OnResponse(string data)
        {
            base.OnResponse(data);
            ReturnCode returnCode = ((ReturnCode)int.Parse(data));
            registerPanel.OnRegisterResponse(returnCode);
        }
    }
}
