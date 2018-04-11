/// <summary>
/// 文档作用：
/// 作者：陈鸿
/// 编辑时间：2017/10/19
/// 备注：控制UI显示得分
/// </summary>
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScore : MonoBehaviour {
    public static UIScore instance = null;

    [SerializeField]
    private Text textScore;         //分数的Text
    private int score = 0;          //当前分数

    void Awake()
    {
        instance = this;
    }

    //加分
    public void Add(int score)
    {
        this.score += score;
        textScore.text = this.score.ToString();
    }

    //扣分
    public void Reduce(int score)
    {
        this.score -= score;

        //如果分数小于0，游戏结束
        if (this.score < 0)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("GameOver");
            return;
        }
        textScore.text = this.score.ToString();
    }
}
