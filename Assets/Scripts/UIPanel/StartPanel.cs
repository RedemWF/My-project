using DG.Tweening;
using UnityEngine.UI;

namespace UIPanel
{
    /// <summary>
    /// 开始游戏面板
    /// </summary>
    public class StartPanel : BasePanel
    {

        private Button loginBtn;
        public override void OnEnter()
        {
            base.OnEnter();

            loginBtn = gameObject.GetComponent<Button>();
            loginBtn.onClick.AddListener(OnLoginClick);
        }

        public void OnLoginClick()
        {
            uiMng.PushPanel(UIPanelType.Login);
        }
        public override void OnPause()
        {
            base.OnPause();
            loginBtn.transform.DOScale(0, .2f).OnComplete(() => loginBtn.gameObject.SetActive(false));
        }
        public override void OnResume()
        {
            base.OnResume();
            loginBtn.gameObject.SetActive(true);
            loginBtn.transform.DOScale(1, .2f);
        }
    }
}
