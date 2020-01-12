using UnityEngine;
using System.Collections;
using EasyFrameWork;

namespace EasyFrameWork
{
    public class TweenDelegate
    {

    }

    public delegate T DoGetter<out T>();
    public delegate void DoSetter<in T>(T pNewValue);
}
