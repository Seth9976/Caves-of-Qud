using System;

namespace XRL.World.Parts
{
	// Token: 0x02000FF0 RID: 4080
	[Serializable]
	public class AdjustLiquidWeightWhileWorn : IPart
	{
		// Token: 0x0600A2AE RID: 41646 RVA: 0x003B7052 File Offset: 0x003B5252
		public override bool SameAs(IPart p)
		{
			return (p as AdjustLiquidWeightWhileWorn).Factor == this.Factor && base.SameAs(p);
		}

		// Token: 0x0600A2AF RID: 41647 RVA: 0x003B7070 File Offset: 0x003B5270
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == AdjustTotalWeightEvent.ID;
		}

		// Token: 0x0600A2B0 RID: 41648 RVA: 0x003B7088 File Offset: 0x003B5288
		public override bool HandleEvent(AdjustTotalWeightEvent E)
		{
			if (this.ParentObject.Equipped != null)
			{
				LiquidVolume liquidVolume = this.ParentObject.LiquidVolume;
				if (liquidVolume != null)
				{
					double liquidWeight = liquidVolume.GetLiquidWeight();
					if (liquidWeight != 0.0)
					{
						E.Weight -= liquidWeight;
						if (this.Factor != 0f)
						{
							E.Weight += liquidWeight * (double)this.Factor;
						}
					}
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A2B1 RID: 41649 RVA: 0x003B70FC File Offset: 0x003B52FC
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x04004362 RID: 17250
		public float Factor;
	}
}
