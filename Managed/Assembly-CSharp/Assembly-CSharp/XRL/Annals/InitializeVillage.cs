using System;
using System.Collections.Generic;
using HistoryKit;
using XRL.Language;
using XRL.Names;
using XRL.World;
using XRL.World.Parts;

namespace XRL.Annals
{
	// Token: 0x02000627 RID: 1575
	[Serializable]
	public class InitializeVillage : HistoricEvent
	{
		// Token: 0x060041E9 RID: 16873 RVA: 0x00152644 File Offset: 0x00150844
		public InitializeVillage(string Region, string BaseFaction = null, string BaseFactionMember = null, string Culture = null, List<string> Palette = null, bool VillageZero = false)
		{
			this.region = Region;
			this.BaseFaction = BaseFaction;
			this.BaseFactionMember = BaseFactionMember;
			this.Culture = Culture;
			this.Palette = Palette;
			this.bVillageZero = VillageZero;
		}

		// Token: 0x060041EA RID: 16874 RVA: 0x0015267C File Offset: 0x0015087C
		public override void Generate()
		{
			GameObject gameObject = null;
			base.SetEntityProperty("type", "village");
			int num = 0;
			string text = this.Culture.Coalesce("Qudish");
			string text6;
			do
			{
				if (InitializeVillage.CHANCE_REGIONAL_NAME.in100())
				{
					GameObject gameObject2 = null;
					string text2 = null;
					string text3 = null;
					string text4 = null;
					string text5 = Grammar.MakeTitleCase(this.region);
					text6 = NameMaker.MakeName(gameObject2, text2, text3, text4, text, null, text5, null, null, null, null, "Site", null, false, false, null, null);
				}
				else
				{
					text6 = NameMaker.MakeName(null, null, null, null, text, null, null, null, null, null, null, "Site", null, false, false, null, null);
				}
			}
			while (this.history.GetEntitiesWherePropertyEquals("name", text6).Count > 0 && ++num < 1000);
			base.SetEntityProperty("name", text6);
			base.SetEntityProperty("region", this.region);
			if (this.Palette.IsNullOrEmpty<string>())
			{
				base.AddListProperty("palette", Crayons.GetRandomDistinctColorsAll(3));
			}
			else
			{
				base.AddListProperty("palette", this.Palette);
			}
			string text7;
			if (this.bVillageZero)
			{
				base.SetEntityProperty("tier", "0");
				base.SetEntityProperty("techTier", "0");
				text7 = GameObject.Create(PopulationManager.RollOneFrom("VillageOneBaseFaction_" + this.region, null, null).Blueprint).GetPrimaryFaction(false);
				base.SetEntityProperty("villageZero", "true");
			}
			else
			{
				GameObjectBlueprint gameObjectBlueprint = (GameObjectFactory.Factory.Blueprints.ContainsKey("Terrain" + this.region) ? GameObjectFactory.Factory.Blueprints["Terrain" + this.region] : null);
				if (gameObjectBlueprint != null)
				{
					base.SetEntityProperty("tier", gameObjectBlueprint.GetTag("RegionTier", "1"));
				}
				else
				{
					base.SetEntityProperty("tier", "1");
				}
				gameObjectBlueprint = (GameObjectFactory.Factory.Blueprints.ContainsKey("Terrain" + this.region) ? GameObjectFactory.Factory.Blueprints["Terrain" + this.region] : null);
				if (gameObjectBlueprint != null)
				{
					base.SetEntityProperty("techTier", base.Random(1, int.Parse(gameObjectBlueprint.GetTag("RegionTier", "1")) + 1).ToString());
				}
				else
				{
					base.SetEntityProperty("techTier", "1");
				}
				int num2 = 0;
				if (this.BaseFaction.IsNullOrEmpty())
				{
					do
					{
						PopulationResult populationResult = PopulationManager.RollOneFrom("LairOwners_" + this.region, null, null);
						gameObject = ((populationResult != null) ? GameObject.Create(populationResult.Blueprint) : null);
						string text8;
						if (gameObject == null)
						{
							text8 = null;
						}
						else
						{
							Brain brain = gameObject.Brain;
							text8 = ((brain != null) ? brain.GetPrimaryFaction(false) : null);
						}
						text7 = text8;
					}
					while (text7 == null && ++num2 < 100);
					if (text7 == null)
					{
						text7 = "Prey";
					}
				}
				else
				{
					text7 = this.BaseFaction;
				}
			}
			GameObjectBlueprint gameObjectBlueprint2;
			if (gameObject != null)
			{
				gameObjectBlueprint2 = gameObject.GetBlueprint(true);
			}
			else if (!this.BaseFactionMember.IsNullOrEmpty())
			{
				gameObjectBlueprint2 = GameObjectFactory.Factory.GetBlueprint(this.BaseFactionMember);
			}
			else
			{
				gameObjectBlueprint2 = Faction.GetMembers(text7, null, true, true).GetRandomElement<GameObjectBlueprint>(null);
			}
			base.SetEntityProperty("villagerPopulation", gameObjectBlueprint2.HasTag("Humanoid", false) ? "humanoid" : "nonHumanoid");
			base.SetEntityProperty("baseFaction", text7);
			base.SetEntityProperty("baseFactionMember", gameObjectBlueprint2.Name);
			Dictionary<string, string> dictionary = QudHistoryHelpers.BuildContextFromObjectTextFragments(gameObjectBlueprint2.Name);
			string text9 = base.ExpandString("<spice.villages.reasonForFounding.!random>", dictionary);
			base.SetEntityProperty("reasonForFounding", text9);
			base.SetEntityProperty("defaultSacredThing", base.ExpandString("<spice.villages.reasonForFounding." + text9 + ".sacredThing.!random>", dictionary));
			base.SetEntityProperty("defaultProfaneThing", base.ExpandString("<spice.villages.reasonForFounding." + text9 + ".profaneThing.!random>", dictionary));
			base.SetEntityProperty("governor", "the mayor");
			this.duration = 0L;
		}

		// Token: 0x04002053 RID: 8275
		public static readonly int CHANCE_REGIONAL_NAME;

		// Token: 0x04002054 RID: 8276
		public string region;

		// Token: 0x04002055 RID: 8277
		[NonSerialized]
		public string BaseFaction;

		// Token: 0x04002056 RID: 8278
		[NonSerialized]
		public string BaseFactionMember;

		// Token: 0x04002057 RID: 8279
		[NonSerialized]
		private string Culture;

		// Token: 0x04002058 RID: 8280
		[NonSerialized]
		private List<string> Palette;

		// Token: 0x04002059 RID: 8281
		public bool bVillageZero;
	}
}
