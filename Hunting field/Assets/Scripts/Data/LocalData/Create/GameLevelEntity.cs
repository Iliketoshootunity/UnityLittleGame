
//===================================================
//作    者：肖海林 
//创建时间：2019-02-22 20:26:19
//备    注：此代码为工具生成 请勿手工修改
//===================================================
using System.Collections;
/// <summary>
/// biao01实体
/// </summary>
 public partial class GameLevelEntity : AbstractEntity
 {
	/// <summary>
	/// 游戏关卡
	/// </summary>
	public int Level { get; set; }
	/// <summary>
	/// 要塞1_1_1_1(类型_y_x_范围)
	/// </summary>
	public string Stronghold { get; set; }
	/// <summary>
	/// 怪物(y_x)
	/// </summary>
	public string Monster { get; set; }
  }
