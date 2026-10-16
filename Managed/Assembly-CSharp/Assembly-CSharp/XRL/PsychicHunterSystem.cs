using System;
using System.Collections.Generic;
using HistoryKit;
using Qud.API;
using UnityEngine;
using XRL.Language;
using XRL.Names;
using XRL.Rules;
using XRL.UI;
using XRL.Wish;
using XRL.World;
using XRL.World.AI;
using XRL.World.AI.GoalHandlers;
using XRL.World.Capabilities;
using XRL.World.Encounters;
using XRL.World.Parts;
using XRL.World.Parts.Mutation;
using XRL.World.ZoneParts;

namespace XRL
{
	// Token: 0x020004F7 RID: 1271
	[HasWishCommand]
	[Serializable]
	public class PsychicHunterSystem : IGameSystem
	{
		// Token: 0x060036BB RID: 14011 RVA: 0x0010816C File Offset: 0x0010636C
		public override void Read(SerializationReader Reader)
		{
			this.Visited = Reader.ReadDictionary<string, bool>();
		}

		// Token: 0x060036BC RID: 14012 RVA: 0x0010817A File Offset: 0x0010637A
		public override void Write(SerializationWriter Writer)
		{
			Writer.Write<string, bool>(this.Visited);
		}

		// Token: 0x060036BD RID: 14013 RVA: 0x00108188 File Offset: 0x00106388
		public override void Register(XRLGame Game, IEventRegistrar Registrar)
		{
			Registrar.Register(ZoneActivatedEvent.ID, 0, false);
		}

		// Token: 0x060036BE RID: 14014 RVA: 0x00108197 File Offset: 0x00106397
		public override bool HandleEvent(ZoneActivatedEvent E)
		{
			this.CheckPsychicHunters(E.Zone);
			return base.HandleEvent(E);
		}

		// Token: 0x060036BF RID: 14015 RVA: 0x001081AC File Offset: 0x001063AC
		public void CreatePsychicHunter(int numHunters, Zone Z)
		{
			if (The.Player.GetPsychicGlimmer(null) < DimensionManager.GLIMMER_EXTRADIMENSIONAL_FLOOR)
			{
				PsychicHunterSystem.CreateSeekerHunters(numHunters, Z);
				return;
			}
			if (numHunters <= 1)
			{
				if (numHunters > 0)
				{
					int num = Stat.Random(1, 100);
					if (num <= 30)
					{
						PsychicHunterSystem.CreateSeekerHunters(numHunters, Z);
						return;
					}
					if (num <= 70)
					{
						PsychicHunterSystem.CreateExtradimensionalSoloHunters(Z, numHunters, null, true, false, true, true);
						return;
					}
					PsychicHunterSystem.CreateExtradimensionalCultHunters(Z, numHunters, null, true, false, true, true);
				}
				return;
			}
			if (50.in100())
			{
				PsychicHunterSystem.CreateSeekerHunters(numHunters, Z);
				return;
			}
			PsychicHunterSystem.CreateExtradimensionalCultHunters(Z, numHunters, null, true, false, true, true);
		}

