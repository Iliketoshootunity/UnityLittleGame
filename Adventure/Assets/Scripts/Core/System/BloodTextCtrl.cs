using UnityEngine;
using System.Collections;
using EasyFrameWork;
using System.Collections.Generic;

namespace EasyFrameWork
{

    public class BloodTextCtrl : SystemCtrlBase<BloodTextCtrl>
    {
        private List<GameObject> m_BloodPool = new List<GameObject>();

        public void Blood(Vector2 pos, int hurtValue)
        {
            GameObject go = null;
            if (m_BloodPool.Count > 0)
            {
                go = m_BloodPool[0];
                m_BloodPool.RemoveAt(0);
            }
            else
            {
                go = ResourcesMrg.Instance.Load(ResourcesMrg.ResourceType.UIWindowChild, "BloodText", isCache: true);
            }
            BloodText bloodText = go.GetComponent<BloodText>();
            bloodText.BloodTextAni(pos, hurtValue);
        }
        public void PushPool(GameObject go)
        {
            m_BloodPool.Add(go);
        }

    }
}
