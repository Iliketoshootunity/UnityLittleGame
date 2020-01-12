using UnityEngine;
using System.Collections;
using EasyFrameWork;
using EasyFrameWork.UI;
using System;
using System.Collections.Generic;
using LFrameWork.Sound;

namespace EasyFrameWork
{
    public class UIGameLevelSelectView : UIWindowViewBase
    {
        [SerializeField]
        private Transform m_Grid;
        [SerializeField]
        private Transform m_LineParent;

        private Transform m_LastLevel;

        private List<Transform> m_LevelList;
        protected override void OnBtnClick(GameObject go)
        {
            if (go.name == "btnClose")
            {
                UIDispatcher.Instance.Dispatc(ConstDefine.GameLevelSelectClickReturnBtn, null);
            }
            else if (go.name == "btnClear")
            {
                UIDispatcher.Instance.Dispatc(ConstDefine.GameLevelSelectClickClearBtn, null);
            }

            EazySoundManager.PlayUISound(Global.Instance.UISound);

        }
        public void SetUI(DataTransfer data)
        {
            m_LevelList = new List<Transform>();
            int maxPassLevel = data.GetData<int>(ConstDefine.MaxPassLevel);
            int maxLevel = data.GetData<int>(ConstDefine.MaxLevel);
            for (int i = 0; i < maxLevel; i++)
            {
                //item
                GameObject go = ResourcesMrg.Instance.Load(ResourcesMrg.ResourceType.UIWindowChild, "GameLevelItem", isCache: true);
                go.transform.SetParent(m_Grid);
                DataTransfer d = new DataTransfer();
                d.SetData(ConstDefine.GameLevelItem_Level, i + 1);
                if (i + 1 <= maxPassLevel)
                {
                    d.SetData(ConstDefine.GameLevelItem_IsLock, false);
                }
                else
                {
                    d.SetData(ConstDefine.GameLevelItem_IsLock, true);
                }
                UIGameLevelItemView itemView = go.GetComponent<UIGameLevelItemView>();
                itemView.OnClickBtn = OnClickGameLevelItemBtn;
                go.transform.localScale = Vector3.one;
                itemView.SetUI(d);
                m_LevelList.Add(go.transform);


                m_LastLevel = go.transform;
            }

            StartCoroutine("CreateLine", maxLevel);
        }

        private IEnumerator CreateLine(int maxLevel)
        {
            yield return null;
            for (int i = 0; i < maxLevel; i++)
            {
                if (i + 7 >= maxLevel) yield break;
                GameObject go = ResourcesMrg.Instance.Load(ResourcesMrg.ResourceType.UIWindowChild, "VLine", isCache: true);
                Vector3 v1 = m_LevelList[i].transform.position;
                Vector3 v2 = m_LevelList[i + 7].transform.position;
                go.transform.SetParent(m_LineParent);
                go.transform.position = (v2 - v1) / 2 + v1;
                go.transform.localScale = Vector3.one;
            }
        }

        private void OnClickGameLevelItemBtn(int obj)
        {
            object[] objs = new object[1];
            objs[0] = obj;
            UIDispatcher.Instance.Dispatc(ConstDefine.GameLevelSelectViewClickItmeBtn, objs);
        }
    }
}
