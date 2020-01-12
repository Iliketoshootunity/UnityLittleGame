
 //===================================================
//作    者：肖海林
//创建时间：2019-04-19 17:27:43
//备    注：此代码为工具生成 请勿手工修改
//===================================================
using System.Collections;
using System.Collections.Generic;
using System;
/// <summary>
/// Config数据管理
/// </summary>
public partial class ConfigDBModel : AbstractDBModel<ConfigDBModel, ConfigEntity>
 {
	/// <summary>
	/// 文件名称
	/// </summary>
	protected override string FileName { get { return "Config"; } }
	/// <summary>
	/// 创建实体
	/// </summary>
	/// <param name="parse"></param>
	/// <returns></returns>
	protected override ConfigEntity MakeEntity(GameDataTableParser parse)
	{
		ConfigEntity entity = new ConfigEntity();
		entity.Id = parse.GetFieldValue("Id").ToInt();
		entity.OneSummonCoin = parse.GetFieldValue("OneSummonCoin").ToInt();
		entity.FiveSummonCoin = parse.GetFieldValue("FiveSummonCoin").ToInt();
		entity.TheProbabilityOfNormalHero = parse.GetFieldValue("TheProbabilityOfNormalHero").ToFloat();
		entity.TheProbabilityOfeliteHero = parse.GetFieldValue("TheProbabilityOfeliteHero").ToFloat();
		entity.TheProbabilityOfLegendHero = parse.GetFieldValue("TheProbabilityOfLegendHero").ToFloat();
		entity.EveryOneTask = parse.GetFieldValue("EveryOneTask");
		entity.TheDebrisOfNormalHero = parse.GetFieldValue("TheDebrisOfNormalHero").ToInt();
		entity.TheDebrisOfEliteHero = parse.GetFieldValue("TheDebrisOfEliteHero").ToInt();
		entity.TheDebrisOfLegendHero = parse.GetFieldValue("TheDebrisOfLegendHero").ToInt();
		return entity;
 	}
}
