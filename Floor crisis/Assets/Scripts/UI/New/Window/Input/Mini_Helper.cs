using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
using System.IO;
using EasyFrameWork;

#endif

namespace EasyFrameWork
{
    /// <summary>
    /// 记录通用方法
    /// </summary>
    public class Mini_Helper : Singleton<Mini_Helper>
    {
        #region 通用变量
        private char[] nextLineSplitCharArray = new char[] { '\r', '\n' };
        public char[] NextLineSplitCharArray { get { return nextLineSplitCharArray; } }

        private float screenWidthHeightRadio = -1f;
        /// <summary>
        /// 屏幕宽高比
        /// </summary>
        public float ScreenWHRadio
        {
            get
            {
                if (screenWidthHeightRadio == -1f)
                {
                    screenWidthHeightRadio = Screen.width / (float)Screen.height;
                }
                return screenWidthHeightRadio;
            }
        }

        private float screenHeightWidthRadio = -1f;
        /// <summary>
        /// 屏幕高宽比
        /// </summary>
        public float ScreenHWRadio
        {
            get
            {
                if (screenHeightWidthRadio == -1f)
                {
                    screenHeightWidthRadio = 1 / ScreenWHRadio;
                }
                return screenHeightWidthRadio;
            }
        }
        #endregion

        #region 数学计算
        private float roundFloatTemp;
        /// <summary>
        /// 四舍五入法转化float为int
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public int GetRoundFloat(float input)
        {
            roundFloatTemp = input % 1;
            if (roundFloatTemp >= 0.5f)
            {
                return (int)(input + 1);
            }
            else if (roundFloatTemp <= -0.5f)
            {
                return (int)(input - 1);
            }
            return (int)input;
        }
        #endregion

        #region Json格式化
        private Stack<char> charStackTemp = new Stack<char>();
        /// <summary>
        /// 将json格式化
        /// </summary>
        /// <param name="configStr"></param>
        public void FormatConfig(ref string configStr)
        {
            if (string.IsNullOrEmpty(configStr)) return;

            int index = 0;
            while (index < configStr.Length)
            {
                char c = configStr[index];
                if (c == '{')
                {
                    charStackTemp.Push(c);
                    index++;
                    InsertNewline(ref configStr, ref index);
                }
                else if (c == '}')
                {
                    if (charStackTemp.Peek() == '{')
                        charStackTemp.Pop();
                    InsertNewline(ref configStr, ref index);
                    index++;
                }
                else if (c == '[')
                {
                    charStackTemp.Push(c);
                    index++;
                    InsertNewline(ref configStr, ref index);
                }
                else if (c == ']')
                {
                    if (charStackTemp.Peek() == '[')
                        charStackTemp.Pop();
                    InsertNewline(ref configStr, ref index);
                    index++;
                }
                else if (c == ',')
                {
                    index++;
                    InsertNewline(ref configStr, ref index);
                }
                else
                {
                    index++;
                }
            }
        }

        private void InsertNewline(ref string configStr, ref int index)
        {
            if (string.IsNullOrEmpty(configStr) || index < 0)
                return;

            if (index < configStr.Length)
            {
                configStr = configStr.Insert(index, "\r\n");
                index = index + 2;
                int tabCount = charStackTemp.Count;
                if (index < configStr.Length)
                {
                    configStr = configStr.Insert(index, GetTabs(tabCount));
                    index = index + tabCount;
                }
                else
                    return;
            }
            else
                return;
        }

        private string GetTabs(int count)
        {
            StringBuilder tabs = new StringBuilder();
            for (int i = 0; i < count; i++)
            {
                tabs.Append('\t');
            }
            string str = tabs.ToString();
            return str;
        }
        #endregion

        #region 工具方法
        /// <summary>
        /// 执行回调
        /// </summary>
        /// <param name="callback">回调函数</param>
        public void DoCallback(Action callback)
        {
            if (callback != null)
            {
                callback();
            }
        }

        /// <summary>
        /// 执行回调
        /// </summary>
        /// <typeparam name="T">回调类型</typeparam>
        /// <param name="callback">回调函数</param>
        /// <param name="para">回调参数</param>
        public void DoCallback<T>(Action<T> callback, T para)
        {
            if (callback != null)
            {
                callback(para);
            }
        }

        /// <summary>
        /// 执行回调
        /// </summary>
        /// <typeparam name="T1">第一个参数的类型</typeparam>
        /// <typeparam name="T2">第二个参数的类型</typeparam>
        /// <param name="callback">回调函数</param>
        /// <param name="arg1">回调参数1</param>
        /// <param name="arg2">回调参数2</param>
        public void DoCallback<T1, T2>(Action<T1, T2> callback, T1 arg1, T2 arg2)
        {
            if (callback != null)
            {
                callback(arg1, arg2);
            }
        }

        private Transform transTemp;
        private GameObject goTemp;
        /// <summary>
        /// 获取某个物体或其子物体的某个组件
        /// </summary>
        /// <typeparam name="T">组件类型</typeparam>
        /// <param name="sourceComponent">已知的组件</param>
        /// <param name="relativePath">子物体的相对路径</param>
        /// <returns></returns>
        public T GetComponentInTransform<T>(Component sourceComponent, string relativePath) where T : Component
        {
            transTemp = null;
            if (sourceComponent != null)
            {
                transTemp = sourceComponent.transform.Find(relativePath);
            }
            else
            {
                goTemp = GameObject.Find(relativePath);
                if (goTemp != null)
                {
                    transTemp = goTemp.transform;
                }
            }
            if (transTemp != null)
            {
                return transTemp.GetComponent<T>();
            }
            return null;
        }

