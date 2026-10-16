using System;

namespace XRL.World.Parts
{
	// Token: 0x02000E82 RID: 3714
	[Serializable]
	public class MimicProperties : IPart
	{
		// Token: 0x060095C4 RID: 38340 RVA: 0x00362C48 File Offset: 0x00360E48
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == BeforeDeathRemovalEvent.ID || ID == EffectAppliedEvent.ID || ID == EffectRemovedEvent.ID;
		}

		// Token: 0x060095C5 RID: 38341 RVA: 0x00362C70 File Offset: 0x00360E70
		public override bool HandleEvent(BeforeDeathRemovalEvent E)
		{
			Zone currentZone = this.ParentObject.CurrentZone;
			if (currentZone != null && this.ParentObject.HasID)
			{
				currentZone.FireEvent(Event.New("CheckStuck", "Invalidate", this.ParentObject.ID));
			}
			return base.HandleEvent(E);
		}

		// Token: 0x060095C6 RID: 38342 RVA: 0x00362CC1 File Offset: 0x00360EC1
		public override bool HandleEvent(EffectAppliedEvent E)
		{
			this.ZoneCheckStuck();
			return base.HandleEvent(E);
		}

		// Token: 0x060095C7 RID: 38343 RVA: 0x00362CD0 File Offset: 0x00360ED0
		public override bool HandleEvent(EffectRemovedEvent E)
		{
			this.ZoneCheckStuck();
			return base.HandleEvent(E);
		}

		// Token: 0x060095C8 RID: 38344 RVA: 0x00362CE0 File Offset: 0x00360EE0
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
