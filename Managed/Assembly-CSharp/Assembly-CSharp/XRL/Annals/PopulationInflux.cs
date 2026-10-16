using System;
using System.Collections.Generic;
using System.Linq;
using HistoryKit;
using Qud.API;
using XRL.Language;
using XRL.World;
using XRL.World.Capabilities;

namespace XRL.Annals
{
	// Token: 0x02000629 RID: 1577
	[Serializable]
	public class PopulationInflux : HistoricEvent
	{
		// Token: 0x060041EE RID: 16878 RVA: 0x00153540 File Offset: 0x00151740
		public PopulationInflux()
		{
			this.bVillageZero = false;
		}

		// Token: 0x060041EF RID: 16879 RVA: 0x0015354F File Offset: 0x0015174F
		public PopulationInflux(bool VillageZero)
		{
			this.bVillageZero = VillageZero;
		}

		// Token: 0x060041F0 RID: 16880 RVA: 0x00153560 File Offset: 0x00151760
		public static BallBag<string> GetRoleBag()
		{
			return new BallBag<string>(0)
			{
				{
					"mayor",
					PopulationInflux.MAYOR_WEIGHT
				},
				{
					"warden",
					PopulationInflux.WARDEN_WEIGHT
				},
				{
					"merchant",
					PopulationInflux.MERCHANT_WEIGHT
				},
				{
					"tinker",
					PopulationInflux.TINKER_WEIGHT
				},
				{
					"apothecary",
					PopulationInflux.APOTHECARY_WEIGHT
				},
				{
					"villager",
					PopulationInflux.VILLAGER_WEIGHT
				}
			};
		}

