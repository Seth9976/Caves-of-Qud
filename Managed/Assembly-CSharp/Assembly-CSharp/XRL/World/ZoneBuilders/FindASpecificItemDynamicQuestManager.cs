using System;
using System.Collections.Generic;
using HistoryKit;
using Qud.API;

namespace XRL.World.ZoneBuilders
{
	// Token: 0x02000A31 RID: 2609
	[Serializable]
	public class FindASpecificItemDynamicQuestManager : QuestManager
	{
		// Token: 0x06006D95 RID: 28053 RVA: 0x0024D74C File Offset: 0x0024B94C
		public FindASpecificItemDynamicQuestManager()
		{
		}

		// Token: 0x06006D96 RID: 28054 RVA: 0x0024D754 File Offset: 0x0024B954
		public FindASpecificItemDynamicQuestManager(string itemID, string questID, string questStepID)
		{
			this._itemID = itemID;
			this._questID = questID;
			this._questStepID = questStepID;
		}

		// Token: 0x06006D97 RID: 28055 RVA: 0x0024D771 File Offset: 0x0024B971
		public override void OnQuestAdded()
		{
			this.RequireEntry();
		}

		// Token: 0x06006D98 RID: 28056 RVA: 0x0024D77C File Offset: 0x0024B97C
		public override void AfterQuestAdded()
		{
			FindASpecificItemDynamicQuestManager.FindASpecificItemDynamicQuestManagerSystem manager = The.Game.RequireSystem<FindASpecificItemDynamicQuestManager.FindASpecificItemDynamicQuestManagerSystem>(() => new FindASpecificItemDynamicQuestManager.FindASpecificItemDynamicQuestManagerSystem());
			if (manager.quests != null)
			{
				IComponent<GameObject>.ThePlayer.ForeachInventoryAndEquipment(delegate(GameObject GO)
				{
					manager.CheckCompleted(GO);
				});
			}
		}

		// Token: 0x06006D99 RID: 28057 RVA: 0x0024D7E1 File Offset: 0x0024B9E1
		public override GameObject GetQuestInfluencer()
		{
			return GameObject.Find((GameObject obj) => obj.GetStringProperty("GivesDynamicQuest", null) == this._questID);
		}

		// Token: 0x06006D9A RID: 28058 RVA: 0x0024D7F4 File Offset: 0x0024B9F4
		public override void Read(GameObject Basis, SerializationReader Reader)
		{
			base.Read(Basis, Reader);
			this.RequireEntry();
		}

		// Token: 0x06006D9B RID: 28059 RVA: 0x0024D808 File Offset: 0x0024BA08
		public FindASpecificItemDynamicQuestManager.FindASpecificItemDynamicQuestManager_QuestEntry RequireEntry()
		{
			if (The.Game.HasFinishedQuest(this._questID))
			{
				return null;
			}
			FindASpecificItemDynamicQuestManager.FindASpecificItemDynamicQuestManagerSystem findASpecificItemDynamicQuestManagerSystem = The.Game.RequireSystem<FindASpecificItemDynamicQuestManager.FindASpecificItemDynamicQuestManagerSystem>();
			FindASpecificItemDynamicQuestManager.FindASpecificItemDynamicQuestManagerSystem findASpecificItemDynamicQuestManagerSystem2 = findASpecificItemDynamicQuestManagerSystem;
			if (findASpecificItemDynamicQuestManagerSystem2.quests == null)
			{
				findASpecificItemDynamicQuestManagerSystem2.quests = new List<FindASpecificItemDynamicQuestManager.FindASpecificItemDynamicQuestManager_QuestEntry>();
			}
			foreach (FindASpecificItemDynamicQuestManager.FindASpecificItemDynamicQuestManager_QuestEntry findASpecificItemDynamicQuestManager_QuestEntry in findASpecificItemDynamicQuestManagerSystem.quests)
			{
				if (findASpecificItemDynamicQuestManager_QuestEntry.itemID == this._itemID)
				{
					return findASpecificItemDynamicQuestManager_QuestEntry;
				}
			}
			FindASpecificItemDynamicQuestManager.FindASpecificItemDynamicQuestManager_QuestEntry findASpecificItemDynamicQuestManager_QuestEntry2 = new FindASpecificItemDynamicQuestManager.FindASpecificItemDynamicQuestManager_QuestEntry
			{
				itemID = this._itemID,
				questID = this._questID,
				questStepID = this._questStepID
			};
			findASpecificItemDynamicQuestManagerSystem.quests.Add(findASpecificItemDynamicQuestManager_QuestEntry2);
			return findASpecificItemDynamicQuestManager_QuestEntry2;
		}

		// Token: 0x04003214 RID: 12820
		public string _itemID;

		// Token: 0x04003215 RID: 12821
		public string _questID;

		// Token: 0x04003216 RID: 12822
		public string _questStepID;

