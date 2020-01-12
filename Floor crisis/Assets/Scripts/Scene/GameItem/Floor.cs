using UnityEngine;
using System.Collections;
using EasyFrameWork;
using System.Collections.Generic;

namespace EasyFrameWork
{
    /// <summary>
    /// 层
    /// </summary>
	public class Floor : MonoBehaviour
    {
        private Collider2D m_Collider;
        private Platform[] m_PlatformArr;
        private int m_LayerCount;
        [SerializeField]
        private SpriteRenderer m_BGRenderer;
        [SerializeField]
        private SpriteRenderer m_LayerBGRenderer;
        [SerializeField]
        private List<SpriteRenderer> m_LayerThreeRendererList;
        [SerializeField]
        private List<SpriteRenderer> m_LayerTwoRendererList;

        [SerializeField]
        private GameObject m_LayerThreeRendererObj;
        [SerializeField]
        private GameObject m_LayerTwoRendererObj;
        // Use this for initialization
        void Start()
        {
            if (m_Collider == null)
            {
                m_Collider = transform.Find("Body").GetComponentInChildren<BoxCollider2D>();
            }
            for (int i = 0; i < 17; i++)
            {
                GameObject go = ResourcesMgr.Instance.Load(ResourcesMgr.ResourceType.Item, "Floor/Platform", isCache: true);
                go.transform.SetParent(transform);
                go.transform.localPosition = new Vector3(i * 0.5f, 0, 0);
                go.transform.localScale = Vector3.one;
                go.GetComponent<Platform>().OnRoleStand = OnRoleStand;
            }
            m_PlatformArr = GetComponentsInChildren<Platform>();
            SetAllPlatformCollider(false);
            FloorBGInfo info = Global.Instance.FloorBGInfoList[Random.Range(0, Global.Instance.FloorBGInfoList.Count)];

            m_BGRenderer.sprite = info.BGSprite;
            m_LayerBGRenderer.sprite = info.LayerSprite;

            List<Sprite> layerList = info.GetLayerNumber(m_LayerCount);
            if (layerList.Count == 1)
            {
                m_LayerThreeRendererList[0].enabled = false;

                m_LayerThreeRendererList[1].enabled = true;
                m_LayerThreeRendererList[1].sprite = layerList[0];

                m_LayerThreeRendererList[2].enabled = false;

                m_LayerThreeRendererObj.gameObject.SetActive(true);
                m_LayerTwoRendererObj.gameObject.SetActive(false);

            }
            else if (layerList.Count == 2)
            {
                m_LayerTwoRendererList[0].enabled = true;
                m_LayerTwoRendererList[0].sprite = layerList[0];

                m_LayerTwoRendererList[1].enabled = true;
                m_LayerTwoRendererList[1].sprite = layerList[1];
                m_LayerThreeRendererObj.gameObject.SetActive(false);
                m_LayerTwoRendererObj.gameObject.SetActive(true);
            }
            else
            {
                for (int i = 0; i < m_LayerThreeRendererList.Count; i++)
                {
                    m_LayerThreeRendererList[i].enabled = true;
                    m_LayerThreeRendererList[i].sprite = layerList[i];
                }
                m_LayerThreeRendererObj.gameObject.SetActive(true);
                m_LayerTwoRendererObj.gameObject.SetActive(false);
            }
        }

        // Update is called once per frame
        void Update()
        {
            Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
            if (pos.y < -1f)
            {
                Destroy(this.gameObject);
            }
        }

        public void Init(int layerCount)
        {
            m_LayerCount = layerCount;
        }

        private void OnRoleStand()
        {
            SetAllPlatformCollider(true);
        }

        public void SetAllPlatformCollider(bool isActive)
        {
            if (m_Collider == null)
            {
                m_Collider = transform.Find("Body").GetComponentInChildren<BoxCollider2D>();
            }
            m_Collider.isTrigger = !isActive;
        }
    }
}
