using UnityEngine;
using System.Collections;
using EasyFrameWork;
using System.Collections.Generic;

namespace EasyFrameWork
{
    public class RecordEditor : MonoBehaviour
    {

        private List<GameObject> m_Obstacle;
        // Use this for initialization
        void Start()
        {
            m_Obstacle = new List<GameObject>();
            GameObject[] gos1 = GameObject.FindGameObjectsWithTag("Obstacle");
            string obstacleStr = "";
            for (int i = 0; i < gos1.Length; i++)
            {
                int index = gos1[i].name.Substring(gos1[i].name.Length - 1).ToInt();
                obstacleStr += (gos1[i].transform.position.x + "_" + gos1[i].transform.position.y + "_" + index.ToString());
                if (i < gos1.Length - 1)
                {
                    obstacleStr += "|";
                }
            }
            Debug.Log(obstacleStr);
            GameObject[] gos2 = GameObject.FindGameObjectsWithTag("Monster");
            string monsterStr = "";
            for (int i = 0; i < gos2.Length; i++)
            {
                monsterStr += (gos2[i].transform.position.x + "_" + gos2[i].transform.position.y );
                if (i < gos2.Length - 1)
                {
                    monsterStr += "|";
                }
            }
            Debug.Log(monsterStr);
            GameObject[] gos3 = GameObject.FindGameObjectsWithTag("Player");
            string playerStr = "";
            for (int i = 0; i < gos2.Length; i++)
            {
                playerStr += (gos3[i].transform.position.x + "_" + gos3[i].transform.position.y);
                if (i < gos3.Length - 1)
                {
                    playerStr += "|";
                }
            }
            Debug.Log(playerStr);
            GameObject[] gos4 = GameObject.FindGameObjectsWithTag("OverPoint");
            string overPointStr = "";
            for (int i = 0; i < gos2.Length; i++)
            {
                overPointStr += (gos4[i].transform.position.x + "_" + gos4[i].transform.position.y);
                if (i < gos4.Length - 1)
                {
                    overPointStr += "|";
                }
            }
            Debug.Log(overPointStr);

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
