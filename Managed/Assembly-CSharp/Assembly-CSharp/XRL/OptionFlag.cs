using System;

namespace XRL
{
	// Token: 0x020004C5 RID: 1221
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
	public class OptionFlag : Attribute
	{
		// Token: 0x060034E7 RID: 13543 RVA: 0x000FFDC1 File Offset: 0x000FDFC1
		public OptionFlag()
		{
		}

		// Token: 0x060034E8 RID: 13544 RVA: 0x000FFDC9 File Offset: 0x000FDFC9
		public OptionFlag(string ID)
		{
			this.ID = ID;
		}

		// Token: 0x04001AC2 RID: 6850
		public string ID;

		// Token: 0x04001AC3 RID: 6851
		public bool AllowMissing;
	}
}