		// Token: 0x060036C0 RID: 14016 RVA: 0x00108230 File Offset: 0x00106430
		public static void CreateSeekerHunters(int numHunters, Zone Z)
		{
			bool flag = false;
			if (The.Game.PlayerReputation.Get("Seekers") >= RuleSettings.REPUTATION_LIKED)
			{
				return;
			}
			for (int i = 1; i <= numHunters; i++)
			{
				XRL.World.GameObject gameObject = XRL.World.GameObject.Create("PsychicSeekerHunter");
				gameObject.Render.SetForegroundColor("M");
				gameObject.Brain.Allegiance.Hostile = true;
				gameObject.Brain.Hibernating = false;
				gameObject.Brain.AddOpinion<OpinionPsychicHunt>(The.Player, 1f);
				gameObject.AwardXP(The.Player.Stat("XP", 0), -1, 0, int.MaxValue, null, The.Player, null, null, null, null);
				gameObject.Statistics["Ego"].BaseValue = The.Player.Statistics["Ego"].Value;
				if (gameObject.Statistics.ContainsKey("MP"))
				{
					gameObject.Statistics["MP"].BaseValue = 0;
				}
				Mutations part = The.Player.GetPart<Mutations>();
				Mutations part2 = gameObject.GetPart<Mutations>();
				foreach (BaseMutation baseMutation in part.MutationList)
				{
					MutationEntry mutationEntry = baseMutation.GetMutationEntry();
					if (((mutationEntry != null) ? mutationEntry.Category : null) != null && mutationEntry.Category.Name == "Mental" && mutationEntry.Cost > 1)
					{
						List<MutationEntry> list = new List<MutationEntry>(gameObject.GetPart<Mutations>().GetMutatePool(null, false));
						list.ShuffleInPlace<MutationEntry>(null);
						MutationEntry mutationEntry2 = null;
						foreach (MutationEntry mutationEntry3 in list)
						{
							if (mutationEntry3.Category != null && mutationEntry3.Category.Name == "Mental" && !gameObject.HasPart(mutationEntry3.Class) && mutationEntry3.Cost > 1)
							{
								mutationEntry2 = mutationEntry3;
								break;
							}
						}
						if (mutationEntry2 != null)
						{
							part2.AddMutation(mutationEntry2.Class, baseMutation.BaseLevel);
						}
					}
				}
				string text;
				if (The.Player.GetPsychicGlimmer(null) < DimensionManager.GLIMMER_FLOOR + 15)
				{
					text = "Osprey";
				}
				else if (The.Player.GetPsychicGlimmer(null) < DimensionManager.GLIMMER_FLOOR + 30)
				{
					text = "Harrier";
				}
				else if (The.Player.GetPsychicGlimmer(null) < DimensionManager.GLIMMER_FLOOR + 45)
				{
					text = "Owl";
				}
				else if (The.Player.GetPsychicGlimmer(null) < DimensionManager.GLIMMER_FLOOR + 60)
				{
					text = "Condor";
				}
				else if (The.Player.GetPsychicGlimmer(null) < DimensionManager.GLIMMER_FLOOR + 75)
				{
					text = "Strix";
				}
				else if (The.Player.GetPsychicGlimmer(null) < DimensionManager.GLIMMER_FLOOR + 90)
				{
					text = "Eagle";
				}
				else
				{
					text = "Rukh";
				}
				string text2 = "Ptoh's " + Grammar.MakeTitleCase(HistoricStringExpander.ExpandString("<spice.seekers.title.!random>", null).Replace("*rank*", text));
				Dictionary<string, string> dictionary = new Dictionary<string, string> { { "*Position*", text2 } };
				string text3 = NameMaker.MakeTitle(gameObject, null, null, null, null, null, null, null, null, null, "PsychicHunter", dictionary, true, null, null);
				XRL.World.GameObject gameObject2 = gameObject;
				string text4 = null;
				bool flag2 = false;
				string text5 = "PsychicHunter";
				bool flag3 = true;
				Dictionary<string, string> dictionary2 = dictionary;
				gameObject2.GiveProperName(text4, flag2, text5, flag3, null, null, dictionary2, null);
				gameObject.RequirePart<DisplayNameColor>(false).SetColorByPriority("M", 30);
				if (!text3.IsNullOrEmpty())
				{
					gameObject.RequirePart<Titles>(false).AddTitle(text3, -40);
				}
				gameObject.RequirePart<AbsorbablePsyche>(false);
				gameObject.AddPart<HasThralls>(new HasThralls(PsychicHunterSystem.GetThrallRoll(The.Player.GetPsychicGlimmer(null)), true), true, false);
				PsychicHunterSystem.RemoveParts(gameObject);
				flag |= PsychicHunterSystem.PlaceHunter(Z, gameObject, null, false, "&C");
			}
			if (flag)
			{
				if (numHunters > 1)
				{
					Popup.Show("{{c|You sense the animus of a vast mind. They are near.}}", null, "Sounds/UI/ui_notification", true, true, true, true, null);
					return;
				}
				Popup.Show("{{c|You sense the animus of a vast mind. Someone is near.}}", null, "Sounds/UI/ui_notification", true, true, true, true, null);
			}
		}

