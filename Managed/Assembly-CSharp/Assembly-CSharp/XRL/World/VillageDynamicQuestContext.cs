using System;
using System.Collections.Generic;
using System.Linq;
using Genkit;
using HistoryKit;
using Qud.API;
using XRL.Core;
using XRL.Language;
using XRL.Rules;
using XRL.World.Parts;
using XRL.World.WorldBuilders;

namespace XRL.World
{
	// Token: 0x0200092E RID: 2350
	[Serializable]
	public class VillageDynamicQuestContext : DynamicQuestContext
	{
		// Token: 0x17000728 RID: 1832
		// (get) Token: 0x06005A32 RID: 23090 RVA: 0x001B539F File Offset: 0x001B359F
		// (set) Token: 0x06005A33 RID: 23091 RVA: 0x001B53D7 File Offset: 0x001B35D7
		public HistoricEntity VillageEntity
		{
			get
			{
				if (this._VillageEntity == null && !this.VillageEntityID.IsNullOrEmpty())
				{
					this._VillageEntity = The.Game.sultanHistory.GetEntity(this.VillageEntityID);
				}
				return this._VillageEntity;
			}
			set
			{
				this._VillageEntity = value;
				this.VillageEntityID = ((value != null) ? value.id : null);
			}
		}

		// Token: 0x17000729 RID: 1833
		// (get) Token: 0x06005A34 RID: 23092 RVA: 0x001B53F2 File Offset: 0x001B35F2
		// (set) Token: 0x06005A35 RID: 23093 RVA: 0x001B5427 File Offset: 0x001B3627
		public HistoricEntitySnapshot villageSnapshot
		{
			get
			{
				if (this._VillageSnapshot == null && !this.VillageEntityID.IsNullOrEmpty())
				{
					HistoricEntity villageEntity = this.VillageEntity;
					this._VillageSnapshot = ((villageEntity != null) ? villageEntity.GetCurrentSnapshot() : null);
				}
				return this._VillageSnapshot;
			}
			set
			{
				this._VillageSnapshot = value;
				this.VillageEntity = ((value != null) ? value.entity : null);
			}
		}

		// Token: 0x1700072A RID: 1834
		// (get) Token: 0x06005A36 RID: 23094 RVA: 0x001B5442 File Offset: 0x001B3642
		public WorldInfo worldInfo
		{
			get
			{
				return The.Game.GetObjectGameState("JoppaWorldInfo") as WorldInfo;
			}
		}

		// Token: 0x06005A37 RID: 23095 RVA: 0x001B5458 File Offset: 0x001B3658
		public VillageDynamicQuestContext()
		{
		}

		// Token: 0x06005A38 RID: 23096 RVA: 0x001B5460 File Offset: 0x001B3660
		public VillageDynamicQuestContext(HistoricEntity VillageEntity)
		{
			this.VillageEntity = VillageEntity;
			this.isVillageZero = VillageEntity.HasEntityProperty("isVillageZero", -1L);
		}

		// Token: 0x06005A39 RID: 23097 RVA: 0x001B5482 File Offset: 0x001B3682
		public VillageDynamicQuestContext(HistoricEntitySnapshot villageSnapshot)
		{
			this.villageSnapshot = villageSnapshot;
			this.isVillageZero = villageSnapshot.hasProperty("isVillageZero");
		}

		// Token: 0x06005A3A RID: 23098 RVA: 0x001B54A2 File Offset: 0x001B36A2
		public override void Write(SerializationWriter Writer)
		{
			base.Write(Writer);
			Writer.Write(this.usedLocations);
		}

		// Token: 0x06005A3B RID: 23099 RVA: 0x001B54B7 File Offset: 0x001B36B7
		public override void Read(SerializationReader Reader)
		{
			base.Read(Reader);
			this.usedLocations = Reader.ReadLocation2DList();
		}

		// Token: 0x06005A3C RID: 23100 RVA: 0x001B54CC File Offset: 0x001B36CC
		public override HistoricEntity originEntity()
		{
			return null;
		}

