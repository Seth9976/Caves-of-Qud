using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using ConsoleLib.Console;
using Genkit;
using HistoryKit;
using LaundryBear.PlatformServices;
using Platform;
using Platform.IO;
using Qud.API;
using UnityEngine;
using XRL.Collections;
using XRL.Core;
using XRL.Messages;
using XRL.Names;
using XRL.Rules;
using XRL.UI;
using XRL.World;
using XRL.World.Capabilities;
using XRL.World.Effects;
using XRL.World.Parts;
using XRL.World.Parts.Mutation;

namespace XRL
{
	// Token: 0x020004DE RID: 1246
	[Serializable]
	public class XRLGame : IEventSource, IEventHandler
	{
		// Token: 0x06003560 RID: 13664 RVA: 0x001015C2 File Offset: 0x000FF7C2
		public XRLGame()
			: this(XRLCore._Console, XRLCore._Buffer)
		{
		}

		// Token: 0x06003561 RID: 13665 RVA: 0x001015D4 File Offset: 0x000FF7D4
		public XRLGame(TextConsole _Console, ScreenBuffer _Buffer)
		{
			XRLGame.Console = _Console;
			XRLGame.Buffer = _Buffer;
			this.ActionManager = new ActionManager();
			this.ActionManager.ActionQueue.Enqueue(null);
			this.ZoneManager = new ZoneManager(this);
			this.PlayerReputation = new Reputation();
			this.WallTime = new Stopwatch();
		}

		// Token: 0x170005AF RID: 1455
		// (get) Token: 0x06003562 RID: 13666 RVA: 0x001016EF File Offset: 0x000FF8EF
		// (set) Token: 0x06003563 RID: 13667 RVA: 0x001016F7 File Offset: 0x000FF8F7
		public string DeathCategory
		{
			get
			{
				return this._DeathCategory;
			}
			set
			{
				this._DeathCategory = value;
			}
		}

		// Token: 0x170005B0 RID: 1456
		// (get) Token: 0x06003564 RID: 13668 RVA: 0x00101700 File Offset: 0x000FF900
		// (set) Token: 0x06003565 RID: 13669 RVA: 0x0010170E File Offset: 0x000FF90E
		public bool IsSeeded
		{
			get
			{
				return this.Flags.HasBit(1);
			}
			set
			{
				(ref this.Flags).SetBit(1, value);
			}
		}

		// Token: 0x170005B1 RID: 1457
		// (get) Token: 0x06003566 RID: 13670 RVA: 0x0010171D File Offset: 0x000FF91D
		// (set) Token: 0x06003567 RID: 13671 RVA: 0x0010172B File Offset: 0x000FF92B
		public bool IsCoda
		{
			get
			{
				return this.Flags.HasBit(2);
			}
			set
			{
				(ref this.Flags).SetBit(2, value);
			}
		}

		// Token: 0x170005B2 RID: 1458
		// (get) Token: 0x06003568 RID: 13672 RVA: 0x0010173C File Offset: 0x000FF93C
		public bool AlternateStart
		{
			get
			{
				bool flag = this._AlternateStart.GetValueOrDefault();
				if (this._AlternateStart == null)
				{
					flag = !this.GetStringGameState("embark", "Joppa").EqualsNoCase("Joppa");
					this._AlternateStart = new bool?(flag);
					return flag;
				}
				return flag;
			}
		}

		// Token: 0x06003569 RID: 13673 RVA: 0x0010178F File Offset: 0x000FF98F
		public T GetSystem<T>() where T : IGameSystem
		{
			return this.GetSystem(typeof(T)) as T;
		}

		// Token: 0x0600356A RID: 13674 RVA: 0x001017AC File Offset: 0x000FF9AC
		public IGameSystem GetSystem(Type Type)
		{
			int i = 0;
			int count = this.Systems.Count;
			while (i < count)
			{
				if (this.Systems[i].GetType() == Type)
				{
					return this.Systems[i];
				}
				i++;
			}
			return null;
		}

		// Token: 0x0600356B RID: 13675 RVA: 0x001017F4 File Offset: 0x000FF9F4
		public T RequireSystem<T>(Func<T> Generator) where T : IGameSystem
		{
			int i = 0;
			int count = this.Systems.Count;
			while (i < count)
			{
				if (this.Systems[i].GetType() == typeof(T))
				{
					if (this.RemovedSystems != null && this.RemovedSystems.Contains(this.Systems[i]))
					{
						this.RemovedSystems.Remove(this.Systems[i]);
					}
					return this.Systems[i] as T;
				}
				i++;
			}
			return this.AddSystem(Generator(), true) as T;
		}

		// Token: 0x0600356C RID: 13676 RVA: 0x001018A8 File Offset: 0x000FFAA8
		public T RequireSystem<T>() where T : IGameSystem, new()
		{
			int i = 0;
			int count = this.Systems.Count;
			while (i < count)
			{
				if (this.Systems[i].GetType() == typeof(T))
				{
					return this.Systems[i] as T;
				}
				i++;
			}
			return (T)((object)this.AddSystem(new T(), true));
		}

		// Token: 0x0600356D RID: 13677 RVA: 0x0010191C File Offset: 0x000FFB1C
		public IGameSystem RequireSystem(string TypeID)
		{
			Type type = ModManager.ResolveType(TypeID, false, false, true);
			if (type == null)
			{
				throw new TypeUnloadedException(TypeID);
			}
			return this.RequireSystem(type);
		}

		// Token: 0x0600356E RID: 13678 RVA: 0x00101944 File Offset: 0x000FFB44
		public IGameSystem RequireSystem(Type Type)
		{
			int i = 0;
			int count = this.Systems.Count;
			while (i < count)
			{
				if (this.Systems[i].GetType() == Type)
				{
					return this.Systems[i];
				}
				i++;
			}
			return this.AddSystem((IGameSystem)Activator.CreateInstance(Type, true), true);
		}

		// Token: 0x0600356F RID: 13679 RVA: 0x001019A2 File Offset: 0x000FFBA2
		public void RegisterEvent(IEventHandler Handler, int EventID, int Order = 0, bool Serialize = false)
		{
			this.RegisteredEvents.Register(Handler, EventID, Order, Serialize);
		}

		// Token: 0x06003570 RID: 13680 RVA: 0x001019B4 File Offset: 0x000FFBB4
		public void UnregisterEvent(IEventHandler Handler, int EventID)
		{
			this.RegisteredEvents.Unregister(Handler, EventID);
		}

		// Token: 0x06003571 RID: 13681 RVA: 0x001019C4 File Offset: 0x000FFBC4
		public bool WantEvent(int ID, int Cascade)
		{
			return this.RegisteredEvents.ContainsKey(ID);
		}

		// Token: 0x06003572 RID: 13682 RVA: 0x001019D2 File Offset: 0x000FFBD2
		public bool HandleEvent(MinEvent E)
		{
			return this.RegisteredEvents.Dispatch(E);
		}

		// Token: 0x06003573 RID: 13683 RVA: 0x001019E5 File Offset: 0x000FFBE5
		public bool HandleEvent(MinEvent E, bool DispatchPlayer)
		{
			if (!this.RegisteredEvents.Dispatch(E))
			{
				return false;
			}
			if (DispatchPlayer && this.Player._Body != null)
			{
				this.Player._Body.HandleEvent(E);
			}
			return true;
		}

		// Token: 0x06003574 RID: 13684 RVA: 0x00101A1C File Offset: 0x000FFC1C
		public void RemoveFlaggedSystems()
		{
			for (int i = 0; i < this.RemovedSystems.Count; i++)
			{
				this.RemoveSystem(this.RemovedSystems[i]);
			}
			this.RemovedSystems.Clear();
			this.HasRemovedSystems = false;
		}

		// Token: 0x06003575 RID: 13685 RVA: 0x00101A63 File Offset: 0x000FFC63
		public void FlagSystemForRemoval(IGameSystem System)
		{
			this.RemovedSystems.Add(System);
			this.HasRemovedSystems = true;
		}

		// Token: 0x06003576 RID: 13686 RVA: 0x00101A78 File Offset: 0x000FFC78
		public void FlagSystemsForRemoval(Type Type)
		{
			for (int i = 0; i < this.Systems.Count; i++)
			{
				if (this.Systems[i].GetType() == Type)
				{
					this.FlagSystemForRemoval(this.Systems[i]);
				}
			}
		}

		// Token: 0x06003577 RID: 13687 RVA: 0x00101AC4 File Offset: 0x000FFCC4
		public IGameSystem AddSystem(string system)
		{
			IGameSystem gameSystem = ModManager.CreateInstance<IGameSystem>(system);
			if (gameSystem == null)
			{
				MetricsManager.LogError("Unknown system class: " + system);
				return null;
			}
			return this.AddSystem(gameSystem, true);
		}

		// Token: 0x06003578 RID: 13688 RVA: 0x00101AF5 File Offset: 0x000FFCF5
		public IGameSystem AddSystem(IGameSystem System, bool DoRegistration = true)
		{
			this.Systems.Add(System);
			System.Removed = false;
			System.OnAdded();
			if (DoRegistration)
			{
				System.ApplyRegistrar(this);
			}
			return System;
		}

		// Token: 0x06003579 RID: 13689 RVA: 0x00101B1B File Offset: 0x000FFD1B
		public void RemoveSystem(IGameSystem System)
		{
			if (this.Systems.Remove(System))
			{
				System.Removed = true;
				System.OnRemoved();
				System.ApplyUnregistrar(this);
			}
		}

		// Token: 0x0600357A RID: 13690 RVA: 0x00101B3F File Offset: 0x000FFD3F
		public void Release()
		{
			this.Running = false;
			if (this.ZoneManager != null)
			{
				this.ZoneManager.Release();
			}
			if (this.ActionManager != null)
			{
				this.ActionManager.Release();
			}
		}

		// Token: 0x0600357B RID: 13691 RVA: 0x00101B6E File Offset: 0x000FFD6E
		public void Clean()
		{
			this.RegisteredEvents.Clean();
			GamePlayer player = this.Player;
			if (player == null)
			{
				return;
			}
			XRL.World.GameObject body = player._Body;
			if (body == null)
			{
				return;
			}
			EventRegistry registeredEvents = body.RegisteredEvents;
			if (registeredEvents == null)
			{
				return;
			}
			registeredEvents.Clean();
		}

		// Token: 0x0600357C RID: 13692 RVA: 0x00101BA1 File Offset: 0x000FFDA1
		public bool HasObjectGameState(string Value)
		{
			return this.ObjectGameState.ContainsKey(Value);
		}

		// Token: 0x0600357D RID: 13693 RVA: 0x00101BAF File Offset: 0x000FFDAF
		public bool HasIntGameState(string Value)
		{
			return this.IntGameState.ContainsKey(Value);
		}

		// Token: 0x0600357E RID: 13694 RVA: 0x00101BBD File Offset: 0x000FFDBD
		public bool HasInt64GameState(string Value)
		{
			return this.Int64GameState.ContainsKey(Value);
		}

		// Token: 0x0600357F RID: 13695 RVA: 0x00101BCB File Offset: 0x000FFDCB
		public bool HasStringGameState(string Value)
		{
			return this.StringGameState.ContainsKey(Value);
		}

		// Token: 0x06003580 RID: 13696 RVA: 0x00101BD9 File Offset: 0x000FFDD9
		public bool HasBooleanGameState(string Value)
		{
			return this.BooleanGameState.ContainsKey(Value);
		}

		// Token: 0x06003581 RID: 13697 RVA: 0x00101BE7 File Offset: 0x000FFDE7
		public bool HasBlueprintBeenSeen(string Name)
		{
			return this.BlueprintsSeen.Contains(Name);
		}

		// Token: 0x06003582 RID: 13698 RVA: 0x00101BF5 File Offset: 0x000FFDF5
		public void BlueprintSeen(string Name)
		{
			this.BlueprintsSeen.Add(Name);
		}

