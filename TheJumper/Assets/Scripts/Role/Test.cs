using UnityEngine;
using System.Collections;
using UnityEngineInternal;
using DG.Tweening;

public class Test : MonoBehaviour
{

    public SpriteRenderer[] RS;

    
    public int index;

    private int curIndex
    {
        get { return index % 2; }
    }

    private int preIndex
    {
        get { return 1 - index % 2; }
    }
    // Use this for initialization
    void Start()
    {
        SpriteRenderer s = GetComponent<SpriteRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            //RenderTweening();
        }

        
    }


    public void RenderTweening()
    {
        Color curColor = new Color(1, 1, 1, 1);
        RS[curIndex].DOColor(curColor, 0.5f).SetEase(Ease.Linear).OnComplete(() => index++);
        Color preColor = new Color(1, 1, 1, 0);
        RS[preIndex].DOColor(preColor, 0.5f).SetEase(Ease.Linear);
    }
}
