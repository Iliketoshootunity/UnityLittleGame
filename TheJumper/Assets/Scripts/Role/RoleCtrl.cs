using UnityEngine;
using System.Collections;
using System;
using DG.Tweening;
using LFrameWork.Sound;

/// <summary>
/// 角色控制器
/// </summary>
public enum JumpDir
{
    Left,
    Right
}

public class RoleCtrl : MonoBehaviour
{
    public enum RoleStatus
    {
        Stand,
        Jump,
        Dead
    }

    public Action<RoleCtrl> OnDead;
    public Action OnJumpToNext;
    private JumpDir m_JumpDir;
    private Platform m_CurPlatform;

    public Platform CurPlatform
    {
        get { return m_CurPlatform; }
    }
    private Platform m_NextPlatform;
    public Platform NextPlatform
    {
        get { return m_NextPlatform; }
    }
    //[HideInInspector]
    public RoleStatus Status;
    private Rigidbody m_Rigidbody;
    private Transform m_Shadow;
    [SerializeField]
    private AudioClip m_JumpAudioClip;
    [SerializeField]
    private AudioClip m_DeadAudioClip;
    private Animator m_Animator;
    private SpriteRenderer m_BodyRenderer;

    [SerializeField]
    private GameObject m_DeadEffect;
    // Use this for initialization
    void Start()
    {
        m_Shadow = transform.Find("Shadow");
        m_Animator = GetComponentInChildren<Animator>();
        m_BodyRenderer = transform.Find("Mesh").GetComponent<SpriteRenderer>();
        //renderer.sortingOrder = 1;
        m_Rigidbody = GetComponent<Rigidbody>();
        m_Rigidbody.isKinematic = true;
        m_Rigidbody.useGravity = false;
        Status = RoleStatus.Stand;
    }

    public void Init(Platform curPlatform)
    {
        m_CurPlatform = curPlatform;
        transform.position = m_CurPlatform.transform.TransformPoint(m_CurPlatform.StandPos);
    }

    // Update is called once per frame
    void Update()
    {
        if (Status == RoleStatus.Stand)
        {
            if (m_CurPlatform == null || m_CurPlatform.PlatfromType == PlatfromType.HideTrap ||
                m_CurPlatform.PlatfromType == PlatfromType.OtherTrap || m_CurPlatform.PlatfromType == PlatfromType.Hide)
            {
                ToDead();
                return;
            }

            transform.position = m_CurPlatform.transform.position - Vector3.forward * 0.01f;
        }


    }
    /// <summary>
    /// 切换到跳跃状态
    /// </summary>
    /// <param name="dir"></param>
    public void ToJump(JumpDir dir)
    {

        m_JumpDir = dir;
        if (dir == JumpDir.Left)
        {
            m_NextPlatform = (Platform)m_CurPlatform.LNode;
            m_BodyRenderer.flipX = true;
        }
        else
        {
            m_NextPlatform = (Platform)m_CurPlatform.RNode;
            m_BodyRenderer.flipX = false;
        }
        m_Animator.SetTrigger("IsJump");
        float x = LocalJumpSecondPos.x;
        if (m_NextPlatform == null || m_CurPlatform.PlatfromType == PlatfromType.Hide)
        {
            x = LocalJumpFirstPos.x;
        }
        else
        {
            if (m_NextPlatform.PlatfromType == PlatfromType.HideTrap || m_NextPlatform.PlatfromType == PlatfromType.OtherTrap)
            {
                //撞到障碍物
                x = LocalJumpFirstPos.x;
            }
            EazySoundManager.PlaySound(m_JumpAudioClip);
        }
        JumpState(x);
    }

    /// <summary>
    /// 跳跃状态
    /// </summary>
    private void JumpState(float endX = 0.65f)
    {
        StartCoroutine(JumpIE(endX));
    }

    private float x;
    private IEnumerator JumpIE(float endX = 0.65f)
    {
        x = 0;
        Status = RoleStatus.Jump;
        m_Shadow.DOScale(Vector3.zero, 0.1f).SetEase(Ease.Linear);
        bool isRun = true;
        if (m_CurPlatform == null) yield break;
        Vector3 startPos = m_CurPlatform.transform.position;
        while (isRun)
        {
            x += Time.deltaTime * 5;
            if (x > endX)
            {
                isRun = false;
                x = endX;
            }
            float y = Parabola(x);
            Vector3 pos = Vector3.zero;
            if (m_JumpDir == JumpDir.Left)
            {
                pos = startPos + new Vector3(-x, y, 0);
            }
            else
            {
                pos = startPos + new Vector3(x, y, 0);
            }

            transform.position = pos;
            m_Shadow.localPosition =
                transform.InverseTransformPoint(startPos);
            yield return null;
        }

        m_CurPlatform.RoleCtrl = null;
        if (m_NextPlatform != null)
        {
            m_CurPlatform = m_NextPlatform;
            m_CurPlatform.RoleCtrl = this;
        }
        else
        {
            m_CurPlatform = null;
        }
        m_NextPlatform = null;
        if (x == LocalJumpSecondPos.x)
        {
            if (OnJumpToNext != null)
            {
                OnJumpToNext();
            }
        }
        m_Shadow.localPosition = Vector3.zero;
        m_Shadow.DOScale(Vector3.one, 0.1f).SetEase(Ease.Linear);
        Status = RoleStatus.Stand;
        m_Animator.SetTrigger("IsStand");

    }



    /// <summary>
    /// 切换到死亡状态
    /// </summary>

    public void ToDead()
    {
        StopAllCoroutines();
        Status = RoleStatus.Dead;
        m_Rigidbody.velocity = Vector3.down * 5;
        m_Rigidbody.isKinematic = false;
        m_Rigidbody.useGravity = true;
        Destroy(m_Shadow.gameObject);
        if (m_CurPlatform != null)
        {
            if (m_CurPlatform.PlatfromType == PlatfromType.OtherTrap || m_CurPlatform.PlatfromType == PlatfromType.HideTrap)
            {
                GameObject go = Instantiate(m_DeadEffect);
                go.transform.position = transform.position;
                go.transform.rotation = Quaternion.identity;
                Destroy(go, 5);
                EazySoundManager.PlaySound(m_DeadAudioClip);
            }
        }


        if (OnDead != null)
        {
            OnDead(this);
        }
    }

    /// <summary>
    /// 跳跃的相对坐标1
    /// </summary>
    public Vector2 LocalJumpFirstPos = new Vector2(0.35f, 1f);
    /// <summary>
    /// 跳跃的相对坐标2
    /// </summary>
    public Vector2 LocalJumpSecondPos = new Vector2(0.65f, 0.65f);
    private float a;
    private float b;
    public float Parabola(float x)
    {
        //求导
        float t1 = (LocalJumpFirstPos.y / LocalJumpFirstPos.x - LocalJumpSecondPos.y / LocalJumpSecondPos.x);
        float t2 = (LocalJumpFirstPos.x - LocalJumpSecondPos.x);
        a = t1 / t2;
        b = (LocalJumpFirstPos.y / LocalJumpFirstPos.x) - a * LocalJumpFirstPos.x;
        return a * x * x + b * x;
    }
}
