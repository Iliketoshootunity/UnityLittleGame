using UnityEngine;
using System.Collections;
using EasyFrameWork;
using EasyFrameWork.UI;
using UnityEngine.UI;
using System;

namespace EasyFrameWork
{
    public class UIGameSceneView : UISceneViewBase
    {
        [SerializeField]
        private Slider m_TimeSlider;
        [SerializeField]
        private Image m_Star1;
        [SerializeField]
        private Image m_Star2;
        [SerializeField]
        private Image m_Star3;
        private float m_Time = 10;
        private float m_Timer = 10;
        private bool m_Star1Change;
        private bool m_Star2Change;
        private bool m_Star3Change;

        public Action<int> OnStarValue;
        protected override void OnStart()
        {
            base.OnStart();
            m_TimeSlider.onValueChanged.AddListener(OnTimeValueChange);

        }
        private void Update()
        {
            m_Timer -= Time.deltaTime;
            m_Timer = Mathf.Clamp(m_Timer, 0, m_Time);
            float process = m_Timer / m_Time;
            m_TimeSlider.value = process;
        }
        private void OnTimeValueChange(float arg0)
        {
            if (arg0 < 0.8f && arg0 > 0.5f)
            {
                if (!m_Star1Change)
                {
                    m_Star1Change = true;
                    m_Star1.color = Color.blue;
                    if (OnStarValue != null)
                    {
                        OnStarValue(2);
                    }
                }
            }
            else if (arg0 > 0.3f && arg0 < 0.5f)
            {
                if (!m_Star2Change)
                {
                    m_Star2Change = true;
                    m_Star2.color = Color.blue;
                    if (OnStarValue != null)
                    {
                        OnStarValue(1);
                    }
                }
            }
            else if (arg0 < 0.2f)
            {
                if (!m_Star3Change)
                {
                    m_Star3Change = true;
                    m_Star3.color = Color.blue;
                    if (OnStarValue != null)
                    {
                        OnStarValue(0);
                    }
                }
            }
        }

        protected override void OnBtnClick(GameObject go)
        {
            base.OnBtnClick(go);
            if (go.name == "btnPause")
            {
                UIDispatcher.Instance.Dispatc(ConstDefine.Pause, null);
            }
            else if (go.name == "btnReturn")
            {
                object[] p = new object[1];
                p[0] = "Level";
                UIDispatcher.Instance.Dispatc(ConstDefine.NextScene, p);
            }
        }
        public void SetUI()
        {
            if (OnStarValue != null)
            {
                OnStarValue(3);
            }
        }


    }
}
