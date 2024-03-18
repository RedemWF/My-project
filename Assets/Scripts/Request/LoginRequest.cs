using Common;
using Model;
using UIPanel;

namespace Request
{
    /// <summary>
    /// 登陆请求
    /// </summary>
    public class LoginRequest : BaseRequest
    {
        //登陆面板
        private LoginPanel loginPanel;
        public override void Awake()
        {
            requestCode = RequestCode.User;
            actionCode = ActionCode.Login;
            loginPanel = GetComponent<LoginPanel>();
            base.Awake();
        }
        /// <summary>
        /// 发送请求
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="password">密码</param>
        public void SendRequest(string username, string password)
        {
            string data = username + "," + password;
            SendRequest(data);

        }
        /// <summary>
        /// 获取响应
        /// </summary>
        /// <param name="data">相应数据</param>
        public override void OnResponse(string data)
        {
            string[] strs = data.Split(',');
            ReturnCode code = (ReturnCode)int.Parse(strs[0]);
            if (code == ReturnCode.Success)
            {
                string username = strs[1];
                int totalCount = int.Parse(strs[2]);
                int winCount = int.Parse(strs[3]);
                UserData user = new UserData(username, totalCount, winCount);
                _facade.SetUserData(user);
            }
            loginPanel.OnLoginResponse(code);
            base.OnResponse(data);
        }

    }
}