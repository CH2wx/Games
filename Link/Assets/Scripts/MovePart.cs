using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MovePart : MonoBehaviour {

    public Transform ChoosePart;// 目标物体的空间变换组件 
    private Vector3 _vec3TargetScreenSpace;// 目标物体的屏幕空间坐标  
    private Vector3 _vec3TargetWorldSpace;// 目标物体的世界空间坐标  
    private Vector3 _vec3MouseScreenSpace;// 鼠标的屏幕空间坐标  
    private Vector3 _vec3Offset;// 偏移 
    public bool isMoving;

    EventSystem eventSystem;
    GraphicRaycaster RaycastInCanvas;
	// Use this for initialization
	void Awake () {
        ChoosePart=transform;
	}

    IEnumerator OnMouseDown()

    {

        // 把目标物体的世界空间坐标转换到它自身的屏幕空间坐标   
        
        _vec3TargetScreenSpace = Camera.main.WorldToScreenPoint(ChoosePart.position);

        // 存储鼠标的屏幕空间坐标（Z值使用目标物体的屏幕空间坐标）   

        _vec3MouseScreenSpace = new Vector3(Input.mousePosition.x, Input.mousePosition.y, _vec3TargetScreenSpace.z);

        // 计算目标物体与鼠标物体在世界空间中的偏移量   

        _vec3Offset = ChoosePart.position - Camera.main.ScreenToWorldPoint(_vec3MouseScreenSpace);

        // 鼠标左键按下   

        while (Input.GetMouseButton(0)&& !EventSystem.current.IsPointerOverGameObject())
        {
            isMoving = true;
            // 存储鼠标的屏幕空间坐标（Z值使用目标物体的屏幕空间坐标）  

            _vec3MouseScreenSpace = new Vector3(Input.mousePosition.x, Input.mousePosition.y, _vec3TargetScreenSpace.z);

            // 把鼠标的屏幕空间坐标转换到世界空间坐标（Z值使用目标物体的屏幕空间坐标），加上偏移量，以此作为目标物体的世界空间坐标  

            _vec3TargetWorldSpace = Camera.main.ScreenToWorldPoint(_vec3MouseScreenSpace) + _vec3Offset;

            // 更新目标物体的世界空间坐标   

            ChoosePart.position = _vec3TargetWorldSpace;

            // 等待固定更新   
            //Debug.Log(ChoosePart.name + " move (" + isMoving + ")");
            yield return new WaitForFixedUpdate();
        }
        isMoving = false;
        //Debug.Log(ChoosePart.name + " move (" + isMoving + ")");
    }

    public void stopMouseDrag()
    {
        StopCoroutine("OnMouseDown");
        isMoving = false;
    }

}
