using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine.UI;
using DG.Tweening;
using LFrameWork.Sound;

public class BGCtrl : MonoSingleton<BGCtrl>
{


    private Vector3 m_OriginScreenPoint;
    private Vector3 m_OrgionWorldPoint;
    private bool m_IsInit = false;
    private SpriteRenderer[] rs;
    [SerializeField]
    private Sprite[] m_SpriteArr;

    private Dictionary<string, Sprite> m_DicSprite;
    private int index;

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
        rs = GetComponentsInChildren<SpriteRenderer>();
        SpriteRenderer r1 = transform.Find("SR1").GetComponent<SpriteRenderer>();
        SpriteRenderer r2 = transform.Find("SR2").GetComponent<SpriteRenderer>();
        Material m_Material1 = new Material(rs[0].material);
        Material m_Material2 = new Material(rs[1].material);
        rs[0].material = m_Material1;
        rs[1].material = m_Material2;



        float cameraHeight = Camera.main.orthographicSize * 2;
        Vector2 cameraSize = new Vector2(Camera.main.aspect * cameraHeight, cameraHeight);
        Vector2 spriteSize = r1.sprite.bounds.size;

        Vector2 scale = transform.localScale;
        if (cameraSize.x >= cameraSize.y)
        {
            scale *= cameraSize.x / spriteSize.x;
        }
        else
        {
            scale *= cameraSize.y / spriteSize.y;
        }
        r1.transform.localScale = scale * 1.1f;
        r2.transform.localScale = scale * 1.1f;

        m_DicSprite = new Dictionary<string, Sprite>();
        for (int i = 0; i < m_SpriteArr.Length; i++)
        {
            m_DicSprite.Add(m_SpriteArr[i].name, m_SpriteArr[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (GameLevelSceneCtrl.Instance.GameStatus == GameStatus.Playing)
        {
            if (!m_IsInit)
            {
                m_IsInit = true;
                m_OriginScreenPoint =
                    Camera.main.WorldToScreenPoint(GlobalInit.Instance.CurRoleCtrl.transform.position);
            }

            if (m_IsInit)
            {
                m_OrgionWorldPoint = Camera.main.ScreenToWorldPoint(m_OriginScreenPoint);
                float x = (GlobalInit.Instance.CurRoleCtrl.transform.position.x - m_OrgionWorldPoint.x) / Screen.width;
                float y = (GlobalInit.Instance.CurRoleCtrl.transform.position.y - m_OrgionWorldPoint.y) / Screen.height;
                Vector2 offset1 = rs[0].material.GetTextureOffset("_MainTex");
                Vector2 offset2 = rs[1].material.GetTextureOffset("_MainTex");
                rs[0].material.SetTextureOffset("_MainTex", new Vector2(offset1.x, offset1.y + y));
                rs[1].material.SetTextureOffset("_MainTex", new Vector2(offset2.x, offset2.y + y));
            }
        }

    }

    private void RenderTweening(float tweenTime = 1)
    {
        Color curColor = new Color(1, 1, 1, 1);
        rs[curIndex].DOColor(curColor, tweenTime).SetEase(Ease.Linear).OnComplete(() => index++);
        Color preColor = new Color(1, 1, 1, 0);
        rs[preIndex].DOColor(preColor, tweenTime).SetEase(Ease.Linear);
    }

    public void ToNext(string name, float tweenTime = 1)
    {
        Sprite s = m_DicSprite[name];
        if (s == null)
        {
            Debug.LogError("not find bg");
        }
        rs[curIndex].sprite = s;
        RenderTweening(tweenTime);
    }



}
