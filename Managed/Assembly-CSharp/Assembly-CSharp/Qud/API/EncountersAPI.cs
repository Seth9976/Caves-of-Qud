using System;
using System.Collections.Generic;
using HistoryKit;
using XRL;
using XRL.Rules;
using XRL.World;
using XRL.World.Parts;

namespace Qud.API
{
	// Token: 0x02000406 RID: 1030
	public static class EncountersAPI
	{
		// Token: 0x060026A1 RID: 9889 RVA: 0x000C6AD1 File Offset: 0x000C4CD1
		public static GameObject GetACreature(Predicate<GameObjectBlueprint> filter = null)
		{
			if (If.OneIn(5000, null, null))
			{
				return EncountersAPI.GetAnAnimatedObject();
			}
			return GameObject.Create(EncountersAPI.GetACreatureBlueprint(filter));
		}

		// Token: 0x060026A2 RID: 9890 RVA: 0x000C6AF2 File Offset: 0x000C4CF2
		public static GameObject GetASampleCreature(Predicate<GameObjectBlueprint> filter = null)
		{
			if (If.OneIn(5000, null, null))
			{
				return EncountersAPI.GetAnAnimatedObject();
			}
			return GameObject.CreateSample(EncountersAPI.GetACreatureBlueprint(filter));
		}

		// Token: 0x060026A3 RID: 9891 RVA: 0x000C6B13 File Offset: 0x000C4D13
		public static GameObject GetACreatureFromFaction(string faction, Predicate<GameObjectBlueprint> filter = null)
		{
			return GameObject.Create(EncountersAPI.GetACreatureBlueprintFromFaction(faction, filter));
		}

		// Token: 0x060026A4 RID: 9892 RVA: 0x000C6B24 File Offset: 0x000C4D24
		public static GameObject GetASampleCreatureFromFaction(string faction)
		{
			GameObjectBlueprint randomElement = GameObjectFactory.Factory.GetFactionMembers(faction, false, true).GetRandomElement<GameObjectBlueprint>(null);
			if (randomElement == null)
			{
				return null;
			}
			return randomElement.createSample();
		}

		// Token: 0x060026A5 RID: 9893 RVA: 0x000C6B50 File Offset: 0x000C4D50
		public static GameObject GetAPlant()
		{
			return GameObject.Create(EncountersAPI.GetAPlantBlueprint());
		}

		// Token: 0x060026A6 RID: 9894 RVA: 0x000C6B5C File Offset: 0x000C4D5C
		public static GameObject GetANonLegendaryCreature(Predicate<GameObjectBlueprint> filter = null)
		{
			if (If.OneIn(5000, null, null))
			{
				return EncountersAPI.GetAnAnimatedObject();
			}
			return GameObject.Create(EncountersAPI.GetANonLegendaryCreatureBlueprint(filter));
		}

		// Token: 0x060026A7 RID: 9895 RVA: 0x000C6B7D File Offset: 0x000C4D7D
		public static GameObject GetANonLegendaryCreatureWithAnInventory(Predicate<GameObjectBlueprint> filter = null)
		{
			if (If.OneIn(5000, null, null))
			{
				return EncountersAPI.GetAnAnimatedObject();
			}
			return GameObject.Create(EncountersAPI.GetANonLegendaryCreatureWithAnInventoryBlueprint(filter));
		}

		// Token: 0x060026A8 RID: 9896 RVA: 0x000C6B9E File Offset: 0x000C4D9E
		public static GameObject GetALegendaryEligibleCreature(Predicate<GameObjectBlueprint> filter = null)
		{
			if (If.OneIn(5000, null, null))
			{
				return EncountersAPI.GetAnAnimatedObject();
			}
			return GameObject.Create(EncountersAPI.GetALegendaryEligibleCreatureBlueprint(filter));
		}