		// Token: 0x06003583 RID: 13699 RVA: 0x00101C04 File Offset: 0x000FFE04
		public void IncrementIntGameState(string Value, int Amount)
		{
			if (!this.IntGameState.ContainsKey(Value))
			{
				this.IntGameState.Add(Value, 0);
			}
			Dictionary<string, int> intGameState = this.IntGameState;
			intGameState[Value] += Amount;
		}

		// Token: 0x06003584 RID: 13700 RVA: 0x00101C48 File Offset: 0x000FFE48
		public void IncrementInt64GameState(string Value, long Amount)
		{
			if (!this.Int64GameState.ContainsKey(Value))
			{
				this.Int64GameState.Add(Value, 0L);
			}
			Dictionary<string, long> int64GameState = this.Int64GameState;
			int64GameState[Value] += Amount;
		}

		// Token: 0x06003585 RID: 13701 RVA: 0x00101C8C File Offset: 0x000FFE8C
		public bool ToggleBooleanGameState(string Value)
		{
			if (!this.BooleanGameState.ContainsKey(Value))
			{
				this.BooleanGameState.Add(Value, false);
			}
			return this.BooleanGameState[Value] = !this.BooleanGameState[Value];
		}

		// Token: 0x06003586 RID: 13702 RVA: 0x00101CD4 File Offset: 0x000FFED4
		public void PopupStartQuest(Quest Quest)
		{
			IQuestSystem system = Quest.System;
			if (system != null)
			{
				system.ShowStartPopup();
				return;
			}
			Quest.ShowStartPopup();
		}

		// Token: 0x06003587 RID: 13703 RVA: 0x00101CF8 File Offset: 0x000FFEF8
		public Quest StartQuest(Quest Quest, string QuestGiverName = null, string QuestGiverLocationName = null, string QuestGiverLocationZoneID = null)
		{
			Quest quest;
			if (this.TryGetQuest(Quest.ID, out quest))
			{
				return quest;
			}
			SoundManager.PlayUISound("Sounds/Misc/sfx_quest_gain", 1f, false, false, SoundRequest.SoundEffectType.None);
			this.Quests.Add(Quest.ID, Quest);
			IQuestSystem questSystem = Quest.InitializeSystem();
			if (QuestGiverName != null)
			{
				Quest.QuestGiverName = QuestGiverName;
			}
			if (QuestGiverLocationName != null)
			{
				Quest.QuestGiverLocationName = QuestGiverLocationName;
			}
			if (QuestGiverLocationZoneID != null)
			{
				Quest.QuestGiverLocationZoneID = QuestGiverLocationZoneID;
			}
			if (Quest.QuestGiverLocationName == null)
			{
				Zone currentZone = The.Player.CurrentZone;
				Quest.QuestGiverLocationName = ((currentZone != null) ? currentZone.DisplayName : null);
			}
			if (Quest.QuestGiverLocationZoneID == null)
			{
				Zone currentZone2 = The.Player.CurrentZone;
				Quest.QuestGiverLocationZoneID = ((currentZone2 != null) ? currentZone2.ZoneID : null);
			}
			this.PopupStartQuest(Quest);
			if (questSystem != null)
			{
				questSystem.Start();
			}
			QuestManager manager = Quest.Manager;
			if (manager != null)
			{
				manager.OnQuestAdded();
			}
			QuestStartedEvent.Send(Quest);
			MetricsManager.LogEvent("Quest:Start:" + Quest.ID);
			QuestManager manager2 = Quest.Manager;
			if (manager2 != null)
			{
				manager2.AfterQuestAdded();
			}
			return Quest;
		}

		// Token: 0x06003588 RID: 13704 RVA: 0x00101DFC File Offset: 0x000FFFFC
		public Quest StartQuest(string QuestID, string QuestGiverName = null, string QuestGiverLocationName = null, string QuestGiverLocationZoneID = null)
		{
			Quest quest;
			if (this.TryGetQuest(QuestID, out quest))
			{
				return quest;
			}
			Quest quest2;
			if (QuestLoader.Loader.QuestsByID.TryGetValue(QuestID, out quest2) || DynamicQuestsGameState.Instance.Quests.TryGetValue(QuestID, out quest2))
			{
				return this.StartQuest(quest2.Copy(), QuestGiverName, QuestGiverLocationName, QuestGiverLocationZoneID);
			}
			return null;
		}

		// Token: 0x06003589 RID: 13705 RVA: 0x00101E50 File Offset: 0x00100050
		public void CompleteQuest(string questID)
		{
			if (this.FinishedQuests.ContainsKey(questID))
			{
				return;
			}
			if (!this.HasQuest(questID))
			{
				if (!QuestLoader.Loader.QuestsByID.ContainsKey(questID))
				{
					return;
				}
				this.StartQuest(questID, null, null, null);
			}
			foreach (string text in this.Quests[questID].StepsByID.Keys)
			{
				this.FinishQuestStep(questID, text, -1, true, null);
			}
		}

		// Token: 0x0600358A RID: 13706 RVA: 0x00101EF0 File Offset: 0x001000F0
		public long GetQuestFinishTime(string QuestID)
		{
			return this.GetInt64GameState("QuestFinishedTime_" + QuestID, -1L);
		}

		/// <summary>
		/// This adds a quest to finished even if it was never started.
		/// </summary>
		/// <param name="QuestID" />
		// Token: 0x0600358B RID: 13707 RVA: 0x00101F08 File Offset: 0x00100108
		public void ForceFinishQuest(string QuestID)
		{
			Quest quest;
			if (this.Quests.TryGetValue(QuestID, out quest))
			{
				this.FinishQuest(quest);
				return;
			}
			this.FinishedQuests.Add(QuestID, new Quest());
		}

		// Token: 0x0600358C RID: 13708 RVA: 0x00101F40 File Offset: 0x00100140
		public void FinishQuest(string QuestID)
		{
			Quest quest;
			if (this.Quests.TryGetValue(QuestID, out quest))
			{
				this.FinishQuest(quest);
			}
		}

		// Token: 0x0600358D RID: 13709 RVA: 0x00101F64 File Offset: 0x00100164
		public void PopupFinishQuest(Quest Quest)
		{
			IQuestSystem system = Quest.System;
			if (system != null)
			{
				system.ShowFinishPopup();
				return;
			}
			Quest.ShowFinishPopup();
		}

		// Token: 0x0600358E RID: 13710 RVA: 0x00101F88 File Offset: 0x00100188
		public void FinishQuest(Quest Quest)
		{
			if (Quest.Finished || this.FinishedQuests.ContainsKey(Quest.ID))
			{
				return;
			}
			SoundManager.PlayUISound("Sounds/Misc/sfx_quest_total_complete", 1f, false, false, SoundRequest.SoundEffectType.None);
			Quest.Finished = true;
			this.FinishedQuests.Add(Quest.ID, Quest);
			this.SetInt64GameState("QuestFinishedTime_" + Quest.ID, XRL.World.Calendar.TotalTimeTicks);
			this.PopupFinishQuest(Quest);
			try
			{
				Quest.Finish();
			}
			catch (Exception ex)
			{
				MetricsManager.LogException("Quest::FinishQuest@" + Quest.ID, ex, "game_exception");
			}
			QuestManager manager = Quest.Manager;
			if (manager != null)
			{
				manager.OnQuestComplete();
			}
			if (The.Player != null)
			{
				ItemNaming.Opportunity(The.Player, null, Quest.GetInfluencer(), null, "Quest", 6, 0, 0, 1, false);
			}
			try
			{
				Quest.FinishPost();
			}
			catch (Exception ex2)
			{
				MetricsManager.LogException("Quest::FinishQuest@" + Quest.ID, ex2, "game_exception");
			}
			if (!Quest.Accomplishment.IsNullOrEmpty())
			{
				IPronounProvider pronounProvider = The.Player.GetPronounProvider(false);
				string text = ((!Quest.Hagiograph.IsNullOrEmpty()) ? Quest.Hagiograph : Quest.Accomplishment);
				text = text.StartReplace().AddReplacer("them", pronounProvider.Objective).AddReplacer("their", pronounProvider.PossessiveAdjective)
					.ToString();
				string text2 = ((!Quest.HagiographCategory.IsNullOrEmpty()) ? Quest.HagiographCategory : "DoesSomethingRad");
				MuralWeight muralWeight = MuralWeight.High;
				if (Quest.Hagiograph.IsNullOrEmpty())
				{
					muralWeight = MuralWeight.Nil;
				}
				if (Quest.ID.Equals("O Glorious Shekhinah!"))
				{
					if (The.Game.GetIntGameState("VisitedSixDayStilt", 0) != 1)
					{
						string accomplishment = Quest.Accomplishment;
						string text3 = text;
						MuralCategory muralCategory = MuralCategoryHelpers.parseCategory(text2);
						MuralWeight muralWeight2 = muralWeight;
						JournalAPI.AddAccomplishment(accomplishment, text3, Quest.Gospel, null, "general", muralCategory, muralWeight2, null, -1L, true);
						XRLCore.Core.Game.SetIntGameState("VisitedSixDayStilt", 1);
						Achievement.SIX_DAY_STILT.Unlock();
					}
				}
				else
				{
					string accomplishment2 = Quest.Accomplishment;
					string text4 = text;
					MuralCategory muralCategory = MuralCategoryHelpers.parseCategory(text2);
					MuralWeight muralWeight2 = muralWeight;
					JournalAPI.AddAccomplishment(accomplishment2, text4, Quest.Gospel, null, "general", muralCategory, muralWeight2, null, -1L, true);
				}
			}
			if (!Quest.Factions.IsNullOrEmpty() && !Quest.Reputation.IsNullOrEmpty())
			{
				int num = int.Parse(Quest.Reputation);
				foreach (string text5 in Quest.Factions.Split(',', StringSplitOptions.None))
				{
					The.Game.PlayerReputation.Modify(text5, num, "Quest", null, null, false, false);
				}
			}
			if (!Quest.Achievement.IsNullOrEmpty())
			{
				AchievementManager.SetAchievement(Quest.Achievement);
			}
			QuestFinishedEvent.Send(Quest);
			MetricsManager.LogEvent("Quest:Complete:" + Quest.ID);
			XRL.World.GameObject player = The.Player;
			if (player != null)
			{
				if (player.HasStat("Level"))
				{
					MetricsManager.LogEvent("Quest:Complete:Level:" + Quest.ID + ":" + player.Stat("Level", 0).ToString());
				}
				if (player.HasStat("HP"))
				{
					MetricsManager.LogEvent("Quest:Complete:HP:" + Quest.ID + ":" + player.Stat("Level", 0).ToString());
				}
				MetricsManager.LogEvent("Quest:Complete:Turns:" + Quest.ID + ":" + XRLCore.Core.Game.Turns.ToString());
				MetricsManager.LogEvent("Quest:Complete:Walltime:" + Quest.ID + ":" + XRLCore.Core.Game._walltime.ToString());
			}
		}

		// Token: 0x0600358F RID: 13711 RVA: 0x00102358 File Offset: 0x00100558
		public bool FinishQuestStep(string QuestID, string QuestStepList, int XP = -1, bool CanFinishQuest = true, string ZoneID = null)
		{
			Quest quest;
			return this.Quests.TryGetValue(QuestID, out quest) && this.FinishQuestStep(quest, QuestStepList, XP, CanFinishQuest, ZoneID);
		}

		// Token: 0x06003590 RID: 13712 RVA: 0x00102384 File Offset: 0x00100584
		public void PopupFinishQuestStep(Quest Quest, QuestStep Step)
		{
			IQuestSystem system = Quest.System;
			if (system != null)
			{
				system.ShowFinishStepPopup(Step);
				return;
			}
			Quest.ShowFinishStepPopup(Step);
		}

