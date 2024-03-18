using Managers;
using UnityEngine;

namespace UIPanel
{
    /// <summary>
    /// 面板基类
    /// </summary>
    public class BasePanel : MonoBehaviour
    {
        //面板管理器
        protected UIManager uiMng;
        protected GameFacade gameFacade;
        public GameFacade GameFacade
        {
            set { gameFacade = value; }
        }

        public UIManager UIMng
        {
            set { uiMng = value; }
        }
        /// <summary>
        /// 界面被显示出来
        /// </summary>
        public virtual void OnEnter()
        {
        }

        /// <summary>
        /// 界面暂停
        /// </summary>
        public virtual void OnPause()
        {

        }

        /// <summary>
        /// 界面继续
        /// </summary>
        public virtual void OnResume()
        {

        }

        /// <summary>
        /// 界面不显示,退出这个界面，界面被关系
        /// </summary>
        public virtual void OnExit()
        {

        }
        protected void PlayClickAudio()
        {
            gameFacade.PlayNormalAudio(AudioNames.ButtionClick);
        }
    }
}


