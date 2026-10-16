using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using Qud.API;
using XRL.Collections;
using XRL.Core;
using XRL.Rules;
using XRL.UI;
using XRL.Wish;
using XRL.World.Loaders;
using XRL.World.ObjectBuilders;
using XRL.World.Parts;
using XRL.World.Parts.Mutation;
using XRL.World.Parts.Skill;

namespace XRL.World
{
	// Token: 0x0200094F RID: 2383
	[HasWishCommand]
	[HasModSensitiveStaticCache]
	[HasGameBasedStaticCache]
	public class GameObjectFactory
	{
		// Token: 0x170007D4 RID: 2004
		// (get) Token: 0x0600618B RID: 24971 RVA: 0x001DC599 File Offset: 0x001DA799
		public static GameObjectFactory Factory
		{
			get
			{
				if (GameObjectFactory._Factory == null)
				{
					Loading.LoadTask("Loading object blueprints", new Action(GameObjectFactory.LoadFactory), true);
				}
				return GameObjectFactory._Factory;
			}
		}

		// Token: 0x0600618C RID: 24972 RVA: 0x001DC5BE File Offset: 0x001DA7BE
		private static void LoadFactory()
		{
			GameObjectFactory._Factory = new GameObjectFactory();
			GameObjectFactory._Factory.LoadBlueprints();
		}

		// Token: 0x0600618D RID: 24973 RVA: 0x001DC5D4 File Offset: 0x001DA7D4
		public static void Init()
		{
			if (GameObjectFactory._Factory == null)
			{
				Loading.LoadTask("Loading object blueprints", new Action(GameObjectFactory.LoadFactory), true);
			}
		}

		// Token: 0x0600618E RID: 24974 RVA: 0x001DC5F4 File Offset: 0x001DA7F4
		[GameBasedCacheInit]
		private static void GameInit()
		{
			foreach (GameObjectBlueprint gameObjectBlueprint in GameObjectFactory.Factory.BlueprintList)
			{
				gameObjectBlueprint.ClearGameCache();
			}
		}

		// Token: 0x0600618F RID: 24975 RVA: 0x001DC648 File Offset: 0x001DA848
		public void DispatchLoadBlueprints(bool Task = true)
		{
			if (!this.LoadBlueprintsHappened)
			{
				this.LoadBlueprintsHappened = true;
				if (Task)
				{
					Loading.LoadTask("Dispatch LoadBlueprint", new Action(this.CallLoadBlueprint), true);
					return;
				}
				this.CallLoadBlueprint();
			}
		}

		// Token: 0x06006190 RID: 24976 RVA: 0x001DC67A File Offset: 0x001DA87A
		public void Hotload()
		{
			this.Blueprints = new Dictionary<string, GameObjectBlueprint>();
			this.BlueprintList = new List<GameObjectBlueprint>();
			this.LoadBlueprints();
		}

		// Token: 0x06006191 RID: 24977 RVA: 0x001DC698 File Offset: 0x001DA898
		public static GameObject create(string blueprint)
		{
			return GameObjectFactory.Factory.CreateObject(blueprint);
		}

		// Token: 0x06006192 RID: 24978 RVA: 0x001DC6A5 File Offset: 0x001DA8A5
		[Obsolete("Use GameObject.Pool (allow true) or GameObject.Clear (allow false)")]
		public void Pool(GameObject objectToPool, bool allowGameObjectPool = false)
		{
			if (allowGameObjectPool)
			{
				objectToPool.Pool();
				return;
			}
			objectToPool.Clear();
		}

		// Token: 0x06006193 RID: 24979 RVA: 0x001DC6B7 File Offset: 0x001DA8B7
		public bool AnyFactionMembers(string Faction)
		{
			return Faction.AnyMembers(Faction, null, true);
		}

		// Token: 0x06006194 RID: 24980 RVA: 0x001DC6C1 File Offset: 0x001DA8C1
		public List<GameObjectBlueprint> GetFactionMembers(string Faction, bool SkipExclude = false, bool ReadOnly = true)
		{
			return Faction.GetMembers(Faction, null, !SkipExclude, ReadOnly);
		}

		// Token: 0x06006195 RID: 24981 RVA: 0x001DC6D0 File Offset: 0x001DA8D0
		public void InitWeights()
		{
			if (this.TierDeltaWeights == null)
			{
				this.TierDeltaWeights = new Dictionary<int, uint>();
				this.TierDeltaWeights.Add(-7, 10U);
				this.TierDeltaWeights.Add(-6, 100U);
				this.TierDeltaWeights.Add(-5, 1000U);
				this.TierDeltaWeights.Add(-4, 10000U);
				this.TierDeltaWeights.Add(-3, 100000U);
				this.TierDeltaWeights.Add(-2, 1000000U);
				this.TierDeltaWeights.Add(-1, 10000000U);
				this.TierDeltaWeights.Add(0, 100000000U);
				this.TierDeltaWeights.Add(1, 10000000U);
				this.TierDeltaWeights.Add(2, 1000000U);
				this.TierDeltaWeights.Add(3, 100000U);
				this.TierDeltaWeights.Add(4, 10000U);
				this.TierDeltaWeights.Add(5, 1000U);
				this.TierDeltaWeights.Add(6, 100U);
				this.TierDeltaWeights.Add(7, 10U);
			}
			if (this.TechTierDeltaWeights == null)
			{
				this.TechTierDeltaWeights = new Dictionary<int, uint>();
				this.TechTierDeltaWeights.Add(-7, 10U);
				this.TechTierDeltaWeights.Add(-6, 100U);
				this.TechTierDeltaWeights.Add(-5, 1000U);
				this.TechTierDeltaWeights.Add(-4, 10000U);
				this.TechTierDeltaWeights.Add(-3, 100000U);
				this.TechTierDeltaWeights.Add(-2, 1000000U);
				this.TechTierDeltaWeights.Add(-1, 10000000U);
				this.TechTierDeltaWeights.Add(0, 100000000U);
				this.TechTierDeltaWeights.Add(1, 10000000U);
				this.TechTierDeltaWeights.Add(2, 1000000U);
				this.TechTierDeltaWeights.Add(3, 100000U);
				this.TechTierDeltaWeights.Add(4, 10000U);
				this.TechTierDeltaWeights.Add(5, 1000U);
				this.TechTierDeltaWeights.Add(6, 100U);
				this.TechTierDeltaWeights.Add(7, 10U);
			}
			if (this.RoleWeightMultipliers == null)
			{
				this.RoleWeightMultipliers = new Dictionary<string, double>();
				this.RoleWeightMultipliers.Add("Common", 4.0);
				this.RoleWeightMultipliers.Add("Minion", 4.0);
				this.RoleWeightMultipliers.Add("Artillery", 0.25);
				this.RoleWeightMultipliers.Add("Skirmisher", 1.0);
				this.RoleWeightMultipliers.Add("Uncommon", 0.25);
				this.RoleWeightMultipliers.Add("Brute", 0.25);
				this.RoleWeightMultipliers.Add("Tank", 0.25);
				this.RoleWeightMultipliers.Add("Specialist", 0.1);
				this.RoleWeightMultipliers.Add("Rare", 0.01);
				this.RoleWeightMultipliers.Add("Leader", 0.1);
				this.RoleWeightMultipliers.Add("Hero", 0.1);
				this.RoleWeightMultipliers.Add("Epic", 0.01);
			}
		}

		// Token: 0x06006196 RID: 24982 RVA: 0x001DCA40 File Offset: 0x001DAC40
		public void FabricateDynamicInheritsTable(string baseName)
		{
			List<GameObjectBlueprint> list = new List<GameObjectBlueprint>();
			for (int i = 0; i < GameObjectFactory.Factory.BlueprintList.Count; i++)
			{
				if (EncountersAPI.IsEligibleForDynamicEncounters(GameObjectFactory.Factory.BlueprintList[i]) && GameObjectFactory.Factory.BlueprintList[i].DescendsFrom(baseName))
				{
					list.Add(GameObjectFactory.Factory.BlueprintList[i]);
				}
			}
			this.FabricateDynamicPopulationTable("DynamicInheritsTable:" + baseName, list);
		}

		// Token: 0x06006197 RID: 24983 RVA: 0x001DCAC4 File Offset: 0x001DACC4
		public void FabricateDynamicArtifactsTable()
		{
			List<GameObjectBlueprint> list = new List<GameObjectBlueprint>();
			for (int i = 0; i < GameObjectFactory.Factory.BlueprintList.Count; i++)
			{
				if (EncountersAPI.IsEligibleForDynamicEncounters(GameObjectFactory.Factory.BlueprintList[i]) && GameObjectFactory.Factory.BlueprintList[i].GetPartParameter<int>("Examiner", "Complexity", 0) != 0)
				{
					list.Add(GameObjectFactory.Factory.BlueprintList[i]);
				}
			}
			this.FabricateDynamicPopulationTable("DynamicArtifactsTable:", list);
		}

		// Token: 0x06006198 RID: 24984 RVA: 0x001DCB4C File Offset: 0x001DAD4C
		public void FabricateDynamicHasPartTable(string baseName)
		{
			List<GameObjectBlueprint> list = new List<GameObjectBlueprint>();
			for (int i = 0; i < GameObjectFactory.Factory.BlueprintList.Count; i++)
			{
				if (EncountersAPI.IsEligibleForDynamicEncounters(GameObjectFactory.Factory.BlueprintList[i]) && GameObjectFactory.Factory.BlueprintList[i].HasPart(baseName))
				{
					list.Add(GameObjectFactory.Factory.BlueprintList[i]);
				}
			}
			this.FabricateDynamicPopulationTable("DynamicHasPartTable:" + baseName, list);
		}

