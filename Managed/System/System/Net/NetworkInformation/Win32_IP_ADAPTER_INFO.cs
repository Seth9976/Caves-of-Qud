using System;
using System.Runtime.InteropServices;

namespace System.Net.NetworkInformation
{
	// Token: 0x02000747 RID: 1863
	internal struct Win32_IP_ADAPTER_INFO
	{
		// Token: 0x04002317 RID: 8983
		private const int MAX_ADAPTER_NAME_LENGTH = 256;

		// Token: 0x04002318 RID: 8984
		private const int MAX_ADAPTER_DESCRIPTION_LENGTH = 128;

		// Token: 0x04002319 RID: 8985
		private const int MAX_ADAPTER_ADDRESS_LENGTH = 8;

		// Token: 0x0400231A RID: 8986
		public IntPtr Next;

		// Token: 0x0400231B RID: 8987
		public int ComboIndex;

		// Token: 0x0400231C RID: 8988
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
		public string AdapterName;

		// Token: 0x0400231D RID: 8989
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 132)]
		public string Description;

		// Token: 0x0400231E RID: 8990
		public uint AddressLength;

		// Token: 0x0400231F RID: 8991
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
		public byte[] Address;

		// Token: 0x04002320 RID: 8992
		public uint Index;

		// Token: 0x04002321 RID: 8993
		public uint Type;

		// Token: 0x04002322 RID: 8994
		public uint DhcpEnabled;

		// Token: 0x04002323 RID: 8995
		public IntPtr CurrentIpAddress;

		// Token: 0x04002324 RID: 8996
		public Win32_IP_ADDR_STRING IpAddressList;

		// Token: 0x04002325 RID: 8997
		public Win32_IP_ADDR_STRING GatewayList;

		// Token: 0x04002326 RID: 8998
		public Win32_IP_ADDR_STRING DhcpServer;

		// Token: 0x04002327 RID: 8999
		public bool HaveWins;

		// Token: 0x04002328 RID: 9000
		public Win32_IP_ADDR_STRING PrimaryWinsServer;

		// Token: 0x04002329 RID: 9001
		public Win32_IP_ADDR_STRING SecondaryWinsServer;

		// Token: 0x0400232A RID: 9002
		public long LeaseObtained;

		// Token: 0x0400232B RID: 9003
		public long LeaseExpires;
	}
}
