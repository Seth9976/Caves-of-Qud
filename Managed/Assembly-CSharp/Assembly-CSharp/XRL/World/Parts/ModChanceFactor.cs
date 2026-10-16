using System;

namespace XRL.World.Parts
{
	// Token: 0x02001067 RID: 4199
	[Serializable]
	public class ModChanceFactor : IPart
	{
		// Token: 0x0600A736 RID: 42806 RVA: 0x003D1D5F File Offset: 0x003CFF5F
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<GetModRarityWeightEvent>.ID;
		}

		// Token: 0x0600A737 RID: 42807 RVA: 0x003D1D78 File Offset: 0x003CFF78
		public override bool HandleEvent(GetModRarityWeightEvent E)
		{
			if ((string.IsNullOrEmpty(this.Mod) || E.Mod.Part == this.Mod) && E.Object == this.ParentObject)
			{
				E.FactorAdjustment += (double)this.Factor;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A738 RID: 42808 RVA: 0x003D1DD3 File Offset: 0x003CFFD3
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0400450B RID: 17675
		public string Mod;

		// Token: 0x0400450C RID: 17676
		public float Factor;
	}
}
