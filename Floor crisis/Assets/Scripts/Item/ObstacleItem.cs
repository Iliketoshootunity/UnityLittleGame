using UnityEngine;
using System.Collections;
using EasyFrameWork;

namespace EasyFrameWork
{
    public class ObstacleItem : MonoBehaviour, IObstacle, IBottomWeakness, ITopWeakness
    {
        public int CoinCount;

        public bool Break()
        {
            if (CoinCount > 1)
            {
                for (int i = 0; i < CoinCount; i++)
                {
                    GameObject go = ResourcesMgr.Instance.Load(ResourcesMgr.ResourceType.Item, "Coin/Coin2", isCache: true);
                    go.transform.position = transform.position;
                    go.transform.rotation = Quaternion.identity;
                    go.transform.localScale = Vector3.one;
                    Destroy(this.gameObject);
                }
            }
            else
            {
                GameObject go = ResourcesMgr.Instance.Load(ResourcesMgr.ResourceType.Item, "Coin/Coin1", isCache: true);
                go.transform.position = transform.position + new Vector3(0, 0.25f, 0);
                go.transform.rotation = Quaternion.identity;
                go.transform.localScale = Vector3.one;
                Destroy(this.gameObject);
            }

            GameObject go1 = ResourcesMgr.Instance.Load(ResourcesMgr.ResourceType.Effect, "DeadEffect", isCache: true);
            go1.transform.position = transform.position + new Vector3(0, 0.4f, 0);
            go1.transform.rotation = Quaternion.identity;
            go1.transform.localScale = Vector3.one;
            return false;
        }
    }
}
