/// <summary>
/// 文档作用：
/// 作者：陈鸿
/// 编辑时间：2017/4/17
/// 备注：关于地雷的代码
/// </summary>
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gird : MonoBehaviour {
    static public int width = 16;
    static public int height = 16;

    static public int mineSum = 15;             //地雷数
    static public int remainUncoverableElementCount = width * height - mineSum; //非地雷区

    static public Element[,] elements = new Element[width, height];
    static private List<int> currentMineList = new List<int>();

    static public List<int> CurrentMineList
    {
        get
        {
            if (currentMineList.Count > 0)                  //若是雷没有扫完，继续游戏，返回剩余雷的位置
                return currentMineList;
            else                                            //若是雷扫完了，则重新随机放置雷
                return GetRandomMineList(mineSum);
        }
        set                                                 //把获取的值添加进currentMineList
        {
            currentMineList = value;
        }
    }

    static public bool MineAt(int x,int y)              //判断是否为地雷
    {
        /*if (x < 0 || y < 0 || x > width || y > height)
            return false;
        
        return elements[x, y].isMine;*/
        if (x >= 0 && y >= 0 && x < width && y < height)
            return elements[x,y].isMine;

        return false;
    }

    static public int GetMineCount(int x, int y)        //获取地雷数量
    {
        int count = 0;

        if (MineAt(x - 1, y + 1))   count++;
        if (MineAt(x, y + 1))       count++;
        if (MineAt(x + 1, y + 1))   count++;
        if (MineAt(x - 1, y))       count++;
        if (MineAt(x + 1, y))       count++;
        if (MineAt(x - 1, y - 1))   count++;
        if (MineAt(x, y - 1))       count++;
        if (MineAt(x + 1, y - 1))   count++;

        return count;
    }
    
    static public void UncoverAllMine(int clickID)
    {
        foreach(var item in elements)
        {
            if(item.isMine)
                item.UncoverMine(clickID == item.GetInstanceID());
            
            item.Selectable.enabled = false;                    //enabled是否被激活
        }
    }

    static public void FFUncover(int x, int y, bool[,] visit)
    {
        if (visit[x, y]) return;

        int count = GetMineCount(x, y);
        elements[x, y].UncoverNormal(count);

        if (count > 0) return;

        visit[x, y] = true;

        FFUncover(x, y + 1, visit);
        FFUncover(x, y - 1, visit);
        FFUncover(x + 1, y, visit);
        FFUncover(x - 1, y, visit);

    }

    static public void DisenableAllElement()       //Disenable禁止
    {
        foreach(var item in elements)
        {
            item.Selectable.enabled = false;
        }
    }

    static public List<int> GetRandomMineList(int num)             //随机放置炸弹
    {
        List<int> mineIndex = new List<int>();              //炸弹索引的容器

        for(int i=0;i<num;i++)
        {
            int r = Random.Range(0, width * height);        //随即赋值

            while (mineIndex.Contains(r))                   //检查是否有相同位置的雷，如果有相同的返回false
                r = Random.Range(0, width * height);
            /* list.contains(o)，系统会对list中的每个元素e调用o.equals(e)，方法，加入list中有n个元素，那么会调用n次o.equals(e)，只要有一次o.equals(e)返回了true，那么list.contains(o)返回true，否则返回false。*/

            mineIndex.Add(r);                               //把没有放置雷的位置添加雷
        }
        return mineIndex;
    }
}
