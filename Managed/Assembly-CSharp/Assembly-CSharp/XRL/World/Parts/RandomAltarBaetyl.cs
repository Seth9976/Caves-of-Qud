using System;
using System.Collections.Generic;
using System.Text;
using ConsoleLib.Console;
using HistoryKit;
using Qud.API;
using XRL.Annals;
using XRL.Core;
using XRL.Language;
using XRL.Rules;
using XRL.UI;
using XRL.Wish;
using XRL.World.Capabilities;
using XRL.World.Effects;

namespace XRL.World.Parts
{
	// Token: 0x02000E0B RID: 3595
	[HasWishCommand]
	[Serializable]
	public class RandomAltarBaetyl : IPart
	{
		// Token: 0x06009127 RID: 37159 RVA: 0x00345679 File Offset: 0x00343879
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x06009128 RID: 37160 RVA: 0x0034567C File Offset: 0x0034387C
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == AutoexploreObjectEvent.ID || ID == BeforeDeathRemovalEvent.ID || ID == BeginConversationEvent.ID || ID == CanSmartUseEvent.ID || ID == EnteredCellEvent.ID || ID == SingletonEvent<GetDebugInternalsEvent>.ID || ID == PooledEvent<GetItemElementsEvent>.ID || ID == PooledEvent<GetPointsOfInterestEvent>.ID || ID == LeftCellEvent.ID;
		}