		// Token: 0x060036C1 RID: 14017 RVA: 0x00108688 File Offset: 0x00106888
		public static void CreateExtradimensionalSoloHunters(Zone Z, int Number = 1, List<XRL.World.GameObject> ObjectList = null, bool Place = true, bool TeleportSwirl = false, bool UseMessage = true, bool UsePopup = true)
		{
			ExtraDimension randomElement = (The.Game.GetObjectGameState("DimensionManager") as DimensionManager).ExtraDimensions.GetRandomElement<ExtraDimension>(null);
			int num = 0;
			for (int i = 0; i < Number; i++)
			{
				XRL.World.GameObject nonLegendaryCreatureAroundPlayerLevel = EncountersAPI.GetNonLegendaryCreatureAroundPlayerLevel(null);
				nonLegendaryCreatureAroundPlayerLevel.Render.SetForegroundColor(randomElement.mainColor);
				nonLegendaryCreatureAroundPlayerLevel.Render.DetailColor = "O";
				if (!nonLegendaryCreatureAroundPlayerLevel.HasProperty("PsychicHunter"))
				{
					nonLegendaryCreatureAroundPlayerLevel.SetStringProperty("PsychicHunter", "true", false);
				}
				nonLegendaryCreatureAroundPlayerLevel.Brain.Allegiance.Clear();
				nonLegendaryCreatureAroundPlayerLevel.Brain.Allegiance.Add("Playerhater", 100);
				nonLegendaryCreatureAroundPlayerLevel.Brain.Allegiance.Hostile = true;
				nonLegendaryCreatureAroundPlayerLevel.Brain.Allegiance.Calm = false;
				nonLegendaryCreatureAroundPlayerLevel.Brain.Hibernating = false;
				nonLegendaryCreatureAroundPlayerLevel.Brain.Aquatic = false;
				nonLegendaryCreatureAroundPlayerLevel.Brain.Mobile = true;
				nonLegendaryCreatureAroundPlayerLevel.Brain.AddOpinion<OpinionPsychicHunt>(The.Player, 1f);
				nonLegendaryCreatureAroundPlayerLevel.RequirePart<Combat>(false);
				ConversationScript part = nonLegendaryCreatureAroundPlayerLevel.GetPart<ConversationScript>();
				if (part != null)
				{
					part.Filter = "Weird";
					part.FilterExtras = randomElement.Name;
				}
				nonLegendaryCreatureAroundPlayerLevel.AwardXP(The.Player.Stat("XP", 0), -1, 0, int.MaxValue, null, The.Player, null, null, null, null);
				nonLegendaryCreatureAroundPlayerLevel.GetStat("Ego").BaseValue = The.Player.Stat("Ego", 0);
				if (nonLegendaryCreatureAroundPlayerLevel.HasStat("MP"))
				{
					nonLegendaryCreatureAroundPlayerLevel.GetStat("MP").BaseValue = 0;
				}
				Mutations part2 = The.Player.GetPart<Mutations>();
				Mutations part3 = nonLegendaryCreatureAroundPlayerLevel.GetPart<Mutations>();
				foreach (BaseMutation baseMutation in part2.MutationList)
				{
					MutationEntry mutationEntry = baseMutation.GetMutationEntry();
					if (((mutationEntry != null) ? mutationEntry.Category : null) != null && mutationEntry.Category.Name == "Mental" && mutationEntry.Cost > 1)
					{
						List<MutationEntry> list = new List<MutationEntry>(nonLegendaryCreatureAroundPlayerLevel.GetPart<Mutations>().GetMutatePool(null, false));
						list.ShuffleInPlace<MutationEntry>(null);
						MutationEntry mutationEntry2 = null;
						foreach (MutationEntry mutationEntry3 in list)
						{
							if (mutationEntry3.Category != null && mutationEntry3.Category.Name == "Mental" && !nonLegendaryCreatureAroundPlayerLevel.HasPart(mutationEntry3.Class) && mutationEntry3.Cost > 1)
							{
								mutationEntry2 = mutationEntry3;
								break;
							}
						}
						if (mutationEntry2 != null)
						{
							part3.AddMutation(mutationEntry2.Class, baseMutation.BaseLevel);
						}
					}
				}
				string text = NameMaker.MakeTitle(nonLegendaryCreatureAroundPlayerLevel, null, null, null, null, null, null, null, null, null, "PsychicHunter", null, true, null, null);
				XRL.World.GameObject gameObject = nonLegendaryCreatureAroundPlayerLevel;
				string text2 = null;
				bool flag = false;
				string text3 = "PsychicHunter";
				bool flag2 = true;
				Func<string, string> func = new Func<string, string>(randomElement.Weirdify);
				gameObject.GiveProperName(text2, flag, text3, flag2, null, null, null, func);
				Titles titles = nonLegendaryCreatureAroundPlayerLevel.RequirePart<Titles>(false);
				if (!text.IsNullOrEmpty())
				{
					titles.AddTitle(text, -40);
				}
				titles.AddTitle("extradimensional " + nonLegendaryCreatureAroundPlayerLevel.GetCreatureType(false, false), -20);
				titles.AddTitle("esper " + HistoricStringExpander.ExpandString("<spice.commonPhrases.hunter.!random>", null), -5);
				nonLegendaryCreatureAroundPlayerLevel.RequirePart<DisplayNameColor>(false).SetColorAndPriority("O", 30);
				XRL.World.Parts.Temporary.AddHierarchically(nonLegendaryCreatureAroundPlayerLevel, -1, null, null, false);
				nonLegendaryCreatureAroundPlayerLevel.RequirePart<AbsorbablePsyche>(false);
				nonLegendaryCreatureAroundPlayerLevel.AddPart<Extradimensional>(new Extradimensional("{{O|" + randomElement.Name.Replace("*dimensionSymbol*", ((char)randomElement.Symbol).ToString()) + "}}", randomElement.WeaponIndex, randomElement.MissileWeaponIndex, randomElement.ArmorIndex, randomElement.ShieldIndex, randomElement.MiscIndex, randomElement.Training, randomElement.SecretID), true, false);
				nonLegendaryCreatureAroundPlayerLevel.RequirePart<ExtradimensionalLoot>(false);
				nonLegendaryCreatureAroundPlayerLevel.AddPart<RevealObservationOnLook>(new RevealObservationOnLook(randomElement.SecretID), true, false);
				PsychicHunterSystem.RemoveParts(nonLegendaryCreatureAroundPlayerLevel);
				if (ObjectList != null)
				{
					ObjectList.Add(nonLegendaryCreatureAroundPlayerLevel);
				}
				if (Place && PsychicHunterSystem.PlaceHunter(Z, nonLegendaryCreatureAroundPlayerLevel, null, TeleportSwirl, "&C"))
				{
					num++;
				}
			}
			if (UseMessage && num > 0)
			{
				PsychicHunterSystem.PsychicPresenceMessage(num, UsePopup);
			}
		}