		// Token: 0x060026A9 RID: 9897 RVA: 0x000C6BBF File Offset: 0x000C4DBF
		public static GameObject GetALegendaryEligibleCreatureFromFaction(string faction, Predicate<GameObjectBlueprint> filter = null)
		{
			if (If.OneIn(5000, null, null))
			{
				return EncountersAPI.GetAnAnimatedObject();
			}
			return GameObject.Create(EncountersAPI.GetALegendaryEligibleCreatureBlueprintFromFaction(faction, filter));
		}

		// Token: 0x060026AA RID: 9898 RVA: 0x000C6BE1 File Offset: 0x000C4DE1
		public static GameObject GetALegendaryEligibleCreatureWithAnInventory(Predicate<GameObjectBlueprint> filter = null)
		{
			if (If.OneIn(5000, null, null))
			{
				return EncountersAPI.GetAnAnimatedObject();
			}
			return GameObject.Create(EncountersAPI.GetALegendaryEligibleCreatureWithAnInventoryBlueprint(filter));
		}

		// Token: 0x060026AB RID: 9899 RVA: 0x000C6C04 File Offset: 0x000C4E04
		public static GameObject GetCreatureAroundPlayerLevel(Predicate<GameObjectBlueprint> filter = null)
		{
			if (The.Player == null)
			{
				return EncountersAPI.GetCreatureAroundLevel(15, null);
			}
			int num = Stat.Random(-2, 2);
			return EncountersAPI.GetCreatureAroundLevel(Math.Max(The.Player.Stat("Level", 0) + num, 1), filter);
		}

		// Token: 0x060026AC RID: 9900 RVA: 0x000C6C48 File Offset: 0x000C4E48
		public static GameObject GetNonLegendaryCreatureAroundPlayerLevel(Predicate<GameObjectBlueprint> filter = null)
		{
			if (The.Player == null)
			{
				return EncountersAPI.GetNonLegendaryCreatureAroundLevel(15, null);
			}
			int num = Stat.Random(-2, 2);
			return EncountersAPI.GetNonLegendaryCreatureAroundLevel(Math.Max(The.Player.Stat("Level", 0) + num, 1), filter);
		}

		// Token: 0x060026AD RID: 9901 RVA: 0x000C6C8C File Offset: 0x000C4E8C
		public static GameObject GetCreatureAroundLevel(int targetLevel, Predicate<GameObjectBlueprint> filter = null)
		{
			if (If.OneIn(5000, null, null))
			{
				return EncountersAPI.GetAnAnimatedObject();
			}
			List<GameObjectBlueprint> list = new List<GameObjectBlueprint>(64);
			List<string> list2 = new List<string>();
			while (targetLevel > 0)
			{
				foreach (GameObjectBlueprint gameObjectBlueprint in GameObjectFactory.Factory.BlueprintList)
				{
					if (EncountersAPI.IsEligibleForDynamicEncounters(gameObjectBlueprint) && gameObjectBlueprint.HasTag("Creature", false) && gameObjectBlueprint.HasStat("Level") && gameObjectBlueprint.BaseStat("Level", 0) == targetLevel && (filter == null || filter(gameObjectBlueprint)))
					{
						if (gameObjectBlueprint.HasTag("AggregateWith", false))
						{
							string tag = gameObjectBlueprint.GetTag("AggregateWith", "");
							if (list2.Contains(tag))
							{
								continue;
							}
							list2.Add(tag);
						}
						list.Add(gameObjectBlueprint);
					}
				}
				if (list.Count > 0)
				{
					return GameObject.Create(list.GetRandomElement<GameObjectBlueprint>(null).Name);
				}
				targetLevel--;
			}
			return GameObject.Create("Dog");
		}

