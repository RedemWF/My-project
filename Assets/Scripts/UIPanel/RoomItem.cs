using UnityEngine;
using UnityEngine.UI;

namespace UIPanel
{
    /// <summary>
    /// 房间面板
    /// </summary>
    public class RoomItem : MonoBehaviour
    {
        [SerializeField]
        private Text username;
        [SerializeField]
        private Text totalCont;
        [SerializeField]
        private Text winCount;
        [SerializeField]
        private Button joinBtn;

        private RoomListPanel panel;

        private int Id;

        private void Start()
        {
            joinBtn.onClick.AddListener(JoinBtnClick);
            panel = GetComponentInParent<RoomListPanel>();
        }
        public void SetRoomInfo(int id, string username, string totalCont, string winCount, RoomListPanel panel)
        {
            SetRoomInfo(id, username, int.Parse(totalCont), int.Parse(winCount), panel);
        }
        public void SetRoomInfo(int id, string username, int totalCont, int winCount, RoomListPanel panel)
        {
            Id = id;
            this.username.text = username;
            this.totalCont.text = $"总场次\n{totalCont.ToString()}";
            this.winCount.text = $"胜场\n{winCount.ToString()}";
            this.panel = panel;
        }
        private void JoinBtnClick()
        {
            panel.OnJoinClick(Id);
        }
        public void DestroySelf()
        {
            Destroy(gameObject);
        }
    }
}