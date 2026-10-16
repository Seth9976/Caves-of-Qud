using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x0200074A RID: 1866
	internal struct Win32LengthFlagsUnion
	{
		// Token: 0x17000D43 RID: 3395
		// (get) Token: 0x06003B2E RID: 15150 RVA: 0x000CBE79 File Offset: 0x000CA079
		public bool IsDnsEligible
		{
			get
			{
				return (this.Flags & 1U) > 0U;
			}
		}

		// Token: 0x17000D44 RID: 3396
		// (get) Token: 0x06003B2F RID: 15151 RVA: 0x000CBE86 File Offset: 0x000CA086
		public bool IsTransient
		{
			get
			{
				return (this.Flags & 2U) > 0U;
			}
		}

		// Token: 0x0400234B RID: 9035
		private const int IP_ADAPTER_ADDRESS_DNS_ELIGIBLE = 1;

		// Token: 0x0400234C RID: 9036
		private const int IP_ADAPTER_ADDRESS_TRANSIENT = 2;

		// Token: 0x0400234D RID: 9037
		public uint Length;

		// Token: 0x0400234E RID: 9038
		public uint Flags;
	}
}