		// Token: 0x060026AE RID: 9902 RVA: 0x000C6DB0 File Offset: 0x000C4FB0
		public static GameObject GetNonLegendaryCreatureAroundLevel(int targetLevel, Predicate<GameObjectBlueprint> filter = null)
		{
			if (If.OneIn(5000, null, null))
			{
				return EncountersAPI.GetAnAnimatedObject();
			}
			List<GameObjectBlueprint> list = new List<GameObjectBlueprint>(64);
			List<string> list2 = new List<string>();
			while (targetLevel > 0)
			{
				foreach (GameObjectBlueprint gameObjectBlueprint in GameObjectFactory.Factory.BlueprintList)
				{
					if (EncountersAPI.IsEligibleForDynamicEncounters(gameObjectBlueprint) && EncountersAPI.IsLegendaryEligible(gameObjectBlueprint) && gameObjectBlueprint.HasTag("Creature", false) && gameObjectBlueprint.HasStat("Level") && gameObjectBlueprint.BaseStat("Level", 0) == targetLevel && (filter == null || filter(gameObjectBlueprint)))
					{
						if (gameObjectBlueprint.HasTag("AggregateWith", false))
						{
							string tag = gameObjectBlueprint.GetTag("AggregateWith", "");
							if (list2.Contains(tag))
							{
								continue;
							}
							list2.Add(tag);
						}
						list.Add(gameObjectBlueprint);
					}
				}
				if (list.Count > 0)
				{
					return GameObject.Create(list.GetRandomElement<GameObjectBlueprint>(null).Name);
				}
				targetLevel--;
			}
			return GameObject.Create("Dog");
		}

		// Token: 0x060026AF RID: 9903 RVA: 0x000C6EE0 File Offset: 0x000C50E0
		public static GameObject GetAnObject(Predicate<GameObjectBlueprint> filter = null)
		{
			string anObjectBlueprint = EncountersAPI.GetAnObjectBlueprint(filter);
			if (anObjectBlueprint == null)
			{
				return null;
			}
			return GameObject.Create(anObjectBlueprint);
		}

		// Token: 0x060026B0 RID: 9904 RVA: 0x000C6F00 File Offset: 0x000C5100
		public static GameObject GetAnObjectNoExclusions(Predicate<GameObjectBlueprint> filter = null)
		{
			string anObjectBlueprintNoExclusions = EncountersAPI.GetAnObjectBlueprintNoExclusions(filter);
			if (anObjectBlueprintNoExclusions == null)
			{
				return null;
			}
			return GameObject.Create(anObjectBlueprintNoExclusions);
		}

		// Token: 0x060026B1 RID: 9905 RVA: 0x000C6F20 File Offset: 0x000C5120
		public static GameObject GetAnItem(Predicate<GameObjectBlueprint> filter = null)
		{
			string anItemBlueprint = EncountersAPI.GetAnItemBlueprint(filter);
			if (anItemBlueprint == null)
			{
				return null;
			}
			return GameObject.Create(anItemBlueprint);
		}

		// Token: 0x060026B2 RID: 9906 RVA: 0x000C6F40 File Offset: 0x000C5140
		public static GameObject GetAnAnimatedObject()
		{
			List<GameObjectBlueprint> list = new List<GameObjectBlueprint>(64);
			List<string> list2 = new List<string>();
			foreach (GameObjectBlueprint gameObjectBlueprint in GameObjectFactory.Factory.BlueprintList)
			{
				if (EncountersAPI.IsEligibleForDynamicEncounters(gameObjectBlueprint) && gameObjectBlueprint.HasTag("Animatable", false))
				{
					if (gameObjectBlueprint.HasTag("AggregateWith", false))
					{
						string tag = gameObjectBlueprint.GetTag("AggregateWith", "");
						if (list2.Contains(tag))
						{
							continue;
						}
						list2.Add(tag);
					}
					list.Add(gameObjectBlueprint);
				}
			}
			GameObject gameObject = GameObject.Create(list.GetRandomElement<GameObjectBlueprint>(null).Name);
			if (!gameObject.HasPart<Brain>())
			{
				AnimateObject.Animate(gameObject, null, null);
			}
			return gameObject;
		}

