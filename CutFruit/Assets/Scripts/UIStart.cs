/// <summary>
/// 文档作用：
/// 作者：陈鸿
/// 编辑时间：2017/10/15
/// 备注：控制游戏的UGUI空间
/// </summary>
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIStart : MonoBehaviour
{

    private Button btnPlay;                 //游戏场景中的开始按钮
    private Button btnSound;                //游戏场景中的声音开关
    private AudioSource audioSourceBG;      //背景音乐播放器
    private Image imgSound;                 //当前声音显示的图片

    public Sprite[] soundSprites;           //声音的图片

    void Start()
    {
        GetComponents();

        btnPlay.onClick.AddListener(onPlayClick);   //监听，当btnPaly被点击，执行onPlayClick方法
        btnSound.onClick.AddListener(onSoundClick);   //监听，当btnSound被点击，执行onSoundClick方法
    }

    void Update()
    {

    }

    private void OnDestroy()
    {
        btnPlay.onClick.RemoveListener(onPlayClick);      //移除监听，不再调用该方法
        btnSound.onClick.RemoveListener(onSoundClick);    //移除监听，不再调用该方法
    }

    //寻找组件
    private void GetComponents()
    {
        btnPlay = transform.Find("btnPlay").GetComponent<Button>();
        btnSound = transform.Find("btnSound").GetComponent<Button>();
        audioSourceBG = transform.Find("btnSound").GetComponent<AudioSource>();

        imgSound = transform.Find("btnSound").GetComponent<Image>();
    }

    //当开始按钮按下的点击事件
    void onPlayClick()
    {
        SceneManager.LoadScene("Play", LoadSceneMode.Single);       //第二个形参可以不写，默认为LoadSceneMode.Single
    }

    //当声音按钮被点击时调用
    void onSoundClick()
    {
        if (audioSourceBG.isPlaying)                    //声音正在播放
        {
            audioSourceBG.Pause();                      //暂停播放音乐
            imgSound.sprite = soundSprites[1];          //更换声音图片
        }
        else                                            //声音停止播放
        {
            audioSourceBG.Play();                       //再次播放音乐
            imgSound.sprite = soundSprites[0];          //更换声音图片
        }
    }

}
