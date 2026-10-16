using System;

namespace XRL.World.Parts
{
	// Token: 0x02000DFF RID: 3583
	[Serializable]
	public class TakenWXU : IPart
	{
		// Token: 0x060090EA RID: 37098 RVA: 0x003437B2 File Offset: 0x003419B2
		public TakenWXU()
		{
		}

		// Token: 0x060090EB RID: 37099 RVA: 0x003437C1 File Offset: 0x003419C1
		public TakenWXU(int WXU)
		{
			this.WXU = WXU;
		}

		// Token: 0x060090EC RID: 37100 RVA: 0x003437D8 File Offset: 0x003419D8
		public override bool SameAs(IPart p)
		{
			TakenWXU takenWXU = p as TakenWXU;
			return takenWXU.WXU == this.WXU && takenWXU.Triggered == this.Triggered && base.SameAs(p);
		}

		// Token: 0x060090ED RID: 37101 RVA: 0x00343813 File Offset: 0x00341A13
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == EquippedEvent.ID || ID == TakenEvent.ID;
		}

		// Token: 0x060090EE RID: 37102 RVA: 0x00343831 File Offset: 0x00341A31
		public override bool HandleEvent(IActOnItemEvent E)
		{
			if (!this.Triggered && E.Actor != null && E.Actor.IsPlayer())
			{
				this.Triggered = true;
				WanderSystem.AwardWXU(this.WXU);
			}
			return true;
		}

		// Token: 0x04003C6E RID: 15470
		public int WXU = 1;

		// Token: 0x04003C6F RID: 15471
		public bool Triggered;
	}
}
