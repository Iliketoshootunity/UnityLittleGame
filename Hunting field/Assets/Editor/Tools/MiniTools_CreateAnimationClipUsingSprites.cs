using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;
using System.Text;

/// <summary>
/// Unity工具
/// 从精灵图片中创建动画片段
/// </summary>
public class MiniTools_CreateAnimationClipUsingSprites : EditorWindow
{
    static List<Sprite> selectedSpritesList;
    static List<Sprite> SelectedSpritesList { get { selectedSpritesList = selectedSpritesList ?? new List<Sprite>(); return selectedSpritesList; } }//选中的精灵列表

    static Rect windowRect = new Rect(0, 0, 500, 450);
    static bool isInSlicedPics = false;

    /// <summary>
    /// 在目标目录下创建动画片段
    /// </summary>
    [MenuItem("MiniTools/创建2D动画")]
    static void CreateAnimationClipInTargetDirectoryBySinglePics()
    {
        GetWindowWithRect(typeof(MiniTools_CreateAnimationClipUsingSprites), windowRect, true, "创建动画片段");
    }

    private string frameStr;
    private int frameCount;
    private bool isAnimationLoop;
    private string savingPathBuffer;
    private bool isAbleToCreateAnimation;
    private StringBuilder sbTemp = new StringBuilder();
    private bool isAlreadyExist;

    Vector2 _scrollPos;

    void OnGUI()
    {
        isAbleToCreateAnimation = SelectedSpritesList.Count != 0;
        sbTemp.Remove(0, sbTemp.Length);
        GUILayout.BeginVertical();
        GUI.skin.label.fontSize = 12;
        GUI.skin.label.fontStyle = FontStyle.Normal;
        GUILayout.Space(12);
        GUILayout.EndVertical();
        //绘制帧数输入框
        GUI.skin.label.normal.textColor = Color.black;
        GUILayout.BeginHorizontal();
        GUILayout.Label("FPS:");
        Rect rectForFrame = new Rect(40, 16, 100, 14);
        frameStr = EditorGUI.TextField(rectForFrame, frameStr);
        try
        {
            frameCount = int.Parse(frameStr);
        }
        catch (System.Exception)
        {
            isAbleToCreateAnimation = false;
            sbTemp.Append("输入数字");
        }
        //绘制循环选项
        GUI.skin.label.normal.textColor = Color.black;
        GUILayout.Space(0);
        Rect rectForLoopToggle = new Rect(200, 14, 100, 16);
        isAnimationLoop = GUI.Toggle(rectForLoopToggle, isAnimationLoop, "Loop");
        GUILayout.EndHorizontal();
        //绘制枚举框
        GUILayout.BeginHorizontal();
        isInSlicedPics = GUILayout.Toggle(isInSlicedPics, isInSlicedPics ? "从Multiply Sprite 中读取 Sprite" : "从多张 Sprite 中读取 Sprite");
        GUILayout.EndHorizontal();
        //绘制保存按钮
        GUILayout.BeginVertical();
        GUILayout.Space(10);
        GUI.skin.label.normal.textColor = Color.red;
        GUILayout.Label(sbTemp.ToString());
        GUI.skin.label.normal.textColor = Color.black;
        GUILayout.Space(10);
        GUILayout.EndVertical();
        GUILayout.BeginHorizontal();
        GUI.enabled = isAbleToCreateAnimation;
        if (GUILayout.Button("保存动画"))
        {
            SaveAnimation();
        }
        GUILayout.EndHorizontal();
        //绘制精灵列表
        GUI.enabled = true;
        GUILayout.BeginVertical();
        GUILayout.Space(10);
        GUILayout.Label(string.Format("精灵列表:（选中{0}张精灵）", SelectedSpritesList.Count));
        GUILayout.EndVertical();
        _scrollPos = GUILayout.BeginScrollView(_scrollPos);
        for (int i = 0; i < SelectedSpritesList.Count; i++)
        {
            GUILayout.Label((i + 1).ToString() + ".  " + SelectedSpritesList[i].name);
        }
        GUILayout.EndScrollView();
    }

