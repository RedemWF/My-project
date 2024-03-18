using Common;
using Model;
using UIPanel;

namespace Request
{
    /// <summary>
    /// 更新房间请求
    /// </summary>
    public class UpdateRoomRequest : BaseRequest
    {
        private RoomPanel roomPanel;
        public override void Awake()
        {
            requestCode = RequestCode.Room;
            actionCode = ActionCode.UpdateRoom;
            roomPanel = GetComponent<RoomPanel>();
            base.Awake();
        }

        public override void OnResponse(string data)
        {
            UserData ud1 = null;
            UserData ud2 = null;
            string[] udStrs = data.Split("|");
            ud1 = new UserData(udStrs[0]);
            if (udStrs.Length > 1)
                ud2 = new UserData(udStrs[1]);
            roomPanel.SetLocalPlayerSync(ud1, ud2);
            base.OnResponse(data);

        }
    }
}