		// Token: 0x060026B3 RID: 9907 RVA: 0x000C7018 File Offset: 0x000C5218
		public static GameObjectBlueprint GetACreatureBlueprintModel(Predicate<GameObjectBlueprint> filter = null)
		{
			List<GameObjectBlueprint> list = new List<GameObjectBlueprint>(64);
			List<string> list2 = new List<string>();
			foreach (GameObjectBlueprint gameObjectBlueprint in GameObjectFactory.Factory.BlueprintList)
			{
				if (EncountersAPI.IsEligibleForDynamicEncounters(gameObjectBlueprint) && gameObjectBlueprint.HasTag("Creature", false) && (filter == null || filter(gameObjectBlueprint)))
				{
					if (gameObjectBlueprint.HasTag("AggregateWith", false))
					{
						string tag = gameObjectBlueprint.GetTag("AggregateWith", "");
						if (list2.Contains(tag))
						{
							continue;
						}
						list2.Add(tag);
					}
					list.Add(gameObjectBlueprint);
				}
			}
			if (list.Count <= 0)
			{
				return null;
			}
			return list.GetRandomElement<GameObjectBlueprint>(null);
		}

		// Token: 0x060026B4 RID: 9908 RVA: 0x000C70E4 File Offset: 0x000C52E4
		public static string GetACreatureBlueprint(Predicate<GameObjectBlueprint> filter = null)
		{
			GameObjectBlueprint acreatureBlueprintModel = EncountersAPI.GetACreatureBlueprintModel(filter);
			if (acreatureBlueprintModel != null)
			{
				return acreatureBlueprintModel.Name;
			}
			return null;
		}

		// Token: 0x060026B5 RID: 9909 RVA: 0x000C7104 File Offset: 0x000C5304
		public static string GetACreatureBlueprintFromFaction(string faction, Predicate<GameObjectBlueprint> filter = null)
		{
			List<GameObjectBlueprint> list = new List<GameObjectBlueprint>(64);
			List<string> list2 = new List<string>();
			foreach (GameObjectBlueprint gameObjectBlueprint in GameObjectFactory.Factory.GetFactionMembers(faction, false, true))
			{
				if (EncountersAPI.IsEligibleForDynamicEncounters(gameObjectBlueprint) && (filter == null || filter(gameObjectBlueprint)))
				{
					if (gameObjectBlueprint.HasTag("AggregateWith", false))
					{
						string tag = gameObjectBlueprint.GetTag("AggregateWith", "");
						if (list2.Contains(tag))
						{
							continue;
						}
						list2.Add(tag);
					}
					list.Add(gameObjectBlueprint);
				}
			}
			if (list.Count <= 0)
			{
				return null;
			}
			return list.GetRandomElement<GameObjectBlueprint>(null).Name;
		}

		// Token: 0x060026B6 RID: 9910 RVA: 0x000C71CC File Offset: 0x000C53CC
		public static string GetAPlantBlueprint()
		{
			List<GameObjectBlueprint> list = new List<GameObjectBlueprint>(64);
			List<string> list2 = new List<string>();
			foreach (GameObjectBlueprint gameObjectBlueprint in GameObjectFactory.Factory.BlueprintList)
			{
				if (EncountersAPI.IsEligibleForDynamicEncounters(gameObjectBlueprint) && (gameObjectBlueprint.HasTag("Plant", false) || gameObjectBlueprint.HasTag("PlantLike", false)))
				{
					if (gameObjectBlueprint.HasTag("AggregateWith", false))
					{
						string tag = gameObjectBlueprint.GetTag("AggregateWith", "");
						if (list2.Contains(tag))
						{
							continue;
						}
						list2.Add(tag);
					}
					list.Add(gameObjectBlueprint);
				}
			}
			if (list.Count <= 0)
			{
				return null;
			}
			return list.GetRandomElement<GameObjectBlueprint>(null).Name;
		}

