/// <summary>
/// 文档作用：
/// 作者：陈鸿
/// 编辑时间：2017/10/16
/// 备注：用来产生、销毁 水果和炸弹
/// </summary>
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("水果的预设")]
    public GameObject[] fruits;
    [Header("炸弹的预设")]
    public GameObject bomb;
    public AudioSource audioSource;     //控制水果的音效

    private float spawnTime = 3f;       //生成水果的间隔时间
    private bool startPlaying = true;   //用于判断游戏是否开始
    private int tempZ = 0;              //用于储存水果z轴的坐标


    //InvokeRepeating()函数可重复调用，可省去我们自动计时的方法，但是不方便暂停，不容易控制，所以在这里我们使用的是Update
    private void Update()
    {
        if (!startPlaying)
        {
            return;
        }

        spawnTime -= Time.deltaTime;
        if (spawnTime < float.Epsilon)
        {
            //时间到了就产生水果，生成的水果数量随机
            int fruitCount = Random.Range(1, 5);
            for (int i = 0; i < fruitCount; i++)
            {
                OnSpawn(true);
            }

            //随机生成炸弹
            int bombIsShow = Random.Range(0, 100);
            if (bombIsShow > 80)
            {
                OnSpawn(false);
            }
            spawnTime = 3f;
        }
    }

    //生成水果
    private void OnSpawn(bool isFruit)
    {
        //生成水果的音效播放
        this.audioSource.Play();

        //x的范围：-8.4 ~ 8.4       y的范围：transform.position.y
        //随机生成水果的坐标范围
        float x = Random.Range(-8.4f, 8.4f);
        float y = transform.position.y;
        float z = tempZ;
        //使水果的不在同一个水平面上
        tempZ += 2;

        if (tempZ > 14)
            tempZ = 0;

        //实例化水果
        int fruitIndex = Random.Range(0, fruits.Length);
        GameObject temp;
        if (isFruit)
            temp = Instantiate(fruits[fruitIndex], new Vector3(x, y, z), Random.rotation) as GameObject;
        else

            temp = Instantiate(bomb, new Vector3(x, y, z), Random.rotation) as GameObject;
        //设置水果的速度
        Vector3 velocity = new Vector3(-x * Random.Range(0.2f, 0.8f), -Physics.gravity.y * Random.Range(1.15f, 1.55f), 0f);
        //获取水果的刚体
        Rigidbody rb = temp.GetComponent<Rigidbody>();
        rb.velocity = velocity;
    }

    //检测碰撞，让掉落后与该碰撞体碰到的物体全部销毁
    private void OnCollisionEnter(Collision collision)
    {
        Destroy(collision.gameObject);
    }
}