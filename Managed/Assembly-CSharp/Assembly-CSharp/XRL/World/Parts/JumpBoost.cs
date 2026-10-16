using System;

namespace XRL.World.Parts
{
	// Token: 0x02001058 RID: 4184
	[Serializable]
	public class JumpBoost : IActivePart
	{
		// Token: 0x0600A695 RID: 42645 RVA: 0x003CEDCC File Offset: 0x003CCFCC
		public JumpBoost()
		{
			this.WorksOnWearer = true;
			this.WorksOnEquipper = true;
		}

		// Token: 0x0600A696 RID: 42646 RVA: 0x003CEDE2 File Offset: 0x003CCFE2
		public override bool WantEvent(int ID, int Cascade)
		{
			return base.WantEvent(ID, Cascade) || ID == PooledEvent<GetJumpingBehaviorEvent>.ID || ID == PooledEvent<JumpedEvent>.ID || ID == GetShortDescriptionEvent.ID;
		}

		// Token: 0x0600A697 RID: 42647 RVA: 0x003CEE08 File Offset: 0x003CD008
		public override bool HandleEvent(GetJumpingBehaviorEvent E)
		{
			if (base.IsReady(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
			{
				if (this.Stack)
				{
					E.CanJumpOverCreatures |= this.CanJumpOverCreatures;
					E.RangeModifier += this.RangeModifier;
					E.MinimumRange += this.MinimumRange;
					Templates.StatCollector stats = E.Stats;
					if (stats != null)
					{
						stats.AddLinearBonusModifier("Range", this.RangeModifier, this.ParentObject.BaseDisplayName);
					}
				}
				else if (E.Priority == 0 || E.Priority < this.EventPriority)
				{
					E.AbilityName = this.AbilityName.Coalesce(E.AbilityName);
					E.CanJumpOverCreatures = this.CanJumpOverCreatures;
					E.RangeModifier = this.RangeModifier;
					E.MinimumRange = this.MinimumRange;
					E.Priority = this.EventPriority;
					Templates.StatCollector stats2 = E.Stats;
					if (stats2 != null)
					{
						stats2.AddLinearBonusModifier("Range", this.RangeModifier, this.ParentObject.BaseDisplayName);
					}
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A698 RID: 42648 RVA: 0x003CEF3C File Offset: 0x003CD13C
		public override bool HandleEvent(JumpedEvent E)
		{
			if (E.Pass == JumpedEvent.PASSES)
			{
				base.ConsumeChargeIfOperational(false, false, false, false, false, false, false, false, false, 1, null, false, 0, false, null);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A699 RID: 42649 RVA: 0x003CEF84 File Offset: 0x003CD184
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			if (this.RangeModifier != 0)
			{
				E.Postfix.Compound("{{rules|", '\n').Append("This item ").Append((this.RangeModifier > 0) ? "increases" : "decreases")
					.Append(" jump distance by ")
					.Append(Math.Abs(this.RangeModifier))
					.Append(".}}");
			}
			if (this.MinimumRange != 0)
			{
				E.Postfix.Compound("{{rules|", '\n').AppendSigned(this.MinimumRange).Append(" minimum jump distance")
					.Append("}}");
			}
			if (this.CanJumpOverCreatures)
			{
				E.Postfix.Compound("{{rules|", '\n').Append("You can jump over creatures.").Append("}}");
			}
			return base.HandleEvent(E);
		}

		// Token: 0x040044CD RID: 17613
		public string AbilityName;

		// Token: 0x040044CE RID: 17614
		public int RangeModifier;

		// Token: 0x040044CF RID: 17615
		public int MinimumRange;

		// Token: 0x040044D0 RID: 17616
		public int EventPriority;

		// Token: 0x040044D1 RID: 17617
		public bool CanJumpOverCreatures;

		// Token: 0x040044D2 RID: 17618
		public bool Stack;
	}
}
