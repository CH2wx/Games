/// <summary>
/// 文档作用：
/// 作者：陈鸿
/// 编辑时间：2017/9/20 
/// 备注：返回按钮
/// </summary>
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnButton : MonoBehaviour
{
    private GameController gameController;

    // Use this for initialization
    void Start()
    {
        gameController = GameObject.FindObjectOfType<GameController>();
    }

    void OnMouseDown()
    {
        if (gameController.startGame)
            gameController.ExitStartUI();
        else
            Application.Quit();
    }
}
