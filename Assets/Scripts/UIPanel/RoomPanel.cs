using Common;
using DG.Tweening;
using Model;
using Request;
using UnityEngine;
using UnityEngine.UI;

namespace UIPanel
{
    /// <summary>
    /// 房间显示面板
    /// </summary>
    public class RoomPanel : BasePanel
    {
        [SerializeField]
        private Text bluePlayerUsername;
        [SerializeField]
        private Text bluePlayerTotalCount;
        [SerializeField]
        private Text bluePlayerWinCount;
        [SerializeField]
        private Text redPlayerUsername;
        [SerializeField]
        private Text redPlayerTotalCount;
        [SerializeField]
        private Text redPlayerWinCount;
        [SerializeField]
        private Button beginBtn;
        [SerializeField]
        private Button exitBtn;
        [SerializeField]
        private RectTransform bluePanel;
        [SerializeField]
        private RectTransform redPanel;
        private UserData userData;
        private UserData ud1;
        private UserData ud2;
        private QuitRoomRequest quitRoomRequest;
        private StartGameRequest startGameRequest;
        private bool IsPopPanel = false;
        private void Start()
        {
            quitRoomRequest = GetComponent<QuitRoomRequest>();
            startGameRequest = GetComponent<StartGameRequest>();
            beginBtn.onClick.AddListener(OnBeginClick);
            exitBtn.onClick.AddListener(OnExitClick);

        }
        public override void OnEnter()
        {
            base.OnEnter();
            EnterAnim();
        }
        public override void OnExit()
        {
            base.OnExit();
            ExitAnim();
        }
        public override void OnPause()
        {
            base.OnPause();
            ExitAnim();
        }
        public override void OnResume()
        {
            base.OnResume();
            EnterAnim();
        }
        private void Update()
        {
            if (userData != null)
            {
                SetBluePlayerRes(userData.UserName, userData.TotalCount.ToString(), userData.WinCount.ToString());
                ClearPlayerRes("red");
                userData = null;
            }

            if (ud1 != null)
            {
                SetBluePlayerRes(ud1.UserName, ud1.TotalCount.ToString(), ud1.WinCount.ToString());
                if (ud2 != null)
                {
                    SetRedPlayerRes(ud2.UserName, ud2.TotalCount.ToString(), ud2.WinCount.ToString());
                }
                else
                {
                    ClearPlayerRes("red");
                }
                ud1 = null;
                ud2 = null;
            }

            if (IsPopPanel)
            {
                uiMng.PopPanel();
                IsPopPanel = false;
            }
        }
        public void SetBluePlayerResSync()
        {
            userData = gameFacade.GetUserData();
        }

        public void SetLocalPlayerSync(UserData ud1, UserData ud2)
        {
            this.ud1 = ud1;
            this.ud2 = ud2;
        }
        public void SetRedPlayerResSync()
        {
            //enemyUserData = gameFacade.GetUserData();
        }
        public void ClearPlayerResSync(string color)
        {

        }
        public void ClearPlayerResSync()
        {

        }
        public void SetBluePlayerRes(string username, string totalCount, string winCount)
        {
            bluePlayerUsername.text = username;
            bluePlayerTotalCount.text = $"总场数：{totalCount}";
            bluePlayerWinCount.text = $"胜场数：{winCount}";
        }
        public void SetRedPlayerRes(string username, string totalCount, string winCount)
        {
            redPlayerUsername.text = username;
            redPlayerTotalCount.text = $"总场数：{totalCount}";
            redPlayerWinCount.text = $"胜场数：{winCount}";
        }
        public void ClearPlayerRes()
        {
            bluePlayerUsername.text = "";
            bluePlayerTotalCount.text = $"";
            bluePlayerWinCount.text = $"";
            redPlayerUsername.text = "";
            redPlayerTotalCount.text = $"";
            redPlayerWinCount.text = $"";
        }
        public void ClearPlayerRes(string color)
        {
            switch (color)
            {
                case "red":
                    redPlayerUsername.text = "";
                    redPlayerTotalCount.text = $"等待玩家加入...";
                    redPlayerWinCount.text = $"";
                    break;
                case "blue":
                    bluePlayerUsername.text = "";
                    bluePlayerTotalCount.text = $"等待玩家加入...";
                    bluePlayerWinCount.text = $"";
                    break;
                default:
                    throw new System.Exception("color not find!");
            }

        }

        public void OnExitResponse()
        {
            uiMng.PopPanel();
        }
        private void OnBeginClick()
        {
            startGameRequest.SendRequest();
        }

        public void OnStartResponse(ReturnCode returnCode)
        {
            if (returnCode == ReturnCode.Fail)
            {
                uiMng.ShowMessageSync("您不是房主，无法开始游戏");
            }
            else
            {
                uiMng.PushPanelSync(UIPanelType.Game);
                gameFacade.EnterPlayingSync();
            }
        }
        private void OnExitClick()
        {
            quitRoomRequest.SendRequest();
        }
        private void EnterAnim()
        {
            gameObject.SetActive(true);
            bluePanel.localPosition = new Vector3(-1000, 0, 0);
            bluePanel.DOLocalMoveX(-350, .2f);
            redPanel.localPosition = new Vector3(1000, 0, 0);
            redPanel.DOLocalMoveX(420, .2f);
            RectTransform beginBtnTrans = beginBtn.GetComponent<RectTransform>();
            RectTransform exitBtnTrans = exitBtn.GetComponent<RectTransform>();
            beginBtnTrans.localPosition = new Vector3(0, 1000, 0);
            beginBtnTrans.DOLocalMoveY(144, .5f);
            exitBtnTrans.localPosition = new Vector3(0, -1000, 0);
            exitBtnTrans.DOLocalMoveY(-144, .5f);
        }
        private void ExitAnim()
        {
            bluePanel.DOLocalMoveX(-1000, .2f);
            redPanel.DOLocalMoveX(1000, .2f);
            RectTransform beginBtnTrans = beginBtn.GetComponent<RectTransform>();
            RectTransform exitBtnTrans = exitBtn.GetComponent<RectTransform>();
            beginBtnTrans.DOLocalMoveY(1000, .2f);
            exitBtnTrans.DOLocalMoveY(-1000, .2f).OnComplete(() =>
            {
                gameObject.SetActive(false);
            });
        }
    }
}
