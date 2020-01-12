using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// 在游戏中按F键截图
/// </summary>
public class GetScreenShotDuringGame : MonoBehaviour
{
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("截图");
            Application.CaptureScreenshot(Application.dataPath + "//" + "DuringGame_" + DateTime.Now.ToLongTimeString().Replace(':', '_'));
            UnityEditor.AssetDatabase.Refresh();
        }
    }
}
