using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

/// <summary>
/// 内存流扩展
/// 读取 byte uByte, short,UnShort,int,Uint,float,Double,bool,string
/// </summary>
public class MMO_MemoryStream : MemoryStream
{

    public MMO_MemoryStream()
    {

    }


    public MMO_MemoryStream(byte[] buffer) : base(buffer)
    {

    }

    #region Short

    public short ReadShort()
    {
        byte[] arr = new byte[2];
        base.Read(arr, 0, 2);
        return BitConverter.ToInt16(arr, 0);

    }

    public void WriteShort(short value)
    {
        byte[] arr = BitConverter.GetBytes(value);
        base.Write(arr, 0, arr.Length);
    }

    #endregion

    #region UShort

    public ushort ReadUShort()
    {
        byte[] arr = new byte[2];
        base.Read(arr, 0, 2);
        return BitConverter.ToUInt16(arr, 0);

    }

    public void WriteUShort(ushort value)
    {
        byte[] arr = BitConverter.GetBytes(value);
        base.Write(arr, 0, arr.Length);
    }

    #endregion

    #region Int

    public int ReadInt()
    {
        byte[] arr = new byte[4];
        base.Read(arr, 0, 4);
        return BitConverter.ToInt32(arr, 0);

    }

    public void WriteInt(int value)
    {
        byte[] arr = BitConverter.GetBytes(value);
        base.Write(arr, 0, arr.Length);
    }

    #endregion

    #region Int

    public uint ReadUInt()
    {
        byte[] arr = new byte[4];
        base.Read(arr, 0, 4);
        return BitConverter.ToUInt32(arr, 0);

    }

    public void WriteUInt(uint value)
    {
        byte[] arr = BitConverter.GetBytes(value);
        base.Write(arr, 0, arr.Length);
    }

    #endregion

    #region float

    public float ReadFloat()
    {
        byte[] arr = new byte[4];
        base.Read(arr, 0, 4);
        return BitConverter.ToSingle(arr, 0);

    }

    public void WriteFloat(float value)
    {
        byte[] arr = BitConverter.GetBytes(value);
        base.Write(arr, 0, arr.Length);
    }

    #endregion
    #region Double

    public double ReadDouble()
    {
        byte[] arr = new byte[8];
        base.Read(arr, 0, 8);
        return BitConverter.ToDouble(arr, 0);

    }

    public void WriteDouble(float value)
    {
        byte[] arr = BitConverter.GetBytes(value);
        base.Write(arr, 0, arr.Length);
    }

    #endregion
    #region bool

    public bool ReadBool()
    {

        return (base.ReadByte() == 1 ? true : false);


    }

    public void WriteBool(bool value)
    {
        base.WriteByte((byte)(value == true ? 1 : 0));
    }

    #endregion

    public string ReadUTF8String()
    {
        ushort length = ReadUShort();
        if (length >= 63355)
        {
            throw new Exception("字符读取过长");
        }
        byte[] arr = new byte[length];
        base.Read(arr, 0, length);
        return Encoding.UTF8.GetString(arr);

    }


    public void WriteUTF8String(string value)
    {
        byte[] arr = Encoding.UTF8.GetBytes(value);

        WriteUShort((ushort)arr.Length);
        base.Write(arr, 0, arr.Length);
    }

}
