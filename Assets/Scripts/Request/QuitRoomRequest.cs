using Common;
using UIPanel;

namespace Request
{
    /// <summary>
    /// 推出房间请求
    /// </summary>
    public class QuitRoomRequest : BaseRequest
    {
        //房间面板
        private RoomPanel _roomPanel;
        public override void Awake()
        {
            requestCode = RequestCode.Room;
            actionCode = ActionCode.QuitRoom;
            _roomPanel = GetComponent<RoomPanel>();
            base.Awake();
        }

        public override void SendRequest()
        {
            base.SendRequest("r");
        }

        public override void OnResponse(string data)
        {
            ReturnCode code = (ReturnCode)int.Parse(data);
            if (code is ReturnCode.Success)
            {
                _roomPanel.OnExitResponse();
            }
            base.OnResponse(data);
        }
    }
}
