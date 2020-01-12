using UnityEngine;
using System.Collections;
using EasyFrameWork;
using EasyFrameWork.UI;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;
using LFrameWork.Sound;

namespace EasyFrameWork
{
    public class UIGameSceneView : UISceneViewBase
    {
        [SerializeField]
        private Text m_KnifeCount;
        [SerializeField]
        private Text m_Level;
        [SerializeField]
        private Image m_Star;
        [SerializeField]
        private Sprite m_StarSprite;
        [SerializeField]
        private RectTransform m_StarTemp;
        [SerializeField]
        private RectTransform m_Rocker;
        [SerializeField]
        private RectTransform m_RockerBG;
        [SerializeField]
        private float m_Time = 1.5f;
        [SerializeField]
        private float G = -98f;
        private Sprite m_GetStar;
        private Sprite m_EmptyStar;
        [SerializeField]
        private AudioClip m_GetStarClip;

        protected override void OnStart()
        {
            base.OnStart();
        }
        protected override void OnBtnClick(GameObject go)
        {
            base.OnBtnClick(go);
            if (go.name == "btnPause")
            {
                object[] obj1 = new object[1];
                obj1[0] = true;
                UIDispatcher.Instance.Dispatc(ConstDefine.Pause, obj1);
            }
            else if (go.name == "btnRestart")
            {
                UIDispatcher.Instance.Dispatc(ConstDefine.Restart, null);
            }
        }

        public void SetUI(int knifeCount, int level)
        {
            m_KnifeCount.text = knifeCount.ToString();
            m_Level.text = level.ToString();
        }

        public void GetStar(Vector3 pos)
        {
            EazySoundManager.PlayUISound(m_GetStarClip);
            Vector3 viewPos = Camera.main.WorldToScreenPoint(pos);
            Vector2 p = Vector3.zero;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(m_Star.transform.parent.GetComponent<RectTransform>(), viewPos, UICamera, out p);
            m_StarTemp.gameObject.SetActive(true);
            m_StarTemp.anchoredPosition = p;
            Vector2 relativePos = m_Star.GetComponent<RectTransform>().anchoredPosition - p;
            Vector2 speed = Vector2.zero;
            //Y值自由落体
            speed.y = ((2 * relativePos.y) / m_Time - G * m_Time) / 2;
            //x值平行移动
            speed.x = relativePos.x / m_Time;
            StartCoroutine(StarMove(speed));
        }
        private IEnumerator StarMove(Vector2 Speed)
        {
            float timer = 0;
            bool isRun = true;
            float vo = Speed.y;
            while (isRun)
            {
                yield return null;
                timer += Time.deltaTime;
                if (timer > m_Time)
                {
                    isRun = false;
                    m_StarTemp.gameObject.SetActive(false);
                    m_Star.sprite = m_StarSprite;
                    m_Star.transform.DoScale(Vector3.one * 1.1f, 0.5f).SetLoopCount(1).SetLoopType(TweenLoopType.YoYo);
                    yield break;
                }
                float sx = Speed.x * Time.deltaTime;
                float vt = vo + G * Time.deltaTime;
                vo = vt;
                float sy = vt * Time.deltaTime;
                m_StarTemp.anchoredPosition += new Vector2(sx, sy);

            }
        }

        public void SetRockerPos(Vector2 screenPos)
        {
            RectTransform rect = ContainerCenter.GetComponent<RectTransform>();
            Vector2 pos = Vector3.zero;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(rect, screenPos, UICamera, out pos);
            m_Rocker.anchoredPosition3D = pos;

        }

    }
}
