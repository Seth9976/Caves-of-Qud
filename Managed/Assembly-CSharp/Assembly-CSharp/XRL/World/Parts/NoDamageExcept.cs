using System;

namespace XRL.World.Parts
{
	// Token: 0x020011F6 RID: 4598
	[Serializable]
	public class NoDamageExcept : IPart
	{
		// Token: 0x0600B709 RID: 46857 RVA: 0x0042B14F File Offset: 0x0042934F
		public override bool SameAs(IPart p)
		{
			return !((p as NoDamageExcept).Except == this.Except) && base.SameAs(p);
		}

		// Token: 0x0600B70A RID: 46858 RVA: 0x0042B172 File Offset: 0x00429372
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == BeforeApplyDamageEvent.ID || ID == PooledEvent<CanBeDismemberedEvent>.ID || ID == PooledEvent<GetElectricalConductivityEvent>.ID;
		}

		// Token: 0x0600B70B RID: 46859 RVA: 0x0042B198 File Offset: 0x00429398
		public override bool HandleEvent(GetElectricalConductivityEvent E)
		{
			if (E.Pass == 1 && E.Object == this.ParentObject && !Damage.IsElectricDamage(this.Except))
			{
				E.Value = 0;
				return false;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B70C RID: 46860 RVA: 0x0042B1CE File Offset: 0x004293CE
		public override bool HandleEvent(BeforeApplyDamageEvent E)
		{
			if (E.Object == this.ParentObject)
			{
				if (this.Except.IsNullOrEmpty())
				{
					return false;
				}
				if (!E.Damage.HasAttribute(this.Except))
				{
					return false;
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B70D RID: 46861 RVA: 0x0042B20C File Offset: 0x0042940C
		public override bool HandleEvent(CanBeDismemberedEvent E)
		{
			if (E.Object == this.ParentObject)
			{
				if (this.Except.IsNullOrEmpty())
				{
					return false;
				}
				if (!E.Attributes.IsNullOrEmpty() && Array.IndexOf<string>(E.Attributes.Split(' ', StringSplitOptions.None), this.Except) == -1)
				{
					return false;
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B70E RID: 46862 RVA: 0x0042B268 File Offset: 0x00429468
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x040049BF RID: 18879
		public string Except;
	}
}
