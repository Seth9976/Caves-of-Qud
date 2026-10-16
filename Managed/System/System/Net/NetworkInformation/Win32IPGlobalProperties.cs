using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Runtime.InteropServices;

namespace System.Net.NetworkInformation
{
	// Token: 0x02000727 RID: 1831
	internal class Win32IPGlobalProperties : IPGlobalProperties
	{
		// Token: 0x06003A64 RID: 14948 RVA: 0x000CA924 File Offset: 0x000C8B24
		private unsafe void FillTcpTable(out List<Win32IPGlobalProperties.Win32_MIB_TCPROW> tab4, out List<Win32IPGlobalProperties.Win32_MIB_TCP6ROW> tab6)
		{
			tab4 = new List<Win32IPGlobalProperties.Win32_MIB_TCPROW>();
			int num = 0;
			Win32IPGlobalProperties.GetTcpTable(null, ref num, true);
			byte[] array = new byte[num];
			Win32IPGlobalProperties.GetTcpTable(array, ref num, true);
			int num2 = Marshal.SizeOf(typeof(Win32IPGlobalProperties.Win32_MIB_TCPROW));
			fixed (byte[] array2 = array)
			{
				byte* ptr;
				if (array == null || array2.Length == 0)
				{
					ptr = null;
				}
				else
				{
					ptr = &array2[0];
				}
				int num3 = Marshal.ReadInt32((IntPtr)((void*)ptr));
				for (int i = 0; i < num3; i++)
				{
					Win32IPGlobalProperties.Win32_MIB_TCPROW win32_MIB_TCPROW = new Win32IPGlobalProperties.Win32_MIB_TCPROW();
					Marshal.PtrToStructure<Win32IPGlobalProperties.Win32_MIB_TCPROW>((IntPtr)((void*)(ptr + i * num2 + 4)), win32_MIB_TCPROW);
					tab4.Add(win32_MIB_TCPROW);
				}
			}
			tab6 = new List<Win32IPGlobalProperties.Win32_MIB_TCP6ROW>();
			if (Environment.OSVersion.Version.Major >= 6)
			{
				int num4 = 0;
				Win32IPGlobalProperties.GetTcp6Table(null, ref num4, true);
				byte[] array3 = new byte[num4];
				Win32IPGlobalProperties.GetTcp6Table(array3, ref num4, true);
				int num5 = Marshal.SizeOf(typeof(Win32IPGlobalProperties.Win32_MIB_TCP6ROW));
				fixed (byte[] array2 = array3)
				{
					byte* ptr2;
					if (array3 == null || array2.Length == 0)
					{
						ptr2 = null;
					}
					else
					{
						ptr2 = &array2[0];
					}
					int num6 = Marshal.ReadInt32((IntPtr)((void*)ptr2));
					for (int j = 0; j < num6; j++)
					{
						Win32IPGlobalProperties.Win32_MIB_TCP6ROW win32_MIB_TCP6ROW = new Win32IPGlobalProperties.Win32_MIB_TCP6ROW();
						Marshal.PtrToStructure<Win32IPGlobalProperties.Win32_MIB_TCP6ROW>((IntPtr)((void*)(ptr2 + j * num5 + 4)), win32_MIB_TCP6ROW);
						tab6.Add(win32_MIB_TCP6ROW);
					}
				}
			}
		}

		// Token: 0x06003A65 RID: 14949 RVA: 0x000CAA6A File Offset: 0x000C8C6A
		private bool IsListenerState(TcpState state)
		{
			return state - TcpState.Listen <= 1 || state - TcpState.FinWait1 <= 2;
		}

		// Token: 0x06003A66 RID: 14950 RVA: 0x000CAA7C File Offset: 0x000C8C7C
		public override TcpConnectionInformation[] GetActiveTcpConnections()
		{
			List<Win32IPGlobalProperties.Win32_MIB_TCPROW> list = null;
			List<Win32IPGlobalProperties.Win32_MIB_TCP6ROW> list2 = null;
			this.FillTcpTable(out list, out list2);
			int count = list.Count;
			TcpConnectionInformation[] array = new TcpConnectionInformation[count + list2.Count];
			for (int i = 0; i < count; i++)
			{
				array[i] = list[i].TcpInfo;
			}
			for (int j = 0; j < list2.Count; j++)
			{
				array[count + j] = list2[j].TcpInfo;
			}
			return array;
		}

