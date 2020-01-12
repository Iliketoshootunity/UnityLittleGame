
//===================================================
//作    者：肖海林 
//创建时间：2018-11-29 15:29:02
//备    注：此代码为工具生成 请勿手工修改
//===================================================
using System.Collections;
/// <summary>
/// biao01实体
/// </summary>
 public partial class GameLevelSpriteEntity : AbstractEntity
 {
	/// <summary>
	/// 游戏关卡Id
	/// </summary>
	public int GameLevelId { get; set; }
	/// <summary>
	/// 精灵Id
	/// </summary>
	public int SpriteId { get; set; }
	/// <summary>
	/// 精灵数量
	/// </summary>
	public int SpriteCount { get; set; }
	/// <summary>
	/// 所有精灵出生坐标 行_列_朝向(1上2下3左4右)
	/// </summary>
	public string AllBronPos { get; set; }
	/// <summary>
	/// 精灵类型1小怪2能量3元灵_
	/// </summary>
	public int Type { get; set; }
  }
