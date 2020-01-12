using UnityEngine;
using System.Collections;
using UnityEngineInternal;

/// <summary>
/// 节点
/// </summary>
public class Node : MonoBehaviour
{

    //左子节点
    private Node _LNode;
    //右子节点
    private Node _RNode;
    //父节点
    private Node _PNode;


    public Node()
    {

    }

    public Node LNode
    {
        get { return _LNode; }
        set { _LNode = value; }
    }

    public Node RNode
    {
        get { return _RNode; }
        set { _RNode = value; }
    }
    public Node PNode
    {
        get { return _PNode; }
        set { _PNode = value; }
    }

}
