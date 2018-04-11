using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Assemble : MonoBehaviour {


    GameObject NowTarget;
    Ray mouseRay;
    RaycastHit mouseHit;
    public List<Part> AllPart = new List<Part>();
	// Use this for initialization
	void Start () {
        AllPart.Clear();
	}
	
	// Update is called once per frame
	void Update () {
        mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        //Physics.Raycast(mouseRay, out mouseHit, 100f,1<<LayerMask.NameToLayer("Default"));

        //EventSystem.current.IsPointerOverGameObject()，判断鼠标是否点到了GameObject上面，这个GameObject包括UI也包括3D世界中的任何物体，所以他只能判断用户是都点到了东西
        if (mouseHit.transform != null && !EventSystem.current.IsPointerOverGameObject())
        {
            //Debug.Log(mouseHit.transform.name);
            NowTarget = mouseHit.transform.gameObject;
            //if(NowTarget.GetComponent<HighlightingSystem.Highlighter>()!=null)
            //{
            //    NowTarget.GetComponent<HighlightingSystem.Highlighter>().On(Color.red);
            //}


            /*       .NET3.0以后的新特性 Lambda表达式
             *       RelayCommand(() => this.AddPerson(), () => this.CanAddPerson());的搜索意思可以翻译为
             *       RelayCommand(参数一, 参数二);
             *       参数一 ：() => this.AddPerson()
             *       参数二 ：() => this.CanAddPerson()
             *       () => this.AddPerson() 的意思是 一个没有参数的方法，返回 this.AddPerson() 而这个返回值的类型不用指定 系统会自动判断
             *       同理 () => this.CanAddPerson() 就是 一个没有参数的方法返回this.CanAddPerson()
             *       
             *       下面这个函数的意思是：如果点击的对象为NowTarget则执行choosePart函数，如果是空则输出“”
             */
            /*MouseResponseManager.Register(NowTarget, () =>
             {
                 NowTarget.GetComponent<Part>().choosePart(Color.yellow);
             }, () => { Debug.Log(""); });*/

        }
        else
        {
            NowTarget = null;
        }    
	}

    private void OnGUI()
    {
        if (GUI.Button(new Rect(1050, 10, 50, 50), "删除"))
        {
            destoryNowTarget();
        }
    }


    //删除对象
    public void destoryNowTarget()
    {
        if (Physics.Raycast(mouseRay, out mouseHit, 200) && mouseHit.transform.tag == "RedCube")
            Destroy(NowTarget);
        RaycastHit[] hit = Physics.RaycastAll(mouseRay);

        foreach (var items in hit)
        {
            if (items.transform.tag == "RedCube")
            {
                Destroy(items.transform.gameObject);
            }
        }
    }

    //检查拼装是否完成
    public void AssembleOver()
    {
        for(int i = 0;i<AllPart.Count;i++)
        {
            if (!AllPart[i].AssembleOver())
            {
                Debug.Log("Assemble is not Over");
                return;
            }               
            //return false;
        }
        //return true;
        Debug.Log("Assemble is Over");
    }

    //添加对象于AllPart数组里
    public void loginPart(Part p)
    {
        AllPart.Add(p);
    }

}
