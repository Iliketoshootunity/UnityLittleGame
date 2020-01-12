
 //===================================================
//作    者：肖海林
//创建时间：2019-04-19 17:27:43
//备    注：此代码为工具生成 请勿手工修改
//===================================================
using System.Collections;
using System.Collections.Generic;
using System;
/// <summary>
/// HeroLevel数据管理
/// </summary>
public partial class HeroLevelDBModel : AbstractDBModel<HeroLevelDBModel, HeroLevelEntity>
 {
	/// <summary>
	/// 文件名称
	/// </summary>
	protected override string FileName { get { return "HeroLevel"; } }
	/// <summary>
	/// 创建实体
	/// </summary>
	/// <param name="parse"></param>
	/// <returns></returns>
	protected override HeroLevelEntity MakeEntity(GameDataTableParser parse)
	{
		HeroLevelEntity entity = new HeroLevelEntity();
		entity.Id = parse.GetFieldValue("Id").ToInt();
		entity.Level = parse.GetFieldValue("Level").ToInt();
		entity.NeedExp = parse.GetFieldValue("NeedExp").ToInt();
		entity.Hp = parse.GetFieldValue("Hp").ToInt();
		entity.PhyAtk = parse.GetFieldValue("PhyAtk").ToInt();
		entity.MgicAtk = parse.GetFieldValue("MgicAtk").ToInt();
		entity.Cri = parse.GetFieldValue("Cri").ToInt();
		entity.CriValue = parse.GetFieldValue("CriValue").ToFloat();
		entity.PhyDef = parse.GetFieldValue("PhyDef").ToInt();
		entity.MgicDef = parse.GetFieldValue("MgicDef").ToInt();
		entity.RageValue = parse.GetFieldValue("RageValue").ToInt();
		return entity;
 	}
}
