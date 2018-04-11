/// <summary>
/// 文档作用：
/// 作者：陈鸿
/// 编辑时间：2017/10/20
/// 备注：游戏结束的类
/// </summary>
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class UIOver : MonoBehaviour {

    public UnityEvent events;   //可以通过这个变量使挂上本脚本的物体拥有Button的Event属性

    public void Onclick()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Start");
    }	
}
