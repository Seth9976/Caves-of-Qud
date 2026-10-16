using System;

namespace XRL.World.Parts
{
	// Token: 0x020010CA RID: 4298
	[Serializable]
	public class DosageChange : IPart
	{
		// Token: 0x0600AB00 RID: 43776 RVA: 0x003E8C8B File Offset: 0x003E6E8B
		public override bool SameAs(IPart p)
		{
			return (p as DosageChange).Amount == this.Amount && base.SameAs(p);
		}

		// Token: 0x0600AB01 RID: 43777 RVA: 0x003E8CA9 File Offset: 0x003E6EA9
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<GetTonicDosageEvent>.ID;
		}

		// Token: 0x0600AB02 RID: 43778 RVA: 0x003E8CBF File Offset: 0x003E6EBF
		public override bool HandleEvent(GetTonicDosageEvent E)
		{
			if (E.Object == this.ParentObject)
			{
				E.Dosage += this.Amount;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x04004682 RID: 18050
		public int Amount = 1;
	}
}
