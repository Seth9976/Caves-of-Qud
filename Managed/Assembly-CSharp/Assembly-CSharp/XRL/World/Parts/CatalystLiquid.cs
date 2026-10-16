using System;

namespace XRL.World.Parts
{
	// Token: 0x02000E3E RID: 3646
	[HasModSensitiveStaticCache]
	[Serializable]
	public class CatalystLiquid : IPart
	{
		// Token: 0x060093E3 RID: 37859 RVA: 0x00353C34 File Offset: 0x00351E34
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == ObjectCreatedEvent.ID || ID == PooledEvent<RepairedEvent>.ID;
		}

		// Token: 0x060093E4 RID: 37860 RVA: 0x00353C54 File Offset: 0x00351E54
		public void SetComponentsFromBloodOf(GameObject Object)
		{
			LiquidVolume liquidVolume = this.ParentObject.LiquidVolume;
			if (liquidVolume != null && Object != null)
			{
				liquidVolume.InitialLiquid = Object.GetBleedLiquid("proteangunk-1000");
			}
		}

		// Token: 0x060093E5 RID: 37861 RVA: 0x00353C84 File Offset: 0x00351E84
		public override bool HandleEvent(ObjectCreatedEvent E)
		{
			InteriorZone interiorZone = ZoneManager.ZoneGenerationContext as InteriorZone;
			if (interiorZone != null)
			{
				this.SetComponentsFromBloodOf(interiorZone.ParentObject);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x060093E6 RID: 37862 RVA: 0x00353CB4 File Offset: 0x00351EB4
		public override bool HandleEvent(RepairedEvent E)
		{
			InteriorZone interiorZone = this.ParentObject.CurrentZone as InteriorZone;
			if (interiorZone != null)
			{
				this.SetComponentsFromBloodOf(interiorZone.ParentObject);
			}
			return base.HandleEvent(E);
		}
	}
}
