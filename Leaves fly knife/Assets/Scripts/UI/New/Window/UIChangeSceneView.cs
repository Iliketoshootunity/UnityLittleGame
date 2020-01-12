using UnityEngine;
using System.Collections;
using EasyFrameWork;
using System;
using UnityEngine.UI;

namespace EasyFrameWork
{
    public class UIChangeSceneView : MonoBehaviour
    {
        [SerializeField]
        private float m_Time;
        [SerializeField]
        private RawImage m_Image;
        private Material m_Mat;

        public void Show(ChangeSceneType changeType, Action onEnd)
        {
            if (m_Mat == null)
            {
                m_Mat = new Material(m_Image.material);
                m_Image.material = m_Mat;
            }
            float startValue = 0;
            float endValue = 1;
            int loopCount = 0;
            switch (changeType)
            {
                case ChangeSceneType.Open:
                case ChangeSceneType.OpenAndClose:
                    if (changeType == ChangeSceneType.OpenAndClose)
                    {
                        loopCount = 1;
                    }
                    startValue = 1;
                    endValue = 0;
                    break;
                case ChangeSceneType.Close:
                case ChangeSceneType.CloseAndOpen:
                    if (changeType == ChangeSceneType.CloseAndOpen)
                    {
                        loopCount = 1;
                    }
                    startValue = 0;
                    endValue = 1;
                    break;
                default:
                    break;
            }
            float number = 0;
            m_Mat.SetFloat("_Amount", startValue);
            TweenManager.To(() => number = startValue, (x) => number = x, endValue, m_Time).SetIsSpeed(false).SetUpdateAction((x) =>
            {
                m_Mat.SetFloat("_Amount", number);
            }).SetLoopCount(loopCount).SetLoopType(TweenLoopType.YoYo).SetEndAction(() => { if (onEnd != null) { onEnd(); } });
        }

    }
}
