using System;
using System.Collections.Generic;
using HistoryKit;
using Qud.API;

namespace XRL.World.Parts
{
	// Token: 0x02000FB9 RID: 4025
	[Serializable]
	public class LocateRelicQuestManager : QuestManager
	{
		// Token: 0x06009FF6 RID: 40950 RVA: 0x003A2E30 File Offset: 0x003A1030
		public override void OnQuestAdded()
		{
			LocateRelicQuestManager.LocateRelicQuestManagerSystem locateRelicQuestManagerSystem = The.Game.RequireSystem<LocateRelicQuestManager.LocateRelicQuestManagerSystem>(() => new LocateRelicQuestManager.LocateRelicQuestManagerSystem());
			LocateRelicQuestManager.LocateRelicQuestManager_QuestEntry locateRelicQuestManager_QuestEntry = new LocateRelicQuestManager.LocateRelicQuestManager_QuestEntry();
			locateRelicQuestManager_QuestEntry.Relic = this.Relic;
			locateRelicQuestManager_QuestEntry.QuestID = this.QuestID;
			LocateRelicQuestManager.LocateRelicQuestManagerSystem locateRelicQuestManagerSystem2 = locateRelicQuestManagerSystem;
			if (locateRelicQuestManagerSystem2.quests == null)
			{
				locateRelicQuestManagerSystem2.quests = new List<LocateRelicQuestManager.LocateRelicQuestManager_QuestEntry>();
			}
			locateRelicQuestManagerSystem.quests.Add(locateRelicQuestManager_QuestEntry);
		}

		// Token: 0x06009FF7 RID: 40951 RVA: 0x003A2EA4 File Offset: 0x003A10A4
		public override void AfterQuestAdded()
		{
			LocateRelicQuestManager.LocateRelicQuestManagerSystem manager = The.Game.RequireSystem<LocateRelicQuestManager.LocateRelicQuestManagerSystem>(() => new LocateRelicQuestManager.LocateRelicQuestManagerSystem());
			if (manager.quests != null)
			{
				IComponent<GameObject>.ThePlayer.ForeachInventoryAndEquipment(delegate(GameObject GO)
				{
					manager.CheckCompleted(GO);
				});
			}
		}

		// Token: 0x04004282 RID: 17026
		public string Relic;

		// Token: 0x04004283 RID: 17027
		public string QuestID;

		// Token: 0x02001CAD RID: 7341
		[Serializable]
		public class LocateRelicQuestManagerSystem : IPlayerSystem
		{
			// Token: 0x0600F1E6 RID: 61926 RVA: 0x0054D3E9 File Offset: 0x0054B5E9
			public override void RegisterPlayer(GameObject Player, IEventRegistrar Registrar)
			{
				Registrar.Register(AddedToInventoryEvent.ID, 0, false);
				Registrar.Register(TookEvent.ID, 0, false);
				Registrar.Register(EquippedEvent.ID, 0, false);
				Registrar.Register(InventoryActionEvent.ID, 0, false);
			}

			// Token: 0x0600F1E7 RID: 61927 RVA: 0x0054D41F File Offset: 0x0054B61F
			public override bool HandleEvent(AddedToInventoryEvent E)
			{
				this.CheckCompleted(E.Item);
				return base.HandleEvent(E);
			}

			// Token: 0x0600F1E8 RID: 61928 RVA: 0x0054D434 File Offset: 0x0054B634
			public override bool HandleEvent(TookEvent E)
			{
				this.CheckCompleted(E.Item);
				return base.HandleEvent(E);
			}

			// Token: 0x0600F1E9 RID: 61929 RVA: 0x0054D449 File Offset: 0x0054B649
			public override bool HandleEvent(EquippedEvent E)
			{
				this.CheckCompleted(E.Item);
				return base.HandleEvent(E);
			}

