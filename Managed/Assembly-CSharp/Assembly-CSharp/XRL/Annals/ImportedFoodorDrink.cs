using System;
using System.Collections.Generic;
using ConsoleLib.Console;
using HistoryKit;
using XRL.Language;
using XRL.Rules;
using XRL.World;
using XRL.World.Parts;
using XRL.World.Skills.Cooking;

namespace XRL.Annals
{
	// Token: 0x02000626 RID: 1574
	[Serializable]
	public class ImportedFoodorDrink : HistoricEvent
	{
		// Token: 0x060041E3 RID: 16867 RVA: 0x0015209F File Offset: 0x0015029F
		public ImportedFoodorDrink()
		{
			this.bVillageZero = false;
		}

		// Token: 0x060041E4 RID: 16868 RVA: 0x001520AE File Offset: 0x001502AE
		public ImportedFoodorDrink(bool VillageZero)
		{
			this.bVillageZero = VillageZero;
		}

		// Token: 0x060041E5 RID: 16869 RVA: 0x001520C0 File Offset: 0x001502C0
		public override void Generate()
		{
			this.duration = 1L;
			HistoricEntitySnapshot snapshotAtYear = this.entity.GetSnapshotAtYear(this.entity.lastYear);
			int num = base.Random(1, 1000);
			int num2 = 0;
			if (num <= 334)
			{
				List<string> list = new List<string>();
				string text = "";
				string liquidTierGroup = ImportedFoodorDrink.getLiquidTierGroup(int.Parse(snapshotAtYear.GetProperty("tier", "unknown")));
				int num3 = base.Random(1, 100);
				int num4;
				if (num3 <= 4)
				{
					num4 = 3;
				}
				else if (num3 <= 52)
				{
					num4 = 2;
				}
				else
				{
					num4 = 1;
				}
				for (int i = 1; i <= num4; i++)
				{
					string text2 = ImportedFoodorDrink.generateUniqueVillageLiquid(liquidTierGroup, list);
					text += text2;
					text = text + "-" + (2000.0 / Math.Pow(2.0, (double)i)).ToString() + ",";
					list.Add(text2);
				}
				text = text.TrimEnd(',');
				LiquidVolume part = GameObject.Create("Waterskin").GetPart<LiquidVolume>();
				part.ComponentLiquids.Clear();
				part.InitialLiquid = text;
				string text3 = ColorUtility.StripFormatting(part.GetLiquidName());
				base.AddEntityListItem("signatureLiquids", part.GetLiquidDesignation(false), false);
				string text4;
				if (If.CoinFlip(null, null))
				{
					text4 = string.Format("No {0} shall be {1} in {2} without lifting a {3} of {4}!|{5}", new object[]
					{
						base.ExpandString("<spice.items.blessing.!random>"),
						base.ExpandString("<spice.commonPhrases.honored.!random>"),
						snapshotAtYear.Name,
						base.ExpandString("<spice.commonPhrases.mug.!random>"),
						text3,
						this.id
					});
				}
				else
				{
					text4 = string.Format("No {0} shall be {1} in {2} without lifting a {3} of {4} and proposing a toast to {5}!|{6}", new object[]
					{
						base.ExpandString("<spice.items.blessing.!random>"),
						base.ExpandString("<spice.commonPhrases.honored.!random>"),
						snapshotAtYear.Name,
						base.ExpandString("<spice.commonPhrases.mug.!random>"),
						text3,
						snapshotAtYear.sacredThing,
						this.id
					});
				}
				base.AddEntityListItem("Gospels", text4, false);
				if (If.Chance(7, null, null))
				{
					base.SetEntityProperty("newFactionName", ImportedFoodorDrink.generateFactionName(text3));
				}
				return;
			}
			num2 += 334;
			if (num <= num2 + 333)
			{
				string randomMeaningfulWord = Grammar.GetRandomMeaningfulWord(snapshotAtYear.sacredThing.Split(',', StringSplitOptions.None)[0]);
				string property = snapshotAtYear.GetProperty("baseFaction", "unknown");
				string text5;
				if (GameObjectFactory.Factory.Blueprints.ContainsKey(property + "_Data"))
				{
					text5 = GameObjectFactory.Factory.Blueprints[property + "_Data"].GetTag("DishNames", "generic");
				}
				else
				{
					text5 = "generic";
				}
				text5 = text5.Split(',', StringSplitOptions.None).GetRandomElement<string>(null);
				string text6 = Grammar.MakeTitleCase(base.ExpandString("<spice.villages.food.specialDishNames.!random>").Replace("*descriptor*", randomMeaningfulWord).Replace("*descriptor.possessive*", Grammar.MakePossessive(randomMeaningfulWord))
					.Replace("*dish*", base.ExpandString("<spice.cooking.recipeNames.categorizedFoods." + text5 + ".!random>")));
				base.SetEntityProperty("signatureDishName", text6);
				base.AddEntityListItem("sacredThings", "the dish known as " + text6, false);
				string text4 = string.Format("Since the first {0}, the villagers of {1} have {2} feasted on {3}.|{4}", new object[]
				{
					Grammar.MakeTitleCaseWithArticle(base.ExpandString("<spice.villages.festivalName.!random>")),
					snapshotAtYear.Name,
					base.ExpandString("<spice.commonPhrases.merrily.!random>"),
					text6,
					this.id
				});
				base.AddEntityListItem("Gospels", text4, false);
				if (If.Chance(7, null, null))
				{
					base.SetEntityProperty("newFactionName", ImportedFoodorDrink.generateFactionName(text6));
				}
				return;
			}
			num2 += 333;
			if (num <= num2 + 333)
			{
				string text7 = (this.bVillageZero ? CookingRecipe.RollOvenSafeIngredient("Ingredients0") : CookingRecipe.RollOvenSafeIngredient("Ingredients" + Stat.Roll(0, 8).ToString()));
				GameObject gameObject = GameObject.Create(text7);
				string text8;
				if (!gameObject.HasPart<LiquidVolume>())
				{
					text8 = gameObject.DisplayNameOnlyDirectAndStripped;
				}
				else
				{
					text8 = ColorUtility.StripFormatting(gameObject.GetPart<LiquidVolume>().GetPrimaryLiquid().GetName(null));
				}
				text8 = text8.Replace(" injector", "");
				base.AddEntityListItem("signatureDishIngredients", text7, false);
				string text4 = string.Format("The villagers of {0} are known throughout the {1} for their {2} use of {3} in meal preparation.|{4}", new object[]
				{
					snapshotAtYear.Name,
					GameObject.Create("Terrain" + snapshotAtYear.GetProperty("region", "unknown")).DisplayNameOnlyDirectAndStripped,
					base.ExpandString("<spice.adjectivesJudgement.!random>"),
					text8,
					this.id
				});
				base.AddEntityListItem("Gospels", text4, false);
				if (If.Chance(7, null, null))
				{
					base.SetEntityProperty("newFactionName", ImportedFoodorDrink.generateFactionName(text8));
				}
				return;
			}
		}

