using LFramework.Plugins.Astart;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "AstarBackInfo01", menuName = "AstarBackInfo")]
public class AstarBakeInfo : ScriptableObject
{
    [Header("网格")]
    public int NumOfRow;                    //行
    public int NumOfColumn;                 //列

    public string GroundMask;
    public string ObstacleMask;
    public Node[,] nodes;
    public float GridCellSize;              //网格单元大小
    public Transform OriginTransform;

    private void OnEnable()
    {
        int index = 0;
        for (int i = 0; i < NumOfRow; i++)
        {
            for (int j = 0; j < NumOfColumn; j++)
            {

                Vector3 rayOrigin = GetPosition(index);
                RaycastHit hit;
                float lastY = 0;
                if (Physics.Raycast(rayOrigin, rayOrigin - Vector3.up * 2000, out hit, 2000, 1 << LayerMask.NameToLayer(ObstacleMask) | 1 << LayerMask.NameToLayer(GroundMask)))
                {
                    nodes[i, j].Position = hit.point;
                    if (hit.collider.gameObject.layer == LayerMask.NameToLayer(ObstacleMask))
                    {
                        nodes[i, j].MaskAsObstacle();
                        lastY = hit.point.y;
                    }
                    else if (hit.collider.gameObject.layer == LayerMask.NameToLayer(GroundMask))
                    {
                        lastY = hit.point.y;
                    }
                    else
                    {
                        nodes[i, j].Position = new Vector3(nodes[i, j].Position.x, lastY, nodes[i, j].Position.z);
                    }
                }
                index++;
            }
        }
    }

    /// <summary>
    /// 根据系列号得到坐标
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    private Vector3 GetPosition(int index)
    {
        int row = GetRow(index);
        int column = GetColumn(index);
        return new Vector3(column * GridCellSize + GridCellSize / 2, 0, row * GridCellSize + GridCellSize / 2);

    }
    /// <summary>
    /// 获得行
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    private int GetRow(int index)
    {
        return (int)(index / NumOfRow);
    }
    /// <summary>
    /// 获得列
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    private int GetColumn(int index)
    {
        return index % NumOfColumn;
    }
}
