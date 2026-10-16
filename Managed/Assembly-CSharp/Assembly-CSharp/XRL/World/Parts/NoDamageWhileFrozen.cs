using System;

namespace XRL.World.Parts
{
	// Token: 0x020011F7 RID: 4599
	[Serializable]
	public class NoDamageWhileFrozen : IPart
	{
		// Token: 0x0600B710 RID: 46864 RVA: 0x0042B273 File Offset: 0x00429473
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == SingletonEvent<BeginTakeActionEvent>.ID || ID == BeforeApplyDamageEvent.ID || ID == PooledEvent<CanBeDismemberedEvent>.ID || ID == PooledEvent<GetElectricalConductivityEvent>.ID;
		}

		// Token: 0x0600B711 RID: 46865 RVA: 0x0042B2A1 File Offset: 0x004294A1
		public override bool HandleEvent(BeginTakeActionEvent E)
		{
			if (!this.ParentObject.IsFrozen())
			{
				this.ParentObject.RemovePart(this);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B712 RID: 46866 RVA: 0x0042B2C3 File Offset: 0x004294C3
		public override bool HandleEvent(BeforeApplyDamageEvent E)
		{
			return E.Object != this.ParentObject && base.HandleEvent(E);
		}

		// Token: 0x0600B713 RID: 46867 RVA: 0x0042B2DC File Offset: 0x004294DC
		public override bool HandleEvent(CanBeDismemberedEvent E)
		{
			return E.Object != this.ParentObject && base.HandleEvent(E);
		}

		// Token: 0x0600B714 RID: 46868 RVA: 0x0042B2F5 File Offset: 0x004294F5
		public override bool HandleEvent(GetElectricalConductivityEvent E)
		{
			if (E.Pass == 1 && E.Object == this.ParentObject)
			{
				E.Value = 0;
				return false;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B715 RID: 46869 RVA: 0x0042B31E File Offset: 0x0042951E
		public override bool AllowStaticRegistration()
		{
			return true;
		}
	}
}