		// Token: 0x06005A3D RID: 23101 RVA: 0x001B54D0 File Offset: 0x001B36D0
		public override DynamicQuestReward getQuestReward()
		{
			if (this.isVillageZero)
			{
				if (this.questNumber == 0)
				{
					DynamicQuestReward dynamicQuestReward = new DynamicQuestReward();
					dynamicQuestReward.StepXP = 750;
					GameObject gameObject = GameObject.Create("Blank Recoiler");
					gameObject.MakeUnderstood(false);
					gameObject.Render.DisplayName = this.villageSnapshot.GetProperty("name", "unknown") + " recoiler";
					Teleporter part = gameObject.GetPart<Teleporter>();
					part.DestinationZone = this.villageSnapshot.GetProperty("zoneID", "unknown");
					part.DestinationX = -1;
					part.DestinationY = -1;
					dynamicQuestReward.rewards.Add(new DynamicQuestRewardElement_GameObject(gameObject));
					dynamicQuestReward.rewards.Add(new DynamicQuestRewardElement_VillageZeroMainQuestHook());
					dynamicQuestReward.postrewards.Add(new DynamicQuestRewardElement_Reputation("villagers of " + this.villageSnapshot.GetProperty("name", "unknown"), 50));
					return dynamicQuestReward;
				}
				if (this.questNumber == 1)
				{
					return new DynamicQuestReward
					{
						StepXP = 1000,
						rewards = 
						{
							new DynamicQuestRewardElement_ChoiceFromPopulation("VillageZero_Reward", 3),
							new DynamicQuestRewardElement_VillageZeroLoot()
						},
						postrewards = 
						{
							new DynamicQuestRewardElement_Reputation("villagers of " + this.villageSnapshot.GetProperty("name", "unknown"), 100)
						}
					};
				}
			}
			this.tier = this.villageSnapshot.Tier;
			return new DynamicQuestReward
			{
				StepXP = this.tier * 1000,
				rewards = 
				{
					new DynamicQuestRewardElement_Reputation("villagers of " + this.villageSnapshot.GetProperty("name", "unknown"), 100),
					new DynamicQuestRewardElement_ChoiceFromPopulation("VillageTier" + this.tier.ToString() + "_QuestReward", 3)
				}
			};
		}

		// Token: 0x06005A3E RID: 23102 RVA: 0x001B56B8 File Offset: 0x001B38B8
		private void findQuestTargetLocationsInRange(List<GeneratedLocationInfo> list, int minRange, int maxRange, Location2D rangeFrom)
		{
			this.worldInfo.ForeachGeneratedLocation(delegate(GeneratedLocationInfo l)
			{
				if (l.distanceTo(rangeFrom) >= minRange && l.distanceTo(rangeFrom) <= maxRange && l.isUndiscovered() && (this.usedLocations == null || !this.usedLocations.Contains(l.zoneLocation)) && l.name != "some forgotten ruins")
				{
					list.Add(l);
				}
			});
		}

		// Token: 0x06005A3F RID: 23103 RVA: 0x001B5708 File Offset: 0x001B3908
		public override GeneratedLocationInfo getNearbyUndiscoveredLocation()
		{
			Location2D location2D = Zone.zoneIDTo240x72Location(this.originZoneId());
			List<GeneratedLocationInfo> list = new List<GeneratedLocationInfo>();
			int num = 12;
			int num2 = 18;
			while (list.Count == 0 && num >= 2)
			{
				this.findQuestTargetLocationsInRange(list, num, num2, location2D);
				num--;
				num2++;
			}
			if (list.Count > 0)
			{
				GeneratedLocationInfo randomElement = list.GetRandomElement<GeneratedLocationInfo>(null);
				if (this.usedLocations == null)
				{
					this.usedLocations = new List<Location2D>();
				}
				this.usedLocations.Add(randomElement.zoneLocation);
				return randomElement;
			}
			return null;
		}

		// Token: 0x06005A40 RID: 23104 RVA: 0x001B5788 File Offset: 0x001B3988
		public override string originZoneId()
		{
			return this.villageSnapshot.GetProperty("zoneID", "unknown");
		}

		// Token: 0x06005A41 RID: 23105 RVA: 0x001B579F File Offset: 0x001B399F
		public override string getQuestOriginZone()
		{
			return this.villageSnapshot.GetProperty("name", "unknown");
		}

		// Token: 0x06005A42 RID: 23106 RVA: 0x001B57B6 File Offset: 0x001B39B6
		public override string questTargetZone(int minDistance, int maxDistance)
		{
			return null;
		}

		// Token: 0x06005A43 RID: 23107 RVA: 0x001B57B9 File Offset: 0x001B39B9
		public override string getQuestGiverZone()
		{
			return this.villageSnapshot.GetProperty("zoneID", "unknown");
		}

