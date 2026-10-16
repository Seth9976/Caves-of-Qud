using System;
using System.Collections.Generic;
using System.Linq;
using Genkit;
using HistoryKit;
using Qud.API;
using UnityEngine;
using XRL.Annals;
using XRL.Language;
using XRL.Names;
using XRL.Rules;
using XRL.World.AI;
using XRL.World.Capabilities;
using XRL.World.Parts;
using XRL.World.Parts.Skill;
using XRL.World.Quests;
using XRL.World.ZoneBuilders.Utility;

namespace XRL.World.ZoneBuilders
{
	// Token: 0x02000B06 RID: 2822
	public class Village : VillageBase
	{
		// Token: 0x170008D1 RID: 2257
		// (get) Token: 0x060070A6 RID: 28838 RVA: 0x00288BF0 File Offset: 0x00286DF0
		public string mayorTemplate
		{
			get
			{
				if (this.villageSnapshot == null)
				{
					return null;
				}
				if (this.villageSnapshot.GetProperty("mayorTemplate", "unknown") != "unknown")
				{
					return this.villageSnapshot.GetProperty("mayorTemplate", "unknown");
				}
				return "Mayor";
			}
		}

		// Token: 0x060070A7 RID: 28839 RVA: 0x00288C44 File Offset: 0x00286E44
		public GameObject generateWarden(GameObject baseObject, bool GivesRep = false)
		{
			GameObject gameObject;
			if (baseObject != null)
			{
				gameObject = baseObject;
			}
			else
			{
				Func<GameObject> func = FuzzyFunctions.DoThisButRarelyDoThat<GameObject>(delegate
				{
					GameObject anonLegendaryCreature = EncountersAPI.GetANonLegendaryCreature((GameObjectBlueprint ob) => ob.HasTag(this.dynamicCreatureTableName, false) && (ob.HasPart("Body") || ob.HasTagOrProperty("BodySubstitute")) && (ob.HasPart("Combat") || ob.HasTagOrProperty("BodySubstitute")) && !ob.HasTag("Merchant", false) && !ob.HasTag("ExcludeFromVillagePopulations", false));
					if (anonLegendaryCreature == null)
					{
						return null;
					}
					return anonLegendaryCreature;
				}, () => EncountersAPI.GetANonLegendaryCreature((GameObjectBlueprint ob) => ob.HasPart("Body") && ob.HasPart("Combat") && !ob.HasTag("Merchant", false) && !ob.HasTag("ExcludeFromVillagePopulations", false)), "33");
				try
				{
					gameObject = func();
				}
				catch (Exception ex)
				{
					Debug.LogException(ex);
					gameObject = null;
				}
				if (gameObject == null)
				{
					gameObject = base.getBaseVillager(true);
					base.preprocessVillager(gameObject, false);
				}
				else
				{
					base.preprocessVillager(gameObject, true);
				}
			}
			gameObject.Brain.Mobile = true;
			gameObject.Brain.Factions = "";
			gameObject.Brain.Allegiance.Clear();
			gameObject.Brain.Allegiance.Add("Wardens", 100);
			gameObject.Brain.Allegiance.Hostile = false;
			gameObject.Brain.Allegiance.Calm = true;
			gameObject = HeroMaker.MakeHero(gameObject, "SpecialVillagerHeroTemplate_Warden", -1, "Warden");
			gameObject.RequirePart<Interesting>(false);
			gameObject.SetIntProperty("VillageWarden", 1, false);
			gameObject.SetIntProperty("NamedVillager", 1, false);
			if (this.isVillageZero)
			{
				gameObject.SetIntProperty("WaterRitualNoSellSkill", 1, false);
			}
			GivesRep givesRep = gameObject.GetPart<GivesRep>();
			if (givesRep != null)
			{
				givesRep.ResetRelatedFactions();
			}
			if (GivesRep)
			{
				gameObject.SetStringProperty("staticFaction1", this.villageFaction + ",friend,defending their village", false);
				string text = gameObject.GetPropertyOrTag("NoHateFactions", null);
				if (text.IsNullOrEmpty())
				{
					text = "Wardens";
				}
				else
				{
					text += ",Wardens";
				}
				gameObject.SetStringProperty("NoHateFactions", text, false);
				if (givesRep == null)
				{
					givesRep = gameObject.AddPart<GivesRep>(true, false);
				}
				givesRep.FillInRelatedFactions(true);
			}
			else if (givesRep != null)
			{
				gameObject.RemovePart(givesRep);
			}
			string text2 = HistoricStringExpander.ExpandString("<spice.villages.warden.introDialog.!random>", null);
			gameObject.SetIntProperty("SuppressSimpleConversation", 1, false);
			ConversationsAPI.addSimpleConversationToObject(gameObject, text2, "Live and drink.", null, null, null, true, true);
			TakeOnRoleEvent.Send(gameObject, "Warden");
			return gameObject;
		}

		// Token: 0x060070A8 RID: 28840 RVA: 0x00288E44 File Offset: 0x00287044
		public GameObject generateMayor(GameObject baseObject, string specialTemplate = "SpecialVillagerHeroTemplate_Mayor", bool GivesRep = true)
		{
			GameObject gameObject;
			if (baseObject != null)
			{
				gameObject = baseObject;
			}
			else
			{
				gameObject = base.getBaseVillager(false);
				base.preprocessVillager(gameObject, false);
				base.setVillagerProperties(gameObject);
			}
			if (gameObject.Brain != null)
			{
				gameObject.Brain.SetFactionFeeling(this.villageFaction, RuleSettings.REPUTATION_LOVED);
				gameObject.Brain.SetFactionFeeling(this.villagerBaseFaction, RuleSettings.REPUTATION_LOVED);
				gameObject.Brain.SetFactionMembership(this.villageFaction, 100);
				if (!this.isVillageZero)
				{
					gameObject.Brain.SetFactionMembership(this.villagerBaseFaction, 25);
				}
			}
			GivesRep givesRep = gameObject.GetPart<GivesRep>();
			if (givesRep != null)
			{
				givesRep.ResetRelatedFactions();
			}
			if (GivesRep)
			{
				string text = gameObject.GetPropertyOrTag("NoHateFactions", null);
				if (text.IsNullOrEmpty())
				{
					text = "Wardens";
				}
				else
				{
					text += ",Wardens";
				}
				gameObject.SetStringProperty("NoHateFactions", text, false);
				if (givesRep == null)
				{
					givesRep = gameObject.AddPart<GivesRep>(true, false);
				}
				givesRep.FillInRelatedFactions(true);
			}
			else if (givesRep != null)
			{
				gameObject.RemovePart(givesRep);
			}
			gameObject = HeroMaker.MakeHero(gameObject, specialTemplate, -1, this.mayorTemplate);
			gameObject.RequirePart<Interesting>(false);
			gameObject.SetStringProperty("Mayor", this.villageFaction, false);
			gameObject.SetIntProperty("VillageMayor", 1, false);
			gameObject.SetIntProperty("NamedVillager", 1, false);
			gameObject.SetIntProperty("ParticipantVillager", 1, false);
			gameObject.SetStringProperty("WaterRitual_Skill", this.signatureSkill ?? base.RollOneFrom("Village_RandomTaughtSkill"), false);
			if (this.signatureDish != null)
			{
				gameObject.AddPart<TeachesDish>(new TeachesDish(this.signatureDish, "What a savory smell! Teach me to cook the favorite dish of " + this.villageName + ".\n"), true, false);
			}
			string text2 = ((this.villageSnapshot.GetList("sacredThings").Count > 0) ? this.villageSnapshot.GetList("sacredThings").GetRandomElement<string>(null) : this.villageSnapshot.GetProperty("defaultSacredThing", "unknown"));
			string text3 = ((this.villageSnapshot.GetList("profaneThings").Count > 0) ? this.villageSnapshot.GetList("profaneThings").GetRandomElement<string>(null) : this.villageSnapshot.GetProperty("defaultProfaneThing", "unknown"));
			string text4 = HistoricStringExpander.ExpandString("<spice.villages.mayor.introDialog.!random>", null).Replace("*villageName*", this.villageName).Replace("*sacredThing*", text2)
				.Replace("*profaneThing*", text3);
			gameObject.SetIntProperty("SuppressSimpleConversation", 1, false);
			base.AddVillagerConversation(gameObject, text4, "Live and drink, =pronouns.formalAddressTerm=.", null, null, false, true);
			LandingPadsSystem.AddDynamicVillageConversation(gameObject);
			TakeOnRoleEvent.Send(gameObject, "Mayor");
			return gameObject;
		}

		// Token: 0x060070A9 RID: 28841 RVA: 0x002890D8 File Offset: 0x002872D8
		public GameObject generateMerchant(GameObject baseObject)
		{
			GameObject gameObject;
			if (!this.isVillageZero && baseObject == null && If.d100(20, null, null))
			{
				gameObject = base.getBaseVillager(true);
				base.preprocessVillager(gameObject, false);
				base.setVillagerProperties(gameObject);
				string text = (gameObject.GetBlueprint(true).DescendsFrom("Dromad") ? "SpecialVillagerHeroTemplate_DromadMerchant" : "SpecialVillagerHeroTemplate_Merchant");
				gameObject = HeroMaker.MakeHero(gameObject, text, -1, "Merchant");
			}
			else
			{
				if (baseObject != null)
				{
					gameObject = baseObject;
				}
				else if (this.isVillageZero)
				{
					gameObject = GameObjectFactory.Factory.Blueprints["DromadTrader_Village0"].createOne();
					base.preprocessVillager(gameObject, true);
				}
				else
				{
					gameObject = GameObjectFactory.Factory.Blueprints["DromadTrader" + this.villageTier.ToString()].createOne();
					base.preprocessVillager(gameObject, true);
				}
				gameObject.RemovePart<DromadCaravan>();
				gameObject.RemovePart<ConversationScript>();
				string text2 = NameMaker.MakeTitle(gameObject, null, null, null, null, null, null, null, null, null, "Merchant", null, false, null, null);
				gameObject.GiveProperName(null, false, null, false, null, null, null, null);
				if (!text2.IsNullOrEmpty())
				{
					gameObject.RequirePart<Titles>(false).AddTitle(text2, -5);
				}
				if (gameObject.GetSpecies() != "dromad")
				{
					gameObject.RequirePart<DisplayNameColor>(false).SetColorByPriority("Y", 30);
					gameObject.RequirePart<MerchantIconColor>(false);
				}
			}
			gameObject.RequirePart<Interesting>(false);
			gameObject.SetIntProperty("SuppressSimpleConversation", 1, false);
			if (gameObject.GetBlueprint(true).DescendsFrom("Dromad"))
			{
				base.AddVillagerConversation(gameObject, "Welcome, =player.species=. What do you desire?", "Live and drink.", null, null, false, true);
			}
			else
			{
				base.AddVillagerConversation(gameObject, "Come. Browse my wares, =player.formalAddressTerm=.", "Live and drink, =pronouns.formalAddressTerm=.", null, null, false, true);
			}
			if (gameObject.Brain.Allegiance.IsNullOrEmpty<KeyValuePair<string, int>>())
			{
				gameObject.Brain.Factions = this.villageFaction + "-100";
			}
			else if (!gameObject.Brain.Allegiance.ContainsKey(this.villageFaction))
			{
				gameObject.Brain.Allegiance[this.villageFaction] = 25;
			}
			GenericInventoryRestocker genericInventoryRestocker = gameObject.RequirePart<GenericInventoryRestocker>(false);
			genericInventoryRestocker.Clear();
			int num = 0;
			while (num <= 2 && this.villageTier > num)
			{
				genericInventoryRestocker.AddTable("Tier" + (this.villageTier - num).ToStringCached() + "Wares");
				num++;
			}
			genericInventoryRestocker.PerformRestock(true);
			gameObject.SetIntProperty("VillageMerchant", 1, false);
			gameObject.SetIntProperty("NamedVillager", 1, false);
			if (this.villageSnapshot.hasProperty("worships_faction") && GameObjectFactory.Factory.GetFactionMembers(this.villageSnapshot.GetProperty("worships_faction", "unknown"), false, true).Count > 0)
			{
				GameObject gameObject2 = GameObjectFactory.Factory.CreateObject(PopulationManager.RollOneFrom("Figurines " + this.villageTier.ToString(), null, null).Blueprint, 0, 0, null, null, delegate(GameObject o)
				{
					RandomFigurine randomFigurine;
					if (o.TryGetPart<RandomFigurine>(out randomFigurine))
					{
						randomFigurine.Creature = GameObjectFactory.Factory.GetFactionMembers(this.villageSnapshot.GetProperty("worships_faction", "unknown"), false, true).GetRandomElement<GameObjectBlueprint>(null).Name;
					}
				}, null, null);
				gameObject.ReceiveObject(gameObject2, false, null);
			}
			TakeOnRoleEvent.Send(gameObject, "Merchant");
			return gameObject;
		}

