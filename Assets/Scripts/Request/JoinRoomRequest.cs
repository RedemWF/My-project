using Common;
using Model;
using UIPanel;

namespace Request
{
    /// <summary>
    /// 加入房间请求
    /// </summary>
    public class JoinRoomRequest : BaseRequest
    {
        //房间列表面板
        private RoomListPanel roomListPanel;
        public override void Awake()
        {
            requestCode = RequestCode.Room;
            actionCode = ActionCode.JoinRoom;
            roomListPanel = GetComponent<RoomListPanel>();
            base.Awake();
        }

        public void JoinRoom(int id)
        {
            base.SendRequest(id.ToString());
        }


        public override void OnResponse(string data)
        {
            string[] strs = data.Split('-');
            string[] strs2 = strs[0].Split(',');
            ReturnCode returnCode = (ReturnCode)int.Parse(strs2[0]);
            UserData ud = null;
            UserData ud1 = null;
            //判断是否创建成功
            if (returnCode == ReturnCode.Success)
            {

                RoleType roleType = (RoleType)int.Parse(strs2[1]);
                string[] udStrs = strs[1].Split('|');
                //设置用户数据
                ud = new UserData(udStrs[0]);
                ud1 = new UserData(udStrs[1]);
                //设置本地玩家类型
                _facade.SetCurrentRoleType(roleType);
            }
            roomListPanel.OnJoinResponse(returnCode, ud, ud1);
        }
    }
}
