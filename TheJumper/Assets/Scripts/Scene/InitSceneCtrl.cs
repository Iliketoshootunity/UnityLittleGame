using UnityEngine;
using System.Collections;
using System;
using UnityEngine.SceneManagement;
using LFrameWork.Sound;
using System.Collections.Generic;

public class InitSceneCtrl : MonoBehaviour
{


    [SerializeField]
    private List<AudioClip> m_Musics;

    void Start()
    {
        //加载场景UI
        UIInitSceneView initUI = UISceneCtrl.Instance.Load(UISceneType.Init).GetComponent<UIInitSceneView>();
        initUI.SetUI(GlobalInit.Instance.IsPlaySound);
        initUI.OnClickStartGameBtn = OnClickStartGameBtn;
        initUI.OnClickAudioBtn = OnClickAudioBtn;
        //模拟点击播放声音按钮
        OnClickAudioBtn(GlobalInit.Instance.IsPlaySound);
    }

    private void OnClickAudioBtn(bool isPlay)
    {
        if (isPlay)
        {
            EazySoundManager.GlobalSoundsVolume = 1;
            EazySoundManager.GlobalUISoundsVolume = 1;
            EazySoundManager.PlayMusic(m_Musics[0], 1, true, false, 1, 1);
        }
        else
        {
            EazySoundManager.GlobalSoundsVolume = 0;
            EazySoundManager.GlobalUISoundsVolume = 0;
            EazySoundManager.StopAll(1);
        }

        GlobalInit.Instance.IsPlaySound = isPlay;

    }

    private void OnClickStartGameBtn()
    {
        //进入到下一个场景
       SceneManager.LoadScene("Mini_2");
    }
}
