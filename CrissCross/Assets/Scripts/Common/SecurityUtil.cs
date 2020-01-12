using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 安全性校验工具
/// </summary>
public class SecurityUtil
{
    /// <summary>
    /// 异或因子
    /// </summary>
    private static byte[] xorScale = new byte[] { 45, 66, 38, 55, 23, 254, 9, 165, 90, 19, 41, 45, 201, 58, 55, 37, 254, 185, 165, 169, 19, 171 };//.data文件的xor加解密因子

    private SecurityUtil()
    {

    }

    public static byte[] Xor(byte[] buffer)
    {

        int iScaleLen = xorScale.Length;
        for (int i = 0; i < buffer.Length; i++)
        {
            buffer[i] = (byte)(buffer[i] ^ xorScale[i % iScaleLen]);
        }
        return buffer;
    }
}
