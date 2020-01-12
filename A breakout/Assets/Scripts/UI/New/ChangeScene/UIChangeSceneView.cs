using UnityEngine;
using System.Collections;
using EasyFrameWork;
using System;
using UnityEngine.UI;
namespace EasyFrameWork
{
    public enum ChangeSceneType
    {
        Open,
        Close,
        All
    }
    public class UIChangeSceneView : MonoBehaviour
    {
        [SerializeField]
        private RectTransformPositionAnimation m_Animation1;
        [SerializeField]
        private RectTransformPositionAnimation m_Animation2;
        [SerializeField]
        private Image m_Image1;
        [SerializeField]
        private Image m_Image2;
        [SerializeField]
        private Image m_Image3;
        public void Show(ChangeSceneType changeType = ChangeSceneType.Open, Action onEnd = null)
        {
            StartCoroutine(ShowIE(changeType, onEnd));
        }

        private IEnumerator ShowIE(ChangeSceneType changeType = ChangeSceneType.Open, Action onEnd = null)
        {
            SetImage(true);
            float time = 0;
            if (changeType == ChangeSceneType.Close)
            {
                m_Animation1.RectTransformPositionTweener.IsFrom = false;
                m_Animation2.RectTransformPositionTweener.IsFrom = false;
            }
            else if (changeType == ChangeSceneType.Open)
            {
                m_Animation1.RectTransformPositionTweener.IsFrom = true;
                m_Animation2.RectTransformPositionTweener.IsFrom = true;
            }
            else
            {
                m_Animation1.RectTransformPositionTweener.IsFrom = true;
                m_Animation1.RectTransformPositionTweener.LoopMaxCount = 1;
                m_Animation1.RectTransformPositionTweener.LoopType = TweenLoopType.YoYo;

                m_Animation2.RectTransformPositionTweener.IsFrom = false;
                m_Animation2.RectTransformPositionTweener.LoopMaxCount = 1;
                m_Animation2.RectTransformPositionTweener.LoopType = TweenLoopType.YoYo;

                time = m_Animation1.RectTransformPositionTweener.Variable;
            }
            m_Animation1.Play();
            m_Animation2.Play();
            yield return new WaitForSeconds(m_Animation1.RectTransformPositionTweener.Variable);
            if (onEnd != null)
            {
                onEnd();
            }
            yield return new WaitForSeconds(time);
            SetImage(false);
        }

        public void SetImage(bool isEnable)
        {
            m_Image1.enabled = isEnable;
            m_Image2.enabled = isEnable;
            m_Image3.enabled = isEnable;
        }

    }
}
