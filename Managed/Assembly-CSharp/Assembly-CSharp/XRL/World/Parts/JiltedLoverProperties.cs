using System;

namespace XRL.World.Parts
{
	// Token: 0x02000E7C RID: 3708
	[Serializable]
	public class JiltedLoverProperties : IPart
	{
		// Token: 0x060095A6 RID: 38310 RVA: 0x003623F6 File Offset: 0x003605F6
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == BeforeDeathRemovalEvent.ID || ID == EffectAppliedEvent.ID || ID == EffectRemovedEvent.ID;
		}

		// Token: 0x060095A7 RID: 38311 RVA: 0x0036241C File Offset: 0x0036061C
		public override bool HandleEvent(BeforeDeathRemovalEvent E)
		{
			Zone currentZone = this.ParentObject.CurrentZone;
			if (currentZone != null && this.ParentObject.HasID)
			{
				currentZone.FireEvent(Event.New("CheckStuck", "Invalidate", this.ParentObject.ID));
			}
			return base.HandleEvent(E);
		}

		// Token: 0x060095A8 RID: 38312 RVA: 0x0036246D File Offset: 0x0036066D
		public override bool HandleEvent(EffectAppliedEvent E)
		{
			this.ZoneCheckStuck();
			return base.HandleEvent(E);
		}

		// Token: 0x060095A9 RID: 38313 RVA: 0x0036247C File Offset: 0x0036067C
		public override bool HandleEvent(EffectRemovedEvent E)
		{
			this.ZoneCheckStuck();
			return base.HandleEvent(E);
		}

		// Token: 0x060095AA RID: 38314 RVA: 0x0036248C File Offset: 0x0036068C
		public void ZoneCheckStuck()
		{
			Zone currentZone = this.ParentObject.CurrentZone;
			if (currentZone != null && this.ParentObject.HasID)
			{
				currentZone.FireEvent(Event.New("CheckStuck", 0, 0, 0));
			}
		}
	}
}
