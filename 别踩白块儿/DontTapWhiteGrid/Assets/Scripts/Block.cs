/// <summary>
/// 文档作用：
/// 作者：陈鸿
/// 编辑时间：2017/9/20
/// 备注：方块的控制代码
/// </summary>
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public bool cantTouch = false;  //默认所有的方块都是可以触摸的，然后在预设体的白方块儿中勾选这个bool值

    //行列索引
    public int rowIndex;
    public int columnIndex;
    //x,y轴的坐标偏移量
    private float x0ff = -2.25f;
    private float y0ff = -3.68f;
    //游戏控制器
    private GameController gameController;

    void Start()
    {
        gameController = GameObject.FindObjectOfType<GameController>();
    }

    private void OnMouseDown()
    {
        gameController.Select(this);

        GameObject o = Instantiate(gameController.normalBlock[0].gameObject, transform.position, Quaternion.identity);
        Block b = o.GetComponent<Block>();
        b.rowIndex = this.rowIndex;
        b.columnIndex = this.columnIndex;

        gameController.blocks.Add(b);

        GetComponent<BoxCollider2D>().enabled = false;                      //当方块被点击后，该方块无法被再次点击
    }

    public void SetPosition(int columnIndex, int rowIndex)
    {
        this.rowIndex = rowIndex;
        this.columnIndex = columnIndex;
        gameObject.transform.position = new Vector3(x0ff + columnIndex * 1.5f, y0ff + rowIndex * 2.68f, 0);
    }

    //让方块向下移动
    public void MoveDown()
    {
        SetPosition(columnIndex, rowIndex - 1);
    }
}
