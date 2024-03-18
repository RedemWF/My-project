using System.Collections.Generic;
using Common;
using DG.Tweening;
using Model;
using Request;
using UnityEngine;
using UnityEngine.UI;

namespace UIPanel
{
    /// <summary>
    /// 房间列表面板
    /// </summary>
    public class RoomListPanel : BasePanel
    {
        [SerializeField]
        private RectTransform battleRes;
        [SerializeField]
        private RectTransform roomList;
        [SerializeField]
        private Button closeBtn;

        [SerializeField]
        private Text username;
        [SerializeField]
        private Text totalCount;
        [SerializeField]
        private Text winCount;

        [SerializeField]
        private RectTransform roomLayout;
        [SerializeField]
        private Scrollbar scrollbar;
        [SerializeField]
        private Button creatRoomBtn;
        [SerializeField]
        private Button freshBtn;

        private GameObject roomItemPrefab;
        private ListRoomRequest roomRequest;
        private List<UserData> udList = null;
        private CreateRoomRequest createRoomRequest;
        private JoinRoomRequest joinRoomRequest;
        private UserData ud1 = null;
        private UserData ud2 = null;

        public override void OnEnter()
        {
            EnterAnim();
            SetBattleRes();
            if (roomRequest == null)
            {
                roomRequest = GetComponent<ListRoomRequest>();
            }
            roomRequest.SendRequest();
        }
        private void Start()
        {
            closeBtn.onClick.AddListener(CloseBtnClick);
            roomItemPrefab = Resources.Load("Prefab/RoomItem") as GameObject;
            creatRoomBtn.onClick.AddListener(CreatRoomClick);
            freshBtn.onClick.AddListener(FreshClick);
            createRoomRequest = GetComponent<CreateRoomRequest>();
            roomRequest = GetComponent<ListRoomRequest>();
            joinRoomRequest = GetComponent<JoinRoomRequest>();
        }
        private void Update()
        {
            if (udList != null)
            {
                LoadRoomItem(udList);
                udList = null;
            }

            if (ud1 != null && ud2 != null)
            {
                var roomPanel = uiMng.PushPanel(UIPanelType.Room);
                (roomPanel as RoomPanel).SetLocalPlayerSync(ud1, ud2);
                ud1 = null;
                ud2 = null;
            }
        }
        private void FreshClick()
        {
            roomRequest.SendRequest();
        }

        private void CreatRoomClick()
        {
            var pushPanel = uiMng.PushPanel(UIPanelType.Room);
            createRoomRequest.SetRoomPanel(pushPanel);
            createRoomRequest.SendRequest();
        }

        public override void OnPause()
        {
            base.OnPause();
            HideAnim();
        }
        public override void OnResume()
        {
            base.OnResume();
            EnterAnim();
            roomRequest.SendRequest();
        }

        private void EnterAnim()
        {

            gameObject.SetActive(true);
            battleRes.localPosition = new Vector3(-1000, 0);
            battleRes.DOLocalMoveX(-468, .5f);
            roomList.localPosition = new Vector3(1000, 0);
            roomList.DOLocalMoveX(281, .5f);
        }
        private void CloseBtnClick()
        {
            uiMng.PopPanel();
            HideAnim();
        }
        public override void OnExit()
        {
            base.OnExit();
            HideAnim();
        }
        private void HideAnim()
        {
            // battleRes.localPosition = new Vector3(-468, 0);
            battleRes.DOLocalMoveX(-1000, .5f);
            // roomList.localPosition = new Vector3(281, 0);
            roomList.DOLocalMoveX(1000, .5f).OnComplete(() => gameObject.SetActive(false));

        }
        private void SetBattleRes()
        {
            UserData ud = gameFacade.GetUserData();
            username.text = ud.UserName;
            totalCount.text = $"总场次：{ud.TotalCount.ToString()}";
            winCount.text = $"胜利：{ud.WinCount.ToString()}";

        }
        public void LoadRoomItemSync(List<UserData> udList)
        {
            this.udList = udList;
        }
        private void LoadRoomItem(List<UserData> udList)
        {
            RoomItem[] roomItems = roomLayout.GetComponentsInChildren<RoomItem>();
            foreach (var item in roomItems)
            {
                item.DestroySelf();
            }
            int count = udList.Count;
            for (int i = 0; i < count; i++)
            {
                scrollbar.value = 1;
                Vector3 sizeDelta = roomLayout.sizeDelta;
                sizeDelta.y = 572;
                GameObject roomItem = Instantiate(roomItemPrefab, roomLayout);
                UserData ud = udList[i];
                roomItem.GetComponent<RoomItem>().SetRoomInfo(ud.UserId, ud.UserName, ud.TotalCount, ud.WinCount, this);
                int length = roomLayout.GetComponentsInChildren<RoomItem>().Length;
                float height = roomItem.GetComponent<RectTransform>().rect.height;
                float totalChildHeight = (height + 10) * length;
                if (totalChildHeight > 572)
                {
                    sizeDelta.y = totalChildHeight;
                }

                roomLayout.sizeDelta = sizeDelta;
            }


        }

        public void OnJoinClick(int id)
        {
            joinRoomRequest.JoinRoom(id);
        }

        public void OnJoinResponse(ReturnCode returnCode, UserData ud1, UserData ud2)
        {
            switch (returnCode)
            {
                case ReturnCode.NotFound:
                    uiMng.ShowMessageSync("房间已销毁，无法加入");
                    break;
                case ReturnCode.Fail:
                    uiMng.ShowMessageSync("房间已满员，无法加入");
                    break;
                case ReturnCode.Success:
                    this.ud1 = ud1;
                    this.ud2 = ud2;
                    break;
            }
        }
        // private void Update()
        // {
        //     if (Input.GetMouseButtonDown(0))
        //     {
        //         LoadRoomItem(1);
        //     }
        // }
    }
}
