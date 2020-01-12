using UnityEngine;
using System.Collections;
using EasyFrameWork;
using System.Collections.Generic;

namespace EasyFrameWork
{
    //扩张方法
    public static class PhyExt
    {

        public static bool Contains(this LayerMask mask, int layer)
        {
            return ((mask.value & (1 << layer)) > 0);
        }
        static List<Component> m_ComponentCache = new List<Component>();

        public static T GetComponentNoAlloc<T>(this GameObject @this) where T : Component
        {
            @this.GetComponents(typeof(T), m_ComponentCache);
            var component = m_ComponentCache.Count > 0 ? m_ComponentCache[0] : null;
            m_ComponentCache.Clear();
            return component as T;
        }
    }
}