		// Token: 0x060041F1 RID: 16881 RVA: 0x001535D4 File Offset: 0x001517D4
		public override void Generate()
		{
			this.duration = 1L;
			HistoricEntitySnapshot snapshotAtYear = this.entity.GetSnapshotAtYear(this.entity.lastYear);
			int num = base.Random(1, 1000);
			if (num <= 400)
			{
				string text = PopulationInflux.GetRoleBag().PeekOne();
				string blueprint;
				do
				{
					blueprint = PopulationManager.RollOneFrom("LairOwners_" + snapshotAtYear.GetProperty("region", "unknown"), null, null).Blueprint;
				}
				while (!EncountersAPI.IsLegendaryEligible(GameObjectFactory.Factory.Blueprints[blueprint]));
				GameObject gameObject;
				if (!(text == "mayor"))
				{
					if (!(text == "warden"))
					{
						if (!(text == "merchant"))
						{
							if (!(text == "tinker"))
							{
								if (!(text == "apothecary"))
								{
									gameObject = HeroMaker.MakeHero(GameObject.Create(blueprint), null, null, -1, "Hero");
								}
								else
								{
									gameObject = HeroMaker.MakeHero(GameObject.Create(blueprint), "SpecialVillagerHeroTemplate_Apothecary", -1, "Apothecary");
								}
							}
							else
							{
								gameObject = HeroMaker.MakeHero(GameObject.Create(blueprint), "SpecialVillagerHeroTemplate_Tinker", -1, "Tinker");
								base.SetEntityProperty("techTier", Tier.Constrain(snapshotAtYear.TechTier + 1).ToString());
							}
						}
						else
						{
							gameObject = HeroMaker.MakeHero(GameObject.Create(blueprint), "SpecialVillagerHeroTemplate_Merchant", -1, "Merchant");
						}
					}
					else
					{
						gameObject = HeroMaker.MakeHero(GameObject.Create(blueprint), "SpecialVillagerHeroTemplate_Warden", -1, "Warden");
					}
				}
				else
				{
					string text2 = snapshotAtYear.GetProperty("mayorTemplate", "unknown");
					if (text2 == "unknown")
					{
						text2 = "Mayor";
					}
					gameObject = HeroMaker.MakeHero(GameObject.Create(blueprint), "SpecialVillagerHeroTemplate_" + text2, -1, text2);
				}
				Dictionary<string, string> dictionary = QudHistoryHelpers.BuildContextFromObjectTextFragments(blueprint);
				string displayName = gameObject.GetDisplayName(int.MaxValue, null, null, false, false, false, false, true, false, true, false, false, false, true, false, false, null, false, false, false, false, null, false, null, false, true);
				string displayName2 = gameObject.GetDisplayName(int.MaxValue, null, null, false, false, false, false, true, false, true, true, false, false, true, false, false, null, false, false, false, false, null, false, null, false, true);
				try
				{
					string text3 = base.ExpandString("<spice.villages." + text + ".reasonIBecame.!random>", dictionary);
					base.AddEntityListItem("immigrant_name", displayName, true);
					base.AddEntityListItem("immigrant_gender", gameObject.GetGender(false).Name, false);
					base.AddEntityListItem("immigrant_role", text, true);
					base.AddEntityListItem("immigrant_dialogWhy_Q", (If.OneIn(3, null, null) ? "Stranger" : gameObject.FormalAddressTerm) + ", why did you come to this village?", true);
					base.AddEntityListItem("immigrant_dialogWhy_A", text3, true);
					base.AddEntityListItem("immigrant_type", blueprint, true);
					base.AddEntityListItem("sacredThings", displayName, false);
					base.AddEntityListItem("profaneThings", base.ExpandString("those who would <spice.commonPhrases.harm.!random> ") + displayName, false);
					base.AddEntityListItem("itemAdjectiveRoots", displayName2, false);
				}
				catch (Exception ex)
				{
					MetricsManager.LogException("Failed to set up immigrant.", ex, "game_exception");
				}
				string text4;
				if (text == "mayor" || text == "warden")
				{
					text4 = string.Concat(new string[]
					{
						"The villagers of ",
						snapshotAtYear.GetProperty("name", "unknown"),
						" asked ",
						displayName2,
						" to ",
						base.ExpandString("<spice.villages." + text + ".villageTask.!random>").Replace("my", gameObject.its)
					});
				}
				else if (text == "villager")
				{
					text4 = displayName2 + " settled down among the villagers";
				}
				else
				{
					text4 = displayName2 + " set up a shop for their trade";
				}
				int num2 = int.Parse(this.history.GetEntitiesWithProperty("Resheph").GetRandomElement(null).GetCurrentSnapshot()
					.GetProperty("flipYear", "unknown"));
				string text5 = Grammar.InitCap(string.Format("{0}, {1} grew tired of {2} and {3} to a place {4}. There {7} came upon {5} and its inhabitants. {6}.|{8}", new object[]
				{
					base.ExpandString("<spice.instancesOf.openingTime.!random>").Replace("*year*", "%" + (num2 + base.Random(900, 999)).ToString() + "%"),
					gameObject.GetDisplayName(int.MaxValue, null, null, false, false, false, false, true, false, true, true, false, false, true, false, false, null, false, false, false, false, null, false, null, false, true),
					gameObject.GetxTag_CommaDelimited("TextFragments", "Activity", null, null),
					base.ExpandString("<spice.commonPhrases.trekked.!random>"),
					base.ExpandString("<spice.history.regions.terrain." + snapshotAtYear.GetProperty("region", "unknown") + ".over.!random>"),
					snapshotAtYear.GetProperty("name", "unknown"),
					text4,
					gameObject.it,
					this.id
				}));
				base.AddEntityListItem("Gospels", text5, false);
				return;
			}
			if (num <= 800)
			{
				string petType = "nonHumanoid";
				int useTier = Math.Max(snapshotAtYear.Tier, 1);
				string text6;
				if (this.bVillageZero)
				{
					text6 = GameObjectFactory.Factory.BlueprintList.Where<GameObjectBlueprint>(delegate(GameObjectBlueprint bp)
					{
						if (!bp.HasTag("Creature", false) || bp.Tier > useTier || bp.HasTag("BaseObject", false) || bp.HasTag("ExcludeFromDynamicEncounters", false) || bp.HasTag("ExcludeFromVillageZero", false) || bp.HasTag("ExcludeFromVillagePopulations", false) || bp.HasTag("Merchant", false) || bp.HasPart("Breeder") || bp.HasProperName())
						{
							return false;
						}
						if (!(petType == "humanoid"))
						{
							return !bp.HasTag("Humanoid", false);
						}
						return bp.HasTag("Humanoid", false);
					}).GetRandomElement<GameObjectBlueprint>(null).Name;
				}
				else
				{
					text6 = GameObjectFactory.Factory.BlueprintList.Where<GameObjectBlueprint>(delegate(GameObjectBlueprint bp)
					{
						if (!bp.HasTag("Creature", false) || bp.Tier > useTier || bp.HasTag("BaseObject", false) || bp.HasTag("ExcludeFromDynamicEncounters", false) || bp.HasTag("ExcludeFromVillagePopulations", false) || bp.HasTag("Merchant", false) || bp.HasProperName())
						{
							return false;
						}
						if (!(petType == "humanoid"))
						{
							return !bp.HasTag("Humanoid", false);
						}
						return bp.HasTag("Humanoid", false);
					}).GetRandomElement<GameObjectBlueprint>(null).Name;
				}
				GameObject gameObject2 = GameObject.Create(text6);
				string text7;
				int num3;
				if (If.Chance(PopulationInflux.CHANCE_MULTIPLE_PETS, null, null))
				{
					text7 = string.Format("Why are there {0} here?", Grammar.Pluralize(gameObject2.DisplayNameOnly));
					num3 = base.Random(2, 4);
				}
				else
				{
					text7 = string.Format("Why{0} there {1} here?", gameObject2.Is, gameObject2.an(int.MaxValue, null, null, false, false, false, false, false, false, true, true, false, true, false, null, false, null, false));
					num3 = 1;
				}
				base.AddEntityListItem("pet_petType", petType, true);
				base.AddEntityListItem("pet_dialogWhy_Q", text7, true);
				base.AddEntityListItem("pet_number", num3.ToString(), true);
				base.AddEntityListItem("pet_petSpecies", text6, true);
				return;
			}
			if (num <= 1000)
			{
				base.AddEntityListItem("populationMultiplier", base.Random(2, 4).ToString(), false);
				string text8 = Grammar.InitCap(string.Format("As the gospel of {0} spread, {1} {2} the village of {3}.|{4}", new object[]
				{
					snapshotAtYear.sacredThing,
					base.ExpandString("<spice.commonPhrases.folks.!random>"),
					base.ExpandString("<spice.instancesOf.flockedTo.!random>"),
					snapshotAtYear.GetProperty("name", "unknown"),
					this.id
				}));
				base.AddEntityListItem("Gospels", text8, false);
				return;
			}
			string name = Factions.GetRandomFactionWithAtLeastOneMember().Name;
			string text9 = base.ExpandString("<spice.villages.immigrants.immigrantPopReason.!random>", QudHistoryHelpers.BuildContextFromObjectTextFragments(GameObjectFactory.Factory.GetFactionMembers(name, false, true).GetRandomElement<GameObjectBlueprint>(null).Name));
			string text10 = (If.CoinFlip(null, null) ? "half" : "whole");
			string text11 = "false";
			if (snapshotAtYear.GetList("immigrantPop_amount").Count > 0)
			{
				text11 = "true";
				text10 = "whole";
			}
			base.AddEntityListItem("immigrantPop_type", name, true);
			base.AddEntityListItem("immigrantPop_reason", text9, true);
			base.AddEntityListItem("immigrantPop_dialogWhy_Q", string.Format("Why is this village populated with {0}?", Faction.GetFormattedName(name)), true);
			base.AddEntityListItem("immigrantPop_dialogWhy_A", text9, true);
			base.AddEntityListItem("immigrantPop_amount", text10, true);
			base.AddEntityListItem("immigrantPop_secondWave", text11, true);
		}

		// Token: 0x0400205B RID: 8283
		public static readonly int MAYOR_WEIGHT = 10;

		// Token: 0x0400205C RID: 8284
		public static readonly int MERCHANT_WEIGHT = 10;

		// Token: 0x0400205D RID: 8285
		public static readonly int WARDEN_WEIGHT = 10;

		// Token: 0x0400205E RID: 8286
		public static readonly int TINKER_WEIGHT = 10;

		// Token: 0x0400205F RID: 8287
		public static readonly int APOTHECARY_WEIGHT = 10;

		// Token: 0x04002060 RID: 8288
		public static readonly int VILLAGER_WEIGHT = 70;

		// Token: 0x04002061 RID: 8289
		public static readonly int CHANCE_MULTIPLE_PETS = 30;

		// Token: 0x04002062 RID: 8290
		public bool bVillageZero;
	}
}
