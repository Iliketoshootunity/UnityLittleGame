
//===================================================
//作    者：肖海林 
//创建时间：2019-04-19 17:27:43
//备    注：此代码为工具生成 请勿手工修改
//===================================================
using System.Collections;
/// <summary>
/// biao01实体
/// </summary>
 public partial class GameLevelGradeEntity : AbstractEntity
 {
	/// <summary>
	/// 关卡ID
	/// </summary>
	public int LevelId { get; set; }
	/// <summary>
	/// 难度等级分类 0=普通 1=困难 2=地狱
	/// </summary>
	public int Grade { get; set; }
	/// <summary>
	/// 关卡描述
	/// </summary>
	public string Desc { get; set; }
	/// <summary>
	/// 过关条件描述文字
	/// </summary>
	public string ConditionDesc { get; set; }
	/// <summary>
	/// 奖励经验
	/// </summary>
	public int Exp { get; set; }
	/// <summary>
	/// 奖励金币
	/// </summary>
	public int Gold { get; set; }
	/// <summary>
	/// 回合限制
	/// </summary>
	public int TimeLimit { get; set; }
	/// <summary>
	/// 所消耗体力
	/// </summary>
	public int Energy { get; set; }
  }