		// Token: 0x060026B7 RID: 9911 RVA: 0x000C72A0 File Offset: 0x000C54A0
		public static string GetANonLegendaryCreatureBlueprint(Predicate<GameObjectBlueprint> filter = null)
		{
			List<GameObjectBlueprint> list = new List<GameObjectBlueprint>(32);
			List<string> list2 = new List<string>();
			foreach (GameObjectBlueprint gameObjectBlueprint in GameObjectFactory.Factory.BlueprintList)
			{
				if (EncountersAPI.IsEligibleForDynamicEncounters(gameObjectBlueprint) && EncountersAPI.IsLegendaryEligible(gameObjectBlueprint))
				{
					if (gameObjectBlueprint.HasTag("AggregateWith", false) && (filter == null || filter(gameObjectBlueprint)))
					{
						string tag = gameObjectBlueprint.GetTag("AggregateWith", "");
						if (list2.Contains(tag))
						{
							continue;
						}
						list2.Add(tag);
					}
					list.Add(gameObjectBlueprint);
				}
			}
			if (list.Count <= 0)
			{
				return null;
			}
			return list.GetRandomElement<GameObjectBlueprint>(null).Name;
		}

		// Token: 0x060026B8 RID: 9912 RVA: 0x000C736C File Offset: 0x000C556C
		public static string GetANonLegendaryCreatureWithAnInventoryBlueprint(Predicate<GameObjectBlueprint> filter = null)
		{
			List<GameObjectBlueprint> list = new List<GameObjectBlueprint>(32);
			List<string> list2 = new List<string>();
			foreach (GameObjectBlueprint gameObjectBlueprint in GameObjectFactory.Factory.BlueprintList)
			{
				if (EncountersAPI.IsEligibleForDynamicEncounters(gameObjectBlueprint) && EncountersAPI.IsLegendaryEligible(gameObjectBlueprint) && gameObjectBlueprint.HasPart("Inventory") && (filter == null || filter(gameObjectBlueprint)))
				{
					if (gameObjectBlueprint.HasTag("AggregateWith", false))
					{
						string tag = gameObjectBlueprint.GetTag("AggregateWith", "");
						if (list2.Contains(tag))
						{
							continue;
						}
						list2.Add(tag);
					}
					list.Add(gameObjectBlueprint);
				}
			}
			if (list.Count <= 0)
			{
				return null;
			}
			return list.GetRandomElement<GameObjectBlueprint>(null).Name;
		}

		// Token: 0x060026B9 RID: 9913 RVA: 0x000C7444 File Offset: 0x000C5644
		public static string GetALegendaryEligibleCreatureBlueprint(Predicate<GameObjectBlueprint> filter = null)
		{
			List<GameObjectBlueprint> list = new List<GameObjectBlueprint>(32);
			List<string> list2 = new List<string>();
			foreach (GameObjectBlueprint gameObjectBlueprint in GameObjectFactory.Factory.BlueprintList)
			{
				if (EncountersAPI.IsEligibleForDynamicEncounters(gameObjectBlueprint) && EncountersAPI.IsLegendaryEligible(gameObjectBlueprint) && gameObjectBlueprint.HasPart("Body") && gameObjectBlueprint.HasPart("Combat") && (filter == null || filter(gameObjectBlueprint)))
				{
					if (gameObjectBlueprint.HasTag("AggregateWith", false))
					{
						string tag = gameObjectBlueprint.GetTag("AggregateWith", "");
						if (list2.Contains(tag))
						{
							continue;
						}
						list2.Add(tag);
					}
					list.Add(gameObjectBlueprint);
				}
			}
			if (list.Count <= 0)
			{
				return null;
			}
			return list.GetRandomElement<GameObjectBlueprint>(null).Name;
		}

		// Token: 0x060026BA RID: 9914 RVA: 0x000C7528 File Offset: 0x000C5728
		public static string GetALegendaryEligibleCreatureBlueprintFromFaction(string faction, Predicate<GameObjectBlueprint> filter = null)
		{
			List<GameObjectBlueprint> list = new List<GameObjectBlueprint>(32);
			List<string> list2 = new List<string>();
			foreach (GameObjectBlueprint gameObjectBlueprint in GameObjectFactory.Factory.GetFactionMembers(faction, false, true))
			{
				if (EncountersAPI.IsEligibleForDynamicEncounters(gameObjectBlueprint) && EncountersAPI.IsLegendaryEligible(gameObjectBlueprint) && gameObjectBlueprint.HasPart("Body") && gameObjectBlueprint.HasPart("Combat") && (filter == null || filter(gameObjectBlueprint)))
				{
					if (gameObjectBlueprint.HasTag("AggregateWith", false))
					{
						string tag = gameObjectBlueprint.GetTag("AggregateWith", "");
						if (list2.Contains(tag))
						{
							continue;
						}
						list2.Add(tag);
					}
					list.Add(gameObjectBlueprint);
				}
			}
			if (list.Count <= 0)
			{
				return null;
			}
			return list.GetRandomElement<GameObjectBlueprint>(null).Name;
		}

