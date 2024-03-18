using UnityEngine;

namespace Managers
{
    /// <summary>
    /// 音频路径配置
    /// </summary>
    public class AudioNames
    {
        public const string Alert = "Alert";
        public const string ArrowShoot = "ArrowShoot";
        public const string Bg_Fast = "Bg(fast)";
        public const string Bg_Moderate = "Bg(moderate)";
        public const string ButtionClick = "ButtonClick";
        public const string Miss = "Miss";
        public const string ShootPerson = "ShootPerson";
        public const string Timer = "Timer";
    }
    /// <summary>
    /// 音频管理器
    /// </summary>
    public class AudioManager : BaseManager
    {
        public AudioManager(GameFacade facade) : base(facade)
        {
        }
        private const string Path_Prefix = "Sounds/";
        //背景音频
        private AudioSource bgAudioSouce;
        //普通音频
        private AudioSource normalAudioSouce;
        public override void Init()
        {
            base.Init();
            //创建音频游戏对象
            GameObject audioSource = new GameObject("AudioSource");
            //为音频对象添加脚本
            bgAudioSouce = audioSource.AddComponent<AudioSource>();
            normalAudioSouce = audioSource.AddComponent<AudioSource>();
            //添加背景音乐
            AudioClip bg_ModerateClip = LoadSound(AudioNames.Bg_Moderate);
            //播放背景音乐
            PlaySound(bgAudioSouce, bg_ModerateClip, .5f, true);
        }
        /// <summary>
        /// 播放背景音乐方法
        /// </summary>
        /// <param name="audioName">音频名称@AudioNames.clss</param>
        public void PlayBgAudio(string audioName)
        {
            PlaySound(bgAudioSouce, LoadSound(audioName), .5f, true);
        }
        /// <summary>
        /// 播放普通音效方法
        /// </summary>
        /// <param name="audioName">音频名称@AudioNames.clss</param>
        public void PlayNormalAudio(string audioName)
        {
            PlaySound(normalAudioSouce, LoadSound(audioName), 1f);
        }
        /// <summary>
        /// 播放音频
        /// </summary>
        /// <param name="audioSource">音频源</param>
        /// <param name="audioClip"></param>
        /// <param name="audioVolume">音频音量</param>
        /// <param name="isLoop">是否循环</param>
        private void PlaySound(AudioSource audioSource, AudioClip audioClip, float audioVolume, bool isLoop = false)
        {
            audioSource.volume = audioVolume;
            audioSource.clip = audioClip;
            audioSource.loop = isLoop;
            audioSource.Play();
        }
        /// <summary>
        /// 加载音频
        /// </summary>
        /// <param name="soundsName">音频名</param>
        /// <returns></returns>
        private AudioClip LoadSound(string soundsName)
        {
            return Resources.Load<AudioClip>(Path_Prefix + soundsName);
        }
    }
}