		// Token: 0x06003A67 RID: 14951 RVA: 0x000CAAF8 File Offset: 0x000C8CF8
		public override IPEndPoint[] GetActiveTcpListeners()
		{
			List<Win32IPGlobalProperties.Win32_MIB_TCPROW> list = null;
			List<Win32IPGlobalProperties.Win32_MIB_TCP6ROW> list2 = null;
			this.FillTcpTable(out list, out list2);
			List<IPEndPoint> list3 = new List<IPEndPoint>();
			int i = 0;
			int count = list.Count;
			while (i < count)
			{
				if (this.IsListenerState(list[i].State))
				{
					list3.Add(list[i].LocalEndPoint);
				}
				i++;
			}
			int j = 0;
			int count2 = list2.Count;
			while (j < count2)
			{
				if (this.IsListenerState(list2[j].State))
				{
					list3.Add(list2[j].LocalEndPoint);
				}
				j++;
			}
			return list3.ToArray();
		}

		// Token: 0x06003A68 RID: 14952 RVA: 0x000CAB9C File Offset: 0x000C8D9C
		public unsafe override IPEndPoint[] GetActiveUdpListeners()
		{
			List<IPEndPoint> list = new List<IPEndPoint>();
			int num = 0;
			Win32IPGlobalProperties.GetUdpTable(null, ref num, true);
			byte[] array = new byte[num];
			Win32IPGlobalProperties.GetUdpTable(array, ref num, true);
			int num2 = Marshal.SizeOf(typeof(Win32IPGlobalProperties.Win32_MIB_UDPROW));
			fixed (byte[] array2 = array)
			{
				byte* ptr;
				if (array == null || array2.Length == 0)
				{
					ptr = null;
				}
				else
				{
					ptr = &array2[0];
				}
				int num3 = Marshal.ReadInt32((IntPtr)((void*)ptr));
				for (int i = 0; i < num3; i++)
				{
					Win32IPGlobalProperties.Win32_MIB_UDPROW win32_MIB_UDPROW = new Win32IPGlobalProperties.Win32_MIB_UDPROW();
					Marshal.PtrToStructure<Win32IPGlobalProperties.Win32_MIB_UDPROW>((IntPtr)((void*)(ptr + i * num2 + 4)), win32_MIB_UDPROW);
					list.Add(win32_MIB_UDPROW.LocalEndPoint);
				}
			}
			if (Environment.OSVersion.Version.Major >= 6)
			{
				int num4 = 0;
				Win32IPGlobalProperties.GetUdp6Table(null, ref num4, true);
				byte[] array3 = new byte[num4];
				Win32IPGlobalProperties.GetUdp6Table(array3, ref num4, true);
				int num5 = Marshal.SizeOf(typeof(Win32IPGlobalProperties.Win32_MIB_UDP6ROW));
				fixed (byte[] array2 = array3)
				{
					byte* ptr2;
					if (array3 == null || array2.Length == 0)
					{
						ptr2 = null;
					}
					else
					{
						ptr2 = &array2[0];
					}
					int num6 = Marshal.ReadInt32((IntPtr)((void*)ptr2));
					for (int j = 0; j < num6; j++)
					{
						Win32IPGlobalProperties.Win32_MIB_UDP6ROW win32_MIB_UDP6ROW = new Win32IPGlobalProperties.Win32_MIB_UDP6ROW();
						Marshal.PtrToStructure<Win32IPGlobalProperties.Win32_MIB_UDP6ROW>((IntPtr)((void*)(ptr2 + j * num5 + 4)), win32_MIB_UDP6ROW);
						list.Add(win32_MIB_UDP6ROW.LocalEndPoint);
					}
				}
			}
			return list.ToArray();
		}

		// Token: 0x06003A69 RID: 14953 RVA: 0x000CACF0 File Offset: 0x000C8EF0
		public override IcmpV4Statistics GetIcmpV4Statistics()
		{
			if (!Socket.OSSupportsIPv4)
			{
				throw new NetworkInformationException();
			}
			Win32_MIBICMPINFO win32_MIBICMPINFO;
			Win32IPGlobalProperties.GetIcmpStatistics(out win32_MIBICMPINFO, 2);
			return new Win32IcmpV4Statistics(win32_MIBICMPINFO);
		}

