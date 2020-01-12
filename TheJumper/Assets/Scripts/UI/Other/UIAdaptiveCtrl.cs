using UnityEngine;
using System.Collections;

public class UIAdaptiveCtrl : MonoBehaviour
{

    [SerializeField]
    private Camera m_Camera;

	// Use this for initialization
	void Start () {

	    float cameraHeight = m_Camera.orthographicSize * 2;
	    Vector2 cameraSize = new Vector2(m_Camera.aspect * cameraHeight, cameraHeight);
	    Vector2 spriteSize = Vector2.one;

	    Vector2 scale = transform.localScale;
	    if (cameraSize.x >= cameraSize.y)
	    {
	        scale *= cameraSize.x / spriteSize.x;
	    }
	    else
	    {
	        scale *= cameraSize.y / spriteSize.y;
	    }
	    transform.localScale = scale * 1.1f;
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