		// Token: 0x060070AA RID: 28842 RVA: 0x002893FC File Offset: 0x002875FC
		public GameObject generateApothecary(GameObject immigrant = null)
		{
			int num = Math.Min(Math.Max(this.villageTier, 1), 8);
			string text = "SpecialVillagerHeroTemplate_Apothecary";
			GameObject gameObject;
			if (immigrant != null || If.d100(50, null, null))
			{
				if (immigrant != null)
				{
					gameObject = immigrant;
				}
				else
				{
					gameObject = base.getBaseVillager(true);
					base.preprocessVillager(gameObject, false);
					base.setVillagerProperties(gameObject);
				}
				GameObject gameObject2;
				if (this.isVillageZero)
				{
					gameObject2 = GameObjectFactory.Factory.Blueprints["HumanApothecary_Village0"].createOne();
				}
				else
				{
					gameObject2 = GameObjectFactory.Factory.Blueprints["HumanApothecary" + num.ToString()].createOne();
				}
				foreach (BaseSkill baseSkill in gameObject2.GetPart<Skills>().SkillList)
				{
					gameObject.AddSkill(baseSkill.Name, null, null);
				}
				GenericInventoryRestocker genericInventoryRestocker = gameObject.RequirePart<GenericInventoryRestocker>(false);
				GenericInventoryRestocker part = gameObject2.GetPart<GenericInventoryRestocker>();
				genericInventoryRestocker.Table = ((part != null) ? part.Table : null) ?? "Village Apothecary 1";
				genericInventoryRestocker.Chance = 100;
				gameObject.Statistics["XP"].BaseValue = Math.Max(gameObject.Stat("XP", 0), gameObject2.Stat("XP", 0));
				gameObject.Statistics["Hitpoints"].BaseValue = Math.Max(gameObject.Stat("Hitpoints", 0), gameObject2.Stat("Hitpoints", 0));
				gameObject.Statistics["Intelligence"].BaseValue = Math.Max(gameObject.Stat("Intelligence", 0), 15);
				gameObject.Statistics["Toughness"].BaseValue = Math.Max(gameObject.Stat("Toughness", 0), 15);
			}
			else if (this.isVillageZero)
			{
				gameObject = GameObjectFactory.Factory.Blueprints["HumanApothecary_Village0"].createOne();
			}
			else
			{
				gameObject = GameObjectFactory.Factory.Blueprints["HumanApothecary" + num.ToString()].createOne();
			}
			gameObject = HeroMaker.MakeHero(gameObject, text, -1, "Apothecary");
			gameObject.RequirePart<Interesting>(false);
			gameObject.RemovePart<ConversationScript>();
			gameObject.SetIntProperty("SuppressSimpleConversation", 1, false);
			if (this.villageTier <= 3)
			{
				ConversationsAPI.addSimpleConversationToObject(gameObject, "I've the cure for what ails you.~You don't look so good. You need more yuckwheat and honey in your diet.~Cook your meals with yuckwheat if you feel sick. Catch a disease early enough and you can kill it.~\"Ease the pain, addle the brain.\" Be careful when you chew witchwood bark.", "Live and drink.", null, null, null, true, true);
			}
			else
			{
				ConversationsAPI.addSimpleConversationToObject(gameObject, "I've the cure for what ails you.~You don't look so good. You need more yuckwheat and honey in your diet.~Cook your meals with yuckwheat if you feel sick. Catch a disease early enough and you can kill it.~\"Ease the pain, addle the brain.\" Be careful when you chew witchwood bark.~In the market for a tonic, =player.formalAddressTerm=? Spend water now or blood later, your choice.~Prickly-boons and yuckwheat for trade.~If you came for the humble pie, you had best not have led any mind-hunters here.~Have you got enough tonics?", "Live and drink.", null, null, null, true, true);
			}
			if (gameObject.Brain.Allegiance.IsNullOrEmpty<KeyValuePair<string, int>>())
			{
				gameObject.Brain.Factions = this.villageFaction + "-100";
			}
			else if (!gameObject.Brain.Allegiance.ContainsKey(this.villageFaction))
			{
				gameObject.Brain.Allegiance[this.villageFaction] = 50;
			}
			gameObject.SetIntProperty("VillageApothecary", 1, false);
			gameObject.SetIntProperty("NamedVillager", 1, false);
			TakeOnRoleEvent.Send(gameObject, "Apothecary");
			return gameObject;
		}

		// Token: 0x060070AB RID: 28843 RVA: 0x0028970C File Offset: 0x0028790C
		public GameObject generateTinker(GameObject immigrant = null)
		{
			string text = "SpecialVillagerHeroTemplate_Tinker";
			GameObject gameObject;
			if (immigrant != null || If.d100(50, null, null))
			{
				if (immigrant != null)
				{
					gameObject = immigrant;
				}
				else
				{
					gameObject = base.getBaseVillager(true);
					base.preprocessVillager(gameObject, false);
					base.setVillagerProperties(gameObject);
				}
				GameObject gameObject2;
				if (this.isVillageZero)
				{
					gameObject2 = GameObjectFactory.Factory.Blueprints["HumanTinker_Village0"].createOne();
				}
				else
				{
					gameObject2 = GameObjectFactory.Factory.Blueprints["HumanTinker" + this.villageTier.ToString()].createOne();
				}
				foreach (BaseSkill baseSkill in gameObject2.GetPart<Skills>().SkillList)
				{
					gameObject.AddSkill(baseSkill.Name, null, null);
				}
				GenericInventoryRestocker genericInventoryRestocker = gameObject.RequirePart<GenericInventoryRestocker>(false);
				GenericInventoryRestocker part = gameObject2.GetPart<GenericInventoryRestocker>();
				genericInventoryRestocker.Table = ((part != null) ? part.Table : null) ?? "Village Tinker 1";
				genericInventoryRestocker.Chance = 100;
				gameObject.GetStat("XP").BaseValue = Math.Max(gameObject.GetStatValue("XP", 0), gameObject2.GetStatValue("XP", 0));
				gameObject.GetStat("Hitpoints").BaseValue = Math.Max(gameObject.GetStatValue("Hitpoints", 0), gameObject2.GetStatValue("Hitpoints", 0));
				gameObject.GetStat("Intelligence").BaseValue = Math.Max(gameObject.GetStatValue("Intelligence", 0), 16);
			}
			else if (this.isVillageZero)
			{
				gameObject = GameObjectFactory.Factory.Blueprints["HumanTinker_Village0"].createOne();
			}
			else
			{
				gameObject = GameObjectFactory.Factory.Blueprints["HumanTinker" + this.villageTier.ToString()].createOne();
			}
			gameObject = HeroMaker.MakeHero(gameObject, text, -1, "Tinker");
			gameObject.RequirePart<Interesting>(false);
			gameObject.RemovePart<ConversationScript>();
			gameObject.SetIntProperty("SuppressSimpleConversation", 1, false);
			ConversationsAPI.addSimpleConversationToObject(gameObject, "Need a gadget repaired or identified, =player.formalAddressTerm=? Or if you're a tinker =player.reflexive=, perhaps you'd like to peruse my schematics?", "Live and drink, tinker.", null, null, null, true, true);
			if (gameObject.Brain.Allegiance.IsNullOrEmpty<KeyValuePair<string, int>>())
			{
				gameObject.Brain.Factions = this.villageFaction + "-100";
			}
			else if (!gameObject.Brain.Allegiance.ContainsKey(this.villageFaction))
			{
				gameObject.Brain.Allegiance[this.villageFaction] = 50;
			}
			gameObject.SetIntProperty("VillageTinker", 1, false);
			gameObject.SetIntProperty("NamedVillager", 1, false);
			TakeOnRoleEvent.Send(gameObject, "Tinker");
			return gameObject;
		}

		// Token: 0x060070AC RID: 28844 RVA: 0x002899B8 File Offset: 0x00287BB8
		public GameObject generateImmigrant(string type, string name, string gender, string role, string whyQ, string whyA)
		{
			GameObject gameObject;
			if (type == null)
			{
				gameObject = GameObject.Create(PopulationManager.RollOneFrom("DynamicInheritsTable:Creature:Tier" + this.villageTier.ToString(), null, null).Blueprint);
			}
			else
			{
				gameObject = GameObject.Create(type);
			}
			base.preprocessVillager(gameObject, true);
			gameObject.SetStringProperty("HeroNameColor", "&Y", false);
			base.setVillagerProperties(gameObject);
			if (role == "mayor")
			{
				gameObject = this.generateMayor(gameObject, "SpecialVillagerHeroTemplate_" + this.mayorTemplate, true);
			}
			else if (role == "warden")
			{
				gameObject = this.generateWarden(gameObject, this.isVillageZero);
			}
			else if (role == "merchant")
			{
				gameObject = this.generateMerchant(gameObject);
			}
			else if (role == "tinker")
			{
				gameObject = this.generateTinker(gameObject);
			}
			else if (role == "apothecary")
			{
				gameObject = this.generateApothecary(gameObject);
			}
			else
			{
				gameObject = HeroMaker.MakeHero(gameObject, null, null, -1, "Hero");
			}
			gameObject.RequirePart<Interesting>(false);
			gameObject.SetIntProperty("NamedVillager", 1, false);
			gameObject.SetIntProperty("ParticipantVillager", 1, false);
			gameObject.Render.DisplayName = name;
			if (!gender.IsNullOrEmpty())
			{
				gameObject.SetGender(gender);
			}
			if (role != "mayor" && role != "warden")
			{
				gameObject.RemovePart<GivesRep>();
			}
			if (role == "villager")
			{
				gameObject.SetIntProperty("SuppressSimpleConversation", 1, false);
			}
			base.AddVillagerConversation(gameObject, gameObject.GetTag("SimpleConversation", "Moon and Sun. Wisdom and will.~May the earth yield for us this season.~Peace, =player.formalAddressTerm=."), "Live and drink.", whyQ, whyA, role != "villager", true);
			return gameObject;
		}