		// Token: 0x06003A6A RID: 14954 RVA: 0x000CAD1C File Offset: 0x000C8F1C
		public override IcmpV6Statistics GetIcmpV6Statistics()
		{
			if (!Socket.OSSupportsIPv6)
			{
				throw new NetworkInformationException();
			}
			Win32_MIB_ICMP_EX win32_MIB_ICMP_EX;
			Win32IPGlobalProperties.GetIcmpStatisticsEx(out win32_MIB_ICMP_EX, 23);
			return new Win32IcmpV6Statistics(win32_MIB_ICMP_EX);
		}

		// Token: 0x06003A6B RID: 14955 RVA: 0x000CAD48 File Offset: 0x000C8F48
		public override IPGlobalStatistics GetIPv4GlobalStatistics()
		{
			if (!Socket.OSSupportsIPv4)
			{
				throw new NetworkInformationException();
			}
			Win32_MIB_IPSTATS win32_MIB_IPSTATS;
			Win32IPGlobalProperties.GetIpStatisticsEx(out win32_MIB_IPSTATS, 2);
			return new Win32IPGlobalStatistics(win32_MIB_IPSTATS);
		}

		// Token: 0x06003A6C RID: 14956 RVA: 0x000CAD74 File Offset: 0x000C8F74
		public override IPGlobalStatistics GetIPv6GlobalStatistics()
		{
			if (!Socket.OSSupportsIPv6)
			{
				throw new NetworkInformationException();
			}
			Win32_MIB_IPSTATS win32_MIB_IPSTATS;
			Win32IPGlobalProperties.GetIpStatisticsEx(out win32_MIB_IPSTATS, 23);
			return new Win32IPGlobalStatistics(win32_MIB_IPSTATS);
		}

		// Token: 0x06003A6D RID: 14957 RVA: 0x000CADA0 File Offset: 0x000C8FA0
		public override TcpStatistics GetTcpIPv4Statistics()
		{
			if (!Socket.OSSupportsIPv4)
			{
				throw new NetworkInformationException();
			}
			Win32_MIB_TCPSTATS win32_MIB_TCPSTATS;
			Win32IPGlobalProperties.GetTcpStatisticsEx(out win32_MIB_TCPSTATS, 2);
			return new Win32TcpStatistics(win32_MIB_TCPSTATS);
		}

		// Token: 0x06003A6E RID: 14958 RVA: 0x000CADCC File Offset: 0x000C8FCC
		public override TcpStatistics GetTcpIPv6Statistics()
		{
			if (!Socket.OSSupportsIPv6)
			{
				throw new NetworkInformationException();
			}
			Win32_MIB_TCPSTATS win32_MIB_TCPSTATS;
			Win32IPGlobalProperties.GetTcpStatisticsEx(out win32_MIB_TCPSTATS, 23);
			return new Win32TcpStatistics(win32_MIB_TCPSTATS);
		}

		// Token: 0x06003A6F RID: 14959 RVA: 0x000CADF8 File Offset: 0x000C8FF8
		public override UdpStatistics GetUdpIPv4Statistics()
		{
			if (!Socket.OSSupportsIPv4)
			{
				throw new NetworkInformationException();
			}
			Win32_MIB_UDPSTATS win32_MIB_UDPSTATS;
			Win32IPGlobalProperties.GetUdpStatisticsEx(out win32_MIB_UDPSTATS, 2);
			return new Win32UdpStatistics(win32_MIB_UDPSTATS);
		}

		// Token: 0x06003A70 RID: 14960 RVA: 0x000CAE24 File Offset: 0x000C9024
		public override UdpStatistics GetUdpIPv6Statistics()
		{
			if (!Socket.OSSupportsIPv6)
			{
				throw new NetworkInformationException();
			}
			Win32_MIB_UDPSTATS win32_MIB_UDPSTATS;
			Win32IPGlobalProperties.GetUdpStatisticsEx(out win32_MIB_UDPSTATS, 23);
			return new Win32UdpStatistics(win32_MIB_UDPSTATS);
		}

		// Token: 0x17000CBA RID: 3258
		// (get) Token: 0x06003A71 RID: 14961 RVA: 0x000CAE4E File Offset: 0x000C904E
		public override string DhcpScopeName
		{
			get
			{
				return Win32NetworkInterface.FixedInfo.ScopeId;
			}
		}

		// Token: 0x17000CBB RID: 3259
		// (get) Token: 0x06003A72 RID: 14962 RVA: 0x000CAE5A File Offset: 0x000C905A
		public override string DomainName
		{
			get
			{
				return Win32NetworkInterface.FixedInfo.DomainName;
			}
		}