		// Token: 0x06006199 RID: 24985 RVA: 0x001DCBD0 File Offset: 0x001DADD0
		public PopulationInfo FabricateDynamicSemanticTable(string Name)
		{
			string[] array = Name.Split(':', StringSplitOptions.None);
			string[] array2 = array[1].Split(',', StringSplitOptions.None);
			for (int i = 0; i < array2.Length; i++)
			{
				array2[i] = "Semantic" + array2[i];
			}
			int num = -1;
			int num2;
			if (array.Length > 2 && int.TryParse(array[2], out num2))
			{
				num = num2;
			}
			int num3 = -1;
			if (array.Length > 3 && int.TryParse(array[3], out num2))
			{
				num3 = num2;
			}
			return this.FabricateTagTable(Name, array2, num, num3, true, false, true);
		}

		// Token: 0x0600619A RID: 24986 RVA: 0x001DCC54 File Offset: 0x001DAE54
		public PopulationInfo FabricateTagTable(string Name, string Tag, int Tier = -1, int TechTier = -1, bool Dynamic = false, bool Strict = false, bool Cache = false)
		{
			PopulationInfo populationInfo;
			using (ScopeDisposedList<string> fromPool = ScopeDisposedList<string>.GetFromPool())
			{
				fromPool.Add(Tag);
				populationInfo = this.FabricateTagTable(Name, fromPool, Tier, TechTier, Dynamic, Strict, Cache);
			}
			return populationInfo;
		}

		// Token: 0x0600619B RID: 24987 RVA: 0x001DCCA0 File Offset: 0x001DAEA0
		public PopulationInfo FabricateTagTable(string Name, IReadOnlyList<string> Tags, int Tier = -1, int TechTier = -1, bool Dynamic = false, bool Strict = false, bool Cache = false)
		{
			List<GameObjectBlueprint> list = new List<GameObjectBlueprint>();
			bool flag = !Strict;
			foreach (GameObjectBlueprint gameObjectBlueprint in this.BlueprintList)
			{
				if (!gameObjectBlueprint.IsBaseBlueprint() && (!Dynamic || EncountersAPI.IsEligibleForDynamicSemanticEncounters(gameObjectBlueprint)) && gameObjectBlueprint.HasTag(Tags[0], false))
				{
					bool flag2 = true;
					int i = 1;
					int count = Tags.Count;
					while (i < count)
					{
						if (!gameObjectBlueprint.HasTag(Tags[i], false))
						{
							flag2 = false;
							break;
						}
						i++;
					}
					if (flag2)
					{
						if (flag)
						{
							list.Clear();
							flag = false;
						}
						list.Add(gameObjectBlueprint);
					}
					else if (flag)
					{
						list.Add(gameObjectBlueprint);
					}
				}
			}
			this.Tiers.Clear();
			uint num = 1U;
			this.InitWeights();
			PopulationInfo populationInfo = new PopulationInfo(Name);
			PopulationGroup populationGroup = new PopulationGroup();
			populationGroup.Chance = "100";
			populationGroup.Number = "1";
			populationGroup.Style = "pickone";
			string text = Name + ":Number";
			string text2 = Name + ":Builder";
			string text3 = Name + ":Weight";
			Dictionary<string, PopulationGroup> dictionary = new Dictionary<string, PopulationGroup>();
			for (int j = 0; j < list.Count; j++)
			{
				GameObjectBlueprint gameObjectBlueprint2 = list[j];
				if (!gameObjectBlueprint2.IsBaseBlueprint() && (!Dynamic || EncountersAPI.IsEligibleForDynamicSemanticEncounters(gameObjectBlueprint2)))
				{
					int num2 = ((Tier == -1) ? int.MinValue : (Tier - gameObjectBlueprint2.Tier));
					int num3 = ((TechTier == -1) ? int.MinValue : (Tier - gameObjectBlueprint2.TechTier));
					uint num4 = num;
					uint num5;
					if (num2 != -2147483648 && this.TierDeltaWeights.TryGetValue(num2, out num5))
					{
						num4 += num5;
					}
					if (num3 != -2147483648 && this.TechTierDeltaWeights.TryGetValue(num3, out num5))
					{
						num4 += num5;
					}
					string text4;
					double num6;
					if ((gameObjectBlueprint2.Tags.TryGetValue("Role", out text4) || gameObjectBlueprint2.Props.TryGetValue("Role", out text4)) && this.RoleWeightMultipliers.TryGetValue(text4, out num6))
					{
						num4 = (uint)Math.Ceiling(num4 * num6);
					}
					if (gameObjectBlueprint2.Tags.TryGetValue(text3, out text4))
					{
						try
						{
							double num7 = double.Parse(text4);
							num4 = (uint)Math.Ceiling(num4 * num7);
						}
						catch (Exception ex)
						{
							MetricsManager.LogException("Invalid table weight tag on: " + j.ToString(), ex, "game_exception");
						}
					}
					if (flag)
					{
						uint num8 = 0U;
						int k = 0;
						int count2 = Tags.Count;
						while (k < count2)
						{
							if (gameObjectBlueprint2.HasTag(Tags[k], false))
							{
								num8 += 1U;
							}
							k++;
						}
						num4 *= num8;
					}
					if (num4 > 0U)
					{
						PopulationObject populationObject = new PopulationObject();
						populationObject.Name = gameObjectBlueprint2.Name;
						populationObject.Weight = num4;
						string text5;
						if (gameObjectBlueprint2.Tags.TryGetValue(text, out text5) && !text5.IsNullOrEmpty())
						{
							populationObject.Number = text5;
						}
						string text6;
						if (gameObjectBlueprint2.Tags.TryGetValue(text2, out text6) && !text6.IsNullOrEmpty())
						{
							populationObject.Builder = text6;
						}
						string tag = gameObjectBlueprint2.GetTag("AggregateWith", null);
						if (!tag.IsNullOrEmpty())
						{
							PopulationGroup populationGroup2;
							if (!dictionary.TryGetValue(tag, out populationGroup2))
							{
								populationGroup2 = new PopulationGroup();
								populationGroup2.Name = "aggregate:" + tag;
								populationGroup2.Weight = 1U;
								populationGroup2.Style = "pickone";
								dictionary.Add(tag, populationGroup2);
								populationGroup.Items.Add(populationGroup2);
							}
							populationGroup2.Weight = Math.Max(populationGroup2.Weight, num4);
							populationGroup2.Items.Add(populationObject);
						}
						else
						{
							populationGroup.Items.Add(populationObject);
						}
					}
				}
			}
			populationInfo.Items.Add(populationGroup);
			if (Cache && !PopulationManager.Populations.TryAdd(populationInfo.Name, populationInfo))
			{
				MetricsManager.LogWarning("Double entry during table fabrication for populationInfo: " + populationInfo.Name);
			}
			return populationInfo;
		}

		// Token: 0x0600619C RID: 24988 RVA: 0x001DD0CC File Offset: 0x001DB2CC
		public void FabricateDynamicObjectsTable(string tableName)
		{
			int num = -1;
			int num2 = -1;
			List<GameObjectBlueprint> list = new List<GameObjectBlueprint>();
			foreach (GameObjectBlueprint gameObjectBlueprint in this.BlueprintList)
			{
				if (gameObjectBlueprint.HasTag(tableName, false))
				{
					list.Add(gameObjectBlueprint);
				}
			}
			this.Tiers.Clear();
			uint num3 = 1U;
			this.InitWeights();
			PopulationInfo populationInfo = new PopulationInfo(tableName);
			PopulationGroup populationGroup = new PopulationGroup();
			populationGroup.Chance = "100";
			populationGroup.Number = "1";
			populationGroup.Style = "pickone";
			string text = tableName + ":Number";
			string text2 = tableName + ":Builder";
			string text3 = tableName + ":Weight";
			Dictionary<string, PopulationGroup> dictionary = new Dictionary<string, PopulationGroup>();
			for (int i = 0; i < list.Count; i++)
			{
				if (EncountersAPI.IsEligibleForDynamicEncounters(list[i]))
				{
					int num4 = int.MinValue;
					int num5 = int.MinValue;
					if (num != -1)
					{
						num4 = num - list[i].Tier;
					}
					if (num2 != -1)
					{
						num5 = num2 - list[i].TechTier;
					}
					uint num6 = num3;
					uint num7;
					if (num4 != -2147483648 && this.TierDeltaWeights.TryGetValue(num4, out num7))
					{
						num6 += num7;
					}
					if (num5 != -2147483648 && this.TechTierDeltaWeights.TryGetValue(num5, out num7))
					{
						num6 += num7;
					}
					string text4;
					double num8;
					if ((list[i].Tags.TryGetValue("Role", out text4) || list[i].Props.TryGetValue("Role", out text4)) && this.RoleWeightMultipliers.TryGetValue(text4, out num8))
					{
						num6 = (uint)Math.Ceiling(num6 * num8);
					}
					if (list[i].Tags.ContainsKey(text3))
					{
						try
						{
							double num9 = Convert.ToDouble(list[i].Tags[text3]);
							num6 = (uint)Math.Ceiling(num6 * num9);
						}
						catch (Exception ex)
						{
							MetricsManager.LogException("Invalid table weight tag on: " + i.ToString(), ex, "game_exception");
						}
					}
					if (num6 > 0U)
					{
						string text5;
						if (!list[i].Tags.TryGetValue(text, out text5))
						{
							text5 = "1";
						}
						string text6;
						if (!list[i].Tags.TryGetValue(text2, out text6))
						{
							text6 = "1";
						}
						string tag = list[i].GetTag("AggregateWith", null);
						if (tag != null)
						{
							PopulationGroup populationGroup2;
							if (!dictionary.TryGetValue(tag, out populationGroup2))
							{
								populationGroup2 = new PopulationGroup();
								populationGroup2.Name = "aggregate:" + tag;
								populationGroup2.Weight = 1U;
								populationGroup2.Style = "pickone";
								dictionary.Add(tag, populationGroup2);
								populationGroup.Items.Add(populationGroup2);
							}
							if (populationGroup2.Weight < num6)
							{
								populationGroup2.Weight = num6;
							}
							populationGroup2.Items.Add(new PopulationObject(list[i].Name, text5, num6, text6));
						}
						else
						{
							populationGroup.Items.Add(new PopulationObject(list[i].Name, text5, num6, text6));
						}
					}
				}
			}
			populationInfo.Items.Add(populationGroup);
			if (PopulationManager.Populations.ContainsKey(populationInfo.Name))
			{
				MetricsManager.LogWarning("Double entry during table fabrication for populationInfo: " + populationInfo.Name);
				return;
			}
			PopulationManager.Populations.Add(populationInfo.Name, populationInfo);
		}

