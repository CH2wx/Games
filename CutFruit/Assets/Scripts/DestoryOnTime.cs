/// <summary>
/// 文档作用：
/// 作者：陈鸿
/// 编辑时间：2017/10/19
/// 备注：根据时间来销毁实例化后不再需要的物体
/// </summary>
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestoryOnTime : MonoBehaviour {

    public float desTime = 2f;

    private void Start()
    {
        Invoke("Dead", desTime);        //2秒后调用Dead函数
    }

    void Dead()
    {
        Destroy(this.gameObject);
    }
}
