using UnityEngine;
using System.Collections;
using EasyFrameWork;
using System;
using System.Collections.Generic;
using System.Diagnostics;
namespace EasyFrameWork
{
    public abstract class Tweener : Tween
    {
        protected float m_Timer;
        protected int m_LoopCount;
        protected bool m_IsFirstDelay = true;
        /// <summary>
        /// 协程变化事件 补间，增加or减少 协程
        /// </summary>
        public Action<Tween, bool, Coroutine> OnCoroutineChangeAction;
        /// <summary>
        ///初始化准备
        /// </summary>
        protected abstract void InitPrepare();
        /// <summary>
        /// 循环准备
        /// </summary>
        protected abstract void LoopPrepare();

        protected abstract void UpdateTween(float process);


        private Coroutine c1;
        private Coroutine c2;
        /// <summary>
        /// 执行补间
        /// </summary>
        public void DoTween()
        {
            c1 = TweenManager.Instance.StartCoroutine(TweenIE());
            if (OnCoroutineChangeAction != null)
            {
                OnCoroutineChangeAction(this, true, c1);
            }
            IsPlay = true;
        }
        public IEnumerator TweenIE()
        {
            if (m_IsFirstDelay)
            {
                m_IsFirstDelay = false;
                if (StartAction != null)
                {
                    StartAction();
                }
                InitPrepare();
                LoopPrepare();
                UpdateTween(0);
                yield return new WaitForSeconds(Delay);
            }
            m_Timer = 0;
            LoopPrepare();
            c2 = TweenManager.Instance.StartCoroutine(TweenrExecuteIE());
            if (OnCoroutineChangeAction != null)
            {
                OnCoroutineChangeAction(this, true, c2);
            }
            yield return c2;
            if (OnCoroutineChangeAction != null)
            {
                OnCoroutineChangeAction(this, false, c1);
                OnCoroutineChangeAction(this, false, c2);
            }
            TweenEnd();
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
                    if (UpdateAction != null)
                    {
                        UpdateAction(1);
                    }
                    UpdateTween(1);
                    isRun = false;
                    yield return null;
                    yield break;
                }
                float process2 = Curve.Evaluate(process1);
                UpdateTween(process2);
                if (UpdateAction != null)
                {
                    UpdateAction(process2);
                }
                yield return null;
            }
        }


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
                    if (OnCoroutineChangeAction != null)
                    {
                        OnCoroutineChangeAction(this, true, c1);
                    }

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
                        if (OnCoroutineChangeAction != null)
                        {
                            OnCoroutineChangeAction(this, true, c1);
                        }
                    }
                    else
                    {
                        if (EndAction != null)
                        {
                            EndAction();
                        }
                        if (IsAutoRelease)
                        {
                            TweenManager.Instance.RemoveTween(this);
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
                if (IsAutoRelease)
                {
                    TweenManager.Instance.RemoveTween(this);
                }
            }
        }

        protected virtual void SetComponent(Component component)
        {
        }
        public Tweener(float variable)
        {
            variable = Variable;
            Index = ++TweenManager.Index;
            Delay = TweenManager.DefaultDelay;
            LoopMaxCount = TweenManager.DefaultLoopMaxCount;
            LoopType = TweenManager.DefaultTweenLoopType;
            Curve = Ease.GetAnimationCurveByCurve(TweenManager.DefaultEasyType);
            IsFrom = TweenManager.DefaultIsForm;
            IsAutoPlay = TweenManager.DefaultIsAutonPlay;
            IsAutoRelease = TweenManager.DefaultIsAutoRelease;
            m_LoopCount = 0;
            m_IsFirstDelay = true;

        }
        public Tweener()
        {
            Index = ++TweenManager.Index;
            //Delay = TweenManager.DefaultDelay;
            //LoopMaxCount = TweenManager.DefaultLoopMaxCount;
            //LoopType = TweenManager.DefaultTweenLoopType;
            //Curve = Ease.GetAnimationCurveByCurve(TweenManager.DefaultEasyType);
            //IsFrom = TweenManager.DefaultIsForm;
            //IsAutoPlay = TweenManager.DefaultIsAutonPlay;
            IsAutoRelease = TweenManager.DefaultIsAutoRelease;
            m_LoopCount = 0;
            m_IsFirstDelay = true;
        }
    }
}