		// Token: 0x06005A44 RID: 23108 RVA: 0x001B57D0 File Offset: 0x001B39D0
		public bool questGiverFilter(GameObject go)
		{
			return go.HasProperty("ParticipantVillager") && go.HasProperty("NamedVillager") && !go.HasProperty("GivesDynamicQuest");
		}

		// Token: 0x06005A45 RID: 23109 RVA: 0x001B57FC File Offset: 0x001B39FC
		public override Func<GameObject, bool> getQuestGiverFilter()
		{
			return new Func<GameObject, bool>(this.questGiverFilter);
		}

		// Token: 0x06005A46 RID: 23110 RVA: 0x001B580A File Offset: 0x001B3A0A
		public bool questActorFilter(GameObject go)
		{
			return go.HasProperty("NamedVillager");
		}

		// Token: 0x06005A47 RID: 23111 RVA: 0x001B5817 File Offset: 0x001B3A17
		public override Func<GameObject, bool> getQuestActorFilter()
		{
			return new Func<GameObject, bool>(this.questActorFilter);
		}

		// Token: 0x06005A48 RID: 23112 RVA: 0x001B5828 File Offset: 0x001B3A28
		public override List<string> getSacredThings()
		{
			List<string> list = new List<string>(this.villageSnapshot.GetList("sacredThings"));
			if (list.Count > 0)
			{
				return list;
			}
			list.Add(this.villageSnapshot.GetProperty("defaultSacredThing", "unknown"));
			return list;
		}

		// Token: 0x06005A49 RID: 23113 RVA: 0x001B5874 File Offset: 0x001B3A74
		public bool IsValidQuestDestination(Location2D location)
		{
			int num = this.villageSnapshot.Tier;
			if (num < 2 || this.isVillageZero)
			{
				num = 2;
			}
			if (this.usedLocations != null && this.usedLocations.Contains(location))
			{
				return false;
			}
			if (XRLCore.Core.Game.ZoneManager.GetZoneTier("JoppaWorld", location.X / 3, location.Y / 3, 10) > num)
			{
				return false;
			}
			if (this.isVillageZero && XRLCore.Core.Game.ZoneManager.GetZoneTerrain("JoppaWorld", location.X / 3, location.Y / 3).GetBlueprint(true).DescendsFrom("TerrainFlowerfields"))
			{
				return false;
			}
			List<GeneratedLocationInfo> generatedLocationsAt = this.worldInfo.GetGeneratedLocationsAt(location);
			if (generatedLocationsAt != null)
			{
				using (List<GeneratedLocationInfo>.Enumerator enumerator = generatedLocationsAt.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (enumerator.Current.name == "some forgotten ruins")
						{
							return false;
						}
					}
				}
				return true;
			}
			return true;
		}

		// Token: 0x06005A4A RID: 23114 RVA: 0x001B598C File Offset: 0x001B3B8C
		public override DynamicQuestDeliveryTarget getQuestDeliveryTarget()
		{
			int num = 0;
			int num2 = 0;
			DynamicQuestDeliveryTarget dynamicQuestDeliveryTarget;
			do
			{
				dynamicQuestDeliveryTarget = this.worldInfo.ResolveDeliveryTargetForVillage(this.villageSnapshot, Math.Max(12 - num, 2), 18 + num, num2, new Predicate<Location2D>(this.IsValidQuestDestination));
				num += 3;
				num2++;
			}
			while (dynamicQuestDeliveryTarget == null && num < 100);
			if (dynamicQuestDeliveryTarget != null)
			{
				if (this.usedLocations == null)
				{
					this.usedLocations = new List<Location2D>();
				}
				this.usedLocations.Add(dynamicQuestDeliveryTarget.location);
			}
			return dynamicQuestDeliveryTarget;
		}

