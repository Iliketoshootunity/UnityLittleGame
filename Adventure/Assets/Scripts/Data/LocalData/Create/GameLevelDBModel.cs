
 //===================================================
//作    者：肖海林
//创建时间：2019-04-19 17:27:43
//备    注：此代码为工具生成 请勿手工修改
//===================================================
using System.Collections;
using System.Collections.Generic;
using System;
/// <summary>
/// GameLevel数据管理
/// </summary>
public partial class GameLevelDBModel : AbstractDBModel<GameLevelDBModel, GameLevelEntity>
 {
	/// <summary>
	/// 文件名称
	/// </summary>
	protected override string FileName { get { return "GameLevel"; } }
	/// <summary>
	/// 创建实体
	/// </summary>
	/// <param name="parse"></param>
	/// <returns></returns>
	protected override GameLevelEntity MakeEntity(GameDataTableParser parse)
	{
		GameLevelEntity entity = new GameLevelEntity();
		entity.Id = parse.GetFieldValue("Id").ToInt();
		entity.Name = parse.GetFieldValue("Name");
		entity.SceneName = parse.GetFieldValue("SceneName");
		entity.Ico = parse.GetFieldValue("Ico");
		entity.Pic = parse.GetFieldValue("Pic");
		entity.Pos = parse.GetFieldValue("Pos");
		entity.BGM = parse.GetFieldValue("BGM");
		entity.Intro = parse.GetFieldValue("Intro");
		entity.IsBoss = parse.GetFieldValue("IsBoss").ToInt();
		entity.BossPic = parse.GetFieldValue("BossPic");
		entity.BoosTip = parse.GetFieldValue("BoosTip");
		return entity;
 	}
}
