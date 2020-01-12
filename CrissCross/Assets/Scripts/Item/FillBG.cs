using UnityEngine;
using System.Collections;
using EasyFrameWork;
using System.Collections.Generic;

namespace EasyFrameWork
{
    public class FillBG : MonoBehaviour
    {

        [SerializeField]
        private List<SpriteRenderer> m_SpriteRendererList;
        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void ToBottom()
        {
            for (int i = 0; i < m_SpriteRendererList.Count; i++)
            {
                m_SpriteRendererList[i].sortingOrder = -2;
                m_SpriteRendererList[i].transform.Find("s").GetComponent<SpriteRenderer>().sortingOrder = -2;
            }
        }
    }
}