		// Token: 0x060026BB RID: 9915 RVA: 0x000C7610 File Offset: 0x000C5810
		public static string GetALegendaryEligibleCreatureWithAnInventoryBlueprint(Predicate<GameObjectBlueprint> filter = null)
		{
			List<GameObjectBlueprint> list = new List<GameObjectBlueprint>(32);
			List<string> list2 = new List<string>();
			foreach (GameObjectBlueprint gameObjectBlueprint in GameObjectFactory.Factory.BlueprintList)
			{
				if (EncountersAPI.IsEligibleForDynamicEncounters(gameObjectBlueprint) && EncountersAPI.IsLegendaryEligible(gameObjectBlueprint) && gameObjectBlueprint.HasPart("Body") && gameObjectBlueprint.HasPart("Combat") && gameObjectBlueprint.HasPart("Inventory") && (filter == null || filter(gameObjectBlueprint)))
				{
					if (gameObjectBlueprint.HasTag("AggregateWith", false))
					{
						string tag = gameObjectBlueprint.GetTag("AggregateWith", "");
						if (list2.Contains(tag))
						{
							continue;
						}
						list2.Add(tag);
					}
					list.Add(gameObjectBlueprint);
				}
			}
			if (list.Count <= 0)
			{
				return null;
			}
			return list.GetRandomElement<GameObjectBlueprint>(null).Name;
		}

		// Token: 0x060026BC RID: 9916 RVA: 0x000C7708 File Offset: 0x000C5908
		public static GameObjectBlueprint GetABlueprintModel(Predicate<GameObjectBlueprint> filter = null)
		{
			List<GameObjectBlueprint> list = new List<GameObjectBlueprint>(64);
			List<string> list2 = new List<string>();
			foreach (GameObjectBlueprint gameObjectBlueprint in GameObjectFactory.Factory.BlueprintList)
			{
				if (filter(gameObjectBlueprint))
				{
					if (gameObjectBlueprint.HasTag("AggregateWith", false))
					{
						string tag = gameObjectBlueprint.GetTag("AggregateWith", "");
						if (list2.Contains(tag))
						{
							continue;
						}
						list2.Add(tag);
					}
					list.Add(gameObjectBlueprint);
				}
			}
			return list.GetRandomElement<GameObjectBlueprint>(null);
		}

		// Token: 0x060026BD RID: 9917 RVA: 0x000C77B0 File Offset: 0x000C59B0
		public static string GetBlueprintWhere(Predicate<GameObjectBlueprint> filter = null)
		{
			GameObjectBlueprint ablueprintModel = EncountersAPI.GetABlueprintModel(filter);
			if (ablueprintModel == null)
			{
				return null;
			}
			return ablueprintModel.Name;
		}

		// Token: 0x060026BE RID: 9918 RVA: 0x000C77C4 File Offset: 0x000C59C4
		public static GameObjectBlueprint GetAnItemBlueprintModel(Predicate<GameObjectBlueprint> filter = null)
		{
			List<GameObjectBlueprint> list = new List<GameObjectBlueprint>(64);
			List<string> list2 = new List<string>();
			foreach (GameObjectBlueprint gameObjectBlueprint in GameObjectFactory.Factory.BlueprintList)
			{
				if (EncountersAPI.IsEligibleForDynamicEncounters(gameObjectBlueprint) && gameObjectBlueprint.HasTag("Item", false) && !gameObjectBlueprint.IsNatural() && gameObjectBlueprint.GetPartParameter<bool>("Physics", "IsReal", true) && (filter == null || filter(gameObjectBlueprint)))
				{
					if (gameObjectBlueprint.HasTag("AggregateWith", false))
					{
						string tag = gameObjectBlueprint.GetTag("AggregateWith", "");
						if (list2.Contains(tag))
						{
							continue;
						}
						list2.Add(tag);
					}
					list.Add(gameObjectBlueprint);
				}
			}
			return list.GetRandomElement<GameObjectBlueprint>(null);
		}

