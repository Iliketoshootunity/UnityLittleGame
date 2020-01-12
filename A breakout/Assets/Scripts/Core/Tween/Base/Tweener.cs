using UnityEngine;
using System.Collections;
using EasyFrameWork;
using System;
using System.Collections.Generic;

namespace EasyFrameWork
{
    public abstract class Tweener : Tween
    {
        /// <summary>
        ///初始化准备
        /// </summary>
        protected abstract void InitPrepare();
        /// <summary>
        /// 循环准备
        /// </summary>
        protected abstract void LoopPrepare();

        private Coroutine c1;
        private Coroutine c2;
        /// <summary>
        /// 执行补间
        /// </summary>
        public void DoTween()
        {
            m_LoopCount = 0;
            c1 = TweenManager.Instance.StartCoroutine(TweenIE());
            TweenManager.Instance.AddCoroutine(Index, c1);
        }

        public IEnumerator TweenIE()
        {
            if (m_IsFirstDelay)
            {
                InitPrepare();
                LoopPrepare();
                yield return new WaitForSeconds(Delay);
                m_IsFirstDelay = false;
            }
            m_Timer = 0;
            LoopPrepare();
            c2 = TweenManager.Instance.StartCoroutine(TweenrExecuteIE());
            TweenManager.Instance.AddCoroutine(Index, c2);
            yield return c2;
            TweenManager.Instance.RemoveCoroutine(Index, c2);
            TweenEnd();
            TweenManager.Instance.RemoveCoroutine(Index, c1);
            c1 = null;
        }
        protected IEnumerator TweenrExecuteIE()
        {
            bool isRun = true;
            while (isRun)
            {
                m_Timer += Time.deltaTime;
                float process1 = m_Timer / Variable;
                if (process1 >= 1)
                {
                    process1 = 1;
                    isRun = false;
                }
                float process2 = Curve.Evaluate(process1);
                UpdateTween(process2);
                yield return null;
            }
        }

        protected abstract void UpdateTween(float process);


        /// <summary>
        /// 补间结束
        /// </summary>
        private void TweenEnd()
        {
            if (LoopType != TweenLoopType.None)
            {
                if (LoopMaxCount == -1)
                {
                    if (LoopType == TweenLoopType.YoYo)
                    {
                        IsFrom = !IsFrom;
                    }
                    c1 = TweenManager.Instance.StartCoroutine(TweenIE());
                    TweenManager.Instance.AddCoroutine(Index, c1);
                }
                else
                {
                    if (m_LoopCount < LoopMaxCount)
                    {
                        m_LoopCount++;
                        if (LoopType == TweenLoopType.YoYo)
                        {
                            IsFrom = !IsFrom;
                        }
                        c1 = TweenManager.Instance.StartCoroutine(TweenIE());
                        TweenManager.Instance.AddCoroutine(Index, c1);
                    }
                    else
                    {
                        if (EndAction != null)
                        {
                            EndAction();
                        }
                    }
                }
            }
            else
            {
                if (EndAction != null)
                {
                    EndAction();
                }
            }
        }
        public Tweener(float variable)
        {
            variable = Variable;
            Index = ++TweenManager.Index;

            Delay = 0;
            LoopMaxCount = 0;
            LoopType = TweenLoopType.None;
            Curve = Ease.GetAnimationCurveByCurve(EaseType.Linner);
            IsFrom = false;
            IsAutoPlay = false;

        }
        public Tweener()
        {
            Index = ++TweenManager.Index;

            Delay = 0;
            LoopMaxCount = 0;
            LoopType = TweenLoopType.None;
            Curve = Ease.GetAnimationCurveByCurve(EaseType.Linner);
            IsFrom = false;
            IsAutoPlay = false;
        }
    }
}
