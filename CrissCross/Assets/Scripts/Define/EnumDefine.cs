using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EasyFrameWork
{
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
        GameLevelSelect,
        GameLevel
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
        None,
        Help,
        GameLevelSelect,
        Pause,
        Win
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

    #region RoleCTrl 角色控制器 相关

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
    }
    /// <summary>
    /// 角色动画名字
    /// </summary>
    public enum RoleAniamtorName
    {
        /// <summary>
        /// 普通闲置动画
        /// </summary>
        Idle_Normal = 1,
        /// <summary>
        /// 战斗闲置动画
        /// </summary>
        Idle_Fight = 2,
        /// <summary>
        /// 跑步动画
        /// </summary>
        Run = 3,
        /// <summary>
        /// 受伤动画
        /// </summary>
        Hurt = 4,
        /// <summary>
        /// 死亡动画
        /// </summary>
        Die = 5,
        /// <summary>
        /// 庆祝动画
        /// </summary>
        Select = 6,
        /// <summary>
        /// 庆祝动画
        /// </summary>
        XiuXian = 7,
        /// <summary>
        /// 物理攻击1动画
        /// </summary>
        PhyAttack1 = 11,
        /// <summary>
        /// 物理攻击2动画
        /// </summary>
        PhyAttack2 = 12,
        /// <summary>
        /// 物理攻击3动画
        /// </summary>
        PhyAttack3 = 13,
        /// <summary>
        /// 技能攻击1动画
        /// </summary>
        SkillAttack1 = 14,
        /// <summary>
        /// 技能攻击2动画
        /// </summary>
        SkillAttack2 = 15,
        /// <summary>
        /// 技能攻击3动画
        /// </summary>
        SkillAttack3 = 16,
        /// <summary>
        /// 技能攻击4动画
        /// </summary>
        SkillAttack4 = 17,
        /// <summary>
        /// 技能攻击5动画
        /// </summary>
        SkillAttack5 = 18,
        /// <summary>
        /// 技能攻击6动画
        /// </summary>
        SkillAttack6 = 19,
    }
    /// <summary>
    /// 角色动画过度条件名字
    /// </summary>
    public enum ToAnimatorCondition
    {

        /// <summary>
        /// 普通闲置动画条件
        /// </summary>
        ToIdleNormal,
        /// <summary>
        ///战斗闲置动画条件
        /// </summary>
        TpIdleFight,
        /// <summary>
        /// 跑步动画条件
        /// </summary>
        ToRun,
        /// <summary>
        /// 受伤动画条件
        /// </summary>
        ToHurt,
        /// <summary>
        /// 死亡动画条件
        /// </summary>
        ToDie,
        /// <summary>
        /// 物理攻击动画条件
        /// </summary>
        ToPhyAttack,
        /// <summary>
        /// 技能攻击动画条件
        /// </summary>
        ToSkillAttack,
        /// <summary>
        /// 庆祝动画条件
        /// </summary>
        ToSelect,
        /// <summary>
        /// 当前状态条件
        /// </summary>
        CurrentState
    }

    public enum IdleType
    {
        /// <summary>
        /// 普通待机
        /// </summary>
        IdleNormal,
        /// <summary>
        /// 战斗待机
        /// </summary>
        IdleFight
    }

    public enum AttackType
    {
        /// <summary>
        /// 物理攻击
        /// </summary>
        PhyAttack,
        /// <summary>
        /// 技能攻击
        /// </summary>
        SkillAttack
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

    public enum Direction
    {
        Left,
        Right,
        Up,
        Down
    }
}