			// Token: 0x0600F1EA RID: 61930 RVA: 0x0054D45E File Offset: 0x0054B65E
			public override bool HandleEvent(InventoryActionEvent E)
			{
				this.CheckCompleted(E.Item);
				return base.HandleEvent(E);
			}

			// Token: 0x0600F1EB RID: 61931 RVA: 0x0054D474 File Offset: 0x0054B674
			public void CheckCompleted(GameObject Object)
			{
				if (this.quests != null)
				{
					for (int i = this.quests.Count - 1; i >= 0; i--)
					{
						LocateRelicQuestManager.LocateRelicQuestManager_QuestEntry locateRelicQuestManager_QuestEntry = this.quests[i];
						if (!locateRelicQuestManager_QuestEntry.Relic.IsNullOrEmpty() && Object != null && Object.HasStringProperty("RelicName") && (Object.GetStringProperty("RelicName", null).EqualsExceptFormattingAndCase(locateRelicQuestManager_QuestEntry.Relic) || Object.GetStringProperty("RelicName", null).Replace("the the ", "the ").EqualsExceptFormattingAndCase(locateRelicQuestManager_QuestEntry.Relic)))
						{
							if (The.Game.HasFinishedQuest(locateRelicQuestManager_QuestEntry.QuestID))
							{
								MetricsManager.LogError(locateRelicQuestManager_QuestEntry.QuestID + " manager for " + locateRelicQuestManager_QuestEntry.Relic + " present after completion, removing");
								this.quests.RemoveAt(i);
							}
							else
							{
								The.Game.CompleteQuest(locateRelicQuestManager_QuestEntry.QuestID);
								JournalAPI.AddAccomplishment("You recovered the historic relic, " + locateRelicQuestManager_QuestEntry.Relic + ".", HistoricStringExpander.ExpandString("<spice.commonPhrases.intrepid.!random.capitalize> =name= recovered " + locateRelicQuestManager_QuestEntry.Relic + ", a historic relic once thought lost to the sands of time.", null), string.Concat(new string[]
								{
									"In an excavation at a site of deep history near ",
									JournalAPI.GetLandmarkNearestPlayer().Text,
									", =name= recovered ",
									locateRelicQuestManager_QuestEntry.Relic,
									", the historic relic once thought lost to the sands of time."
								}), null, "general", MuralCategory.VisitsLocation, MuralWeight.High, null, -1L, true);
								this.quests.RemoveAt(i);
							}
						}
					}
				}
				if (this.quests.Count == 0)
				{
					The.Game.FlagSystemsForRemoval(typeof(LocateRelicQuestManager.LocateRelicQuestManagerSystem));
				}
			}

			// Token: 0x040070C4 RID: 28868
			public List<LocateRelicQuestManager.LocateRelicQuestManager_QuestEntry> quests;
		}

		// Token: 0x02001CAE RID: 7342
		[Serializable]
		public sealed class LocateRelicQuestManager_QuestEntry : IComposite
		{
			// Token: 0x17000FD3 RID: 4051
			// (get) Token: 0x0600F1EC RID: 61932 RVA: 0x0054D61D File Offset: 0x0054B81D
			public bool WantFieldReflection
			{
				get
				{
					return false;
				}
			}

			// Token: 0x0600F1ED RID: 61933 RVA: 0x0054D620 File Offset: 0x0054B820
			public void Write(SerializationWriter Writer)
			{
				Writer.WriteOptimized(this.Relic);
				Writer.WriteOptimized(this.QuestID);
			}

			// Token: 0x0600F1EE RID: 61934 RVA: 0x0054D63A File Offset: 0x0054B83A
			public void Read(SerializationReader Reader)
			{
				this.Relic = Reader.ReadOptimizedString();
				this.QuestID = Reader.ReadOptimizedString();
			}

			// Token: 0x040070C5 RID: 28869
			public string Relic;

			// Token: 0x040070C6 RID: 28870
			public string QuestID;
		}
	}
}
