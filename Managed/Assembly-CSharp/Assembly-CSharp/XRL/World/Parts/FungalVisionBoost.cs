using System;

namespace XRL.World.Parts
{
	// Token: 0x020011EE RID: 4590
	[Serializable]
	public class FungalVisionBoost : IPart
	{
		// Token: 0x0600B6C6 RID: 46790 RVA: 0x0042A586 File Offset: 0x00428786
		public override bool SameAs(IPart p)
		{
			return (p as FungalVisionBoost).bBoosted == this.bBoosted && base.SameAs(p);
		}

		// Token: 0x0600B6C7 RID: 46791 RVA: 0x0042A5A4 File Offset: 0x004287A4
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x040049B4 RID: 18868
		public bool bBoosted;
	}
}
