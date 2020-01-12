using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region Scene 枚举
public enum SceneType
{
    /// <summary>
    /// 登录场景
    /// </summary>
    LogOn,
    /// <summary>
    /// 加载场景
    /// </summary>
    Loading,
    /// <summary>
    /// 选择角色场景
    /// </summary>
    SelectRole,
    /// <summary>
    /// 主城场景
    /// </summary>
    WorldMap,
    /// <summary>
    /// 游戏关卡场景
    /// </summary>
    Gamelevel

}

#endregion

#region SceneUI 枚举
/// <summary>
/// 场景UI类型
/// </summary>
public enum UISceneType
{
    /// <summary>
    /// 初始化场景
    /// </summary>
    Init,
    /// <summary>
    /// /关卡场景
    /// </summary>
    GameLevel,
}
#endregion

#region 窗口UI 枚举
/// <summary>
/// 窗口容器类型
/// </summary>
public enum UIWinndowContainerType
{
    /// <summary>
    /// 左上
    /// </summary>
    TL,
    /// <summary>
    /// 右上
    /// </summary>
    TR,
    /// <summary>
    /// 左下
    /// </summary>
    BL,
    /// <summary>
    /// 右下
    /// </summary>
    BR,
    /// <summary>
    /// 中间
    /// </summary>
    Center
}

/// <summary>
/// 窗口类型
/// </summary>
public enum UIViewType
{
    /// <summary>
    /// 未设置
    /// </summary>
    None,
    /// <summary>
    /// 帮助窗口
    /// </summary>
    GameLevelHelp,


    /// <summary>
    /// 关卡地图
    /// </summary>
    GameLevelPause,
    /// <summary>
    /// 关卡失败
    /// </summary>
    GameLevelFail

}

/// <summary>
/// 各种动画类型
/// </summary>
public enum UIWindowShowAniType
{
    /// <summary>
    /// 正常动画
    /// </summary>
    Normal,
    /// <summary>
    /// 从中间放大
    /// </summary>
    CenterToBig,
    /// <summary>
    /// 从上子下
    /// </summary>
    FormTop,
    /// <summary>
    /// 从下自上
    /// </summary>
    ForBottom,
    /// <summary>
    /// 从左自右
    /// </summary>
    FormLeft,
    /// <summary>
    /// 从右自左
    /// </summary>
    FormRight
}
#endregion

# region RoleCTrl 角色控制器 相关

/// <summary>
/// 数值变化类型
/// </summary>
public enum ValueChangeType
{
    Add,
    Reduce
}

/// <summary>
/// 角色状态机状态
/// </summary>
public enum RoleState
{
    /// <summary>
    /// 未设置
    /// </summary>  
    None = 0,

    /// <summary>
    /// 闲置状态
    /// </summary>
    Idle = 1,
    /// <summary>
    /// 跑步状态
    /// </summary>
    Run = 2,
    /// <summary>
    /// 攻击状态
    /// </summary>
    Attack = 3,
    /// <summary>
    /// 受伤状态
    /// </summary>
    Hurt = 4,
    /// <summary>
    /// 死亡状态
    /// </summary>
    Die = 5,
    /// <summary>
    /// 庆祝状态
    /// </summary>
    Select = 6,
    /// <summary>
    /// 异常状态
    /// </summary>
    Abnormal = 7,
    /// <summary>
    /// 站立状态
    /// </summary>
    Stand = 8
}
/// <summary>
/// 角色动画名字
/// </summary>
public enum RoleAniamtorName
{
    /// <summary>
    /// 向上移动动画
    /// </summary>
    MoveUp,
    /// <summary>
    /// 向下移动动画
    /// </summary>
    MoveDwon,
    /// <summary>
    /// 向左移动动画
    /// </summary>
    MoveLeft,
    /// <summary>
    /// 向右移动动画
    /// </summary>
    MoveRight,

    /// <summary>
    /// 向上站立动画
    /// </summary>
    StandUp,
    /// <summary>
    /// 向下站立动画
    /// </summary>
    StandDwon,
    /// <summary>
    /// 向左站立动画
    /// </summary>
    StandLeft,
    /// <summary>
    /// 向右站立动画
    /// </summary>
    StandRight,

    /// <summary>
    /// 向上击晕动画
    /// </summary>
    StunUp,
    /// <summary>
    /// 向下站立动画
    /// </summary>
    StunDwon,
    /// <summary>
    /// 向左站立动画
    /// </summary>
    StunLeft,
    /// <summary>
    /// 向右站立动画
    /// </summary>
    StunRight,

