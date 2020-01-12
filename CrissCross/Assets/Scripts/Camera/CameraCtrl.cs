using UnityEngine;
using System.Collections;
using EasyFrameWork;

namespace EasyFrameWork
{
    public class CameraCtrl : MonoBehaviour
    {

        // Use this for initialization
        void Start()
        {

            Vector2 rect = GameLevelDBModel.Instance.GetRowAndColumn(Global.Instance.CurLevel);
            float rowLength = rect.x * Global.Instance.RowInterval; ;
            float columnLength = rect.y * Global.Instance.ColumnInterval; ;
            transform.position = new Vector3(columnLength / 2, -rowLength / 2, -20);
            float heightSize1 = rowLength / 2;
            float weightSize = columnLength / 2;
            float heightSize2 = Screen.height / (float)Screen.width * weightSize;
            if (heightSize1 > heightSize2)
            {
                Camera.main.orthographicSize = heightSize1 + heightSize1 / 10;
            }
            else
            {
                Camera.main.orthographicSize = heightSize2 + heightSize2 / 10;
            }

        }

    }
}
