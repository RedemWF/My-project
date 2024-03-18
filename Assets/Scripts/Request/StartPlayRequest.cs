using Common;

namespace Request
{
    /// <summary>
    /// 开始游玩请求
    /// </summary>
    public class StartPlayRequest : BaseRequest
    {
        private bool isStartPlaying = false;
        public override void Awake()
        {
            // requestCode = RequestCode.Game;
            actionCode = ActionCode.StartPlay;
            base.Awake();
        }

        private void Update()
        {
            if (isStartPlaying)
            {
                _facade.StartPlaying();
                isStartPlaying = false;
            }
        }

        public override void OnResponse(string data)
        {
            isStartPlaying = true;
            base.OnResponse(data);
        }
    }
}
