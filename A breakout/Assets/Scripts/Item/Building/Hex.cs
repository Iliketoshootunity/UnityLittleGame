using UnityEngine;
using System.Collections;
using EasyFrameWork;
using System.Collections.Generic;

namespace EasyFrameWork
{
    [System.Serializable]
    public class Hex : MonoBehaviour
    {
        public int Row;                 //行
        public int Column;              //列
        public Sprite PlayerSprite;
        public Sprite MonsterSprite;
        public Sprite MixTureSprite;
        public Sprite NormalSprite;
        private int m_Count;
        public SpriteRenderer m_SpriteRenderer;
        [SerializeField]
        private List<int> m_LigeanceTypeList;
        private void Start()
        {
            m_LigeanceTypeList = new List<int>();
            Wipe();
        }

        public void Enter(bool isPlayer)
        {
            if (isPlayer)
            {
                m_LigeanceTypeList.Add(1);
            }
            else
            {
                m_LigeanceTypeList.Add(2);
            }
        }
        public void Level(bool isPlayer)
        {
            if (isPlayer)
            {
                if (m_LigeanceTypeList.Contains(1))
                {
                    m_LigeanceTypeList.Remove(1);
                }

            }
            else
            {
                if (m_LigeanceTypeList.Contains(2))
                {
                    m_LigeanceTypeList.Remove(2);
                }

            }
        }

        public void Show()
        {
            if (m_LigeanceTypeList.FindAll(x => x == 1).Count > 0 && m_LigeanceTypeList.FindAll(x => x == 2).Count > 0)
            {
                m_SpriteRenderer.sprite = MixTureSprite;
            }
            else if (m_LigeanceTypeList.FindAll(x => x == 1).Count > 0)
            {
                m_SpriteRenderer.sprite = PlayerSprite;
            }
            else if (m_LigeanceTypeList.FindAll(x => x == 2).Count > 0)
            {
                m_SpriteRenderer.sprite = MonsterSprite;
            }
            else
            {
                m_SpriteRenderer.sprite = NormalSprite;
            }

        }



        public void Wipe()
        {
            m_SpriteRenderer.sprite = NormalSprite;
        }
    }
}
