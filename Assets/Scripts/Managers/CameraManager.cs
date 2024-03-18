using Cameras;
using DG.Tweening;
using UnityEngine;

namespace Managers
{
    /// <summary>
    /// 摄像机管理器
    /// </summary>
    public class CameraManager : BaseManager
    {

        public CameraManager(GameFacade facade) : base(facade) { }
        //摄像机对象
        private GameObject cameraGO;
        //摄像机动画
        private Animator cameraAnim;
        //摄像机跟随目标类
        private FollowTarget followTarget;
        //摄像机位置
        private Vector3 originalPosition;
        //摄像机旋转
        private Vector3 originalRotation;
        public override void Init()
        {
            //获取主相机
            cameraGO = Camera.main.gameObject;
            //获取相机动画
            cameraAnim = cameraGO.GetComponent<Animator>();
            //获取相机跟随目标脚本
            followTarget = cameraGO.GetComponent<FollowTarget>();
            //获取相机位置
            originalPosition = cameraGO.transform.position;
            //获取相机旋转
            originalRotation = cameraGO.transform.eulerAngles;
            //默认开启动画，关闭跟随目标脚本
            WalkthroughScene();
            base.Init();
        }

        public override void Update()
        {

            base.Update();
        }
        /// <summary>
        /// 跟随目标方法
        /// </summary>
        public void FollowTarget()
        {
            //获取跟随目标transform
            followTarget.target = facade.GetCurrentGameObject().transform;
            //关闭相机动画
            cameraAnim.enabled = false;
            //设置跟随旋转
            Quaternion qTarget = Quaternion.LookRotation(followTarget.target.position - cameraGO.transform.position);
            //平滑切换目标跟随动画
            cameraGO.transform.DORotateQuaternion(qTarget, 1f).OnComplete(() =>
            {
                //开启目标跟随脚本
                followTarget.enabled = true;
            });
        }
        /// <summary>
        /// 开启相机动画
        /// </summary>
        public void WalkthroughScene()
        {
            //设置跟随目标脚本关闭
            followTarget.enabled = false;
            cameraGO.transform.DOMove(originalPosition, 1f);
            cameraGO.transform.DORotate(originalRotation, 1f).OnComplete(() =>
            {
                cameraAnim.enabled = true;
            });
        }

    }
}