		// Token: 0x0600619D RID: 24989 RVA: 0x001DD47C File Offset: 0x001DB67C
		public void FabricateStaticObjectsTable(string tableName)
		{
			int num = -1;
			int num2 = -1;
			List<GameObjectBlueprint> list = new List<GameObjectBlueprint>();
			foreach (GameObjectBlueprint gameObjectBlueprint in this.BlueprintList)
			{
				if (gameObjectBlueprint.HasTag(tableName, false))
				{
					list.Add(gameObjectBlueprint);
				}
			}
			this.Tiers.Clear();
			uint num3 = 1U;
			this.InitWeights();
			PopulationInfo populationInfo = new PopulationInfo(tableName);
			PopulationGroup populationGroup = new PopulationGroup();
			populationGroup.Chance = "100";
			populationGroup.Number = "1";
			populationGroup.Style = "pickone";
			string text = tableName + ":Number";
			string text2 = tableName + ":Builder";
			string text3 = tableName + ":Weight";
			Dictionary<string, PopulationGroup> dictionary = new Dictionary<string, PopulationGroup>();
			for (int i = 0; i < list.Count; i++)
			{
				if (!list[i].IsBaseBlueprint())
				{
					int num4 = int.MinValue;
					int num5 = int.MinValue;
					if (num != -1)
					{
						num4 = num - list[i].Tier;
					}
					if (num2 != -1)
					{
						num5 = num2 - list[i].TechTier;
					}
					uint num6 = num3;
					uint num7;
					if (num4 != -2147483648 && this.TierDeltaWeights.TryGetValue(num4, out num7))
					{
						num6 += num7;
					}
					if (num5 != -2147483648 && this.TechTierDeltaWeights.TryGetValue(num5, out num7))
					{
						num6 += num7;
					}
					string text4;
					double num8;
					if ((list[i].Tags.TryGetValue("Role", out text4) || list[i].Props.TryGetValue("Role", out text4)) && this.RoleWeightMultipliers.TryGetValue(text4, out num8))
					{
						num6 = (uint)Math.Ceiling(num6 * num8);
					}
					if (list[i].Tags.ContainsKey(text3))
					{
						try
						{
							double num9 = Convert.ToDouble(list[i].Tags[text3]);
							num6 = (uint)Math.Ceiling(num6 * num9);
						}
						catch (Exception ex)
						{
							MetricsManager.LogException("Invalid table weight tag on: " + i.ToString(), ex, "game_exception");
						}
					}
					if (num6 > 0U)
					{
						string text5;
						if (!list[i].Tags.TryGetValue(text, out text5))
						{
							text5 = "1";
						}
						string text6;
						if (!list[i].Tags.TryGetValue(text2, out text6))
						{
							text6 = "1";
						}
						string tag = list[i].GetTag("AggregateWith", null);
						if (tag != null)
						{
							PopulationGroup populationGroup2;
							if (!dictionary.TryGetValue(tag, out populationGroup2))
							{
								populationGroup2 = new PopulationGroup();
								populationGroup2.Name = "aggregate:" + tag;
								populationGroup2.Weight = 1U;
								populationGroup2.Style = "pickone";
								dictionary.Add(tag, populationGroup2);
								populationGroup.Items.Add(populationGroup2);
							}
							if (populationGroup2.Weight < num6)
							{
								populationGroup2.Weight = num6;
							}
							populationGroup2.Items.Add(new PopulationObject(list[i].Name, text5, num6, text6));
						}
						else
						{
							populationGroup.Items.Add(new PopulationObject(list[i].Name, text5, num6, text6));
						}
					}
				}
			}
			populationInfo.Items.Add(populationGroup);
			if (PopulationManager.Populations.ContainsKey(populationInfo.Name))
			{
				MetricsManager.LogWarning("Double entry during table fabrication for populationInfo: " + populationInfo.Name);
				return;
			}
			PopulationManager.Populations.Add(populationInfo.Name, populationInfo);
		}

		// Token: 0x0600619E RID: 24990 RVA: 0x001DD82C File Offset: 0x001DBA2C
		public void FabricateDynamicPopulationTable(string tableName, List<GameObjectBlueprint> dynamicTableObjects)
		{
			this.Tiers.Clear();
			uint num = 1U;
			this.InitWeights();
			for (int i = 0; i <= 8; i++)
			{
				PopulationInfo populationInfo = new PopulationInfo();
				if (i == 0)
				{
					populationInfo.Name = tableName;
				}
				else
				{
					populationInfo.Name = tableName + ":Tier" + i.ToString();
				}
				PopulationGroup populationGroup = new PopulationGroup();
				populationGroup.Chance = "100";
				populationGroup.Number = "1";
				populationGroup.Style = "pickone";
				string text = tableName + ":Number";
				string text2 = tableName + ":Builder";
				string text3 = tableName + ":Weight";
				Dictionary<string, PopulationGroup> dictionary = new Dictionary<string, PopulationGroup>();
				for (int j = 0; j < dynamicTableObjects.Count; j++)
				{
					if (EncountersAPI.IsEligibleForDynamicEncounters(dynamicTableObjects[j]))
					{
						int num2 = i - dynamicTableObjects[j].Tier;
						if (i == 0)
						{
							num2 = 0;
						}
						uint num3;
						if (!this.TierDeltaWeights.TryGetValue(num2, out num3))
						{
							num3 = num;
						}
						string text4;
						double num4;
						if ((dynamicTableObjects[j].Tags.TryGetValue("Role", out text4) || dynamicTableObjects[j].Props.TryGetValue("Role", out text4)) && this.RoleWeightMultipliers.TryGetValue(text4, out num4))
						{
							num3 = (uint)Math.Ceiling(num3 * num4);
						}
						if (dynamicTableObjects[j].Tags.ContainsKey(text3))
						{
							try
							{
								double num5 = Convert.ToDouble(dynamicTableObjects[j].Tags[text3]);
								num3 = (uint)Math.Ceiling(num3 * num5);
							}
							catch (Exception ex)
							{
								MetricsManager.LogException("Invalid table weight tag on: " + j.ToString(), ex, "game_exception");
							}
						}
						if (num3 > 0U)
						{
							string text5;
							if (!dynamicTableObjects[j].Tags.TryGetValue(text, out text5))
							{
								text5 = "1";
							}
							string text6;
							if (!dynamicTableObjects[j].Tags.TryGetValue(text2, out text6))
							{
								text6 = "1";
							}
							string tag = dynamicTableObjects[j].GetTag("AggregateWith", null);
							if (tag != null)
							{
								PopulationGroup populationGroup2;
								if (!dictionary.TryGetValue(tag, out populationGroup2))
								{
									populationGroup2 = new PopulationGroup();
									populationGroup2.Name = "aggregate:" + tag;
									populationGroup2.Weight = 1U;
									populationGroup2.Style = "pickone";
									dictionary.Add(tag, populationGroup2);
									populationGroup.Items.Add(populationGroup2);
								}
								if (populationGroup2.Weight < num3)
								{
									populationGroup2.Weight = num3;
								}
								populationGroup2.Items.Add(new PopulationObject(dynamicTableObjects[j].Name, text5, num3, text6));
							}
							else
							{
								populationGroup.Items.Add(new PopulationObject(dynamicTableObjects[j].Name, text5, num3, text6));
							}
						}
					}
				}
				populationInfo.Items.Add(populationGroup);
				if (PopulationManager.Populations.ContainsKey(populationInfo.Name))
				{
					MetricsManager.LogWarning("Double entry during table fabrication for populationInfo: " + populationInfo.Name);
				}
				else
				{
					PopulationManager.Populations.Add(populationInfo.Name, populationInfo);
				}
			}
		}

		// Token: 0x0600619F RID: 24991 RVA: 0x001DDB58 File Offset: 0x001DBD58
		public void FabricateMultitierDynamicPopulationTable(string tableName, IEnumerable<GameObjectBlueprint> dynamicTableObjects, int minTier, int maxTier)
		{
			this.Tiers.Clear();
			uint num = 1U;
			this.InitWeights();
			PopulationInfo populationInfo = new PopulationInfo();
			populationInfo.Name = tableName;
			PopulationGroup populationGroup = new PopulationGroup();
			populationGroup.Chance = "100";
			populationGroup.Number = "1";
			populationGroup.Style = "pickone";
			string text = tableName + ":Number";
			string text2 = tableName + ":Builder";
			string text3 = tableName + ":Weight";
			Dictionary<string, PopulationGroup> dictionary = new Dictionary<string, PopulationGroup>();
			foreach (GameObjectBlueprint gameObjectBlueprint in dynamicTableObjects)
			{
				if (EncountersAPI.IsEligibleForDynamicEncounters(gameObjectBlueprint))
				{
					int num2 = 0;
					if (gameObjectBlueprint.Tier < minTier || gameObjectBlueprint.Tier > maxTier)
					{
						num2 = Math.Min(Math.Abs(minTier - gameObjectBlueprint.Tier), Math.Abs(minTier - gameObjectBlueprint.Tier));
					}
					if (num2 < 0)
					{
						num2 = 0;
					}
					uint num3;
					if (!this.TierDeltaWeights.TryGetValue(num2, out num3))
					{
						num3 = num;
					}
					string text4;
					double num4;
					if ((gameObjectBlueprint.Tags.TryGetValue("Role", out text4) || gameObjectBlueprint.Props.TryGetValue("Role", out text4)) && this.RoleWeightMultipliers.TryGetValue(text4, out num4))
					{
						num3 = (uint)Math.Ceiling(num3 * num4);
					}
					if (gameObjectBlueprint.Tags.ContainsKey(text3))
					{
						try
						{
							double num5 = Convert.ToDouble(gameObjectBlueprint.Tags[text3]);
							num3 = (uint)Math.Ceiling(num3 * num5);
						}
						catch (Exception ex)
						{
							string text5 = "Invalid table weight tag on: ";
							GameObjectBlueprint gameObjectBlueprint2 = gameObjectBlueprint;
							MetricsManager.LogException(text5 + ((gameObjectBlueprint2 != null) ? gameObjectBlueprint2.ToString() : null), ex, "game_exception");
						}
					}
					if (num3 > 0U)
					{
						string text6;
						if (!gameObjectBlueprint.Tags.TryGetValue(text, out text6))
						{
							text6 = "1";
						}
						string text7;
						if (!gameObjectBlueprint.Tags.TryGetValue(text2, out text7))
						{
							text7 = "1";
						}
						string tag = gameObjectBlueprint.GetTag("AggregateWith", null);
						if (tag != null)
						{
							PopulationGroup populationGroup2;
							if (!dictionary.TryGetValue(tag, out populationGroup2))
							{
								populationGroup2 = new PopulationGroup();
								populationGroup2.Name = "aggregate:" + tag;
								populationGroup2.Weight = 1U;
								populationGroup2.Style = "pickone";
								dictionary.Add(tag, populationGroup2);
								populationGroup.Items.Add(populationGroup2);
							}
							if (populationGroup2.Weight < num3)
							{
								populationGroup2.Weight = num3;
							}
							populationGroup2.Items.Add(new PopulationObject(gameObjectBlueprint.Name, text6, num3, text7));
						}
						else
						{
							populationGroup.Items.Add(new PopulationObject(gameObjectBlueprint.Name, text6, num3, text7));
						}
					}
				}
			}
			populationInfo.Items.Add(populationGroup);
			if (PopulationManager.Populations.ContainsKey(populationInfo.Name))
			{
				MetricsManager.LogWarning("Double entry during table fabrication for populationInfo: " + populationInfo.Name);
				return;
			}
			PopulationManager.Populations.Add(populationInfo.Name, populationInfo);
		}

