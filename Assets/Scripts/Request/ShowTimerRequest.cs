using System.Collections;
using System.Collections.Generic;
using Common;
using Request;
using UIPanel;
using UnityEngine;

namespace Request
{
    /// <summary>
    /// 倒计时请求
    /// </summary>
    public class ShowTimerRequest : BaseRequest
    {
        private GamePanel _gamePanel;
        public override void Awake()
        {
            _gamePanel = GetComponent<GamePanel>();
            actionCode = ActionCode.ShowTimer;
            base.Awake();
        }


        public override void OnResponse(string data)
        {
            int time = int.Parse(data);
            _gamePanel.ShowTimeSync(time);
            base.OnResponse(data);
        }
    }
}

