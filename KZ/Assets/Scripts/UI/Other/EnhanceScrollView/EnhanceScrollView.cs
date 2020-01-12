using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 增强的滚轮，为环形滚轮准备
/// </summary>
public class EnhanceScrollView : MonoBehaviour
{
    //缩放动画
    public AnimationCurve ScaleCurve;
    //位置曲线
    public AnimationCurve PositionCurve;
    //深度曲线
    public AnimationCurve DepthCurve = new AnimationCurve(new Keyframe(0, 0), new Keyframe(0.5f, 1), new Keyframe(1, 0));
    //固定的y值
    public float YFixedPositionValue = 46;
    //起始中心物体索引
    public int StartCenterIndex;
    //物品的宽度
    public float CellWidth = 10f;
    public float LerpTime = 0.2f;
    //横轴总宽度
    private float mTotalHorizontalWidth;

    private float mOriginHorizontalSliderValue = 0.1f;
    //当前SliderValue
    private float m_CurHorizontalSliderValue;

    private float mCurTweenTimer = 0;
    //当前中间位置的物体
    private EnhanceScrollViewItem mCurCenterItem;
    //之前中间位置的物体
    private EnhanceScrollViewItem mPreCenterItem;
    //深度因子
    private int mDepthFactor = 5;
    //拖拽因子
    public float DragFactor = 0.0001f;
    //能否移动物体
    private bool mChangeItem = true;
    //是否开启Tween
    private bool mEnbaleLerpTween = true;
    //间隔因子
    private float mIntervalFactor = 0;
    //中间的x值
    private float m_CenterXValue = 0;
    private bool m_IsRightDrag;
    /// <summary>
    /// 中间的索引
    /// </summary>
    private int m_CenterIndex = 0;

    public List<EnhanceScrollViewItem> mItemList;
    //排序后的
    private List<EnhanceScrollViewItem> mSortItemList;
    private void Start()
    {
        //规则 极左-极右（0-1）

        mChangeItem = true;
        int count = mItemList.Count;
        m_CenterIndex = count / 2;
        if (count % 2 == 0) m_CenterIndex = count / 2 - 1;
        mIntervalFactor = (Mathf.RoundToInt((1f / count) * 10000f)) * 0.0001f;
        int index = 0;
        for (int i = 0; i < count; i++)
        {
            mItemList[i].StartIndex = i;
            mItemList[i].LocalSliderValue = mIntervalFactor * (m_CenterIndex + index);
            mItemList[i].SetSelectState(false);
            mItemList[i].Init(this);
            index++;
        }
        //越界的情况下设置中心的item
        if (StartCenterIndex < 0 || StartCenterIndex > count)
        {
            StartCenterIndex = m_CenterIndex;
        }
        //排序
        mSortItemList = new List<EnhanceScrollViewItem>(mItemList.ToArray());
        mTotalHorizontalWidth = CellWidth * count;
        mCurCenterItem = mSortItemList[StartCenterIndex];
        m_CurHorizontalSliderValue = 0.5f - mCurCenterItem.LocalSliderValue;
        m_CenterXValue = PositionCurve.Evaluate(0.5f) * mTotalHorizontalWidth;
        PrepareTween(0, m_CurHorizontalSliderValue, false);
    }

    private void Update()
    {
        if (mEnbaleLerpTween)
        {
            TweenToTarget();
        }
    }


    //播放前的预处理
    private void PrepareTween(float originValue, float targetValue, bool needTween = false)
    {
        originValue = (Mathf.RoundToInt((originValue) * 10000f)) * 0.0001f;
        targetValue = (Mathf.RoundToInt((targetValue) * 10000f)) * 0.0001f;
        mOriginHorizontalSliderValue = originValue;
        m_CurHorizontalSliderValue = targetValue;
        if (needTween)
        {
            mCurTweenTimer = 0;
        }
        else
        {
            SortEnhanceItem();
            originValue = targetValue;
            UpdateScrollView(targetValue);
            OnSelectedOver();
        }
        mEnbaleLerpTween = needTween;
    }
    //播放到目标
    private void TweenToTarget()
    {
        mCurTweenTimer += Time.deltaTime;
        if (mCurTweenTimer >= LerpTime)
        {
            mCurTweenTimer = LerpTime;
        }
        float progress = mCurTweenTimer / LerpTime;
        float value = Mathf.Lerp(mOriginHorizontalSliderValue, m_CurHorizontalSliderValue, progress);
        UpdateScrollView(value);
        if (mCurTweenTimer >= LerpTime)
        {
            mChangeItem = true;
            mEnbaleLerpTween = false;
            OnSelectedOver();
        }
    }
    //更新界面,sliderValue为整个界面的滑动条值，思路是通过整个界面的滑动条值，计算子界面的滑动条值
    //sliderValue 为整个界面的sliderValue,大小无限制
    private void UpdateScrollView(float sliderValue)
    {
        for (int i = 0; i < mItemList.Count; i++)
        {
            EnhanceScrollViewItem item = mItemList[i];
            float childSliderVaue = sliderValue + item.LocalSliderValue;
            item.SliderValue = childSliderVaue;
            float xPosValue = GetXPosValue(childSliderVaue);
            float scaleValue = GetScaleValue(childSliderVaue);
            float depthValue = GetDepthValue(childSliderVaue);
            item.UpdateInScrollView(xPosValue, YFixedPositionValue, scaleValue, depthValue, mDepthFactor, mItemList.Count);
        }
    }

