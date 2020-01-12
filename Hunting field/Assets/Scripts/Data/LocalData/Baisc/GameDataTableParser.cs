using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Object = System.Object;

public class GameDataTableParser : IDisposable
{
    /// <summary>
    /// 异或因子
    /// </summary>
    private byte[] xorScale = new byte[] { 45, 66, 38, 55, 23, 254, 9, 165, 90, 19, 41, 45, 201, 58, 55, 37, 254, 185, 165, 169, 19, 171 };//.data文件的xor加解密因子
    /// <summary>
    /// 行数
    /// </summary>
    private int row;
    /// <summary>
    /// 行数
    /// </summary>
    public int Row { get { return row; } }
    /// <summary>
    /// 类数
    /// </summary>
    private int column;

    private string[] fileNames;


    //字段信息 和数据分别保存
    /// <summary>
    /// 字段信息
    /// </summary>
    public string[] FileNames { get { return fileNames; } }
    /// <summary>
    /// 字段名字对应的列数
    /// </summary>
    private Dictionary<string, int> fileNameToColumnDic;
    /// <summary>
    /// 实际数据
    /// </summary>
    private string[,] gameData;

    /// <summary>
    /// 当前行，开始为从第三行读取
    /// </summary>
    private int CurRowNO = 3;
    /// <summary>
    /// 是否结束
    /// </summary>
    public bool Eof
    {
        get
        {
            return CurRowNO == row;
        }
    }

    public GameDataTableParser(string path)
    {
        //-------------
        //1.读取文件
        //-------------
        TextAsset obj = Resources.Load<TextAsset>(path);
        //TextAsset
        //FileStream fs = new FileStream(path, FileMode.Open);
        byte[] buffer = obj.bytes;
        //fs.Read(buffer, 0, buffer.Length);
        //fs.Close();

        //------------------
        //第3步：xor解密
        //------------------
        int iScaleLen = xorScale.Length;
        for (int i = 0; i < buffer.Length; i++)
        {
            buffer[i] = (byte)(buffer[i] ^ xorScale[i % iScaleLen]);
        }

        using (MMO_MemoryStream ms = new MMO_MemoryStream(buffer))
        {
            row = ms.ReadInt();
            column = ms.ReadInt();
            gameData = new string[row, column];
            //字段名字
            fileNames = new string[column];
            fileNameToColumnDic = new Dictionary<string, int>();
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < column; j++)
                {
                    string str = ms.ReadUTF8String();
                    //读取第一行数据，第一行数据为字段铭记
                    if (i == 0)
                    {
                        fileNames[j] = str;
                        fileNameToColumnDic[str] = j;
                    }
                    if (i > 2)
                    {
                        gameData[i, j] = str;
                    }
                }
            }
        }
    }

    public void Next()
    {
        if (Eof) return;
        CurRowNO++;
    }

    public string GetFieldValue(string fileName)
    {
        try
        {
            if (CurRowNO < 3 && CurRowNO >= row) return null;
            int colum = fileNameToColumnDic[fileName];
            return gameData[CurRowNO, colum];

        }
        catch { return null; }

    }

    public void Dispose()
    {
        fileNameToColumnDic.Clear();
        fileNameToColumnDic = null;

        fileNames = null;
        gameData = null;
    }
}

