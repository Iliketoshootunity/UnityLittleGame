using System.Collections;
using System.Collections.Generic;
using EasyFrameWork.Buff;
using UnityEngine;

/// <summary>
/// ��ɫ������Ϣ
/// </summary>
[System.Serializable]
public class RoleSkillInfo
{
    /// <summary>
    /// ���
    /// </summary>
    public int Index;
    /// <summary>
    /// �Ƿ��ǽ�ս����
    /// </summary>
    public bool IsMeleeAttack;
    /// <summary>
    /// �����˺�
    /// </summary>
    public float AttackValue;

    /// <summary>
    /// ��������
    /// </summary>
    public int AttackCount;

    /// <summary>
    ///����ǽ�ս���� ������Χ
    /// </summary>
    public float AttackRange;

    /// <summary>
    ///�����Զ�̹��� �ӵ�����
    /// </summary>
    public string BulletName;

    /// <summary>
    /// ���������ӳ�
    /// </summary>
    public float HurtDelay;

    /// <summary>
    /// �Ƿ���Ļ��
    /// </summary>
    public bool IsCameraShake;

    /// <summary>
    /// ��Ч����
    /// </summary>
    public string EffectName;

    /// <summary>
    /// ��Ч����ʱ��
    /// </summary>
    public float EffectLifeTime;

    /// <summary>
    /// ���Լ������쳣״̬
    /// </summary>
    public BuffType BuffToMe;

    /// <summary>
    /// �����˸����쳣״̬
    /// </summary>
    public BuffType BuffToEnemy;
}
