using System;
using System.Runtime.InteropServices;

namespace System.Net.NetworkInformation
{
	// Token: 0x02000746 RID: 1862
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	internal struct Win32_IP_ADAPTER_ADDRESSES
	{
		// Token: 0x17000D3F RID: 3391
		// (get) Token: 0x06003B2A RID: 15146 RVA: 0x000CBE44 File Offset: 0x000CA044
		public bool DdnsEnabled
		{
			get
			{
				return (this.Flags & 1U) > 0U;
			}
		}

		// Token: 0x17000D40 RID: 3392
		// (get) Token: 0x06003B2B RID: 15147 RVA: 0x000CBE51 File Offset: 0x000CA051
		public bool DhcpEnabled
		{
			get
			{
				return (this.Flags & 4U) > 0U;
			}
		}

		// Token: 0x17000D41 RID: 3393
		// (get) Token: 0x06003B2C RID: 15148 RVA: 0x000CBE5E File Offset: 0x000CA05E
		public bool IsReceiveOnly
		{
			get
			{
				return (this.Flags & 8U) > 0U;
			}
		}

		// Token: 0x17000D42 RID: 3394
		// (get) Token: 0x06003B2D RID: 15149 RVA: 0x000CBE6B File Offset: 0x000CA06B
		public bool NoMulticast
		{
			get
			{
				return (this.Flags & 16U) > 0U;
			}
		}

		// Token: 0x040022EB RID: 8939
		public AlignmentUnion Alignment;

		// Token: 0x040022EC RID: 8940
		public IntPtr Next;

		// Token: 0x040022ED RID: 8941
		[MarshalAs(UnmanagedType.LPStr)]
		public string AdapterName;

		// Token: 0x040022EE RID: 8942
		public IntPtr FirstUnicastAddress;

		// Token: 0x040022EF RID: 8943
		public IntPtr FirstAnycastAddress;

		// Token: 0x040022F0 RID: 8944
		public IntPtr FirstMulticastAddress;

		// Token: 0x040022F1 RID: 8945
		public IntPtr FirstDnsServerAddress;

		// Token: 0x040022F2 RID: 8946
		public string DnsSuffix;

		// Token: 0x040022F3 RID: 8947
		public string Description;

		// Token: 0x040022F4 RID: 8948
		public string FriendlyName;

		// Token: 0x040022F5 RID: 8949
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
		public byte[] PhysicalAddress;

		// Token: 0x040022F6 RID: 8950
		public uint PhysicalAddressLength;

		// Token: 0x040022F7 RID: 8951
		public uint Flags;

		// Token: 0x040022F8 RID: 8952
		public uint Mtu;

		// Token: 0x040022F9 RID: 8953
		public NetworkInterfaceType IfType;

		// Token: 0x040022FA RID: 8954
		public OperationalStatus OperStatus;

		// Token: 0x040022FB RID: 8955
		public int Ipv6IfIndex;

		// Token: 0x040022FC RID: 8956
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
		public uint[] ZoneIndices;

		// Token: 0x040022FD RID: 8957
		public IntPtr FirstPrefix;

		// Token: 0x040022FE RID: 8958
		public ulong TransmitLinkSpeed;

		// Token: 0x040022FF RID: 8959
		public ulong ReceiveLinkSpeed;

		// Token: 0x04002300 RID: 8960
		public IntPtr FirstWinsServerAddress;

		// Token: 0x04002301 RID: 8961
		public IntPtr FirstGatewayAddress;

		// Token: 0x04002302 RID: 8962
		public uint Ipv4Metric;

		// Token: 0x04002303 RID: 8963
		public uint Ipv6Metric;

		// Token: 0x04002304 RID: 8964
		public ulong Luid;

		// Token: 0x04002305 RID: 8965
		public Win32_SOCKET_ADDRESS Dhcpv4Server;

		// Token: 0x04002306 RID: 8966
		public uint CompartmentId;

		// Token: 0x04002307 RID: 8967
		public ulong NetworkGuid;

		// Token: 0x04002308 RID: 8968
		public int ConnectionType;

		// Token: 0x04002309 RID: 8969
		public int TunnelType;

		// Token: 0x0400230A RID: 8970
		public Win32_SOCKET_ADDRESS Dhcpv6Server;

		// Token: 0x0400230B RID: 8971
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 130)]
		public byte[] Dhcpv6ClientDuid;

		// Token: 0x0400230C RID: 8972
		public ulong Dhcpv6ClientDuidLength;

		// Token: 0x0400230D RID: 8973
		public ulong Dhcpv6Iaid;

		// Token: 0x0400230E RID: 8974
		public IntPtr FirstDnsSuffix;

		// Token: 0x0400230F RID: 8975
		public const int GAA_FLAG_INCLUDE_WINS_INFO = 64;

		// Token: 0x04002310 RID: 8976
		public const int GAA_FLAG_INCLUDE_GATEWAYS = 128;

		// Token: 0x04002311 RID: 8977
		private const int MAX_ADAPTER_ADDRESS_LENGTH = 8;

		// Token: 0x04002312 RID: 8978
		private const int MAX_DHCPV6_DUID_LENGTH = 130;

		// Token: 0x04002313 RID: 8979
		private const int IP_ADAPTER_DDNS_ENABLED = 1;

		// Token: 0x04002314 RID: 8980
		private const int IP_ADAPTER_DHCP_ENABLED = 4;

		// Token: 0x04002315 RID: 8981
		private const int IP_ADAPTER_RECEIVE_ONLY = 8;

		// Token: 0x04002316 RID: 8982
		private const int IP_ADAPTER_NO_MULTICAST = 16;
	}
}