		// Token: 0x060061A0 RID: 24992 RVA: 0x001DDE80 File Offset: 0x001DC080
		public List<GameObjectBlueprint> GetBlueprintsWithTag(string Tag, bool ExcludeBase = true)
		{
			List<GameObjectBlueprint> list = new List<GameObjectBlueprint>();
			foreach (GameObjectBlueprint gameObjectBlueprint in this.BlueprintList)
			{
				if (gameObjectBlueprint.HasTag(Tag, false) && (!ExcludeBase || !gameObjectBlueprint.IsBaseBlueprint()))
				{
					list.Add(gameObjectBlueprint);
				}
			}
			return list;
		}

		// Token: 0x060061A1 RID: 24993 RVA: 0x001DDEF0 File Offset: 0x001DC0F0
		public List<GameObjectBlueprint> GetBlueprintsInheritingFrom(string Name, bool ExcludeBase = true)
		{
			List<GameObjectBlueprint> list = new List<GameObjectBlueprint>();
			foreach (GameObjectBlueprint gameObjectBlueprint in this.BlueprintList)
			{
				if (gameObjectBlueprint.InheritsFrom(Name) && (!ExcludeBase || !gameObjectBlueprint.IsBaseBlueprint()))
				{
					list.Add(gameObjectBlueprint);
				}
			}
			return list;
		}

		// Token: 0x060061A2 RID: 24994 RVA: 0x001DDF60 File Offset: 0x001DC160
		[return: TupleElementNames(new string[] { "Load", "Blueprints" })]
		public Dictionary<Type, ValueTuple<MethodInfo, List<GameObjectBlueprint>>> GetBlueprintsWantingPreload()
		{
			List<Type> typesWithAttribute = ModManager.GetTypesWithAttribute(typeof(WantLoadBlueprintAttribute), true);
			Type[] array = new Type[] { typeof(GameObjectBlueprint) };
			Dictionary<Type, ValueTuple<MethodInfo, List<GameObjectBlueprint>>> dictionary = new Dictionary<Type, ValueTuple<MethodInfo, List<GameObjectBlueprint>>>(typesWithAttribute.Count);
			foreach (Type type in typesWithAttribute)
			{
				MethodInfo method = type.GetMethod("LoadBlueprint", BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic, null, CallingConventions.Any, array, null);
				if (method != null)
				{
					dictionary[type] = new ValueTuple<MethodInfo, List<GameObjectBlueprint>>(method, new List<GameObjectBlueprint>());
				}
			}
			foreach (KeyValuePair<Type, ValueTuple<MethodInfo, List<GameObjectBlueprint>>> keyValuePair in dictionary)
			{
				Type type2;
				ValueTuple<MethodInfo, List<GameObjectBlueprint>> valueTuple;
				keyValuePair.Deconstruct(out type2, out valueTuple);
				ref ValueTuple<MethodInfo, List<GameObjectBlueprint>> ptr = valueTuple;
				Type type3 = type2;
				List<GameObjectBlueprint> item = ptr.Item2;
				foreach (GameObjectBlueprint gameObjectBlueprint in this.BlueprintList)
				{
					foreach (KeyValuePair<string, GamePartBlueprint> keyValuePair2 in gameObjectBlueprint.Parts)
					{
						if (keyValuePair2.Value.T == type3)
						{
							item.Add(gameObjectBlueprint);
							break;
						}
					}
				}
			}
			return dictionary;
		}

		// Token: 0x060061A3 RID: 24995 RVA: 0x001DE100 File Offset: 0x001DC300
		public void LoadBlueprints()
		{
			MutationFactory.CheckInit();
			ObjectBlueprintLoader objectBlueprintLoader = new ObjectBlueprintLoader();
			objectBlueprintLoader.LoadAllBlueprints();
			foreach (ObjectBlueprintLoader.ObjectBlueprintXMLData objectBlueprintXMLData in objectBlueprintLoader.BakedBlueprints())
			{
				GameObjectBlueprint gameObjectBlueprint = this.LoadBakedXML(objectBlueprintXMLData);
				this.Blueprints[objectBlueprintXMLData.Name] = gameObjectBlueprint;
				this.BlueprintList.Add(gameObjectBlueprint);
				if (!objectBlueprintXMLData.PreviousName.IsNullOrEmpty())
				{
					CompatManager.SetCompatEntry("blueprint", objectBlueprintXMLData.PreviousName, objectBlueprintXMLData.Name);
				}
			}
			foreach (GameObjectBlueprint gameObjectBlueprint2 in this.BlueprintList)
			{
				GameObjectBlueprint shallowParent = gameObjectBlueprint2.ShallowParent;
				if (shallowParent != null)
				{
					shallowParent.hasChildren = true;
				}
			}
			StringMap<string> stringMap;
			if (CompatManager.CompatEntries.TryGetValue("blueprint", out stringMap))
			{
				foreach (KeyValuePair<string, string> keyValuePair in stringMap)
				{
					GameObjectBlueprint gameObjectBlueprint3;
					if (this.Blueprints.TryGetValue(keyValuePair.Value, out gameObjectBlueprint3))
					{
						this.Blueprints.TryAdd(keyValuePair.Key, gameObjectBlueprint3);
					}
				}
			}
		}

		// Token: 0x060061A4 RID: 24996 RVA: 0x001DE264 File Offset: 0x001DC464
		public KeyValuePair<string, Dictionary<string, string>> ParseXTagNode(ObjectBlueprintLoader.ObjectBlueprintXMLChildNode node)
		{
			return new KeyValuePair<string, Dictionary<string, string>>(node.NodeName.Substring(4), new Dictionary<string, string>(node.Attributes));
		}

		// Token: 0x060061A5 RID: 24997 RVA: 0x001DE284 File Offset: 0x001DC484
		public Statistic ParseStatNode(ObjectBlueprintLoader.ObjectBlueprintXMLChildNode node, GameObjectBlueprint blueprint = null)
		{
			Statistic statistic = new Statistic();
			statistic.Name = node.Name;
			try
			{
				if (node.Attributes.ContainsKey("Min"))
				{
					statistic.Min = Convert.ToInt32(node.Attributes["Min"]);
				}
				if (node.Attributes.ContainsKey("Max"))
				{
					statistic.Max = Convert.ToInt32(node.Attributes["Max"]);
				}
				if (node.Attributes.ContainsKey("Value"))
				{
					statistic.BaseValue = Convert.ToInt32(node.Attributes["Value"]);
				}
				if (node.Attributes.ContainsKey("Boost"))
				{
					statistic.Boost = Convert.ToInt32(node.Attributes["Boost"]);
				}
				if (node.Attributes.ContainsKey("sValue"))
				{
					statistic.sValue = node.Attributes["sValue"];
				}
			}
			catch (Exception ex)
			{
				MetricsManager.LogError("Error parsing stat parameters for " + (((blueprint != null) ? blueprint.Name : null) ?? "unknown blueprint"), ex);
			}
			return statistic;
		}

