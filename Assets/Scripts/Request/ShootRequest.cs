using System.Collections;
using System.Collections.Generic;
using Common;
using Managers;
using Request;
using Tools;
using UnityEngine;

public class ShootRequest : BaseRequest
{
    /// <summary>
    /// 射击请求
    /// </summary>
    private PlayerManager playerManager;
    public PlayerManager PlayerManager { get => playerManager; set => playerManager = value; }
    private bool isShoot = false;
    private RoleType rt;
    private Vector3 pos;
    private Vector3 rotation;
    public override void Awake()
    {
        requestCode = RequestCode.Game;
        actionCode = ActionCode.Shoot;

        base.Awake();
    }
    private void Update()
    {
        if (isShoot)
        {
            playerManager.RemoteShoot(rt, pos, rotation);
            isShoot = false;
        }
    }
    public void SendRequest(RoleType roleType, Vector3 pos, Vector3 rotation)
    {
        string data = $"{((int)roleType)}|{pos.x},{pos.y},{pos.z}|{rotation.x},{rotation.y},{rotation.z}";
        base.SendRequest(data);
    }
    public override void OnResponse(string data)
    {
        string[] strs = data.Split('|');
        rt = (RoleType)int.Parse(strs[0]);
        pos = UnityTools.ParseVector3(strs[1]);
        rotation = UnityTools.ParseVector3(strs[2]);
        isShoot = true;
        base.OnResponse(data);
    }
}