		// Token: 0x060026BF RID: 9919 RVA: 0x000C78A4 File Offset: 0x000C5AA4
		public static string GetAnItemBlueprint(Predicate<GameObjectBlueprint> filter = null)
		{
			GameObjectBlueprint anItemBlueprintModel = EncountersAPI.GetAnItemBlueprintModel(filter);
			if (anItemBlueprintModel == null)
			{
				return null;
			}
			return anItemBlueprintModel.Name;
		}

		// Token: 0x060026C0 RID: 9920 RVA: 0x000C78B8 File Offset: 0x000C5AB8
		public static GameObjectBlueprint GetAnObjectBlueprintModel(Predicate<GameObjectBlueprint> filter = null)
		{
			List<GameObjectBlueprint> list = new List<GameObjectBlueprint>(64);
			List<string> list2 = new List<string>();
			foreach (GameObjectBlueprint gameObjectBlueprint in GameObjectFactory.Factory.BlueprintList)
			{
				if (EncountersAPI.IsEligibleForDynamicEncounters(gameObjectBlueprint) && !gameObjectBlueprint.IsNatural() && gameObjectBlueprint.GetPartParameter<bool>("Physics", "IsReal", true) && (filter == null || filter(gameObjectBlueprint)))
				{
					if (gameObjectBlueprint.HasTag("AggregateWith", false))
					{
						string tag = gameObjectBlueprint.GetTag("AggregateWith", "");
						if (list2.Contains(tag))
						{
							continue;
						}
						list2.Add(tag);
					}
					list.Add(gameObjectBlueprint);
				}
			}
			return list.GetRandomElement<GameObjectBlueprint>(null);
		}

		// Token: 0x060026C1 RID: 9921 RVA: 0x000C7988 File Offset: 0x000C5B88
		public static string GetAnObjectBlueprint(Predicate<GameObjectBlueprint> filter = null)
		{
			GameObjectBlueprint anObjectBlueprintModel = EncountersAPI.GetAnObjectBlueprintModel(filter);
			if (anObjectBlueprintModel == null)
			{
				return null;
			}
			return anObjectBlueprintModel.Name;
		}

		// Token: 0x060026C2 RID: 9922 RVA: 0x000C799C File Offset: 0x000C5B9C
		public static string GetAnObjectBlueprintNoExclusions(Predicate<GameObjectBlueprint> filter = null)
		{
			List<GameObjectBlueprint> list = new List<GameObjectBlueprint>(64);
			List<string> list2 = new List<string>();
			foreach (GameObjectBlueprint gameObjectBlueprint in GameObjectFactory.Factory.BlueprintList)
			{
				if (!gameObjectBlueprint.IntProps.ContainsKey("Natural") && gameObjectBlueprint.GetPartParameter<bool>("Physics", "IsReal", true) && (filter == null || filter(gameObjectBlueprint)))
				{
					if (gameObjectBlueprint.HasTag("AggregateWith", false))
					{
						string tag = gameObjectBlueprint.GetTag("AggregateWith", "");
						if (list2.Contains(tag))
						{
							continue;
						}
						list2.Add(tag);
					}
					list.Add(gameObjectBlueprint);
				}
			}
			if (list.Count <= 0)
			{
				return null;
			}
			return list.GetRandomElement<GameObjectBlueprint>(null).Name;
		}