		// Token: 0x060061A6 RID: 24998 RVA: 0x001DE3BC File Offset: 0x001DC5BC
		public InventoryObject ParseInventoryObjectNode(ObjectBlueprintLoader.ObjectBlueprintXMLChildNode node)
		{
			bool flag;
			bool.TryParse(node.GetAttribute("BoostModChance"), out flag);
			int num;
			int.TryParse(node.GetAttribute("SetMods"), out num);
			string attribute = node.GetAttribute("AutoMod");
			string attribute2 = node.GetAttribute("Number");
			string text = "1";
			if (!attribute2.IsNullOrEmpty())
			{
				text = attribute2;
			}
			string attribute3 = node.GetAttribute("Chance");
			int num2 = 100;
			if (!attribute3.IsNullOrEmpty())
			{
				int.TryParse(attribute3, out num2);
			}
			bool flag2;
			bool.TryParse(node.GetAttribute("NoSell"), out flag2);
			bool flag3;
			bool.TryParse(node.GetAttribute("NoEquip"), out flag3);
			bool flag4;
			bool.TryParse(node.GetAttribute("NotReal"), out flag4);
			bool flag5;
			bool.TryParse(node.GetAttribute("Full"), out flag5);
			string attribute4 = node.GetAttribute("CellChance");
			int? num3 = null;
			int num4;
			if (!attribute4.IsNullOrEmpty() && int.TryParse(attribute4, out num4))
			{
				num3 = new int?(num4);
			}
			string attribute5 = node.GetAttribute("CellFullChance");
			int? num5 = null;
			int num6;
			if (!attribute5.IsNullOrEmpty() && int.TryParse(attribute5, out num6))
			{
				num5 = new int?(num6);
			}
			string attribute6 = node.GetAttribute("StringProperties");
			Dictionary<string, string> dictionary = null;
			if (!attribute6.IsNullOrEmpty())
			{
				string[] array = attribute6.Split(',', StringSplitOptions.None);
				dictionary = new Dictionary<string, string>(array.Length);
				string[] array2 = array;
				for (int i = 0; i < array2.Length; i++)
				{
					string[] array3 = array2[i].Split(':', StringSplitOptions.None);
					dictionary.Add(array3[0], array3[1]);
				}
			}
			string attribute7 = node.GetAttribute("IntProperties");
			Dictionary<string, int> dictionary2 = null;
			if (!attribute7.IsNullOrEmpty())
			{
				string[] array4 = attribute7.Split(',', StringSplitOptions.None);
				dictionary2 = new Dictionary<string, int>(array4.Length);
				string[] array2 = array4;
				for (int i = 0; i < array2.Length; i++)
				{
					string[] array5 = array2[i].Split(':', StringSplitOptions.None);
					dictionary2.Add(array5[0], Convert.ToInt32(array5[1]));
				}
			}
			string attribute8 = node.GetAttribute("Blueprint");
			string attribute9 = node.GetAttribute("CellType");
			string text2 = text;
			int num7 = num2;
			bool flag6 = flag;
			return new InventoryObject(attribute8, text2, num7, num, flag6, flag3, flag2, flag4, flag5, num3, num5, attribute9, attribute, dictionary, dictionary2);
		}

		// Token: 0x060061A7 RID: 24999 RVA: 0x001DE5EC File Offset: 0x001DC7EC
		public GameObjectBlueprint LoadBakedXML(ObjectBlueprintLoader.ObjectBlueprintXMLData node)
		{
			try
			{
				GameObjectBlueprint gameObjectBlueprint = new GameObjectBlueprint();
				gameObjectBlueprint.Name = node.Name;
				gameObjectBlueprint.Inherits = node.Inherits;
				this.Blueprints[node.Name] = gameObjectBlueprint;
				foreach (KeyValuePair<string, ObjectBlueprintLoader.ObjectBlueprintXMLChildNode> keyValuePair in node.NamedNodes("part"))
				{
					GamePartBlueprint gamePartBlueprint = new GamePartBlueprint("XRL.World.Parts", keyValuePair.Key);
					gamePartBlueprint.Name = keyValuePair.Value.Name;
					gamePartBlueprint.Parameters = keyValuePair.Value.Attributes;
					gameObjectBlueprint.UpdatePart(gamePartBlueprint.Name, gamePartBlueprint);
				}
				foreach (KeyValuePair<string, ObjectBlueprintLoader.ObjectBlueprintXMLChildNode> keyValuePair2 in node.NamedNodes("mutation"))
				{
					GamePartBlueprint gamePartBlueprint2 = new GamePartBlueprint("XRL.World.Parts.Mutation", keyValuePair2.Key);
					gamePartBlueprint2.Name = keyValuePair2.Value.Name;
					gamePartBlueprint2.Parameters = keyValuePair2.Value.Attributes;
					gameObjectBlueprint.Mutations[gamePartBlueprint2.Name] = gamePartBlueprint2;
				}
				foreach (KeyValuePair<string, ObjectBlueprintLoader.ObjectBlueprintXMLChildNode> keyValuePair3 in node.NamedNodes("builder"))
				{
					GamePartBlueprint gamePartBlueprint3 = new GamePartBlueprint("XRL.World.ObjectBuilders", keyValuePair3.Key);
					gamePartBlueprint3.Name = keyValuePair3.Value.Name;
					gamePartBlueprint3.Parameters = keyValuePair3.Value.Attributes;
					gameObjectBlueprint.Builders[gamePartBlueprint3.Name] = gamePartBlueprint3;
				}
				foreach (KeyValuePair<string, ObjectBlueprintLoader.ObjectBlueprintXMLChildNode> keyValuePair4 in node.NamedNodes("skill"))
				{
					GamePartBlueprint gamePartBlueprint4 = new GamePartBlueprint("XRL.World.Parts.Skill", keyValuePair4.Key);
					gamePartBlueprint4.Name = keyValuePair4.Value.Name;
					gamePartBlueprint4.Parameters = keyValuePair4.Value.Attributes;
					gameObjectBlueprint.Skills[gamePartBlueprint4.Name] = gamePartBlueprint4;
				}
				foreach (KeyValuePair<string, ObjectBlueprintLoader.ObjectBlueprintXMLChildNode> keyValuePair5 in node.NamedNodes("stat"))
				{
					gameObjectBlueprint.UpdateStat(keyValuePair5.Key, this.ParseStatNode(keyValuePair5.Value, gameObjectBlueprint));
				}
				foreach (KeyValuePair<string, ObjectBlueprintLoader.ObjectBlueprintXMLChildNode> keyValuePair6 in node.NamedNodes("property"))
				{
					string attribute = keyValuePair6.Value.GetAttribute("Value");
					if (attribute == null || (!attribute.Contains("{{{remove}}}") && !attribute.Contains("*delete")))
					{
						gameObjectBlueprint.Props[keyValuePair6.Key] = attribute;
					}
				}
				foreach (KeyValuePair<string, ObjectBlueprintLoader.ObjectBlueprintXMLChildNode> keyValuePair7 in node.NamedNodes("intproperty"))
				{
					string attribute2 = keyValuePair7.Value.GetAttribute("Value");
					if (attribute2 == null || (!attribute2.Contains("{{{remove}}}") && !attribute2.Contains("*delete")))
					{
						gameObjectBlueprint.IntProps[keyValuePair7.Key] = Convert.ToInt32(attribute2);
					}
				}
				foreach (KeyValuePair<string, ObjectBlueprintLoader.ObjectBlueprintXMLChildNode> keyValuePair8 in node.NamedNodes("tag").Concat<KeyValuePair<string, ObjectBlueprintLoader.ObjectBlueprintXMLChildNode>>(node.NamedNodes("stag")))
				{
					string text = keyValuePair8.Value.Name;
					string text2;
					if (!keyValuePair8.Value.Attributes.TryGetValue("Value", out text2))
					{
						text2 = "";
					}
					if (keyValuePair8.Value.NodeName == "stag")
					{
						text = "Semantic" + text;
						if (text2 == "")
						{
							text2 = "true";
						}
					}
					if (!text2.Contains("{{{remove}}}") && !text2.Contains("*delete"))
					{
						gameObjectBlueprint.Tags.Add(text, text2);
					}
				}
				if (node.Children.ContainsKey("inventoryobject"))
				{
					if (node.UnnamedNodes("inventoryobject") == null)
					{
						MetricsManager.LogWarning("likely missing Blueprint attribute on inventoryobject " + node.ToString());
					}
					else
					{
						gameObjectBlueprint.Inventory = new List<InventoryObject>(node.Children["inventoryobject"].Unnamed.Count);
						foreach (ObjectBlueprintLoader.ObjectBlueprintXMLChildNode objectBlueprintXMLChildNode in node.UnnamedNodes("inventoryobject"))
						{
							gameObjectBlueprint.Inventory.Add(this.ParseInventoryObjectNode(objectBlueprintXMLChildNode));
						}
					}
				}
				foreach (string text3 in node.Children.Keys)
				{
					if (text3.StartsWith("xtag"))
					{
						if (gameObjectBlueprint.xTags == null)
						{
							gameObjectBlueprint.xTags = new Dictionary<string, Dictionary<string, string>>();
						}
						foreach (ObjectBlueprintLoader.ObjectBlueprintXMLChildNode objectBlueprintXMLChildNode2 in node.UnnamedNodes(text3))
						{
							KeyValuePair<string, Dictionary<string, string>> keyValuePair9 = this.ParseXTagNode(objectBlueprintXMLChildNode2);
							gameObjectBlueprint.xTags.Add(keyValuePair9.Key, keyValuePair9.Value);
						}
					}
				}
				return gameObjectBlueprint;
			}
			catch (Exception ex)
			{
				MetricsManager.LogError("Error while loading baked blueprint " + node.Name, ex);
			}
			return null;
		}

		// Token: 0x060061A8 RID: 25000 RVA: 0x001DECC0 File Offset: 0x001DCEC0
		public GameObject CreateObject(string ObjectBlueprint)
		{
			return this.CreateObject(ObjectBlueprint, 0, 0, null, null, null, null, null);
		}

		// Token: 0x060061A9 RID: 25001 RVA: 0x001DECDC File Offset: 0x001DCEDC
		public GameObject CreateObject(string ObjectBlueprint, int BonusModChance = 0, int SetModNumber = 0, string AutoMod = null, Action<GameObject> BeforeObjectCreated = null, Action<GameObject> AfterObjectCreated = null, string Context = null, List<GameObject> ProvideInventory = null)
		{
			GameObject gameObject;
			try
			{
				if (ObjectBlueprint == null)
				{
					gameObject = null;
				}
				else
				{
					this.ObjectsCreated += 1L;
					GameObjectBlueprint blueprint = this.GetBlueprint(ObjectBlueprint);
					gameObject = this.CreateObject(blueprint, BonusModChance, SetModNumber, AutoMod, BeforeObjectCreated, AfterObjectCreated, Context, ProvideInventory);
				}
			}
			catch (Exception ex)
			{
				MetricsManager.LogException("Failed creating:" + ObjectBlueprint, ex, "game_exception");
				GameObject gameObject2 = this.CreateObject("PhysicalObject");
				gameObject2.Render.DisplayName = "[invalid blueprint:" + ObjectBlueprint + "]";
				gameObject2.GetPart<Description>().Short = "Failed building: " + ObjectBlueprint + "\n\n" + ex.ToString();
				gameObject = gameObject2;
			}
			return gameObject;
		}

