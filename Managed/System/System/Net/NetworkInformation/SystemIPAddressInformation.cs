using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x02000705 RID: 1797
	internal class SystemIPAddressInformation : IPAddressInformation
	{
		// Token: 0x0600398C RID: 14732 RVA: 0x000C8D7C File Offset: 0x000C6F7C
		public SystemIPAddressInformation(IPAddress address, bool isDnsEligible, bool isTransient)
		{
			this.address = address;
			this.dnsEligible = isDnsEligible;
			this.transient = isTransient;
		}

		// Token: 0x17000C7C RID: 3196
		// (get) Token: 0x0600398D RID: 14733 RVA: 0x000C8DA0 File Offset: 0x000C6FA0
		public override IPAddress Address
		{
			get
			{
				return this.address;
			}
		}

		// Token: 0x17000C7D RID: 3197
		// (get) Token: 0x0600398E RID: 14734 RVA: 0x000C8DA8 File Offset: 0x000C6FA8
		public override bool IsTransient
		{
			get
			{
				return this.transient;
			}
		}

		// Token: 0x17000C7E RID: 3198
		// (get) Token: 0x0600398F RID: 14735 RVA: 0x000C8DB0 File Offset: 0x000C6FB0
		public override bool IsDnsEligible
		{
			get
			{
				return this.dnsEligible;
			}
		}

		// Token: 0x040021E9 RID: 8681
		private IPAddress address;

		// Token: 0x040021EA RID: 8682
		internal bool transient;

		// Token: 0x040021EB RID: 8683
		internal bool dnsEligible = true;
	}
}
