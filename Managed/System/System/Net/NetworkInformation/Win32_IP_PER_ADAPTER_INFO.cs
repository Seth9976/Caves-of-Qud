using System;
using System.Runtime.InteropServices;

namespace System.Net.NetworkInformation
{
	// Token: 0x02000732 RID: 1842
	[StructLayout(LayoutKind.Sequential)]
	internal class Win32_IP_PER_ADAPTER_INFO
	{
		// Token: 0x0400229B RID: 8859
		public uint AutoconfigEnabled;

		// Token: 0x0400229C RID: 8860
		public uint AutoconfigActive;

		// Token: 0x0400229D RID: 8861
		public IntPtr CurrentDnsServer;

		// Token: 0x0400229E RID: 8862
		public Win32_IP_ADDR_STRING DnsServerList;
	}
}
