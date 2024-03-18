using Common;
using Request;
using UnityEngine;
using UnityEngine.UI;

namespace UIPanel
{
    /// <summary>
    /// 注册面板
    /// </summary>
    public class RegisterPanel : BasePanel
    {
        [SerializeField]
        private InputField usernameIf;
        [SerializeField]
        private InputField passwordIf;
        [SerializeField]
        private InputField rePasswordIf;
        [SerializeField]
        private Button registerBtn;
        [SerializeField]
        private Button closeBtn;
        private RegisterRequest registerRequest;

        private void Start()
        {

        }
        public override void OnEnter()
        {
            registerRequest = GetComponent<RegisterRequest>();
            gameObject.SetActive(true);
            registerBtn.onClick.AddListener(OnRegisterClick);
            closeBtn.onClick.AddListener(OnCloseClick);
        }
        private void OnRegisterClick()
        {
            PlayClickAudio();
            string msg = "";
            if (string.IsNullOrEmpty(usernameIf.text))
            {
                msg = "\n 用户名不能为空！";
            }
            if (string.IsNullOrEmpty(passwordIf.text))
            {
                msg = "\n 密码不能为空！";
            }
            if (string.IsNullOrEmpty(rePasswordIf.text))
            {
                msg = "\n 重复密码不能为空！";
            }
            if (rePasswordIf.text != passwordIf.text)
            {
                msg = "\n 密码与重复密码不一致！";
            }
            if (msg is not "")
            {
                uiMng.ShowMessage(msg);
            }
            else
            {
                registerRequest.SendRequest(usernameIf.text, passwordIf.text);
            }

        }
        private void OnCloseClick()
        {
            PlayClickAudio();
            uiMng.PopPanel();
        }
        public override void OnExit()
        {
            gameObject.SetActive(false);
        }
        public void OnRegisterResponse(ReturnCode returnCode)
        {
            if (returnCode == ReturnCode.Success)
            {
                uiMng.ShowMessageSync("注册成功！");
            }
            else
            {
                uiMng.ShowMessageSync("注册失败！");
            }
        }

    }
}
