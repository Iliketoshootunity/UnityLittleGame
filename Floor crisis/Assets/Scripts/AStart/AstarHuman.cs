using UnityEngine;
using System.Collections;
using LFramework.Plugins.Astart;

public class AstarHuman : MonoBehaviour
{
    ArrayList list;

    private bool isMove;

    private int index = 0;

    private Node startNode;

    private Node endNode;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButton(0))
        {
            list = new ArrayList();
            list.Clear();
            Vector3 position = Vector3.back;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                position = hit.point;
            }
            startNode = GridManager.Instance.GetNode(transform.position);
            endNode = GridManager.Instance.GetNode(position);
            list = AStar.FindPath(startNode, endNode);
            //isMove = true;
        }

        if (isMove)
        {
            transform.position = Vector3.Lerp(transform.position, ((Node)list[index]).Position, Time.deltaTime * 3);
            if (Vector3.Distance(transform.position, ((Node)list[index]).Position) < 0.1f)
            {
                index++;
                //if (index == list.Count - 1)
                //{
                //    isMove = false;
                //    index = 0;

                //}
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        if (list != null)
        {
            for (int i = 0; i < list.Count - 1; i++)
            {
                Vector3 startPos = ((Node)list[i]).Position;
                Vector3 endPos = ((Node)list[i + 1]).Position;
                Gizmos.DrawLine(startPos, endPos);
            }


        }

        if (startNode != null)
        {
            Gizmos.DrawSphere(startNode.Position, 0.1f);
        }

        if (endNode != null)
        {
            Gizmos.DrawSphere(endNode.Position, 0.1f);
        }
    }
}