		// Token: 0x17000CBC RID: 3260
		// (get) Token: 0x06003A73 RID: 14963 RVA: 0x000CAE66 File Offset: 0x000C9066
		public override string HostName
		{
			get
			{
				return Win32NetworkInterface.FixedInfo.HostName;
			}
		}

		// Token: 0x17000CBD RID: 3261
		// (get) Token: 0x06003A74 RID: 14964 RVA: 0x000CAE72 File Offset: 0x000C9072
		public override bool IsWinsProxy
		{
			get
			{
				return Win32NetworkInterface.FixedInfo.EnableProxy > 0U;
			}
		}

		// Token: 0x17000CBE RID: 3262
		// (get) Token: 0x06003A75 RID: 14965 RVA: 0x000CAE81 File Offset: 0x000C9081
		public override NetBiosNodeType NodeType
		{
			get
			{
				return Win32NetworkInterface.FixedInfo.NodeType;
			}
		}

		// Token: 0x06003A76 RID: 14966
		[DllImport("iphlpapi.dll")]
		private static extern int GetTcpTable(byte[] pTcpTable, ref int pdwSize, bool bOrder);

		// Token: 0x06003A77 RID: 14967
		[DllImport("iphlpapi.dll")]
		private static extern int GetTcp6Table(byte[] TcpTable, ref int SizePointer, bool Order);

		// Token: 0x06003A78 RID: 14968
		[DllImport("iphlpapi.dll")]
		private static extern int GetUdpTable(byte[] pUdpTable, ref int pdwSize, bool bOrder);

		// Token: 0x06003A79 RID: 14969
		[DllImport("iphlpapi.dll")]
		private static extern int GetUdp6Table(byte[] Udp6Table, ref int SizePointer, bool Order);

		// Token: 0x06003A7A RID: 14970
		[DllImport("iphlpapi.dll")]
		private static extern int GetTcpStatisticsEx(out Win32_MIB_TCPSTATS pStats, int dwFamily);

		// Token: 0x06003A7B RID: 14971
		[DllImport("iphlpapi.dll")]
		private static extern int GetUdpStatisticsEx(out Win32_MIB_UDPSTATS pStats, int dwFamily);

		// Token: 0x06003A7C RID: 14972
		[DllImport("iphlpapi.dll")]
		private static extern int GetIcmpStatistics(out Win32_MIBICMPINFO pStats, int dwFamily);

		// Token: 0x06003A7D RID: 14973
		[DllImport("iphlpapi.dll")]
		private static extern int GetIcmpStatisticsEx(out Win32_MIB_ICMP_EX pStats, int dwFamily);

		// Token: 0x06003A7E RID: 14974
		[DllImport("iphlpapi.dll")]
		private static extern int GetIpStatisticsEx(out Win32_MIB_IPSTATS pStats, int dwFamily);

		// Token: 0x06003A7F RID: 14975
		[DllImport("Ws2_32.dll")]
		private static extern ushort ntohs(ushort netshort);

		// Token: 0x04002269 RID: 8809
		public const int AF_INET = 2;

		// Token: 0x0400226A RID: 8810
		public const int AF_INET6 = 23;

		// Token: 0x02000728 RID: 1832
		[StructLayout(LayoutKind.Explicit)]
		private struct Win32_IN6_ADDR
		{
			// Token: 0x0400226B RID: 8811
			[FieldOffset(0)]
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
			public byte[] Bytes;
		}

		// Token: 0x02000729 RID: 1833
		[StructLayout(LayoutKind.Sequential)]
		private class Win32_MIB_TCPROW
		{
			// Token: 0x17000CBF RID: 3263
			// (get) Token: 0x06003A81 RID: 14977 RVA: 0x000CAE95 File Offset: 0x000C9095
			public IPEndPoint LocalEndPoint
			{
				get
				{
					return new IPEndPoint((long)((ulong)this.LocalAddr), (int)Win32IPGlobalProperties.ntohs((ushort)this.LocalPort));
				}
			}

			// Token: 0x17000CC0 RID: 3264
			// (get) Token: 0x06003A82 RID: 14978 RVA: 0x000CAEAF File Offset: 0x000C90AF
			public IPEndPoint RemoteEndPoint
			{
				get
				{
					return new IPEndPoint((long)((ulong)this.RemoteAddr), (int)Win32IPGlobalProperties.ntohs((ushort)this.RemotePort));
				}
			}

