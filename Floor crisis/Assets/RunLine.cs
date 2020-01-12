using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RunLine : MonoBehaviour
{
    [SerializeField]
    private float m_IntervalTime = 0.1f;

    [SerializeField]
    private int m_MaxCount = 1000;
    private float timer;
    private List<Vector3> points = new List<Vector3>();
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnDrawGizmos()
    {
        if (points == null)
        {
            points = new List<Vector3>();
        }
        timer += Time.deltaTime;
        if (timer > m_IntervalTime)
        {
            timer = 0;
            points.Add(transform.position);
            if (points.Count >= m_MaxCount)
            {
                points.RemoveAt(0);
            }
        }

        for (int i = 0; i < points.Count; i++)
        {
            Vector3 endPos=Vector3.back;
            if (i == points.Count - 1)
            {
                return;
            }
            endPos = points[i + 1];
            Gizmos.DrawLine(points[i], points[i + 1]);
        }
    }
}
