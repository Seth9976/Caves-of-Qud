using System;

namespace XRL.World.Parts
{
	// Token: 0x02001235 RID: 4661
	[Serializable]
	public class HostileTurretBuilder : IPart
	{
		// Token: 0x0600B92E RID: 47406 RVA: 0x00439680 File Offset: 0x00437880
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<DidInitialEquipEvent>.ID;
		}

		// Token: 0x0600B92F RID: 47407 RVA: 0x00439698 File Offset: 0x00437898
		public override bool HandleEvent(DidInitialEquipEvent E)
		{
			this.ParentObject.FireEventOnBodyparts(Event.New("PrepIntegratedHostToReceiveAmmo", "Host", this.ParentObject));
			this.ParentObject.FireEventOnBodyparts(Event.New("GenerateIntegratedHostInitialAmmo", "Host", this.ParentObject));
			CommandReloadEvent.Execute(this.ParentObject, true, false, 0);
			this.ParentObject.RemovePart(this);
			return base.HandleEvent(E);
		}
	}
}
