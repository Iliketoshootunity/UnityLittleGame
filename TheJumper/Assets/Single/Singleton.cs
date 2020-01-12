using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Singleton<T> : IDisposable where T : new()
{
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

    public virtual void Dispose()
    {

    }
}
