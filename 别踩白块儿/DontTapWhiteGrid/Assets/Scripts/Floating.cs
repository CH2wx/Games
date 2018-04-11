/// <summary>
/// 文档作用：
/// 作者：陈鸿
/// 编辑时间：2017/9/19
/// 备注：控制Start button的浮动
/// </summary>
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floating : MonoBehaviour
{
    private float radian = 0;           //私有弧度值
    private float perRadian = 0.02f;     //每次变化的弧度值，控制浮动的快慢
    private float radius = 0.2f;         //半径值，控制浮动的上下边距

    private Vector3 oldPosition;        //基准点，记录初始位置

    // Use this for initialization
    void Start()
    {
        oldPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        radian += perRadian;            //改变弧度值
        float dy = Mathf.Cos(radian) * radius;   //通过弧度值来控制"Start"Button的运动
        transform.position = oldPosition + new Vector3(0, dy, 0);   //把弧度的偏移量与原位置相加，使之进行上下滑动
    }
}
