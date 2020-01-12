using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EasyFrameWork.UI
{
    /// <summary>
    /// 场景UI控制器
    /// </summary>

    public class UISceneCtrl : Singleton<UISceneCtrl>
    {

        public UISceneViewBase CurrentUIScene;

        public GameObject Load(UISceneType type)
        {
            GameObject go = null;
            //注意命名格式要和枚举一致 UI Root_Init
            go = ResourcesMrg.Instance.Load(ResourcesMrg.ResourceType.UIScene,
                string.Format("UI Root_{0}", type.ToString()));
            CurrentUIScene = go.GetComponent<UISceneViewBase>();
            return go;
        }

        /// <summary>
        /// 屏幕坐标转世界坐标
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
                    //窗口坐标
                    Vector3 viewPos = CurrentUIScene.UICamera.ScreenToViewportPoint(screenPosition);
                    //画布的世界坐标
                    Vector3 worldPos = CurrentUIScene.MainCanvans.transform.position;
                    //画布的高度
                    float worldScreenHeight = Mathf.Tan(CurrentUIScene.UICamera.fieldOfView * Mathf.PI * 0.002777778f) * CurrentUIScene.MainCanvans.planeDistance * 2f;
                    //左边的距离
                    float rightDis = (viewPos.x - 0.5f) * worldScreenHeight * Screen.width / (float)Screen.height;
                    //右边的距离
                    float upDis = (viewPos.y - 0.5f) * worldScreenHeight;
                    //世界坐标
                    worldPos += CurrentUIScene.UICamera.transform.up * upDis + CurrentUIScene.UICamera.transform.right * rightDis;
                    return worldPos;
                }
            }

        }

    }
}
