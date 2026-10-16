using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using XRL.Rules;
using XRL.UI;
using XRL.Wish;
using XRL.World.AI;
using XRL.World.Parts;

namespace XRL.World.ZoneBuilders
{
	// Token: 0x02000A84 RID: 2692
	[HasWishCommand]
	public class FactionEncounters : ZoneBuilderSandbox
	{
		// Token: 0x06006E8E RID: 28302 RVA: 0x0025DD04 File Offset: 0x0025BF04
		public static bool BuildFactionEncounter(string Faction, Zone Z, int ZoneLevel, int ZoneTier)
		{
			List<GameObject> list = Event.NewGameObjectList();
			GameObject gameObject = null;
			string text;
			int num;
			if (Data.TryGetText("FactionEncounterNumber_" + Faction, out text))
			{
				num = text.RollCached();
			}
			else
			{
				num = Data.GetText("FactionEncounterNumber_*Default").RollCached();
			}
			if (Faction == "Pariahs")
			{
				list.Add(PariahSpawner.GeneratePariah(Math.Max(1, Stat.Random(ZoneLevel - 4, ZoneLevel + 4)), false, false));
				for (int i = 1; i < num; i++)
				{
					list.Add(PariahSpawner.GeneratePariah(Math.Max(1, Stat.Random(ZoneLevel - 4, ZoneLevel + 4)), true, false));
				}
				gameObject = list[0];
				if (The.Game.GetStringGameState("SlynthSettlementFaction", "") == Faction)
				{
					int j = 0;
					int num2 = Stat.Random(1, 3);
					while (j < num2)
					{
						GameObject gameObject2 = GameObject.Create("BaseSlynth");
						list.Add(PariahSpawner.GeneratePariah(gameObject2, false, false));
						j++;
					}
				}
			}
			else
			{
				BallBag<string> ballBag = new BallBag<string>(0);
				foreach (GameObjectBlueprint gameObjectBlueprint in GameObjectFactory.Factory.GetFactionMembers(Faction, false, true))
				{
					if (Math.Abs(ZoneLevel - ZoneBuilderSandbox.GetLevelOfObject(gameObjectBlueprint.Name)) <= 10)
					{
						ballBag.Add(gameObjectBlueprint.Name, Math.Max(5, 25 - Math.Abs(ZoneLevel - ZoneBuilderSandbox.GetLevelOfObject(gameObjectBlueprint.Name))));
					}
				}
				if (ballBag.Count == 0)
				{
					return false;
				}
				for (int k = 0; k < num; k++)
				{
					string text2 = ballBag.PeekOne();
					list.Add(GameObject.Create(text2));
					if (k == 0)
					{
						gameObject = list[k];
					}
				}
			}
			string text3 = ZoneBuilderSandbox.PopulationOr("FactionEncounterPartyObjects_" + Faction, "FactionEncounterPartyObjects_*Default");
			string text4 = ZoneBuilderSandbox.PopulationOr("FactionEncounterZoneObjects_" + Faction, "FactionEncounterZoneObjects_*Default");
			string text5 = ZoneBuilderSandbox.PopulationOr("FactionEncounterMemberInventory_" + Faction, "FactionEncounterMemberInventory_*Default");
			string text6 = ZoneBuilderSandbox.PopulationOr("FactionEncounterLeaderInventory_" + Faction, "FactionEncounterLeaderInventory_*Default");
			if (Faction.Equals("Templar") && gameObject.Blueprint == "Templar Squire")
			{
				gameObject = GameObject.Create("Knight Templar");
				list.Add(gameObject);
			}
			HeroMaker.MakeHero(gameObject, "BaseFactionHeroTemplate_" + Faction, "SpecialFactionHeroTemplate_" + Faction, -1, "Hero");
			if (Faction == "Barathrumites")
			{
				gameObject.AddSkill("Tinkering", null, null);
				gameObject.AddSkill("Tinkering_Repair", null, null);
				gameObject.AddSkill("Tinkering_Tinker1", null, null);
				gameObject.AddSkill("Tinkering_Tinker2", null, null);
			}
			foreach (GameObject gameObject3 in list)
			{
				if (gameObject3 != gameObject)
				{
					gameObject3.SetAlliedLeader<AllyRetinue>(gameObject, 0, false);
					gameObject3.EquipFromPopulationTable(text5, ZoneTier, null, null, false, true);
				}
				else
				{
					gameObject3.EquipFromPopulationTable(text6, ZoneTier, null, null, false, true);
				}
			}
			foreach (PopulationResult populationResult in PopulationManager.Generate(text3, "zonetier", ZoneTier.ToString(), null, null, null))
			{
				for (int l = 0; l < populationResult.Number; l++)
				{
					GameObject gameObject4 = GameObject.Create(populationResult.Blueprint);
					if (!string.IsNullOrEmpty(populationResult.Hint))
					{
						gameObject4.SetStringProperty("PlacementHint", populationResult.Hint, false);
					}
					list.Add(gameObject4);
				}
			}
			ZoneBuilderSandbox.PlaceParty(list, Z, -1);
			foreach (PopulationResult populationResult2 in PopulationManager.Generate(text4, "zonetier", ZoneTier.ToString(), null, null, null))
			{
				for (int m = 0; m < populationResult2.Number; m++)
				{
					ZoneBuilderSandbox.PlaceObjectInArea(Z, Z.area, GameObject.Create(populationResult2.Blueprint), 0, 0, populationResult2.Hint, null, false);
				}
			}
			return true;
		}

		// Token: 0x06006E8F RID: 28303 RVA: 0x0025E158 File Offset: 0x0025C358
		public bool BuildZone(Zone Z)
		{
			int num = 0;
			for (int i = 0; i < this.Rolls; i++)
			{
				if (this.Chance.in100())
				{
					num++;
				}
			}
			if (num > 0)
			{
				int level = Z.Level;
				int newTier = Z.NewTier;
				for (int j = 0; j < num; j++)
				{
					FactionEncounters.BuildFactionEncounter(PopulationManager.RollOneFrom(this.Population, null, null).Blueprint, Z, level, newTier);
				}
			}
			return true;
		}

		// Token: 0x06006E90 RID: 28304 RVA: 0x0025E1C8 File Offset: 0x0025C3C8
		[WishCommand(null, null, Regex = "^factionencounter:\\s*(.*?)(?::(\\d+)){0,2}$")]
		public static bool HandleFactionEncounterWish(Match match)
		{
			string value = match.Groups[1].Value;
			Zone currentZone = The.Player.CurrentZone;
			int num = currentZone.Level;
			int num2 = currentZone.NewTier;
			if (match.Groups[2].Success)
			{
				try
				{
					num2 = Convert.ToInt32(match.Groups[2].Value);
					num = num2 * 5;
				}
				catch
				{
				}
			}
			if (match.Groups[3].Success)
			{
				try
				{
					num = Convert.ToInt32(match.Groups[3].Value);
				}
				catch
				{
				}
			}
			if (!FactionEncounters.BuildFactionEncounter(value, currentZone, num, num2))
			{
				Popup.Show("No members found for '" + value + "'.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
			}
			return true;
		}

		// Token: 0x04003298 RID: 12952
		public string Population = "GenericFactionPopulation";

		// Token: 0x04003299 RID: 12953
		public int Chance = 2;

		// Token: 0x0400329A RID: 12954
		public int Rolls = 2;
	}
}
