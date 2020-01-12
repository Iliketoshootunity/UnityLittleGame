using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class PlatformInfo
{
    /// <summary>
    /// 普通平台名字
    /// </summary>
    public string NormalPlatfromName;
    /// <summary>
    /// 其他陷阱平台名字
    /// </summary>
    public List<string> OtherTrapPlatfromNames;
    /// <summary>
    /// 隐藏陷阱名字
    /// </summary>
    public string HideTrapPlatfromName;
    /// <summary>
    /// 背景名字
    /// </summary>
    public string BGName;

}
