/// <summary>
/// 文档作用：
/// 作者：陈鸿
/// 编辑时间：
/// 备注：游戏的总控制
/// </summary>
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public float downTime = 0.7f;   //用于控制下落的速度
    public static int score = 0;            //表示游戏中的分数
    public static bool showScore = false;   //是否显示分数
    public GameObject startUI;              //开始界面
    public GameObject gameUI;               //游戏界面
    public GameObject container;            //方块的父物体

    public GameObject whiteBlock;
    public GameObject[] normalBlock;
    public bool startGame = false;          //用于判断是否游戏开始，用来处理返回按钮的两种操作
    public bool gameOver = false;           //用于判断游戏是否结束

    public ArrayList blocks;               //用于储存已实例化的方块
    private float gameTime = 0f;
    private bool isTouch = false;           //用于判断用户是否进行了游戏的第一次点击事件

    private void Update()
    {
        if (isTouch && gameTime >= downTime && score > float.Epsilon)   //当玩家点击方块且游戏时间大于下落速度、分数大于0，则使方块下落且
        {
            MoveDown();
            gameTime -= downTime;
        }
        gameTime += Time.deltaTime;
        if (!isTouch)                       //如果玩家没有触碰方块，那么一直让游戏的时间保持在0防止出现，当十秒后再开始游戏，而游戏时间已经10+秒，从而MoveDown运行十次而是游戏出现问题
            gameTime = 0;
        if (score <= 0 && startGame && isTouch) //当玩家进入游戏界面并且开始点击方块后分数小于0，说明游戏结束
        {
            gameOver = true;
            isTouch = false;
            GameEnd();
        }
    }

    //进入游戏界面
    public void EnterGameUI()
    {
        startGame = true;
        //游戏开始，绘制游戏需要的GUI
        showScore = true;
        startUI.SetActive(false);
        gameUI.SetActive(true);
        //开始游戏
        StartGame();
    }

    //返回主界面
    public void ExitStartUI()
    {
        startGame = false;
        gameTime = 0;
        isTouch = false;
        startUI.SetActive(true);
        gameUI.SetActive(false);
        //不绘制GUI
        showScore = false;
        Clean();
        //重置分数
        score = 0;
    }

    private void StartGame()
    {
        blocks = new ArrayList();
        for (int rowIndex = 0; rowIndex < 4; rowIndex++)
        {
            AddBlock(rowIndex);
        }
    }

    //在指定的行里添加一个方块儿
    void AddBlock(int rowIndex)
    {
        float randomNum = Random.Range(0f, 1f);
        GameObject prefab = randomNum > 0.8f ? whiteBlock : normalBlock[4];

        GameObject o = Instantiate(prefab) as GameObject;
        o.transform.parent = container.transform;
        Block b = o.GetComponent<Block>();
        int columnIndex = Random.Range(0, 4);
        b.SetPosition(columnIndex, rowIndex);

        //添加到blocks数组中
        blocks.Add(b);
    }

    /*这个方法不断地重新创建与删除十分的消耗内存，对于这种大量的重复使用相同对象的操作，我们可以使用更好的办法，如：对象池*/
    public void Select(Block block)
    {
        if (!isTouch)
            isTouch = true;

        GetComponent<AudioSource>().Play();     //播放点击音效,只能在有且只有一个Audio时有效，如果有多个音效，则需要加入音效名添加判断才行
        score += block.cantTouch ? -5 : 1;      //判断点击的是否为可触摸的方块儿，如果是不可触摸的，分数减五，否则加一

        if (score < float.Epsilon && startGame)
        {
            gameOver = true;
            GameEnd();
        }
        //MoveDown();                           //该句可以实现点击方块所有方块往下移动的效果
    }

    //使方块向下移动
    private void MoveDown()
    {
        //我们常用的i原意是index “索引”的意思
        for (int i = 0; i < blocks.Count; i++)
        {
            Block b = (Block)blocks[i];

            //下移之后删除物体
            b.MoveDown();
            //删掉移除屏幕的色块
            if (b.rowIndex <= -1 && b.transform.position.y < -6.3f)
            {
                if (b.GetComponent<BoxCollider2D>().isActiveAndEnabled && b.name != "block(Clone)" && b.name != "block1(Clone)")
                {
                    score--;
                }
                blocks.RemoveAt(i);
                //print(b.name + ":" + b.rowIndex);
                Destroy(b.gameObject);
                i--;
            }
        }
        //在最上面添加一个方块儿
        AddBlock(3);
    }

    //清理游戏数据
    public void Clean()
    {
        for (int i = 0; i < blocks.Count; i++)
        {
            Block b = (Block)blocks[i];
            blocks.RemoveAt(i);
            Destroy(b.gameObject);
            i--;
        }
    }

    //游戏结束
    private void GameEnd()
    {
        GameObject.Find("GameUI").transform.GetChild(2).gameObject.SetActive(false);
        blocks.Clear();
        GameObject.Find("GameUI").transform.GetChild(3).gameObject.SetActive(true);
        score = 0;
    }
}