    private void OnInspectorUpdate()
    {
        Repaint();
        if (isInSlicedPics)
        {
            RefreshSpriteListInSlicedPic();
        }
        else
        {
            RefreshSpriteList();
        }
    }

    private void SaveAnimation()
    {
        string savingPath = EditorUtility.SaveFilePanelInProject("保存动画片段", SelectedSpritesList[0].name + ".anim", "anim", "Select Path To Save AnimationClip", savingPathBuffer);
        if (savingPath.Length > 0)
        {
            savingPathBuffer = savingPath;
            AnimationClip clip = AssetDatabase.LoadAssetAtPath<AnimationClip>(savingPath);
            isAlreadyExist = true;
            if (clip == null)
            {
                isAlreadyExist = false;
                clip = new AnimationClip();
            }
            EditorCurveBinding curveBinding = new EditorCurveBinding();
            curveBinding.type = typeof(SpriteRenderer);
            curveBinding.path = "";
            curveBinding.propertyName = "m_Sprite";
            ObjectReferenceKeyframe[] keyFrames = new ObjectReferenceKeyframe[SelectedSpritesList.Count];
            float frameTime = 1f / frameCount;
            for (int i = 0; i < SelectedSpritesList.Count; i++)
            {
                keyFrames[i] = new ObjectReferenceKeyframe();
                keyFrames[i].time = frameTime * i;
                keyFrames[i].value = SelectedSpritesList[i];
            }
            clip.frameRate = frameCount;
            AnimationClipSettings clipSetting = AnimationUtility.GetAnimationClipSettings(clip);
            clipSetting.loopTime = isAnimationLoop;
            AnimationUtility.SetAnimationClipSettings(clip, clipSetting);
            AnimationUtility.SetObjectReferenceCurve(clip, curveBinding, keyFrames);
            if (!isAlreadyExist)
            {
                AssetDatabase.CreateAsset(clip, savingPath);
            }
            AssetDatabase.SaveAssets();
            if (!isAlreadyExist)
            {
                Debug.Log(savingPath + "创建成功");
            }
            else
            {
                Debug.Log(savingPath + "保存成功");
            }
        }
    }

    /// <summary>
    /// 刷新精灵列表
    /// </summary>
    private static void RefreshSpriteList()
    {
        SelectedSpritesList.Clear();
        Object[] selectedObjects = Selection.GetFiltered(typeof(Texture2D), SelectionMode.Assets);
        foreach (Object objectSelection in selectedObjects)
        {
            Object[] spriteObjects = AssetDatabase.LoadAllAssetsAtPath(AssetDatabase.GetAssetPath(objectSelection));
            for (int i = 0; i < spriteObjects.Length; i++)
            {
                if (spriteObjects[i] is Sprite)
                {
                    SelectedSpritesList.Add(spriteObjects[i] as Sprite);
                }
            }
        }
        SelectedSpritesList.Sort(delegate (Sprite lhs, Sprite rhs)
        {
            return lhs.name.CompareTo(rhs.name);
        });
    }

    /// <summary>
    /// 刷新分割的图片中的精灵列表
    /// </summary>
    private static void RefreshSpriteListInSlicedPic()
    {
        SelectedSpritesList.Clear();
        Object[] selectedObjects = Selection.GetFiltered(typeof(Sprite), SelectionMode.Deep);
        foreach (Object objectSelection in selectedObjects)
        {
            if (objectSelection is Sprite)
            {
                SelectedSpritesList.Add(objectSelection as Sprite);
            }
        }
        SelectedSpritesList.Sort(delegate (Sprite lhs, Sprite rhs)
        {
            return lhs.name.CompareTo(rhs.name);
        });
    }
}
