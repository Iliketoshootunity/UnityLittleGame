using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 数据管理基类
/// </summary>
/// <typeparam name="T"></typeparam>
/// <typeparam name="E"></typeparam>

public abstract class AbstractDBModel<T, E> where T : class, new()
    where E : AbstractEntity
{

    protected List<E> m_List;
    protected Dictionary<int, E> m_Dic;

    protected AbstractDBModel()
    {
        m_List = new List<E>();
        m_Dic = new Dictionary<int, E>();
        LoadData();
    }

    #region 单例
    private static T _instance;

    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new T();

            }
            return _instance;
        }
    }

    #endregion

    protected abstract string FileName { get; }

    protected abstract E MakeEntity(GameDataTableParser parser);
    /// <summary>
    /// 加载数据
    /// </summary>
    private void LoadData()
    {
        using (GameDataTableParser parser = new GameDataTableParser("DataTable/" + FileName))
        {
      
            while (!parser.Eof)
            {
                E e = MakeEntity(parser);
                if (!m_Dic.ContainsKey(e.Id))
                {
                    m_List.Add(e);
                    m_Dic.Add(e.Id, e);
                }
                parser.Next();
            }
        }

    }

    public List<E> GetList()
    {
        return m_List;
    }

    public E Get(int id)
    {
        return m_Dic[id];
    }



}
