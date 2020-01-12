using UnityEngine;
using System.Collections;
using EasyFrameWork;
using System.Collections.Generic;

namespace EasyFrameWork
{
    [System.Serializable]
    public struct ItemInfo
    {
        public GameObject Prefab;
        public Vector3 IntancePos;
    }
    public class ItemRoot : MonoBehaviour
    {
        [SerializeField]
        private List<ItemInfo> m_ItemInfo = new List<ItemInfo>();
        // Use this for initialization
        void Start()
        {
            if (m_ItemInfo != null && m_ItemInfo.Count > 0)
            {
                for (int i = 0; i < m_ItemInfo.Count; i++)
                {
                    GameObject go = GameObject.Instantiate(m_ItemInfo[i].Prefab);
                    go.transform.parent = this.transform;
                    go.transform.localPosition = m_ItemInfo[i].IntancePos;
                    go.transform.localEulerAngles = Vector3.zero;
                    go.transform.localScale = Vector3.one;
                }
            }
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnDrawGizmos()
        {
            if (m_ItemInfo != null && m_ItemInfo.Count > 0)
            {
                for (int i = 0; i < m_ItemInfo.Count; i++)
                {
                    Gizmos.DrawWireSphere(m_ItemInfo[i].IntancePos + transform.position, 0.1f);
                }
            }
        }
    }
}