		// Token: 0x060061AA RID: 25002 RVA: 0x001DED94 File Offset: 0x001DCF94
		public GameObject CreateObject(string ObjectBlueprint, Action<GameObject> BeforeObjectCreated)
		{
			GameObject gameObject;
			try
			{
				if (ObjectBlueprint == null)
				{
					gameObject = null;
				}
				else
				{
					this.ObjectsCreated += 1L;
					GameObjectBlueprint blueprint = this.GetBlueprint(ObjectBlueprint);
					gameObject = this.CreateObject(blueprint, 0, 0, null, BeforeObjectCreated, null, null, null);
				}
			}
			catch (Exception ex)
			{
				MetricsManager.LogException("Failed creating:" + ObjectBlueprint, ex, "game_exception");
				GameObject gameObject2 = this.CreateObject("PhysicalObject");
				gameObject2.Render.DisplayName = "[invalid blueprint:" + ObjectBlueprint + "]";
				gameObject2.GetPart<Description>().Short = "Failed building: " + ObjectBlueprint + "\n\n" + ex.ToString();
				gameObject = gameObject2;
			}
			return gameObject;
		}

		// Token: 0x060061AB RID: 25003 RVA: 0x001DEE48 File Offset: 0x001DD048
		private void CallLoadBlueprint()
		{
			int num = 0;
			object[] array = new object[1];
			foreach (KeyValuePair<Type, ValueTuple<MethodInfo, List<GameObjectBlueprint>>> keyValuePair in this.GetBlueprintsWantingPreload())
			{
				Type type;
				ValueTuple<MethodInfo, List<GameObjectBlueprint>> valueTuple;
				keyValuePair.Deconstruct(out type, out valueTuple);
				ValueTuple<MethodInfo, List<GameObjectBlueprint>> valueTuple2 = valueTuple;
				Type type2 = type;
				MethodInfo item = valueTuple2.Item1;
				foreach (GameObjectBlueprint gameObjectBlueprint in valueTuple2.Item2)
				{
					num++;
					if (num % 20 == 0)
					{
						Event.ResetPool(true);
					}
					try
					{
						array[0] = gameObjectBlueprint;
						item.Invoke(null, array);
					}
					catch (Exception ex)
					{
						MetricsManager.LogError(string.Concat(new string[] { "Exception invoking '", type2.Name, ".", item.Name, "' with blueprint '", gameObjectBlueprint.Name, "'" }), ex);
					}
				}
			}
		}

		// Token: 0x060061AC RID: 25004 RVA: 0x001DEF7C File Offset: 0x001DD17C
		private static void ProcessAsInventory(GameObject Object, InventoryObject InventoryObject, Inventory TargetInventory)
		{
			if (InventoryObject != null)
			{
				if (InventoryObject.NoEquip)
				{
					Object.SetIntProperty("NoEquip", 1, false);
				}
				if (InventoryObject.NoSell)
				{
					Object.SetIntProperty("WontSell", 1, false);
				}
				if (InventoryObject.NotReal)
				{
					Object.Physics.IsReal = false;
				}
				if (InventoryObject.Full)
				{
					LiquidVolume liquidVolume = Object.LiquidVolume;
					if (liquidVolume != null && liquidVolume.MaxVolume > 0 && liquidVolume.ComponentLiquids.Count > 0)
					{
						liquidVolume.Volume = liquidVolume.MaxVolume;
						liquidVolume.FlushWeightCaches();
					}
				}
			}
			if (TargetInventory != null)
			{
				if (TargetInventory.ParentObject == Object)
				{
					MetricsManager.LogError("target inventory for " + Object.DebugName + " was self");
					return;
				}
				GameObjectFactory.eCommandTakeObject.SetParameter("Object", Object);
				TargetInventory.FireEvent(GameObjectFactory.eCommandTakeObject);
			}
		}

		// Token: 0x060061AD RID: 25005 RVA: 0x001DF04C File Offset: 0x001DD24C
		public static void ProcessSpecification(string Blueprint, Action<GameObject> AfterObjectCreated = null, InventoryObject InventoryObject = null, int Count = 1, int BonusModChance = 0, int SetModNumber = 0, string AutoMod = null, string Context = null, Action<GameObject> BeforeObjectCreated = null, GameObject OwningObject = null, Inventory TargetInventory = null, List<GameObject> ProvideInventory = null)
		{
			try
			{
				if (TargetInventory == null && OwningObject != null)
				{
					TargetInventory = OwningObject.Inventory;
				}
				if (Blueprint.Length > 0 && Blueprint.StartsWith("$CALLBLUEPRINTMETHOD:", StringComparison.CurrentCultureIgnoreCase))
				{
					GameObjectFactory.ProcessSpecification(PopulationManager.resolveCallBlueprintSlug(Blueprint), AfterObjectCreated, InventoryObject, Count, BonusModChance, SetModNumber, AutoMod, Context, BeforeObjectCreated, OwningObject, TargetInventory, ProvideInventory);
				}
				else if (Blueprint.Length > 0 && Blueprint.StartsWith("$CALLOBJECTMETHOD:", StringComparison.CurrentCultureIgnoreCase))
				{
					for (int i = 0; i < Count; i++)
					{
						foreach (GameObject gameObject in PopulationManager.resolveCallObjectSlug(Blueprint))
						{
							if (AfterObjectCreated != null)
							{
								AfterObjectCreated(gameObject);
							}
							GameObjectFactory.ProcessAsInventory(gameObject, InventoryObject, TargetInventory);
						}
					}
				}
				else if (Blueprint.Length > 0 && Blueprint[0] == '#')
				{
					List<string> list = new List<string>(Blueprint.Substring(1).Split(',', StringSplitOptions.None));
					for (int j = 0; j < Count; j++)
					{
						if (list.Count <= 0)
						{
							break;
						}
						string randomElement = list.GetRandomElement<string>(null);
						list.Remove(randomElement);
						GameObjectFactory.ProcessSpecification(randomElement, AfterObjectCreated, InventoryObject, 1, BonusModChance, SetModNumber, AutoMod, Context, BeforeObjectCreated, OwningObject, TargetInventory, ProvideInventory);
					}
				}
				else if (Blueprint.Length > 0 && (Blueprint[0] == '@' || Blueprint[0] == '*'))
				{
					string text = Blueprint.Substring(1);
					if (text.Contains("{zonetier}"))
					{
						text = text.Replace("{zonetier}", ZoneManager.zoneGenerationContextTier.ToString());
					}
					GameObjectFactory.populationContext.Clear();
					GameObjectFactory.populationContext.Add("zonetier", ZoneManager.zoneGenerationContextTier.ToString());
					GameObjectFactory.populationContext.Add("zonetier+1", (ZoneManager.zoneGenerationContextTier + 1).ToString());
					if (OwningObject != null)
					{
						GameObjectFactory.populationContext.Add("ownertier", OwningObject.GetTier().ToString());
						GameObjectFactory.populationContext.Add("ownertechtier", OwningObject.GetTechTier().ToString());
					}
					for (int k = 0; k < Count; k++)
					{
						foreach (PopulationResult populationResult in PopulationManager.Generate(text, GameObjectFactory.populationContext, null))
						{
							for (int l = 0; l < populationResult.Number; l++)
							{
								InventoryObject inventoryObject = InventoryObject;
								GameObject gameObject2 = null;
								if (ProvideInventory != null)
								{
									foreach (GameObject gameObject3 in ProvideInventory)
									{
										if (gameObject3.Blueprint == populationResult.Blueprint)
										{
											gameObject2 = gameObject3;
											break;
										}
									}
									if (gameObject2 != null)
									{
										inventoryObject = null;
										if (gameObject2.Count == 1)
										{
											ProvideInventory.Remove(gameObject2);
										}
										else
										{
											gameObject2 = gameObject2.RemoveOne();
										}
									}
								}
								if (gameObject2 == null)
								{
									gameObject2 = GameObjectFactory.Factory.CreateObject(populationResult.Blueprint, BonusModChance, SetModNumber, AutoMod, BeforeObjectCreated, null, Context, null);
								}
								if (l == 0 && populationResult.Number > 1 && populationResult.Hint != "NoStack" && gameObject2.CanGenerateStacked())
								{
									Stacker stacker = gameObject2.Stacker;
									if (stacker != null && stacker != null && stacker.StackCount == 1)
									{
										stacker.StackCount = populationResult.Number;
										l = populationResult.Number;
									}
								}
								if (AfterObjectCreated != null)
								{
									AfterObjectCreated(gameObject2);
								}
								GameObjectFactory.ProcessAsInventory(gameObject2, inventoryObject, TargetInventory);
							}
						}
					}
				}
				else if (ProvideInventory != null)
				{
					for (int m = 0; m < Count; m++)
					{
						InventoryObject inventoryObject2 = InventoryObject;
						GameObject gameObject4 = null;
						foreach (GameObject gameObject5 in ProvideInventory)
						{
							if (gameObject5.Blueprint == Blueprint)
							{
								gameObject4 = gameObject5;
								break;
							}
						}
						if (gameObject4 != null)
						{
							inventoryObject2 = null;
							if (gameObject4.Count == 1)
							{
								ProvideInventory.Remove(gameObject4);
							}
							else
							{
								gameObject4 = gameObject4.RemoveOne();
							}
						}
						if (gameObject4 == null)
						{
							gameObject4 = GameObjectFactory.Factory.CreateObject(Blueprint, BonusModChance, 0, null, BeforeObjectCreated, null, Context, null);
							if (m == 0 && Count > 1 && gameObject4.CanGenerateStacked())
							{
								Stacker stacker2 = gameObject4.Stacker;
								if (stacker2 != null && stacker2.StackCount == 1)
								{
									stacker2.StackCount = Count;
									m = Count;
								}
							}
						}
						if (AfterObjectCreated != null)
						{
							AfterObjectCreated(gameObject4);
						}
						GameObjectFactory.ProcessAsInventory(gameObject4, inventoryObject2, TargetInventory);
					}
				}
				else
				{
					for (int n = 0; n < Count; n++)
					{
						GameObject gameObject6 = GameObjectFactory.Factory.CreateObject(Blueprint, BonusModChance, 0, null, BeforeObjectCreated, null, Context, null);
						Stacker stacker3;
						if (n == 0 && Count > 1 && gameObject6.CanGenerateStacked() && gameObject6.TryGetPart<Stacker>(out stacker3) && stacker3.StackCount == 1)
						{
							stacker3.StackCount = Count;
							n = Count;
						}
						if (AfterObjectCreated != null)
						{
							AfterObjectCreated(gameObject6);
						}
						GameObjectFactory.ProcessAsInventory(gameObject6, InventoryObject, TargetInventory);
					}
				}
			}
			catch (Exception ex)
			{
				MetricsManager.LogException("GameObjectFactory::ProcessSpecification", ex, "game_exception");
			}
		}

