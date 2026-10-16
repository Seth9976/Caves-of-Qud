using System;

namespace XRL.World.Parts
{
	// Token: 0x02001206 RID: 4614
	[Serializable]
	public class CompleteQuestOnTaken : IPart
	{
		// Token: 0x0600B793 RID: 46995 RVA: 0x0042CEE8 File Offset: 0x0042B0E8
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x0600B794 RID: 46996 RVA: 0x0042CEEB File Offset: 0x0042B0EB
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == EquippedEvent.ID || ID == TakenEvent.ID || ID == DroppedEvent.ID || ID == QuestStartedEvent.ID || ID == UnequippedEvent.ID;
		}

		// Token: 0x0600B795 RID: 46997 RVA: 0x0042CF21 File Offset: 0x0042B121
		public override bool HandleEvent(DroppedEvent E)
		{
			this.CompleteQuest(E.Actor);
			return base.HandleEvent(E);
		}

		// Token: 0x0600B796 RID: 46998 RVA: 0x0042CF36 File Offset: 0x0042B136
		public override bool HandleEvent(EquippedEvent E)
		{
			this.CompleteQuest(E.Actor);
			return base.HandleEvent(E);
		}

		// Token: 0x0600B797 RID: 46999 RVA: 0x0042CF4B File Offset: 0x0042B14B
		public override bool HandleEvent(TakenEvent E)
		{
			this.CompleteQuest(E.Actor);
			return base.HandleEvent(E);
		}

		// Token: 0x0600B798 RID: 47000 RVA: 0x0042CF60 File Offset: 0x0042B160
		public override bool HandleEvent(UnequippedEvent E)
		{
			this.CompleteQuest(E.Actor);
			return base.HandleEvent(E);
		}

		// Token: 0x0600B799 RID: 47001 RVA: 0x0042CF75 File Offset: 0x0042B175
		public override bool HandleEvent(QuestStartedEvent E)
		{
			if (E.Quest.Name == this.Quest)
			{
				this.CompleteQuest(The.Player);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B79A RID: 47002 RVA: 0x0042CFA1 File Offset: 0x0042B1A1
		public void CompleteQuest(GameObject Actor)
		{
			if (Actor != null && Actor.IsPlayer())
			{
				The.Game.FinishQuestStep(this.Quest, this.QuestStep, -1, true, null);
			}
		}

		// Token: 0x040049D6 RID: 18902
		public string Quest;

		// Token: 0x040049D7 RID: 18903
		public string QuestStep;
	}
}
