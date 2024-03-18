using Common;
using DG.Tweening;
using Managers;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace UIPanel
{
    /// <summary>
    /// 游戏面板
    /// </summary>
    public class GamePanel : BasePanel
    {
        [SerializeField]
        private Text timer;//倒计时文本
        [SerializeField]
        private Button winBtn;//胜利面板
        [SerializeField]
        private Button loseBtn;//失败面板

        private int time = -1;

        private void Start()
        {
            winBtn.gameObject.SetActive(false);
            loseBtn.gameObject.SetActive(false);
        }
        private void Update()
        {
            if (time > -1)
            {
                ShowTime(time);
                time = -1;
            }
        }
        /// <summary>
        /// 异步显示倒计时
        /// </summary>
        /// <param name="time">时间</param>
        public void ShowTimeSync(int time)
        {
            this.time = time;
        }
        /// <summary>
        /// 显示倒计时
        /// </summary>
        /// <param name="time"></param>
        public void ShowTime(int time)
        {
            timer.gameObject.SetActive(true);
            timer.text = time.ToString();
            timer.transform.localScale = Vector3.one;
            Color tempColor = timer.color;
            tempColor.a = 1;
            timer.color = tempColor;
            timer.transform.DOScale(2, .3f).SetDelay(0.3f);
            timer.DOFade(0, .3f).SetDelay(0.3f).OnComplete(() => timer.gameObject.SetActive(false));
            gameFacade.PlayNormalAudio(AudioNames.Alert);
        }
        /// <summary>
        /// 游戏结束面板
        /// </summary>
        /// <param name="returnCode"></param>
        public void OnGameOver(ReturnCode returnCode)
        {
            Button tmpBtn = null;
            switch (returnCode)
            {
                case ReturnCode.Success://胜利
                    tmpBtn = winBtn;
                    break;
                case ReturnCode.Fail://失败
                    tmpBtn = loseBtn;
                    break;
            }
            tmpBtn.gameObject.SetActive(true);
            tmpBtn.transform.localScale = Vector3.zero;
            tmpBtn.transform.DOScale(1, .5f);
        }
    }
}