		// Token: 0x060061AE RID: 25006 RVA: 0x001DF5DC File Offset: 0x001DD7DC
		public GameObject CreateObject(GameObjectBlueprint Blueprint, int BonusModChance = 0, int SetModNumber = 0, string AutoMod = null, Action<GameObject> BeforeObjectCreated = null, Action<GameObject> AfterObjectCreated = null, string Context = null, List<GameObject> ProvideInventory = null)
		{
			if (Blueprint == null)
			{
				return null;
			}
			GameObject gameObject = GameObject.Get();
			gameObject.SetBlueprint(Blueprint);
			string text;
			GameObjectBlueprint gameObjectBlueprint;
			if (Blueprint.Tags.TryGetValue("CreateSubstituteBlueprint", out text) && this.Blueprints.TryGetValue(text, out gameObjectBlueprint))
			{
				gameObject.SetBlueprint(gameObjectBlueprint);
			}
			bool flag = Context == "Initialization";
			if (flag)
			{
				gameObject.ID = "init:" + Blueprint.Name;
			}
			gameObject.Property.Rebase<string, string>(Blueprint.Props);
			gameObject.IntProperty.Rebase<string, int>(Blueprint.IntProps);
			for (GameObjectBlueprint gameObjectBlueprint2 = Blueprint; gameObjectBlueprint2 != null; gameObjectBlueprint2 = gameObjectBlueprint2.ShallowParent)
			{
				if (gameObjectBlueprint2.Stats == null)
				{
					XRLCore.LogError("Null stats on blueprint for " + Blueprint.Name);
				}
				else if (gameObject.Statistics == null)
				{
					XRLCore.LogError("Null statistics on new object for " + Blueprint.Name);
				}
				else
				{
					foreach (KeyValuePair<string, Statistic> keyValuePair in gameObjectBlueprint2.Stats)
					{
						if (!gameObject.Statistics.ContainsKey(keyValuePair.Key))
						{
							gameObject.Statistics.Add(keyValuePair.Key, new Statistic(keyValuePair.Value)
							{
								Owner = gameObject
							});
						}
					}
				}
			}
			gameObject.FinalizeStats();
			foreach (GamePartBlueprint gamePartBlueprint in Blueprint.allparts.Values)
			{
				if (Stat.Random(1, gamePartBlueprint.ChanceOneIn) == 1)
				{
					if (gamePartBlueprint.T == null)
					{
						XRLCore.LogError("Unknown part " + gamePartBlueprint.Name + "!");
						return null;
					}
					GamePartBlueprint.PartReflectionCache reflector = gamePartBlueprint.Reflector;
					IPart part = ((reflector != null) ? reflector.GetInstance() : null) ?? (Activator.CreateInstance(gamePartBlueprint.T) as IPart);
					part.ParentObject = gameObject;
					gamePartBlueprint.InitializePartInstance(part);
					gameObject.AddPart(part, !flag, true);
					string text2;
					if (gamePartBlueprint.TryGetParameter<string>("Builder", out text2))
					{
						(Activator.CreateInstance(ModManager.ResolveType("XRL.World.PartBuilders", text2, false, false, true)) as IPartBuilder).BuildPart(part, Context);
					}
				}
			}
			if (Blueprint.Mutations != null)
			{
				foreach (GamePartBlueprint gamePartBlueprint2 in Blueprint.Mutations.Values)
				{
					if (Stat.Random(1, gamePartBlueprint2.ChanceOneIn) == 1)
					{
						string text3 = "XRL.World.Parts.Mutation." + gamePartBlueprint2.Name;
						Type type = ModManager.ResolveType(text3, false, false, true);
						if (type == null)
						{
							MetricsManager.LogError("Unknown mutation " + text3);
							return null;
						}
						GamePartBlueprint.PartReflectionCache reflector2 = gamePartBlueprint2.Reflector;
						BaseMutation baseMutation = (((reflector2 != null) ? reflector2.GetNewInstance() : null) ?? Activator.CreateInstance(type)) as BaseMutation;
						if (baseMutation == null)
						{
							MetricsManager.LogError("Mutation " + text3 + " is not a BaseMutation");
						}
						else
						{
							gamePartBlueprint2.InitializePartInstance(baseMutation);
							string text4;
							if (gamePartBlueprint2.TryGetParameter<string>("Builder", out text4))
							{
								(Activator.CreateInstance(ModManager.ResolveType("XRL.World.PartBuilders." + text4, false, false, true)) as IPartBuilder).BuildPart(baseMutation, Context);
							}
							if (baseMutation.CapOverride == -1)
							{
								baseMutation.CapOverride = baseMutation.Level;
							}
							gameObject.RequirePart<Mutations>(false).AddMutation(baseMutation, baseMutation.Level, true);
						}
					}
				}
			}
			if (Blueprint.Tags != null)
			{
				string text5;
				if (Blueprint.Tags.TryGetValue("MutationPopulation", out text5) && !text5.IsNullOrEmpty())
				{
					gameObject.MutateFromPopulationTable(text5, ZoneManager.zoneGenerationContextTier);
				}
				string text6;
				if (Blueprint.Tags.TryGetValue("Modded", out text6))
				{
					if (!text6.IsNullOrEmpty())
					{
						int num = Convert.ToInt32(text6);
						if (SetModNumber < num)
						{
							SetModNumber = num;
						}
					}
					else if (SetModNumber < 1)
					{
						SetModNumber = 1;
					}
				}
			}
			if (Blueprint.Skills != null)
			{
				foreach (GamePartBlueprint gamePartBlueprint3 in Blueprint.Skills.Values)
				{
					if (Stat.Random(1, gamePartBlueprint3.ChanceOneIn) == 1)
					{
						string text7 = "XRL.World.Parts.Skill." + gamePartBlueprint3.Name;
						Type type2 = ModManager.ResolveType(text7, false, false, true);
						if (type2 == null)
						{
							MetricsManager.LogError("Unknown skill " + text7);
							return null;
						}
						GamePartBlueprint.PartReflectionCache reflector3 = gamePartBlueprint3.Reflector;
						BaseSkill baseSkill = (((reflector3 != null) ? reflector3.GetNewInstance() : null) ?? Activator.CreateInstance(type2)) as BaseSkill;
						if (baseSkill == null)
						{
							MetricsManager.LogError("Skill " + text7 + " is not a BaseSkill");
							return null;
						}
						gamePartBlueprint3.InitializePartInstance(baseSkill);
						string text8;
						if (gamePartBlueprint3.TryGetParameter<string>("Builder", out text8))
						{
							(Activator.CreateInstance(ModManager.ResolveType("XRL.World.PartBuilders." + text8, false, false, true)) as IPartBuilder).BuildPart(baseSkill, Context);
						}
						gameObject.RequirePart<Skills>(false).AddSkill(baseSkill, null, null);
					}
				}
			}
			if (Blueprint.Inventory != null)
			{
				using (List<InventoryObject>.Enumerator enumerator3 = Blueprint.Inventory.GetEnumerator())
				{
					while (enumerator3.MoveNext())
					{
						InventoryObject InventoryObject = enumerator3.Current;
						if (InventoryObject.Chance.in100())
						{
							int num2 = (InventoryObject.BoostModChance ? 30 : 0);
							Action<GameObject> action = null;
							if (InventoryObject.NeedsToPreconfigureObject())
							{
								action = delegate(GameObject GO)
								{
									InventoryObject.PreconfigureObject(GO);
								};
							}
							int num3 = InventoryObject.Number.RollCached();
							GameObjectFactory.ProcessSpecification(InventoryObject.Blueprint, null, InventoryObject, num3, num2, InventoryObject.SetMods, InventoryObject.AutoMod, Context, action, gameObject, gameObject.Inventory, ProvideInventory);
						}
					}
				}
			}
			string tag = gameObject.GetTag("InventoryPopulationTable", null);
			if (tag != null)
			{
				gameObject.EquipFromPopulationTable(tag, ZoneManager.zoneGenerationContextTier, null, Context, false, true);
				gameObject.FireEvent("InventoryPopulated");
			}
			GameObjectFactory.ApplyBuilders(gameObject, Blueprint, Context);
			if (!AutoMod.IsNullOrEmpty())
			{
				if (AutoMod.Contains(","))
				{
					using (List<string>.Enumerator enumerator4 = AutoMod.CachedCommaExpansion().GetEnumerator())
					{
						while (enumerator4.MoveNext())
						{
							string text9 = enumerator4.Current;
							gameObject.ApplyModification(text9, true, null, false);
						}
						goto IL_06C8;
					}
				}
				gameObject.ApplyModification(AutoMod, true, null, false);
			}
			IL_06C8:
			ModificationFactory.ApplyModifications(gameObject, Blueprint, BonusModChance, SetModNumber, Context);
			if (BeforeObjectCreated != null)
			{
				BeforeObjectCreated(gameObject);
			}
			GameObject gameObject2 = null;
			BeforeObjectCreatedEvent.Process(gameObject, Context, ref gameObject2);
			ObjectCreatedEvent.Process(gameObject, Context, ref gameObject2);
			AfterObjectCreatedEvent.Process(gameObject, Context, ref gameObject2);
			if (gameObject2 != null)
			{
				gameObject = gameObject2;
			}
			if (AfterObjectCreated != null)
			{
				AfterObjectCreated(gameObject);
			}
			return gameObject;
		}

