using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EasyFrameWork
{
    #region Scene ö��
    public enum SceneType
    {
        /// <summary>
        /// ��¼����
        /// </summary>
        LogOn,
        /// <summary>
        /// ���س���
        /// </summary>
        Loading,
        /// <summary>
        /// ѡ���ɫ����
        /// </summary>
        SelectRole,
        /// <summary>
        /// ���ǳ���
        /// </summary>
        WorldMap,
        /// <summary>
        /// ��Ϸ�ؿ�����
        /// </summary>
        Gamelevel

    }

    #endregion

    #region SceneUI ö��
    /// <summary>
    /// ����UI����
    /// </summary>
    public enum UISceneType
    {
        /// <summary>
        /// ��ʼ������
        /// </summary>
        Init,
        GameLevelSelect,
        GameLevel
    }
    #endregion

    #region ����UI ö��
    /// <summary>
    /// ������������
    /// </summary>
    public enum UIWinndowContainerType
    {
        /// <summary>
        /// ����
        /// </summary>
        TL,
        /// <summary>
        /// ����
        /// </summary>
        TR,
        /// <summary>
        /// ����
        /// </summary>
        BL,
        /// <summary>
        /// ����
        /// </summary>
        BR,
        /// <summary>
        /// �м�
        /// </summary>
        Center
    }

    /// <summary>
    /// ��������
    /// </summary>
    public enum UIViewType
    {
        None,
        Help,
        GameLevelSelect,
        Pause,
        Win
    }

    /// <summary>
    /// ���ֶ�������
    /// </summary>
    public enum UIWindowShowAniType
    {
        /// <summary>
        /// ��������
        /// </summary>
        Normal,
        /// <summary>
        /// ���м�Ŵ�
        /// </summary>
        CenterToBig,
        /// <summary>
        /// ��������
        /// </summary>
        FormTop,
        /// <summary>
        /// ��������
        /// </summary>
        ForBottom,
        /// <summary>
        /// ��������
        /// </summary>
        FormLeft,
        /// <summary>
        /// ��������
        /// </summary>
        FormRight
    }
    #endregion

    #region RoleCTrl ��ɫ������ ���

    /// <summary>
    /// ��ֵ�仯����
    /// </summary>
    public enum ValueChangeType
    {
        Add,
        Reduce
    }

    /// <summary>
    /// ��ɫ״̬��״̬
    /// </summary>
    public enum RoleState
    {
        /// <summary>
        /// δ����
        /// </summary>  
        None = 0,

        /// <summary>
        /// ����״̬
        /// </summary>
        Idle = 1,
        /// <summary>
        /// �ܲ�״̬
        /// </summary>
        Run = 2,
        /// <summary>
        /// ����״̬
        /// </summary>
        Attack = 3,
        /// <summary>
        /// ����״̬
        /// </summary>
        Hurt = 4,
        /// <summary>
        /// ����״̬
        /// </summary>
        Die = 5,
        /// <summary>
        /// ��ף״̬
        /// </summary>
        Select = 6,
    }
    /// <summary>
    /// ��ɫ��������
    /// </summary>
    public enum RoleAniamtorName
    {
        /// <summary>
        /// ��ͨ���ö���
        /// </summary>
        Idle_Normal = 1,
        /// <summary>
        /// ս�����ö���
        /// </summary>
        Idle_Fight = 2,
        /// <summary>
        /// �ܲ�����
        /// </summary>
        Run = 3,
        /// <summary>
        /// ���˶���
        /// </summary>
        Hurt = 4,
        /// <summary>
        /// ��������
        /// </summary>
        Die = 5,
        /// <summary>
        /// ��ף����
        /// </summary>
        Select = 6,
        /// <summary>
        /// ��ף����
        /// </summary>
        XiuXian = 7,
        /// <summary>
        /// ������1����
        /// </summary>
        PhyAttack1 = 11,
        /// <summary>
        /// ������2����
        /// </summary>
        PhyAttack2 = 12,
        /// <summary>
        /// ������3����
        /// </summary>
        PhyAttack3 = 13,
        /// <summary>
        /// ���ܹ���1����
        /// </summary>
        SkillAttack1 = 14,
        /// <summary>
        /// ���ܹ���2����
        /// </summary>
        SkillAttack2 = 15,
        /// <summary>
        /// ���ܹ���3����
        /// </summary>
        SkillAttack3 = 16,
        /// <summary>
        /// ���ܹ���4����
        /// </summary>
        SkillAttack4 = 17,
        /// <summary>
        /// ���ܹ���5����
        /// </summary>
        SkillAttack5 = 18,
        /// <summary>
        /// ���ܹ���6����
        /// </summary>
        SkillAttack6 = 19,
    }
    /// <summary>
    /// ��ɫ����������������
    /// </summary>
    public enum ToAnimatorCondition
    {

        /// <summary>
        /// ��ͨ���ö�������
        /// </summary>
        ToIdleNormal,
        /// <summary>
        ///ս�����ö�������
        /// </summary>
        TpIdleFight,
        /// <summary>
        /// �ܲ���������
        /// </summary>
        ToRun,
        /// <summary>
        /// ���˶�������
        /// </summary>
        ToHurt,
        /// <summary>
        /// ������������
        /// </summary>
        ToDie,
        /// <summary>
        /// ��������������
        /// </summary>
        ToPhyAttack,
        /// <summary>
        /// ���ܹ�����������
        /// </summary>
        ToSkillAttack,
        /// <summary>
        /// ��ף��������
        /// </summary>
        ToSelect,
        /// <summary>
        /// ��ǰ״̬����
        /// </summary>
        CurrentState
    }

    public enum IdleType
    {
        /// <summary>
        /// ��ͨ����
        /// </summary>
        IdleNormal,
        /// <summary>
        /// ս������
        /// </summary>
        IdleFight
    }

    public enum AttackType
    {
        /// <summary>
        /// ������
        /// </summary>
        PhyAttack,
        /// <summary>
        /// ���ܹ���
        /// </summary>
        SkillAttack
    }


    /// <summary>
    /// ��ɫ����
    /// </summary>
    public enum RoleType
    {
        /// <summary>
        /// δ����
        /// </summary>
        None = 0,
        /// <summary>
        /// ��ǰ���
        /// </summary>
        MainPlayer = 1,
        /// <summary>
        /// ��ǰ���
        /// </summary>
        OtherPlayer = 2,
        /// <summary>
        /// ��
        /// </summary>
        Monster = 3
    }
    #endregion

    #region ��Ϸ�ؿ����

    /// <summary>
    /// ��Ϸ�ؿ��Ѷ�
    /// </summary>
    public enum GameLevelGrade
    {
        Normal = 0,
        Hard = 1,
        Hell = 2
    }

    /// <summary>
    /// ��Ϸ�ؿ���������
    /// </summary>
    public enum GameLevelRewardType
    {
        /// <summary>
        /// װ��
        /// </summary>
        Equip,
        /// <summary>
        /// ��Ʒ
        /// </summary>
        Item,
        /// <summary>
        /// ����
        /// </summary>
        Material
    }
    #endregion

    public enum Direction
    {
        Left,
        Right,
        Up,
        Down
    }
}




