using Common;
using UIPanel;

namespace Request
{
    /// <summary>
    /// 开始游戏请求
    /// </summary>
    public class StartGameRequest : BaseRequest
    {
        private RoomPanel _roomPanel;
        public override void Awake()
        {
            requestCode = RequestCode.Game;
            actionCode = ActionCode.StartGame;
            _roomPanel = GetComponent<RoomPanel>();
            base.Awake();
        }

        public override void SendRequest()
        {
            base.SendRequest("r");
        }

        public override void OnResponse(string data)
        {
            ReturnCode returnCode = (ReturnCode)int.Parse(data);
            _roomPanel.OnStartResponse(returnCode);
            base.OnResponse(data);
        }
    }
}
