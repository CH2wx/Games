/// <summary>
/// 文档作用：
/// 作者：陈鸿
/// 编辑时间：2017/9/21
/// 备注：该代码用于控制分数的显示与创建
/// </summary>
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour {
    //public int x = 40, y = 17, w = 100, h = 50;   //如果无法知晓该在Rect中添加多少值，可以先设置变量，在外面设好位置后把值复制进来并注释掉测试的公有变量
	void OnGUI()
    {
        if (GameController.showScore)
        {
            GUIStyle style = new GUIStyle();
            style.normal.textColor = Color.black;
            style.fontSize = 18;
            style.fontStyle = FontStyle.Bold;
            GUI.Label(new Rect(80, 35, 70, 35), "Score:" + GameController.score, style);
        }
    }
}
