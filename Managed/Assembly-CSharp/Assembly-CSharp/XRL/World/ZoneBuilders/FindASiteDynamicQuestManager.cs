using System;
using System.Collections.Generic;
using HistoryKit;
using Qud.API;
using XRL.Wish;

namespace XRL.World.ZoneBuilders
{
	// Token: 0x02000A2F RID: 2607
	[HasWishCommand]
	[Serializable]
	public class FindASiteDynamicQuestManager : QuestManager
	{
		// Token: 0x06006D87 RID: 28039 RVA: 0x0024C66F File Offset: 0x0024A86F
		public FindASiteDynamicQuestManager()
		{
		}

		// Token: 0x06006D88 RID: 28040 RVA: 0x0024C677 File Offset: 0x0024A877
		public FindASiteDynamicQuestManager(string zoneID, string secretID, string questID, string questStepID)
			: this()
		{
			this._zoneID = zoneID;
			this._secretID = secretID;
			this._questID = questID;
			this._questStepID = questStepID;
		}

		// Token: 0x06006D89 RID: 28041 RVA: 0x0024C69C File Offset: 0x0024A89C
		[WishCommand(null, null)]
		public static bool GotoDynamicQuestWhere()
		{
			FindASiteDynamicQuestManager.FindASiteDynamicQuestManagerSystem system = The.Game.GetSystem<FindASiteDynamicQuestManager.FindASiteDynamicQuestManagerSystem>();
			if (system != null)
			{
				using (List<FindASiteDynamicQuestManager.FindASiteDynamicQuestManager_QuestEntry>.Enumerator enumerator = system.quests.GetEnumerator())
				{
					if (enumerator.MoveNext())
					{
						FindASiteDynamicQuestManager.FindASiteDynamicQuestManager_QuestEntry findASiteDynamicQuestManager_QuestEntry = enumerator.Current;
						The.ZoneManager.GetZone(findASiteDynamicQuestManager_QuestEntry.zoneID).GetCell(0, 0).AddObject(The.Player, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
						return true;
					}
				}
				return true;
			}
			return true;
		}

		// Token: 0x06006D8A RID: 28042 RVA: 0x0024C730 File Offset: 0x0024A930
		[WishCommand(null, null)]
		public static bool DynamicQuestWhere()
		{
			FindASiteDynamicQuestManager.FindASiteDynamicQuestManagerSystem system = The.Game.GetSystem<FindASiteDynamicQuestManager.FindASiteDynamicQuestManagerSystem>();
			if (system != null)
			{
				foreach (FindASiteDynamicQuestManager.FindASiteDynamicQuestManager_QuestEntry findASiteDynamicQuestManager_QuestEntry in system.quests)
				{
					IComponent<GameObject>.AddPlayerMessage(string.Concat(new string[] { "quest in ", findASiteDynamicQuestManager_QuestEntry.zoneID, " secret id is ", findASiteDynamicQuestManager_QuestEntry.secretID, " for quest ", findASiteDynamicQuestManager_QuestEntry.questID }), null, true);
				}
			}
			return true;
		}

		// Token: 0x06006D8B RID: 28043 RVA: 0x0024C7D0 File Offset: 0x0024A9D0
		public override void OnQuestAdded()
		{
			FindASiteDynamicQuestManager.FindASiteDynamicQuestManagerSystem findASiteDynamicQuestManagerSystem = The.Game.RequireSystem<FindASiteDynamicQuestManager.FindASiteDynamicQuestManagerSystem>(() => new FindASiteDynamicQuestManager.FindASiteDynamicQuestManagerSystem());
			FindASiteDynamicQuestManager.FindASiteDynamicQuestManager_QuestEntry findASiteDynamicQuestManager_QuestEntry = new FindASiteDynamicQuestManager.FindASiteDynamicQuestManager_QuestEntry();
			findASiteDynamicQuestManager_QuestEntry.zoneID = this._zoneID;
			findASiteDynamicQuestManager_QuestEntry.secretID = this._secretID;
			findASiteDynamicQuestManager_QuestEntry.questID = this._questID;
			findASiteDynamicQuestManager_QuestEntry.questStepID = this._questStepID;
			if (findASiteDynamicQuestManagerSystem.quests == null)
			{
				findASiteDynamicQuestManagerSystem.quests = new List<FindASiteDynamicQuestManager.FindASiteDynamicQuestManager_QuestEntry>();
			}
			findASiteDynamicQuestManagerSystem.quests.Add(findASiteDynamicQuestManager_QuestEntry);
		}

		// Token: 0x06006D8C RID: 28044 RVA: 0x0024C85C File Offset: 0x0024AA5C
		public override void AfterQuestAdded()
		{
			The.Game.RequireSystem<FindASiteDynamicQuestManager.FindASiteDynamicQuestManagerSystem>(() => new FindASiteDynamicQuestManager.FindASiteDynamicQuestManagerSystem()).CheckCompleted(null, null);
		}

		// Token: 0x06006D8D RID: 28045 RVA: 0x0024C88E File Offset: 0x0024AA8E
		public override GameObject GetQuestInfluencer()
		{
			return GameObject.Find((GameObject obj) => obj.GetStringProperty("GivesDynamicQuest", null) == this._questID);
		}

		// Token: 0x06006D8E RID: 28046 RVA: 0x0024C8A4 File Offset: 0x0024AAA4
		public override string GetQuestZoneID()
		{
			FindASiteDynamicQuestManager.FindASiteDynamicQuestManagerSystem findASiteDynamicQuestManagerSystem = The.Game.RequireSystem<FindASiteDynamicQuestManager.FindASiteDynamicQuestManagerSystem>(() => new FindASiteDynamicQuestManager.FindASiteDynamicQuestManagerSystem());
			if (findASiteDynamicQuestManagerSystem.quests != null)
			{
				int num = findASiteDynamicQuestManagerSystem.quests.Count - 1;
				while (num >= 0 && num < findASiteDynamicQuestManagerSystem.quests.Count)
				{
					FindASiteDynamicQuestManager.FindASiteDynamicQuestManager_QuestEntry findASiteDynamicQuestManager_QuestEntry = findASiteDynamicQuestManagerSystem.quests[num];
					if (!findASiteDynamicQuestManager_QuestEntry.zoneID.IsNullOrEmpty())
					{
						return findASiteDynamicQuestManager_QuestEntry.zoneID;
					}
					num--;
				}
			}
			return base.GetQuestZoneID();
		}

		// Token: 0x04003204 RID: 12804
		public string _zoneID;

		// Token: 0x04003205 RID: 12805
		public string _secretID;

		// Token: 0x04003206 RID: 12806
		public string _questID;

		// Token: 0x04003207 RID: 12807
		public string _questStepID;

		// Token: 0x02001AE2 RID: 6882
		[Serializable]
		public class FindASiteDynamicQuestManagerSystem : IGameSystem
		{
			// Token: 0x0600E98C RID: 59788 RVA: 0x0053B518 File Offset: 0x00539718
			public override void Register(XRLGame Game, IEventRegistrar Registrar)
			{
				Registrar.Register(PooledEvent<SecretVisibilityChangedEvent>.ID, 0, false);
				Registrar.Register(ZoneActivatedEvent.ID, 0, false);
			}

			// Token: 0x0600E98D RID: 59789 RVA: 0x0053B534 File Offset: 0x00539734
			public override bool HandleEvent(SecretVisibilityChangedEvent E)
			{
				JournalMapNote journalMapNote = E.Entry as JournalMapNote;
				if (journalMapNote != null && journalMapNote.Revealed)
				{
					this.CheckCompleted(null, journalMapNote);
				}
				return base.HandleEvent(E);
			}

			// Token: 0x0600E98E RID: 59790 RVA: 0x0053B567 File Offset: 0x00539767
			public override bool HandleEvent(ZoneActivatedEvent E)
			{
				this.CheckCompleted(E.Zone, null);
				return base.HandleEvent(E);
			}

			// Token: 0x0600E98F RID: 59791 RVA: 0x0053B580 File Offset: 0x00539780
			public void CheckCompleted(Zone Zone = null, JournalMapNote Secret = null)
			{
				if (this.quests != null)
				{
					int num = this.quests.Count - 1;
					while (num >= 0 && num < this.quests.Count)
					{
						FindASiteDynamicQuestManager.FindASiteDynamicQuestManager_QuestEntry findASiteDynamicQuestManager_QuestEntry = this.quests[num];
						if (Zone == null)
						{
							Zone = The.Player.CurrentZone;
						}
						if (!(((Zone != null) ? Zone.ZoneID : null) != findASiteDynamicQuestManager_QuestEntry.zoneID) || The.ZoneManager.HasVisitedZone(findASiteDynamicQuestManager_QuestEntry.zoneID))
						{
							if (Secret == null)
							{
								Secret = JournalAPI.GetMapNote(findASiteDynamicQuestManager_QuestEntry.secretID);
								if (Secret != null && !Secret.Revealed)
								{
									goto IL_0240;
								}
							}
							else if (Secret.ID != findASiteDynamicQuestManager_QuestEntry.secretID)
							{
								goto IL_0240;
							}
							if (((Zone != null) ? Zone.ZoneID : null) == findASiteDynamicQuestManager_QuestEntry.zoneID || The.ZoneManager.HasVisitedZone(findASiteDynamicQuestManager_QuestEntry.zoneID) || (Secret != null && Secret.Revealed))
							{
								if (!The.Game.HasFinishedQuestStep(findASiteDynamicQuestManager_QuestEntry.questID, findASiteDynamicQuestManager_QuestEntry.questStepID))
								{
									string text;
									if ((text = ((Secret != null) ? Secret.Text : null)) == null)
									{
										text = ((Zone != null) ? Zone.DisplayName : null) ?? "a site";
									}
									string text2 = text;
									The.Game.FinishQuestStep(findASiteDynamicQuestManager_QuestEntry.questID, findASiteDynamicQuestManager_QuestEntry.questStepID, -1, true, null);
									JournalAPI.AddAccomplishment("You located " + text2 + ".", string.Concat(new string[]
									{
										"Through the use of ",
										IComponent<GameObject>.ThePlayer.GetPronounProvider(false).PossessiveAdjective,
										" divinely ",
										HistoricStringExpander.ExpandString("<spice.elements." + The.Player.GetMythicDomain(5, null) + ".adjectives.!random>", null),
										" eyes, =name= discovered the lost location of ",
										text2,
										"."
									}), string.Concat(new string[]
									{
										"Acting against the persecution of ",
										Factions.GetMostLikedFormattedName(true),
										", =name= led an army to the lost gates of ",
										text2,
										". ",
										The.Player.GetPronounProvider(false).CapitalizedSubjective,
										" liberated its citizens, who together in ",
										The.Player.GetPronounProvider(false).PossessiveAdjective,
										" honor <spice.history.gospels.Celebration.LateSultanate.!random>."
									}), null, "general", MuralCategory.VisitsLocation, MuralWeight.Medium, null, -1L, true);
								}
								if (this.quests.Contains(findASiteDynamicQuestManager_QuestEntry))
								{
									this.quests.Remove(findASiteDynamicQuestManager_QuestEntry);
								}
							}
						}
						IL_0240:
						num--;
					}
				}
				if (this.quests != null && this.quests.Count == 0)
				{
					The.Game.FlagSystemsForRemoval(typeof(FindASiteDynamicQuestManager.FindASiteDynamicQuestManagerSystem));
				}
			}

			// Token: 0x04006AF3 RID: 27379
			public List<FindASiteDynamicQuestManager.FindASiteDynamicQuestManager_QuestEntry> quests;
		}

		// Token: 0x02001AE3 RID: 6883
		[Serializable]
		public class FindASiteDynamicQuestManager_QuestEntry : IComposite
		{
			// Token: 0x17000EF1 RID: 3825
			// (get) Token: 0x0600E990 RID: 59792 RVA: 0x0053B80F File Offset: 0x00539A0F
			public bool WantFieldReflection
			{
				get
				{
					return false;
				}
			}

			// Token: 0x0600E991 RID: 59793 RVA: 0x0053B812 File Offset: 0x00539A12
			public void Write(SerializationWriter Writer)
			{
				Writer.WriteOptimized(this.zoneID);
				Writer.WriteOptimized(this.secretID);
				Writer.WriteOptimized(this.questID);
				Writer.WriteOptimized(this.questStepID);
			}

			// Token: 0x0600E992 RID: 59794 RVA: 0x0053B844 File Offset: 0x00539A44
			public void Read(SerializationReader Reader)
			{
				this.zoneID = Reader.ReadOptimizedString();
				this.secretID = Reader.ReadOptimizedString();
				this.questID = Reader.ReadOptimizedString();
				this.questStepID = Reader.ReadOptimizedString();
			}

			// Token: 0x04006AF4 RID: 27380
			public string zoneID;

			// Token: 0x04006AF5 RID: 27381
			public string secretID;

			// Token: 0x04006AF6 RID: 27382
			public string questID;

			// Token: 0x04006AF7 RID: 27383
			public string questStepID;
		}
	}
}
