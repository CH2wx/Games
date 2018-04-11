/// <summary>
/// 文档作用：
/// 作者：陈鸿
/// 编辑时间：4.26
/// 备注：物体跟随移动
/// </summary>
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walk_Follow : MonoBehaviour {
    public Transform target;
    public float smothing = 5.0f;

    [HideInInspector]
    public Vector3 offset;


	// Use this for initialization
	void Start () {
        offset = target.position - transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 targetOffset = target.position - offset;
        transform.position = Vector3.Lerp(transform.position, targetOffset, smothing * Time.deltaTime);
	}
}
