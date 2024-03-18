using System.Collections;
using System.Collections.Generic;
using Managers;
using Request;
using UnityEngine;

/// <summary>
/// 攻击请求
/// </summary>
public class AttackRequest : BaseRequest
{
    public override void Awake()
    {
        requestCode = Common.RequestCode.Game;
        actionCode = Common.ActionCode.Attack;
        base.Awake();
    }
    public void SendRequest(int damage)
    {
        base.SendRequest(damage.ToString());
    }
}
