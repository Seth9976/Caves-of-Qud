using System;

namespace XRL.World.Parts
{
	// Token: 0x02000F9C RID: 3996
	[Serializable]
	public class CyberneticsRack : IPart
	{
		// Token: 0x06009EA3 RID: 40611 RVA: 0x00397730 File Offset: 0x00395930
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x06009EA4 RID: 40612 RVA: 0x00397733 File Offset: 0x00395933
		public override bool AllowStaticRegistration()
		{
			return true;
		}
	}
}