		// Token: 0x060041E6 RID: 16870 RVA: 0x001525B8 File Offset: 0x001507B8
		public static string generateUniqueVillageLiquid(string tierGroup, List<string> exclusions)
		{
			string blueprint;
			do
			{
				blueprint = PopulationManager.RollOneFrom("Villages_LiquidType_" + tierGroup + "_*Default", null, null).Blueprint;
			}
			while (exclusions.Contains(blueprint));
			return blueprint;
		}

		// Token: 0x060041E7 RID: 16871 RVA: 0x001525EC File Offset: 0x001507EC
		public static string getLiquidTierGroup(int tier)
		{
			if (tier < 3)
			{
				return "Lowtier";
			}
			if (tier < 6)
			{
				return "Midtier";
			}
			return "Hightier";
		}

		// Token: 0x060041E8 RID: 16872 RVA: 0x00152607 File Offset: 0x00150807
		public static string generateFactionName(string root)
		{
			if (If.CoinFlip(null, null))
			{
				return Grammar.MakeTitleCase(HistoricStringExpander.ExpandString("<spice.commonPhrases.cult.!random>", null) + " of the " + root);
			}
			return Grammar.MakeTitleCase(root + HistoricStringExpander.ExpandString(" <spice.commonPhrases.cult.!random>", null));
		}

		// Token: 0x04002052 RID: 8274
		public bool bVillageZero;
	}
}