		// Token: 0x06003591 RID: 13713 RVA: 0x001023AC File Offset: 0x001005AC
		public bool FinishQuestStep(Quest Quest, string QuestStepList, int XP = -1, bool CanFinishQuest = true, string ZoneID = null)
		{
			bool flag = false;
			try
			{
				if (!this.HasQuest(Quest.ID))
				{
					return flag;
				}
				foreach (string text in QuestStepList.Split('~', StringSplitOptions.None))
				{
					QuestStep questStep;
					if (!Quest.StepsByID.TryGetValue(text, out questStep))
					{
						MetricsManager.LogError("FinishQuestStep", "Invalid quest step ID: " + Quest.ID + "@" + text);
					}
					else
					{
						flag = true;
						if (!questStep.Finished)
						{
							SoundManager.PlayUISound("Sounds/Misc/sfx_quest_step_complete", 1f, false, false, SoundRequest.SoundEffectType.None);
							questStep.Finished = true;
							questStep.Failed = false;
							if (!questStep.Awarded)
							{
								if (XP >= 0)
								{
									questStep.XP = XP;
								}
								this.PopupFinishQuestStep(Quest, questStep);
								if (questStep.XP > 0)
								{
									XRL.World.GameObject player = The.Player;
									int xp = questStep.XP;
									int num = -1;
									int num2 = 0;
									int maxValue = int.MaxValue;
									XRL.World.GameObject gameObject = null;
									XRL.World.GameObject influencer = Quest.GetInfluencer();
									XRL.World.GameObject gameObject2 = null;
									XRL.World.GameObject gameObject3 = null;
									string text2 = ZoneID;
									if (ZoneID == null)
									{
										QuestManager manager = Quest.Manager;
										text2 = ((manager != null) ? manager.GetQuestZoneID() : null);
									}
									player.AwardXP(xp, num, num2, maxValue, gameObject, influencer, gameObject2, gameObject3, text2, null);
								}
								questStep.Awarded = true;
							}
							Quest.FinishStep(questStep);
							QuestManager manager2 = Quest.Manager;
							if (manager2 != null)
							{
								manager2.OnStepComplete(text);
							}
							QuestStepFinishedEvent.Send(Quest, questStep);
						}
					}
				}
				if (CanFinishQuest)
				{
					this.CheckQuestFinishState(Quest);
				}
			}
			catch (Exception ex)
			{
				MetricsManager.LogError("FinishQuestStep", ex);
				MessageQueue.AddPlayerMessage(string.Concat(new string[]
				{
					"Error finishing quest step ",
					Quest.ID,
					" @ ",
					QuestStepList,
					" : ",
					ex.ToString()
				}), 'R', true);
			}
			return flag;
		}

		// Token: 0x06003592 RID: 13714 RVA: 0x00102568 File Offset: 0x00100768
		public void CheckQuestFinishState(string QuestID)
		{
			Quest quest;
			if (this.Quests.TryGetValue(QuestID, out quest))
			{
				this.FinishQuest(quest);
			}
		}

		// Token: 0x06003593 RID: 13715 RVA: 0x0010258C File Offset: 0x0010078C
		public void CheckQuestFinishState(Quest Quest)
		{
			if (Quest.Finished)
			{
				return;
			}
			foreach (KeyValuePair<string, QuestStep> keyValuePair in Quest.StepsByID)
			{
				string text;
				QuestStep questStep;
				keyValuePair.Deconstruct(out text, out questStep);
				QuestStep questStep2 = questStep;
				if (!questStep2.Finished && !questStep2.Optional)
				{
					return;
				}
			}
			this.FinishQuest(Quest);
		}

		// Token: 0x06003594 RID: 13716 RVA: 0x00102608 File Offset: 0x00100808
		public void PopupFailQuestStep(Quest Quest, QuestStep Step)
		{
			IQuestSystem system = Quest.System;
			if (system != null)
			{
				system.ShowFailStepPopup(Step);
				return;
			}
			Quest.ShowFailStepPopup(Step);
		}

		// Token: 0x06003595 RID: 13717 RVA: 0x00102630 File Offset: 0x00100830
		public void FailQuestStep(string QuestID, string StepID, bool ShowMessage = true)
		{
			Quest quest;
			if (this.Quests.TryGetValue(QuestID, out quest))
			{
				this.FailQuestStep(quest, StepID, ShowMessage);
			}
		}

		// Token: 0x06003596 RID: 13718 RVA: 0x00102658 File Offset: 0x00100858
		public void FailQuestStep(Quest Quest, string StepID, bool ShowMessage = true)
		{
			if (!this.HasQuest(Quest.ID))
			{
				return;
			}
			QuestStep questStep;
			if (!Quest.StepsByID.TryGetValue(StepID, out questStep))
			{
				MetricsManager.LogError("FinishQuestStep", "Invalid quest step ID: " + Quest.ID + "@" + StepID);
				return;
			}
			if (questStep.Failed)
			{
				return;
			}
			SoundManager.PlayUISound("Sounds/Misc/sfx_quest_step_fail", 1f, false, false, SoundRequest.SoundEffectType.None);
			questStep.Finished = false;
			questStep.Failed = true;
			if (ShowMessage)
			{
				this.PopupFailQuestStep(Quest, questStep);
			}
			Quest.FailStep(questStep);
		}

		// Token: 0x06003597 RID: 13719 RVA: 0x001026E0 File Offset: 0x001008E0
		public bool FinishedQuest(string questID)
		{
			return this.FinishedQuests.ContainsKey(questID);
		}

		// Token: 0x06003598 RID: 13720 RVA: 0x001026F0 File Offset: 0x001008F0
		public bool FinishedQuestStep(string questID)
		{
			string[] array = questID.Split('~', StringSplitOptions.None);
			return this.Quests.ContainsKey(array[0]) && this.Quests[array[0]].StepsByID.ContainsKey(array[1]) && this.Quests[array[0]].StepsByID[array[1]].Finished;
		}

		// Token: 0x06003599 RID: 13721 RVA: 0x00102758 File Offset: 0x00100958
		public bool HasGameState(string Name)
		{
			return this.HasStringGameState(Name) || this.HasIntGameState(Name) || this.HasObjectGameState(Name) || this.HasInt64GameState(Name) || this.HasBooleanGameState(Name);
		}

		// Token: 0x0600359A RID: 13722 RVA: 0x00102788 File Offset: 0x00100988
		public bool TestGameState(string Spec)
		{
			string[] array = Spec.Split(XRLGame.spaceSplitter, 3);
			string text = array[0];
			string text2 = ((array.Length >= 2) ? array[1] : null);
			string text3 = ((array.Length >= 3) ? array[2] : null);
			if (text2 != null && text2[0] == '!')
			{
				return !this.TestGameStateInternal(text, text2.Substring(1), text3);
			}
			return this.TestGameStateInternal(text, text2, text3);
		}

