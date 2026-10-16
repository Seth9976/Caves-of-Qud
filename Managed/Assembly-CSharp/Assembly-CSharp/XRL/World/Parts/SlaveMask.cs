using System;

namespace XRL.World.Parts
{
	// Token: 0x02001097 RID: 4247
	[Serializable]
	public class SlaveMask : IPart
	{
		// Token: 0x0600A90A RID: 43274 RVA: 0x003DD14D File Offset: 0x003DB34D
		public override bool WantTurnTick()
		{
			return !this.AffiliationResetDone;
		}

		// Token: 0x0600A90B RID: 43275 RVA: 0x003DD158 File Offset: 0x003DB358
		public override void TurnTick(long TimeTick, int Amount)
		{
			this.CheckForAffiliationReset(null);
		}

		// Token: 0x0600A90C RID: 43276 RVA: 0x003DD161 File Offset: 0x003DB361
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == UnequippedEvent.ID;
		}

		// Token: 0x0600A90D RID: 43277 RVA: 0x003DD177 File Offset: 0x003DB377
		public override bool HandleEvent(UnequippedEvent E)
		{
			this.ResetAffiliation(E.Actor);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A90E RID: 43278 RVA: 0x003DD18C File Offset: 0x003DB38C
		public void ResetAffiliation(GameObject who = null)
		{
			if (this.AffiliationResetDone)
			{
				return;
			}
			if (who == null)
			{
				who = this.ParentObject.Equipped;
				if (who == null)
				{
					return;
				}
			}
			this.AffiliationResetDone = true;
			who.PartyLeader = null;
			Brain brain = who.Brain;
			if (brain != null)
			{
				brain.Goals.Clear();
			}
			who.RemovePart<Preacher>();
			who.RemovePart<DomesticatedSlave>();
			string partParameter = who.GetBlueprint(true).GetPartParameter<string>("ConversationScript", "ConversationID", null);
			if (!string.IsNullOrEmpty(partParameter))
			{
				who.RequirePart<ConversationScript>(false).ConversationID = partParameter;
			}
			this.ParentObject.RemovePart(this);
		}

		// Token: 0x0600A90F RID: 43279 RVA: 0x003DD224 File Offset: 0x003DB424
		public void CheckForAffiliationReset(GameObject who = null)
		{
			if (this.AffiliationResetDone)
			{
				return;
			}
			if (who == null)
			{
				who = this.ParentObject.Equipped;
				if (who == null)
				{
					return;
				}
			}
			GameObject partyLeader = who.PartyLeader;
			if (!GameObject.Validate(ref partyLeader))
			{
				this.ResetAffiliation(who);
			}
		}

		// Token: 0x040045CE RID: 17870
		public bool AffiliationResetDone;
	}
}
