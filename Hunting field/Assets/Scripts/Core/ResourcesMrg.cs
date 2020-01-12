using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using EasyFrameWork;

namespace EasyFrameWork
{
    public class ResourcesMrg : Singleton<ResourcesMrg>
    {


        #region 资源类型
        /// <summary>
        /// 资源类型
        /// </summary>
        public enum ResourceType
        {
            /// <summary>
            /// 场景UI
            /// </summary>
            UIScene,
            /// <summary>
            /// 窗口UI
            /// </summary>
            UIWindow,
            /// <summary>
            /// 子窗口UI
            /// </summary>
            UIWindowChild,
            /// <summary>
            /// 其他UI
            /// </summary>
            UIOther,
            /// <summary>
            /// 角色
            /// </summary>
            Role,
            /// <summary>
            /// 特效
            /// </summary>
            Effect,
            /// <summary>
            /// 场景物体
            /// </summary>
            Scene,

            Item,

        }
        #endregion

        /// <summary>
        /// 资源加载到缓存中
        /// </summary>
        private Hashtable prefabsCache;

        public ResourcesMrg()
        {
            prefabsCache = new Hashtable();
        }

        /// <summary>
        /// 加载资源
        /// </summary>
        /// <param name="type">资源类型</param>
        /// <param name="path">短路径</param>
        /// <param name="isCache">是否缓存到内存中</param>
        /// <returns></returns>
        public GameObject Load(ResourceType type, string path, bool isCache = false, bool isClone = true)
        {

            GameObject obj = null;
            if (prefabsCache.Contains(path))
            {
                obj = prefabsCache[path] as GameObject;
            }
            else
            {
                StringBuilder sbr = new StringBuilder();
                switch (type)
                {
                    case ResourceType.UIScene:
                        sbr.Append("UIPerfabs/UIScenes/");
                        break;
                    case ResourceType.UIWindow:
                        sbr.Append("UIPerfabs/UIWindows/");
                        break;
                    case ResourceType.Role:
                        sbr.Append("RolePerfabs/");
                        break;
                    case ResourceType.Effect:
                        sbr.Append("EffectPrefabs/");
                        break;
                    case ResourceType.UIOther:
                        sbr.Append("UIPerfabs/UIOther/");
                        break;
                    case ResourceType.UIWindowChild:
                        sbr.Append("UIPerfabs/UIWindwoChild/");
                        break;
                    case ResourceType.Scene:
                        sbr.Append("Scenes/");
                        break;
                    case ResourceType.Item:
                        sbr.Append("Items/");
                        break;
                    default:
                        break;
                }
                sbr.Append(path);
                obj = Resources.Load<GameObject>(sbr.ToString());
                if (isCache)
                {
                    prefabsCache.Add(path, obj);
                }

            }

            if (obj == null)
            {
                Debug.LogError("资源加载路径错误");
            }
            else
            {
                if (isClone)
                {
                    obj = GameObject.Instantiate<GameObject>(obj);
                }

            }

            return obj;
        }

        public override void Dispose()
        {
            //清空资源缓存
            prefabsCache.Clear();
            //未使用的资源进行释放
            Resources.UnloadUnusedAssets();
        }

    }
}

