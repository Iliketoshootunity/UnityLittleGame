using UnityEngine;
using System.Collections;
using EasyFrameWork;
using UnityEngine.UI;
using System;

namespace EasyFrameWork
{
    public class UIChangeSceneView : MonoBehaviour
    {
        [SerializeField]
        private Image m_Image;

        public void Show(ChangeSceneType changeType, float time = 1, Action onEnd = null)
        {

            if (changeType == ChangeSceneType.Open)
            {
                m_Image.color = new Color(0, 0, 0, 1);
                m_Image.DoColor(new Color(0, 0, 0, 0), time).SetEndAction(() => { if (onEnd != null) { onEnd(); } });
            }
            else if (changeType == ChangeSceneType.Close)
            {
                m_Image.color = new Color(0, 0, 0, 0);
                m_Image.DoColor(new Color(0, 0, 0, 1), time).SetEndAction(() => { if (onEnd != null) { onEnd(); } });
            }
            else
            {

            }

        }

        public void SetEnable(bool isEnable)
        {
            m_Image.raycastTarget = isEnable;
        }

    }
}
