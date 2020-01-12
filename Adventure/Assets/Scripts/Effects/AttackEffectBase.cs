using UnityEngine;
using System.Collections;
using EasyFrameWork;
using System.Collections.Generic;

namespace EasyFrameWork
{
    public class AttackEffectBase : EffectBase
    {
        public bool NeedTarget = false; //需要目标
        public bool ShowOne = false;
        protected string m_EffectName;
        protected string m_EffectSingleName;
        protected RoleCtrl m_Master;
        protected List<RoleCtrl> m_EnemyList;
        protected List<Vector2> m_PosList;
        protected bool m_IsPlayOver;
        protected Vector2 m_ShowOnePos;
        protected List<GameObject> m_SingleEffectList;
        public void Init(RoleCtrl master, string effectName, List<RoleCtrl> enemyList, List<Vector2> posList)
        {
            m_SingleEffectList = new List<GameObject>();
          
            m_EnemyList = enemyList;
            m_PosList = posList;
            m_EffectName = effectName;
            m_EffectSingleName = m_EffectName + "_Single";
            m_Master = master;
            if (ShowOne)
            {
                //通用只显示一个特效的位置
                Vector2 totalPos = Vector2.zero;
                for (int i = 0; i < posList.Count; i++)
                {
                    totalPos += posList[i];
                }
                m_ShowOnePos = totalPos / posList.Count;
                CreateSingleEffect(m_ShowOnePos);
            }
            else
            {
                if (NeedTarget)
                {
                    for (int i = 0; i < enemyList.Count; i++)
                    {
                        CreateSingleEffect(enemyList[i].transform.position);
                    }
                }
                else
                {
                    for (int i = 0; i < posList.Count; i++)
                    {
                        CreateSingleEffect(posList[i]);
                    }
                }
            }

        }
        public virtual GameObject CreateSingleEffect(Vector2 pos)
        {
            GameObject go = ResourcesMrg.Instance.Load(ResourcesMrg.ResourceType.Effect, m_EffectSingleName, isCache: true);
            go.transform.SetParent(this.transform);
            go.transform.position = pos;
            m_SingleEffectList.Add(go);
            return go;
        }
    }
}
