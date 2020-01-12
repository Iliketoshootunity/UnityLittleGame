using UnityEngine;
using System.Collections;
using EasyFrameWork;

namespace EasyFrameWork
{
    public class SpriteAnimator : MonoBehaviour
    {
        public SpriteRenderer SpriteRenderer;
        public Sprite[] SpriteArr;
        public bool IsAutoPlay = true;
        public bool DestoryOnPlayOver = true;
        public float FPS;
        private float m_Time;
        private float m_Timer;
        private int m_Index = 0;
        private bool m_IsPlay;
        // Use this for initialization
        void Start()
        {
            m_Time = 1 / FPS;
            if (IsAutoPlay)
            {
                PlayAni();
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (m_IsPlay)
            {
                m_Timer += Time.deltaTime;
                if (m_Timer > m_Time)
                {
                    m_Timer = 0;
                    m_Index++;
                    if (m_Index > SpriteArr.Length - 1)
                    {
                        if (DestoryOnPlayOver)
                        {
                            Destroy(this.gameObject);
                            return;
                        }
                        m_Index = 0;
                        StopAni();
                        return;
                    }
                    SpriteRenderer.sprite = SpriteArr[m_Index];
                }
            }
        }

        public void PlayAni()
        {
            m_IsPlay = true;
            if (SpriteArr == null || SpriteArr.Length <= 0)
            {
                m_IsPlay = false;
                return;
            }
            if (SpriteRenderer == null)
            {
                SpriteRenderer = GetComponent<SpriteRenderer>();
                if (SpriteRenderer == null)
                {
                    m_IsPlay = false;
                    return;
                }
            }
            SpriteRenderer.sprite = SpriteArr[0];
            m_Index = 0;
        }
        public void StopAni()
        {
            m_IsPlay = false;
        }
    }
}
