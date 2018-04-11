/// <summary>
/// 文档作用：
/// 作者：陈鸿
/// 编辑时间：2017/10/18
/// 备注：物体控制脚本
/// </summary>
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectControl : MonoBehaviour
{
    public GameObject halfFruit;            //一半的水果
    public GameObject splash;
    public GameObject splashFlat;
    public AudioClip ac;                    //用于播放物体被切割时销毁的声音

    private bool dead = false;              //用于防止OnCut被多次调用的标志

    //被切割的时候调用
    public void OnCut()
    {
        //防止重复调用该函数而生成多个水果
        if (dead)
            return;

        if (gameObject.name.Contains("Bomb(Clone)"))          //炸弹
        {
            Instantiate<GameObject>(splash, transform.position, Quaternion.identity);

            //切到炸弹减50分
            UIScore.instance.Reduce(50);
        }
        else               //水果
        {
            //生成被切割的水果
            for (int i = 0; i < 2; i++)
            {
                GameObject temp = Instantiate<GameObject>(halfFruit, transform.position, Random.rotation);
                /// <summary>
                /// Random.onUnitSphere :   返回半径为1的球体在表面上的一个随机点。
                /// ForceMode.Impulse   :   添加一个瞬间冲击力到刚体，使用它的质量。 
                /// 设刚体质量为m=2.0f，力向量为f=(10.0f,0.0f,0.0f)。
                /// 此种方式采用瞬间力作用方式，即把t的值默认为1，不再采用系统的帧频间隔，即   f*1.0 = m*v1
                /// 即可得v1 = f / m = 10.0 / 2.0 = 5.0，即刚体每帧增加5.0米，从而可得刚体每秒的速度为v2 = (1 / 0.02) * 5.0 = 250m / s。
                /// </summary>
                temp.GetComponent<Rigidbody>().AddForce(Random.onUnitSphere * 5f, ForceMode.Impulse);               //给物体一个随机的力，并且设置力的模式是一个冲力
            }
            //生成特效
            Instantiate<GameObject>(splash, transform.position, Quaternion.identity);
            Instantiate<GameObject>(splashFlat, transform.position, Quaternion.identity);

            /*柠檬加15分，苹果加10分，西瓜加5分*/
            if (this.gameObject.tag == "Apple")
                UIScore.instance.Add(10);
            else if (this.gameObject.tag == "Lemo")
                UIScore.instance.Add(15);
            else if (this.gameObject.tag == "Watermelon")
                UIScore.instance.Add(5);
        }


        /*为了防止在播放声音结束之前物体就被销毁而导致声音中断，我们使用另一个播放函数PlayClipAtPoint（），在物体被切割的那点播放声音*/
        AudioSource.PlayClipAtPoint(ac, transform.position);

        Destroy(this.gameObject);

        dead = true;
    }
}