		// Token: 0x060061AF RID: 25007 RVA: 0x001DFD98 File Offset: 0x001DDF98
		public GameObject CreateSampleObject(GameObjectBlueprint Blueprint, Action<GameObject> BeforeObjectCreated = null)
		{
			return this.CreateObject(Blueprint, -9999, 0, null, BeforeObjectCreated, null, "Sample", null);
		}

		// Token: 0x060061B0 RID: 25008 RVA: 0x001DFDBC File Offset: 0x001DDFBC
		public GameObject CreateSampleObject(string Blueprint, Action<GameObject> BeforeObjectCreated = null)
		{
			return this.CreateObject(Blueprint, -9999, 0, null, BeforeObjectCreated, null, "Sample", null);
		}

		// Token: 0x060061B1 RID: 25009 RVA: 0x001DFDE0 File Offset: 0x001DDFE0
		public GameObject CreateUnmodifiedObject(GameObjectBlueprint Blueprint, string Context = null, Action<GameObject> BeforeObjectCreated = null)
		{
			return this.CreateObject(Blueprint, -9999, 0, null, BeforeObjectCreated, null, Context, null);
		}

		// Token: 0x060061B2 RID: 25010 RVA: 0x001DFE04 File Offset: 0x001DE004
		public GameObject CreateUnmodifiedObject(string Blueprint, string Context = null, Action<GameObject> BeforeObjectCreated = null)
		{
			return this.CreateObject(Blueprint, -9999, 0, null, BeforeObjectCreated, null, Context, null);
		}

		// Token: 0x060061B3 RID: 25011 RVA: 0x001DFE25 File Offset: 0x001DE025
		public static void ApplyBuilder(GameObject NewObject, string Builder)
		{
			GameObjectFactory.ApplyBuilder(NewObject, new GamePartBlueprint(Builder), null);
		}

		// Token: 0x060061B4 RID: 25012 RVA: 0x001DFE34 File Offset: 0x001DE034
		public static void ApplyBuilder(GameObject NewObject, GamePartBlueprint Builder, string Context = null)
		{
			Type t = Builder.T;
			if (t == null)
			{
				MetricsManager.LogError("Unknown builder " + Builder.Name);
				return;
			}
			IObjectBuilder objectBuilder;
			if (!GameObjectFactory.ObjectBuilders.TryGetValue(t, out objectBuilder))
			{
				objectBuilder = Activator.CreateInstance(t) as IObjectBuilder;
				if (objectBuilder == null)
				{
					MetricsManager.LogError("Builder '" + Builder.Name + "' is not an instance of IObjectBuilder");
					return;
				}
				GameObjectFactory.ObjectBuilders[t] = objectBuilder;
			}
			objectBuilder.Initialize();
			Builder.InitializePartInstance(objectBuilder);
			objectBuilder.Apply(NewObject, Context);
		}

		// Token: 0x060061B5 RID: 25013 RVA: 0x001DFEC4 File Offset: 0x001DE0C4
		public static void ApplyBuilders(GameObject NewObject, GameObjectBlueprint Blueprint, string Context = null)
		{
			if (Blueprint.Builders.IsNullOrEmpty<KeyValuePair<string, GamePartBlueprint>>())
			{
				return;
			}
			foreach (KeyValuePair<string, GamePartBlueprint> keyValuePair in Blueprint.Builders)
			{
				string text;
				GamePartBlueprint gamePartBlueprint;
				keyValuePair.Deconstruct(out text, out gamePartBlueprint);
				GamePartBlueprint gamePartBlueprint2 = gamePartBlueprint;
				if (Stat.Random(1, gamePartBlueprint2.ChanceOneIn) == 1)
				{
					GameObjectFactory.ApplyBuilder(NewObject, gamePartBlueprint2, Context);
				}
			}
		}

		// Token: 0x060061B6 RID: 25014 RVA: 0x001DFF44 File Offset: 0x001DE144
		public GameObjectBlueprint GetBlueprint(string Name)
		{
			if (Name == null)
			{
				MetricsManager.LogError("called with null Name");
				return null;
			}
			GameObjectBlueprint gameObjectBlueprint;
			if (this.Blueprints.TryGetValue(Name, out gameObjectBlueprint))
			{
				return gameObjectBlueprint;
			}
			MetricsManager.LogError("Unknown blueprint (tell support@freeholdentertainment.com the following text): " + Name);
			return null;
		}

		// Token: 0x060061B7 RID: 25015 RVA: 0x001DFF84 File Offset: 0x001DE184
		public GameObjectBlueprint GetBlueprintIfExists(string Name)
		{
			GameObjectBlueprint gameObjectBlueprint;
			if (Name == null || !this.Blueprints.TryGetValue(Name, out gameObjectBlueprint))
			{
				return null;
			}
			return gameObjectBlueprint;
		}

		// Token: 0x060061B8 RID: 25016 RVA: 0x001DFFA7 File Offset: 0x001DE1A7
		public bool HasBlueprint(string Name)
		{
			return this.Blueprints.ContainsKey(Name);
		}

		/// <summary>
		/// Not intended for actual use by game objects, but makes finding blueprints via wish easier
		/// </summary>
		// Token: 0x060061B9 RID: 25017 RVA: 0x001DFFB8 File Offset: 0x001DE1B8
		public GameObjectBlueprint GetBlueprintIgnoringCase(string name)
		{
			if (name == null)
			{
				return null;
			}
			foreach (string text in this.Blueprints.Keys)
			{
				if (string.Compare(text, name, true) == 0)
				{
					return this.Blueprints[text];
				}
			}
			return null;
		}

		// Token: 0x060061BA RID: 25018 RVA: 0x001E002C File Offset: 0x001DE22C
		[WishCommand(null, null, Command = "bpxml")]
		public static void HandleBlueprintXML(string bpname)
		{
			GameObjectBlueprint blueprintIgnoringCase = GameObjectFactory.Factory.GetBlueprintIgnoringCase(bpname);
			if (blueprintIgnoringCase == null)
			{
				Popup.Show("No blueprint named \"" + bpname + "\" found.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
				return;
			}
			Popup.Show(blueprintIgnoringCase.BlueprintXML().Replace("&", "&&").Replace("^", "^^"), null, "Sounds/UI/ui_notification", true, true, true, true, null);
		}

		// Token: 0x060061BB RID: 25019 RVA: 0x001E00A0 File Offset: 0x001DE2A0
		[WishCommand(null, null, Command = "partpoolcount")]
		public static void PartPoolWish()
		{
			List<ValueTuple<Type, IPartPool>> list = new List<ValueTuple<Type, IPartPool>>();
			foreach (KeyValuePair<Type, GamePartBlueprint.PartReflectionCache> keyValuePair in GamePartBlueprint.PartReflectionCache.CacheByType)
			{
				if (keyValuePair.Value.Pool != null)
				{
					list.Add(new ValueTuple<Type, IPartPool>(keyValuePair.Key, keyValuePair.Value.Pool));
				}
			}
			list.Sort((ValueTuple<Type, IPartPool> a, ValueTuple<Type, IPartPool> b) => b.Item2.Capacity.CompareTo(a.Item2.Capacity));
			StringBuilder stringBuilder = Event.NewStringBuilder(null);
			foreach (ValueTuple<Type, IPartPool> valueTuple in list)
			{
				stringBuilder.Compound(valueTuple.Item1.Name, '\n').Append(": ").Append(valueTuple.Item2.Count)
					.Append('/')
					.Append(valueTuple.Item2.Capacity);
			}
			Popup.Show(Event.FinalizeString(stringBuilder), null, "Sounds/UI/ui_notification", true, true, true, true, null);
		}

		// Token: 0x04002DF2 RID: 11762
		[ModSensitiveStaticCache(false)]
		private static GameObjectFactory _Factory = null;

		// Token: 0x04002DF3 RID: 11763
		private bool LoadBlueprintsHappened;

		/// <remarks>
		/// The same blueprint can exist under multiple keys in Blueprints for save compatibility purposes,
		/// iterate BlueprintList for distinct blueprints.
		/// </remarks>
		// Token: 0x04002DF4 RID: 11764
		public Dictionary<string, GameObjectBlueprint> Blueprints = new Dictionary<string, GameObjectBlueprint>();

		// Token: 0x04002DF5 RID: 11765
		public List<GameObjectBlueprint> BlueprintList = new List<GameObjectBlueprint>();

		// Token: 0x04002DF6 RID: 11766
		public static Queue<GameObject> ObjectPool = new Queue<GameObject>();

		// Token: 0x04002DF7 RID: 11767
		private Dictionary<int, List<GameObjectBlueprint>> Tiers = new Dictionary<int, List<GameObjectBlueprint>>();

		// Token: 0x04002DF8 RID: 11768
		private Dictionary<int, uint> TierDeltaWeights;

		// Token: 0x04002DF9 RID: 11769
		private Dictionary<int, uint> TechTierDeltaWeights;

		// Token: 0x04002DFA RID: 11770
		private Dictionary<string, double> RoleWeightMultipliers;

		// Token: 0x04002DFB RID: 11771
		public long ObjectsCreated;

		// Token: 0x04002DFC RID: 11772
		[NonSerialized]
		private static Event eObjectCreated = new Event("ObjectCreated", "Context", null);

		// Token: 0x04002DFD RID: 11773
		[NonSerialized]
		private static Event eCommandTakeObject = new Event("CommandTakeObject", "Object", null, "EnergyCost", 0).SetSilent(true);

		// Token: 0x04002DFE RID: 11774
		[NonSerialized]
		public static Dictionary<string, string> populationContext = new Dictionary<string, string>();

		// Token: 0x04002DFF RID: 11775
		private static Dictionary<Type, IObjectBuilder> ObjectBuilders = new Dictionary<Type, IObjectBuilder>();
	}
}
