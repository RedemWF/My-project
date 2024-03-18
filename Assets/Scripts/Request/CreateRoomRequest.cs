using System.Collections;
using System.Collections.Generic;
using Common;
using Model;
using UIPanel;

namespace Request
{
    /// <summary>
    /// 创建房间请求
    /// </summary>
    public class CreateRoomRequest : BaseRequest
    {
        //房间面板
        private RoomPanel roomPanel;
        /// <summary>
        /// 设置房间面板
        /// </summary>
        /// <param name="panel">面板UI</param>
        public void SetRoomPanel(BasePanel panel)
        {
            roomPanel = panel as RoomPanel;
        }
        public override void Awake()
        {
            requestCode = RequestCode.Room;
            actionCode = ActionCode.CreateRoom;
            roomPanel = GetComponent<RoomPanel>();
            base.Awake();
        }
        public override void SendRequest()
        {
            base.SendRequest("r");
        }
        public override void OnResponse(string data)
        {
            string[] strs = data.Split(",");
            ReturnCode returnCode = (ReturnCode)int.Parse(strs[0]);
            //设置玩家类型
            RoleType roleType = (RoleType)int.Parse(strs[1]);
            _facade.SetCurrentRoleType(roleType);
            if (returnCode == ReturnCode.Success)
            {
                roomPanel.SetBluePlayerResSync();
            }
            base.OnResponse(data);
        }
    }
}
