using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIViewMgr : Singleton<UIViewMgr>
{

    private Dictionary<UIViewType, ISystemCtrl> m_Dic = new Dictionary<UIViewType, ISystemCtrl>();

    public UIViewMgr()
    {

        ////关卡页面 控制层和视图层的映射关系
        m_Dic.Add(UIViewType.GameLevelPause, GameLevelCtrl.Instance);
        m_Dic.Add(UIViewType.GameLevelFail, GameLevelCtrl.Instance);
        m_Dic.Add(UIViewType.GameLevelHelp, GameLevelCtrl.Instance);
    }

    public void OpenView(UIViewType type)
    {
        if (!m_Dic.ContainsKey(type)) return;
        m_Dic[type].OpenView(type);
    }
}