			// Token: 0x17000CC1 RID: 3265
			// (get) Token: 0x06003A83 RID: 14979 RVA: 0x000CAEC9 File Offset: 0x000C90C9
			public TcpConnectionInformation TcpInfo
			{
				get
				{
					return new SystemTcpConnectionInformation(this.LocalEndPoint, this.RemoteEndPoint, this.State);
				}
			}

			// Token: 0x0400226C RID: 8812
			public TcpState State;

			// Token: 0x0400226D RID: 8813
			public uint LocalAddr;

			// Token: 0x0400226E RID: 8814
			public uint LocalPort;

			// Token: 0x0400226F RID: 8815
			public uint RemoteAddr;

			// Token: 0x04002270 RID: 8816
			public uint RemotePort;
		}

		// Token: 0x0200072A RID: 1834
		[StructLayout(LayoutKind.Sequential)]
		private class Win32_MIB_TCP6ROW
		{
			// Token: 0x17000CC2 RID: 3266
			// (get) Token: 0x06003A85 RID: 14981 RVA: 0x000CAEE2 File Offset: 0x000C90E2
			public IPEndPoint LocalEndPoint
			{
				get
				{
					return new IPEndPoint(new IPAddress(this.LocalAddr.Bytes, (long)((ulong)this.LocalScopeId)), (int)Win32IPGlobalProperties.ntohs((ushort)this.LocalPort));
				}
			}

			// Token: 0x17000CC3 RID: 3267
			// (get) Token: 0x06003A86 RID: 14982 RVA: 0x000CAF0C File Offset: 0x000C910C
			public IPEndPoint RemoteEndPoint
			{
				get
				{
					return new IPEndPoint(new IPAddress(this.RemoteAddr.Bytes, (long)((ulong)this.RemoteScopeId)), (int)Win32IPGlobalProperties.ntohs((ushort)this.RemotePort));
				}
			}

			// Token: 0x17000CC4 RID: 3268
			// (get) Token: 0x06003A87 RID: 14983 RVA: 0x000CAF36 File Offset: 0x000C9136
			public TcpConnectionInformation TcpInfo
			{
				get
				{
					return new SystemTcpConnectionInformation(this.LocalEndPoint, this.RemoteEndPoint, this.State);
				}
			}

			// Token: 0x04002271 RID: 8817
			public TcpState State;

			// Token: 0x04002272 RID: 8818
			public Win32IPGlobalProperties.Win32_IN6_ADDR LocalAddr;

			// Token: 0x04002273 RID: 8819
			public uint LocalScopeId;

			// Token: 0x04002274 RID: 8820
			public uint LocalPort;

			// Token: 0x04002275 RID: 8821
			public Win32IPGlobalProperties.Win32_IN6_ADDR RemoteAddr;

			// Token: 0x04002276 RID: 8822
			public uint RemoteScopeId;

			// Token: 0x04002277 RID: 8823
			public uint RemotePort;
		}

		// Token: 0x0200072B RID: 1835
		[StructLayout(LayoutKind.Sequential)]
		private class Win32_MIB_UDPROW
		{
			// Token: 0x17000CC5 RID: 3269
			// (get) Token: 0x06003A89 RID: 14985 RVA: 0x000CAF4F File Offset: 0x000C914F
			public IPEndPoint LocalEndPoint
			{
				get
				{
					return new IPEndPoint((long)((ulong)this.LocalAddr), (int)Win32IPGlobalProperties.ntohs((ushort)this.LocalPort));
				}
			}

			// Token: 0x04002278 RID: 8824
			public uint LocalAddr;

			// Token: 0x04002279 RID: 8825
			public uint LocalPort;
		}

		// Token: 0x0200072C RID: 1836
		[StructLayout(LayoutKind.Sequential)]
		private class Win32_MIB_UDP6ROW
		{
			// Token: 0x17000CC6 RID: 3270
			// (get) Token: 0x06003A8B RID: 14987 RVA: 0x000CAF69 File Offset: 0x000C9169
			public IPEndPoint LocalEndPoint
			{
				get
				{
					return new IPEndPoint(new IPAddress(this.LocalAddr.Bytes, (long)((ulong)this.LocalScopeId)), (int)Win32IPGlobalProperties.ntohs((ushort)this.LocalPort));
				}
			}

			// Token: 0x0400227A RID: 8826
			public Win32IPGlobalProperties.Win32_IN6_ADDR LocalAddr;

			// Token: 0x0400227B RID: 8827
			public uint LocalScopeId;

			// Token: 0x0400227C RID: 8828
			public uint LocalPort;
		}
	}
}