		// Token: 0x060036C2 RID: 14018 RVA: 0x00108B1C File Offset: 0x00106D1C
		public static void CreateExtradimensionalSoloDeviant(Zone Z)
		{
			ExtraDimension randomElement = (The.Game.GetObjectGameState("DimensionManager") as DimensionManager).ExtraDimensions.GetRandomElement<ExtraDimension>(null);
			XRL.World.GameObject nonLegendaryCreatureAroundPlayerLevel = EncountersAPI.GetNonLegendaryCreatureAroundPlayerLevel(null);
			nonLegendaryCreatureAroundPlayerLevel.Render.SetForegroundColor(randomElement.mainColor);
			nonLegendaryCreatureAroundPlayerLevel.Render.DetailColor = "O";
			if (!nonLegendaryCreatureAroundPlayerLevel.HasProperty("PsychicHunter"))
			{
				nonLegendaryCreatureAroundPlayerLevel.SetStringProperty("PsychicHunter", "true", false);
			}
			nonLegendaryCreatureAroundPlayerLevel.Brain.Hibernating = false;
			nonLegendaryCreatureAroundPlayerLevel.Brain.Aquatic = false;
			nonLegendaryCreatureAroundPlayerLevel.Brain.Mobile = true;
			nonLegendaryCreatureAroundPlayerLevel.Brain.Allegiance.Clear();
			nonLegendaryCreatureAroundPlayerLevel.Brain.Allegiance.Add("Entropic", 100);
			nonLegendaryCreatureAroundPlayerLevel.RequirePart<Combat>(false);
			ConversationScript part = nonLegendaryCreatureAroundPlayerLevel.GetPart<ConversationScript>();
			if (part != null)
			{
				part.Filter = "Weird";
				part.FilterExtras = randomElement.Name;
			}
			nonLegendaryCreatureAroundPlayerLevel.AwardXP(The.Player.Stat("XP", 0), -1, 0, int.MaxValue, null, The.Player, null, null, null, null);
			nonLegendaryCreatureAroundPlayerLevel.GetStat("Ego").BaseValue = The.Player.Stat("Ego", 0);
			if (nonLegendaryCreatureAroundPlayerLevel.HasStat("MP"))
			{
				nonLegendaryCreatureAroundPlayerLevel.GetStat("MP").BaseValue = 0;
			}
			Mutations part2 = The.Player.GetPart<Mutations>();
			Mutations part3 = nonLegendaryCreatureAroundPlayerLevel.GetPart<Mutations>();
			foreach (BaseMutation baseMutation in part2.MutationList)
			{
				MutationEntry mutationEntry = baseMutation.GetMutationEntry();
				if (((mutationEntry != null) ? mutationEntry.Category : null) != null && mutationEntry.Category.Name == "Mental" && mutationEntry.Cost > 1)
				{
					List<MutationEntry> list = new List<MutationEntry>(nonLegendaryCreatureAroundPlayerLevel.GetPart<Mutations>().GetMutatePool(null, false));
					list.ShuffleInPlace<MutationEntry>(null);
					MutationEntry mutationEntry2 = null;
					foreach (MutationEntry mutationEntry3 in list)
					{
						if (mutationEntry3.Category != null && mutationEntry3.Category.Name == "Mental" && !nonLegendaryCreatureAroundPlayerLevel.HasPart(mutationEntry3.Class) && mutationEntry3.Cost > 1)
						{
							mutationEntry2 = mutationEntry3;
							break;
						}
					}
					if (mutationEntry2 != null)
					{
						part3.AddMutation(mutationEntry2.Class, baseMutation.BaseLevel);
					}
				}
			}
			string text = NameMaker.MakeTitle(nonLegendaryCreatureAroundPlayerLevel, null, null, null, null, null, null, null, null, null, "PsychicHunter", null, true, null, null);
			XRL.World.GameObject gameObject = nonLegendaryCreatureAroundPlayerLevel;
			string text2 = null;
			bool flag = false;
			string text3 = "PsychicHunter";
			bool flag2 = true;
			Func<string, string> func = new Func<string, string>(randomElement.Weirdify);
			gameObject.GiveProperName(text2, flag, text3, flag2, null, null, null, func);
			Titles titles = nonLegendaryCreatureAroundPlayerLevel.RequirePart<Titles>(false);
			if (!text.IsNullOrEmpty())
			{
				titles.AddTitle(text, -40);
			}
			titles.AddTitle(nonLegendaryCreatureAroundPlayerLevel.GetCreatureType(false, false), -20);
			titles.AddTitle("transdimensional " + HistoricStringExpander.ExpandString("<spice.commonPhrases.entropist.!random>", null), -5);
			nonLegendaryCreatureAroundPlayerLevel.RequirePart<DisplayNameColor>(false).SetColorAndPriority("O", 30);
			XRL.World.Parts.Temporary.AddHierarchically(nonLegendaryCreatureAroundPlayerLevel, -1, null, null, false);
			nonLegendaryCreatureAroundPlayerLevel.RequirePart<AbsorbablePsyche>(false);
			nonLegendaryCreatureAroundPlayerLevel.AddPart<Extradimensional>(new Extradimensional("{{O|" + randomElement.Name.Replace("*dimensionSymbol*", ((char)randomElement.Symbol).ToString()) + "}}", randomElement.WeaponIndex, randomElement.MissileWeaponIndex, randomElement.ArmorIndex, randomElement.ShieldIndex, randomElement.MiscIndex, randomElement.Training, randomElement.SecretID), true, false);
			nonLegendaryCreatureAroundPlayerLevel.RequirePart<ExtradimensionalLoot>(false);
			nonLegendaryCreatureAroundPlayerLevel.AddPart<RevealObservationOnLook>(new RevealObservationOnLook(randomElement.SecretID), true, false);
			PsychicHunterSystem.RemoveParts(nonLegendaryCreatureAroundPlayerLevel);
			Debug.Log(nonLegendaryCreatureAroundPlayerLevel.DisplayName);
			Debug.Log(nonLegendaryCreatureAroundPlayerLevel.GetPart<Description>().Short);
			PsychicHunterSystem.PlaceHunter(Z, nonLegendaryCreatureAroundPlayerLevel, null, false, "&C");
		}

