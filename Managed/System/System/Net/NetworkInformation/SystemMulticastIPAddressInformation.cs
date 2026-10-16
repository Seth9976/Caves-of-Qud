using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x02000706 RID: 1798
	internal class SystemMulticastIPAddressInformation : MulticastIPAddressInformation
	{
		// Token: 0x06003990 RID: 14736 RVA: 0x000C8DB8 File Offset: 0x000C6FB8
		private SystemMulticastIPAddressInformation()
		{
		}

		// Token: 0x06003991 RID: 14737 RVA: 0x000C8DC0 File Offset: 0x000C6FC0
		public SystemMulticastIPAddressInformation(SystemIPAddressInformation addressInfo)
		{
			this.innerInfo = addressInfo;
		}

		// Token: 0x17000C7F RID: 3199
		// (get) Token: 0x06003992 RID: 14738 RVA: 0x000C8DCF File Offset: 0x000C6FCF
		public override IPAddress Address
		{
			get
			{
				return this.innerInfo.Address;
			}
		}

		// Token: 0x17000C80 RID: 3200
		// (get) Token: 0x06003993 RID: 14739 RVA: 0x000C8DDC File Offset: 0x000C6FDC
		public override bool IsTransient
		{
			get
			{
				return this.innerInfo.IsTransient;
			}
		}

		// Token: 0x17000C81 RID: 3201
		// (get) Token: 0x06003994 RID: 14740 RVA: 0x000C8DE9 File Offset: 0x000C6FE9
		public override bool IsDnsEligible
		{
			get
			{
				return this.innerInfo.IsDnsEligible;
			}
		}

		// Token: 0x17000C82 RID: 3202
		// (get) Token: 0x06003995 RID: 14741 RVA: 0x00003062 File Offset: 0x00001262
		public override PrefixOrigin PrefixOrigin
		{
			get
			{
				return PrefixOrigin.Other;
			}
		}

		// Token: 0x17000C83 RID: 3203
		// (get) Token: 0x06003996 RID: 14742 RVA: 0x00003062 File Offset: 0x00001262
		public override SuffixOrigin SuffixOrigin
		{
			get
			{
				return SuffixOrigin.Other;
			}
		}

		// Token: 0x17000C84 RID: 3204
		// (get) Token: 0x06003997 RID: 14743 RVA: 0x00003062 File Offset: 0x00001262
		public override DuplicateAddressDetectionState DuplicateAddressDetectionState
		{
			get
			{
				return DuplicateAddressDetectionState.Invalid;
			}
		}

		// Token: 0x17000C85 RID: 3205
		// (get) Token: 0x06003998 RID: 14744 RVA: 0x0004D084 File Offset: 0x0004B284
		public override long AddressValidLifetime
		{
			get
			{
				return 0L;
			}
		}

		// Token: 0x17000C86 RID: 3206
		// (get) Token: 0x06003999 RID: 14745 RVA: 0x0004D084 File Offset: 0x0004B284
		public override long AddressPreferredLifetime
		{
			get
			{
				return 0L;
			}
		}

		// Token: 0x17000C87 RID: 3207
		// (get) Token: 0x0600399A RID: 14746 RVA: 0x0004D084 File Offset: 0x0004B284
		public override long DhcpLeaseLifetime
		{
			get
			{
				return 0L;
			}
		}

		// Token: 0x0600399B RID: 14747 RVA: 0x000C8DF8 File Offset: 0x000C6FF8
		internal static MulticastIPAddressInformationCollection ToMulticastIpAddressInformationCollection(IPAddressInformationCollection addresses)
		{
			MulticastIPAddressInformationCollection multicastIPAddressInformationCollection = new MulticastIPAddressInformationCollection();
			foreach (IPAddressInformation ipaddressInformation in addresses)
			{
				multicastIPAddressInformationCollection.InternalAdd(new SystemMulticastIPAddressInformation((SystemIPAddressInformation)ipaddressInformation));
			}
			return multicastIPAddressInformationCollection;
		}

		// Token: 0x040021EC RID: 8684
		private SystemIPAddressInformation innerInfo;
	}
}