		// Token: 0x06005A4B RID: 23115 RVA: 0x001B5A04 File Offset: 0x001B3C04
		public override GameObject getQuestRemoteInteractable()
		{
			List<GameObject> list = new List<GameObject>();
			if (this.villageSnapshot.hasProperty("worships_creature"))
			{
				GameObject cachedObjects = XRLCore.Core.Game.ZoneManager.GetCachedObjects(this.villageSnapshot.GetProperty("worships_creature_id", "unknown"));
				if (cachedObjects != null)
				{
					GameObject gameObject = GameObject.Create(PopulationManager.RollOneFrom("Village_RandomBaseStatue_*Default", null, null).Blueprint);
					gameObject.GetPart<RandomStatue>().SetCreature(cachedObjects.DeepCopy(false, true, null));
					gameObject.SetStringProperty("QuestVerb", "pray at", false);
					gameObject.SetStringProperty("QuestEvent", "Prayed", false);
					gameObject.AddPart<Shrine>(new Shrine(), true, false);
					list.Add(gameObject);
				}
				else
				{
					MetricsManager.LogWarning("Unable to find a creature for cache id: " + this.villageSnapshot.GetProperty("worships_creature_id", "unknown"));
				}
			}
			if (this.villageSnapshot.hasProperty("despises_creature"))
			{
				GameObject cachedObjects2 = XRLCore.Core.Game.ZoneManager.GetCachedObjects(this.villageSnapshot.GetProperty("despises_creature_id", "unknown"));
				if (cachedObjects2 != null)
				{
					GameObject gameObject2 = GameObject.Create(PopulationManager.RollOneFrom("Village_RandomBaseStatue_*Default", null, null).Blueprint);
					gameObject2.GetPart<RandomStatue>().SetCreature(cachedObjects2.DeepCopy(false, true, null));
					if (50.in100())
					{
						gameObject2.AddPart<ModDesecrated>(new ModDesecrated(), true, false);
						gameObject2.SetStringProperty("QuestVerb", "pray at", false);
						gameObject2.SetStringProperty("QuestEvent", "Prayed", false);
					}
					else
					{
						gameObject2.SetStringProperty("QuestVerb", "desecrate", false);
						gameObject2.SetStringProperty("QuestEvent", "Desecrated", false);
					}
					gameObject2.AddPart<Shrine>(new Shrine(), true, false);
					list.Add(gameObject2);
				}
				else
				{
					MetricsManager.LogWarning("Unable to find a creature for cache id: " + this.villageSnapshot.GetProperty("despises_creature_id", "unknown"));
				}
			}
			if (this.villageSnapshot.hasProperty("worships_faction"))
			{
				GameObject gameObject3 = EncountersAPI.GetALegendaryEligibleCreatureFromFaction(this.villageSnapshot.GetProperty("worships_faction", "unknown"), null) ?? EncountersAPI.GetACreatureFromFaction(this.villageSnapshot.GetProperty("worships_faction", "unknown"), null);
				if (gameObject3 != null)
				{
					GameObject gameObject4 = GameObject.Create(PopulationManager.RollOneFrom("Village_RandomBaseStatue_*Default", null, null).Blueprint);
					gameObject4.GetPart<RandomStatue>().SetCreature(gameObject3.DeepCopy(false, true, null));
					gameObject4.SetStringProperty("QuestVerb", "pray at", false);
					gameObject4.SetStringProperty("QuestEvent", "Prayed", false);
					gameObject4.AddPart<Shrine>(new Shrine(), true, false);
					list.Add(gameObject4);
				}
				else
				{
					MetricsManager.LogWarning("Unable to find a creature for faction: " + this.villageSnapshot.GetProperty("worships_faction", "unknown"));
				}
			}
			if (this.villageSnapshot.hasProperty("despises_faction"))
			{
				GameObject gameObject5 = EncountersAPI.GetALegendaryEligibleCreatureFromFaction(this.villageSnapshot.GetProperty("despises_faction", "unknown"), null) ?? EncountersAPI.GetACreatureFromFaction(this.villageSnapshot.GetProperty("despises_faction", "unknown"), null);
				if (gameObject5 != null)
				{
					GameObject gameObject6 = GameObject.Create(PopulationManager.RollOneFrom("Village_RandomBaseStatue_*Default", null, null).Blueprint);
					gameObject6.GetPart<RandomStatue>().SetCreature(gameObject5.DeepCopy(false, true, null));
					if (50.in100())
					{
						gameObject6.AddPart<ModDesecrated>(new ModDesecrated(), true, false);
						gameObject6.SetStringProperty("QuestVerb", "pray at", false);
						gameObject6.SetStringProperty("QuestEvent", "Prayed", false);
					}
					else
					{
						gameObject6.SetStringProperty("QuestVerb", "desecrate", false);
						gameObject6.SetStringProperty("QuestEvent", "Desecrated", false);
					}
					gameObject6.AddPart<Shrine>(new Shrine(), true, false);
					list.Add(gameObject6);
				}
				else
				{
					MetricsManager.LogWarning("Unable to find a creature for faction: " + this.villageSnapshot.GetProperty("despises_faction", "unknown"));
				}
			}
			if (this.villageSnapshot.hasProperty("worships_sultan"))
			{
				GameObject gameObject7 = GameObject.Create("SultanShrine");
				gameObject7.SetStringProperty("ForceSultan", this.villageSnapshot.GetProperty("worships_sultan_id", "unknown"), false);
				gameObject7.SetStringProperty("QuestVerb", "pray at", false);
				gameObject7.SetStringProperty("QuestEvent", "Prayed", false);
				gameObject7.AddPart<Shrine>(new Shrine(), true, false);
				list.Add(gameObject7);
			}
			if (this.villageSnapshot.hasProperty("despises_sultan"))
			{
				GameObject gameObject8 = GameObject.Create("SultanShrine");
				gameObject8.SetStringProperty("ForceSultan", this.villageSnapshot.GetProperty("despises_sultan_id", "unknown"), false);
				if (50.in100())
				{
					gameObject8.AddPart<ModDesecrated>(new ModDesecrated(), true, false);
					gameObject8.SetStringProperty("QuestVerb", "pray at", false);
					gameObject8.SetStringProperty("QuestEvent", "Prayed", false);
				}
				else
				{
					gameObject8.SetStringProperty("QuestVerb", "desecrate", false);
					gameObject8.SetStringProperty("QuestEvent", "Desecrated", false);
				}
				gameObject8.AddPart<Shrine>(new Shrine(), true, false);
				list.Add(gameObject8);
			}
			int maxTechTier = Convert.ToInt32(this.villageSnapshot.GetProperty("techTier", "unknown")) + 2;
			GameObject gameObject9 = EncountersAPI.GetAnObject((GameObjectBlueprint b) => b.TechTier <= maxTechTier && !b.HasTag("NotQuestable", false) && b.HasTag("QuestableVerb", false) && !string.IsNullOrEmpty(b.GetTag("QuestableVerb", "")));
			if (gameObject9 == null)
			{
				gameObject9 = EncountersAPI.GetAnObject((GameObjectBlueprint b) => b.HasTag("QuestableVerb", false) && !string.IsNullOrEmpty(b.GetTag("QuestableVerb", "")));
			}
			if (gameObject9 != null)
			{
				string[] array = gameObject9.GetTag("QuestableVerb", null).Split(',', StringSplitOptions.None);
				string[] array2 = gameObject9.GetTag("QuestableEvent", null).Split(',', StringSplitOptions.None);
				int num = Stat.Random(0, array.Count<string>() - 1);
				gameObject9.SetStringProperty("QuestVerb", array[num], false);
				gameObject9.SetStringProperty("QuestEvent", array2[num], false);
				list.Add(gameObject9);
				list.Add(gameObject9);
				list.Add(gameObject9);
			}
			GameObject randomElement = list.GetRandomElement<GameObject>(null);
			if (randomElement != null)
			{
				randomElement.SetIntProperty("norestock", 1, false);
				randomElement.SetStringProperty("NeverStack", "1", false);
			}
			return list.GetRandomElement<GameObject>(null);
		}

