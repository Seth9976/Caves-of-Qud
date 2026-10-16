using System;
using System.Collections.Generic;
using System.Globalization;
using Qud.API;
using XRL.Language;
using XRL.Rules;
using XRL.UI;
using XRL.World.AI;
using XRL.World.Anatomy;
using XRL.World.Parts;

namespace XRL.World.QuestManagers
{
	// Token: 0x020009BD RID: 2493
	[Serializable]
	public class SpreadPax : IQuestSystem
	{
		// Token: 0x06006B25 RID: 27429 RVA: 0x00227C44 File Offset: 0x00225E44
		private bool hasStepName(string s)
		{
			for (int i = 0; i < this.Steps.Count; i++)
			{
				if (this.Steps[i].Name == s)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06006B26 RID: 27430 RVA: 0x00227C84 File Offset: 0x00225E84
		public void Check()
		{
			GameObject player = The.Player;
			if (((player != null) ? player.CurrentCell : null) == null)
			{
				return;
			}
			for (int i = 0; i < this.Steps.Count; i++)
			{
				if (!this.Steps[i].Finished)
				{
					if (this.Steps[i].Target.StartsWith("Underground:"))
					{
						int num = int.Parse(this.Steps[i].Target.Split(':', StringSplitOptions.None)[1]) + 10;
						Zone currentZone = The.Player.CurrentZone;
						if (currentZone != null && currentZone.Z >= num)
						{
							The.Game.FinishQuestStep(this.MyQuestID, this.Steps[i].Name, -1, true, null);
							this.Steps[i].Finished = true;
						}
					}
					else
					{
						if (this.Steps[i].Target.StartsWith("Faction:"))
						{
							string text = this.Steps[i].Target.Split(':', StringSplitOptions.None)[1];
							using (List<Cell>.Enumerator enumerator = The.Player.CurrentCell.GetLocalAdjacentCells(3, true).GetEnumerator())
							{
								while (enumerator.MoveNext())
								{
									Cell cell = enumerator.Current;
									for (int j = 0; j < cell.Objects.Count; j++)
									{
										Brain brain = cell.Objects[j].Brain;
										AllegianceSet allegianceSet = ((brain != null) ? brain.GetBaseAllegiance() : null);
										int num2;
										if (allegianceSet != null && allegianceSet.TryGetValue(text, out num2) && num2 > 0)
										{
											The.Game.FinishQuestStep(this.MyQuestID, this.Steps[i].Name, -1, true, null);
											this.Steps[i].Finished = true;
											goto IL_03BD;
										}
									}
								}
								goto IL_03BD;
							}
						}
						if (this.Steps[i].Target.StartsWith("Item:"))
						{
							string text2 = this.Steps[i].Target.Split(':', StringSplitOptions.None)[1];
							using (List<Cell>.Enumerator enumerator = The.Player.CurrentCell.GetLocalAdjacentCells(3, true).GetEnumerator())
							{
								while (enumerator.MoveNext())
								{
									Cell cell2 = enumerator.Current;
									for (int k = 0; k < cell2.Objects.Count; k++)
									{
										if (cell2.Objects[k].Blueprint == text2 || RandomAltarBaetyl.DisplayNameMatches(cell2.Objects[k].Blueprint, text2))
										{
											The.Game.FinishQuestStep(this.MyQuestID, this.Steps[i].Name, -1, true, null);
											this.Steps[i].Finished = true;
											goto IL_03BD;
										}
									}
								}
								goto IL_03BD;
							}
						}
						if (this.Steps[i].Target.StartsWith("Place:") && The.Player.CurrentZone != null)
						{
							string text3 = this.Steps[i].Target.Split(':', StringSplitOptions.None)[1];
							string text4 = WorldFactory.Factory.ZoneDisplayName(The.Player.CurrentZone.ZoneID);
							string text5;
							if (text4.Contains(text3, CompareOptions.IgnoreCase) || (SpreadPax.paxPlacesAlias.TryGetValue(text3, out text5) && text4.Contains(text5, CompareOptions.IgnoreCase)))
							{
								The.Game.FinishQuestStep(this.MyQuestID, this.Steps[i].Name, -1, true, null);
								this.Steps[i].Finished = true;
							}
						}
					}
				}
				IL_03BD:;
			}
			int num3 = 0;
			for (int l = 0; l < this.Steps.Count; l++)
			{
				if (this.Steps[l].Finished)
				{
					num3++;
				}
			}
			if (num3 >= 4)
			{
				The.Game.FinishQuest(this.MyQuestID);
			}
		}

		// Token: 0x06006B27 RID: 27431 RVA: 0x002280C8 File Offset: 0x002262C8
		public void Init()
		{
			if (this.Steps != null)
			{
				return;
			}
			this.Steps = new List<PaxQuestStep>();
			Stat.ReseedFrom("SpreadPax", false);
			List<string> list = new List<string>(SpreadPax.paxPlaces);
			if (!The.Game.GetStringGameState("embark", "").Contains("Joppa"))
			{
				list.RemoveAll(new Predicate<string>(SpreadPax.paxPlacesExcludeAltStarts.Contains<string>));
			}
			int num = 6;
			while (this.Steps.Count < num)
			{
				PaxQuestStep paxQuestStep = new PaxQuestStep();
				int num2 = Stat.Random(1, 4);
				if (num2 == 1)
				{
					paxQuestStep.Name = "Spread Klanq Deep in the Earth";
					paxQuestStep.Target = "Underground:20";
					paxQuestStep.Text = "Puff Klanq spores at a depth of at least 20 levels.";
				}
				else if (num2 == 2)
				{
					Faction randomFactionWithAtLeastOneMember = Factions.GetRandomFactionWithAtLeastOneMember((Faction f) => !f.Name.Contains("villagers of"));
					TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
					paxQuestStep.Name = "Spread Klanq to " + textInfo.ToTitleCase(randomFactionWithAtLeastOneMember.DisplayName);
					paxQuestStep.Target = "Faction:" + randomFactionWithAtLeastOneMember.Name;
					paxQuestStep.Text = "Puff Klanq spores on a sentient member of the " + randomFactionWithAtLeastOneMember.DisplayName + " faction.";
				}
				else if (num2 == 3)
				{
					string anObjectBlueprint = EncountersAPI.GetAnObjectBlueprint((GameObjectBlueprint ob) => ob.GetPartParameter<bool>("Physics", "IsReal", true) && ob.GetPartParameter<bool>("Physics", "Takeable", true) && !ob.HasPart("Brain") && !ob.HasPart("Combat") && !ob.HasTag("NoSparkingQuest", false));
					GameObject gameObject = GameObject.CreateSample(anObjectBlueprint);
					TextInfo textInfo2 = new CultureInfo("en-US", false).TextInfo;
					paxQuestStep.Name = "Spread Klanq onto " + textInfo2.ToTitleCase(gameObject.GetReferenceDisplayName(int.MaxValue, null, null, false, true, false, false, true, false, true, false, null, false));
					paxQuestStep.Target = "Item:" + anObjectBlueprint;
					paxQuestStep.Text = "Puff Klanq spores onto " + gameObject.GetReferenceDisplayName(int.MaxValue, null, null, false, true, false, false, true, false, true, false, null, false);
				}
				else if (num2 == 4)
				{
					string randomElement = list.GetRandomElement<string>(null);
					string text;
					if (!SpreadPax.paxPlacesDisplay.TryGetValue(randomElement, out text))
					{
						text = randomElement;
					}
					string text2;
					if (!SpreadPax.paxPlacesPreposition.TryGetValue(randomElement, out text2))
					{
						text2 = "in";
					}
					paxQuestStep.Name = "Spread Klanq " + text2 + " " + text;
					paxQuestStep.Target = "Place:" + randomElement;
					paxQuestStep.Text = "Puff Klanq spores in the vicinity of " + text + ".";
				}
				if (!this.hasStepName(paxQuestStep.Name))
				{
					this.Steps.Add(paxQuestStep);
				}
			}
		}

		// Token: 0x06006B28 RID: 27432 RVA: 0x00228362 File Offset: 0x00226562
		public override void RegisterPlayer(GameObject Player, IEventRegistrar Registrar)
		{
			Registrar.Register(PooledEvent<CommandEvent>.ID, 0, false);
		}

		// Token: 0x06006B29 RID: 27433 RVA: 0x00228371 File Offset: 0x00226571
		public override bool HandleEvent(CommandEvent E)
		{
			if (E.Command == "ActivatePaxInfection")
			{
				this.Check();
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06006B2A RID: 27434 RVA: 0x00228394 File Offset: 0x00226594
		public static bool StartQuest()
		{
			try
			{
				Quest quest = new Quest();
				SpreadPax spreadPax = The.Game.RequireSystem<SpreadPax>();
				spreadPax.Init();
				quest.ID = Guid.NewGuid().ToString();
				quest.System = spreadPax;
				quest.Name = SpreadPax.questName;
				quest.Level = 25;
				quest.Finished = false;
				quest.Accomplishment = "Conspiring with its eponymous mushroom scientist, you spread Klanq throughout Qud.";
				quest.Hagiograph = string.Concat(new string[]
				{
					"Bless the ",
					Calendar.GetDay(),
					" of ",
					Calendar.GetMonth(),
					", when =name= cemented a historic alliance with the godhead Klanq and the two became one! Together the being known as Klanq-=name= puffed the Royal Vapor into every nook and crevice of Qud."
				});
				quest.HagiographCategory = "DoesSomethingRad";
				quest.StepsByID = new Dictionary<string, QuestStep>();
				spreadPax.MyQuestID = quest.ID;
				for (int i = 0; i < spreadPax.Steps.Count; i++)
				{
					QuestStep questStep = new QuestStep();
					questStep.ID = Guid.NewGuid().ToString();
					questStep.Name = spreadPax.Steps[i].Name;
					questStep.Finished = false;
					questStep.Text = spreadPax.Steps[i].Text;
					questStep.XP = 1500;
					spreadPax.Steps[i].Name = questStep.ID;
					quest.StepsByID.Add(questStep.ID, questStep);
				}
				The.Game.StartQuest(quest, "Pax Klanq", null, null);
			}
			catch (Exception ex)
			{
				MetricsManager.LogException("SpreadPax.StartQuest", ex, "game_exception");
			}
			return true;
		}

		// Token: 0x06006B2B RID: 27435 RVA: 0x0022854C File Offset: 0x0022674C
		public override void Finish()
		{
			Body body = The.Player.Body;
			if (body == null)
			{
				return;
			}
			foreach (BodyPart bodyPart in body.LoopParts())
			{
				GameObject equipped = bodyPart.Equipped;
				if (!(((equipped != null) ? equipped.Blueprint : null) != "PaxInfection") && bodyPart.Equipped.Destroy(null, true, false, null))
				{
					string ordinalName = bodyPart.GetOrdinalName();
					string possessiveAdjective = The.Player.GetPronounProvider(false).PossessiveAdjective;
					Popup.Show("The infected crust of skin on your " + ordinalName + " loosens and breaks away.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
					JournalAPI.AddAccomplishment("To the dismay of fungi everywhere, you cured the Pax Klanq infection on your " + ordinalName + ".", string.Concat(new string[]
					{
						"Bless the ",
						Calendar.GetDay(),
						" of ",
						Calendar.GetMonth(),
						", when =name= dissolved a sham alliance with the treacherous fungus Klanq by eradicating it from ",
						possessiveAdjective,
						" ",
						ordinalName,
						"!"
					}), string.Concat(new string[]
					{
						"While traveling around ",
						The.Player.CurrentZone.GetTerrainDisplayName(true),
						", =name= stumbled upon a clan of fungi performing a secret ritual. Because of  ",
						Grammar.MakePossessive(The.Player.BaseDisplayNameStripped),
						" <spice.elements.",
						The.Player.GetMythicDomain(5, null),
						".quality.!random>, they furiously rebuked ",
						The.Player.GetPronounProvider(false).Objective,
						" and dissolved the Klanq on ",
						Grammar.MakePossessive(The.Player.BaseDisplayNameStripped),
						" ",
						ordinalName,
						"."
					}), null, "general", MuralCategory.BodyExperienceNeutral, MuralWeight.Medium, null, -1L, true);
				}
			}
		}

		// Token: 0x06006B2C RID: 27436 RVA: 0x00228738 File Offset: 0x00226938
		public override GameObject GetInfluencer()
		{
			return GameObject.FindByBlueprint("Pax Klanq");
		}

		// Token: 0x0400310F RID: 12559
		public List<PaxQuestStep> Steps;

		// Token: 0x04003110 RID: 12560
		public string MyQuestID;

		// Token: 0x04003111 RID: 12561
		[NonSerialized]
		public static string[] paxPlaces = new string[]
		{
			"Joppa", "Kyakukya", "Asphalt Mines", "Rusted Archway", "Rustwell", "Red Rock", "Grit Gate", "Six Day Stilt", "Golgotha", "Bethesda Susa",
			"Omonporch"
		};

		// Token: 0x04003112 RID: 12562
		[NonSerialized]
		public static string[] paxPlacesExcludeAltStarts = new string[] { "Joppa" };

		// Token: 0x04003113 RID: 12563
		[NonSerialized]
		public static Dictionary<string, string> paxPlacesPreposition = new Dictionary<string, string>
		{
			{ "Rusted Archway", "at" },
			{ "Rustwell", "at" },
			{ "Six Day Stilt", "at" }
		};

		// Token: 0x04003114 RID: 12564
		[NonSerialized]
		public static Dictionary<string, string> paxPlacesDisplay = new Dictionary<string, string>
		{
			{ "Asphalt Mines", "the Asphalt Mines" },
			{ "Rusted Archway", "the Rusted Archway" },
			{ "Rustwell", "the Rust Wells" },
			{ "Six Day Stilt", "the Six Day Stilt" }
		};

		// Token: 0x04003115 RID: 12565
		[NonSerialized]
		public static Dictionary<string, string> paxPlacesAlias = new Dictionary<string, string> { { "Six Day Stilt", "Stiltgrounds" } };

		// Token: 0x04003116 RID: 12566
		[NonSerialized]
		public static string questName = "Spread Klanq around Qud in 4 Ways of Your Choosing";
	}
}