		// Token: 0x060070AD RID: 28845 RVA: 0x00289B64 File Offset: 0x00287D64
		public GameObject generatePet(string species, out string name)
		{
			GameObject gameObject;
			if (species == null)
			{
				gameObject = GameObject.Create(PopulationManager.RollOneFrom("DynamicInheritsTable:BaseAnimal:Tier" + this.villageTier.ToString(), null, null).Blueprint);
			}
			else
			{
				gameObject = GameObject.Create(species);
			}
			string text = NameMaker.MakeTitle(gameObject, null, null, null, null, null, null, null, null, null, null, null, false, null, null);
			name = gameObject.GiveProperName(null, false, null, false, null, null, null, null);
			if (!text.IsNullOrEmpty())
			{
				gameObject.RequirePart<Titles>(false).AddTitle(text, -5);
			}
			base.setVillagerProperties(gameObject);
			gameObject.RequirePart<SmartuseForceTwiddles>(false);
			gameObject.RemovePart<Pettable>();
			Pettable pettable = new Pettable();
			gameObject.AddPart<Pettable>(pettable, true, false);
			pettable.PettableIfPositiveFeeling = true;
			pettable.UseFactionForFeelingFloor = this.villageFaction;
			gameObject.SetIntProperty("VillagePet", 1, false);
			gameObject.RequirePart<Interesting>(false).Key = "VillagePet";
			ConversationsAPI.addSimpleConversationToObject(gameObject, gameObject.GetTag("SimpleConversation", "*does not react*"), "Live and drink.", null, null, null, false, true);
			return gameObject;
		}

		// Token: 0x060070AE RID: 28846 RVA: 0x00289C7C File Offset: 0x00287E7C
		public List<PopulationResult> ResolveBuildingContents(List<PopulationResult> templateResults)
		{
			List<PopulationResult> list = new List<PopulationResult>(templateResults.Count);
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			foreach (PopulationResult populationResult in templateResults)
			{
				for (int i = 0; i < populationResult.Number; i++)
				{
					if (!populationResult.Blueprint.StartsWith("*"))
					{
						list.Add(new PopulationResult(populationResult.Blueprint, 1, null, null));
					}
					else if (this.staticVillageResults.ContainsKey(populationResult.Blueprint) && !Stat.Chance(20))
					{
						list.Add(new PopulationResult(this.staticVillageResults[populationResult.Blueprint], 1, null, null));
					}
					else if (dictionary.ContainsKey(populationResult.Blueprint) && !Stat.Chance(20))
					{
						list.Add(new PopulationResult(dictionary[populationResult.Blueprint], 1, null, null));
					}
					else
					{
						PopulationResult populationResult2 = new PopulationResult(null, 1, null, null);
						string text = "DynamicSemanticTable:" + populationResult.Blueprint.Replace("*", "") + "::" + this.villageTechTier.ToString();
						populationResult2.Blueprint = PopulationManager.RollOneFrom(text, null, null).Blueprint;
						populationResult2.Hint = populationResult.Hint;
						if (populationResult2.Blueprint.IsNullOrEmpty())
						{
							Debug.LogError("Couldn't resolve object for " + populationResult.Blueprint);
						}
						else
						{
							list.Add(populationResult2);
							if (this.staticPerBuilding.Contains(populationResult.Blueprint) && !dictionary.ContainsKey(populationResult.Blueprint))
							{
								dictionary.Add(populationResult.Blueprint, populationResult2.Blueprint);
							}
							if (this.staticPerVillage.Contains(populationResult.Blueprint) && !this.staticVillageResults.ContainsKey(populationResult.Blueprint))
							{
								this.staticVillageResults.Add(populationResult.Blueprint, populationResult2.Blueprint);
							}
						}
					}
				}
			}
			return list;
		}

		// Token: 0x060070AF RID: 28847 RVA: 0x00289EA4 File Offset: 0x002880A4
		public override void addInitialStructures()
		{
			List<ISultanDungeonSegment> list = new List<ISultanDungeonSegment>();
			int num = 7;
			int num2 = 72;
			int num3 = 7;
			int num4 = 17;
			string blueprint = PopulationManager.RollOneFrom(base.ResolvePopulationTableName("Village_InitialStructureSegmentation"), null, "Full").Blueprint;
			if (blueprint == "None")
			{
				return;
			}
			foreach (string text in blueprint.Split(';', StringSplitOptions.None))
			{
				if (text == "FullHMirror")
				{
					list.Add(new SultanRectDungeonSegment(new Rect2D(2, 2, 78, 22))
					{
						mutator = "HMirror"
					});
				}
				else if (text == "FullVMirror")
				{
					list.Add(new SultanRectDungeonSegment(new Rect2D(2, 2, 78, 22))
					{
						mutator = "VMirror"
					});
				}
				else if (text == "FullHVMirror")
				{
					list.Add(new SultanRectDungeonSegment(new Rect2D(2, 2, 78, 22))
					{
						mutator = "HVMirror"
					});
				}
				else if (text == "Full")
				{
					list.Add(new SultanRectDungeonSegment(new Rect2D(2, 2, 78, 22)));
				}
				else if (text.StartsWith("BSP:"))
				{
					int num5 = Convert.ToInt32(text.Split(':', StringSplitOptions.None)[1]);
					base.partition(new Rect2D(2, 2, 78, 24), ref num5, list);
				}
				else if (text.StartsWith("Ring:"))
				{
					int num6 = Convert.ToInt32(text.Split(':', StringSplitOptions.None)[1]);
					list.Add(new SultanRectDungeonSegment(new Rect2D(2, 2, 78, 22)));
					if (num6 == 2)
					{
						list.Add(new SultanRectDungeonSegment(new Rect2D(20, 8, 60, 16)));
					}
					if (num6 == 3)
					{
						list.Add(new SultanRectDungeonSegment(new Rect2D(15, 8, 65, 16)));
						list.Add(new SultanRectDungeonSegment(new Rect2D(25, 10, 55, 14)));
					}
				}
				else if (text.StartsWith("Blocks"))
				{
					string[] array2 = text.Split(':', StringSplitOptions.None)[1].Split(',', StringSplitOptions.None);
					int num7 = array2[0].RollCached();
					for (int j = 0; j < num7; j++)
					{
						int num8 = array2[1].RollCached();
						int num9 = array2[2].RollCached();
						int num10 = Stat.Random(2, 78 - num8);
						int num11 = Stat.Random(2, 23 - num9);
						int num12 = num10 + num8;
						int num13 = num11 + num9;
						if (num < num10)
						{
							num = num10;
						}
						if (num2 > num12)
						{
							num2 = num12;
						}
						if (num3 < num11)
						{
							num3 = num11;
						}
						if (num4 > num13)
						{
							num4 = num13;
						}
						SultanRectDungeonSegment sultanRectDungeonSegment = new SultanRectDungeonSegment(new Rect2D(num10, num11, num10 + num8, num11 + num9));
						if (text.Contains("[HMirror]"))
						{
							sultanRectDungeonSegment.mutator = "HMirror";
						}
						if (text.Contains("[VMirror]"))
						{
							sultanRectDungeonSegment.mutator = "VMirror";
						}
						if (text.Contains("[HVMirror]"))
						{
							sultanRectDungeonSegment.mutator = "HVMirror";
						}
						list.Add(sultanRectDungeonSegment);
					}
				}
				else if (text.StartsWith("Circle"))
				{
					string[] array3 = text.Split(':', StringSplitOptions.None)[1].Split(',', StringSplitOptions.None);
					list.Add(new SultanCircleDungeonSegment(Location2D.Get(array3[0].RollCached(), array3[1].RollCached()), array3[2].RollCached()));
				}
				else if (text.StartsWith("Tower"))
				{
					string[] array4 = text.Split(':', StringSplitOptions.None)[1].Split(',', StringSplitOptions.None);
					list.Add(new SultanTowerDungeonSegment(Location2D.Get(array4[0].RollCached(), array4[1].RollCached()), array4[2].RollCached(), array4[3].RollCached()));
				}
			}
			ColorOutputMap colorOutputMap = new ColorOutputMap(80, 25);
			for (int k = 0; k < list.Count; k++)
			{
				string text2 = PopulationManager.RollOneFrom(base.ResolvePopulationTableName("Village_StructureTemplate"), null, null).Blueprint;
				int num14 = 3;
				string text3 = "";
				string blueprint2 = PopulationManager.RollOneFrom(base.ResolvePopulationTableName("Village_StructureTemplate"), null, null).Blueprint;
				int num15 = 3;
				if (text2.Contains(","))
				{
					string[] array5 = text2.Split(',', StringSplitOptions.None);
					text2 = array5[0];
					text3 = array5[1];
				}
				WaveCollapseFastModel waveCollapseFastModel = new WaveCollapseFastModel(text2, num14, list[k].width(), list[k].height(), true, false, 8, 0);
				waveCollapseFastModel.Run(Stat.Random(int.MinValue, 2147483646), 0);
				if (!text3.IsNullOrEmpty())
				{
					waveCollapseFastModel.ClearColors(text3, "border1");
				}
				waveCollapseFastModel.UpdateSample(blueprint2.Split(',', StringSplitOptions.None)[0], num15, true, false, 8, 0);
				waveCollapseFastModel.Run(Stat.Random(int.MinValue, 2147483646), 0);
				ColorOutputMap colorOutputMap2 = new ColorOutputMap(waveCollapseFastModel);
				colorOutputMap2.ReplaceBorders(new Color32(byte.MaxValue, 0, 0, byte.MaxValue), new Color32(0, 0, 0, byte.MaxValue));
				if (list[k].mutator == "HMirror")
				{
					colorOutputMap2.HMirror();
				}
				if (list[k].mutator == "VMirror")
				{
					colorOutputMap2.VMirror();
				}
				if (list[k].mutator == "HVMirror")
				{
					colorOutputMap2.HMirror();
					colorOutputMap2.VMirror();
				}
				colorOutputMap.Paste(colorOutputMap2, list[k].x1, list[k].y1);
				MemoryHelper.GCCollect();
			}
			string text4 = base.RollOneFrom("Village_InitialStructureSegmentationFullscreenMutation");
			int num16 = 0;
			int num17 = 0;
			for (int l = 0; l < list.Count; l++)
			{
				string text5 = PopulationManager.RollOneFrom(base.ResolvePopulationTableName("Village_StructureWall"), null, null).Blueprint;
				if (text5 == "*auto")
				{
					text5 = base.GetDefaultWall(this.zone);
				}
				for (int m = list[l].y1; m < list[l].y2; m++)
				{
					for (int n = list[l].x1; n < list[l].x2; n++)
					{
						if (list[l].contains(n, m))
						{
							for (int num18 = l + 1; num18 < list.Count; num18++)
							{
								if (list[num18].contains(n, m))
								{
									goto IL_0847;
								}
							}
							Color32 color = colorOutputMap.getPixel(n, m);
							if (list[l].HasCustomColor(n, m))
							{
								color = list[l].GetCustomColor(n, m);
							}
							if (WaveCollapseTools.equals(color, ColorOutputMap.BLACK))
							{
								this.zone.GetCell(n + num16, m + num17).ClearWalls();
								this.zone.GetCell(n + num16, m + num17).AddObject(text5, null, null, null, null);
								if (text4 == "VMirror" || text4 == "HVMirror")
								{
									this.zone.GetCell(n + num16, this.zone.Height - (m + num17) - 1).ClearWalls();
									this.zone.GetCell(n + num16, this.zone.Height - (m + num17) - 1).AddObject(text5, null, null, null, null);
								}
								if (text4 == "HMirror" || text4 == "HVMirror")
								{
									this.zone.GetCell(this.zone.Width - (n + num16) - 1, m + num17).ClearWalls();
									this.zone.GetCell(this.zone.Width - (n + num16) - 1, m + num17).AddObject(text5, null, null, null, null);
								}
								if (text4 == "HVMirror")
								{
									this.zone.GetCell(this.zone.Width - (n + num16) - 1, this.zone.Height - (m + num17) - 1).ClearWalls();
									this.zone.GetCell(this.zone.Width - (n + num16) - 1, this.zone.Height - (m + num17) - 1).AddObject(text5, null, null, null, null);
								}
							}
						}
						IL_0847:;
					}
				}
			}
		}

