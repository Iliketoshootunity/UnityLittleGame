using UnityEngine;
using System.Collections;
using EasyFrameWork;
using LFramework.Plugins.Astart;

namespace EasyFrameWork
{
    public class CameraCtrl : MonoBehaviour
    {

        // Use this for initialization
        IEnumerator Start()
        {
            yield return null;
            Vector2 rect = new Vector2(8, 14);
            float rowLength = rect.x * GridManager.Instance.RowInterval; ;
            float columnLength = rect.y * GridManager.Instance.ColumnInterval;
            float heightSize1 = rowLength / 2;
            float weightSize = columnLength / 2;
            float heightSize2 = Screen.height / (float)Screen.width * weightSize;
            if (heightSize1 > heightSize2)
            {
                Camera.main.orthographicSize = heightSize1 + heightSize1 / 5;
            }
            else
            {
                Camera.main.orthographicSize = heightSize2 + heightSize2 / 5;
            }
            transform.position = new Vector3(columnLength / 2, -rowLength / 2, -20);
            Vector3 viewPos = Camera.main.WorldToViewportPoint(GridManager.Instance.GetCell(0, 0).transform.position);
            float dis = 0;
            if (viewPos.y > 0.8f)
            {
                float v = viewPos.y - 0.8f;
                dis = v * Screen.height / 100f;
            }
            transform.position = new Vector3(columnLength / 2, -rowLength / 2 + dis, -20);

        }

    }
}