    //获取缩放动画的值
    private float GetScaleValue(float sliderValue)
    {
        float scaleValue = ScaleCurve.Evaluate(sliderValue);
        return scaleValue;
    }
    //获取位置动画的值
    private float GetXPosValue(float sliderValue)
    {
        float xPosValue = PositionCurve.Evaluate(sliderValue) * mTotalHorizontalWidth;
        return xPosValue;
    }
    //获取深度动画的值
    private float GetDepthValue(float sliderValue)
    {
        float depthValue = DepthCurve.Evaluate(sliderValue);
        return depthValue;
    }

    //根据x值排序
    private void SortEnhanceItem()
    {
        mSortItemList.Sort((a, b) =>
        {
            return a.transform.localPosition.x.CompareTo(b.transform.localPosition.x);
        });
        for (int i = mSortItemList.Count - 1; i >= 0; i--)
            mSortItemList[i].RealIndex = i;
    }
    //选择完成
    private void OnSelectedOver()
    {
        if (mPreCenterItem != null)
        {
            mPreCenterItem.SetSelectState(false);
        }
        if (mCurCenterItem != null)
        {
            mCurCenterItem.SetSelectState(true);
        }
    }
    //设置中间
    public void SetCenterItem(EnhanceScrollViewItem selectItem)
    {
        if (!mChangeItem) return;
        if (mCurCenterItem == selectItem) return;
        mChangeItem = false;
        mPreCenterItem = mCurCenterItem;
        mCurCenterItem = selectItem;
        bool isRight = false;
        if (selectItem.transform.localPosition.x > m_CenterXValue)
        {
            isRight = true;
        }
        SortEnhanceItem();
        int moveIndex = Mathf.Abs(mPreCenterItem.RealIndex - selectItem.RealIndex);
        float intervalValue = 0.0f;
        if (isRight)
        {
            intervalValue = -moveIndex * mIntervalFactor;
        }
        else
        {
            intervalValue = moveIndex * mIntervalFactor;
        }
        PrepareTween(m_CurHorizontalSliderValue, m_CurHorizontalSliderValue + intervalValue, true);
    }
    //拖拽中
    public void OnDrag(Vector2 delta)
    {
        if (Mathf.Abs(delta.x) > 0.0f)
        {
            float target = m_CurHorizontalSliderValue + delta.x * DragFactor;
            PrepareTween(0, target, false);
        }
    }
    //拖拽结束
    public void OnDragEnd()
    {
        float minDis = Mathf.Infinity;
        int minIndex = 0;
        for (int i = 0; i < mItemList.Count; i++)
        {
            float distance = Mathf.Abs(mItemList[i].transform.localPosition.x - m_CenterXValue);
            if (distance < minDis)
            {
                minDis = distance;
                minIndex = i;
            }
        }
        float target = 0;
        EnhanceScrollViewItem item = mItemList[minIndex];
        float remainder = item.SliderValue % mIntervalFactor;
        bool isEven = mItemList.Count % 2 == 0;
        float start = m_CurHorizontalSliderValue;
        //在中心的右边
        if (item.transform.localPosition.x > m_CenterXValue)
        {
            if (m_CurHorizontalSliderValue > 0)
            {
                if (!isEven)
                {
                    start = m_CurHorizontalSliderValue + mIntervalFactor / 2;
                }

                target = start + (mIntervalFactor - m_CurHorizontalSliderValue % mIntervalFactor) - mIntervalFactor;
            }
            else
            {
                if (!isEven)
                {
                    start = m_CurHorizontalSliderValue + mIntervalFactor / 2;
                }
                target = start - (mIntervalFactor + m_CurHorizontalSliderValue % mIntervalFactor);
            }
        }
        else//左边
        {
            if (m_CurHorizontalSliderValue > 0)
            {
                if (!isEven)
                {
                    start = m_CurHorizontalSliderValue - mIntervalFactor / 2;
                }
                target = start - m_CurHorizontalSliderValue % mIntervalFactor + mIntervalFactor;
            }
            else
            {
                if (!isEven)
                {
                    start = m_CurHorizontalSliderValue - mIntervalFactor / 2;
                }
                target = start - m_CurHorizontalSliderValue % mIntervalFactor;
            }

        }
        mPreCenterItem = mCurCenterItem;
        mCurCenterItem = item;
        PrepareTween(m_CurHorizontalSliderValue, target, true);
        mChangeItem = false;
    }
}
