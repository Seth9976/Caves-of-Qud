using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Genkit;
using HistoryKit;
using Qud.API;
using XRL.Messages;
using XRL.Rules;
using XRL.UI;
using XRL.Wish;
using XRL.World.Capabilities;
using XRL.World.Conversations;
using XRL.World.Parts;

namespace XRL.World.Quests
{
	// Token: 0x02000B1D RID: 2845
	[HasWishCommand]
	[Serializable]
	public class LandingPadsSystem : IQuestSystem
	{
		// Token: 0x170008EA RID: 2282
		// (get) Token: 0x06007247 RID: 29255 RVA: 0x002B341D File Offset: 0x002B161D
		public override bool RemoveWithQuest
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06007248 RID: 29256 RVA: 0x002B3420 File Offset: 0x002B1620
		public void updateQuestStatus()
		{
			if (this.stage != 0)
			{
				return;
			}
			Quest quest = this.getQuest();
			StringBuilder stringBuilder = Event.NewStringBuilder(null);
			stringBuilder.Append("The following factions have agreed to provide sanctuary for the slynth.\n");
			if (this.candidateFactions.Count > 0)
			{
				stringBuilder.Append('\n');
			}
			foreach (string text in this.candidateFactions)
			{
				if (The.Game.PlayerReputation.GetLevel(text) >= 2)
				{
					stringBuilder.Compound("{{green|û}} {{white|" + Faction.GetFormattedName(text) + "}}", '\n');
				}
				else
				{
					stringBuilder.Compound("{{red|X}} {{K|" + Faction.GetFormattedName(text) + " [reputation too low]}}", '\n');
				}
			}
			quest.StepsByID["Sanctuary Candidates"].Text = stringBuilder.ToString();
			if (this.candidateFactionsCount() >= LandingPadsSystem.REQUIRED_CANDIDATES)
			{
				The.Game.FinishQuestStep(LandingPadsSystem.QUEST_NAME, "Consult Settlements", -1, true, null);
				return;
			}
			QuestStep questStep = The.Game.Quests[LandingPadsSystem.QUEST_NAME].StepsByID["Consult Settlements"];
			if (questStep.Finished)
			{
				questStep.XP = 0;
				questStep.Finished = false;
			}
		}

		// Token: 0x06007249 RID: 29257 RVA: 0x002B3578 File Offset: 0x002B1778
		public Quest getQuest()
		{
			if (!The.Game.Quests.ContainsKey(LandingPadsSystem.QUEST_NAME))
			{
				return null;
			}
			return The.Game.Quests[LandingPadsSystem.QUEST_NAME];
		}

		// Token: 0x0600724A RID: 29258 RVA: 0x002B35A6 File Offset: 0x002B17A6
		public override void OnAdded()
		{
			this.hydroponZone = The.Game.GetStringGameState("HydroponZoneID", null);
		}

		// Token: 0x0600724B RID: 29259 RVA: 0x002B35BE File Offset: 0x002B17BE
		public override void Register(XRLGame Game, IEventRegistrar Registrar)
		{
			Registrar.Register(PooledEvent<AfterReputationChangeEvent>.ID, 0, false);
			Registrar.Register(ZoneActivatedEvent.ID, 0, false);
			Registrar.Register(PooledEvent<GenericCommandEvent>.ID, 0, false);
		}

