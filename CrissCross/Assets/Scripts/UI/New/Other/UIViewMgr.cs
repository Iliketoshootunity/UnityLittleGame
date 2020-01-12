using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace EasyFrameWork.UI
{

    public class UIViewMgr : Singleton<UIViewMgr>
    {

        private Dictionary<UIViewType, ISystemCtrl> m_Dic = new Dictionary<UIViewType, ISystemCtrl>();

        public UIViewMgr()
        {

            ////�ؿ�ҳ�� ���Ʋ����ͼ���ӳ���ϵ
            m_Dic.Add(UIViewType.GameLevelSelect, GameLevelCtrl.Instance);
            m_Dic.Add(UIViewType.Win, GameLevelCtrl.Instance);
            m_Dic.Add(UIViewType.Pause, GameLevelCtrl.Instance);

        }

        public void OpenView(UIViewType type)
        {
            if (!m_Dic.ContainsKey(type)) return;
            m_Dic[type].OpenView(type);
        }
    }
}