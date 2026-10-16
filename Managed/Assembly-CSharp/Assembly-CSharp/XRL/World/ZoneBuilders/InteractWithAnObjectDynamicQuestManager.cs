using System;
using System.Collections.Generic;
using System.Linq;
using HistoryKit;
using Qud.API;
using UnityEngine;
using XRL.Language;

namespace XRL.World.ZoneBuilders
{
	// Token: 0x02000A34 RID: 2612
	[Serializable]
	public class InteractWithAnObjectDynamicQuestManager : QuestManager
	{
		// Token: 0x06006DA5 RID: 28069 RVA: 0x0024E506 File Offset: 0x0024C706
		public InteractWithAnObjectDynamicQuestManager()
		{
		}

		// Token: 0x06006DA6 RID: 28070 RVA: 0x0024E50E File Offset: 0x0024C70E
		public InteractWithAnObjectDynamicQuestManager(string itemID, string questID, string questStepID)
		{
			this._itemID = itemID;
			this._questID = questID;
			this._questStepID = questStepID;
		}

		// Token: 0x06006DA7 RID: 28071 RVA: 0x0024E52C File Offset: 0x0024C72C
		public override void OnQuestAdded()
		{
			InteractWithAnObjectDynamicQuestManager.System system = The.Game.RequireSystem<InteractWithAnObjectDynamicQuestManager.System>();
			InteractWithAnObjectDynamicQuestManager.QuestEntry questEntry = new InteractWithAnObjectDynamicQuestManager.QuestEntry();
			GameObject gameObject = The.ZoneManager.peekCachedObject(this._itemID);
			questEntry.itemID = this._itemID;
			questEntry.verb = gameObject.GetStringProperty("QuestVerb", null);
			questEntry.questID = this._questID;
			questEntry.questStepID = this._questStepID;
			if (system.quests == null)
			{
				system.quests = new List<InteractWithAnObjectDynamicQuestManager.QuestEntry>();
			}
			system.quests.Add(questEntry);
		}

		// Token: 0x06006DA8 RID: 28072 RVA: 0x0024E5B0 File Offset: 0x0024C7B0
		public override GameObject GetQuestInfluencer()
		{
			return GameObject.Find((GameObject obj) => obj.GetStringProperty("GivesDynamicQuest", null) == this._questID);
		}

		// Token: 0x0400321E RID: 12830
		public string _itemID;

		// Token: 0x0400321F RID: 12831
		public string _questID;

		// Token: 0x04003220 RID: 12832
		public string _questStepID;

		// Token: 0x04003221 RID: 12833
		public List<InteractWithAnObjectDynamicQuestManager.QuestEntry> quests;

		// Token: 0x02001AEE RID: 6894
		[Serializable]
		public class System : IPlayerSystem
		{
			// Token: 0x0600E9BD RID: 59837 RVA: 0x0053BDCC File Offset: 0x00539FCC
			public void QuestableInteract(GameObject Object)
			{
				if (Object == null || this.quests == null)
				{
					return;
				}
				for (int i = this.quests.Count - 1; i >= 0; i--)
				{
					InteractWithAnObjectDynamicQuestManager.QuestEntry questEntry = this.quests[i];
					if (Object.IDMatch(questEntry.itemID))
					{
						this.FinishEntry(questEntry, Object);
					}
				}
			}

			// Token: 0x0600E9BE RID: 59838 RVA: 0x0053BE20 File Offset: 0x0053A020
			public void FinishEntry(InteractWithAnObjectDynamicQuestManager.QuestEntry Entry, GameObject Object)
			{
				The.Game.FinishQuestStep(Entry.questID, Entry.questStepID, -1, true, null);
				GameObject gameObject = The.ZoneManager.peekCachedObject(Entry.itemID);
				if (gameObject == null)
				{
					Debug.LogError("no cached object for item ID " + Entry.itemID);
					return;
				}
				string[] array = Entry.verb.Split(' ', StringSplitOptions.None);
				string text = ((array.Length > 1) ? (Grammar.PastTenseOf(array[0]) + " " + string.Join(" ", array.Skip<string>(1).ToArray<string>())) : Grammar.PastTenseOf(Entry.verb));
				string text2 = HistoricStringExpander.ExpandString("<spice.professions.!random>", null);
				JournalAPI.AddAccomplishment(string.Concat(new string[]
				{
					"You ",
					text,
					" ",
					gameObject.t(int.MaxValue, null, null, false, false, false, false, false, true, false, false, null, false, true, false, null, true, null, false),
					"."
				}), string.Concat(new string[]
				{
					"While exploring ",
					Object.CurrentZone.DisplayName,
					", =name= ",
					text,
					" the fabled contraption called ",
					gameObject.t(int.MaxValue, null, null, false, false, false, false, false, true, true, true, null, false, true, false, null, true, null, false),
					"."
				}), string.Concat(new string[]
				{
					"While visiting an obscure <spice.professions.",
					text2,
					".guildhall>, =name= met with a group of <spice.professions.",
					text2,
					".plural> and commissed what came to be known as ",
					gameObject.t(int.MaxValue, null, null, false, false, false, false, false, true, true, true, null, false, true, false, null, true, null, false),
					"."
				}), null, "general", MuralCategory.FindsObject, MuralWeight.Medium, null, -1L, true);
				this.quests.Remove(Entry);
			}

			// Token: 0x04006B10 RID: 27408
			public List<InteractWithAnObjectDynamicQuestManager.QuestEntry> quests;
		}

		// Token: 0x02001AEF RID: 6895
		[Serializable]
		public class QuestEntry : IComposite
		{
			// Token: 0x17000EF3 RID: 3827
			// (get) Token: 0x0600E9BF RID: 59839 RVA: 0x0053BFFA File Offset: 0x0053A1FA
			public bool WantFieldReflection
			{
				get
				{
					return false;
				}
			}

			// Token: 0x0600E9C0 RID: 59840 RVA: 0x0053BFFD File Offset: 0x0053A1FD
			public void Write(SerializationWriter Writer)
			{
				Writer.WriteOptimized(this.itemID);
				Writer.WriteOptimized(this.verb);
				Writer.WriteOptimized(this.questID);
				Writer.WriteOptimized(this.questStepID);
			}

			// Token: 0x0600E9C1 RID: 59841 RVA: 0x0053C02F File Offset: 0x0053A22F
			public void Read(SerializationReader Reader)
			{
				this.itemID = Reader.ReadOptimizedString();
				this.verb = Reader.ReadOptimizedString();
				this.questID = Reader.ReadOptimizedString();
				this.questStepID = Reader.ReadOptimizedString();
			}

			// Token: 0x04006B11 RID: 27409
			public string itemID;

			// Token: 0x04006B12 RID: 27410
			public string verb;

			// Token: 0x04006B13 RID: 27411
			public string questID;

			// Token: 0x04006B14 RID: 27412
			public string questStepID;
		}
	}
}
