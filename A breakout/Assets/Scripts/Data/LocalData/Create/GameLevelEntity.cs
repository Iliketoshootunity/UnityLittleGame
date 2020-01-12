
//===================================================
//作    者：肖海林 
//创建时间：2019-02-18 14:33:52
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
	/// 障碍物信息 x_y_t|x_y_t
	/// </summary>
	public string Obstacle { get; set; }
	/// <summary>
	/// 格式 如障碍信息
	/// </summary>
	public string Monster { get; set; }
	/// <summary>
	/// 格式 如障碍信息
	/// </summary>
	public string Player { get; set; }
	/// <summary>
	/// 格式 如障碍信息
	/// </summary>
	public string OverPoint { get; set; }
  }
