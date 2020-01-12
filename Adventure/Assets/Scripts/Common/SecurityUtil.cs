using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��ȫ��У�鹤��
/// </summary>
public class SecurityUtil
{
    /// <summary>
    /// �������
    /// </summary>
    private static byte[] xorScale = new byte[] { 45, 66, 38, 55, 23, 254, 9, 165, 90, 19, 41, 45, 201, 58, 55, 37, 254, 185, 165, 169, 19, 171 };//.data�ļ���xor�ӽ�������

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