		// Token: 0x06009129 RID: 37161 RVA: 0x003456E0 File Offset: 0x003438E0
		public override bool HandleEvent(AutoexploreObjectEvent E)
		{
			if (!this.Fulfilled && !E.AutogetOnlyMode && E.Command != "Chat" && JournalAPI.GetMapNote(this.MapNoteID) == null && !this.ParentObject.IsHostileTowards(E.Actor))
			{
				E.Command = "Chat";
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600912A RID: 37162 RVA: 0x00345741 File Offset: 0x00343941
		public override bool HandleEvent(GetItemElementsEvent E)
		{
			if (E.IsRelevantCreature(this.ParentObject))
			{
				E.Add("chance", 1);
				E.Add("circuitry", 1);
				E.Add("scholarship", 1);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600912B RID: 37163 RVA: 0x0034577C File Offset: 0x0034397C
		public override bool HandleEvent(GetDebugInternalsEvent E)
		{
			string text = "DemandObject";
			GameObject demandObject = this.DemandObject;
			E.AddEntry(this, text, (demandObject != null) ? demandObject.DebugName : null);
			E.AddEntry(this, "DemandBlueprint", this.DemandBlueprint);
			E.AddEntry(this, "DemandIsMod", this.DemandIsMod);
			E.AddEntry(this, "DemandCount", this.DemandCount);
			E.AddEntry(this, "RewardType", this.RewardType);
			E.AddEntry(this, "RewardTier", this.RewardTier);
			E.AddEntry(this, "RewardID", this.RewardID);
			string text2 = "Reward";
			GameObject gameObject = The.ZoneManager.peekCachedObject(this.RewardID);
			E.AddEntry(this, text2, (gameObject != null) ? gameObject.DebugName : null);
			E.AddEntry(this, "RewardAttributePointsAmount", this.RewardAttributePointsAmount);
			E.AddEntry(this, "RewardMutationPointsAmount", this.RewardMutationPointsAmount);
			E.AddEntry(this, "RewardSkillPointsAmount", this.RewardSkillPointsAmount);
			E.AddEntry(this, "RewardExperiencePointsAmount", this.RewardExperiencePointsAmount);
			E.AddEntry(this, "RewardLicensePointsAmount", this.RewardLicensePointsAmount);
			E.AddEntry(this, "RewardReputationAmount", this.RewardReputationAmount);
			E.AddEntry(this, "RewardReputation", this.RewardReputation);
			E.AddEntry(this, "RewardReputationFaction", this.RewardReputationFaction);
			E.AddEntry(this, "RewardItem", this.RewardItem);
			E.AddEntry(this, "RewardItemMod", this.RewardItemMod);
			E.AddEntry(this, "RewardItemBonusModChance", this.RewardItemBonusModChance);
			E.AddEntry(this, "RewardItemSetModNumber", this.RewardItemSetModNumber);
			E.AddEntry(this, "RewardItemMinorBestowalChance", this.RewardItemMinorBestowalChance);
			E.AddEntry(this, "RewardItemElementBestowalChance", this.RewardItemElementBestowalChance);
			E.AddEntry(this, "Fulfilled", this.Fulfilled);
			return base.HandleEvent(E);
		}

		// Token: 0x0600912C RID: 37164 RVA: 0x00345950 File Offset: 0x00343B50
		public override bool HandleEvent(GetPointsOfInterestEvent E)
		{
			if (!this.Fulfilled && E.StandardChecks(this, E.Actor, null) && E.Actor.IsPlayer())
			{
				E.Add(this.ParentObject, this.ParentObject.GetReferenceDisplayName(int.MaxValue, null, null, false, false, false, false, false, false, false, false, null, false), null, null, null, null, null, -1, null, 0);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600912D RID: 37165 RVA: 0x003459BC File Offset: 0x00343BBC
		public override bool HandleEvent(BeginConversationEvent E)
		{
			if (E.SpeakingWith == this.ParentObject && E.Conversation.ID == this.ParentObject.GetBlueprint(true).GetPartParameter<string>("ConversationScript", "ConversationID", null) && this.CanTalk(E.Actor))
			{
				this.BaetylWantsSacrifice();
				E.RequestInterfaceExit();
				return false;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600912E RID: 37166 RVA: 0x00345A28 File Offset: 0x00343C28
		public override bool HandleEvent(BeforeDeathRemovalEvent E)
		{
			if (base.Visible())
			{
				this.RemoveJournalNote();
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600912F RID: 37167 RVA: 0x00345A3F File Offset: 0x00343C3F
		public override bool HandleEvent(CanSmartUseEvent E)
		{
			return (!E.Actor.IsPlayer() || !this.CanTalk(E.Actor)) && base.HandleEvent(E);
		}

		// Token: 0x06009130 RID: 37168 RVA: 0x00345A65 File Offset: 0x00343C65
		public override bool HandleEvent(LeftCellEvent E)
		{
			this.FromCell = E.Cell;
			return base.HandleEvent(E);
		}

		// Token: 0x06009131 RID: 37169 RVA: 0x00345A7C File Offset: 0x00343C7C
		public override bool HandleEvent(EnteredCellEvent E)
		{
			this.GenerateDemand(null);
			this.GenerateReward(100);
			if (this.FromCell != null && this.FromCell.IsVisible())
			{
				JournalMapNote mapNote = JournalAPI.GetMapNote(this.MapNoteID);
				if (mapNote != null && (E.Cell.ParentZone == null || E.Cell.ParentZone.ZoneID != mapNote.ZoneID))
				{
					this.RemoveJournalNote();
				}
			}
			this.FromCell = null;
			return base.HandleEvent(E);
		}

		// Token: 0x06009132 RID: 37170 RVA: 0x00345AFC File Offset: 0x00343CFC
		public static string GetModDemandName(string part, int num)
		{
			StringBuilder stringBuilder = Event.NewStringBuilder(null);
			stringBuilder.Append(Grammar.Cardinal(num)).Append(' ');
			ModEntry modEntry = ModificationFactory.ModsByPart[part];
			if (modEntry.TinkerDisplayName.Contains("with ") || modEntry.TinkerDisplayName.Contains("of "))
			{
				stringBuilder.Append((num == 1) ? "item" : "items").Append(' ').Append(modEntry.TinkerDisplayName);
			}
			else
			{
				stringBuilder.Append(modEntry.TinkerDisplayName).Append(' ').Append((num == 1) ? "item" : "items");
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06009133 RID: 37171 RVA: 0x00345BB0 File Offset: 0x00343DB0
		public string GetDemandName(bool Caps = true)
		{
			string text = (this.DemandIsMod ? RandomAltarBaetyl.GetModDemandName(this.DemandBlueprint, this.DemandCount) : this.DemandObject.GetDemandName(this.DemandCount));
			if (Caps)
			{
				text = ColorUtility.ToUpperExceptFormatting(text);
			}
			return text;
		}

		// Token: 0x06009134 RID: 37172 RVA: 0x00345BF8 File Offset: 0x00343DF8
		public void GenerateDemand(Zone Z = null)
		{
			if (this.DemandBlueprint == null)
			{
				if (30.in100())
				{
					int num = 0;
					ModEntry randomElement;
					for (;;)
					{
						randomElement = ModificationFactory.ModList.GetRandomElement<ModEntry>(null);
						if (!randomElement.NoSparkingQuest && randomElement.MinTier <= 8)
						{
							break;
						}
						if (++num > 10000)
						{
							goto Block_12;
						}
					}
					this.DemandBlueprint = randomElement.Part;
					this.DemandCount = Stat.Random(3, 6);
					this.DemandIsMod = true;
					if (this.RewardTier == 0)
					{
						this.RewardTier = randomElement.TinkerTier;
						if (randomElement.TinkerAllowed)
						{
							this.RewardTier += randomElement.Rarity;
						}
						else
						{
							this.RewardTier += randomElement.Rarity * randomElement.Rarity;
						}
						if (randomElement.CanAutoTinker)
						{
							this.RewardTier--;
						}
						if (Z == null && this.ParentObject != null)
						{
							Z = this.ParentObject.CurrentZone;
						}
						if (Z != null && Z.NewTier > this.RewardTier)
						{
							this.RewardTier = Z.NewTier;
						}
						this.RewardTier += Stat.Random(1, 2);
						Tier.Constrain(ref this.RewardTier);
						goto IL_0266;
					}
					goto IL_0266;
					Block_12:
					throw new Exception("cannot find any baetyl demands, aborting to avoid infinite loop");
				}
				int num2 = 0;
				GameObjectBlueprint randomElement2;
				for (;;)
				{
					randomElement2 = GameObjectFactory.Factory.BlueprintList.GetRandomElement<GameObjectBlueprint>(null);
					if (!randomElement2.HasPart("Brain") && randomElement2.HasPart("Physics") && randomElement2.HasPart("Render") && !randomElement2.Tags.ContainsKey("NoSparkingQuest") && !randomElement2.Tags.ContainsKey("BaseObject") && !randomElement2.Tags.ContainsKey("ExcludeFromDynamicEncounters") && randomElement2.GetPartParameter<bool>("Physics", "Takeable", true) && randomElement2.GetPartParameter<bool>("Physics", "IsReal", true) && !randomElement2.GetPartParameter<string>("Render", "DisplayName", null).Contains("[") && (!randomElement2.Props.ContainsKey("SparkingQuestBlueprint") || randomElement2.Name == randomElement2.Props["SparkingQuestBlueprint"]))
					{
						break;
					}
					if (++num2 > 10000)
					{
						goto Block_23;
					}
				}
				this.DemandBlueprint = randomElement2.Name;
				this.DemandCount = Stat.Random(3, 6);
				this.DemandIsMod = false;
				goto IL_0266;
				Block_23:
				throw new Exception("cannot find any baetyl demands, aborting to avoid infinite loop");
			}
			IL_0266:
			if (this.DemandObject == null && !this.DemandIsMod)
			{
				this.DemandObject = GameObject.CreateSample(this.DemandBlueprint);
				if (this.RewardTier == 0)
				{
					this.RewardTier = this.DemandObject.GetTier();
					if (Z == null && this.ParentObject != null)
					{
						Z = this.ParentObject.CurrentZone;
					}
					if (Z != null && Z.NewTier > this.RewardTier)
					{
						this.RewardTier = Z.NewTier;
					}
					this.RewardTier += Stat.Random(1, 2);
					Tier.Constrain(ref this.RewardTier);
				}
			}
		}

		// Token: 0x06009135 RID: 37173 RVA: 0x00345EFC File Offset: 0x003440FC
		public void GenerateReward(int Value = 100)
		{
			bool flag = !this.RewardType.IsNullOrEmpty();
			if (flag && Value == 100)
			{
				return;
			}
			RandomAltarBaetylReward randomAltarBaetylReward;
			if (flag)
			{
				randomAltarBaetylReward = RandomAltarBaetylRewardManager.GetReward(this.RewardType);
			}
			else
			{
				randomAltarBaetylReward = RandomAltarBaetylRewardManager.GetRandomReward(The.Player);
				this.RewardType = ((randomAltarBaetylReward != null) ? randomAltarBaetylReward.Name : null);
			}
			if (randomAltarBaetylReward == null)
			{
				randomAltarBaetylReward = RandomAltarBaetylRewardManager.GetRandomReward(The.Player);
				this.RewardType = ((randomAltarBaetylReward != null) ? randomAltarBaetylReward.Name : null);
				if (randomAltarBaetylReward == null)
				{
					return;
				}
				flag = false;
			}
			if (!flag)
			{
				this.RewardItem = randomAltarBaetylReward.Item;
				this.RewardItemMod = randomAltarBaetylReward.ItemMod;
				string itemBonusModChance = randomAltarBaetylReward.ItemBonusModChance;
				this.RewardItemBonusModChance = ((itemBonusModChance != null) ? itemBonusModChance.RollCached() : 0);
				string itemSetModNumber = randomAltarBaetylReward.ItemSetModNumber;
				this.RewardItemSetModNumber = ((itemSetModNumber != null) ? itemSetModNumber.RollCached() : 0);
				string itemMinorBestowalChance = randomAltarBaetylReward.ItemMinorBestowalChance;
				this.RewardItemMinorBestowalChance = ((itemMinorBestowalChance != null) ? itemMinorBestowalChance.RollCached() : 0);
				string itemElementBestowalChance = randomAltarBaetylReward.ItemElementBestowalChance;
				this.RewardItemElementBestowalChance = ((itemElementBestowalChance != null) ? itemElementBestowalChance.RollCached() : 0);
				string attributePoints = randomAltarBaetylReward.AttributePoints;
				this.RewardAttributePointsAmount = ((attributePoints != null) ? attributePoints.RollCached() : 0);
				string mutationPoints = randomAltarBaetylReward.MutationPoints;
				this.RewardMutationPointsAmount = ((mutationPoints != null) ? mutationPoints.RollCached() : 0);
				string skillPoints = randomAltarBaetylReward.SkillPoints;
				this.RewardSkillPointsAmount = ((skillPoints != null) ? skillPoints.RollCached() : 0);
				string experiencePoints = randomAltarBaetylReward.ExperiencePoints;
				this.RewardExperiencePointsAmount = ((experiencePoints != null) ? experiencePoints.RollCached() : 0);
				string licensePoints = randomAltarBaetylReward.LicensePoints;
				this.RewardLicensePointsAmount = ((licensePoints != null) ? licensePoints.RollCached() : 0);
				this.RewardReputation = randomAltarBaetylReward.Reputation;
				string rewardReputation = this.RewardReputation;
				this.RewardReputationAmount = ((rewardReputation != null) ? rewardReputation.RollCached() : 0);
				this.RewardReputationFaction = randomAltarBaetylReward.ReputationFaction;
			}
			this.GenerateRewardItem(Value);
			this.GenerateRewardAmounts(Value);
			this.GenerateRewardReputationFaction(Value);
		}

		// Token: 0x06009136 RID: 37174 RVA: 0x003460B4 File Offset: 0x003442B4
		private void GenerateRewardItem(int Value = 100)
		{
			if (!this.RewardID.IsNullOrEmpty())
			{
				if (Value == 100 && !this.RewardItem.IsNullOrEmpty())
				{
					return;
				}
				The.ZoneManager.UncacheObject(this.RewardID);
				this.RewardID = null;
			}
			if (!this.RewardItem.IsNullOrEmpty())
			{
				GameObject gameObject = this.GenerateItem(this.RewardItem, Value);
				if (gameObject != null)
				{
					this.RewardID = The.ZoneManager.CacheObject(gameObject, false, false);
				}
			}
		}

		// Token: 0x06009137 RID: 37175 RVA: 0x0034612C File Offset: 0x0034432C
		private void GenerateRewardAmounts(int Value = 100)
		{
			if (Value == 100)
			{
				return;
			}
			if (this.RewardAttributePointsAmount > 0)
			{
				this.RewardAttributePointsAmount = this.AdjustByValue(this.RewardAttributePointsAmount, Value, false);
			}
			if (this.RewardMutationPointsAmount > 0)
			{
				this.RewardMutationPointsAmount = this.AdjustByValue(this.RewardMutationPointsAmount, Value, false);
			}
			if (this.RewardSkillPointsAmount > 0)
			{
				this.RewardSkillPointsAmount = this.AdjustByValue(this.RewardSkillPointsAmount, Value, false);
			}
			if (this.RewardExperiencePointsAmount > 0)
			{
				this.RewardExperiencePointsAmount = this.AdjustByValue(this.RewardExperiencePointsAmount, Value, false);
			}
			if (this.RewardLicensePointsAmount > 0)
			{
				this.RewardLicensePointsAmount = this.AdjustByValue(this.RewardLicensePointsAmount, Value, false);
			}
			if (this.RewardReputationAmount > 0)
			{
				this.RewardReputationAmount = this.VaryRewardReputation(this.RewardReputationAmount, Value);
			}
		}

		// Token: 0x06009138 RID: 37176 RVA: 0x003461EC File Offset: 0x003443EC
		private int VaryRewardReputation(int Amount, int Value = 100)
		{
			return GivesRep.VaryRep(Amount + Value - 100);
		}

		// Token: 0x06009139 RID: 37177 RVA: 0x003461FC File Offset: 0x003443FC
		private void GenerateRewardReputationFaction(int Value = 100)
		{
			if (this.RewardReputationAmount > 0 && this.RewardReputationFaction.IsNullOrEmpty())
			{
				List<string> list = new List<string>(64);
				foreach (Faction faction in Factions.Loop())
				{
					if (faction.Visible)
					{
						list.Add(faction.Name);
					}
				}
				this.RewardReputationFaction = list.GetRandomElement<string>(null);
			}
		}

		// Token: 0x0600913A RID: 37178 RVA: 0x00346280 File Offset: 0x00344480
		private GameObject RetrieveRewardItem()
		{
			return The.ZoneManager.PullCachedObject(this.RewardID, true);
		}

		// Token: 0x0600913B RID: 37179 RVA: 0x00346293 File Offset: 0x00344493
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600913C RID: 37180 RVA: 0x00346296 File Offset: 0x00344496
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("BaetylLeaving");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600913D RID: 37181 RVA: 0x003462AB File Offset: 0x003444AB
		public override bool FireEvent(Event E)
		{
			if (E.ID == "BaetylLeaving" && base.Visible())
			{
				this.RemoveJournalNote();
			}
			return base.FireEvent(E);
		}

		// Token: 0x0600913E RID: 37182 RVA: 0x003462D4 File Offset: 0x003444D4
		public override bool Render(RenderEvent E)
		{
			if (!this.Fulfilled && this.lastSparkFrame != XRLCore.CurrentFrame)
			{
				this.lastSparkFrame = XRLCore.CurrentFrame;
				if (Stat.RandomCosmetic(1, 120) <= 2)
				{
					for (int i = 0; i < 2; i++)
					{
						this.ParentObject.ParticleText("&Y" + ((char)Stat.RandomCosmetic(191, 198)).ToString(), 0.2f, 20);
					}
					for (int j = 0; j < 2; j++)
					{
						this.ParentObject.ParticleText("&W\u000f", 0.02f, 10);
					}
					base.PlayWorldSound("sfx_spark", 0.35f, 0.35f, false, null, 0f, 1f, 1f, int.MaxValue);
				}
			}
			return true;
		}

		// Token: 0x0600913F RID: 37183 RVA: 0x003463A4 File Offset: 0x003445A4
		public static bool DisplayNameMatches(string B1, string B2)
		{
			GameObjectBlueprint gameObjectBlueprint = GameObjectFactory.Factory.Blueprints[B1];
			GameObjectBlueprint gameObjectBlueprint2 = GameObjectFactory.Factory.Blueprints[B2];
			string partParameter = gameObjectBlueprint.GetPartParameter<string>("Render", "DisplayName", null);
			if (partParameter == null)
			{
				return false;
			}
			string partParameter2 = gameObjectBlueprint2.GetPartParameter<string>("Render", "DisplayName", null);
			return partParameter2 != null && (partParameter == partParameter2 || ColorUtility.StripFormatting(partParameter) == ColorUtility.StripFormatting(partParameter2));
		}

		// Token: 0x06009140 RID: 37184 RVA: 0x00346420 File Offset: 0x00344620
		private int AdjustByValue(int Number, int Value, bool AllowZero = false)
		{
			if (Value != 100)
			{
				if (Value <= 10 && Number > (AllowZero ? 0 : 1))
				{
					Number--;
				}
				if (Value <= 50 && Number > (AllowZero ? 0 : 1))
				{
					Number--;
				}
				if (Value >= 150)
				{
					Number++;
				}
				if (Value >= 300)
				{
					Number++;
				}
			}
			return Number;
		}

		// Token: 0x06009141 RID: 37185 RVA: 0x00346478 File Offset: 0x00344678
		public GameObject GenerateItem(string Spec, int Value, int TierSpec)
		{
			int num = Tier.Constrain(this.AdjustByValue(TierSpec, Value, false));
			if (Spec.Contains("{tier}"))
			{
				Spec = Spec.Replace("{tier}", num.ToString());
			}
			int num2 = this.RewardItemBonusModChance * Value / 100;
			int num3 = this.AdjustByValue(this.RewardItemSetModNumber, Value, true);
			int num4 = this.RewardItemMinorBestowalChance * Value / 100;
			int num5 = this.RewardItemElementBestowalChance * Value / 100;
			GameObject obj = null;
			GameObjectFactory.ProcessSpecification(Spec, delegate(GameObject o)
			{
				GameObject obj2 = obj;
				if (obj2 != null)
				{
					obj2.Obliterate(null, false, null);
				}
				obj = o;
			}, null, 1, num2, num3, this.RewardItemMod, null, null, null, null, null);
			if (obj == null)
			{
				MetricsManager.LogError(string.Format("NULL object returned from spec {0} {1} {2} in RandomAltarBaetyl::GenerateItem", Spec, Value, TierSpec));
				return RelicGenerator.GenerateRelic(num, false);
			}
			bool flag = false;
			bool flag2 = false;
			int num6 = 20;
			string type = RelicGenerator.GetType(obj);
			string subtype = RelicGenerator.GetSubtype(type);
			string text = null;
			while (num4.in100())
			{
				if (RelicGenerator.ApplyBasicBestowal(obj, type, num, subtype, false, false))
				{
					flag = true;
					num6 += 20;
				}
				num4 -= 100;
			}
			if (num5.in100())
			{
				if (text == null)
				{
					text = RelicGenerator.SelectElement(obj, null, null, null);
				}
				if (!text.IsNullOrEmpty() && RelicGenerator.ApplyElementBestowal(obj, text, type, num, subtype))
				{
					flag2 = true;
					num6 += 40;
				}
			}
			if (flag || flag2)
			{
				obj.SetStringProperty("Mods", "None", false);
				if (text == null)
				{
					text = RelicGenerator.SelectElement(obj, null, null, null) ?? "might";
				}
				string text2 = null;
				string text3 = null;
				if (15.in100())
				{
					List<JournalMapNote> mapNotes = JournalAPI.GetMapNotes((JournalMapNote note) => note.Has("ruins") && !note.Has("historic") && note.Text != "some forgotten ruins");
					if (mapNotes.Count > 0)
					{
						text2 = HistoricStringExpander.ExpandString(string.Concat(new string[]
						{
							"<spice.elements.",
							text,
							".adjectives.!random> ",
							HistoricStringExpander.ExpandString("<spice.itemTypes." + type + ".!random>", null),
							" of ",
							mapNotes.GetRandomElement<JournalMapNote>(null).Text
						}), null);
						text3 = "the";
					}
				}
				if (text2 == null)
				{
					GameObject alegendaryEligibleCreature = EncountersAPI.GetALegendaryEligibleCreature(null);
					HeroMaker.MakeHero(alegendaryEligibleCreature, null, null, -1, "Hero");
					Dictionary<string, string> dictionary = new Dictionary<string, string>
					{
						{ "*element*", text },
						{ "*itemType*", type },
						{
							"*personNounPossessive*",
							Grammar.MakePossessive(HistoricStringExpander.ExpandString("<spice.personNouns.!random>", null))
						},
						{
							"*creatureNamePossessive*",
							Grammar.MakePossessive(alegendaryEligibleCreature.an(int.MaxValue, null, null, false, false, false, false, true, true, true, false, false, true, false, null, false, null, false))
						}
					};
					text2 = HistoricStringExpander.ExpandString("<spice.history.relics.names.!random>", null, null, dictionary, null);
					QudHistoryHelpers.ExtractArticle(ref text2, out text3);
				}
				obj.RequirePart<OriginalItemType>(false);
				text2 = QudHistoryHelpers.Ansify(Grammar.MakeTitleCase(text2), null);
				obj.GiveProperName(text2, true, null, false, null, null, null, null);
				if (!text3.IsNullOrEmpty())
				{
					obj.SetStringProperty("IndefiniteArticle", text3, false);
					obj.SetStringProperty("DefiniteArticle", text3, false);
				}
				obj.SetImportant(true, false, false);
				AfterPseudoRelicGeneratedEvent.Send(obj, text, type, subtype, num);
			}
			Commerce commerce;
			if (num6 != 0 && obj.TryGetPart<Commerce>(out commerce))
			{
				commerce.Value += commerce.Value * (double)num6 / 100.0;
			}
			return obj;
		}

		// Token: 0x06009142 RID: 37186 RVA: 0x00346834 File Offset: 0x00344A34
		public GameObject GenerateItem(string Spec, int Value = 100)
		{
			return this.GenerateItem(Spec, Value, this.RewardTier);
		}

		// Token: 0x06009143 RID: 37187 RVA: 0x00346844 File Offset: 0x00344A44
		public bool ItemMatchesDemand(GameObject obj)
		{
			if (this.DemandIsMod)
			{
				return obj.HasPart(this.DemandBlueprint);
			}
			return obj.Blueprint == this.DemandBlueprint || obj.GetPropertyOrTag("SparkingQuestBlueprint", null) == this.DemandBlueprint || RandomAltarBaetyl.DisplayNameMatches(obj.Blueprint, this.DemandBlueprint);
		}

		// Token: 0x06009144 RID: 37188 RVA: 0x003468A5 File Offset: 0x00344AA5
		private List<GameObject> SortForSacrifice(List<GameObject> List)
		{
			if (List.Count > 1)
			{
				List.Sort(new Comparison<GameObject>(this.SortForSacrifice));
			}
			return List;
		}

		// Token: 0x06009145 RID: 37189 RVA: 0x003468C4 File Offset: 0x00344AC4
		private int SortForSacrifice(GameObject A, GameObject B)
		{
			int num = A.ValueEach.CompareTo(B.ValueEach);
			if (num != 0)
			{
				return num;
			}
			int num2 = A.Count.CompareTo(B.Count);
			if (num2 != 0)
			{
				return -num2;
			}
			int num3 = A.WeightEach.CompareTo(B.WeightEach);
			if (num3 != 0)
			{
				return -num3;
			}
			return A.GetCachedDisplayNameForSort().CompareTo(B.GetCachedDisplayNameForSort());
		}

		// Token: 0x06009146 RID: 37190 RVA: 0x00346934 File Offset: 0x00344B34
		public void BaetylWantsSacrifice()
		{
			if (this.ParentObject.IsHostileTowards(The.Player))
			{
				return;
			}
			if (this.Fulfilled)
			{
				Popup.Show("I AM SATED, MORTAL. BEGONE.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
				return;
			}
			this.GenerateDemand(null);
			this.GenerateReward(100);
			StringBuilder stringBuilder = Event.NewStringBuilder(null);
			stringBuilder.Append("PETTY MORTAL! BRING ME ").Append(this.GetDemandName(true)).Append(", AND I SHALL REWARD YOU WITH ")
				.Append(this.GetRewardDescription())
				.Append('.');
			int num = 0;
			int num2 = this.DemandCount;
			List<GameObject> list = Event.NewGameObjectList();
			List<GameObject> list2 = Event.NewGameObjectList();
			List<GameObject> list3 = Event.NewGameObjectList();
			List<GameObject> list4 = Event.NewGameObjectList();
			foreach (Cell cell in this.ParentObject.CurrentCell.GetAdjacentCells(true))
			{
				list.AddRange(cell.GetObjectsInCell());
			}
			foreach (GameObject gameObject in this.SortForSacrifice(list))
			{
				if (this.ItemMatchesDemand(gameObject))
				{
					num += gameObject.Count;
					list4.Add(gameObject);
					list2.Add(gameObject);
					if (num >= num2)
					{
						break;
					}
				}
			}
			if (num < num2)
			{
				foreach (GameObject gameObject2 in this.SortForSacrifice(The.Player.GetInventoryAndEquipment()))
				{
					if (this.ItemMatchesDemand(gameObject2) && !gameObject2.IsImportant() && (gameObject2.Equipped == null || gameObject2.CanBeUnequipped(null, null, false, false)))
					{
						num += gameObject2.Count;
						list4.Add(gameObject2);
						list3.Add(gameObject2);
						if (num >= num2)
						{
							break;
						}
					}
				}
			}
			if (num >= num2)
			{
				stringBuilder.Append("\n\nOffer ").Append(this.ParentObject.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false)).Append(' ');
				if (num > num2)
				{
					stringBuilder.Append(num2.ToString()).Append(" out of ");
				}
				if (list2.Count > 0)
				{
					stringBuilder.Append(Grammar.MakeAndList(list2, true, true, false, true, false, null)).Append(" nearby");
					if (list3.Count > 0)
					{
						stringBuilder.Append(" and ").Append(Grammar.MakeAndList(list3, false, true, false, true, true, null));
					}
				}
				else
				{
					stringBuilder.Append(Grammar.MakeAndList(list3, false, true, false, true, true, null));
				}
				stringBuilder.Append('?');
				if (Popup.ShowYesNo(stringBuilder.ToString(), "Sounds/UI/ui_notification", true, DialogResult.Yes, null) == DialogResult.Yes)
				{
					foreach (GameObject gameObject3 in list4)
					{
						gameObject3.DustPuff("&y", 15);
						int count = gameObject3.Count;
						if (num2 >= count)
						{
							gameObject3.Obliterate(null, false, null);
						}
						else
						{
							for (int i = 0; i < num2; i++)
							{
								gameObject3.Destroy(null, false, false, null);
							}
						}
						num2 -= count;
						if (num2 <= 0)
						{
							break;
						}
					}
					int num3 = 100;
					if (Options.SifrahBaetylOfferings)
					{
						int num4 = The.Player.StatMod("Intelligence", 0);
						int num5 = this.RewardTier + this.ParentObject.GetIntProperty("BaetylOfferingSifrahDifficultyModifier", 0);
						BaetylOfferingSifrah baetylOfferingSifrah = new BaetylOfferingSifrah(this.ParentObject, num4, num5);
						baetylOfferingSifrah.Play(this.ParentObject);
						if (!baetylOfferingSifrah.Abort)
						{
							num3 = baetylOfferingSifrah.Performance;
						}
					}
					this.ParentObject.Render.ColorString = "&K^g";
					this.GenerateReward(num3);
					string text = this.GiveReward(The.Player, false, false, false, false, false, false, false, false, num3);
					this.Fulfilled = true;
					string demandName = this.GetDemandName(false);
					JournalAPI.AddAccomplishment(string.Concat(new string[] { "You appeased a baetyl with ", demandName, ", and in return received ", text, "." }), string.Concat(new string[]
					{
						"While leading a small army through ",
						Grammar.GetProsaicZoneName(The.Player.CurrentZone),
						", =name= demanded that a local baetyl use its powers to transmute ",
						demandName,
						" into ",
						this.GetRewardDescription().ToLower(),
						"."
					}), string.Concat(new string[]
					{
						"While leading a small army through ",
						Grammar.GetProsaicZoneName(The.Player.CurrentZone),
						", =name= demanded that a local baetyl use its powers to transmute ",
						demandName,
						" into ",
						this.GetRewardDescription().ToLower(),
						"."
					}), null, "general", MuralCategory.AppeasesBaetyl, MuralWeight.Medium, null, -1L, true);
					Popup.Show("I ACCEPT YOUR OFFERING!\n\nThe sparking baetyl gives you " + text + "!", null, "Sounds/UI/ui_notification", true, true, true, true, null);
				}
			}
			else
			{
				Popup.Show(stringBuilder.ToString(), null, "Sounds/UI/ui_notification", true, true, true, true, null);
			}
			this.UpdateJournalNote();
		}

		// Token: 0x06009147 RID: 37191 RVA: 0x00346E7C File Offset: 0x0034507C
		public string GiveReward(GameObject Receiver, bool IgnorePlayerControl = false, bool SkipItemMessage = false, bool SkipAttributePointsMessage = false, bool SkipMutationPointsMessage = false, bool SkipSkillPointsMessage = false, bool SkipExperiencePointsMessage = false, bool SkipLicensePointsMessage = false, bool SkipReputationMessage = false, int Value = 100)
		{
			List<string> list = new List<string>();
			if (!this.RewardID.IsNullOrEmpty())
			{
				GameObject gameObject = this.RetrieveRewardItem();
				Receiver.ReceiveObject(gameObject, false, null);
				if (!SkipItemMessage)
				{
					list.Add(gameObject.an(int.MaxValue, null, null, false, false, false, false, false, false, false, false, false, true, false, null, false, null, false));
				}
			}
			if (this.RewardAttributePointsAmount > 0)
			{
				Receiver.GetStat("AP").BaseValue += this.RewardAttributePointsAmount;
				if (!SkipAttributePointsMessage)
				{
					list.Add(this.RewardAttributePointsAmount.ToString() + " attribute " + ((this.RewardAttributePointsAmount == 1) ? "point" : "points"));
				}
			}
			if (this.RewardMutationPointsAmount > 0)
			{
				Receiver.GainMP(this.RewardMutationPointsAmount);
				if (!SkipMutationPointsMessage)
				{
					list.Add(this.RewardMutationPointsAmount.ToString() + " mutation " + ((this.RewardMutationPointsAmount == 1) ? "point" : "points"));
				}
			}
			if (this.RewardSkillPointsAmount > 0)
			{
				int num = this.RewardSkillPointsAmount * (50 + (Receiver.BaseStat("Intelligence", 0) - 10) * 4);
				Receiver.GetStat("SP").BaseValue += num;
				if (!SkipSkillPointsMessage)
				{
					list.Add(num.ToString() + " skill " + ((num == 1) ? "point" : "points"));
				}
			}
			if (this.RewardExperiencePointsAmount > 0)
			{
				int num2 = this.RewardExperiencePointsAmount * (50 + Receiver.Stat("Level", 0));
				Receiver.AwardXP(num2, -1, 0, int.MaxValue, null, this.ParentObject, null, null, null, null);
				if (!SkipExperiencePointsMessage)
				{
					list.Add(num2.ToString() + " experience " + ((num2 == 1) ? "point" : "points"));
				}
			}
			if (this.RewardLicensePointsAmount > 0)
			{
				Receiver.ModIntProperty("CyberneticsLicenses", this.RewardLicensePointsAmount, false);
				if (!SkipLicensePointsMessage)
				{
					list.Add(this.RewardLicensePointsAmount.ToString() + " cybernetics license " + ((this.RewardLicensePointsAmount == 1) ? "point" : "points"));
				}
			}
			if (this.RewardReputationAmount > 0 && (IgnorePlayerControl || Receiver.IsPlayerControlled()))
			{
				if (this.RewardReputationFaction.Contains("|"))
				{
					string[] array = this.RewardReputationFaction.Split('|', StringSplitOptions.None);
					bool flag = true;
					foreach (string text in array)
					{
						int num3;
						if (flag)
						{
							num3 = this.RewardReputationAmount;
							flag = false;
						}
						else
						{
							num3 = this.VaryRewardReputation(this.RewardReputation.RollCached(), Value);
						}
						The.Game.PlayerReputation.Modify(text, num3, "BaetylReward", null, null, false, false);
						if (!SkipReputationMessage)
						{
							list.Add(num3.ToString() + " reputation with " + Faction.GetFormattedName(text));
						}
					}
				}
				else
				{
					The.Game.PlayerReputation.Modify(this.RewardReputationFaction, this.RewardReputationAmount, "BaetylReward", null, null, false, false);
					if (!SkipReputationMessage)
					{
						list.Add(this.RewardReputationAmount.ToString() + " reputation with " + Faction.GetFormattedName(this.RewardReputationFaction));
					}
				}
			}
			return Grammar.MakeAndList(list, true);
		}

		// Token: 0x06009148 RID: 37192 RVA: 0x003471B8 File Offset: 0x003453B8
		public string GetRewardDescription()
		{
			string text = null;
			if (!this.RewardType.IsNullOrEmpty())
			{
				RandomAltarBaetylReward reward = RandomAltarBaetylRewardManager.GetReward(this.RewardType);
				if (reward != null)
				{
					text = reward.Description;
				}
			}
			return text ?? "STUFF";
		}

		// Token: 0x06009149 RID: 37193 RVA: 0x003471F4 File Offset: 0x003453F4
		public bool CanTalk(GameObject Actor = null)
		{
			return IsConversationallyResponsiveEvent.Check(this.ParentObject, Actor ?? The.Player, true, false) && !this.ParentObject.IsInStasis() && !this.ParentObject.HasEffect<Asleep>();
		}

		// Token: 0x17000A7F RID: 2687
		// (get) Token: 0x0600914A RID: 37194 RVA: 0x00347230 File Offset: 0x00345430
		public string MapNoteID
		{
			get
			{
				return "Baetyl." + this.ParentObject.ID;
			}
		}

		// Token: 0x0600914B RID: 37195 RVA: 0x00347248 File Offset: 0x00345448
		public void UpdateJournalNote()
		{
			StringBuilder stringBuilder = Event.NewStringBuilder(null);
			if (this.Fulfilled)
			{
				stringBuilder.Append("A \"SATED\" baetyl");
			}
			else
			{
				this.GenerateDemand(null);
				stringBuilder.Append("A baetyl demanding \"").Append(this.GetDemandName(true)).Append("\" and promising \"")
					.Append(this.GetRewardDescription())
					.Append("\".");
			}
			JournalMapNote mapNote = JournalAPI.GetMapNote(this.MapNoteID);
			if (mapNote == null)
			{
				JournalAPI.AddMapNote(this.ParentObject.GetCurrentCell().ParentZone.ZoneID, stringBuilder.ToString(), "Baetyls", null, this.MapNoteID, true, false, -1L, false);
				return;
			}
			mapNote.Text = stringBuilder.ToString();
		}

		// Token: 0x0600914C RID: 37196 RVA: 0x003472FD File Offset: 0x003454FD
		public void RemoveJournalNote(JournalMapNote note)
		{
			if (note != null)
			{
				JournalAPI.DeleteMapNote(note);
			}
		}

		// Token: 0x0600914D RID: 37197 RVA: 0x00347308 File Offset: 0x00345508
		public void RemoveJournalNote()
		{
			this.RemoveJournalNote(JournalAPI.GetMapNote(this.MapNoteID));
		}

		// Token: 0x0600914E RID: 37198 RVA: 0x0034731C File Offset: 0x0034551C
		[WishCommand(null, null, Command = "baetylrewarditem")]
		public static bool HandleBaetylRewardWish()
		{
			switch (Stat.Random(1, 4))
			{
			case 1:
				return RandomAltarBaetyl.HandleBaetylRewardMeleeWish();
			case 2:
				return RandomAltarBaetyl.HandleBaetylRewardArmorWish();
			case 3:
				return RandomAltarBaetyl.HandleBaetylRewardMissileWish();
			case 4:
				return RandomAltarBaetyl.HandleBaetylRewardArtifactWish();
			default:
				return true;
			}
		}

		// Token: 0x0600914F RID: 37199 RVA: 0x00347364 File Offset: 0x00345564
		[WishCommand(null, null, Command = "baetylrewardmelee")]
		public static bool HandleBaetylRewardMeleeWish()
		{
			return RandomAltarBaetyl.HandleBaetylRewardWish("@Melee Weapons {tier}R");
		}

		// Token: 0x06009150 RID: 37200 RVA: 0x00347370 File Offset: 0x00345570
		[WishCommand(null, null, Command = "baetylrewardarmor")]
		public static bool HandleBaetylRewardArmorWish()
		{
			return RandomAltarBaetyl.HandleBaetylRewardWish("@Armor {tier}R");
		}

		// Token: 0x06009151 RID: 37201 RVA: 0x0034737C File Offset: 0x0034557C
		[WishCommand(null, null, Command = "baetylrewardmissile")]
		public static bool HandleBaetylRewardMissileWish()
		{
			return RandomAltarBaetyl.HandleBaetylRewardWish("@Missile {tier}");
		}

		// Token: 0x06009152 RID: 37202 RVA: 0x00347388 File Offset: 0x00345588
		[WishCommand(null, null, Command = "baetylrewardartifact")]
		public static bool HandleBaetylRewardArtifactWish()
		{
			return RandomAltarBaetyl.HandleBaetylRewardWish("@Artifact {tier}R");
		}

		// Token: 0x06009153 RID: 37203 RVA: 0x00347394 File Offset: 0x00345594
		private static bool HandleBaetylRewardWish(string Spec)
		{
			RandomAltarBaetyl randomAltarBaetyl = new RandomAltarBaetyl();
			randomAltarBaetyl.GenerateDemand(The.Player.CurrentZone);
			GameObject gameObject = randomAltarBaetyl.GenerateItem(Spec, 100);
			Popup.Show("Generated " + gameObject.an(int.MaxValue, null, null, false, false, false, false, false, false, true, false, false, true, false, null, false, null, false) + " as reward for " + randomAltarBaetyl.GetDemandName(true), null, "Sounds/UI/ui_notification", true, true, true, true, null);
			The.Player.CurrentCell.AddObject(gameObject, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
			return true;
		}

		// Token: 0x04003C88 RID: 15496
		public GameObject DemandObject;

		// Token: 0x04003C89 RID: 15497
		public string DemandBlueprint;

		// Token: 0x04003C8A RID: 15498
		public bool DemandIsMod;

		// Token: 0x04003C8B RID: 15499
		public int DemandCount;

		// Token: 0x04003C8C RID: 15500
		public string RewardType;

		// Token: 0x04003C8D RID: 15501
		public int RewardTier;

		// Token: 0x04003C8E RID: 15502
		public string RewardID;

		// Token: 0x04003C8F RID: 15503
		public int RewardAttributePointsAmount;

		// Token: 0x04003C90 RID: 15504
		public int RewardMutationPointsAmount;

		// Token: 0x04003C91 RID: 15505
		public int RewardSkillPointsAmount;

		// Token: 0x04003C92 RID: 15506
		public int RewardExperiencePointsAmount;

		// Token: 0x04003C93 RID: 15507
		public int RewardLicensePointsAmount;

		// Token: 0x04003C94 RID: 15508
		public int RewardReputationAmount;

		// Token: 0x04003C95 RID: 15509
		public string RewardReputation;

		// Token: 0x04003C96 RID: 15510
		public string RewardReputationFaction;

		// Token: 0x04003C97 RID: 15511
		public string RewardItem;

		// Token: 0x04003C98 RID: 15512
		public string RewardItemMod;

		// Token: 0x04003C99 RID: 15513
		public int RewardItemBonusModChance;

		// Token: 0x04003C9A RID: 15514
		public int RewardItemSetModNumber;

		// Token: 0x04003C9B RID: 15515
		public int RewardItemMinorBestowalChance;

		// Token: 0x04003C9C RID: 15516
		public int RewardItemElementBestowalChance;

		// Token: 0x04003C9D RID: 15517
		public bool Fulfilled;

		// Token: 0x04003C9E RID: 15518
		[NonSerialized]
		private Cell FromCell;

		// Token: 0x04003C9F RID: 15519
		private int lastSparkFrame;
	}
}