        private StringBuilder sbForPercentage = new StringBuilder();
        /// <summary>
        /// 获取小数对应的百分数字符串
        /// </summary>
        /// <param name="percentage">小数</param>
        /// <param name="numAfterDot">百分数的小数点后保留位数</param>
        /// <returns></returns>
        public string GetStringForPercentage(float percentage, int numAfterDot)
        {
            sbForPercentage.Remove(0, sbForPercentage.Length);
            if (percentage < 0)
            {
                sbForPercentage.Append('-');
            }
            percentage = Mathf.Abs(percentage);
            percentage *= 100;
            sbForPercentage.Append((int)percentage);
            if (numAfterDot > 0)
            {
                sbForPercentage.Append('.');
                for (int i = 0; i < numAfterDot; i++)
                {
                    percentage -= (int)percentage;
                    percentage *= 10;
                    sbForPercentage.Append((int)percentage);
                }
            }
            sbForPercentage.Append('%');
            return sbForPercentage.ToString();
        }

        /// <summary>
        /// 获取类型对应的字符串
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public string GetStringForType<T>()
        {
            return typeof(T).ToString();
        }

        /// <summary>
        /// 获取object对应的字符串
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public string GetStringForType(object obj)
        {
            return obj.GetType().ToString();
        }

        /// <summary>
        /// 将目标RectTranform置于其父物体中心,锚点放在父物体四周,大小设置为父物体大小
        /// </summary>
        /// <param name="targetRect"></param>
        public void SetTargetFullOfParent(RectTransform targetRect)
        {
            if (targetRect == null || targetRect.parent == null || !(targetRect.parent is RectTransform))
            {
                return;
            }
            targetRect.localScale = Vector3.one;
            targetRect.localPosition = Vector3.zero;
            targetRect.localRotation = Quaternion.identity;
            targetRect.anchorMin = Vector2.zero;
            targetRect.anchorMax = Vector2.one;
            targetRect.pivot = Vector2.one * 0.5f;
            targetRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, (targetRect.parent as RectTransform).rect.width);
            targetRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, (targetRect.parent as RectTransform).rect.height);
        }

#if UNITY_EDITOR
        public string ResourcesStr { get { return "Resources/"; } }
        /// <summary>
        /// 获取相对于Resources文件夹的相对路径(去除后缀名)
        /// </summary>
        /// <param name="fullPath">完整路径</param>
        /// <returns>相对路径</returns>
        public string GetRelativePathToResourcesFolder(string fullPath)
        {
            try
            {
                return fullPath.Substring(fullPath.IndexOf(ResourcesStr) + ResourcesStr.Length, fullPath.Length - (fullPath.IndexOf(ResourcesStr) + ResourcesStr.Length) - (fullPath.Length - fullPath.LastIndexOf('.')));
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// 获取资源所在的Resources文件夹的完整路径
        /// </summary>
        /// <param name="assetObj">资源</param>
        /// <returns></returns>
        public string GetResourcesFolderFullPath(UnityEngine.Object assetObj)
        {
            string relativePath = AssetDatabase.GetAssetPath(assetObj);
            try
            {
                if (!string.IsNullOrEmpty(relativePath))
                {
                    return Application.dataPath + relativePath.Substring(6, relativePath.IndexOf(ResourcesStr) - 6) + ResourcesStr;
                }
            }
            catch (Exception) { }
            return string.Empty;
        }

        /// <summary>
        /// 获取Resources文件夹的完整路径(若Assets文件夹不存在Resources文件夹,则在Assets文件夹下接一个Resources文件夹)
        /// 以"Resources/"结尾
        /// </summary>
        /// <returns></returns>
        public string GetResourcesFolderFullPath()
        {
            string[] resourcesDirectories = Directory.GetDirectories(Application.dataPath, "Resources", SearchOption.AllDirectories);
            if (resourcesDirectories == null || resourcesDirectories.Length == 0)
            {
                return Application.dataPath + '/' + ResourcesStr;
            }
            else
            {
                return (resourcesDirectories[0] + '/').Replace('\\', '/');
            }
        }

        /// <summary>
        /// 保存字符串到Resources文件夹中
        /// </summary>
        /// <param name="content">存储内容</param>
        /// <param name="relativePathToResources">存储位置相对于Resources文件夹路径</param>
        /// <param name="postfix">后缀名(eg. ".txt")</param>
        /// <returns></returns>
        public bool SaveStringToResourcesFolder(string content, string relativePathToResources, string postfix)
        {
            try
            {
                if (string.IsNullOrEmpty(relativePathToResources))
                {
                    return false;
                }
                string fullFilePath = GetResourcesFolderFullPath() + relativePathToResources + (string.IsNullOrEmpty(postfix) ? string.Empty : postfix);
                string directoryPath = Path.GetDirectoryName(fullFilePath);
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }
                if (File.Exists(fullFilePath))
                {
                    File.Delete(fullFilePath);
                }
                if (string.IsNullOrEmpty(content))
                {
                    content = string.Empty;
                }
                File.WriteAllText(fullFilePath, content);
                AssetDatabase.Refresh();
                return true;
            }
            catch (Exception ex)
            {
                Debug.LogError(ex.Message);
                AssetDatabase.Refresh();
                return false;
            }
        }
#endif
        #endregion
    }
}

