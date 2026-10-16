using System;

namespace XRL.World.Parts
{
	// Token: 0x0200120B RID: 4619
	[Obsolete]
	[Serializable]
	public class QuestGiverImposter : IPartWithPrefabImposter
	{
		// Token: 0x0600B7AF RID: 47023 RVA: 0x0042D34F File Offset: 0x0042B54F
		public override bool SameAs(IPart p)
		{
			return true;
		}

		// Token: 0x0600B7B0 RID: 47024 RVA: 0x0042D352 File Offset: 0x0042B552
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == AfterConversationEvent.ID || ID == SingletonEvent<BeginTakeActionEvent>.ID || ID == EnteredCellEvent.ID || ID == ZoneActivatedEvent.ID;
		}

		// Token: 0x0600B7B1 RID: 47025 RVA: 0x0042D380 File Offset: 0x0042B580
		public override bool HandleEvent(AfterConversationEvent E)
		{
			this.UpdateIcon();
			return base.HandleEvent(E);
		}

		// Token: 0x0600B7B2 RID: 47026 RVA: 0x0042D38F File Offset: 0x0042B58F
		public override bool HandleEvent(BeginTakeActionEvent E)
		{
			this.UpdateIcon();
			return base.HandleEvent(E);
		}

		// Token: 0x0600B7B3 RID: 47027 RVA: 0x0042D39E File Offset: 0x0042B59E
		public override bool HandleEvent(EnteredCellEvent E)
		{
			this.UpdateIcon();
			return base.HandleEvent(E);
		}

		// Token: 0x0600B7B4 RID: 47028 RVA: 0x0042D3AD File Offset: 0x0042B5AD
		public override bool HandleEvent(ZoneActivatedEvent E)
		{
			this.UpdateIcon();
			return base.HandleEvent(E);
		}

		// Token: 0x0600B7B5 RID: 47029 RVA: 0x0042D3BC File Offset: 0x0042B5BC
		public void UpdateIcon()
		{
			if (this.GivesQuestList == null)
			{
				this.GivesQuestList = this.GivesQuests.Split(',', StringSplitOptions.None);
			}
			if (this.AcceptsQuestList == null)
			{
				this.AcceptsQuestList = this.AcceptsQuests.Split(',', StringSplitOptions.None);
			}
			foreach (string text in this.GivesQuestList)
			{
				if (!IComponent<GameObject>.TheGame.HasQuest(text))
				{
					this.prefabID = "Prefabs/Imposters/QuestAvailable";
					return;
				}
			}
			foreach (string text2 in this.AcceptsQuestList)
			{
				if (IComponent<GameObject>.TheGame.HasQuest(text2) && !IComponent<GameObject>.TheGame.FinishedQuests.ContainsKey(text2) && IComponent<GameObject>.TheGame.Quests[text2].ReadyToTurnIn())
				{
					this.prefabID = "Prefabs/Imposters/QuestComplete";
					return;
				}
			}
			this.prefabID = null;
		}

		// Token: 0x040049DE RID: 18910
		public string GivesQuests = "";

		// Token: 0x040049DF RID: 18911
		public string AcceptsQuests = "";

		// Token: 0x040049E0 RID: 18912
		[NonSerialized]
		public string[] GivesQuestList;

		// Token: 0x040049E1 RID: 18913
		[NonSerialized]
		public string[] AcceptsQuestList;
	}
}
