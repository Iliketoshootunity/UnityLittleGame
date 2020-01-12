using UnityEngine;
using System.Collections;


public enum PlatfromType
{
    Normal,
    Hide,
    HideTrap,
    OtherTrap

}

public class Platform : Node
{
    public enum PlatformStatus
    {
        Show,
        Stand,
        Fall
    }
    [HideInInspector]
    public PlatformStatus Status;
    [HideInInspector]
    public PlatformInfo CurPlatformInfo;
    [HideInInspector]
    public RoleCtrl RoleCtrl;
    [HideInInspector]
    public bool BeginFall;
    [HideInInspector]
    public int HideTrapChildCount;

    public PlatfromType PlatfromType;
    public AnimationCurve StartDownCurve;
    public Vector3 StandPos;

    private Color m_BottomColor = new Color(0.35f, 0.35f, 0.35f, 1);
    private Rigidbody m_Rigidbody;
    private Vector3 m_EndPos;
    private Vector3 m_StartPos;
    private float m_Timer = 0;
    private float m_StartDownTime = 0.1f;
    private float m_StandTime;
    private bool m_IsShowHide;
    private SpriteRenderer m_BodyRenderer;
    private SpriteRenderer m_HideBodyRenderer;


    // Use this for initialization
    void Start()
    {
        m_Rigidbody = GetComponentInChildren<Rigidbody>();
        m_BodyRenderer = transform.Find("Body").GetComponent<SpriteRenderer>();
        Transform t = transform.Find("HideBody");
        if (t != null)
        {
            m_HideBodyRenderer = t.GetComponent<SpriteRenderer>();
        }

        if (m_BodyRenderer != null)
        {
            m_BodyRenderer.color = new Color(m_BodyRenderer.color.r, m_BodyRenderer.color.g, m_BodyRenderer.color.b, 1);
        }
        if (m_HideBodyRenderer != null)
        {
            m_HideBodyRenderer.color = new Color(m_HideBodyRenderer.color.r, m_HideBodyRenderer.color.g, m_HideBodyRenderer.color.b, 0);

        }
        m_Rigidbody.isKinematic = true;
        m_Rigidbody.useGravity = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (GameLevelSceneCtrl.Instance.GameStatus == GameStatus.Playing)
        {
            if (Status == PlatformStatus.Show)
            {
                m_Timer += Time.deltaTime;
                float process = m_Timer / m_StartDownTime;
                process = Mathf.Clamp01(process);
                transform.position = Vector3.Lerp(m_StartPos, m_EndPos, StartDownCurve.Evaluate(process));
                if (process == 1)
                {
                    Status = PlatformStatus.Stand;
                    m_Timer = 0;
                }
            }
            else if (Status == PlatformStatus.Stand)
            {
                if (!CheckInCameraRange())
                {
                    m_Timer = m_StandTime;
                }
                if (BeginFall)
                {
                    m_Timer += Time.deltaTime;
                    if (m_Timer > m_StandTime)
                    {
                        ToFall();
                    }
                }

            }
            else if (Status == PlatformStatus.Fall)
            {
                if (!CheckInCameraRange(0))
                {
                    Destroy(this.gameObject);
                }
            }
        }

        BodyColorTweeningAndLayer();

    }
    //startFallTime 比跳跃的时间短一点
    public void Init(Vector3 startPos, Vector3 endPos, float startFallTime, float standTime, PlatformStatus status, PlatformInfo platformInfo, PlatfromType platfromType = PlatfromType.Normal)
    {
        if (status == PlatformStatus.Show)
        {
            transform.position = startPos;
        }
        else
        {
            transform.position = endPos;
        }

        PlatfromType = platfromType;
        CurPlatformInfo = platformInfo;
        m_EndPos = endPos;
        m_StartPos = startPos;
        Status = status;
        m_Timer = 0;
        m_StandTime = standTime;
        m_StartDownTime = startFallTime;
        m_IsShowHide = false;
    }

    public void ShowHideTrap()
    {
        if (m_HideBodyRenderer == null || m_BodyRenderer == null)
        {
            return;
        }
        if (m_IsShowHide)
        {
            return;
        }
        //body 隐藏 hideBody 出现
        m_IsShowHide = true;
        StartCoroutine("ShowHideTrapIE");
    }

    private IEnumerator ShowHideTrapIE()
    {
        m_BodyRenderer.gameObject.SetActive(true);
        m_HideBodyRenderer.gameObject.SetActive(true);
        for (int i = 0; i < 10; i++)
        {
            float process = (i + 1) / (float)10;
            process = Mathf.Clamp01(process);
            float a1 = process;
            float a2 = 1 - process;
            if (m_BodyRenderer != null && m_HideBodyRenderer != null)
            {
                m_BodyRenderer.color = new Color(m_BodyRenderer.color.r, m_BodyRenderer.color.g, m_BodyRenderer.color.b, a2);
                m_HideBodyRenderer.color = new Color(m_HideBodyRenderer.color.r, m_HideBodyRenderer.color.g, m_HideBodyRenderer.color.b, a1);
            }
            yield return null;
        }
        m_BodyRenderer.gameObject.SetActive(false);
        m_HideBodyRenderer.gameObject.SetActive(true);
    }


    private void ToFall()
    {
        if (RoleCtrl != null)
        {
            RoleCtrl.ToDead();
        }
        ChildPlatformBeginFall();
        m_Rigidbody.isKinematic = false;
        m_Rigidbody.velocity = Vector3.down * 7;
        m_Rigidbody.useGravity = true;
        Status = PlatformStatus.Fall;
    }

    private void ChildPlatformBeginFall()
    {
        if (LNode != null)
        {
            ((Platform)LNode).BeginFall = true;
        }
        if (RNode != null)
        {
            ((Platform)RNode).BeginFall = true;
        }


    }

    private bool CheckInCameraRange(float dif = 0.1f)
    {
        Vector3 viewportPos = Camera.main.WorldToViewportPoint(transform.position);
        if (viewportPos.y < dif)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public void BodyColorTweeningAndLayer()
    {
        Vector3 viewportPos = Camera.main.WorldToViewportPoint(transform.position);
        float process = viewportPos.y / 0.5f;
        //位置
        transform.position = new Vector3(transform.position.x, transform.position.y, viewportPos.y);
        //颜色
        process = Mathf.Clamp01(process);
        process = 1 - process;

        if (m_BodyRenderer != null && m_BodyRenderer.gameObject.activeSelf)
        {
            Color c1 = new Color(Color.white.r, Color.white.g, Color.white.b, m_BodyRenderer.color.a);
            Color c2 = new Color(m_BottomColor.r, m_BottomColor.g, m_BottomColor.b, m_BodyRenderer.color.a);
            Color color = Color.Lerp(c1, c2, process);
            m_BodyRenderer.color = color;
        }
        if (m_HideBodyRenderer != null && m_HideBodyRenderer.gameObject.activeSelf)
        {
            Color c1 = new Color(Color.white.r, Color.white.g, Color.white.b, m_HideBodyRenderer.color.a);
            Color c2 = new Color(m_BottomColor.r, m_BottomColor.g, m_BottomColor.b, m_HideBodyRenderer.color.a);
            Color color = Color.Lerp(c1, c2, process);
            m_HideBodyRenderer.color = color;
        }
    }

}