		// Token: 0x06005A4C RID: 23116 RVA: 0x001B6044 File Offset: 0x001B4244
		public override GameObject getQuestGenericRemoteInteractable()
		{
			List<GameObject> list = new List<GameObject>();
			int maxTechTier = Convert.ToInt32(this.villageSnapshot.GetProperty("techTier", "unknown")) + 2;
			GameObject gameObject = EncountersAPI.GetAnObject((GameObjectBlueprint b) => b.TechTier <= maxTechTier && !b.HasTag("NotQuestable", false) && b.HasTag("QuestableVerb", false) && !string.IsNullOrEmpty(b.GetTag("QuestableVerb", "")));
			if (gameObject == null)
			{
				gameObject = EncountersAPI.GetAnObject((GameObjectBlueprint b) => b.HasTag("QuestableVerb", false) && !string.IsNullOrEmpty(b.GetTag("QuestableVerb", "")));
			}
			if (gameObject != null)
			{
				string[] array = gameObject.GetTag("QuestableVerb", null).Split(',', StringSplitOptions.None);
				string[] array2 = gameObject.GetTag("QuestableEvent", null).Split(',', StringSplitOptions.None);
				int num = Stat.Random(0, array.Count<string>() - 1);
				gameObject.SetStringProperty("QuestVerb", array[num], false);
				gameObject.SetStringProperty("QuestEvent", array2[num], false);
				list.Add(gameObject);
			}
			GameObject randomElement = list.GetRandomElement<GameObject>(null);
			if (randomElement != null)
			{
				randomElement.SetIntProperty("norestock", 1, false);
				randomElement.SetIntProperty("QuestItem", 1, false);
			}
			return randomElement;
		}

