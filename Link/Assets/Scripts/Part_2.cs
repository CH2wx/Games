using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Part_2 : MonoBehaviour {

    List<GameObject> sons;
    public bool active = false;
    MeshRenderer meshRenderer;
    Material material;
    Renderer renderer;
    // Use this for initialization
    void Awake () {
        //Mesh Renderer（网格渲染器）
        meshRenderer = transform.GetComponent<MeshRenderer>();
        material = transform.GetComponent<Material>();
        renderer = transform.GetComponent<Renderer>();

    }

    // Update is called once per frame
    void Update () {
		
	}

    public void activate()
    {
        if (!active)
        {
            meshRenderer.enabled = true;     //enabled  adj.激活的
            active = true;
            ChangeAlpha(1);
        }          
        else
        {       
            return;
        }
            
    }
    public void close()
    {
        if(active)
        {
            meshRenderer.enabled = false;
            active = false;
        }
        else
        {
            return;
        }
    }

    //改变透明度
    public void ChangeAlpha(float Alpha)
    {
        Color a = renderer.material.color;  //renderer.material.color是渲染器的材质颜色
        a.a = Alpha;
        renderer.material.color = a;
    }

    public void actChilds()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).GetComponent<Part_2>() == null)
                transform.GetChild(i).gameObject.AddComponent<Part_2>();
            
            transform.GetChild(i).gameObject.SetActive(true);
        }
    }
    
    void OnMouseEnter()
    {
        //Debug.Log("MouseEnter " + name);
        if(!active)
        {
            meshRenderer.enabled = true;
            ChangeAlpha(0.5f);
        }
    }

    void OnMouseExit()
    {
        if (!active)
        {
            ChangeAlpha(0);
            meshRenderer.enabled = false;           
        }
    }
}
