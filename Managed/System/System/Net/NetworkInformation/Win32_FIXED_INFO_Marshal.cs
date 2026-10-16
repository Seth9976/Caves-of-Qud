using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Net.NetworkInformation
{
	// Token: 0x02000741 RID: 1857
	internal struct Win32_FIXED_INFO_Marshal
	{
		// Token: 0x040022D9 RID: 8921
		private const int MAX_HOSTNAME_LEN = 128;

		// Token: 0x040022DA RID: 8922
		private const int MAX_DOMAIN_NAME_LEN = 128;

		// Token: 0x040022DB RID: 8923
		private const int MAX_SCOPE_ID_LEN = 256;

		// Token: 0x040022DC RID: 8924
		[FixedBuffer(typeof(byte), 132)]
		public Win32_FIXED_INFO_Marshal.<HostName>e__FixedBuffer HostName;

		// Token: 0x040022DD RID: 8925
		[FixedBuffer(typeof(byte), 132)]
		public Win32_FIXED_INFO_Marshal.<DomainName>e__FixedBuffer DomainName;

		// Token: 0x040022DE RID: 8926
		public IntPtr CurrentDnsServer;

		// Token: 0x040022DF RID: 8927
		public Win32_IP_ADDR_STRING DnsServerList;

		// Token: 0x040022E0 RID: 8928
		public NetBiosNodeType NodeType;

		// Token: 0x040022E1 RID: 8929
		[FixedBuffer(typeof(byte), 260)]
		public Win32_FIXED_INFO_Marshal.<ScopeId>e__FixedBuffer ScopeId;

		// Token: 0x040022E2 RID: 8930
		public uint EnableRouting;

		// Token: 0x040022E3 RID: 8931
		public uint EnableProxy;

		// Token: 0x040022E4 RID: 8932
		public uint EnableDns;

		// Token: 0x02000742 RID: 1858
		[UnsafeValueType]
		[CompilerGenerated]
		[StructLayout(LayoutKind.Sequential, Size = 132)]
		public struct <HostName>e__FixedBuffer
		{
			// Token: 0x040022E5 RID: 8933
			public byte FixedElementField;
		}

		// Token: 0x02000743 RID: 1859
		[UnsafeValueType]
		[CompilerGenerated]
		[StructLayout(LayoutKind.Sequential, Size = 132)]
		public struct <DomainName>e__FixedBuffer
		{
			// Token: 0x040022E6 RID: 8934
			public byte FixedElementField;
		}

		// Token: 0x02000744 RID: 1860
		[CompilerGenerated]
		[UnsafeValueType]
		[StructLayout(LayoutKind.Sequential, Size = 260)]
		public struct <ScopeId>e__FixedBuffer
		{
			// Token: 0x040022E7 RID: 8935
			public byte FixedElementField;
		}
	}
}
