using UnityEngine;
using UnityEngine.UI;

namespace UIPanel
{
    /// <summary>
    /// 消息面板
    /// </summary>
    public class MessagePanel : BasePanel
    {
        private Text text;
        private float showTime = 1;
        private string message = null;
        public override void OnEnter()
        {
            base.OnEnter();
            text = GetComponent<Text>();
            text.enabled = false;
            uiMng.InjectMsgPanel(this);
        }
        private void Update()
        {
            if (!string.IsNullOrEmpty(message))
            {
                ShowMessage(message);
                message = null;
            }
        }
        /// <summary>
        /// 异步显示消息
        /// </summary>
        /// <param name="msg"></param>
        public void ShowMessageSync(string msg)
        {
            message = msg;
        }
        public void ShowMessage(string msg)
        {
            text.CrossFadeAlpha(1, .2f, false);
            text.color = Color.white;
            text.text = msg;
            text.enabled = true;
            Invoke(nameof(Hide), showTime);
        }
        /// <summary>
        /// 隐藏面板
        /// </summary>
        public void Hide()
        {
            text.CrossFadeAlpha(0, 1, false);
        }
    }
}
