using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace EasyFrameWork.UI
{
    /// <summary>
    /// UI������ͼ����
    /// </summary>
    public class UISceneViewBase : UIViewBase
    {
        /// <summary>
        /// �м䲿λ����
        /// </summary>
        public Transform ContainerCenter;

        /// <summary>
        /// �м䲿λ����
        /// </summary>
        public Transform ContainerLeftBottom;
        /// <summary>
        /// HUD
        /// </summary>
        //public bl_HUDText HUD;
        [SerializeField]
        private Canvas m_MainCanvans;

        public Canvas MainCanvans { get { if (m_MainCanvans == null) { m_MainCanvans = GetComponentInChildren<Canvas>(); } return m_MainCanvans; } }
        [SerializeField]
        private Camera m_UICamera;
        public Camera UICamera { get { if (m_UICamera == null) { m_UICamera = MainCanvans.worldCamera; } return m_UICamera; } }

    }
}