		// Token: 0x060026C3 RID: 9923 RVA: 0x000C7A7C File Offset: 0x000C5C7C
		public static string GetARandomDescendantOf(string Parent)
		{
			List<GameObjectBlueprint> list = new List<GameObjectBlueprint>(32);
			List<string> list2 = new List<string>();
			foreach (GameObjectBlueprint gameObjectBlueprint in GameObjectFactory.Factory.BlueprintList)
			{
				if (EncountersAPI.IsEligibleForDynamicEncounters(gameObjectBlueprint) && gameObjectBlueprint.DescendsFrom(Parent))
				{
					if (gameObjectBlueprint.HasTag("AggregateWith", false))
					{
						string tag = gameObjectBlueprint.GetTag("AggregateWith", "");
						if (list2.Contains(tag))
						{
							continue;
						}
						list2.Add(tag);
					}
					list.Add(gameObjectBlueprint);
				}
			}
			if (list.Count <= 0)
			{
				return null;
			}
			return list.GetRandomElement<GameObjectBlueprint>(null).Name;
		}

		// Token: 0x060026C4 RID: 9924 RVA: 0x000C7B3C File Offset: 0x000C5D3C
		public static GameObject CreateARandomDescendantOf(string Parent)
		{
			string arandomDescendantOf = EncountersAPI.GetARandomDescendantOf(Parent);
			if (!arandomDescendantOf.IsNullOrEmpty())
			{
				return GameObject.Create(arandomDescendantOf);
			}
			return null;
		}

		// Token: 0x060026C5 RID: 9925 RVA: 0x000C7B60 File Offset: 0x000C5D60
		public static string GetATransmutationBlueprintFor(GameObject Object)
		{
			if (Object.IsCreature)
			{
				return EncountersAPI.GetACreatureBlueprint(null);
			}
			if (!Object.HasTag("Item"))
			{
				return EncountersAPI.GetARandomDescendantOf(EncountersAPI.GetProgenitor(Object).Name);
			}
			if (!Object.HasPart<Food>())
			{
				return EncountersAPI.GetAnItemBlueprint(null);
			}
			return EncountersAPI.GetAnItemBlueprint((GameObjectBlueprint x) => x.HasPart("Food"));
		}

		// Token: 0x060026C6 RID: 9926 RVA: 0x000C7BD0 File Offset: 0x000C5DD0
		private static GameObjectBlueprint GetProgenitor(GameObject Object)
		{
			GameObjectBlueprint gameObjectBlueprint = Object.GetBlueprint(true);
			for (;;)
			{
				GameObjectBlueprint shallowParent = gameObjectBlueprint.ShallowParent;
				if (shallowParent == null || shallowParent.Name.EndsWith("Object"))
				{
					break;
				}
				gameObjectBlueprint = shallowParent;
			}
			return gameObjectBlueprint;
		}

		// Token: 0x060026C7 RID: 9927 RVA: 0x000C7C06 File Offset: 0x000C5E06
		public static bool IsEligibleForDynamicEncounters(GameObjectBlueprint Blueprint)
		{
			return !Blueprint.IsBaseBlueprint() && Blueprint.HasPart("Render") && !Blueprint.IsExcludedFromDynamicEncounters();
		}

		// Token: 0x060026C8 RID: 9928 RVA: 0x000C7C2A File Offset: 0x000C5E2A
		public static bool IsEligibleForDynamicSemanticEncounters(GameObjectBlueprint Blueprint)
		{
			return !Blueprint.IsBaseBlueprint() && Blueprint.HasPart("Render") && (!Blueprint.IsExcludedFromDynamicEncounters() || Blueprint.HasTag("IncludeInDynamicSemanticTables", false));
		}

		// Token: 0x060026C9 RID: 9929 RVA: 0x000C7C5C File Offset: 0x000C5E5C
		public static bool IsLegendaryEligible(GameObjectBlueprint Blueprint)
		{
			return Blueprint.HasTag("Creature", false) && !Blueprint.HasPart("GivesRep") && !Blueprint.HasPart("Uplift") && !Blueprint.Name.Contains("Hero");
		}

		// Token: 0x04001354 RID: 4948
		public const int ANIMATED_OBJECT_CHANCE_ONE_IN = 5000;
	}
}
