using Common;
using UnityEngine;

namespace Player
{
    /// <summary>
    /// 模型数据类
    /// </summary>
    public class RoleData
    {
        //类型
        public RoleType RoleType { get; private set; }
        //模型预制体
        public GameObject RolePrefab { get; private set; }
        //箭支预制体
        public GameObject ArrowPrefab { get; private set; }
        //出生位置
        public Vector3 SpawnPosition { get; private set; }
        //爆炸效果
        public GameObject ExplosionEffect { get; private set; }

        public RoleData(RoleType roleType, string rolePath, string arrowPath, string explosionPath, Transform spawnPosition)
        {
            RoleType = roleType;
            RolePrefab = Resources.Load<GameObject>("Prefab/" + rolePath);
            ArrowPrefab = Resources.Load<GameObject>("Prefab/" + arrowPath);
            ExplosionEffect = Resources.Load<GameObject>("Prefab/" + explosionPath);
            ArrowPrefab.GetComponent<Arrow>().explosionEffect = ExplosionEffect;
            SpawnPosition = spawnPosition.position;
        }
    }
}