		// Token: 0x02001AE8 RID: 6888
		[Serializable]
		public class FindASpecificItemDynamicQuestManagerSystem : IPlayerSystem
		{
			// Token: 0x0600E9A4 RID: 59812 RVA: 0x0053BA1A File Offset: 0x00539C1A
			public override void RegisterPlayer(GameObject Player, IEventRegistrar Registrar)
			{
				Registrar.Register(AddedToInventoryEvent.ID, 0, false);
				Registrar.Register(TookEvent.ID, 0, false);
				Registrar.Register(EquipperEquippedEvent.ID, 0, false);
				Registrar.Register(InventoryActionEvent.ID, 0, false);
			}

			// Token: 0x0600E9A5 RID: 59813 RVA: 0x0053BA50 File Offset: 0x00539C50
			public override bool HandleEvent(AddedToInventoryEvent E)
			{
				this.CheckCompleted(E.Item);
				return base.HandleEvent(E);
			}

			// Token: 0x0600E9A6 RID: 59814 RVA: 0x0053BA65 File Offset: 0x00539C65
			public override bool HandleEvent(TookEvent E)
			{
				this.CheckCompleted(E.Item);
				return base.HandleEvent(E);
			}

			// Token: 0x0600E9A7 RID: 59815 RVA: 0x0053BA7A File Offset: 0x00539C7A
			public override bool HandleEvent(EquipperEquippedEvent E)
			{
				this.CheckCompleted(E.Item);
				return base.HandleEvent(E);
			}

			// Token: 0x0600E9A8 RID: 59816 RVA: 0x0053BA8F File Offset: 0x00539C8F
			public override bool HandleEvent(InventoryActionEvent E)
			{
				this.CheckCompleted(E.Item);
				return base.HandleEvent(E);
			}

			// Token: 0x0600E9A9 RID: 59817 RVA: 0x0053BAA4 File Offset: 0x00539CA4
			public void CheckCompleted(GameObject go)
			{
				if (GameObject.Validate(go) && this.quests != null)
				{
					int num = this.quests.Count - 1;
					while (num >= 0 && num < this.quests.Count)
					{
						FindASpecificItemDynamicQuestManager.FindASpecificItemDynamicQuestManager_QuestEntry findASpecificItemDynamicQuestManager_QuestEntry = this.quests[num];
						if (go.IDMatch(findASpecificItemDynamicQuestManager_QuestEntry.itemID))
						{
							The.Game.FinishQuestStep(findASpecificItemDynamicQuestManager_QuestEntry.questID, findASpecificItemDynamicQuestManager_QuestEntry.questStepID, -1, true, null);
							string referenceDisplayName = The.ZoneManager.peekCachedObject(findASpecificItemDynamicQuestManager_QuestEntry.itemID).GetReferenceDisplayName(int.MaxValue, null, null, false, false, false, false, false, false, false, true, null, false);
							string text = HistoricStringExpander.ExpandString("<spice.professions.!random>", null);
							JournalAPI.AddAccomplishment("You recovered " + referenceDisplayName + ".", string.Concat(new string[]
							{
								"While exploring ",
								The.Player.CurrentZone.DisplayName,
								", =name= recovered the fabled artifact called ",
								referenceDisplayName,
								"."
							}), string.Concat(new string[] { "While visiting an obscure <spice.professions.", text, ".guildhall>, =name= met with a group of <spice.professions.", text, ".plural> and commissed what came to be known as the ", referenceDisplayName, "." }), null, "general", MuralCategory.FindsObject, MuralWeight.Medium, null, -1L, true);
							this.quests.Remove(findASpecificItemDynamicQuestManager_QuestEntry);
						}
						num--;
					}
				}
				if (this.quests != null && this.quests.Count == 0)
				{
					The.Game.FlagSystemsForRemoval(typeof(FindASpecificItemDynamicQuestManager.FindASpecificItemDynamicQuestManagerSystem));
				}
			}

			// Token: 0x04006B03 RID: 27395
			public List<FindASpecificItemDynamicQuestManager.FindASpecificItemDynamicQuestManager_QuestEntry> quests;
		}

		// Token: 0x02001AE9 RID: 6889
		[Serializable]
		public class FindASpecificItemDynamicQuestManager_QuestEntry : IComposite
		{
			// Token: 0x17000EF2 RID: 3826
			// (get) Token: 0x0600E9AA RID: 59818 RVA: 0x0053BC27 File Offset: 0x00539E27
			public bool WantFieldReflection
			{
				get
				{
					return false;
				}
			}

			// Token: 0x0600E9AB RID: 59819 RVA: 0x0053BC2A File Offset: 0x00539E2A
			public void Write(SerializationWriter Writer)
			{
				Writer.WriteOptimized(this.itemID);
				Writer.WriteOptimized(this.questID);
				Writer.WriteOptimized(this.questStepID);
			}

			// Token: 0x0600E9AC RID: 59820 RVA: 0x0053BC50 File Offset: 0x00539E50
			public void Read(SerializationReader Reader)
			{
				this.itemID = Reader.ReadOptimizedString();
				this.questID = Reader.ReadOptimizedString();
				this.questStepID = Reader.ReadOptimizedString();
			}

			// Token: 0x04006B04 RID: 27396
			public string itemID;

			// Token: 0x04006B05 RID: 27397
			public string questID;

			// Token: 0x04006B06 RID: 27398
			public string questStepID;
		}
	}
}