		// Token: 0x0600359B RID: 13723 RVA: 0x001027EC File Offset: 0x001009EC
		private bool TestGameStateInternal(string name, string op, string val)
		{
			string text;
			if (op != null && val != null && this.StringGameState.TryGetValue(name, out text))
			{
				if (!(op == "="))
				{
					if (!(op == "~"))
					{
						if (!(op == "contains"))
						{
							if (!(op == "~contains"))
							{
								if (!(op == "isin"))
								{
									if (op == "~isin")
									{
										if (val.Contains(text, CompareOptions.IgnoreCase))
										{
											return true;
										}
									}
								}
								else if (val.Contains(text))
								{
									return true;
								}
							}
							else if (text.Contains(val, CompareOptions.IgnoreCase))
							{
								return true;
							}
						}
						else if (text.Contains(val))
						{
							return true;
						}
					}
					else if (text.EqualsNoCase(val))
					{
						return true;
					}
				}
				else if (text == val)
				{
					return true;
				}
			}
			int num;
			int num2;
			if (op != null && val != null && this.IntGameState.TryGetValue(name, out num) && int.TryParse(val, out num2))
			{
				uint num3 = <PrivateImplementationDetails>.ComputeStringHash(op);
				if (num3 <= 588024921U)
				{
					if (num3 != 284975636U)
					{
						if (num3 != 537692064U)
						{
							if (num3 == 588024921U)
							{
								if (op == "&")
								{
									if ((num & num2) == num2)
									{
										return true;
									}
								}
							}
						}
						else if (op == "%")
						{
							if (num % num2 == 0)
							{
								return true;
							}
						}
					}
					else if (op == ">=")
					{
						if (num >= num2)
						{
							return true;
						}
					}
				}
				else if (num3 <= 957132539U)
				{
					if (num3 != 940354920U)
					{
						if (num3 == 957132539U)
						{
							if (op == "<")
							{
								if (num < num2)
								{
									return true;
								}
							}
						}
					}
					else if (op == "=")
					{
						if (num == num2)
						{
							return true;
						}
					}
				}
				else if (num3 != 990687777U)
				{
					if (num3 == 2499223986U)
					{
						if (op == "<=")
						{
							if (num <= num2)
							{
								return true;
							}
						}
					}
				}
				else if (op == ">")
				{
					if (num > num2)
					{
						return true;
					}
				}
			}
			long num4;
			long num5;
			if (op != null && val != null && this.Int64GameState.TryGetValue(name, out num4) && long.TryParse(val, out num5))
			{
				uint num3 = <PrivateImplementationDetails>.ComputeStringHash(op);
				if (num3 <= 588024921U)
				{
					if (num3 != 284975636U)
					{
						if (num3 != 537692064U)
						{
							if (num3 == 588024921U)
							{
								if (op == "&")
								{
									if ((num4 & num5) == num5)
									{
										return true;
									}
								}
							}
						}
						else if (op == "%")
						{
							if (num4 % num5 == 0L)
							{
								return true;
							}
						}
					}
					else if (op == ">=")
					{
						if (num4 >= num5)
						{
							return true;
						}
					}
				}
				else if (num3 <= 957132539U)
				{
					if (num3 != 940354920U)
					{
						if (num3 == 957132539U)
						{
							if (op == "<")
							{
								if (num4 < num5)
								{
									return true;
								}
							}
						}
					}
					else if (op == "=")
					{
						if (num4 == num5)
						{
							return true;
						}
					}
				}
				else if (num3 != 990687777U)
				{
					if (num3 == 2499223986U)
					{
						if (op == "<=")
						{
							if (num4 <= num5)
							{
								return true;
							}
						}
					}
				}
				else if (op == ">")
				{
					if (num4 > num5)
					{
						return true;
					}
				}
			}
			bool flag;
			if (this.BooleanGameState.TryGetValue(name, out flag))
			{
				bool flag2;
				if (op == null && val == null)
				{
					if (flag)
					{
						return true;
					}
				}
				else if (op != null && val != null && bool.TryParse(val, out flag2) && op == "=" && flag == flag2)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600359C RID: 13724 RVA: 0x00102B68 File Offset: 0x00100D68
		public void PopupFailQuest(Quest Quest)
		{
			IQuestSystem system = Quest.System;
			if (system != null)
			{
				system.ShowFailPopup();
				return;
			}
			Quest.ShowFailPopup();
		}

		// Token: 0x0600359D RID: 13725 RVA: 0x00102B8C File Offset: 0x00100D8C
		public bool FailQuest(string QuestID)
		{
			Quest quest;
			if (!this.Quests.TryGetValue(QuestID, out quest))
			{
				return false;
			}
			SoundManager.PlayUISound("Sounds/Misc/sfx_quest_total_fail", 1f, false, false, SoundRequest.SoundEffectType.None);
			this.FinishedQuests.Add(QuestID, quest);
			this.Quests.Remove(QuestID);
			this.PopupFailQuest(quest);
			quest.Fail();
			return true;
		}

		// Token: 0x0600359E RID: 13726 RVA: 0x00102BE5 File Offset: 0x00100DE5
		public bool TryGetQuest(string QuestID, out Quest Quest)
		{
			return this.Quests.TryGetValue(QuestID, out Quest) || this.FinishedQuests.TryGetValue(QuestID, out Quest);
		}

		// Token: 0x0600359F RID: 13727 RVA: 0x00102C05 File Offset: 0x00100E05
		public bool HasQuest(string ID)
		{
			return this.Quests.ContainsKey(ID) || this.FinishedQuests.ContainsKey(ID);
		}

		// Token: 0x060035A0 RID: 13728 RVA: 0x00102C28 File Offset: 0x00100E28
		public int GetQuestGiverState(XRL.World.GameObject Object)
		{
			if (Object == null)
			{
				return -1;
			}
			string text = Object.GetTagOrStringProperty("QuestGiver", null) ?? Object.GetTagOrStringProperty("GivesDynamicQuest", null);
			if (text.IsNullOrEmpty())
			{
				return -1;
			}
			int num = 0;
			foreach (ReadOnlySpan<char> readOnlySpan in text.DelimitedBy(';'))
			{
				if (this.FinishedQuests.ContainsKey(readOnlySpan))
				{
					num = 2;
					break;
				}
				if (this.Quests.ContainsKey(readOnlySpan))
				{
					num = 1;
				}
			}
			return num;
		}

		// Token: 0x060035A1 RID: 13729 RVA: 0x00102CAD File Offset: 0x00100EAD
		public bool HasFinishedQuest(string ID)
		{
			return this.FinishedQuests.ContainsKey(ID);
		}

		// Token: 0x060035A2 RID: 13730 RVA: 0x00102CBC File Offset: 0x00100EBC
		public bool HasFinishedQuestStep(string questID, string questStepID)
		{
			return this.FinishedQuests.ContainsKey(questID) || (this.HasQuest(questID) && this.Quests[questID].StepsByID.ContainsKey(questStepID) && this.Quests[questID].StepsByID[questStepID].Finished);
		}

		// Token: 0x060035A3 RID: 13731 RVA: 0x00102D1C File Offset: 0x00100F1C
		public bool HasUnfinishedQuest(string questID)
		{
			return this.HasQuest(questID) && !this.HasFinishedQuest(questID);
		}

		// Token: 0x060035A4 RID: 13732 RVA: 0x00102D34 File Offset: 0x00100F34
		public bool HasQuestProperty(string QuestID, string Key)
		{
			Quest quest;
			return (this.Quests.TryGetValue(QuestID, out quest) || this.FinishedQuests.TryGetValue(QuestID, out quest)) && quest.HasProperty(Key);
		}

		// Token: 0x170005B3 RID: 1459
		// (get) Token: 0x060035A5 RID: 13733 RVA: 0x00102D6A File Offset: 0x00100F6A
		// (set) Token: 0x060035A6 RID: 13734 RVA: 0x00102D7C File Offset: 0x00100F7C
		public string gameMode
		{
			get
			{
				return this.GetStringGameState("GameMode", "");
			}
			set
			{
				this.SetStringGameState("GameMode", value);
			}
		}

		// Token: 0x060035A7 RID: 13735 RVA: 0x00102D8C File Offset: 0x00100F8C
		public string GetStringGameState(string State, string Default = "")
		{
			string text;
			if (this.StringGameState.TryGetValue(State, out text))
			{
				return text;
			}
			return Default;
		}

		// Token: 0x060035A8 RID: 13736 RVA: 0x00102DAC File Offset: 0x00100FAC
		public void AppendStringGameState(string State, string Value)
		{
			if (!this.StringGameState.ContainsKey(State))
			{
				this.StringGameState.Add(State, Value);
				return;
			}
			this.StringGameState[State] = this.StringGameState[State] + Value;
		}

		// Token: 0x060035A9 RID: 13737 RVA: 0x00102DE8 File Offset: 0x00100FE8
		public void AppendStringGameState(string State, string Value, string Separator)
		{
			if (!this.StringGameState.ContainsKey(State))
			{
				this.StringGameState.Add(State, Value);
				return;
			}
			this.StringGameState[State] = this.StringGameState[State] + Separator + Value;
		}

		// Token: 0x060035AA RID: 13738 RVA: 0x00102E25 File Offset: 0x00101025
		public void SetStringGameState(string State, string Value)
		{
			this.StringGameState[State] = Value;
		}

		// Token: 0x060035AB RID: 13739 RVA: 0x00102E34 File Offset: 0x00101034
		public void RemoveStringGameState(string State)
		{
			this.StringGameState.Remove(State);
		}

		// Token: 0x060035AC RID: 13740 RVA: 0x00102E44 File Offset: 0x00101044
		public bool HasDelimitedGameState(string State, char Separator, string Value)
		{
			string text;
			return this.StringGameState.TryGetValue(State, out text) && text.HasDelimitedSubstring(Separator, Value, StringComparison.Ordinal);
		}

		// Token: 0x060035AD RID: 13741 RVA: 0x00102E6C File Offset: 0x0010106C
		public bool HasAnyDelimitedGameState(string State, char Separator, string First, string Second)
		{
			string text;
			return this.StringGameState.TryGetValue(State, out text) && (text.HasDelimitedSubstring(Separator, First, StringComparison.Ordinal) || text.HasDelimitedSubstring(Separator, Second, StringComparison.Ordinal));
		}

		// Token: 0x060035AE RID: 13742 RVA: 0x00102EA4 File Offset: 0x001010A4
		public bool TryAddDelimitedGameState(string State, char Separator, string Value)
		{
			string text;
			if (this.StringGameState.TryGetValue(State, out text))
			{
				if (text.HasDelimitedSubstring(Separator, Value, StringComparison.Ordinal))
				{
					return false;
				}
				this.StringGameState[State] = text + Separator + Value;
			}
			else
			{
				this.StringGameState[State] = Value;
			}
			return true;
		}

		// Token: 0x060035AF RID: 13743 RVA: 0x00102EF8 File Offset: 0x001010F8
		public int GetIntGameState(string State, int Default = 0)
		{
			int num;
			if (this.IntGameState.TryGetValue(State, out num))
			{
				return num;
			}
			return Default;
		}

		// Token: 0x060035B0 RID: 13744 RVA: 0x00102F18 File Offset: 0x00101118
		public void SetIntGameState(string State, int Value)
		{
			this.IntGameState[State] = Value;
		}

		// Token: 0x060035B1 RID: 13745 RVA: 0x00102F28 File Offset: 0x00101128
		public int ModIntGameState(string State, int Value)
		{
			int num = this.GetIntGameState(State, 0) + Value;
			this.IntGameState[State] = num;
			return num;
		}

		// Token: 0x060035B2 RID: 13746 RVA: 0x00102F4E File Offset: 0x0010114E
		public void RemoveIntGameState(string State)
		{
			this.IntGameState.Remove(State);
		}

		// Token: 0x060035B3 RID: 13747 RVA: 0x00102F60 File Offset: 0x00101160
		public long GetInt64GameState(string State, long Default = 0L)
		{
			long num;
			if (this.Int64GameState.TryGetValue(State, out num))
			{
				return num;
			}
			return Default;
		}

		// Token: 0x060035B4 RID: 13748 RVA: 0x00102F80 File Offset: 0x00101180
		public void SetInt64GameState(string State, long Value)
		{
			this.Int64GameState[State] = Value;
		}

		// Token: 0x060035B5 RID: 13749 RVA: 0x00102F8F File Offset: 0x0010118F
		public void RemoveInt64GameState(string State)
		{
			this.Int64GameState.Remove(State);
		}

		// Token: 0x060035B6 RID: 13750 RVA: 0x00102FA0 File Offset: 0x001011A0
		public bool TryGetFloatGameState(string State, out float Value)
		{
			int num;
			if (this.IntGameState.TryGetValue(State, out num))
			{
				Value = BitConverter.Int32BitsToSingle(num);
				return true;
			}
			Value = 0f;
			return false;
		}

		// Token: 0x060035B7 RID: 13751 RVA: 0x00102FD0 File Offset: 0x001011D0
		public float GetFloatGameState(string State, float Default = 0f)
		{
			int num;
			if (this.IntGameState.TryGetValue(State, out num))
			{
				return BitConverter.Int32BitsToSingle(num);
			}
			return Default;
		}

		// Token: 0x060035B8 RID: 13752 RVA: 0x00102FF5 File Offset: 0x001011F5
		public void SetFloatGameState(string State, float Value)
		{
			this.IntGameState[State] = BitConverter.SingleToInt32Bits(Value);
		}

		// Token: 0x060035B9 RID: 13753 RVA: 0x00103009 File Offset: 0x00101209
		public void RemoveFloatGameState(string State)
		{
			this.IntGameState.Remove(State);
		}

		// Token: 0x060035BA RID: 13754 RVA: 0x00103018 File Offset: 0x00101218
		public bool TryGetDoubleGameState(string State, out double Value)
		{
			long num;
			if (this.Int64GameState.TryGetValue(State, out num))
			{
				Value = BitConverter.Int64BitsToDouble(num);
				return true;
			}
			Value = 0.0;
			return false;
		}

		// Token: 0x060035BB RID: 13755 RVA: 0x0010304C File Offset: 0x0010124C
		public double GetDoubleGameState(string State, double Default = 0.0)
		{
			long num;
			if (this.Int64GameState.TryGetValue(State, out num))
			{
				return BitConverter.Int64BitsToDouble(num);
			}
			return Default;
		}

		// Token: 0x060035BC RID: 13756 RVA: 0x00103071 File Offset: 0x00101271
		public void SetDoubleGameState(string State, float Value)
		{
			this.Int64GameState[State] = BitConverter.DoubleToInt64Bits((double)Value);
		}

		// Token: 0x060035BD RID: 13757 RVA: 0x00103086 File Offset: 0x00101286
		public void RemoveDoubleGameState(string State)
		{
			this.Int64GameState.Remove(State);
		}

		// Token: 0x060035BE RID: 13758 RVA: 0x00103098 File Offset: 0x00101298
		public bool GetBooleanGameState(string State, bool Default = false)
		{
			bool flag;
			if (this.BooleanGameState.TryGetValue(State, out flag))
			{
				return flag;
			}
			return Default;
		}

		// Token: 0x060035BF RID: 13759 RVA: 0x001030B8 File Offset: 0x001012B8
		public bool TryGetBooleanGameState(string State, out bool Result)
		{
			return this.BooleanGameState.TryGetValue(State, out Result);
		}

		// Token: 0x060035C0 RID: 13760 RVA: 0x001030C7 File Offset: 0x001012C7
		public void SetBooleanGameState(string State, bool Value)
		{
			this.BooleanGameState[State] = Value;
		}

		// Token: 0x060035C1 RID: 13761 RVA: 0x001030D6 File Offset: 0x001012D6
		public void RemoveBooleanGameState(string State)
		{
			this.BooleanGameState.Remove(State);
		}

		// Token: 0x060035C2 RID: 13762 RVA: 0x001030E8 File Offset: 0x001012E8
		public object GetObjectGameState(string State)
		{
			object obj;
			if (this.ObjectGameState.TryGetValue(State, out obj))
			{
				return obj;
			}
			return null;
		}

		// Token: 0x060035C3 RID: 13763 RVA: 0x00103108 File Offset: 0x00101308
		public void SetObjectGameState(string State, Location2D _Value)
		{
			throw new InvalidDataException("Don't set Location2D game states!");
		}

		// Token: 0x060035C4 RID: 13764 RVA: 0x00103114 File Offset: 0x00101314
		public T RequireGameState<T>(string StateID, Func<T> Generator) where T : class
		{
			object obj;
			if (this.ObjectGameState.TryGetValue(StateID, out obj))
			{
				return obj as T;
			}
			T t = Generator();
			this.ObjectGameState[StateID] = t;
			return t;
		}

		// Token: 0x060035C5 RID: 13765 RVA: 0x00103158 File Offset: 0x00101358
		public T RequireTransientGameState<T>(string StateID, Func<T> Generator) where T : class
		{
			object obj;
			if (this.TransientGameState.TryGetValue(StateID, out obj))
			{
				return obj as T;
			}
			T t = Generator();
			this.TransientGameState[StateID] = t;
			return t;
		}

		// Token: 0x060035C6 RID: 13766 RVA: 0x0010319C File Offset: 0x0010139C
		public T RequireTransientGameState<T>(string StateID) where T : class, new()
		{
			object obj;
			if (this.TransientGameState.TryGetValue(StateID, out obj))
			{
				return obj as T;
			}
			T t = new T();
			this.TransientGameState[StateID] = t;
			return t;
		}

		// Token: 0x060035C7 RID: 13767 RVA: 0x001031DE File Offset: 0x001013DE
		public void SetObjectGameState(string State, object Value)
		{
			this.ObjectGameState[State] = Value;
		}

		// Token: 0x060035C8 RID: 13768 RVA: 0x001031F0 File Offset: 0x001013F0
		public int GetWorldSeed(string Key = null)
		{
			bool isSeeded = this.IsSeeded;
			int num;
			if (!this.IntGameState.TryGetValue("WorldSeed", out num))
			{
				string text;
				int num2;
				if (this.StringGameState.TryGetValue("OriginalWorldSeed", out text) && !text.IsNullOrEmpty() && text[0] == '#' && int.TryParse(text.AsSpan(1), out num2))
				{
					num = num2;
				}
				else
				{
					num = new global::System.Random(Hash.String("WorldSeed" + text)).Next(0, int.MaxValue);
				}
				this.IntGameState["WorldSeed"] = num;
			}
			if (Key == null)
			{
				return num;
			}
			return (int)Hash.FNV1A32(Key, (uint)num);
		}

		// Token: 0x170005B4 RID: 1460
		// (get) Token: 0x060035C9 RID: 13769 RVA: 0x00103291 File Offset: 0x00101491
		// (set) Token: 0x060035CA RID: 13770 RVA: 0x00103299 File Offset: 0x00101499
		[Obsolete("Use XRLGame.Player without underscore")]
		public GamePlayer _Player
		{
			get
			{
				return this.Player;
			}
			set
			{
				this.Player = value;
			}
		}

		// Token: 0x060035CB RID: 13771 RVA: 0x001032A4 File Offset: 0x001014A4
		public void CreateNewGame()
		{
			this.Player = new GamePlayer();
			this.PlayerName = "";
			this.Quests = new StringMap<Quest>();
			this.FinishedQuests = new StringMap<Quest>();
			this.StringGameState = new Dictionary<string, string>();
			this.IntGameState = new Dictionary<string, int>();
			this.Int64GameState = new Dictionary<string, long>();
			this.ObjectGameState = new Dictionary<string, object>();
			this.BooleanGameState = new Dictionary<string, bool>();
			this.BlueprintsSeen = new HashSet<string>();
			this.WorldMazes = new Dictionary<string, Maze>();
			this.Segments = 0L;
			this.Turns = 0L;
			this.FactionIDSequence = 0;
			this.TimeOffset = Stat.Random(0, 365) * 1200 + 325;
			this.TimeTicks = (long)this.TimeOffset;
			this.WallTime = new Stopwatch();
			this.WallTime.Start();
			this._walltime = 0L;
			MemoryHelper.GCCollectMax();
		}

		// Token: 0x060035CC RID: 13772 RVA: 0x00103390 File Offset: 0x00101590
		public void SaveQuests(SerializationWriter Writer)
		{
			Writer.Write(this.Quests.Count);
			foreach (KeyValuePair<string, Quest> keyValuePair in this.Quests)
			{
				Writer.Write(keyValuePair.Key);
				keyValuePair.Value.Save(Writer);
			}
			Writer.Write(this.FinishedQuests.Count);
			foreach (KeyValuePair<string, Quest> keyValuePair2 in this.FinishedQuests)
			{
				Writer.Write(keyValuePair2.Key);
				keyValuePair2.Value.Save(Writer);
			}
		}

		// Token: 0x060035CD RID: 13773 RVA: 0x00103470 File Offset: 0x00101670
		public void LoadQuests(SerializationReader Reader)
		{
			int num = Reader.ReadInt32();
			this.Quests.Clear();
			this.Quests.EnsureCapacity(num);
			for (int i = 0; i < num; i++)
			{
				string text = Reader.ReadString();
				this.Quests.Add(text, Quest.Load(Reader));
			}
			num = Reader.ReadInt32();
			this.FinishedQuests.Clear();
			this.FinishedQuests.EnsureCapacity(num);
			for (int j = 0; j < num; j++)
			{
				string text2 = Reader.ReadString();
				this.FinishedQuests.Add(text2, Quest.Load(Reader));
			}
		}

		// Token: 0x060035CE RID: 13774 RVA: 0x00103508 File Offset: 0x00101708
		public void SaveSystems(SerializationWriter Writer)
		{
			this.RemoveFlaggedSystems();
			Writer.Write(this.Systems.Count);
			foreach (IGameSystem gameSystem in this.Systems)
			{
				if (gameSystem != null)
				{
					gameSystem.BeforeSave();
				}
				Writer.Write(gameSystem);
				if (gameSystem != null)
				{
					gameSystem.AfterSave();
				}
			}
		}

		// Token: 0x060035CF RID: 13775 RVA: 0x00103584 File Offset: 0x00101784
		public void LoadSystems(SerializationReader Reader)
		{
			int num = Reader.ReadInt32();
			this.Systems.EnsureCapacity<IGameSystem>(num);
			for (int i = 0; i < num; i++)
			{
				IGameSystem gameSystem = Reader.ReadComposite() as IGameSystem;
				if (gameSystem != null)
				{
					this.Systems.Add(gameSystem);
				}
			}
		}

		// Token: 0x060035D0 RID: 13776 RVA: 0x001035CC File Offset: 0x001017CC
		public void SaveObjectGameState(SerializationWriter Writer)
		{
			Writer.WriteOptimized(this.ObjectGameState.Count);
			foreach (KeyValuePair<string, object> keyValuePair in this.ObjectGameState)
			{
				string text;
				object obj;
				keyValuePair.Deconstruct(out text, out obj);
				string text2 = text;
				object obj2 = obj;
				Writer.WriteOptimized(text2);
				Writer.WriteObject(obj2);
			}
		}

		// Token: 0x060035D1 RID: 13777 RVA: 0x00103648 File Offset: 0x00101848
		public void LoadObjectGameState(SerializationReader Reader)
		{
			int num = Reader.ReadOptimizedInt32();
			this.ObjectGameState = new Dictionary<string, object>(num);
			for (int i = 0; i < num; i++)
			{
				string text = Reader.ReadOptimizedString();
				object obj = Reader.ReadObject();
				if (obj != null)
				{
					this.ObjectGameState[text] = obj;
				}
			}
		}

		// Token: 0x060035D2 RID: 13778 RVA: 0x00103694 File Offset: 0x00101894
		public string GetCacheDirectory(string FileName = null)
		{
			if (this._CacheDirectory == null)
			{
				string text = DataManager.SyncedPath("Saves/" + this.GameID);
				try
				{
					global::Platform.IO.Directory.CreateDirectory(text);
				}
				catch (Exception ex)
				{
					MetricsManager.LogException("GetCacheDirectory", ex, "game_exception");
					return null;
				}
				this._CacheDirectory = text;
			}
			if (!FileName.IsNullOrEmpty())
			{
				return global::Platform.IO.Path.Combine(new string[] { this._CacheDirectory, FileName });
			}
			return this._CacheDirectory;
		}

		// Token: 0x060035D3 RID: 13779 RVA: 0x00103720 File Offset: 0x00101920
		public static XRLGame LoadCurrentGame(string GameName, bool ShowPopup = false, Dictionary<string, object> GameState = null)
		{
			Task<XRLGame> task = XRLGame.LoadGame(XRLCore.Core.Game.GetCacheDirectory(GameName), false, ShowPopup, GameState);
			task.Wait();
			return task.Result;
		}

		// Token: 0x060035D4 RID: 13780 RVA: 0x00103748 File Offset: 0x00101948
		private static async Task<bool> RestoreBackup(string originalPath, string backupSuffix)
		{
			TaskAwaiter<bool> taskAwaiter = global::Platform.IO.File.ExistsAsync(originalPath + backupSuffix).GetAwaiter();
			if (!taskAwaiter.IsCompleted)
			{
				await taskAwaiter;
				TaskAwaiter<bool> taskAwaiter2;
				taskAwaiter = taskAwaiter2;
				taskAwaiter2 = default(TaskAwaiter<bool>);
			}
			if (taskAwaiter.GetResult())
			{
				try
				{
					await global::Platform.IO.File.CopyAsync(originalPath + backupSuffix, originalPath, true);
					return true;
				}
				catch
				{
					return false;
				}
			}
			return false;
		}

		// Token: 0x060035D5 RID: 13781 RVA: 0x00103794 File Offset: 0x00101994
		public static async Task<XRLGame> LoadGame(string Path, bool Session = false, bool ShowPopup = false, Dictionary<string, object> GameState = null)
		{
			XRLGame xrlgame = null;
			XRLGame Return = null;
			string filePath = Path + ".sav.gz";
			int fileVersion = -1;
			string gameVersion = "{unknown}";
			try
			{
				TaskAwaiter<bool> taskAwaiter = global::Platform.IO.File.ExistsAsync(filePath).GetAwaiter();
				TaskAwaiter<bool> taskAwaiter2;
				if (!taskAwaiter.IsCompleted)
				{
					await taskAwaiter;
					taskAwaiter = taskAwaiter2;
					taskAwaiter2 = default(TaskAwaiter<bool>);
				}
				if (!taskAwaiter.GetResult())
				{
					taskAwaiter = XRLGame.RestoreBackup(filePath, ".bak").GetAwaiter();
					if (!taskAwaiter.IsCompleted)
					{
						await taskAwaiter;
						taskAwaiter = taskAwaiter2;
						taskAwaiter2 = default(TaskAwaiter<bool>);
					}
					if (!taskAwaiter.GetResult())
					{
						filePath = Path + ".sav";
						taskAwaiter = global::Platform.IO.File.ExistsAsync(filePath).GetAwaiter();
						if (!taskAwaiter.IsCompleted)
						{
							await taskAwaiter;
							taskAwaiter = taskAwaiter2;
							taskAwaiter2 = default(TaskAwaiter<bool>);
						}
						if (!taskAwaiter.GetResult())
						{
							if (ShowPopup)
							{
								Popup.Show("No saved game exists. (" + DataManager.SanitizePathForDisplay(Path) + ")", null, "Sounds/UI/ui_notification", true, true, true, true, null);
							}
							return null;
						}
					}
				}
				SerializationReader Reader;
				if (!SerializationReader.TryGetShared(out Reader))
				{
					return null;
				}
				Loading.Status status = Loading.StartTask("Loading game", null, true);
				try
				{
					MemoryStream memory;
					using (Stream stream = global::Platform.IO.File.OpenRead(filePath))
					{
						memory = Reader.Stream;
						if (stream.Length >= 2L && stream.ReadByte() == 31)
						{
							stream.ReadByte();
						}
						bool flag = true;
						stream.Position = 0L;
						if (flag)
						{
							using (GZipStream decompressingStream = new GZipStream(stream, CompressionMode.Decompress))
							{
								await decompressingStream.CopyToAsync(memory);
							}
							GZipStream decompressingStream = null;
						}
						else
						{
							await stream.CopyToAsync(memory);
							stream.Dispose();
						}
						memory.Position = 0L;
						Reader.Start(true);
						XRL.World.GameObject.ExternalLoadBindings.Clear();
						if (Reader.ReadInt32() != 123457)
						{
							gameVersion = "2.0.167.0 or prior";
							throw new Exception("Save file is the incorrect version.");
						}
						fileVersion = Reader.FileVersion;
						gameVersion = Reader.ReadString();
						if (CapabilityManager.CurrentPlatformClassification() == CapabilityManager.PlatformClassification.PC)
						{
							try
							{
								if (fileVersion != 409 && Path.EndsWith("Primary"))
								{
									string text = filePath + string.Format("_upgradebackup_{0}.gz", fileVersion);
									if (!global::Platform.IO.File.Exists(text))
									{
										global::Platform.IO.File.Copy(filePath, text, false);
										string text2 = global::System.IO.Path.Combine(global::System.IO.Path.GetDirectoryName(filePath), "Cache.db");
										string text3 = text2 + string.Format("_upgradebackup_{0}.gz", fileVersion);
										if (global::Platform.IO.File.Exists(text2) && !global::Platform.IO.File.Exists(text3))
										{
											global::Platform.IO.File.Copy(text2, text3, false);
										}
									}
								}
							}
							catch (Exception ex)
							{
								MetricsManager.LogException("save upgrade backup", ex, "game_exception");
							}
						}
						if (Reader.FileVersion < 395 || Reader.FileVersion > 409)
						{
							throw new Exception("Save file is the incorrect version (" + gameVersion + ").");
						}
						The.Core.ResetGameBasedStaticCaches();
						global::UnityEngine.Debug.Log("Load game object...");
						Return = Reader.ReadInstanceFields<XRLGame>(BindingFlags.Instance | BindingFlags.Public, FieldAttributes.Static | FieldAttributes.Literal | FieldAttributes.NotSerialized);
						xrlgame = XRLCore.Core.Game;
						XRLCore.Core.Game = Return;
						global::UnityEngine.Debug.Log("Load player...");
						Return.Player = GamePlayer.Load(Reader);
						global::UnityEngine.Debug.Log("Load systems...");
						Return.LoadSystems(Reader);
						if (Reader.ReadInt32() != 111111)
						{
							throw new Exception("checkval (1) wasn't correct");
						}
						global::UnityEngine.Debug.Log("Load zone manager...");
						try
						{
							Return.ZoneManager = ZoneManager.Load(Return, Reader);
						}
						catch (Exception ex2)
						{
							Reader.Errors++;
							Reader.UnspoolTo(222222, true);
							MetricsManager.LogException("Exception loading zone manager", ex2, "game_exception");
						}
						if (Reader.ReadInt32() != 222222)
						{
							throw new Exception("checkval (2) wasn't correct");
						}
						Return.ActionManager = ActionManager.Load(Reader);
						Return.LoadQuests(Reader);
						if (Reader.ReadInt32() != 333333)
						{
							throw new Exception("checkval (3) wasn't correct");
						}
						Return.PlayerReputation = new Reputation();
						Return.PlayerReputation.Read(Reader);
						if (Reader.ReadInt32() != 333444)
						{
							throw new Exception("checkval (34 wasn't correct");
						}
						global::UnityEngine.Debug.Log("Load globals...");
						Examiner.LoadGlobals(Reader);
						if (Reader.ReadInt32() != 444444)
						{
							throw new Exception("checkval (4) wasn't correct");
						}
						TinkerItem.LoadGlobals(Reader);
						Return.WorldMazes = Reader.ReadDictionary<string, Maze>();
						Factions.Load(Reader);
						XRLCore.Core.Game.sultanHistory = History.Load(Reader);
						if (Reader.ReadInt32() != 555555)
						{
							throw new Exception("checkval (5) wasn't correct");
						}
						Gender.Clear();
						PronounSet.Clear();
						Gender.LoadAll(Reader);
						PronounSet.LoadAll(Reader);
						if (Reader.ReadInt32() != 666666)
						{
							throw new Exception("checkval (6) wasn't correct");
						}
						Return.StringGameState = Reader.ReadDictionary<string, string>();
						Return.IntGameState = Reader.ReadDictionary<string, int>();
						Return.Int64GameState = Reader.ReadDictionary<string, long>();
						Return.BooleanGameState = Reader.ReadDictionary<string, bool>();
						Return.LoadObjectGameState(Reader);
						JournalAPI.Load(Reader);
						int num = Reader.ReadInt32();
						Return.BlueprintsSeen = new HashSet<string>(num);
						for (int i = 0; i < num; i++)
						{
							Return.BlueprintSeen(Reader.ReadString());
						}
						global::UnityEngine.Debug.Log("Read game objects...");
						Reader.FinalizeRead();
						Return.ActionManager.FinalizeRead();
						Return.RequireSystem<PsychicHunterSystem>(() => new PsychicHunterSystem());
						Return.RequireSystem<AmbientSoundsSystem>(() => new AmbientSoundsSystem());
						Return.RequireSystem<NorthShevaSystem>();
						Return.Systems.ForEach(delegate(IGameSystem s)
						{
							if (s != null)
							{
								s.AfterLoad(Return);
							}
						});
						if (Reader.Errors > 0)
						{
							Popup.DisplayLoadError(Reader, "save", Reader.Errors);
						}
						if (Reader.FileVersion < 396)
						{
							try
							{
								string stringGameState = Return.GetStringGameState("TauChime", "");
								if (!stringGameState.IsNullOrEmpty() && !stringGameState.HasDelimitedSubstring(',', "Dead", StringComparison.Ordinal))
								{
									List<XRL.World.GameObject> inventoryEquipmentAndCyberneticsReadonly = Return.Player.Body.GetInventoryEquipmentAndCyberneticsReadonly();
									bool flag2 = false;
									foreach (XRL.World.GameObject gameObject in inventoryEquipmentAndCyberneticsReadonly)
									{
										if (gameObject.Blueprint == "TauChime")
										{
											flag2 = true;
											gameObject.RemovePart<GameUnique>();
											break;
										}
									}
									if (!flag2)
									{
										XRL.World.GameObject gameObject2 = XRL.World.GameObject.Create("TauChime");
										Return.SetStringGameState("TauChime", gameObject2.ID);
										Return.Player.Body.ReceiveObject(gameObject2, false, null);
									}
								}
							}
							catch (Exception ex3)
							{
								MetricsManager.LogException("TauChimeCompat", ex3, "game_exception");
							}
						}
					}
					Stream stream = null;
					memory = null;
				}
				finally
				{
					SerializationReader.ReleaseShared();
					status.Dispose();
				}
				The.Core.HostileWalkObjects = new List<XRL.World.GameObject>();
				The.Core.OldHostileWalkObjects = new List<XRL.World.GameObject>();
				The.Core.CludgeCreaturesRendered = new List<XRL.World.GameObject>();
				global::UnityEngine.Debug.Log("Collect...");
				MemoryHelper.GCCollect();
				The.Core.Reset();
				int num2 = Return.GetIntGameState("NextRandomSeed", 0);
				if (num2 == 0)
				{
					num2 = Return.GetIntGameState("RandomSeed", 0);
				}
				ZoneManager zoneManager = Return.ZoneManager;
				string text4;
				if (zoneManager == null)
				{
					text4 = null;
				}
				else
				{
					Zone activeZone = zoneManager.ActiveZone;
					text4 = ((activeZone != null) ? activeZone.ZoneID : null);
				}
				Stat.ReseedFrom(text4 + num2.ToString(), false);
				MarkovBook.CorpusData.Clear();
				Gender.Init();
				PronounSet.Init();
				NameStyles.CheckInit();
				try
				{
					FungalVisionary.VisionLevel = 0;
					GameManager.Instance.GreyscaleLevel = Return.GetIntGameState("GreyscaleLevel", 0);
				}
				catch (Exception)
				{
				}
				Return.ImportGameState(GameState);
				global::UnityEngine.Debug.Log("Seed: " + Return.GetStringGameState("OriginalWorldSeed", ""));
				Inventory inventory = Return.Player._Body.Inventory;
				if (inventory != null)
				{
					inventory.Validate();
				}
				Return.Player.Messages.Cache_0_12Valid = false;
				Reader = null;
				status = default(Loading.Status);
			}
			catch (Exception ex4)
			{
				string text5 = "That save file appears to be corrupt, you can try to restore the backup in your save directory (" + DataManager.SanitizePathForDisplay(Path) + ".sav.gz.bak) by removing the 'bak' file extension.";
				ModInfo modInfo;
				StackFrame stackFrame;
				if (ModManager.TryGetStackMod(ex4, out modInfo, out stackFrame))
				{
					MethodBase method = stackFrame.GetMethod();
					Type declaringType = method.DeclaringType;
					string text6 = ((declaringType != null) ? declaringType.FullName : null) + "." + method.Name;
					modInfo.Error(text6 + "::" + ((ex4 != null) ? ex4.ToString() : null));
					text5 = string.Concat(new string[] { "That save file is likely not loading because of a mod error from ", modInfo.DisplayTitleStripped, " (", text6, "), make sure the correct mods are enabled or contact the mod author." });
				}
				else
				{
					if (fileVersion < 409)
					{
						text5 = "That save file looks like it's from an older save format revision (" + gameVersion + "). Sorry!\n\nYou can probably change to a previous branch in your game client and get it to load if you want to finish it off.";
					}
					else if (fileVersion > 409)
					{
						text5 = "That save file looks like it's from a newer save format revision (" + gameVersion + ").\n\nYou can probably change to a newer branch in your game client and get it to load if you want to finish it off.";
					}
					MetricsManager.LogException("XRLGame.LoadGame::", ex4, "serialization_error");
				}
				if (ShowPopup)
				{
					Popup.Show(text5, null, "Sounds/UI/ui_notification", true, true, true, true, null);
				}
				throw;
			}
			try
			{
				for (int j = 0; j < Return.Systems.Count; j++)
				{
					Return.Systems[j].ApplyRegistrar(Return);
				}
				AbilityManager.UpdateFavorites();
				The.Player.FireEvent("GameRestored");
				AfterGameLoadedEvent.Send(Return);
				Return.PlayerReputation.AfterGameLoaded();
				ModManager.CallAfterGameLoaded();
				ZoneManager zoneManager2 = Return.ZoneManager;
				if (((zoneManager2 != null) ? zoneManager2.ActiveZone : null) != null)
				{
					ZoneManager.PaintWalls(Return.ZoneManager.ActiveZone, 0, 0, -1, -1);
					ZoneManager.PaintWater(Return.ZoneManager.ActiveZone, 0, 0, -1, -1);
					Return.ZoneManager.ActiveZone.Activated();
					ZoneManager.ActivateObjects(Return.ZoneManager.ActiveZone);
				}
				MemoryHelper.GCCollectMax();
				if (xrlgame != null)
				{
					xrlgame.Release();
				}
				if (Session)
				{
					Return.StartSession();
				}
				if (fileVersion < 398)
				{
					XRLGame @return = Return;
					PartRack partRack;
					if (@return == null)
					{
						partRack = null;
					}
					else
					{
						GamePlayer player = @return.Player;
						if (player == null)
						{
							partRack = null;
						}
						else
						{
							XRL.World.GameObject body = player._Body;
							partRack = ((body != null) ? body.PartsList : null);
						}
					}
					PartRack partRack2 = partRack;
					if (!partRack2.IsNullOrEmpty<IPart>())
					{
						foreach (IPart part in partRack2)
						{
							BaseMutation baseMutation = part as BaseMutation;
							int num3;
							if (baseMutation != null && !baseMutation.Variant.IsNullOrEmpty() && !GameObjectFactory.Factory.HasBlueprint(baseMutation.Variant) && int.TryParse(baseMutation.Variant, out num3))
							{
								List<string> variants = baseMutation.GetVariants();
								if (variants.IsNullOrEmpty<string>())
								{
									baseMutation.Variant = null;
								}
								else if (num3 < 0 || num3 >= variants.Count)
								{
									baseMutation.Variant = variants.GetRandomElement<string>(new global::System.Random());
								}
								else
								{
									baseMutation.Variant = variants[num3];
								}
							}
						}
					}
				}
				if (fileVersion < 399)
				{
					XRL.World.GameObject body2 = Return.Player.Body;
					if (body2.Render.RenderLayer < 100)
					{
						body2.Render.RenderLayer = 100;
					}
				}
			}
			catch (Exception ex5)
			{
				MetricsManager.LogException("AfterGameLoaded", ex5, "game_exception");
			}
			return Return;
		}

		// Token: 0x060035D6 RID: 13782 RVA: 0x001037F0 File Offset: 0x001019F0
		public bool RestoreCheckpoint()
		{
			if (XRLGame.LoadCurrentGame("Checkpoint", false, null) == null)
			{
				return false;
			}
			Task task = this.SaveCopy("Checkpoint", "Primary");
			Task task2 = this.CacheCopy("CheckpointCache", "Cache", true);
			task.Wait();
			task2.Wait();
			return true;
		}

		// Token: 0x060035D7 RID: 13783 RVA: 0x0010383B File Offset: 0x00101A3B
		public void Checkpoint()
		{
			this.SaveGame("Checkpoint", "Saving game", true, true);
		}

		// Token: 0x060035D8 RID: 13784 RVA: 0x00103850 File Offset: 0x00101A50
		public void QuickSave()
		{
			this.SaveGame("Quick", "Saving game", true, true);
		}

		// Token: 0x060035D9 RID: 13785 RVA: 0x00103868 File Offset: 0x00101A68
		public async Task<bool> QuickLoad()
		{
			bool flag;
			if (!global::Platform.IO.File.Exists(The.Game.GetCacheDirectory("Quick.sav.gz")))
			{
				flag = XRLGame.LoadCurrentGame("Primary", false, null) != null;
			}
			else if (XRLGame.LoadCurrentGame("Quick", false, null) == null)
			{
				flag = false;
			}
			else
			{
				Task task = this.SaveCopy("Quick", "Primary");
				Task cacheCopy = this.CacheCopy("QuickCache", "Cache", true);
				await task;
				await cacheCopy;
				flag = true;
			}
			return flag;
		}

		// Token: 0x060035DA RID: 13786 RVA: 0x001038AC File Offset: 0x00101AAC
		public Task CacheCopy(string From, string To, bool Reset = false)
		{
			XRLGame.<CacheCopy>d__188 <CacheCopy>d__;
			<CacheCopy>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<CacheCopy>d__.<>4__this = this;
			<CacheCopy>d__.From = From;
			<CacheCopy>d__.To = To;
			<CacheCopy>d__.Reset = Reset;
			<CacheCopy>d__.<>1__state = -1;
			<CacheCopy>d__.<>t__builder.Start<XRLGame.<CacheCopy>d__188>(ref <CacheCopy>d__);
			return <CacheCopy>d__.<>t__builder.Task;
		}

		// Token: 0x060035DB RID: 13787 RVA: 0x00103908 File Offset: 0x00101B08
		public Task SaveCopy(string srcName, string dstName)
		{
			XRLGame.<SaveCopy>d__189 <SaveCopy>d__;
			<SaveCopy>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<SaveCopy>d__.<>4__this = this;
			<SaveCopy>d__.srcName = srcName;
			<SaveCopy>d__.dstName = dstName;
			<SaveCopy>d__.<>1__state = -1;
			<SaveCopy>d__.<>t__builder.Start<XRLGame.<SaveCopy>d__189>(ref <SaveCopy>d__);
			return <SaveCopy>d__.<>t__builder.Task;
		}

		// Token: 0x060035DC RID: 13788 RVA: 0x0010395C File Offset: 0x00101B5C
		public void StartSession()
		{
			using (DelayShutdown.AutoScopeForceMainThread())
			{
				string cacheDirectory = this.GetCacheDirectory("Primary.sav.gz");
				if (global::Platform.IO.File.Exists(cacheDirectory))
				{
					int num;
					if (int.TryParse(Options.GetOption("OptionSessionBackups", "3"), out num) && num > 0)
					{
						string cacheDirectory2 = this.GetCacheDirectory("Cache.db");
						string text = DataManager.LocalPath("Session/");
						string text2 = text + "Session{0}.sav.gz";
						string text3 = text + "SessionCache{0}.db";
						try
						{
							global::Platform.IO.Directory.CreateDirectory(text);
							for (int i = num; i > 0; i--)
							{
								string text4 = string.Format(text2, i);
								string text5 = string.Format(text3, i);
								if (global::Platform.IO.File.Exists(text4))
								{
									if (i + 1 > num)
									{
										global::Platform.IO.File.Delete(text4);
									}
									else
									{
										string text6 = string.Format(text2, i + 1);
										global::Platform.IO.File.Delete(text6);
										global::Platform.IO.File.Move(text4, text6, false);
									}
								}
								if (global::Platform.IO.File.Exists(text5))
								{
									if (i + 1 > num)
									{
										global::Platform.IO.File.Delete(text5);
									}
									else
									{
										string text7 = string.Format(text3, i + 1);
										global::Platform.IO.File.Delete(text7);
										global::Platform.IO.File.Move(text5, text7, false);
									}
								}
							}
							global::Platform.IO.File.Copy(cacheDirectory, string.Format(text2, 1), false);
							if (global::Platform.IO.File.Exists(cacheDirectory2))
							{
								using (DataManager.StartCacheOperation(true))
								{
									DataManager.CloseCacheConnection();
									global::Platform.IO.File.Copy(cacheDirectory2, string.Format(text3, 1), false);
								}
							}
						}
						catch (Exception ex)
						{
							MetricsManager.LogException("Exception rolling session backups", ex, "game_exception");
						}
					}
				}
			}
		}

		// Token: 0x060035DD RID: 13789 RVA: 0x00103B54 File Offset: 0x00101D54
		public SaveGameJSON SaveGameInfo()
		{
			XRL.World.GameObject player = The.Player;
			RenderEvent renderEvent = player.RenderForUI("SaveGameInfo", false);
			TimeSpan timeSpan = TimeSpan.FromTicks(this._walltime);
			return new SaveGameJSON
			{
				SaveVersion = 409,
				GameVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString(),
				ID = this.GameID,
				Name = this.PlayerName,
				Level = player.Statistics["Level"].Value,
				GenoSubType = "",
				GameMode = this.GetStringGameState("GameMode", "Classic"),
				CharIcon = renderEvent.Tile,
				FColor = renderEvent.GetForegroundColorChar(),
				DColor = renderEvent.GetDetailColorChar(),
				Location = this.ZoneManager.GetZoneDisplayName(player.CurrentZone.ZoneID, false, false, true, true),
				InGameTime = string.Format("{0:D2}:{1:D2}:{2:D2}", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds),
				Turn = this.Turns,
				SaveTime = DateTime.Now.ToLongDateString() + " at " + DateTime.Now.ToLongTimeString(),
				ModsEnabled = ModManager.GetRunningMods().ToList<string>()
			};
		}

		// Token: 0x060035DE RID: 13790 RVA: 0x00103CBC File Offset: 0x00101EBC
		public Task SaveGame(string GameName, string Message = "Saving game", bool CopyCache = false, bool CopyPrimary = false)
		{
			XRLGame.<>c__DisplayClass193_0 CS$<>8__locals1 = new XRLGame.<>c__DisplayClass193_0();
			CS$<>8__locals1.<>4__this = this;
			if (this.Transient)
			{
				return null;
			}
			Task task;
			using (DelayShutdown.AutoScopeForceMainThread())
			{
				if (this.DontSaveThisIsAReplay)
				{
					task = null;
				}
				else
				{
					Task saveTask = this.SaveTask;
					if (saveTask != null && !saveTask.IsCompleted)
					{
						task = null;
					}
					else if (!this.Running)
					{
						task = (this.SaveTask = null);
					}
					else
					{
						string text = GameName + ".sav.gz";
						CS$<>8__locals1.primaryFilePath = this.GetCacheDirectory(text);
						MemoryHelper.GCCollectMax();
						SerializationWriter serializationWriter;
						if (!SerializationWriter.TryGetShared(out serializationWriter))
						{
							task = (this.SaveTask = null);
						}
						else
						{
							Loading.Status status = Loading.StartTask(Message, null, true);
							try
							{
								if (this.WallTime != null)
								{
									this._walltime += this.WallTime.ElapsedTicks;
									this.WallTime.Reset();
									this.WallTime.Start();
								}
								else
								{
									this.WallTime = new Stopwatch();
									this.WallTime.Start();
								}
								SaveGameJSON saveGameJSON = this.SaveGameInfo();
								this.SetIntGameState("GreyscaleLevel", GameManager.Instance.GreyscaleLevel);
								this.SetIntGameState("NextRandomSeed", Stat.Rnd.Next());
								serializationWriter.Start(409, true);
								serializationWriter.Write(123457);
								serializationWriter.Write(Assembly.GetExecutingAssembly().GetName().Version.ToString());
								serializationWriter.WriteFields<XRLGame>(this, BindingFlags.Instance | BindingFlags.Public, FieldAttributes.Static | FieldAttributes.Literal | FieldAttributes.NotSerialized);
								this.Player.Save(serializationWriter);
								this.SaveSystems(serializationWriter);
								serializationWriter.Write(111111);
								this.ZoneManager.Save(serializationWriter);
								serializationWriter.Write(222222);
								this.ActionManager.Save(serializationWriter);
								this.SaveQuests(serializationWriter);
								serializationWriter.Write(333333);
								this.PlayerReputation.Write(serializationWriter);
								serializationWriter.Write(333444);
								Examiner.SaveGlobals(serializationWriter);
								serializationWriter.Write(444444);
								TinkerItem.SaveGlobals(serializationWriter);
								serializationWriter.Write<string, Maze>(this.WorldMazes);
								Factions.Save(serializationWriter);
								this.sultanHistory.Save(serializationWriter);
								serializationWriter.Write(555555);
								Gender.SaveAll(serializationWriter);
								PronounSet.SaveAll(serializationWriter);
								serializationWriter.Write(666666);
								serializationWriter.Write<string, string>(this.StringGameState);
								serializationWriter.Write<string, int>(this.IntGameState);
								serializationWriter.Write<string, long>(this.Int64GameState);
								serializationWriter.Write<string, bool>(this.BooleanGameState);
								this.SaveObjectGameState(serializationWriter);
								JournalAPI.Save(serializationWriter);
								serializationWriter.Write(this.BlueprintsSeen.Count);
								foreach (string text2 in this.BlueprintsSeen)
								{
									serializationWriter.Write(text2);
								}
								serializationWriter.FinalizeWrite();
								MetricsManager.LogInfo(string.Format("Done {0} in {1}ms", Message, this.WallTime.ElapsedMilliseconds));
								string cacheDirectory = this.GetCacheDirectory(GameName + ".json");
								bool flag = false;
								try
								{
									string text3 = JsonUtility.ToJson(saveGameJSON, true);
									global::Platform.IO.File.WriteAllText(cacheDirectory, text3);
									if (global::Platform.IO.File.Exists(CS$<>8__locals1.primaryFilePath))
									{
										global::Platform.IO.File.Copy(CS$<>8__locals1.primaryFilePath, CS$<>8__locals1.primaryFilePath + ".bak", true);
										flag = true;
									}
									using (MemoryStream memoryStream = new MemoryStream())
									{
										using (GZipStream gzipStream = new GZipStream(memoryStream, global::System.IO.Compression.CompressionLevel.Fastest, true))
										{
											byte[] buffer = serializationWriter.Stream.GetBuffer();
											gzipStream.Write(buffer, 0, (int)serializationWriter.Stream.Length);
										}
										byte[] array = memoryStream.ToArray();
										global::Platform.IO.File.WriteAllBytes(CS$<>8__locals1.primaryFilePath, array);
									}
									MemoryHelper.GCCollect();
									this.CheckSave(CS$<>8__locals1.primaryFilePath);
									Task task2 = null;
									Task task3 = null;
									if (CopyPrimary)
									{
										task2 = this.SaveCopy(GameName, "Primary");
									}
									if (CopyCache)
									{
										task3 = this.CacheCopy("Cache", GameName + "Cache", false);
									}
									if (task2 != null)
									{
										task2.Wait();
									}
									if (task3 != null)
									{
										task3.Wait();
									}
								}
								catch (Exception ex)
								{
									this.SaveGameError(CS$<>8__locals1.primaryFilePath, ex, flag);
								}
								finally
								{
									SerializationWriter.ReleaseShared();
								}
								task = null;
							}
							catch (Exception ex2)
							{
								SerializationWriter.ReleaseShared();
								this.SaveGameError(CS$<>8__locals1.primaryFilePath, ex2, false);
								task = (this.SaveTask = Task.FromException(ex2));
							}
							finally
							{
								MemoryHelper.GCCollectMax();
								status.Dispose();
								if (CapabilityManager.HasCapability(new CapabilityManager.CapabilityType?(CapabilityManager.CapabilityType.RequiresStorageExpansion)))
								{
									CS$<>8__locals1.storage = State.GetStorage();
									CS$<>8__locals1.storage.GetTotalStorageQuota(out CS$<>8__locals1.totalStorage, out CS$<>8__locals1.journalSize);
									CS$<>8__locals1.storage.GetMaxStorageQuota(out CS$<>8__locals1.totalStorageMax, out CS$<>8__locals1.journalSizeMax);
									CS$<>8__locals1.storage.GetUsedStorageQuota(new OnQuotaRemainingCheck(CS$<>8__locals1.<SaveGame>g__OnUsedStorageReceived|0));
								}
							}
						}
					}
				}
			}
			return task;
		}

		// Token: 0x060035DF RID: 13791 RVA: 0x00104248 File Offset: 0x00102448
		private bool ShouldExpandStorage(long totalStorage, long storageUsed)
		{
			return (float)(totalStorage - storageUsed) < (float)totalStorage * 0.125f;
		}

		// Token: 0x060035E0 RID: 13792 RVA: 0x00104258 File Offset: 0x00102458
		private bool ShouldExpandJournal(long cacheSize, long journalSize)
		{
			return (float)(journalSize - cacheSize) < (float)journalSize * 0.125f;
		}

		// Token: 0x060035E1 RID: 13793 RVA: 0x00104268 File Offset: 0x00102468
		public unsafe void CheckSave(string Path)
		{
			using (Stream stream = global::Platform.IO.File.OpenRead(Path))
			{
				if (stream.Length < 2L)
				{
					throw new IOException("Save file is less than two bytes.");
				}
				Span<byte> span = new Span<byte>(stackalloc byte[(UIntPtr)2], 2);
				stream.Read(span);
				if (*span[0] != 31 || *span[1] != 139)
				{
					throw new IOException("Save file is missing gzip header.");
				}
			}
		}

		// Token: 0x060035E2 RID: 13794 RVA: 0x001042E8 File Offset: 0x001024E8
		public void SaveGameError(string Path, Exception Exception, bool RestoreBackup = false)
		{
			MetricsManager.LogException("SaveGame", Exception, "game_exception");
			if (RestoreBackup && global::Platform.IO.File.Exists(Path + ".bak"))
			{
				try
				{
					global::Platform.IO.File.Copy(Path + ".bak", Path, true);
				}
				catch (Exception ex)
				{
					global::UnityEngine.Debug.LogWarning(string.Format("Failed to restore backup save file. {0}", ex));
				}
			}
			Popup.ShowFailAsync("There was a fatal exception attempting to save your game. Caves of Qud attempted to recover your prior save. You probably want to close the game and reload your most recent save. It'd be helpful to send the save and logs to support@freeholdgames.com", true, true, true);
		}

		// Token: 0x060035E3 RID: 13795 RVA: 0x00104360 File Offset: 0x00102560
		public void ImportGameState(Dictionary<string, object> GameState)
		{
			if (GameState != null)
			{
				foreach (KeyValuePair<string, object> keyValuePair in GameState)
				{
					string text = keyValuePair.Value as string;
					if (text != null)
					{
						this.SetStringGameState(keyValuePair.Key, text);
					}
					else
					{
						int? num = keyValuePair.Value as int?;
						if (num != null)
						{
							this.SetIntGameState(keyValuePair.Key, num.Value);
						}
						else
						{
							long? num2 = keyValuePair.Value as long?;
							if (num2 != null)
							{
								this.SetInt64GameState(keyValuePair.Key, num2.Value);
							}
							else
							{
								bool? flag = keyValuePair.Value as bool?;
								if (flag != null)
								{
									this.SetBooleanGameState(keyValuePair.Key, flag.Value);
								}
								else
								{
									this.SetObjectGameState(keyValuePair.Key, keyValuePair.Value);
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x04001AE8 RID: 6888
		public const int FLAG_SEEDED = 1;

		// Token: 0x04001AE9 RID: 6889
		public const int FLAG_CODA = 2;

		// Token: 0x04001AEA RID: 6890
		public static TextConsole Console;

		// Token: 0x04001AEB RID: 6891
		public static ScreenBuffer Buffer;

		// Token: 0x04001AEC RID: 6892
		public string GameID;

		// Token: 0x04001AED RID: 6893
		public string PlayerName = "Player";

		// Token: 0x04001AEE RID: 6894
		public int Flags;

		// Token: 0x04001AEF RID: 6895
		public bool Running;

		// Token: 0x04001AF0 RID: 6896
		public bool bZoned = true;

		// Token: 0x04001AF1 RID: 6897
		public string DeathReason = "You quit.";

		// Token: 0x04001AF2 RID: 6898
		public string _DeathCategory;

		// Token: 0x04001AF3 RID: 6899
		public long Turns;

		// Token: 0x04001AF4 RID: 6900
		public long TimeTicks;

		// Token: 0x04001AF5 RID: 6901
		public long PlayerActionTicks;

		// Token: 0x04001AF6 RID: 6902
		public int TimeOffset;

		// Token: 0x04001AF7 RID: 6903
		public long Segments;

		// Token: 0x04001AF8 RID: 6904
		public long ActionTicks;

		// Token: 0x04001AF9 RID: 6905
		public long _walltime;

		// Token: 0x04001AFA RID: 6906
		public int GameObjectIDSequence = 1;

		// Token: 0x04001AFB RID: 6907
		public int FactionIDSequence;

		// Token: 0x04001AFC RID: 6908
		public int BodyPartIDSequence;

		// Token: 0x04001AFD RID: 6909
		[NonSerialized]
		public float frameDelta;

		// Token: 0x04001AFE RID: 6910
		[NonSerialized]
		public float realtimeSinceStartup;

		// Token: 0x04001AFF RID: 6911
		[NonSerialized]
		public bool forceNoDeath;

		// Token: 0x04001B00 RID: 6912
		[NonSerialized]
		public string lastFindId;

		// Token: 0x04001B01 RID: 6913
		[NonSerialized]
		public XRL.World.GameObject lastFind;

		// Token: 0x04001B02 RID: 6914
		[NonSerialized]
		public bool InForceFieldPush;

		// Token: 0x04001B03 RID: 6915
		[NonSerialized]
		public bool Transient;

		// Token: 0x04001B04 RID: 6916
		[NonSerialized]
		public string[] ExitViewStack;

		// Token: 0x04001B05 RID: 6917
		public static readonly string MarketingPostfix = "";

		// Token: 0x04001B06 RID: 6918
		public static readonly Version MarketingVersion = new Version("1.0.5");

		// Token: 0x04001B07 RID: 6919
		public static readonly Version CoreVersion = typeof(XRLGame).Assembly.GetName().Version;

		// Token: 0x04001B08 RID: 6920
		[NonSerialized]
		public const int SaveVersion = 409;

		// Token: 0x04001B09 RID: 6921
		[NonSerialized]
		public const int MinSaveVersion = 395;

		// Token: 0x04001B0A RID: 6922
		[NonSerialized]
		public const int MaxSaveVersion = 409;

		// Token: 0x04001B0B RID: 6923
		[NonSerialized]
		public History sultanHistory;

		// Token: 0x04001B0C RID: 6924
		[NonSerialized]
		public Stopwatch WallTime;

		// Token: 0x04001B0D RID: 6925
		[NonSerialized]
		public Dictionary<string, Maze> WorldMazes = new Dictionary<string, Maze>();

		// Token: 0x04001B0E RID: 6926
		[NonSerialized]
		public GamePlayer Player;

		// Token: 0x04001B0F RID: 6927
		[NonSerialized]
		public Reputation PlayerReputation;

		// Token: 0x04001B10 RID: 6928
		[NonSerialized]
		public ActionManager ActionManager;

		// Token: 0x04001B11 RID: 6929
		[NonSerialized]
		public ZoneManager ZoneManager;

		// Token: 0x04001B12 RID: 6930
		[NonSerialized]
		public List<XRL.World.GameObject> Objects = new List<XRL.World.GameObject>();

		// Token: 0x04001B13 RID: 6931
		[NonSerialized]
		public EventRegistry RegisteredEvents = EventRegistry.Get();

		// Token: 0x04001B14 RID: 6932
		[NonSerialized]
		public StringMap<Quest> Quests = new StringMap<Quest>();

		// Token: 0x04001B15 RID: 6933
		[NonSerialized]
		public StringMap<Quest> FinishedQuests = new StringMap<Quest>();

		// Token: 0x04001B16 RID: 6934
		[NonSerialized]
		public Task SaveTask;

		// Token: 0x04001B17 RID: 6935
		[NonSerialized]
		private bool? _AlternateStart;

		// Token: 0x04001B18 RID: 6936
		[NonSerialized]
		public List<IGameSystem> Systems = new List<IGameSystem>();

		// Token: 0x04001B19 RID: 6937
		[NonSerialized]
		private List<IGameSystem> RemovedSystems = new List<IGameSystem>();

		// Token: 0x04001B1A RID: 6938
		[NonSerialized]
		public bool HasRemovedSystems;

		// Token: 0x04001B1B RID: 6939
		[NonSerialized]
		public Dictionary<string, string> StringGameState = new Dictionary<string, string>();

		// Token: 0x04001B1C RID: 6940
		[NonSerialized]
		public Dictionary<string, int> IntGameState = new Dictionary<string, int>();

		// Token: 0x04001B1D RID: 6941
		[NonSerialized]
		public Dictionary<string, long> Int64GameState = new Dictionary<string, long>();

		// Token: 0x04001B1E RID: 6942
		[NonSerialized]
		public Dictionary<string, object> ObjectGameState = new Dictionary<string, object>();

		// Token: 0x04001B1F RID: 6943
		[NonSerialized]
		public Dictionary<string, bool> BooleanGameState = new Dictionary<string, bool>();

		// Token: 0x04001B20 RID: 6944
		[NonSerialized]
		public Dictionary<string, object> TransientGameState = new Dictionary<string, object>();

		// Token: 0x04001B21 RID: 6945
		[NonSerialized]
		public HashSet<string> BlueprintsSeen = new HashSet<string>();

		// Token: 0x04001B22 RID: 6946
		private static char[] spaceSplitter = new char[] { ' ' };

		// Token: 0x04001B23 RID: 6947
		[NonSerialized]
		public string _CacheDirectory;

		// Token: 0x04001B24 RID: 6948
		[NonSerialized]
		public bool DontSaveThisIsAReplay;
	}
}
