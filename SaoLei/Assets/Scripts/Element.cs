/// <summary>
/// 文档作用：
/// 作者：陈鸿
/// 编辑时间：2017/4/16
/// 备注：扫雷的雷的代码
/// </summary>
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

[RequireComponent(typeof (Selectable))]
public class Element : MonoBehaviour,IPointerClickHandler
{     //IPointerClickHandler继承这个类可以在里面的 void OnPointerClick(PointerEventData eventData)函数中实现点击执行命令的作用
    public bool isMine = false;     //mine 地雷的意思,用于判断是否为地雷
    public Sprite[] emptySprite;    //没有雷的图片
    public Sprite[] mineSprite;     //地雷的图片
    public Sprite[] uncoverSptite;     //点开的图片

    [HideInInspector]
    public Sprite rawSprite;

    private Selectable selectable = null;
    private Image image = null;

    public bool IsUncover
    {
        get
        {
            if (selectable == null)
                selectable = GetComponent<Button>();

            return selectable.enabled;
        }
    }
    private int gird_X, gird_Y;

    public Selectable Selectable { get { return selectable; } } 

	// Use this for initialization
	public void Start () {
        selectable = GetComponent<Selectable>();
        image = GetComponent<Image>();
        rawSprite = image.sprite;

        gird_X = (int)(image.rectTransform.anchoredPosition.x - 10) / 20;
        gird_Y = (int)(image.rectTransform.anchoredPosition.y + 10) / -20;

        int index = gird_Y * Gird.height + gird_X;

        isMine = Gird.CurrentMineList.Contains(index);

        Gird.elements[gird_X, gird_Y] = this;
	}
	
	public void UncoverMine(bool Click)
    {
        if (image == null) image = GetComponent<Image>();

        image.sprite = Click ? mineSprite[0] : mineSprite[1];

        selectable.enabled = false;
    }

    public void UncoverNormal(int mineCount)
    {
        if (image == null) image = GetComponent<Image>();

        image.sprite = emptySprite[mineCount];

        if (IsUncover) Gird.remainUncoverableElementCount--;        //非地雷区贴图减一

        selectable.enabled = false;                                 //可点击属性取消
    }

    public void ElementOnClickLeft()
    {
        if (image == null) image = GetComponent<Image>();

        if(isMine)
        {
            Gird.UncoverAllMine(this.GetInstanceID());
            GameManager.instance.GameOver();
        }
        else
        {
            Gird.FFUncover(gird_X, gird_Y, new bool[Gird.width, Gird.height]);
        }

        if (Gird.remainUncoverableElementCount <= 0)
            GameManager.instance.GameWin();
    }

    public void ElementOnClickRight()
    {
        if (image == null) image = GetComponent<Image>();

        image.sprite = uncoverSptite[GetDisplaySpriteIndex()];
    }
    
    public void OnPointerClick(PointerEventData eventData)
    {
        //throw new NotImplementedException();      抛出异常
        if (!IsUncover) return;                     //如果被翻开了，返回上一级

        GameManager.instance.StopAllCoroutines();
        GameManager.instance.StartCoroutine(GameManager.instance.SetOutputText("Gam", "ing..."));

        if(eventData.button==PointerEventData.InputButton.Left)     //图片被点击的话
        {
            ElementOnClickLeft();
        }
        else
        {
            ElementOnClickRight();
        }
    }

    private int GetDisplaySpriteIndex()
    {
        if (image == null) image = GetComponent<Image>();

        for(int i=0;i<uncoverSptite.Length;i++)
        {
            if (uncoverSptite[i].name == image.name)
                return i == uncoverSptite.Length - 1 ? 0 : ++i;
        }

        return 0;
    }

}
