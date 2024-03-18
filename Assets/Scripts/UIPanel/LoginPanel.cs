using Common;
using DG.Tweening;
using Request;
using UnityEngine;
using UnityEngine.UI;

namespace UIPanel
{
    /// <summary>
    /// 登陆面板
    /// </summary>
    public class LoginPanel : BasePanel
    {
        //关闭按钮
        private Button closeBtn;

        //用户名输入框
        [SerializeField]
        private InputField usernameInput;

        //密码输入框
        [SerializeField]
        private InputField passwordInput;

        //登陆按钮
        [SerializeField]
        private Button loginBtn;
        //注册按钮
        [SerializeField]
        private Button registerBtn;
        //登陆请求
        private LoginRequest loginRequest;

        private void Start()
        {
            loginRequest = GetComponent<LoginRequest>();
            closeBtn = transform.Find("CloseBtn").GetComponent<Button>();
            //添加按钮监听
            closeBtn.onClick.AddListener(OnCloseClick);
            loginBtn.onClick.AddListener(OnLoginClick);
            registerBtn.onClick.AddListener(OnRegisterClick);
        }
        public override void OnEnter()
        {
            base.OnEnter();
            gameObject.SetActive(true);
            OnEnterAnim();

        }
        //面板进入动画
        private void OnEnterAnim()
        {

            transform.localScale = Vector3.zero;
            transform.DOScale(1, .2f);
            transform.localPosition = new Vector3(1000, 0, 0);
            transform.DOLocalMove(Vector3.zero, .2f);
        }
        /// <summary>
        /// 关闭点击事件
        /// </summary>
        private void OnCloseClick()
        {
            //播放按钮音效
            PlayClickAudio();
            //动画
            transform.DOScale(0, .2f);
            var tweenerCore = transform.DOLocalMove(new Vector3(1000, 0, 0), .2f);
            tweenerCore.onComplete += () =>
            {
                //在栈中弹出面板
                uiMng.PopPanel();
            };
        }
        //登陆按钮点击
        private void OnLoginClick()
        {
            PlayClickAudio();
            string msg = "";
            if (string.IsNullOrEmpty(usernameInput.text)) msg = "用户名不能为空！";
            if (string.IsNullOrEmpty(passwordInput.text)) msg = "密码不能为空！";
            if (msg != "")
            {
                uiMng.ShowMessage(msg);
            }
            else
            {
                loginRequest.SendRequest(usernameInput.text, passwordInput.text);
            }

        }
        //注册按钮点击
        private void OnRegisterClick()
        {
            PlayClickAudio();
            uiMng.PushPanel(UIPanelType.Register);
        }
        //登陆响应
        public void OnLoginResponse(ReturnCode returnCode)
        {
            if (returnCode == ReturnCode.Success)
            {
                uiMng.PushPanelSync(UIPanelType.RoomList);

            }
            else
            {
                uiMng.ShowMessageSync("用户名或密码不正确，请重新输入！");
            }
        }

        public override void OnResume()
        {
            base.OnResume();
            gameObject.SetActive(true);
            OnEnterAnim();
        }
        public override void OnPause()
        {
            base.OnPause();
            transform.DOScale(0, .2f);
            var tweenerCore = transform.DOLocalMove(new Vector3(1000, 0, 0), .2f);
            tweenerCore.onComplete += () =>
            {
                gameObject.SetActive(false);
            };
        }
        public override void OnExit()
        {
            base.OnExit();
            gameObject.SetActive(false);
        }

    }
}
