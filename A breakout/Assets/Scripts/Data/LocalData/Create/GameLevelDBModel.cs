
 //===================================================
//作    者：肖海林
//创建时间：2019-02-18 14:33:52
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
		entity.Level = parse.GetFieldValue("Level").ToInt();
		entity.Obstacle = parse.GetFieldValue("Obstacle");
		entity.Monster = parse.GetFieldValue("Monster");
		entity.Player = parse.GetFieldValue("Player");
		entity.OverPoint = parse.GetFieldValue("OverPoint");
		return entity;
 	}
}