		// Token: 0x06005A4D RID: 23117 RVA: 0x001B6148 File Offset: 0x001B4348
		public override GameObject getQuestDeliveryItem()
		{
			bool flag = this.villageSnapshot.hasProperty("signatureItem") && GameObject.CreateSample(this.villageSnapshot.GetProperty("signatureItem", "unknown")).IsTakeable();
			bool flag2 = false;
			if (this.villageSnapshot.hasProperty("signatureHistoricObjectType"))
			{
				GameObject gameObject = GameObject.CreateSample(this.villageSnapshot.GetProperty("signatureHistoricObjectType", "unknown"));
				flag2 = gameObject.IsTakeable() && gameObject.Weight <= 100;
			}
			int num = global::HistoryKit.Switch.RandomWhere(new bool[] { flag, flag2 });
			GameObject gameObject2;
			if (num != 0)
			{
				if (num != 1)
				{
					gameObject2 = EncountersAPI.GetAnItem((GameObjectBlueprint ob) => ob.Tier <= this.villageSnapshot.Tier && ob.GetPartParameter<bool>("Physics", "Takeable", true) && ob.GetPartParameter<int>("Physics", "Weight", 0) <= 100 && !ob.HasTag("ExcludeFromQuests", false));
				}
				else
				{
					gameObject2 = GameObject.Create(this.villageSnapshot.GetProperty("signatureHistoricObjectType", "unknown"));
				}
			}
			else
			{
				gameObject2 = GameObject.Create(this.villageSnapshot.GetProperty("signatureItem", "unknown"));
			}
			gameObject2.Render.SetForegroundColor(this.villageSnapshot.GetList("palette")[0]);
			gameObject2.Render.DetailColor = this.villageSnapshot.GetList("palette")[1];
			gameObject2.SetIntProperty("norestock", 1, false);
			gameObject2.SetIntProperty("QuestItem", 1, false);
			gameObject2.SetStringProperty("NeverStack", "1", false);
			return gameObject2;
		}

		// Token: 0x06005A4E RID: 23118 RVA: 0x001B62B0 File Offset: 0x001B44B0
		public override string getQuestItemNameMutation(string name)
		{
			string text = (this.villageSnapshot.hasListProperty("itemAdjectiveRoots") ? Grammar.Adjectify(this.villageSnapshot.GetList("itemAdjectiveRoots").GetRandomElement<string>(null)) : HistoricStringExpander.ExpandString("<spice.commonPhrases.sacred.!random>", null));
			return HistoricStringExpander.ExpandString("<spice.quests.questContext.itemNameMutation.!random>", null, null, new Dictionary<string, string>
			{
				{ "*adj*", text },
				{
					"*adj.cap*",
					Grammar.MakeTitleCase(text)
				},
				{ "*itemName*", name },
				{
					"*itemName.cap*",
					Grammar.MakeTitleCase(name)
				},
				{
					"*noun.cap*",
					Grammar.MakeTitleCase(HistoricStringExpander.ExpandString("<spice.nouns.!random>", null))
				}
			}, null);
		}

		// Token: 0x06005A4F RID: 23119 RVA: 0x001B6361 File Offset: 0x001B4561
		public override string assassinationTargetId()
		{
			return null;
		}

		// Token: 0x06005A50 RID: 23120 RVA: 0x001B6364 File Offset: 0x001B4564
		public override GameObject getALostItem()
		{
			return null;
		}

		// Token: 0x04002C9E RID: 11422
		public string VillageEntityID;

		// Token: 0x04002C9F RID: 11423
		public bool isVillageZero;

		// Token: 0x04002CA0 RID: 11424
		[NonSerialized]
		public List<Location2D> usedLocations;

		// Token: 0x04002CA1 RID: 11425
		[NonSerialized]
		private HistoricEntity _VillageEntity;

		// Token: 0x04002CA2 RID: 11426
		[NonSerialized]
		private HistoricEntitySnapshot _VillageSnapshot;
	}
}
