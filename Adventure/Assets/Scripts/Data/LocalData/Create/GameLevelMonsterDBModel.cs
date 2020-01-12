
 //===================================================
//作    者：肖海林
//创建时间：2019-04-19 17:27:43
//备    注：此代码为工具生成 请勿手工修改
//===================================================
using System.Collections;
using System.Collections.Generic;
using System;
/// <summary>
/// GameLevelMonster数据管理
/// </summary>
public partial class GameLevelMonsterDBModel : AbstractDBModel<GameLevelMonsterDBModel, GameLevelMonsterEntity>
 {
	/// <summary>
	/// 文件名称
	/// </summary>
	protected override string FileName { get { return "GameLevelMonster"; } }
	/// <summary>
	/// 创建实体
	/// </summary>
	/// <param name="parse"></param>
	/// <returns></returns>
	protected override GameLevelMonsterEntity MakeEntity(GameDataTableParser parse)
	{
		GameLevelMonsterEntity entity = new GameLevelMonsterEntity();
		entity.Id = parse.GetFieldValue("Id").ToInt();
		entity.GameLevelId = parse.GetFieldValue("GameLevelId").ToInt();
		entity.Grade = parse.GetFieldValue("Grade").ToInt();
		entity.SpriteId = parse.GetFieldValue("SpriteId").ToInt();
		entity.SpriteRowAndCol = parse.GetFieldValue("SpriteRowAndCol");
		entity.Exp = parse.GetFieldValue("Exp").ToInt();
		entity.Gold = parse.GetFieldValue("Gold").ToInt();
		return entity;
 	}
}
