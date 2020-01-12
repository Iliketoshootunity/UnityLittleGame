using UnityEngine;
using System.Collections;
using EasyFrameWork;
using System.Collections.Generic;

namespace EasyFrameWork
{
    public class NormalAttackEffect : AttackEffectBase
    {
        protected List<SpriteAnimator> m_SpriteAniList;
        public override void Play()
        {
            base.Play();
            if (m_SpriteAniList == null)
            {
                m_SpriteAniList = new List<SpriteAnimator>();
            }
            for (int i = 0; i < m_SingleEffectList.Count; i++)
            {
                m_SpriteAniList.Add(m_SingleEffectList[i].GetComponentInChildren<SpriteAnimator>());
            }
            for (int i = 0; i < m_SpriteAniList.Count; i++)
            {
                m_SpriteAniList[i].PlayAni();
            }
        }
        public override void PlayOver()
        {
            base.PlayOver();
            GameObject.Destroy(this.gameObject);
        }
    }
}
