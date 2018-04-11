/// <summary>
/// 文档作用：
/// 作者：陈鸿
/// 编辑时间：
/// 备注：
/// </summary>
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CameraController : MonoBehaviour
{
    public float speed = 5.0f;
    // x方向移动的速度
    public float xSpeed = 1f;

    // y方向移动的速度
    public float ySpeed = 1f;

    // z方向移动的速度
    public float zSpeed = 1f;


    public Vector3 velocity = Vector3.zero;

    // 是否按下Q键
    private bool isAdown = false;
    // 是否按下E键
    private bool isDdown = false;

    private float x = 0.0f;
    private float y = 0.0f;

    private Vector2 first = Vector2.zero;
    private Vector2 second = Vector2.zero;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float theZ = Input.GetAxis("Vertical") * Time.deltaTime * speed;
        float theX = Input.GetAxis("Horizontal") * Time.deltaTime * speed;

        // 设置位移
        Vector3 v1 = new Vector3(theX, 0, theZ);
        v1 = this.transform.TransformDirection(v1);
        v1 = v1 - Vector3.Dot(v1, Vector3.up) * Vector3.up;
        v1 = this.transform.InverseTransformDirection(v1);
        this.transform.Translate(v1);

        // 设置旋转角度
        Vector3 v2 = new Vector3(0, theX, 0);
        // this.transform.Rotate(v2);

        if (Input.GetKeyDown(KeyCode.Q))
            this.isAdown = true;
        else if (Input.GetKeyUp(KeyCode.Q))
            this.isAdown = false;
        else if (Input.GetKeyUp(KeyCode.E))
            this.isDdown = false;
        else if (Input.GetKeyDown(KeyCode.E))
            this.isDdown = true;

        if (this.isAdown)
            this.transform.position = Vector3.SmoothDamp(this.transform.position
                    , this.transform.position - new Vector3(0, this.ySpeed, 0)
                    , ref velocity, 0.1f);
        if (this.isDdown)
            this.transform.position = Vector3.SmoothDamp(this.transform.position
                    , this.transform.position + new Vector3(0, this.ySpeed, 0)
                    , ref velocity, 0.1f);

    }

    private void Rotate()
    {

        //Input.GetAxis("Mouse X")获得相机左右旋转的角度(处理X)  
        //Input.GetAxis("Mouse Y")获得相机上下旋转的角度(处理Y)
        //this.x += Input.GetAxis("Mouse X") * speed;
        //this.y -= Input.GetAxis("Mouse Y") * speed;

        //R代表左转，T代表右转
        if (Input.GetKey(KeyCode.R))
            this.x -= speed;
        if (Input.GetKey(KeyCode.T))
            this.x += speed;

        this.y -= Input.GetAxis("Mouse ScrollWheel") * speed * 10;

        if (this.y < -360f)
            this.y += 360f;
        if (this.y > 360f)
            this.y -= 360f;

        y = Mathf.Clamp(y, -20f, 80f);
        Quaternion rotation = Quaternion.Euler(y * 0.5f, x, 0);
        this.transform.rotation = rotation;

    }

    private void LateUpdate()
    {
        Rotate();
    }

    //可以用此代码进行鼠标左右拖动的判断
    /*private void OnGUI()
    {
        if (Event.current.type == EventType.MouseDown)
        {//记录鼠标按下的位置
            first = Event.current.mousePosition;
        }
        if (Event.current.type == EventType.MouseDrag)
        {//记录鼠标拖动的位置
            second = Event.current.mousePosition;
        }
        if (second.x < first.x)
        {
            print("left");

            this.x += speed * 10;
        }
        if (second.x > first.x)
        {//拖动的位置的x坐标比按下的位置的x坐标大时,响应向右事件
            print("right");
            this.x -= speed * 10;
        }

        //this.x += second.x - first.x;
        first = second;

        Quaternion rotation = Quaternion.Euler(0, this.x, 0);
        this.transform.rotation = rotation;
    }*/
}
