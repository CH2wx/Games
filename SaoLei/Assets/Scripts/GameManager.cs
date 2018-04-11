/// <summary>
/// 文档作用：游戏控制
/// 作者：陈鸿
/// 编辑时间：2017/4/17
/// 备注：
/// </summary>
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public int mineSum = Gird.mineSum;

    public Text outputText;
    public InputField maxMineCountInputField;
    public Image faceImage;

    public Sprite[] faceSprites;

    static public GameManager instance = null;

    private void Awake()
    {
        Gird.mineSum = mineSum;
        Gird.remainUncoverableElementCount = Gird.width * Gird.height - mineSum;

        faceImage.sprite = faceSprites[0];

        StopAllCoroutines();
        StartCoroutine(SetOutputText("Ready", "..."));

        instance = this;
    }

    public void GameOver()
    {
        faceImage.sprite = faceSprites[2];
        StopAllCoroutines();
        StartCoroutine(SetOutputText("Game Over", "..."));
    }

    public void GameWin()
    {
        faceImage.sprite = faceSprites[1];
        Gird.DisenableAllElement();
        foreach (var item in Gird.elements)
        {
            if (item.isMine)
                item.Selectable.image.sprite = item.uncoverSptite[1];
        }

        StopAllCoroutines();
        StartCoroutine(SetOutputText("", "YOU WIN!!!"));
    }

    public void SetMaxMineCount()
    {
        int c = Convert.ToInt32(maxMineCountInputField.text);
        if (c <= 0 || c > 255) c = 15;

        mineSum = c;
        Gird.CurrentMineList.Clear();

        maxMineCountInputField.text = mineSum.ToString();
    }

    public void Restart()
    {
        SetMaxMineCount();
        Awake();

        StopAllCoroutines();
        StartCoroutine(SetOutputText("Gam", "ing..."));

        foreach(var items in Gird.elements)
        {
            items.Selectable.image.sprite = items.rawSprite;
            items.Start();
            items.Selectable.enabled = true;
        }
    }

    public void LogoOnClick()
    {
        Application.OpenURL("https://baidu.com");
    }

    public IEnumerator SetOutputText(string s1, string s2, float speed = 0.8f)
    {
        int len1 = s1.Length;
        int len2 = s2.Length;

        while (true)
        {
            Queue<char> s2chQueue = new Queue<char>();
            while (s2.Length > 0)
            {
                s1 += s2[0];
                s2chQueue.Enqueue(s2[0]);
                outputText.text = s1;
                s2 = s2.Remove(0, 1);
                yield return new WaitForSeconds(speed);
            }

            while (s1.Length != len1)
            {
                s2 += s2chQueue.Dequeue();
                s1 = s1.Remove(s1.Length - 1, 1);
                outputText.text = s1;
                yield return new WaitForSeconds(speed);
            }
            yield return new WaitForEndOfFrame();
        }
    }
}