		// Token: 0x060070B0 RID: 28848 RVA: 0x0028A740 File Offset: 0x00288940
		public static void villageClear(Zone Z)
		{
			string tag = Z.GetTerrainObject().GetTag("VillageClearBehavior", null);
			if (!tag.IsNullOrEmpty())
			{
				string[] array = tag.Split(':', StringSplitOptions.None);
				if (array[0] == "circles")
				{
					int num = int.Parse(array[1]);
					for (int i = 0; i < num; i++)
					{
						foreach (Cell cell in Z.GetRandomCell(0).GetCellsInACosmeticCircle(Stat.Random(6, 10)))
						{
							Debug.Log("clearing " + cell.X.ToString() + "," + cell.Y.ToString());
							cell.Clear(null, false, false, (GameObject o) => o.GetBlueprint(true).DescendsFrom("Widget"));
						}
					}
				}
			}
		}

		// Token: 0x060070B1 RID: 28849 RVA: 0x0028A844 File Offset: 0x00288A44
		public bool BuildZone(Zone Z)
		{
			bool flag = false;
			this.zone = Z;
			this.zone.SetZoneProperty("relaxedbiomes", "true");
			this.zone.SetZoneProperty("faction", this.villageFaction);
			this.villageSnapshot = base.villageEntity.GetCurrentSnapshot();
			this.region = this.villageSnapshot.GetProperty("region", "unknown");
			this.villagerBaseFaction = this.villageSnapshot.GetProperty("baseFaction", "unknown");
			this.villageName = this.villageSnapshot.GetProperty("name", "unknown");
			this.dynamicCreatureTableName = "DynamicObjectsTable:" + this.region + "_Creatures";
			Z.SetZoneProperty("villageEntityId", base.villageEntity.id);
			this.isVillageZero = this.villageSnapshot.GetProperty("isVillageZero", "false").EqualsNoCase("true");
			Tier.Constrain(ref this.villageTier);
			Tier.Constrain(ref this.villageTechTier);
			base.generateVillageTheme();
			base.generateSignatureItems();
			base.generateSignatureDish("Focal");
			base.generateSignatureLiquid();
			base.generateSignatureSkill();
			this.generateStoryType();
			base.getVillageDoorStyle();
			base.makeSureThereIsEnoughSpace();
			foreach (Cell cell in Z.GetCells())
			{
				for (int i = cell.Objects.Count - 1; i >= 0; i--)
				{
					GameObject gameObject = cell.Objects[i];
					if (!gameObject.IsPlayer() && !gameObject.HasTagOrProperty("NoVillageStrip"))
					{
						if (gameObject.HasTagOrProperty("RequireVillagePlacement"))
						{
							gameObject.Physics.CurrentCell = null;
							this.requiredPlacementObjects.Add(gameObject);
						}
						else if (gameObject.IsCombat || gameObject.HasTagOrProperty("BodySubstitute"))
						{
							gameObject.Physics.CurrentCell = null;
							this.originalCreatures.Add(gameObject);
						}
						else if (gameObject.IsWall() && gameObject.HasTag("Category_Settlement"))
						{
							gameObject.Physics.CurrentCell = null;
							this.originalWalls.Add(gameObject);
						}
						else if (gameObject.GetBlueprint(true).InheritsFrom("Plant") || gameObject.GetBlueprint(true).InheritsFrom("BasePlant") || gameObject.GetBlueprint(true).HasTag("PlantLike", false))
						{
							gameObject.Physics.CurrentCell = null;
							if (gameObject != null)
							{
								this.originalPlants.Add(gameObject);
							}
						}
						else if (gameObject.HasPart<LiquidVolume>())
						{
							gameObject.Physics.CurrentCell = null;
							if (gameObject.IsOpenLiquidVolume())
							{
								this.originalLiquids.Add(gameObject);
							}
						}
						else if (gameObject.GetBlueprint(true).InheritsFrom("Furniture"))
						{
							gameObject.Physics.CurrentCell = null;
							this.originalFurniture.Add(gameObject);
						}
						else if (gameObject.GetBlueprint(true).InheritsFrom("Item"))
						{
							gameObject.Physics.CurrentCell = null;
							this.originalItems.Add(gameObject);
						}
					}
				}
			}
			Village.villageClear(Z);
			this.addInitialStructures();
			InfluenceMap regionMap = new InfluenceMap(Z.Width, Z.Height);
			for (int j = 0; j < Z.Width; j++)
			{
				for (int k = 0; k < Z.Height; k++)
				{
					regionMap.Walls[j, k] = (Z.GetCell(j, k).HasObjectWithTagOrProperty("Wall") ? 1 : 0);
				}
			}
			try
			{
				regionMap.SeedAllUnseeded(false, true);
				goto IL_0400;
			}
			catch (Exception)
			{
				base.makeSureThereIsEnoughSpace();
				regionMap.SeedAllUnseeded(false, true);
				goto IL_0400;
			}
			IL_03F4:
			regionMap.AddSeedAtMaximaInLargestSeed(false);
			IL_0400:
			if (regionMap.LargestSize() <= 150)
			{
				regionMap.SeedGrowthProbability = new List<int>();
				for (int l2 = 0; l2 < regionMap.Seeds.Count; l2++)
				{
					regionMap.SeedGrowthProbability.Add(Stat.Random(10, 1000));
				}
				regionMap.Recalculate(flag);
				int num = Stat.Random(4, 9);
				int num2 = 0;
				int num3 = regionMap.FindClosestSeedTo(Location2D.Get(40, 13), (InfluenceMapRegion r) => r.maxRect.ReduceBy(1, 1).Width >= 6 && r.maxRect.ReduceBy(1, 1).Height >= 6 && r.AdjacentRegions.Count > 1);
				Location2D location2D = regionMap.Seeds[num3];
				this.townSquare = regionMap.Regions[num3];
				this.townSquareLayout = null;
				foreach (InfluenceMapRegion influenceMapRegion in regionMap.Regions)
				{
					Rect2D Rect = GridTools.MaxRectByArea(influenceMapRegion.GetGrid(), false, 0, 0).Translate(influenceMapRegion.BoundingBox.UpperLeft).ReduceBy(1, 1);
					PopulationLayout populationLayout = new PopulationLayout(Z, influenceMapRegion, Rect);
					if (influenceMapRegion.AdjacentRegions.Count <= 1 && influenceMapRegion.Size >= 9 && !influenceMapRegion.IsEdgeRegion() && influenceMapRegion != this.townSquare)
					{
						this.buildings.Add(populationLayout);
					}
					else if ((Rect.Width >= 5 && Rect.Height >= 5 && num > 0) || influenceMapRegion == this.townSquare)
					{
						List<GameObject> originalLiquids = this.originalLiquids;
						string text;
						if (originalLiquids == null)
						{
							text = null;
						}
						else
						{
							GameObject randomElement = originalLiquids.GetRandomElement<GameObject>(null);
							text = ((randomElement != null) ? randomElement.Blueprint : null);
						}
						string text2 = text ?? base.getZoneDefaultLiquid(this.zone);
						if (influenceMapRegion == this.townSquare)
						{
							this.townSquareLayout = populationLayout;
							if (base.fabricateStoryBuilding())
							{
								this.buildings.Add(populationLayout);
							}
						}
						else
						{
							string text3 = PopulationManager.RollOneFrom(base.ResolvePopulationTableName("Villages_BuildingStyle"), null, null).Blueprint;
							if (text3.StartsWith("wfc,") && !base.getWfcBuildingTemplate(text3.Split(',', StringSplitOptions.None)[1]).Any<ColorOutputMap>((ColorOutputMap t) => t.extrawidth <= Rect.Width && t.extraheight <= Rect.Height))
							{
								text3 = "squarehut";
							}
							this.buildings.Add(populationLayout);
							if (text3 == "burrow")
							{
								base.FabricateBurrow(populationLayout);
								populationLayout.hasStructure = true;
							}
							if (text3 == "aerie")
							{
								base.FabricateAerie(populationLayout);
							}
							if (text3 == "pond")
							{
								base.FabricatePond(populationLayout, text2);
							}
							if (text3 == "islandpond")
							{
								base.FabricateIslandPond(populationLayout, text2);
								populationLayout.hasStructure = true;
							}
							if (text3 == "walledpond")
							{
								base.FabricateWalledPond(populationLayout, text2);
								populationLayout.hasStructure = true;
							}
							if (text3 == "walledislandpond")
							{
								base.FabricateWalledIslandPond(populationLayout, text2);
								populationLayout.hasStructure = true;
							}
							if (text3 == "tent")
							{
								base.FabricateTent(populationLayout);
								populationLayout.hasStructure = true;
							}
							if (text3 == "roundhut")
							{
								base.FabricateHut(populationLayout, true);
								populationLayout.hasStructure = true;
							}
							if (text3 == "squarehut")
							{
								base.FabricateHut(populationLayout, false);
								populationLayout.hasStructure = true;
							}
							if (text3.StartsWith("wfc,"))
							{
								base.getWfcBuildingTemplate(text3.Split(',', StringSplitOptions.None)[1]).ShuffleInPlace<ColorOutputMap>(null);
								bool flag2 = false;
								foreach (ColorOutputMap colorOutputMap in base.getWfcBuildingTemplate(text3.Split(',', StringSplitOptions.None)[1]))
								{
									int num4 = colorOutputMap.width / 2;
									int num5 = colorOutputMap.height / 2;
									if (colorOutputMap.extrawidth <= populationLayout.innerRect.Width && colorOutputMap.extraheight <= populationLayout.innerRect.Height)
									{
										for (int m = 0; m < colorOutputMap.width; m++)
										{
											for (int n = 0; n < colorOutputMap.height; n++)
											{
												Cell cell2 = Z.GetCell(populationLayout.position.X - num4 + m, populationLayout.position.Y - num5 + n);
												if (cell2 != null)
												{
													if (colorOutputMap.getPixel(m, n).Equals(ColorOutputMap.BLACK))
													{
														cell2.AddObject(base.getAVillageWall(), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
													}
													else
													{
														colorOutputMap.getPixel(m, n).Equals(ColorOutputMap.RED);
													}
												}
											}
										}
										populationLayout.hasStructure = true;
										flag2 = true;
										break;
									}
								}
								if (!flag2)
								{
									base.FabricateHut(populationLayout, false);
									populationLayout.hasStructure = true;
								}
							}
							num--;
							num2++;
						}
					}
					else if (influenceMapRegion.AdjacentRegions.Count == 1 && !influenceMapRegion.IsEdgeRegion() && this.townSquare != influenceMapRegion)
					{
						VillageBase.MakeCaveBuilding(Z, regionMap, influenceMapRegion, "Shale");
						this.buildings.Add(populationLayout);
						populationLayout.hasStructure = true;
					}
				}
				base.placeStatues();
				regionMap.SeedAllUnseeded(flag, true);
				base.CarvePathwaysFromLocations(Z, true, regionMap, location2D);
				this.zone.ClearReachableMap(false);
				this.zone.BuildReachableMap(location2D.X, location2D.Y);
				base.SnakeToConnections(Location2D.Get(location2D.X, location2D.Y));
				base.clearDegenerateDoors();
				base.applyDoorFilters();
				for (int num6 = 0; num6 < Z.Width; num6++)
				{
					for (int num7 = 0; num7 < Z.Height; num7++)
					{
						regionMap.Walls[num6, num7] = (Z.GetCell(num6, num7).HasObjectWithTag("Wall") ? 1 : 0);
					}
				}
				List<Location2D> list = new List<Location2D>();
				foreach (PopulationLayout populationLayout2 in this.buildings)
				{
					Location2D position = populationLayout2.position;
					if (position != null)
					{
						list.Add(position);
					}
				}
				regionMap.Recalculate(flag);
				InfluenceMap influenceMap = regionMap.copy();
				using (Pathfinder pathfinder = this.zone.getPathfinder(null))
				{
					NoiseMap noiseMap = new NoiseMap(80, 25, 10, 3, 3, 4, 80, 80, 6, 3, -3, 1, new List<NoiseMapNode>());
					for (int num8 = 0; num8 < this.zone.Width; num8++)
					{
						for (int num9 = 0; num9 < this.zone.Height; num9++)
						{
							if (this.zone.GetCell(num8, num9).HasWall())
							{
								pathfinder.CurrentNavigationMap[num8, num9] = 4999;
							}
							else
							{
								pathfinder.CurrentNavigationMap[num8, num9] = noiseMap.Noise[num8, num9];
							}
						}
					}
					foreach (PopulationLayout populationLayout3 in this.buildings)
					{
						foreach (Location2D location2D2 in populationLayout3.originalRegion.Cells)
						{
							int x3 = location2D2.X;
							int y = location2D2.Y;
							if (x3 != 0 && x3 != 79 && y != 0 && y != 24 && Z.GetCell(x3, y).IsEmpty())
							{
								int num10 = 0;
								int num11 = 0;
								if (Z.GetCell(x3 - 1, y).HasWall() || Z.GetCell(x3 - 1, y).HasObjectWithTag("Door"))
								{
									num11++;
								}
								if (Z.GetCell(x3 + 1, y).HasWall() || Z.GetCell(x3 + 1, y).HasObjectWithTag("Door"))
								{
									num11++;
								}
								if (Z.GetCell(x3, y - 1).HasWall() || Z.GetCell(x3, y - 1).HasObjectWithTag("Door"))
								{
									num10++;
								}
								if (Z.GetCell(x3, y + 1).HasWall() || Z.GetCell(x3, y + 1).HasObjectWithTag("Door"))
								{
									num10++;
								}
								if ((num10 == 2 && num11 == 0) || (num10 == 0 && num11 == 2))
								{
									influenceMap.Walls[x3, y] = 1;
								}
							}
						}
					}
					for (int num12 = 0; num12 < 80; num12++)
					{
						for (int num13 = 0; num13 < 25; num13++)
						{
							if (this.burrowedDoors.Contains(Location2D.Get(num12, num13)))
							{
								influenceMap.Walls[num12, num13] = 1;
							}
						}
					}
					influenceMap.Recalculate(flag);
					foreach (PopulationLayout populationLayout4 in this.buildings)
					{
						string blueprint = PopulationManager.RollOneFrom(base.ResolvePopulationTableName("Villages_BuildingFloor"), null, null).Blueprint;
						string text4 = PopulationManager.RollOneFrom(base.ResolvePopulationTableName("Villages_BuildingPath"), null, null).Blueprint;
						if (text4 == "Pond")
						{
							text4 = base.getZoneDefaultLiquid(this.zone);
						}
						if (pathfinder.FindPath(populationLayout4.position, location2D, false, true, 9999, false))
						{
							foreach (PathfinderNode pathfinderNode in pathfinder.Steps)
							{
								if (!text4.IsNullOrEmpty())
								{
									this.zone.GetCell(pathfinderNode.pos).AddObject(text4, null, null, null, null);
								}
								if (!this.buildingPaths.Contains(pathfinderNode.pos))
								{
									this.buildingPaths.Add(pathfinderNode.pos);
								}
							}
						}
						foreach (Location2D location2D3 in populationLayout4.originalRegion.Cells)
						{
							if (!Z.GetCell(location2D3).HasWall() && !this.buildingPaths.Contains(location2D3))
							{
								if (influenceMap.Regions.Count<InfluenceMapRegion>() <= populationLayout4.region.Seed)
								{
									populationLayout4.outside.Add(location2D3);
									int num14 = Z.GetCell(location2D3).CountObjectWithTagCardinalAdjacent("Wall");
									if (num14 > 0)
									{
										populationLayout4.outsideWall.Add(location2D3);
									}
									if (num14 >= 2)
									{
										populationLayout4.outsideCorner.Add(location2D3);
									}
								}
								else if (!influenceMap.Regions[populationLayout4.region.Seed].Cells.Contains(location2D3))
								{
									populationLayout4.outside.Add(location2D3);
									int num15 = Z.GetCell(location2D3).CountObjectWithTagCardinalAdjacent("Wall");
									if (num15 > 0)
									{
										populationLayout4.outsideWall.Add(location2D3);
									}
									if (num15 >= 2)
									{
										populationLayout4.outsideCorner.Add(location2D3);
									}
								}
								else
								{
									populationLayout4.inside.Add(location2D3);
									if (!blueprint.IsNullOrEmpty())
									{
										Z.GetCell(location2D3).AddObject(blueprint, null, null, null, null);
									}
									int num16 = Z.GetCell(location2D3).CountObjectWithTagCardinalAdjacent("Wall");
									if (num16 > 0)
									{
										populationLayout4.insideWall.Add(location2D3);
									}
									if (num16 >= 2)
									{
										populationLayout4.insideCorner.Add(location2D3);
									}
								}
							}
						}
					}
				}
				Dictionary<InfluenceMapRegion, Rect2D> dictionary = new Dictionary<InfluenceMapRegion, Rect2D>();
				Dictionary<InfluenceMapRegion, string> dictionary2 = new Dictionary<InfluenceMapRegion, string>();
				InfluenceMap influenceMap2 = new InfluenceMap(Z.Width, Z.Height);
				influenceMap2.Seeds = new List<Location2D>(regionMap.Seeds);
				Z.SetInfluenceMapWalls(influenceMap2.Walls);
				influenceMap2.Recalculate(false);
				int num17 = 0;
				for (int num18 = 0; num18 < influenceMap2.Regions.Count; num18++)
				{
					InfluenceMapRegion R = influenceMap2.Regions[num18];
					Rect2D rect2D;
					if (!dictionary.ContainsKey(R))
					{
						rect2D = GridTools.MaxRectByArea(R.GetGrid(), false, 0, 0).Translate(R.BoundingBox.UpperLeft);
						dictionary.Add(R, rect2D);
					}
					else
					{
						rect2D = dictionary[R];
					}
					if (num18 != num3)
					{
						if (list.Contains(regionMap.Seeds[R.Seed]))
						{
							dictionary2.Add(R, "building");
							PopulationLayout populationLayout5 = this.buildings.First<PopulationLayout>((PopulationLayout b) => b.position == regionMap.Seeds[R.Seed]);
							string text5 = base.RollOneFrom("Villages_BuildingTheme_" + this.villageTheme);
							using (List<PopulationResult>.Enumerator enumerator7 = this.ResolveBuildingContents(PopulationManager.Generate(base.ResolvePopulationTableName("Villages_BuildingContents_Dwelling_" + text5), null, null, null, null, null)).GetEnumerator())
							{
								while (enumerator7.MoveNext())
								{
									PopulationResult populationResult = enumerator7.Current;
									base.PlaceObjectInBuilding(populationResult, populationLayout5, null);
								}
								goto IL_1CF1;
							}
						}
						if (rect2D.Area >= 4)
						{
							dictionary2.Add(R, "greenspace");
							if (num17 == 0 && this.signatureHistoricObjectInstance != null)
							{
								string text6 = "IronFence";
								string text7 = "Iron Gate";
								Z.GetCell(rect2D.Center).AddObject(this.signatureHistoricObjectInstance, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
								ZoneBuilderSandbox.encloseRectWithWall(this.zone, new Rect2D(rect2D.Center.x - 1, rect2D.Center.y - 1, rect2D.Center.x + 1, rect2D.Center.y + 1), text6);
								Z.GetCell(rect2D.Center).GetCellFromDirection(Directions.GetRandomCardinalDirection(), true).Clear(null, false, false, null)
									.AddObject(text7, null, null, null, null);
							}
							else
							{
								string blueprint2 = PopulationManager.RollOneFrom(base.ResolvePopulationTableName("Villages_GreenspaceContents"), null, null).Blueprint;
								int num19 = 20;
								if (blueprint2 == "aquaculture")
								{
									List<GameObject> originalLiquids2 = this.originalLiquids;
									string text8;
									if (originalLiquids2 == null)
									{
										text8 = null;
									}
									else
									{
										GameObject randomElement2 = originalLiquids2.GetRandomElement<GameObject>(null);
										text8 = ((randomElement2 != null) ? randomElement2.Blueprint : null);
									}
									string text9 = text8 ?? base.getZoneDefaultLiquid(this.zone);
									GameObject afarmablePlant = base.getAFarmablePlant();
									Maze maze = RecursiveBacktrackerMaze.Generate(Math.Max(1, R.BoundingBox.Width / 3 + 1), Math.Max(1, R.BoundingBox.Height / 3 + 1), false, ZoneBuilderSandbox.GetOracleIntFromString("aquaculture" + num17.ToString(), 0, 2147483646));
									for (int num20 = R.BoundingBox.x1; num20 <= R.BoundingBox.x2; num20++)
									{
										for (int num21 = R.BoundingBox.y1; num21 <= R.BoundingBox.y2; num21++)
										{
											int num22 = (num20 - R.BoundingBox.x1) / 3;
											int num23 = (num21 - R.BoundingBox.y1) / 3;
											int num24 = (num20 - R.BoundingBox.x1) % 3;
											int num25 = (num21 - R.BoundingBox.y1) % 3;
											bool flag3 = false;
											if (num24 == 1 && num25 == 1)
											{
												flag3 = maze.Cell[num22, num23].AnyOpen();
											}
											if (num24 == 1 && num25 == 0)
											{
												flag3 = maze.Cell[num22, num23].N;
											}
											if (num24 == 1 && num25 == 2)
											{
												flag3 = maze.Cell[num22, num23].S;
											}
											if (num24 == 2 && num25 == 1)
											{
												flag3 = maze.Cell[num22, num23].E;
											}
											if (num24 == 0 && num25 == 1)
											{
												flag3 = maze.Cell[num22, num23].W;
											}
											if (flag3)
											{
												if (R.Cells.Contains(Location2D.Get(num20, num21)) && !this.buildingPaths.Contains(Location2D.Get(num20, num21)))
												{
													Cell cell3 = Z.GetCell(num20, num21);
													if (cell3 != null)
													{
														cell3.AddObject(afarmablePlant.Blueprint, new Action<GameObject>(base.setVillageDomesticatedProperties), null);
													}
												}
											}
											else if (R.Cells.Contains(Location2D.Get(num20, num21)) && Z.GetCell(num20, num21) != null)
											{
												Z.GetCell(num20, num21).AddObject(text9, null, null, null, null);
											}
										}
									}
								}
								else if (blueprint2 == "farm" && rect2D.Area >= num19 && rect2D.Width >= 7 && rect2D.Height <= 7)
								{
									rect2D = rect2D.ReduceBy(1, 1).Clamp(1, 1, 78, 23);
									if (rect2D.Width <= 6 || rect2D.Height <= 6)
									{
										goto IL_1CF1;
									}
									Location2D location = rect2D.GetRandomDoorCell("?", 2).location;
									ZoneBuilderSandbox.PlaceObjectOnRect(Z, "BrinestalkFence", rect2D, false);
									base.GetCell(Z, location).Clear(null, false, false, null);
									base.GetCell(Z, location).AddObject("Brinestalk Gate", null, null, null, null);
									string cellSide = rect2D.GetCellSide(location.Point);
									Rect2D rect2D2 = rect2D.ReduceBy(0, 0);
									int num26 = 0;
									if (cellSide == "N")
									{
										num26 = ((Stat.Random(0, 1) == 0) ? 2 : 3);
									}
									if (cellSide == "S")
									{
										num26 = ((Stat.Random(0, 1) == 0) ? 0 : 1);
									}
									if (cellSide == "E")
									{
										num26 = ((Stat.Random(0, 1) == 0) ? 0 : 3);
									}
									if (cellSide == "W")
									{
										num26 = ((Stat.Random(0, 1) == 0) ? 1 : 2);
									}
									if (num26 == 0 || num26 == 1)
									{
										rect2D2.y2 = rect2D2.y1 + 3;
									}
									else
									{
										rect2D2.y1 = rect2D2.y2 - 3;
									}
									if (num26 == 0 || num26 == 3)
									{
										rect2D2.x2 = rect2D2.x1 + 3;
									}
									else
									{
										rect2D2.x1 = rect2D2.x2 - 3;
									}
									base.ClearRect(Z, rect2D2);
									ZoneBuilderSandbox.PlaceObjectOnRect(Z, base.getAVillageWall(), rect2D2, false);
									Location2D location2 = rect2D2.GetRandomDoorCell(cellSide, 1).location;
									Z.GetCell(location2).Clear(null, false, false, null);
									Z.GetCell(location2).AddObject(base.getAVillageDoor(), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
									this.burrowedDoors.Add(Location2D.Get(location2.X, location2.Y));
									ZoneBuilderSandbox.PlacePopulationInRect(Z, rect2D.ReduceBy(1, 1), base.ResolvePopulationTableName("Villages_FarmAnimals"), new Action<GameObject>(base.setVillageDomesticatedProperties));
									ZoneBuilderSandbox.PlacePopulationInRect(Z, rect2D2.ReduceBy(1, 1), base.ResolvePopulationTableName("Villages_FarmHutContents"));
								}
								else if (blueprint2 == "garden" || blueprint2 == "farm")
								{
									int num27 = Stat.Random(1, 4);
									GameObject afarmablePlant2 = base.getAFarmablePlant();
									List<GameObject> originalLiquids3 = this.originalLiquids;
									string text10;
									if (originalLiquids3 == null)
									{
										text10 = null;
									}
									else
									{
										GameObject randomElement3 = originalLiquids3.GetRandomElement<GameObject>(null);
										text10 = ((randomElement3 != null) ? randomElement3.Blueprint : null);
									}
									string text11 = text10 ?? base.getZoneDefaultLiquid(this.zone);
									if (num27 == 1)
									{
										bool flag4 = Stat.Random(1, 100) <= 33;
										for (int num28 = R.BoundingBox.x1; num28 <= R.BoundingBox.x2; num28++)
										{
											for (int num29 = R.BoundingBox.y1; num29 <= R.BoundingBox.y2; num29++)
											{
												if (num28 % 2 == 0)
												{
													if (R.Cells.Contains(Location2D.Get(num28, num29)) && !this.buildingPaths.Contains(Location2D.Get(num28, num29)))
													{
														Cell cell4 = Z.GetCell(num28, num29);
														if (cell4 != null)
														{
															cell4.AddObject(afarmablePlant2.Blueprint, new Action<GameObject>(base.setVillageDomesticatedProperties), null);
														}
													}
												}
												else if (flag4 && R.Cells.Contains(Location2D.Get(num28, num29)) && Z.GetCell(num28, num29) != null)
												{
													Z.GetCell(num28, num29).AddObject(text11, null, null, null, null);
												}
											}
										}
									}
									if (num27 == 2)
									{
										List<GameObject> originalLiquids4 = this.originalLiquids;
										string text12;
										if (originalLiquids4 == null)
										{
											text12 = null;
										}
										else
										{
											GameObject randomElement4 = originalLiquids4.GetRandomElement<GameObject>(null);
											text12 = ((randomElement4 != null) ? randomElement4.Blueprint : null);
										}
										string text13 = text12 ?? base.getZoneDefaultLiquid(this.zone);
										bool flag5 = Stat.Random(1, 100) <= 33;
										for (int num30 = R.BoundingBox.x1; num30 <= R.BoundingBox.x2; num30++)
										{
											for (int num31 = R.BoundingBox.y1; num31 <= R.BoundingBox.y2; num31++)
											{
												if (num31 % 2 == 0)
												{
													if (R.Cells.Contains(Location2D.Get(num30, num31)) && !this.buildingPaths.Contains(Location2D.Get(num30, num31)))
													{
														Cell cell5 = Z.GetCell(num30, num31);
														if (cell5 != null)
														{
															cell5.AddObject(afarmablePlant2.Blueprint, new Action<GameObject>(base.setVillageDomesticatedProperties), null);
														}
													}
												}
												else if (flag5 && R.Cells.Contains(Location2D.Get(num30, num31)) && Z.GetCell(num30, num31) != null)
												{
													Z.GetCell(num30, num31).AddObject(text13, null, null, null, null);
												}
											}
										}
									}
									if (num27 == 3)
									{
										int num32 = Stat.Random(20, 98);
										for (int num33 = R.BoundingBox.x1; num33 <= R.BoundingBox.x2; num33++)
										{
											for (int num34 = R.BoundingBox.y1; num34 <= R.BoundingBox.y2; num34++)
											{
												if (R.Cells.Contains(Location2D.Get(num33, num34)) && !this.buildingPaths.Contains(Location2D.Get(num33, num34)) && Stat.Random(1, 100) <= num32)
												{
													Cell cell6 = Z.GetCell(num33, num34);
													if (cell6 != null)
													{
														cell6.AddObject(afarmablePlant2.Blueprint, new Action<GameObject>(base.setVillageDomesticatedProperties), null);
													}
												}
											}
										}
									}
									if (num27 == 4)
									{
										int num35 = Stat.Random(20, 98);
										for (int num36 = R.BoundingBox.x1; num36 <= R.BoundingBox.x2; num36++)
										{
											for (int num37 = R.BoundingBox.y1; num37 <= R.BoundingBox.y2; num37++)
											{
												if (R.Cells.Contains(Location2D.Get(num36, num37)) && !this.buildingPaths.Contains(Location2D.Get(num36, num37)) && Stat.Random(1, 100) <= num35)
												{
													Cell cell7 = Z.GetCell(num36, num37);
													if (cell7 != null)
													{
														cell7.AddObject(base.getAFarmablePlant(), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
													}
												}
											}
										}
									}
								}
							}
							num17++;
						}
						else if (influenceMap2.SeedToRegionMap[R.Seed].AdjacentRegions.Count == 1)
						{
							dictionary2.Add(R, "cubby");
						}
						else
						{
							dictionary2.Add(R, "hall");
						}
					}
					IL_1CF1:;
				}
				base.placeNonTakeableSignatureItems();
				this.buildings.RemoveAll((PopulationLayout b) => b.inside.Count == 0 && b.outside.Count == 0);
				GameObject gameObject2 = base.generateVillageOven();
				base.PlaceObjectInBuilding(gameObject2, this.buildings.GetRandomElement<PopulationLayout>(null), If.OneIn(10, null, null) ? "Outside" : "Inside", (Location2D l) => !this.zone.GetCell(l).HasOpenLiquidVolume() && !this.zone.GetCell(l).MightBlockPaths());
				if (gameObject2 != null && gameObject2.CurrentCell != null)
				{
					gameObject2.CurrentCell.RemoveObjects((GameObject o) => o.IsOpenLiquidVolume());
				}
				if (this.villageSnapshot.GetProperty("abandoned", "unknown") != "true")
				{
					GameObject gameObject3 = null;
					GameObject gameObject4 = null;
					GameObject gameObject5 = null;
					GameObject gameObject6 = null;
					GameObject gameObject7 = null;
					if (this.villageSnapshot.listProperties.ContainsKey("immigrant_type"))
					{
						List<string> list2 = this.villageSnapshot.listProperties["immigrant_type"];
						List<string> list3 = this.villageSnapshot.listProperties["immigrant_name"];
						List<string> list4 = this.villageSnapshot.listProperties["immigrant_gender"];
						List<string> list5 = this.villageSnapshot.listProperties["immigrant_role"];
						List<string> list6 = this.villageSnapshot.listProperties["immigrant_dialogWhy_Q"];
						List<string> list7 = this.villageSnapshot.listProperties["immigrant_dialogWhy_A"];
						for (int num38 = 0; num38 < list2.Count; num38++)
						{
							string text14 = list2[num38];
							string text15;
							if (num38 >= list3.Count)
							{
								Debug.LogWarning("missing immigrant name for " + text14 + " in position " + num38.ToString());
								text15 = "MISSING_NAME";
							}
							else
							{
								text15 = list3[num38];
							}
							string text16;
							if (num38 >= list4.Count)
							{
								Debug.LogWarning("missing immigrant gender for " + text14 + " in position " + num38.ToString());
								text16 = null;
							}
							else
							{
								text16 = list4[num38];
							}
							string text17;
							if (num38 >= list5.Count)
							{
								Debug.LogWarning("missing immigrant role for " + text14 + " in position " + num38.ToString());
								text17 = "villager";
							}
							else
							{
								text17 = list5[num38];
							}
							string text18;
							if (num38 >= list6.Count)
							{
								Debug.LogWarning("missing immigrant dialog why Q for " + text14 + " in position " + num38.ToString());
								text18 = "MISSING_QUESTION";
							}
							else
							{
								text18 = list6[num38];
							}
							string text19;
							if (num38 >= list7.Count)
							{
								Debug.LogWarning("missing immigrant dialog why A for " + text14 + " in position " + num38.ToString());
								text19 = "MISSING_ANSWER";
							}
							else
							{
								text19 = list7[num38];
							}
							try
							{
								GameObject gameObject8 = this.generateImmigrant(text14, text15, text16, text17, text18, text19);
								if (text17 == "mayor")
								{
									gameObject3 = gameObject8;
								}
								else if (text17 == "merchant")
								{
									gameObject4 = gameObject8;
								}
								else if (text17 == "tinker")
								{
									gameObject5 = gameObject8;
								}
								else if (text17 == "apothecary")
								{
									gameObject6 = gameObject8;
								}
								else if (text17 == "warden")
								{
									gameObject7 = gameObject8;
								}
								else
								{
									ZoneBuilderSandbox.PlaceObject(Z, regionMap.Regions.GetRandomElement<InfluenceMapRegion>(null), gameObject8, null, null, false);
								}
							}
							catch (Exception ex)
							{
								MetricsManager.LogException("Failed to generate immigrant.", ex, "game_exception");
							}
						}
					}
					if (this.villageSnapshot.GetProperty("government", "unknown") != "anarchism")
					{
						GameObject gameObject9 = null;
						if (this.villageSnapshot.GetProperty("government", "unknown") == "colonialism")
						{
							gameObject9 = GameObject.Create(this.villageSnapshot.GetProperty("colonistType", "unknown"));
						}
						if (gameObject7 != null)
						{
							ZoneBuilderSandbox.PlaceObject(Z, this.townSquare, gameObject7, null, null, false);
						}
						else
						{
							ZoneBuilderSandbox.PlaceObject(Z, this.townSquare, this.generateWarden(gameObject9, this.isVillageZero), null, null, false);
						}
					}
					GameObject gameObject10 = null;
					if (this.villageSnapshot.GetProperty("government", "unknown") == "colonialism")
					{
						gameObject10 = GameObject.Create(this.villageSnapshot.GetProperty("colonistType", "unknown"));
						base.setVillagerProperties(gameObject10);
					}
					if (gameObject3 != null)
					{
						base.PlaceObjectInBuilding(gameObject3, this.buildings[0], If.OneIn(100, null, null) ? "Outside" : "Inside", null);
					}
					else
					{
						base.PlaceObjectInBuilding(this.generateMayor(gameObject10, "SpecialVillagerHeroTemplate_" + this.mayorTemplate, true), this.buildings[0], If.OneIn(100, null, null) ? "Outside" : "Inside", null);
					}
					if (gameObject4 != null)
					{
						base.PlaceObjectInBuilding(gameObject4, this.buildings[(this.buildings.Count >= 2) ? 1 : 0], If.OneIn(100, null, null) ? "Outside" : "Inside", null);
					}
					else if (this.isVillageZero || If.Chance(30, null, null))
					{
						base.PlaceObjectInBuilding(this.generateMerchant(null), this.buildings[(this.buildings.Count >= 2) ? 1 : 0], If.OneIn(100, null, null) ? "Outside" : "Inside", null);
					}
					if (this.isVillageZero)
					{
						if (this.region == "Saltmarsh")
						{
							ZoneBuilderSandbox.PlaceObject(Z, this.townSquare, GameObject.Create("WatervineFarmerConvert"), null, null, false);
						}
						else if (this.region == "DesertCanyon")
						{
							ZoneBuilderSandbox.PlaceObject(Z, this.townSquare, GameObject.Create("PigFarmerConvert"), null, null, false);
						}
						else if (this.region == "Hills")
						{
							ZoneBuilderSandbox.PlaceObject(Z, this.townSquare, GameObject.Create("CannibalConvert"), null, null, false);
						}
						else if (this.region == "Saltdunes")
						{
							ZoneBuilderSandbox.PlaceObject(Z, this.townSquare, GameObject.Create("IssachariConvert"), null, null, false);
						}
						else
						{
							ZoneBuilderSandbox.PlaceObject(Z, this.townSquare, GameObject.Create("WatervineFarmerConvert"), null, null, false);
						}
					}
					GameObject gameObject11 = null;
					bool flag6 = false;
					if (gameObject5 != null)
					{
						gameObject11 = gameObject5;
					}
					else if (this.isVillageZero || If.Chance(25, null, null))
					{
						gameObject11 = this.generateTinker(null);
						flag6 = true;
					}
					if (gameObject11 != null)
					{
						int num39;
						string text20;
						if (this.buildings.Count >= 3)
						{
							num39 = 2;
							text20 = (If.OneIn(50, null, null) ? "Outside" : "AlongInsideWall");
						}
						else if (this.buildings.Count >= 2)
						{
							num39 = 1;
							text20 = (If.OneIn(50, null, null) ? "Outside" : "AlongInsideWall");
						}
						else
						{
							num39 = 0;
							text20 = (If.OneIn(50, null, null) ? "Outside" : "AlongInsideWall");
						}
						base.PlaceObjectInBuilding(gameObject11, this.buildings[num39], text20, null);
						int num40 = 0;
						int num41 = Stat.Random(2, 3);
						while (num40 < num41)
						{
							base.PlaceObjectInBuilding(GameObject.Create("Workbench"), this.buildings[num39], text20, null);
							num40++;
						}
						int num42 = 0;
						int num43 = Stat.Random(0, 2);
						while (num42 < num43)
						{
							base.PlaceObjectInBuilding(GameObject.Create("Table"), this.buildings[num39], text20, null);
							num42++;
						}
					}
					GameObject gameObject12 = null;
					bool flag7 = false;
					if (gameObject6 != null)
					{
						gameObject12 = gameObject6;
					}
					else if (this.isVillageZero || If.Chance(25, null, null))
					{
						gameObject12 = this.generateApothecary(null);
						flag7 = true;
					}
					if (gameObject12 != null)
					{
						int num44;
						if (this.buildings.Count >= 4)
						{
							num44 = 3;
						}
						else if (this.buildings.Count >= 3)
						{
							num44 = 2;
						}
						else if (this.buildings.Count >= 2)
						{
							num44 = 1;
						}
						else
						{
							num44 = 0;
						}
						string text21 = (If.OneIn(50, null, null) ? "Outside" : "AlongInsideWall");
						base.PlaceObjectInBuilding(gameObject12, this.buildings[num44], text21, null);
						int num45 = 0;
						int num46 = Stat.Random(1, 2);
						while (num45 < num46)
						{
							base.PlaceObjectInBuilding(GameObject.Create("Table"), this.buildings[num44], text21, null);
							num45++;
						}
						int num47 = 0;
						int num48 = Stat.Random(0, 1);
						while (num47 < num48)
						{
							base.PlaceObjectInBuilding(GameObject.Create("Alchemist Table"), this.buildings[num44], text21, null);
							num47++;
						}
						int num49 = 0;
						int num50 = Stat.Random(2, 3);
						while (num49 < num50)
						{
							base.PlaceObjectInBuilding(GameObject.Create("Woven Basket"), this.buildings[num44], text21, null);
							num49++;
						}
					}
					int num51 = Stat.Random(4, 10);
					if (!this.isVillageZero)
					{
						if (!flag6)
						{
							num51++;
						}
						if (!flag7)
						{
							num51++;
						}
					}
					if (this.villageSnapshot.listProperties.ContainsKey("populationMultiplier"))
					{
						foreach (string text22 in this.villageSnapshot.listProperties["populationMultiplier"])
						{
							num51 *= int.Parse(text22);
						}
					}
					for (int num52 = 0; num52 < num51; num52++)
					{
						ZoneBuilderSandbox.PlaceObject(Z, regionMap.Regions.GetRandomElement<InfluenceMapRegion>(null), base.generateVillager(false), null, null, false);
					}
					if (this.villageSnapshot.GetProperty("government", "unknown") == "colonialism")
					{
						int num53 = 0;
						int num54 = Stat.Random(2, 3);
						while (num53 < num54)
						{
							GameObject gameObject13 = GameObject.Create(this.villageSnapshot.GetProperty("colonistType", "unknown"));
							base.setVillagerProperties(gameObject13);
							gameObject13.SetIntProperty("SuppressSimpleConversation", 1, false);
							base.AddVillagerConversation(gameObject13, gameObject13.GetTag("SimpleConversation", "Moon and Sun. Wisdom and will.~May the earth yield for us this season.~Peace, =player.formalAddressTerm=."), "Live and drink.", null, null, false, true);
							ZoneBuilderSandbox.PlaceObject(Z, regionMap.Regions.GetRandomElement<InfluenceMapRegion>(null), gameObject13, null, null, false);
							num53++;
						}
					}
					if (this.villageSnapshot.GetProperty("government", "unknown") == "representative democracy")
					{
						int num55 = 0;
						int num56 = Stat.Random(2, 4);
						while (num55 < num56)
						{
							ZoneBuilderSandbox.PlaceObject(Z, regionMap.Regions.GetRandomElement<InfluenceMapRegion>(null), this.generateMayor(null, "SpecialVillagerHeroTemplate_" + this.mayorTemplate, false), null, null, false);
							num55++;
						}
					}
					ZoneBuilderSandbox.PlaceObject(Z, regionMap.Regions.GetRandomElement<InfluenceMapRegion>(null), base.generateVillager(true), null, null, false);
				}
				if (this.villageSnapshot.listProperties.ContainsKey("pet_petSpecies"))
				{
					int x2;
					int x;
					for (x = 0; x < this.villageSnapshot.listProperties["pet_petSpecies"].Count; x = x2 + 1)
					{
						try
						{
							List<string> petNames = new List<string>();
							int num57 = int.Parse(this.villageSnapshot.listProperties["pet_number"][x]);
							for (int num58 = 0; num58 < num57; num58++)
							{
								string text23;
								GameObject gameObject14 = this.generatePet(this.villageSnapshot.listProperties["pet_petSpecies"][x], out text23);
								ZoneBuilderSandbox.PlaceObject(Z, regionMap.Regions.GetRandomElement<InfluenceMapRegion>(null), gameObject14, null, null, false);
								petNames.Add(text23);
							}
							GameObject petSample = GameObject.Create(this.villageSnapshot.listProperties["pet_petSpecies"][x]);
							this.zone.ForeachObjectWithTagOrProperty("Villager", delegate(GameObject o)
							{
								string text26 = HistoricStringExpander.ExpandString("<spice.villages.pet.originStory.!random>", null, null, QudHistoryHelpers.BuildContextFromObjectTextFragments(this.villageSnapshot.listProperties["pet_petSpecies"][x]), null);
								if (int.Parse(this.villageSnapshot.listProperties["pet_number"][x]) == 1)
								{
									text26 = text26.Replace("@them@", petSample.them).Replace("@they@", petSample.it).Replace("@they're@", petSample.itis)
										.Replace("@they've@", petSample.ithas)
										.Replace("@their@", petSample.its)
										.Replace("@Them@", petSample.Them)
										.Replace("@They@", petSample.It)
										.Replace("@They're@", petSample.Itis)
										.Replace("@They've@", petSample.Ithas)
										.Replace("@Their@", petSample.Its)
										.Replace("@has@", "has")
										.Replace("@Name@", petNames[0]);
								}
								else
								{
									text26 = text26.Replace("@them@", "them").Replace("@they@", "they").Replace("@they're@", "they're")
										.Replace("@they've@", "they've")
										.Replace("@their@", "their")
										.Replace("@Them@", "Them")
										.Replace("@They@", "They")
										.Replace("@They're@", "They're")
										.Replace("@They've@", "They've")
										.Replace("@Their@", "Their")
										.Replace("@has@", "have")
										.Replace("@Name@", Grammar.MakeAndList(petNames, true));
								}
								if (!o.HasTagOrProperty("VillagePet"))
								{
									this.AddVillagerConversation(o, o.GetTag("SimpleConversation", "Moon and Sun. Wisdom and will.~May the earth yield for us this season.~Peace, =player.formalAddressTerm=."), "Live and drink.", this.villageSnapshot.listProperties["pet_dialogWhy_Q"][x], text26, true, true);
								}
							});
						}
						catch (Exception ex2)
						{
							MetricsManager.LogException("Failed to generate pet.", ex2, "game_exception");
						}
						x2 = x;
					}
				}
				Z.ForeachObjectWithPart("Brain", delegate(GameObject obj)
				{
					GameObject partyLeader = obj.PartyLeader;
					AllegianceSet allegianceSet = ((partyLeader != null) ? partyLeader.Brain.Allegiance : null);
					int num61;
					if (allegianceSet != null && allegianceSet.TryGetValue(this.villageFaction, out num61))
					{
						obj.Brain.Allegiance.TryAdd(this.villageFaction, num61);
					}
				});
				Z.ForeachObjectWithPart("SecretObject", delegate(GameObject obj)
				{
					obj.RemovePart<SecretObject>();
				});
				base.placeStories();
				Z.ForeachObject(delegate(GameObject o)
				{
					if ((o.GetBlueprint(true).HasTag("Furniture", false) || o.GetBlueprint(true).HasTag("Vessel", false)) && o.Physics != null)
					{
						o.Physics.Owner = this.villageFaction;
					}
					if (this.villageSnapshot.listProperties.ContainsKey("signatureLiquids") && o.GetBlueprint(true).HasTag("Vessel", false) && If.Chance(80, null, null))
					{
						LiquidVolume liquidVolume = o.LiquidVolume;
						if (liquidVolume != null)
						{
							liquidVolume.InitialLiquid = this.villageSnapshot.GetList("signatureLiquids").GetRandomElement<string>(null);
						}
					}
					if (o.HasStringProperty("GivesDynamicQuest") && o.Brain != null)
					{
						o.Brain.Wanders = false;
						o.Brain.WandersRandomly = false;
					}
				});
				if (this.villageSnapshot.GetProperty("abandoned", "unknown") == "true")
				{
					int num59 = 1;
					try
					{
						num59 = Convert.ToInt32(this.villageSnapshot.GetProperty("ruinScale", "unknown"));
						if (num59 < 1)
						{
							num59 = 1;
						}
						if (num59 > 4)
						{
							num59 = 4;
						}
					}
					catch (Exception ex3)
					{
						global::Logger.Exception(ex3);
					}
					if (num59 > 1)
					{
						int num60 = 10;
						if (num59 == 3)
						{
							num60 = 50;
						}
						if (num59 == 4)
						{
							num60 = 100;
						}
						new Ruiner().RuinZone(Z, num60, false, 100000, 500000);
						foreach (GameObject gameObject15 in this.originalPlants)
						{
							ZoneBuilderSandbox.PlaceObject(gameObject15, Z, null);
						}
					}
					if (If.Chance(70, null, null))
					{
						foreach (GameObject gameObject16 in this.originalCreatures)
						{
							ZoneBuilderSandbox.PlaceObject(gameObject16, Z, null);
						}
					}
					Z.ReplaceAll("Torchpost", "Unlit Torchpost");
					Z.ReplaceAll("Sconce", "Unlit Torchpost");
				}
				ZoneBuilderSandbox.EnsureAllVoidsConnected(Z, false, 0.2f, null);
				if (Z.HasBuilder("RiverBuilder"))
				{
					bool flag8 = false;
					List<GameObject> originalLiquids5 = this.originalLiquids;
					string text24;
					if (originalLiquids5 == null)
					{
						text24 = null;
					}
					else
					{
						GameObject randomElement5 = originalLiquids5.GetRandomElement<GameObject>(null);
						text24 = ((randomElement5 != null) ? randomElement5.Blueprint : null);
					}
					new RiverBuilder(flag8, text24 ?? base.getZoneDefaultLiquid(Z), true).BuildZone(Z);
				}
				if (Z.HasBuilder("RoadBuilder"))
				{
					new RoadBuilder(false).BuildZone(Z);
				}
				foreach (GameObject gameObject17 in this.requiredPlacementObjects)
				{
					if (gameObject17.IsCombat)
					{
						base.setVillagerProperties(gameObject17);
					}
					ZoneBuilderSandbox.PlaceObject(gameObject17, this.zone, null);
				}
				string text25 = ((this.villageSnapshot.GetProperty("abandoned", "unknown") == "true") ? Stat.Random(5, 25).ToString() : (10 - this.villageTechTier).ToString());
				PowerGrid powerGrid = new PowerGrid();
				powerGrid.DamageChance = text25;
				if ((10 + this.villageTechTier * 3).in100())
				{
					powerGrid.MissingConsumers = "1d6";
					powerGrid.MissingProducers = "1d3";
				}
				powerGrid.BuildZone(Z);
				Hydraulics hydraulics = new Hydraulics();
				hydraulics.DamageChance = text25;
				if ((10 + this.villageTechTier * 3).in100())
				{
					hydraulics.MissingConsumers = "1d6";
					hydraulics.MissingProducers = "1d3";
				}
				hydraulics.BuildZone(Z);
				MechanicalPower mechanicalPower = new MechanicalPower();
				mechanicalPower.DamageChance = text25;
				if ((20 - this.villageTechTier).in100())
				{
					mechanicalPower.MissingConsumers = "1d6";
					mechanicalPower.MissingProducers = "1d3";
				}
				mechanicalPower.BuildZone(Z);
				Z.SetMusic("Music/Mehmets Book on Strings");
				Z.FireEvent("VillageInit");
				this.cleanup();
				new IsCheckpoint().BuildZoneWithKey(Z, this.villageName);
				Cell cell8 = Z.GetCell(0, 0);
				if (this.SurfaceRevealer && !cell8.HasObject("VillageSurface") && base.villageEntity != null)
				{
					GameObject gameObject18 = GameObject.Create("VillageSurface");
					VillageSurface villageSurface;
					if (gameObject18.TryGetPart<VillageSurface>(out villageSurface))
					{
						Cell worldCell = Z.GetWorldCell();
						villageSurface.VillageName = this.villageName;
						villageSurface.RevealKey = "villageReveal_" + this.villageName;
						villageSurface.RevealLocation = new Vector2i(worldCell.X, worldCell.Y);
						villageSurface.RevealSecret = base.villageEntity.id;
						villageSurface.IsVillageZero = base.villageEntity.GetEntityProperty("isVillageZero", -1L).EqualsNoCase("true");
						if (base.villageEntity.GetEntityProperty("abandoned", -1L).EqualsNoCase("true"))
						{
							villageSurface.RevealString = "You discover the abandoned village of " + this.villageName + ".";
						}
						else
						{
							villageSurface.RevealString = "You discover the village of " + this.villageName + ".";
						}
						cell8.AddObject(gameObject18, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
					}
				}
				return true;
			}
			goto IL_03F4;
		}

		// Token: 0x04003425 RID: 13349
		public string dynamicCreatureTableName;

		// Token: 0x04003426 RID: 13350
		public bool SurfaceRevealer = true;

		// Token: 0x04003427 RID: 13351
		private string[] staticPerVillage = new string[] { "*Storage,*Furniture", "*LiquidStorage,*Furniture", "*Seating,*Furniture", "*Sleep*,*Furniture" };

		// Token: 0x04003428 RID: 13352
		private string[] staticPerBuilding = new string[] { "*LightSource,*Furniture" };

		// Token: 0x04003429 RID: 13353
		private Dictionary<string, string> staticVillageResults = new Dictionary<string, string>();
	}
}
