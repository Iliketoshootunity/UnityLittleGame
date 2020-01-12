
 //===================================================
//作    者：肖海林
//创建时间：2018-11-29 15:29:02
//备    注：此代码为工具生成 请勿手工修改
//===================================================
using System.Collections;
using System.Collections.Generic;
using System;
/// <summary>
/// GameLevelSprite数据管理
/// </summary>
public partial class GameLevelSpriteDBModel : AbstractDBModel<GameLevelSpriteDBModel, GameLevelSpriteEntity>
 {
	/// <summary>
	/// 文件名称
	/// </summary>
	protected override string FileName { get { return "GameLevelSprite"; } }
	/// <summary>
	/// 创建实体
	/// </summary>
	/// <param name="parse"></param>
	/// <returns></returns>
	protected override GameLevelSpriteEntity MakeEntity(GameDataTableParser parse)
	{
		GameLevelSpriteEntity entity = new GameLevelSpriteEntity();
		entity.Id = parse.GetFieldValue("Id").ToInt();
		entity.GameLevelId = parse.GetFieldValue("GameLevelId").ToInt();
		entity.SpriteId = parse.GetFieldValue("SpriteId").ToInt();
		entity.SpriteCount = parse.GetFieldValue("SpriteCount").ToInt();
		entity.AllBronPos = parse.GetFieldValue("AllBronPos");
		entity.Type = parse.GetFieldValue("Type").ToInt();
		return entity;
 	}
}
