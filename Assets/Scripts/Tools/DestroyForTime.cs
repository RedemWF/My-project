using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 倒计时销毁工具
/// </summary>
public class DestroyForTime : MonoBehaviour
{
    public float time = 1;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, time);
    }
}
