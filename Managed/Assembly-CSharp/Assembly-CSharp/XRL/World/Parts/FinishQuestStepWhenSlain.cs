using System;

namespace XRL.World.Parts
{
	// Token: 0x02001208 RID: 4616
	[Serializable]
	public class FinishQuestStepWhenSlain : IPart
	{
		// Token: 0x17000B2A RID: 2858
		// (get) Token: 0x0600B7A0 RID: 47008 RVA: 0x0042D0A3 File Offset: 0x0042B2A3
		public virtual bool Clean
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600B7A1 RID: 47009 RVA: 0x0042D0A6 File Offset: 0x0042B2A6
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == ReplicaCreatedEvent.ID || ID == AfterDieEvent.ID || ID == ZoneActivatedEvent.ID || ID == PooledEvent<ReplaceInContextEvent>.ID;
		}

		// Token: 0x0600B7A2 RID: 47010 RVA: 0x0042D0D4 File Offset: 0x0042B2D4
		public override bool HandleEvent(ReplicaCreatedEvent E)
		{
			if (E.Object == this.ParentObject)
			{
				E.WantToRemove(this);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B7A3 RID: 47011 RVA: 0x0042D0F2 File Offset: 0x0042B2F2
		public override bool HandleEvent(AfterDieEvent E)
		{
			if (E.Dying == this.ParentObject)
			{
				this.Trigger();
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B7A4 RID: 47012 RVA: 0x0042D10F File Offset: 0x0042B30F
		public override bool HandleEvent(ZoneActivatedEvent E)
		{
			if (this.Clean && The.Game.HasFinishedQuestStep(this.Quest, this.Step))
			{
				this.ParentObject.RemovePart(this);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B7A5 RID: 47013 RVA: 0x0042D144 File Offset: 0x0042B344
		public override bool HandleEvent(ReplaceInContextEvent E)
		{
			this.ParentObject.RemovePart(this);
			E.Replacement.AddPart<FinishQuestStepWhenSlain>(this, true, false);
			return base.HandleEvent(E);
		}

		// Token: 0x0600B7A6 RID: 47014 RVA: 0x0042D168 File Offset: 0x0042B368
		public virtual void Trigger()
		{
			if (this.GameState != null)
			{
				The.Game.SetIntGameState(this.GameState, 1);
			}
			Quest quest;
			if (!The.Game.TryGetQuest(this.Quest, out quest))
			{
				if (!this.RequireQuest)
				{
					return;
				}
				quest = The.Game.StartQuest(this.Quest, null, null, null);
			}
			The.Game.FinishQuestStep(quest, this.Step, -1, true, null);
			if (this.Clean)
			{
				this.ParentObject.RemovePart(this);
			}
		}

		// Token: 0x040049D8 RID: 18904
		public string Quest;

		// Token: 0x040049D9 RID: 18905
		public string Step;

		// Token: 0x040049DA RID: 18906
		public string GameState;

		// Token: 0x040049DB RID: 18907
		public bool RequireQuest;
	}
}
