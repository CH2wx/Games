/*
 * 这个代码用于函数的组件上，属于部件的代码
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEditor;

public class Part : MonoBehaviour
{
    public string partName;

    //public Vector3 finalyPosition;
    bool chooseed = false;
    public List<WeldPoint> canUseWeldPoint;
    public GameObject Parent;

    void Start()
    {
        login();
        partName = transform.name;
        gameObject.AddComponent<HighlightingSystem.Highlighter>();
    }
    void Update()
    {
        if (GetComponent<MovePart>().isMoving)
        {
            GetComponent<HighlightingSystem.Highlighter>().On(Color.red);
        }
    }

    public bool AssembleOver()
    {
        if (canUseWeldPoint.Count == 0 && Parent != null)
            return true;
        return false;
    }

    void login()
    {
        GameObject.Find("Manger").GetComponent<Assemble>().loginPart(this);
    }

    public void deleteWeldPoint(GameObject weldPoint)
    {
        canUseWeldPoint.Remove(weldPoint.GetComponent<WeldPoint>());
        Destroy(weldPoint);
    }

    public void choosePart(Color c)
    {
        if (chooseed)
        {
            /*
             * ConstantOn(Color c); 边缘发光
             * ConstantOff(Color c);边缘发光效果关闭
             * FlashingOn(Color.red, Color.blue);  从一种颜色到另一种颜色之间的闪烁
             */
            GetComponent<HighlightingSystem.Highlighter>().ConstantOff();
            chooseed = false;
        }      
        else
        {
            GetComponent<HighlightingSystem.Highlighter>().ConstantOn(c);
            chooseed = true;
        }
    }


}
