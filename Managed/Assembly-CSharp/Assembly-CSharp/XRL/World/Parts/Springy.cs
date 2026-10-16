using System;

namespace XRL.World.Parts
{
	// Token: 0x02001202 RID: 4610
	[Serializable]
	public class Springy : IPart
	{
		// Token: 0x0600B75B RID: 46939 RVA: 0x0042BE74 File Offset: 0x0042A074
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<GetSpringinessEvent>.ID;
		}

		// Token: 0x0600B75C RID: 46940 RVA: 0x0042BE8C File Offset: 0x0042A08C
		public override bool HandleEvent(GetSpringinessEvent E)
		{
			if (this.Factor > 0f)
			{
				E.LinearIncrease += this.ParentObject.GetWeightTimes((double)this.Factor);
			}
			else
			{
				E.LinearReduction += -this.ParentObject.GetWeightTimes((double)this.Factor);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x040049C5 RID: 18885
		public float Factor = 0.5f;
	}
}
