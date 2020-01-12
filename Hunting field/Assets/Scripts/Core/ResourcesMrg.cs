using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using EasyFrameWork;

namespace EasyFrameWork
{
    public class ResourcesMrg : Singleton<ResourcesMrg>
    {


        #region ��Դ����
        /// <summary>
        /// ��Դ����
        /// </summary>
        public enum ResourceType
        {
            /// <summary>
            /// ����UI
            /// </summary>
            UIScene,
            /// <summary>
            /// ����UI
            /// </summary>
            UIWindow,
            /// <summary>
            /// �Ӵ���UI
            /// </summary>
            UIWindowChild,
            /// <summary>
            /// ����UI
            /// </summary>
            UIOther,
            /// <summary>
            /// ��ɫ
            /// </summary>
            Role,
            /// <summary>
            /// ��Ч
            /// </summary>
            Effect,
            /// <summary>
            /// ��������
            /// </summary>
            Scene,

            Item,

        }
        #endregion

        /// <summary>
        /// ��Դ���ص�������
        /// </summary>
        private Hashtable prefabsCache;

        public ResourcesMrg()
        {
            prefabsCache = new Hashtable();
        }

        /// <summary>
        /// ������Դ
        /// </summary>
        /// <param name="type">��Դ����</param>
        /// <param name="path">��·��</param>
        /// <param name="isCache">�Ƿ񻺴浽�ڴ���</param>
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
                Debug.LogError("��Դ����·������");
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
            //�����Դ����
            prefabsCache.Clear();
            //δʹ�õ���Դ�����ͷ�
            Resources.UnloadUnusedAssets();
        }

    }
}