		// Token: 0x060036C3 RID: 14019 RVA: 0x00108F50 File Offset: 0x00107150
		public static void CreateExtradimensionalCultHunters(Zone Z, int Number = 1, List<XRL.World.GameObject> ObjectList = null, bool Place = true, bool TeleportSwirl = false, bool UseMessage = true, bool UsePopup = true)
		{
			PsychicFaction randomElement = (The.Game.GetObjectGameState("DimensionManager") as DimensionManager).PsychicFactions.GetRandomElement<PsychicFaction>(null);
			string factionName = randomElement.factionName;
			int num = 0;
			for (int i = 0; i < Number; i++)
			{
				List<GameObjectBlueprint> factionMembers = GameObjectFactory.Factory.GetFactionMembers(factionName, false, true);
				factionMembers.ShuffleInPlace<GameObjectBlueprint>(null);
				GameObjectBlueprint gameObjectBlueprint = null;
				GameObjectBlueprint gameObjectBlueprint2 = null;
				int num2 = int.MaxValue;
				int num3 = The.Player.Stat("Level", 0);
				int num4 = 0;
				foreach (GameObjectBlueprint gameObjectBlueprint3 in factionMembers)
				{
					if (gameObjectBlueprint3.HasStat("Level") && EncountersAPI.IsEligibleForDynamicEncounters(gameObjectBlueprint3) && EncountersAPI.IsLegendaryEligible(gameObjectBlueprint3))
					{
						int value = gameObjectBlueprint3.GetStat("Level", null).Value;
						if (gameObjectBlueprint == null && value < num3)
						{
							gameObjectBlueprint = gameObjectBlueprint3;
							num4 = value;
						}
						else if (value < num3 && value > num4)
						{
							gameObjectBlueprint = gameObjectBlueprint3;
							num4 = value;
						}
						else if (value < num3 && value == num4 && 50.in100())
						{
							gameObjectBlueprint = gameObjectBlueprint3;
							num4 = value;
						}
						if (gameObjectBlueprint2 == null)
						{
							gameObjectBlueprint2 = gameObjectBlueprint3;
							num2 = value;
						}
						else if (value < num2)
						{
							gameObjectBlueprint2 = gameObjectBlueprint3;
							num2 = value;
						}
						else if (value == num2 && 50.in100())
						{
							gameObjectBlueprint2 = gameObjectBlueprint3;
							num2 = value;
						}
					}
				}
				GameObjectBlueprint gameObjectBlueprint4 = gameObjectBlueprint ?? gameObjectBlueprint2;
				if (gameObjectBlueprint4 == null)
				{
					Debug.Log("No member found from faction:" + factionName);
					return;
				}
				string name = gameObjectBlueprint4.Name;
				string preferredMutation = randomElement.preferredMutation;
				XRL.World.GameObject gameObject = XRL.World.GameObject.Create(name);
				if (gameObject.Stat("Level", 0) - The.Player.Stat("Level", 0) < 10)
				{
					gameObject.Render.SetForegroundColor(randomElement.mainColor);
					gameObject.Render.DetailColor = "O";
					if (!gameObject.HasProperty("PsychicHunter"))
					{
						gameObject.SetStringProperty("PsychicHunter", "true", false);
					}
					gameObject.Brain.Allegiance.Clear();
					gameObject.Brain.Allegiance.Add("Playerhater", 100);
					gameObject.Brain.Allegiance.Hostile = true;
					gameObject.Brain.Allegiance.Calm = false;
					gameObject.Brain.Hibernating = false;
					gameObject.Brain.Aquatic = false;
					gameObject.Brain.Mobile = true;
					gameObject.Brain.AddOpinion<OpinionPsychicHunt>(The.Player, 1f);
					gameObject.RequirePart<Combat>(false);
					ConversationScript part = gameObject.GetPart<ConversationScript>();
					if (part != null)
					{
						part.Filter = "Weird";
						part.FilterExtras = randomElement.factionName;
					}
					gameObject.AwardXP(The.Player.Stat("XP", 0), -1, 0, int.MaxValue, null, The.Player, null, null, null, null);
					gameObject.GetStat("Ego").BaseValue = The.Player.Stat("Ego", 0);
					if (gameObject.HasStat("MP"))
					{
						gameObject.GetStat("MP").BaseValue = 0;
					}
					Mutations part2 = The.Player.GetPart<Mutations>();
					Mutations part3 = gameObject.GetPart<Mutations>();
					bool flag = false;
					if (preferredMutation != "none")
					{
						part3.AddMutation(preferredMutation, gameObject.Stat("Level", 0) / 4);
					}
					else
					{
						flag = true;
					}
					foreach (BaseMutation baseMutation in part2.MutationList)
					{
						if (!flag)
						{
							flag = true;
						}
						else
						{
							MutationEntry mutationEntry = baseMutation.GetMutationEntry();
							if (((mutationEntry != null) ? mutationEntry.Category : null) != null && mutationEntry.Category.Name == "Mental" && mutationEntry.Cost > 1)
							{
								List<MutationEntry> list = new List<MutationEntry>(gameObject.GetPart<Mutations>().GetMutatePool(null, false));
								list.ShuffleInPlace<MutationEntry>(null);
								MutationEntry mutationEntry2 = null;
								foreach (MutationEntry mutationEntry3 in list)
								{
									if (mutationEntry3.Category != null && mutationEntry3.Category.Name == "Mental" && !gameObject.HasPart(mutationEntry3.Class) && mutationEntry3.Cost > 1)
									{
										mutationEntry2 = mutationEntry3;
										break;
									}
								}
								if (mutationEntry2 != null)
								{
									part3.AddMutation(mutationEntry2.Class, baseMutation.BaseLevel);
								}
							}
						}
					}
					string text = NameMaker.MakeTitle(gameObject, null, null, null, null, null, null, null, null, null, "PsychicHunter", null, true, null, null);
					XRL.World.GameObject gameObject2 = gameObject;
					string text2 = null;
					bool flag2 = false;
					string text3 = "PsychicHunter";
					bool flag3 = true;
					Func<string, string> func = new Func<string, string>(randomElement.Weirdify);
					gameObject2.GiveProperName(text2, flag2, text3, flag3, null, null, null, func);
					Titles titles = gameObject.RequirePart<Titles>(false);
					if (!text.IsNullOrEmpty())
					{
						titles.AddTitle(text, -40);
					}
					titles.AddTitle("extradimensional " + gameObject.GetCreatureType(false, false), -20);
					titles.AddTitle("esper from the " + randomElement.cultForm.Replace("*CultSymbol*", ((char)randomElement.cultSymbol).ToString()), -5);
					gameObject.RequirePart<DisplayNameColor>(false).SetColorAndPriority("O", 30);
					XRL.World.Parts.Temporary.AddHierarchically(gameObject, -1, null, null, false);
					gameObject.RequirePart<AbsorbablePsyche>(false);
					gameObject.AddPart<Extradimensional>(new Extradimensional("{{O|" + randomElement.dimensionName.Replace("*DimensionSymbol*", ((char)randomElement.dimensionSymbol).ToString()) + "}}", randomElement.dimensionalWeaponIndex, randomElement.dimensionalMissileWeaponIndex, randomElement.dimensionalArmorIndex, randomElement.dimensionalShieldIndex, randomElement.dimensionalMiscIndex, randomElement.dimensionalTraining, randomElement.dimensionSecretID), true, false);
					gameObject.RequirePart<ExtradimensionalLoot>(false);
					gameObject.AddPart<RevealObservationOnLook>(new RevealObservationOnLook(randomElement.dimensionSecretID), true, false);
					PsychicHunterSystem.RemoveParts(gameObject);
					if (ObjectList != null)
					{
						ObjectList.Add(gameObject);
					}
					if (Place && PsychicHunterSystem.PlaceHunter(Z, gameObject, null, TeleportSwirl, "&C"))
					{
						num++;
					}
				}
			}
			if (UseMessage && num > 0)
			{
				PsychicHunterSystem.PsychicPresenceMessage(num, UsePopup);
			}
		}

