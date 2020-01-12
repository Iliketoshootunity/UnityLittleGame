using UnityEngine;
using System.Collections;
using System.Net.Mime;
using UnityEngine.UI;
using System;

public class UIGameLevelSkillView : UISubViewBase
{
    public static UIGameLevelSkillView Instance;

    public Action OnClickSkillButton;
    [SerializeField]
    private Image m_SkillImage;
    [SerializeField]
    private Sprite m_AbsorbSprite;
    [SerializeField]
    private Sprite m_ReleaseSprite;

    protected override void OnAwake()
    {
        Instance = this;
    }

    protected override void OnStart()
    {
        EventTriggerListener.Get(this.gameObject).onClick = OnClick;
    }

    protected override void BeforeOnDestory()
    {
        m_AbsorbSprite = null;
        m_ReleaseSprite = null;
        m_SkillImage = null;
        EventTriggerListener.Get(this.gameObject).onClick = null;
    }

    private void OnClick(GameObject go)
    {
        if (OnClickSkillButton != null)
        {
            OnClickSkillButton();
        }
    }
    /// <summary>
    /// 改变按钮类型 如果是吸收 则改为释放 如果是释放 则改为吸收
    /// </summary>
    public void ChangeSkillType(bool isAbsorb)
    {
        if (isAbsorb)
        {
            m_SkillImage.sprite = m_AbsorbSprite;
        }
        else
        {
            m_SkillImage.sprite = m_ReleaseSprite;
        }
    }

}
