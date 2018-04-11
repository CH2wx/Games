/// <summary>
/// 文档作用：
/// 作者：陈鸿
/// 编辑时间：2017/10/17
/// 备注：实现切水果的功能 (刀痕效果、
/// </summary>
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseControl : MonoBehaviour
{
    [SerializeField]    //意思是外部可见
    private LineRenderer lineRenderer;                  //直线渲染器
    [SerializeField]
    private AudioSource audioSource;                    //挥刀音效，声音组件

    private bool firstMouseDown = false;                //鼠标是否第一次按下
    private bool mouseDown = false;                     //鼠标是否一直按下状态

    private Vector3[] positions = new Vector3[10];      //用来保存所有的坐标
    private int posCount = 0;                           //用来储存当前保存的坐标的数量
    private Vector3 head;                               //用来记录当前帧鼠标的坐标
    private Vector3 last;                               //用来记录上一帧鼠标的坐标

    private void Update()
    {
        //如果鼠标左键按下，在PC和手机端，这都是获取是否点击的函数
        if (Input.GetMouseButtonDown(0))
        {
            firstMouseDown = true;
            mouseDown = true;
            audioSource.Play();                         //鼠标按下时播放声音
        }
        if(Input.GetMouseButtonUp(0))
        {
            mouseDown = false;
        }
        onDranLine();

        firstMouseDown = false;                         //只需检测一瞬即可
    }

    //控制划线
    private void onDranLine()
    {
        //print("firstMouseDown:" + firstMouseDown + ",mouseDown:" + mouseDown);
        if (firstMouseDown)
        {
            posCount = 0;                               //重置计数器

            head = Camera.main.ScreenToWorldPoint(Input.mousePosition);     //储存当前鼠标在屏幕上的位置
            last = head;
        }

        if (mouseDown)
        {
            head = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //如果距离较远，则保存坐标的位置
            if (Vector3.Distance(head, last) > 0.01f)
            {
                SavePosition(head);
                posCount++;

                onRayCast(head);            //发射一条射线
            }

            last = head;
        }
        //当鼠标松开或者不再移动的时候，使被渲染的直线全部集中在一点，消失移动的轨迹
        if (Input.GetMouseButtonUp(0) || (Vector3.Distance(Camera.main.ScreenToWorldPoint(Input.mousePosition), last) < float.Epsilon))
        {
            SavePosition(last);
            posCount++;
        }

        ChangePositions(positions);
    }

    //保存坐标点
    private void SavePosition(Vector3 pos)
    {
        pos.z = 0;

        if (posCount <= 9)
        {
            for (int i = posCount; i < 10; i++)
            {
                positions[i] = pos;
            }
        }
        else
        {
            for(int i = 0; i < 9; i++)
            {
                positions[i] = positions[i + 1];
            }
            positions[9] = pos;
        }
    }

    //更改鼠标数组里的值
    private void ChangePositions(Vector3[] positions)
    {
        lineRenderer.SetPositions(positions);
    }

    //从屏幕发射射线入游戏中
    private void onRayCast(Vector3 worldPos)
    {
        //Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);          //从屏幕上的点发射一条射线
        RaycastHit[] hits = Physics.RaycastAll(ray);                //检测所有物体

        for (int i = 0; i < hits.Length; i++)
        {
            //print(hits[i].collider.gameObject.name);              //这句代码用来检测射线是否检测到了物体
            hits[i].collider.gameObject.SendMessage("OnCut", SendMessageOptions.DontRequireReceiver);    //SendMessageOptions.DontRequireReceiver表示即使没检测到也不会报错，发送的消息为一个方法名，场景中的所有脚本都会查找这个方法，如果有，则实现
        }
    }
}