		// Token: 0x0600724C RID: 29260 RVA: 0x002B35E7 File Offset: 0x002B17E7
		public override bool HandleEvent(AfterReputationChangeEvent E)
		{
			if (this.stage == 0)
			{
				this.updateQuestStatus();
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600724D RID: 29261 RVA: 0x002B3600 File Offset: 0x002B1800
		public override bool HandleEvent(ZoneActivatedEvent E)
		{
			if (this.SlynthLeaveDays >= 0 && E.Zone.ZoneID == this.hydroponZone)
			{
				this.HydroponActivated(E.Zone);
			}
			if (this.SlynthSettleDays >= 0 && E.Zone.ZoneID == this.settledZone)
			{
				this.SettlementActivated(E.Zone);
			}
			if (this.SlynthLeaveDays == -1 && this.SlynthArriveDays == -1 && this.SlynthSettleDays == -1 && The.Game.HasFinishedQuest(LandingPadsSystem.QUEST_NAME))
			{
				The.Game.FlagSystemForRemoval(this);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600724E RID: 29262 RVA: 0x002B36A8 File Offset: 0x002B18A8
		public override bool HandleEvent(GenericCommandEvent E)
		{
			if (E.Command == "AfterChavvahLocationSet")
			{
				string[] array = (string[])E.Object;
				if (this.settledFaction.IsNullOrEmpty())
				{
					int num = this.candidateFactions.IndexOf("Chavvah");
					if (num >= 0)
					{
						this.candidateFactionZones[num] = array[1];
					}
				}
				else if (this.settledFaction == "Chavvah")
				{
					The.Game.SetStringGameState("SlynthSettlementZone", this.settledZone = array[1]);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600724F RID: 29263 RVA: 0x002B373A File Offset: 0x002B193A
		public int candidateFactionsCount()
		{
			return this.candidateFactions.Count<string>((string f) => The.Game.PlayerReputation.Get(f) >= RuleSettings.REPUTATION_LOVED);
		}

		// Token: 0x06007250 RID: 29264 RVA: 0x002B3768 File Offset: 0x002B1968
		public override void Finish()
		{
			this.RollResult();
			this.stage = 100;
			this.SlynthLeaveDays = 1;
			this.SlynthArriveDays = Stat.Random(5, 7);
			this.SlynthSettleDays = this.SlynthArriveDays + 7;
			this.CandidateCount = this.candidateFactionsCount();
			base.Quest.SetProperty("Faction", this.settledFaction);
			base.Quest.SetProperty("Zone", this.settledZone);
			base.Quest.SetProperty("Count", this.CandidateCount);
			string zoneDisplayName = The.ZoneManager.GetZoneDisplayName(this.settledZone, false, true, false, false);
			JournalAPI.AddAccomplishment("You found a new home for a young people.", "Inwith Maqqom Yd, =name= created a new people from lilypads and coral-stuff, and sent them to live throughout wider Qud.", "Acting against labor laws restricting the rights of newly sentient beings, =name= trekked to the reef-skirted pad of the Hydropon and led the plant people there to their new home, " + zoneDisplayName + ".", null, "general", MuralCategory.BecomesLoved, MuralWeight.Medium, null, -1L, true);
			The.Game.PlayerReputation.Modify(this.settledFaction, -600, "Quest", null, null, false, false);
		}

		// Token: 0x06007251 RID: 29265 RVA: 0x002B385C File Offset: 0x002B1A5C
		public void RollResult()
		{
			if (this.stage < 50)
			{
				this.stage = 50;
			}
			if (this.settledFaction != null)
			{
				return;
			}
			int num;
			do
			{
				num = Stat.Rand.Next(0, this.candidateFactions.Count);
			}
			while (The.Game.PlayerReputation.Get(this.candidateFactions[num]) < RuleSettings.REPUTATION_LOVED);
			The.Game.SetStringGameState("SlynthSettlementFaction", this.settledFaction = this.candidateFactions[num]);
			The.Game.SetStringGameState("SlynthSettlementZone", this.settledZone = this.candidateFactionZones[num]);
		}

		// Token: 0x06007252 RID: 29266 RVA: 0x002B3908 File Offset: 0x002B1B08
		public long GetQuestFinishDays()
		{
			long questFinishTime = The.Game.GetQuestFinishTime(LandingPadsSystem.QUEST_NAME);
			if (questFinishTime < 0L)
			{
				return -1L;
			}
			return (The.Game.TimeTicks - questFinishTime) / 1200L;
		}

		// Token: 0x06007253 RID: 29267 RVA: 0x002B3940 File Offset: 0x002B1B40
		public void HydroponActivated(Zone Z)
		{
			if (this.SlynthLeaveDays >= 0 && this.GetQuestFinishDays() >= (long)this.SlynthLeaveDays && !The.Game.HasIntGameState("LandingPadsSlynthLeft"))
			{
				this.SlynthLeaveDays = -1;
				The.Game.SetIntGameState("LandingPadsSlynthLeft", 1);
				List<GameObject> objects = Z.GetObjects("BaseSlynth");
				int num = Stat.Random(8, 10);
				using (List<GameObject>.Enumerator enumerator = objects.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (enumerator.Current.Destroy(null, true, false, null) && --num <= 0)
						{
							break;
						}
					}
				}
			}
		}

		// Token: 0x06007254 RID: 29268 RVA: 0x002B39F4 File Offset: 0x002B1BF4
		public void GetSlynthCells(Zone Z, out List<Cell> Cells, out int Amount, out bool Wanders)
		{
			if (this.settledFaction == "Pariahs")
			{
				Cells = Z.GetEmptyReachableCells(new Rect2D(43, 18, 46, 22));
				Amount = 2;
				Wanders = false;
				return;
			}
			if (this.settledFaction == "Mechanimists")
			{
				Cells = new List<Cell>(Stat.Random(5, 9));
				Wanders = true;
				for (int i = 0; i < Cells.Capacity; i++)
				{
					int num = Stat.Random(0, Directions.DirectionList.Length);
					string text = ".";
					if (num < Directions.DirectionList.Length)
					{
						text = Directions.DirectionList[num];
					}
					string zoneFromIDAndDirection = The.ZoneManager.GetZoneFromIDAndDirection(Z.ZoneID, text);
					Zone zone = The.ZoneManager.GetZone(zoneFromIDAndDirection);
					for (int j = 0; j < 100; j++)
					{
						Cell randomCell = zone.GetRandomCell(1);
						if (randomCell.IsReachable() && randomCell.IsPassable(null, true) && !Cells.Contains(randomCell))
						{
							Cells.Add(randomCell);
							break;
						}
					}
				}
				Amount = Cells.Count;
				return;
			}
			if (this.settledFaction == "YdFreehold")
			{
				int num2 = ((Z.Z == 10) ? 11 : 10);
				Cells = The.ZoneManager.GetZone(Z.ZoneWorld, Z.wX, Z.wY, Z.X, Z.Y, num2).GetEmptyReachableCells();
				Cells.AddRange(Z.GetEmptyReachableCells());
				Amount = Math.Min(Stat.Random(5, 9), Cells.Count);
				Wanders = true;
				return;
			}
			if (this.settledFaction == "Mopango")
			{
				Cells = Z.GetCells((Cell C) => C.IsEmpty() && C.IsReachable() && C.HasObject("MopangoHideoutTile"));
				Amount = Math.Min(Stat.Random(5, 9), Cells.Count);
				Wanders = true;
				if (Cells.Count < Amount)
				{
					Cells.AddRange(Z.GetEmptyReachableCells());
					return;
				}
			}
			else
			{
				if (this.settledFaction == "Chavvah")
				{
					Cells = new List<Cell>(Stat.Random(5, 9));
					Wanders = true;
					string[] array = new string[3];
					ChavvahSystem system = The.Game.GetSystem<ChavvahSystem>();
					string text2 = ((system != null) ? system.TrunkID : null) ?? Z.ZoneID;
					for (int k = 0; k < 3; k++)
					{
						text2 = (array[k] = The.ZoneManager.GetZoneFromIDAndDirection(text2, "U"));
					}
					for (int l = 0; l < Cells.Capacity; l++)
					{
						string randomElement = array.GetRandomElement<string>(null);
						Zone zone2 = The.ZoneManager.GetZone(randomElement);
						for (int m = 0; m < 100; m++)
						{
							Cell randomCell2 = zone2.GetRandomCell(1);
							if (randomCell2.IsReachable() && randomCell2.IsPassable(null, true) && !Cells.Contains(randomCell2))
							{
								Cells.Add(randomCell2);
								break;
							}
						}
					}
					Amount = Cells.Count;
					return;
				}
				Cells = Z.GetEmptyReachableCells();
				Amount = Math.Min(Stat.Random(5, 9), Cells.Count);
				Wanders = true;
			}
		}

		// Token: 0x06007255 RID: 29269 RVA: 0x002B3D0C File Offset: 0x002B1F0C
		public void SettlementActivated(Zone Z)
		{
			if (this.SlynthArriveDays >= 0 && this.GetQuestFinishDays() >= (long)this.SlynthArriveDays && !The.Game.HasIntGameState("LandingPadsSlynthArrived"))
			{
				this.SlynthArriveDays = -1;
				The.Game.SetIntGameState("LandingPadsSlynthArrived", 1);
				List<Cell> list;
				int num;
				bool flag;
				this.GetSlynthCells(Z, out list, out num, out flag);
				string text = this.settledFaction + "-100";
				for (int i = 0; i < num; i++)
				{
					Cell randomElement = list.GetRandomElement<Cell>(null);
					GameObject gameObject = ((randomElement != null) ? randomElement.AddObject("BaseSlynth", null, null, null, null) : null);
					if (gameObject != null)
					{
						gameObject.RequirePart<ConversationScript>(false).ConversationID = "SlynthSettler";
						gameObject.Brain.Wanders = flag;
						gameObject.Brain.Factions = text;
						gameObject.Brain.Allegiance.Calm = true;
						gameObject.MakeActive(false);
						list.Remove(randomElement);
					}
				}
			}
			if (this.SlynthSettleDays >= 0 && this.GetQuestFinishDays() >= (long)this.SlynthSettleDays && !The.Game.HasIntGameState("LandingPadsSlynthSettled"))
			{
				this.SlynthSettleDays = -1;
				The.Game.SetIntGameState("LandingPadsSlynthSettled", 1);
			}
		}

		// Token: 0x06007256 RID: 29270 RVA: 0x002B3E48 File Offset: 0x002B2048
		public static void AddDynamicVillageConversation(GameObject Speaker)
		{
			if (Speaker == null)
			{
				return;
			}
			ConversationScript part = Speaker.GetPart<ConversationScript>();
			ConversationXMLBlueprint conversationXMLBlueprint = ((part != null) ? part.Blueprint : null);
			if (conversationXMLBlueprint == null)
			{
				MetricsManager.LogWarning(Speaker.DebugName + " has no existing conversation blueprint.");
				return;
			}
			ConversationXMLBlueprint conversationXMLBlueprint2;
			if (!Conversation.Blueprints.TryGetValue("DynamicVillageMayor", out conversationXMLBlueprint2))
			{
				MetricsManager.LogWarning("DynamicVillageMayor blueprint cannot be found.");
				return;
			}
			foreach (ConversationXMLBlueprint conversationXMLBlueprint3 in conversationXMLBlueprint2.Children)
			{
				if (conversationXMLBlueprint3.ID.StartsWith("Slynth") || conversationXMLBlueprint3.Name == "Part")
				{
					conversationXMLBlueprint.AddChild(conversationXMLBlueprint3);
				}
			}
			foreach (ConversationXMLBlueprint conversationXMLBlueprint4 in conversationXMLBlueprint2.GetChild("Welcome").Children)
			{
				if (conversationXMLBlueprint4.ID.StartsWith("Slynth"))
				{
					ConversationsAPI.DistributeChoice(conversationXMLBlueprint, "Start", conversationXMLBlueprint4);
				}
			}
		}

		// Token: 0x06007257 RID: 29271 RVA: 0x002B3F74 File Offset: 0x002B2174
		public static void RevealHydropon()
		{
			JournalAPI.RevealMapNote(JournalAPI.GetMapNote("$hydropon"), false, null);
		}

		// Token: 0x06007258 RID: 29272 RVA: 0x002B3F88 File Offset: 0x002B2188
		public static void SlynthQuestWish(Faction Faction = null, bool Complete = false)
		{
			The.Game.StartQuest(LandingPadsSystem.QUEST_NAME, null, null, null);
			LandingPadsSystem system = The.Game.GetSystem<LandingPadsSystem>();
			if (system.candidateFactions.Count < LandingPadsSystem.REQUIRED_CANDIDATES && The.Game.HasUnfinishedQuest(LandingPadsSystem.QUEST_NAME))
			{
				LandingPadsSystem.SlynthQuestCandidates();
			}
			if (Faction != null)
			{
				int num = system.candidateFactions.IndexOf(Faction.Name);
				The.Game.SetStringGameState("SlynthSettlementFaction", system.settledFaction = ((num >= 0) ? system.candidateFactions[num] : null));
				The.Game.SetStringGameState("SlynthSettlementZone", system.settledZone = ((num >= 0) ? system.candidateFactionZones[num] : null));
			}
			if (Complete || Faction != null)
			{
				The.Game.CompleteQuest(LandingPadsSystem.QUEST_NAME);
			}
		}

		// Token: 0x06007259 RID: 29273 RVA: 0x002B405C File Offset: 0x002B225C
		public static void SlynthQuestCandidates()
		{
			LandingPadsSystem system = The.Game.GetSystem<LandingPadsSystem>();
			system.candidateFactions.Clear();
			system.candidateFactionZones.Clear();
			system.candidateFactions.Add("Joppa");
			system.candidateFactionZones.Add("JoppaWorld.11.22.1.1.10");
			system.candidateFactions.Add("Mechanimists");
			system.candidateFactionZones.Add("JoppaWorld.5.2.1.1.10");
			system.candidateFactions.Add("Barathrumites");
			system.candidateFactionZones.Add("JoppaWorld.22.14.1.0.13");
			system.candidateFactions.Add("Kyakukya");
			system.candidateFactionZones.Add("JoppaWorld.27.20.1.1.10");
			system.candidateFactions.Add("Ezra");
			system.candidateFactionZones.Add("JoppaWorld.53.4.0.0.10");
			system.candidateFactions.Add("Mopango");
			system.candidateFactionZones.Add("JoppaWorld.53.3.0.0.11");
			system.candidateFactions.Add("Pariahs");
			system.candidateFactionZones.Add("JoppaWorld.5.2.1.2.10");
			system.candidateFactions.Add("YdFreehold");
			system.candidateFactionZones.Add("JoppaWorld.67.17.1.1.10");
			system.candidateFactions.Add("Hindren");
			system.candidateFactionZones.Add(The.Game.GetStringGameState("BeyLahZoneID", ""));
			system.candidateFactions.Add("Chavvah");
			system.candidateFactionZones.Add(The.Game.GetSystem<ChavvahSystem>().TrunkID);
			Zone zone = The.ZoneManager.GetZone("JoppaWorld");
			List<VillageTerrain> list = new List<VillageTerrain>();
			for (int i = 0; i < zone.Height; i++)
			{
				for (int j = 0; j < zone.Width; j++)
				{
					GameObject firstObjectWithPart = zone.GetCell(j, i).GetFirstObjectWithPart("VillageTerrain");
					if (firstObjectWithPart != null)
					{
						list.Add(firstObjectWithPart.GetPart<VillageTerrain>());
					}
				}
			}
			list.ShuffleInPlace<VillageTerrain>(null);
			for (int k = 0; k < 3; k++)
			{
				VillageTerrain villageTerrain = list[k];
				Cell currentCell = villageTerrain.ParentObject.CurrentCell;
				HistoricEntitySnapshot currentSnapshot = villageTerrain.Village.GetCurrentSnapshot();
				villageTerrain.FireEvent(Event.New("VillageReveal", 0, 0, 0));
				system.candidateFactions.Add("villagers of " + currentSnapshot.Name);
				system.candidateFactionZones.Add(ZoneID.Assemble("JoppaWorld", currentCell.X, currentCell.Y, 1, 1, 10));
			}
			int num = -The.Player.GetIntProperty("AllVisibleRepModifier", 0);
			foreach (string text in system.candidateFactions)
			{
				The.Game.PlayerReputation.Set(text, RuleSettings.REPUTATION_LOVED + num);
			}
			system.updateQuestStatus();
		}

		// Token: 0x0600725A RID: 29274 RVA: 0x002B4350 File Offset: 0x002B2550
		[WishCommand("slynthquest", null)]
		public static void SlynthQuestWish(string Value)
		{
			Popup.Suppress = true;
			ItemNaming.Suppress = true;
			LandingPadsSystem.SlynthQuestWish(null, false);
			LandingPadsSystem system = The.Game.GetSystem<LandingPadsSystem>();
			if (!Value.Contains("start"))
			{
				if (Value.Contains("complete"))
				{
					LandingPadsSystem.SlynthQuestWish(null, true);
				}
				else if (Value.Contains("reset"))
				{
					LandingPadsSystem.ResetSlynthQuest(system);
				}
				else if (Value.Contains("leave"))
				{
					LandingPadsSystem.SlynthQuestWish(null, true);
					system.SlynthLeaveDays = 0;
					if (The.Player.InZone(The.Game.GetStringGameState("HydroponZoneID", "")))
					{
						system.HydroponActivated(The.ActiveZone);
					}
					else
					{
						The.Player.ZoneTeleport(The.Game.GetStringGameState("HydroponZoneID", ""), -1, -1, null, null, null, null, "You are transported!", true, null);
					}
				}
				else if (Value.Contains("arrive"))
				{
					LandingPadsSystem.SlynthQuestWish(null, true);
					system.SlynthLeaveDays = 0;
					system.SlynthArriveDays = 0;
					if (The.Player.InZone(system.settledZone))
					{
						system.SettlementActivated(The.ActiveZone);
					}
					else
					{
						The.Player.ZoneTeleport(system.settledZone, -1, -1, null, null, null, null, "You are transported!", true, null);
					}
				}
				else if (Value.Contains("settle"))
				{
					LandingPadsSystem.SlynthQuestWish(null, true);
					system.SlynthLeaveDays = 0;
					system.SlynthArriveDays = 0;
					system.SlynthSettleDays = 0;
					if (The.Player.InZone(system.settledZone))
					{
						system.SettlementActivated(The.ActiveZone);
					}
					else
					{
						The.Player.ZoneTeleport(system.settledZone, -1, -1, null, null, null, null, "You are transported!", true, null);
					}
				}
				else
				{
					Faction faction = Factions.Loop().FirstOrDefault<Faction>((Faction f) => f.Name.EqualsNoCase(Value));
					if (faction == null)
					{
						MessageQueue.AddPlayerMessage("No faction found by that name.", null, true);
						return;
					}
					LandingPadsSystem.SlynthQuestWish(faction, false);
				}
			}
			Popup.Suppress = false;
			ItemNaming.Suppress = false;
		}

		// Token: 0x0600725B RID: 29275 RVA: 0x002B456C File Offset: 0x002B276C
		public static void ResetSlynthQuest(LandingPadsSystem System)
		{
			The.Game.Quests.Remove(LandingPadsSystem.QUEST_NAME);
			The.Game.FinishedQuests.Remove(LandingPadsSystem.QUEST_NAME);
			The.Game.RemoveInt64GameState("QuestFinishedTime_" + LandingPadsSystem.QUEST_NAME);
			System.candidateFactions.Clear();
			System.candidateFactionZones.Clear();
			System.stage = 0;
			System.settledZone = null;
			System.settledFaction = null;
			System.SlynthLeaveDays = -1;
			System.SlynthArriveDays = -1;
			System.SlynthSettleDays = -1;
			if (!The.Player.InZone(System.hydroponZone))
			{
				The.Player.ZoneTeleport(System.hydroponZone, -1, -1, null, null, null, null, "You are transported!", true, null);
			}
			Zone activeZone = The.ActiveZone;
			List<Cell> reachableCells = activeZone.GetReachableCells();
			reachableCells.ShuffleInPlace<Cell>(null);
			int i = activeZone.CountObjects("BaseSlynth");
			int num = 0;
			while (i <= 14)
			{
				reachableCells[num++].AddObject("BaseSlynth", null, null, null, null).MakeActive(false);
				i++;
			}
		}

		// Token: 0x0400350F RID: 13583
		public static readonly string QUEST_NAME = "Landing Pads";

		// Token: 0x04003510 RID: 13584
		public static readonly int REQUIRED_CANDIDATES = 3;

		// Token: 0x04003511 RID: 13585
		public int stage;

		// Token: 0x04003512 RID: 13586
		public List<string> candidateFactions = new List<string>();

		// Token: 0x04003513 RID: 13587
		public List<string> candidateFactionZones = new List<string>();

		// Token: 0x04003514 RID: 13588
		public string hydroponZone;

		// Token: 0x04003515 RID: 13589
		public string settledZone;

		// Token: 0x04003516 RID: 13590
		public string settledFaction;

		// Token: 0x04003517 RID: 13591
		public int SlynthLeaveDays = -1;

		// Token: 0x04003518 RID: 13592
		public int SlynthArriveDays = -1;

		// Token: 0x04003519 RID: 13593
		public int SlynthSettleDays = -1;

		// Token: 0x0400351A RID: 13594
		public int CandidateCount = -1;

		// Token: 0x0400351B RID: 13595
		public bool visited;

		// Token: 0x02001B76 RID: 7030
		private enum STAGE
		{
			// Token: 0x04006D2F RID: 27951
			unsettled,
			// Token: 0x04006D30 RID: 27952
			decided = 50,
			// Token: 0x04006D31 RID: 27953
			settled = 100
		}
	}
}
