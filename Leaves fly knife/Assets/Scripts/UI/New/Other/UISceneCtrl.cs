using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EasyFrameWork.UI
{
    /// <summary>
    /// ����UI������
    /// </summary>

    public class UISceneCtrl : Singleton<UISceneCtrl>
    {

        public UISceneViewBase CurrentUIScene;

        public GameObject Load(UISceneType type)
        {
            GameObject go = null;
            //ע��������ʽҪ��ö��һ�� UI Root_Init
            go = ResourcesMrg.Instance.Load(ResourcesMrg.ResourceType.UIScene,
                string.Format("UI Root_{0}", type.ToString()));
            CurrentUIScene = go.GetComponent<UISceneViewBase>();
            return go;
        }

        /// <summary>
        /// ��Ļ����ת��������
        /// </summary>
        public Vector3 TransferScreenToWorldPosition(Vector2 screenPosition)
        {
            if (CurrentUIScene.UICamera == null)
            {
                return Camera.main.ScreenToWorldPoint(screenPosition);
            }
            else
            {
                if (CurrentUIScene.UICamera.orthographic)
                {
                    return CurrentUIScene.UICamera.ScreenToWorldPoint(screenPosition);
                }
                else
                {
                    //��������
                    Vector3 viewPos = CurrentUIScene.UICamera.ScreenToViewportPoint(screenPosition);
                    //��������������
                    Vector3 worldPos = CurrentUIScene.MainCanvans.transform.position;
                    //�����ĸ߶�
                    float worldScreenHeight = Mathf.Tan(CurrentUIScene.UICamera.fieldOfView * Mathf.PI * 0.002777778f) * CurrentUIScene.MainCanvans.planeDistance * 2f;
                    //��ߵľ���
                    float rightDis = (viewPos.x - 0.5f) * worldScreenHeight * Screen.width / (float)Screen.height;
                    //�ұߵľ���
                    float upDis = (viewPos.y - 0.5f) * worldScreenHeight;
                    //��������
                    worldPos += CurrentUIScene.UICamera.transform.up * upDis + CurrentUIScene.UICamera.transform.right * rightDis;
                    return worldPos;
                }
            }

        }

    }
}
