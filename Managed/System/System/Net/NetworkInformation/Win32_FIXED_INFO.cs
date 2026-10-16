using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x02000740 RID: 1856
	internal struct Win32_FIXED_INFO
	{
		// Token: 0x040022D0 RID: 8912
		public string HostName;

		// Token: 0x040022D1 RID: 8913
		public string DomainName;

		// Token: 0x040022D2 RID: 8914
		public IntPtr CurrentDnsServer;

		// Token: 0x040022D3 RID: 8915
		public Win32_IP_ADDR_STRING DnsServerList;

		// Token: 0x040022D4 RID: 8916
		public NetBiosNodeType NodeType;

		// Token: 0x040022D5 RID: 8917
		public string ScopeId;

		// Token: 0x040022D6 RID: 8918
		public uint EnableRouting;

		// Token: 0x040022D7 RID: 8919
		public uint EnableProxy;

		// Token: 0x040022D8 RID: 8920
		public uint EnableDns;
	}
}
