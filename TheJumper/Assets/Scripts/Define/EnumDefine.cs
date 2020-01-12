using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    /// <summary>
    /// /�ؿ�����
    /// </summary>
    GameLevel,
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
    /// <summary>
    /// δ����
    /// </summary>
    None,
    /// <summary>
    /// ��������
    /// </summary>
    GameLevelHelp,


    /// <summary>
    /// �ؿ���ͼ
    /// </summary>
    GameLevelPause,
    /// <summary>
    /// �ؿ�ʧ��
    /// </summary>
    GameLevelFail

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

# region RoleCTrl ��ɫ������ ���

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
    /// <summary>
    /// �쳣״̬
    /// </summary>
    Abnormal = 7,
    /// <summary>
    /// վ��״̬
    /// </summary>
    Stand = 8
}
/// <summary>
/// ��ɫ��������
/// </summary>
public enum RoleAniamtorName
{
    /// <summary>
    /// �����ƶ�����
    /// </summary>
    MoveUp,
    /// <summary>
    /// �����ƶ�����
    /// </summary>
    MoveDwon,
    /// <summary>
    /// �����ƶ�����
    /// </summary>
    MoveLeft,
    /// <summary>
    /// �����ƶ�����
    /// </summary>
    MoveRight,

    /// <summary>
    /// ����վ������
    /// </summary>
    StandUp,
    /// <summary>
    /// ����վ������
    /// </summary>
    StandDwon,
    /// <summary>
    /// ����վ������
    /// </summary>
    StandLeft,
    /// <summary>
    /// ����վ������
    /// </summary>
    StandRight,

    /// <summary>
    /// ���ϻ��ζ���
    /// </summary>
    StunUp,
    /// <summary>
    /// ����վ������
    /// </summary>
    StunDwon,
    /// <summary>
    /// ����վ������
    /// </summary>
    StunLeft,
    /// <summary>
    /// ����վ������
    /// </summary>
    StunRight,

    /// <summary>
    /// ���Ϲ���01
    /// </summary>
    Attack01Up,
    /// <summary>
    /// ���¹���01
    /// </summary>
    Attack01Down,
    /// <summary>
    /// ���󹥻�01
    /// </summary>
    Attack01Left,
    /// <summary>
    /// ���ҹ���01
    /// </summary>
    Attack01Right,

    /// <summary>
    /// ���Ϲ���02
    /// </summary>
    Attack02Up,
    /// <summary>
    /// ���¹���02
    /// </summary>
    Attack02Down,
    /// <summary>
    /// ���󹥻�02
    /// </summary>
    Attack02Left,
    /// <summary>
    /// ���ҹ���02
    /// </summary>
    Attack02Right,


    /// <summary>
    /// ��������
    /// </summary>
    Dead
}
/// <summary>
/// ��ɫ����������������
/// </summary>
public enum ToAnimatorCondition
{
    /// <summary>
    ///�����ƶ�
    /// </summary>
    ToMoveUp,
    /// <summary>
    ///�����ƶ�
    /// </summary>
    ToMoveDown,
    /// <summary>
    ///�����ƶ�
    /// </summary>
    ToMoveLeft,
    /// <summary>
    ///�����ƶ�
    /// </summary>
    ToMoveRight,

    /// <summary>
    /// ���ϻ���
    /// </summary>
    ToStunUp,
    /// <summary>
    /// ���»���
    /// </summary>
    ToStunDown,
    /// <summary>
    /// �������
    /// </summary>
    ToStunLeft,
    /// <summary>
    /// ���һ���
    /// </summary>
    ToStunRight,

    /// <summary>
    /// ����վ��
    /// </summary>
    ToStandUp,
    /// <summary>
    /// ����վ��
    /// </summary>
    ToStandDown,
    /// <summary>
    /// ����վ��
    /// </summary>
    ToStandLeft,
    /// <summary>
    /// ����վ��
    /// </summary>
    ToStandRight,

    /// <summary>
    /// ���Ϲ���01
    /// </summary>
    ToAttack01Up,
    /// <summary>
    /// ���¹���01
    /// </summary>
    ToAttack01Down,
    /// <summary>
    /// ���󹥻�01
    /// </summary>
    ToAttack01Left,
    /// <summary>
    /// ���ҹ���01
    /// </summary>
    ToAttack01Right,

    /// <summary>
    /// ���Ϲ���02
    /// </summary>
    ToAttack02Up,
    /// <summary>
    /// ���¹���02
    /// </summary>
    ToAttack02Down,
    /// <summary>
    /// ���󹥻�02
    /// </summary>
    ToAttack02Left,
    /// <summary>
    /// ���ҹ���02
    /// </summary>
    ToAttack02Right,

    /// <summary>
    /// ����
    /// </summary>
    ToDead,
    /// <summary>
    /// ��ǰ״̬����
    /// </summary>
    CurrentState
}

/// <summary>
/// �ƶ�״̬
/// </summary>
public enum RunType
{
    /// <summary>
    /// ��ͨ�ܶ�
    /// </summary>
    RunNormal,
    /// <summary>
    /// ���
    /// </summary>
    RunCharge,
}

/// <summary>
/// �쳣����
/// </summary>
public enum AbnormalType
{
    None,
    /// <summary>
    /// ѣ��
    /// </summary>
    Vertigo,
    /// <summary>
    /// ʯ��
    /// </summary>
    Petrified,

    /// <summary>
    ///���� 
    /// </summary>
    Stun
}


public enum AttackType
{
    /// <summary>
    /// ���չ���
    /// </summary>
    XiShouAttack,
    /// <summary>
    /// �ͷŹ���
    /// </summary>
    ShiFangAttack
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


public enum MoveDirection
{
    None,
    Up,
    Down,
    Left,
    Right

}