		// Token: 0x060036C4 RID: 14020 RVA: 0x001095EC File Offset: 0x001077EC
		public static bool PlaceHunter(Zone Zone, XRL.World.GameObject Hunter, Cell Cell = null, bool TeleportSwirl = false, string TeleportColor = "&C")
		{
			int num = 0;
			while (num < 100 && Cell == null)
			{
				int num2 = Stat.Random(0, Zone.Width - 1);
				int num3 = Stat.Random(0, Zone.Height - 1);
				Cell cell = Zone.GetCell(num2, num3);
				if (cell.IsSpawnable() && cell.GetNavigationWeightFor(Hunter, false, false, false, false, false) < 30)
				{
					Cell = cell;
				}
				num++;
			}
			if (Cell == null)
			{
				List<Cell> passableCells = Zone.GetPassableCells(Hunter);
				if (passableCells.IsNullOrEmpty<Cell>())
				{
					return false;
				}
				Cell = passableCells.GetRandomElement<Cell>(null);
			}
			Cell.AddObject(Hunter, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
			Hunter.MakeActive(false);
			Hunter.Brain.PushGoal(new Kill(The.Player, false));
			if (TeleportSwirl)
			{
				Hunter.SmallTeleportSwirl(Cell, TeleportColor, true, null, false);
			}
			return true;
		}

		// Token: 0x060036C5 RID: 14021 RVA: 0x001096B4 File Offset: 0x001078B4
		public static void PsychicPresenceMessage(int Number = 1, bool UsePopup = true)
		{
			string text = ((Number > 1) ? "psychic presences" : "a psychic presence");
			Messaging.XDidY(The.Player, "sense", text + " foreign to this place and time", null, null, "c", null, null, false, false, null, null, false, false, false, false, UsePopup, null);
		}

		// Token: 0x060036C6 RID: 14022 RVA: 0x00109700 File Offset: 0x00107900
		public static int GetNumPsychicHunters(int glimmer)
		{
			int num = Stat.Random(1, 1000);
			int num2 = 0;
			if (glimmer < 20)
			{
				return 0;
			}
			if (glimmer >= 20 && glimmer <= 49)
			{
				if (num >= (glimmer - 20 + 15) * 2)
				{
					return 0;
				}
				num2++;
			}
			else
			{
				if ((double)num >= (double)(glimmer - 40 + 105) * 0.666)
				{
					return 0;
				}
				num2++;
			}
			if (glimmer >= 35 && num2 > 0)
			{
				if (Stat.Random(1, 1000) >= (glimmer - 20) * 5)
				{
					return num2;
				}
				num2++;
			}
			if (glimmer >= 50)
			{
				if (Stat.Random(1, 1000) >= (glimmer - 20) * 5)
				{
					return num2;
				}
				num2++;
				if (Stat.Random(1, 1000) >= (glimmer - 20) * 5)
				{
					return num2;
				}
				num2++;
				if (glimmer >= 80)
				{
					if (Stat.Random(1, 1000) >= (glimmer - 50) * 5)
					{
						return num2;
					}
					num2++;
				}
			}
			return num2;
		}

		// Token: 0x060036C7 RID: 14023 RVA: 0x001097E0 File Offset: 0x001079E0
		public static string GetThrallRoll(int glimmer)
		{
			string text;
			if (glimmer <= DimensionManager.GLIMMER_FLOOR + 15)
			{
				text = "0-1";
			}
			else if (glimmer <= DimensionManager.GLIMMER_FLOOR + 30)
			{
				text = "0-2";
			}
			else if (glimmer <= DimensionManager.GLIMMER_FLOOR + 45)
			{
				text = "1-2";
			}
			else if (glimmer <= DimensionManager.GLIMMER_FLOOR + 60)
			{
				text = "1-3";
			}
			else if (glimmer <= DimensionManager.GLIMMER_FLOOR + 75)
			{
				text = "2-3";
			}
			else if (glimmer <= DimensionManager.GLIMMER_FLOOR + 90)
			{
				text = "2-4";
			}
			else
			{
				text = "3-4";
			}
			return text;
		}

		// Token: 0x060036C8 RID: 14024 RVA: 0x0010986C File Offset: 0x00107A6C
		public static string GetPsychicGlimmerDescription(int glimmer)
		{
			string text = "Your psychic glimmer represents how noticeable you are in the vast psychic aether. As your mental mutations increase in level, so does your psychic glimmer and the frequency, strength, and number of those who desire to absorb your mind.";
			string text2 = "";
			string text3;
			if (glimmer <= DimensionManager.GLIMMER_FLOOR + 15)
			{
				text3 = "Currently, you are being watched and pursued by ospreys, Ptoh's servants and birds of psychic prey who pluck larval espers from their egg sacs.";
			}
			else if (glimmer <= DimensionManager.GLIMMER_FLOOR + 30)
			{
				text3 = "Currently, you are being watched and pursued by harriers, Ptoh's servants and birds of psychic prey who pluck fledgling espers from the shallows.";
			}
			else if (glimmer <= DimensionManager.GLIMMER_FLOOR + 45)
			{
				text3 = "Currently, you are being watched and pursued by owls, Ptoh's servants and birds of psychic prey who snatch espers from the nighted weald.";
			}
			else if (glimmer <= DimensionManager.GLIMMER_FLOOR + 60)
			{
				text3 = "Currently, you are being watched and pursued by condors, Ptoh's servants and birds of psychic prey who snatch thriving espers from the vast wood.";
			}
			else if (glimmer <= DimensionManager.GLIMMER_FLOOR + 75)
			{
				text3 = "Currently, you are being watched and pursued by strixes, Ptoh's servants and birds of psychic prey who drink the blood of mature espers.";
			}
			else if (glimmer <= DimensionManager.GLIMMER_FLOOR + 90)
			{
				text3 = "Currently, you are being watched and pursued by eagles, Ptoh's servants and birds of psychic prey who seize powerful espers from their roosts.";
			}
			else
			{
				text3 = "Currently, you are being watched and pursued by rukhs, Ptoh's most powerful servants and birds of psychic prey who seize masterful espers from their belfries.";
			}
			if (glimmer >= DimensionManager.GLIMMER_EXTRADIMENSIONAL_FLOOR)
			{
				text2 = "\n\nYou are also visible to psychic beings from other dimensions.";
			}
			return text + "\n\n" + text3 + text2;
		}

		// Token: 0x060036C9 RID: 14025 RVA: 0x00109918 File Offset: 0x00107B18
		public void CheckPsychicHunters(Zone Z)
		{
			if (Z.IsWorldMap())
			{
				return;
			}
			if (this.Visited.ContainsKey(Z.ZoneID))
			{
				return;
			}
			this.Visited.Add(Z.ZoneID, true);
			int num;
			if (!The.ZoneManager.TryGetZoneProperty<int>(Z.ZoneID, "AmbushChance", out num))
			{
				WorldBlueprint worldBlueprint = Z.ResolveWorldBlueprint();
				num = ((worldBlueprint != null) ? worldBlueprint.PsychicHunterChance : null) ?? 100;
			}
			if (!num.in100())
			{
				return;
			}
			AmbientStabilization part = Z.GetPart<AmbientStabilization>();
			int numPsychicHunters = PsychicHunterSystem.GetNumPsychicHunters(The.Player.GetPsychicGlimmer(null));
			if (numPsychicHunters > 0)
			{
				if (part != null && (part.Strength + 50).in100())
				{
					Messaging.EmitMessage(The.Player, "Some dimensional interlopers attempt to enter this region of spacetime, but the ambient normality field keeps them at bay.", ' ', false, false, false, null, null);
					return;
				}
				this.CreatePsychicHunter(numPsychicHunters, Z);
			}
		}

		// Token: 0x060036CA RID: 14026 RVA: 0x001099F8 File Offset: 0x00107BF8
		public static void RemoveParts(XRL.World.GameObject Object)
		{
			Object.RemovePart(typeof(MentalShield));
			Object.RemovePart(typeof(DroidScramblerWeakness));
			Object.RemovePart(typeof(DromadCaravan));
			Object.RemovePart(typeof(SecretObject));
		}

		// Token: 0x060036CB RID: 14027 RVA: 0x00109A49 File Offset: 0x00107C49
		[WishCommand("seekerhunter", null)]
		public static void SeekerWish()
		{
			PsychicHunterSystem.CreateSeekerHunters(1, The.ActiveZone);
		}

		// Token: 0x060036CC RID: 14028 RVA: 0x00109A58 File Offset: 0x00107C58
		[WishCommand("seekerhunter", null)]
		public static void SeekerWish(string Number)
		{
			int num;
			if (int.TryParse(Number, out num))
			{
				PsychicHunterSystem.CreateSeekerHunters(num, The.ActiveZone);
			}
		}

		// Token: 0x060036CD RID: 14029 RVA: 0x00109A7A File Offset: 0x00107C7A
		[WishCommand("extraculthunter", null)]
		public static void CultWish()
		{
			PsychicHunterSystem.CreateExtradimensionalCultHunters(The.ActiveZone, 1, null, true, true, true, true);
		}

		// Token: 0x060036CE RID: 14030 RVA: 0x00109A8C File Offset: 0x00107C8C
		[WishCommand("extraculthunter", null)]
		public static void CultWish(string Number)
		{
			int num;
			if (int.TryParse(Number, out num))
			{
				PsychicHunterSystem.CreateExtradimensionalCultHunters(The.ActiveZone, num, null, true, true, true, true);
			}
		}

		// Token: 0x060036CF RID: 14031 RVA: 0x00109AB3 File Offset: 0x00107CB3
		[WishCommand("extrasolohunter", null)]
		public static void SoloWish()
		{
			PsychicHunterSystem.CreateExtradimensionalSoloHunters(The.ActiveZone, 1, null, true, true, true, true);
		}

		// Token: 0x060036D0 RID: 14032 RVA: 0x00109AC8 File Offset: 0x00107CC8
		[WishCommand("extrasolohunter", null)]
		public static void SoloWish(string Number)
		{
			int num;
			if (int.TryParse(Number, out num))
			{
				PsychicHunterSystem.CreateExtradimensionalSoloHunters(The.ActiveZone, num, null, true, true, true, true);
			}
		}

		// Token: 0x04001B54 RID: 6996
		[NonSerialized]
		public Dictionary<string, bool> Visited = new Dictionary<string, bool>();
	}
}