    /// <summary>
    /// 向上攻击01
    /// </summary>
    Attack01Up,
    /// <summary>
    /// 向下攻击01
    /// </summary>
    Attack01Down,
    /// <summary>
    /// 向左攻击01
    /// </summary>
    Attack01Left,
    /// <summary>
    /// 向右攻击01
    /// </summary>
    Attack01Right,

    /// <summary>
    /// 向上攻击02
    /// </summary>
    Attack02Up,
    /// <summary>
    /// 向下攻击02
    /// </summary>
    Attack02Down,
    /// <summary>
    /// 向左攻击02
    /// </summary>
    Attack02Left,
    /// <summary>
    /// 向右攻击02
    /// </summary>
    Attack02Right,


    /// <summary>
    /// 死亡动画
    /// </summary>
    Dead
}
/// <summary>
/// 角色动画过度条件名字
/// </summary>
public enum ToAnimatorCondition
{
    /// <summary>
    ///向上移动
    /// </summary>
    ToMoveUp,
    /// <summary>
    ///向下移动
    /// </summary>
    ToMoveDown,
    /// <summary>
    ///向左移动
    /// </summary>
    ToMoveLeft,
    /// <summary>
    ///向右移动
    /// </summary>
    ToMoveRight,

    /// <summary>
    /// 向上击晕
    /// </summary>
    ToStunUp,
    /// <summary>
    /// 向下击晕
    /// </summary>
    ToStunDown,
    /// <summary>
    /// 向左击晕
    /// </summary>
    ToStunLeft,
    /// <summary>
    /// 向右击晕
    /// </summary>
    ToStunRight,

    /// <summary>
    /// 向上站立
    /// </summary>
    ToStandUp,
    /// <summary>
    /// 向下站立
    /// </summary>
    ToStandDown,
    /// <summary>
    /// 向左站立
    /// </summary>
    ToStandLeft,
    /// <summary>
    /// 向右站立
    /// </summary>
    ToStandRight,

    /// <summary>
    /// 向上攻击01
    /// </summary>
    ToAttack01Up,
    /// <summary>
    /// 向下攻击01
    /// </summary>
    ToAttack01Down,
    /// <summary>
    /// 向左攻击01
    /// </summary>
    ToAttack01Left,
    /// <summary>
    /// 向右攻击01
    /// </summary>
    ToAttack01Right,

    /// <summary>
    /// 向上攻击02
    /// </summary>
    ToAttack02Up,
    /// <summary>
    /// 向下攻击02
    /// </summary>
    ToAttack02Down,
    /// <summary>
    /// 向左攻击02
    /// </summary>
    ToAttack02Left,
    /// <summary>
    /// 向右攻击02
    /// </summary>
    ToAttack02Right,

    /// <summary>
    /// 死亡
    /// </summary>
    ToDead,
    /// <summary>
    /// 当前状态条件
    /// </summary>
    CurrentState
}

/// <summary>
/// 移动状态
/// </summary>
public enum RunType
{
    /// <summary>
    /// 普通跑动
    /// </summary>
    RunNormal,
    /// <summary>
    /// 冲锋
    /// </summary>
    RunCharge,
}

/// <summary>
/// 异常类型
/// </summary>
public enum AbnormalType
{
    None,
    /// <summary>
    /// 眩晕
    /// </summary>
    Vertigo,
    /// <summary>
    /// 石化
    /// </summary>
    Petrified,

    /// <summary>
    ///击晕 
    /// </summary>
    Stun
}


public enum AttackType
{
    /// <summary>
    /// 吸收攻击
    /// </summary>
    XiShouAttack,
    /// <summary>
    /// 释放攻击
    /// </summary>
    ShiFangAttack
}


/// <summary>
/// 角色类型
/// </summary>
public enum RoleType
{
    /// <summary>
    /// 未设置
    /// </summary>
    None = 0,
    /// <summary>
    /// 当前玩家
    /// </summary>
    MainPlayer = 1,
    /// <summary>
    /// 当前玩家
    /// </summary>
    OtherPlayer = 2,
    /// <summary>
    /// 怪
    /// </summary>
    Monster = 3
}
#endregion

#region 游戏关卡相关

/// <summary>
/// 游戏关卡难度
/// </summary>
public enum GameLevelGrade
{
    Normal = 0,
    Hard = 1,
    Hell = 2
}

/// <summary>
/// 游戏关卡奖励类型
/// </summary>
public enum GameLevelRewardType
{
    /// <summary>
    /// 装备
    /// </summary>
    Equip,
    /// <summary>
    /// 物品
    /// </summary>
    Item,
    /// <summary>
    /// 道具
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


