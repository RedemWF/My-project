
using Common;
using UIPanel;

namespace Request
{
    //游戏结束请求
    public class GameOverRequest : BaseRequest
    {
        //结束面板
        private GamePanel gamePanel;
        private bool isGameOver = false;
        private ReturnCode returnCode;
        public override void Awake()
        {
            requestCode = RequestCode.Game;
            actionCode = ActionCode.GameOver;
            gamePanel = GetComponent<GamePanel>();
            base.Awake();
        }
        private void Update()
        {
            if (isGameOver)
            {
                gamePanel.OnGameOver(returnCode);
                isGameOver = false;
            }
        }
        public override void OnResponse(string data)
        {
            returnCode = (ReturnCode)int.Parse(data);
            isGameOver = true;
            base.OnResponse(data);
        }
    }
}