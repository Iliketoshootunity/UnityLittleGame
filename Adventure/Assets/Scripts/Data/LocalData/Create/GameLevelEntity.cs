
//===================================================
//作    者：肖海林 
//创建时间：2019-04-19 17:27:43
//备    注：此代码为工具生成 请勿手工修改
//===================================================
using System.Collections;
/// <summary>
/// biao01实体
/// </summary>
 public partial class GameLevelEntity : AbstractEntity
 {
	/// <summary>
	/// 关卡名字
	/// </summary>
	public string Name { get; set; }
	/// <summary>
	/// 场景名字
	/// </summary>
	public string SceneName { get; set; }
	/// <summary>
	/// 关卡图标
	/// </summary>
	public string Ico { get; set; }
	/// <summary>
	/// 关卡图片
	/// </summary>
	public string Pic { get; set; }
	/// <summary>
	/// 在地图的位置
	/// </summary>
	public string Pos { get; set; }
	/// <summary>
	/// 背景音乐
	/// </summary>
	public string BGM { get; set; }
	/// <summary>
	/// 副本提示
	/// </summary>
	public string Intro { get; set; }
	/// <summary>
	/// 是否是Boss关卡 0 表示不是 1表示是
	/// </summary>
	public int IsBoss { get; set; }
	/// <summary>
	/// Boss图标
	/// </summary>
	public string BossPic { get; set; }
	/// <summary>
	/// Boss关卡提示
	/// </summary>
	public string BoosTip { get; set; }
  }
