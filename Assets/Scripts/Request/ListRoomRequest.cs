using System.Collections;
using System.Collections.Generic;
using Common;
using Model;
using Request;
using UIPanel;
using UnityEngine;

namespace Request
{
    /// <summary>
    /// 房间列表请求
    /// </summary>
    public class ListRoomRequest : BaseRequest
    {
        //房间列表面板
        private RoomListPanel listPanel;
        public override void Awake()
        {
            requestCode = RequestCode.Room;
            actionCode = ActionCode.ListRoom;
            listPanel = GetComponent<RoomListPanel>();
            base.Awake();
        }
        public override void SendRequest()
        {
            base.SendRequest("r");

        }
        public override void OnResponse(string data)
        {
            List<UserData> udList = new List<UserData>();
            if (data != "0")
            {
                string[] udArray = data.Split("|");
                foreach (var ud in udArray)
                {
                    string[] strings = ud.Split(",");
                    udList.Add(new UserData(int.Parse(strings[0]), strings[1], int.Parse(strings[2]), int.Parse(strings[3])));
                }
            }
            //异步加载房间
            listPanel.LoadRoomItemSync(udList);
            base.OnResponse(data);
        }
    }

}