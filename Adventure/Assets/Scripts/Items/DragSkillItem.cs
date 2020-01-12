using UnityEngine;
using System.Collections;
using EasyFrameWork;
using System.Collections.Generic;

namespace EasyFrameWork
{
    public class DragSkillItem : MonoBehaviour
    {
        public RoleCtrl Master;
        private List<GameObject> m_CellPoolList;
        private List<GameObject> m_CellList;
        public void Init(RoleCtrl master, int skillType, int range)
        {
            Master = master;
            if (m_CellPoolList == null)
            {
                m_CellPoolList = new List<GameObject>();
            }
            if (m_CellList == null)
            {
                m_CellList = new List<GameObject>();
            }

            List<Vector2> posArr = GameSceneCtrl.Instance.GetPositionsByShape(skillType, Vector2.zero, range);
            for (int i = 0; i < posArr.Count; i++)
            {
                GameObject go = null;
                if (m_CellPoolList.Count > 0)
                {
                    go = m_CellPoolList[0];
                    m_CellPoolList.RemoveAt(0);
                    go.SetActive(true);
                }
                else
                {
                    go = ResourcesMrg.Instance.Load(ResourcesMrg.ResourceType.Scene, "Items/SkillCell", isCache: true);
                }
                go.transform.SetParent(this.transform);
                go.transform.localPosition = posArr[i];
            }

        }

        /// <summary>
        ///刷新位置之后
        /// </summary>
        public void AfterRefreshPositio()
        {
            Master.AttackOrigin = transform.position;
            //寻找敌人
            //TODO
        }

        public void Hide()
        {
            int count = m_CellList.Count;
            for (int i = 0; i < count; i++)
            {
                GameObject go = m_CellList[0];
                m_CellList.RemoveAt(0);
                go.SetActive(false);
                m_CellPoolList.Add(go);
            }
        }
    }
}
