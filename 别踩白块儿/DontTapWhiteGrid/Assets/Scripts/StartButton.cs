/// <summary>
/// 文档作用：
/// 作者：陈鸿
/// 编辑时间：2017/9/19 
/// 备注：开始按钮的点击事件
/// </summary>
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartButton : MonoBehaviour
{
    private GameController gameController;
    public static AsyncOperation gameOverToStart;           //用于判断场景加载是否完毕

    void Start()
    {
        gameController = GameObject.FindObjectOfType<GameController>();
    }

    void OnMouseDown()
    {
        GameController.showScore = true;
        gameController.EnterGameUI();
        GameObject.Find("GameUI").transform.GetChild(3).gameObject.SetActive(false);
        if (gameController.gameOver)
        {
            GameController.showScore = false;
            gameOverToStart = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(0);
        }
    }
}
