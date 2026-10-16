using System;

namespace XRL.World
{
	// Token: 0x020007E0 RID: 2016
	[GameEvent(Base = true)]
	public abstract class IXPEvent : MinEvent
	{
		// Token: 0x06005120 RID: 20768 RVA: 0x001889C7 File Offset: 0x00186BC7
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06005121 RID: 20769 RVA: 0x001889D0 File Offset: 0x00186BD0
		public override void Reset()
		{
			base.Reset();
			this.Actor = null;
			this.Kill = null;
			this.InfluencedBy = null;
			this.PassedUpFrom = null;
			this.PassedDownFrom = null;
			this.ZoneID = null;
			this.Amount = 0;
			this.AmountBefore = 0;
			this.Tier = 0;
			this.Minimum = 0;
			this.Maximum = 0;
			this.Deed = null;
			this.TierScaling = true;
		}

		// Token: 0x06005122 RID: 20770 RVA: 0x00188A40 File Offset: 0x00186C40
		public void ApplyTo(IXPEvent E)
		{
			E.Actor = this.Actor;
			E.Kill = this.Kill;
			E.InfluencedBy = this.InfluencedBy;
			E.PassedUpFrom = this.PassedUpFrom;
			E.PassedDownFrom = this.PassedDownFrom;
			E.ZoneID = this.ZoneID;
			E.Amount = this.Amount;
			E.AmountBefore = this.AmountBefore;
			E.Tier = this.Tier;
			E.Minimum = this.Minimum;
			E.Maximum = this.Maximum;
			E.Deed = this.Deed;
			E.TierScaling = this.TierScaling;
		}

		// Token: 0x04002892 RID: 10386
		public GameObject Actor;

		// Token: 0x04002893 RID: 10387
		public GameObject Kill;

		// Token: 0x04002894 RID: 10388
		public GameObject InfluencedBy;

		// Token: 0x04002895 RID: 10389
		public GameObject PassedUpFrom;

		// Token: 0x04002896 RID: 10390
		public GameObject PassedDownFrom;

		// Token: 0x04002897 RID: 10391
		public string ZoneID;

		// Token: 0x04002898 RID: 10392
		public int Amount;

		// Token: 0x04002899 RID: 10393
		public int AmountBefore;

		// Token: 0x0400289A RID: 10394
		public int Tier;

		// Token: 0x0400289B RID: 10395
		public int Minimum;

		// Token: 0x0400289C RID: 10396
		public int Maximum;

		// Token: 0x0400289D RID: 10397
		public string Deed;

		// Token: 0x0400289E RID: 10398
		public bool TierScaling = true;
	}
}
