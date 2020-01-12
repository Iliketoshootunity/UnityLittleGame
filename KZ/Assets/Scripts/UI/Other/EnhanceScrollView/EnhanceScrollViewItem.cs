using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[System.Serializable]
public class EnhanceScrollViewItem : MonoBehaviour, IDragHandler, IEndDragHandler, IPointerClickHandler
{

    //起始索引
    [SerializeField]
    private int m_StartIndex = 0;
    public int StartIndex
    {
        get { return m_StartIndex; }
        set { m_StartIndex = value; }
    }

    //实时索引
    [SerializeField]
    private int m_RealIndex;
    public int RealIndex
    {
        get { return m_RealIndex; }
        set { m_RealIndex = value; }
    }

    //相对于0.5f
    [SerializeField]
    private float m_LocalSliderValue;

    public float LocalSliderValue
    {
        get { return m_LocalSliderValue; }
        set { m_LocalSliderValue = value; }
    }
    //
    [SerializeField]
    private float m_SliderValue;

    public float SliderValue
    {
        get { return m_SliderValue; }
        set { m_SliderValue = value; }
    }

    private EnhanceScrollView m_ScrollViewCtrl;
    private Image m_Image;
    private RectTransform m_Rect;
    private Vector2 m_PrePos;

    public void Init(EnhanceScrollView ctrl)
    {
        m_ScrollViewCtrl = ctrl;
    }

    //更新在ScrollView的位置
    public void UpdateInScrollView(float xValue, float yValue, float scaleValue, float depthValue, int depthFactor, float itemCount)
    {
        if (m_Rect == null)
        {
            m_Rect = GetComponent<RectTransform>();
        }
        Vector3 targetPos = Vector3.one;
        Vector3 targetScale = Vector3.one;
        // 位置
        targetPos.x = xValue;
        targetPos.y = yValue;
        m_Rect.anchoredPosition3D = targetPos;
        //深度
        SetItemDepth(depthValue, depthFactor, itemCount);
        //缩放
        targetScale.x = targetScale.y = scaleValue;
        transform.localScale = targetScale;
    }
    //设置选中状态
    public void SetSelectState(bool isCenter)
    {
        if (m_Image == null)
            m_Image = GetComponent<Image>();
        m_Image.color = isCenter ? Color.white : Color.gray;
    }
    private void SetItemDepth(float depthCurveValue, int depthFactor, float itemCount)
    {
        int newDepth = (int)(depthCurveValue * itemCount);
        this.transform.SetSiblingIndex(newDepth);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (m_PrePos == Vector2.zero)
        {
            m_PrePos = eventData.position;
            return;
        }
        m_ScrollViewCtrl.OnDrag(eventData.position - m_PrePos);
        m_PrePos = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        m_PrePos = Vector2.zero;
        m_ScrollViewCtrl.OnDragEnd();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        print("Click");

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        m_ScrollViewCtrl.SetCenterItem(this);
    }
}
