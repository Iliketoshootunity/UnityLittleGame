
//===================================================
//作    者：肖海林 
//创建时间：2019-04-19 17:27:43
//备    注：此代码为工具生成 请勿手工修改
//===================================================
using System.Collections;
/// <summary>
/// biao01实体
/// </summary>
 public partial class GameLevelMonsterEntity : AbstractEntity
 {
	/// <summary>
	/// 游戏关卡Id
	/// </summary>
	public int GameLevelId { get; set; }
	/// <summary>
	/// 难度等级
	/// </summary>
	public int Grade { get; set; }
	/// <summary>
	/// 怪物Id
	/// </summary>
	public int SpriteId { get; set; }
	/// <summary>
	/// 怪物坐标X_Y
	/// </summary>
	public string SpriteRowAndCol { get; set; }
	/// <summary>
	/// 怪物掉落经验
	/// </summary>
	public int Exp { get; set; }
	/// <summary>
	/// 怪物掉落金币
	/// </summary>
	public int Gold { get; set; }
  }
