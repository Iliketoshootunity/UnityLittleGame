
//===================================================
//作    者：肖海林 
//创建时间：2018-11-29 15:28:33
//备    注：此代码为工具生成 请勿手工修改
//===================================================
using System.Collections;
/// <summary>
/// biao01实体
/// </summary>
 public partial class GameLevelEntity : AbstractEntity
 {
	/// <summary>
	/// 地图类型信息
	/// </summary>
	public string MapTypeInfo { get; set; }
	/// <summary>
	/// 行间距
	/// </summary>
	public float RowSpacing { get; set; }
	/// <summary>
	/// 列间距
	/// </summary>
	public float ColumnSpacing { get; set; }
	/// <summary>
	/// 地图源头
	/// </summary>
	public string Orgin { get; set; }
	/// <summary>
	/// 分数1
	/// </summary>
	public int Star2 { get; set; }
	/// <summary>
	/// 分数2
	/// </summary>
	public int Star3 { get; set; }
	/// <summary>
	/// 在UI地图上的位置
	/// </summary>
	public string PosInMap { get; set; }
	/// <summary>
	/// 游戏时间
	/// </summary>
	public float Time { get; set; }
	/// <summary>
	/// 玩家的位置
	/// </summary>
	public string RolePos { get; set; }
  }
