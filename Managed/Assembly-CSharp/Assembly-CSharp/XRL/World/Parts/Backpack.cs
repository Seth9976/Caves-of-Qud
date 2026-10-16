using System;

namespace XRL.World.Parts
{
	// Token: 0x02000FFE RID: 4094
	[Serializable]
	public class Backpack : IPart
	{
		// Token: 0x0600A325 RID: 41765 RVA: 0x003BA39D File Offset: 0x003B859D
		public override bool SameAs(IPart p)
		{
			return (p as Backpack).WeightWhenWorn == this.WeightWhenWorn && base.SameAs(p);
		}

		// Token: 0x0600A326 RID: 41766 RVA: 0x003BA3BB File Offset: 0x003B85BB
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == AdjustTotalWeightEvent.ID;
		}

		// Token: 0x0600A327 RID: 41767 RVA: 0x003BA3D1 File Offset: 0x003B85D1
		public override bool HandleEvent(AdjustTotalWeightEvent E)
		{
			if (this.ParentObject.Equipped != null)
			{
				E.Weight = E.Weight - E.BaseWeight + (double)this.WeightWhenWorn;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A328 RID: 41768 RVA: 0x003BA402 File Offset: 0x003B8602
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x04004395 RID: 17301
		public int WeightWhenWorn = 1;
	}
}
