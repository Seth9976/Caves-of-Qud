using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace System.Net.NetworkInformation
{
	// Token: 0x0200073D RID: 1853
	internal sealed class Win32NetworkInterface2 : NetworkInterface
	{
		// Token: 0x06003B14 RID: 15124
		[DllImport("iphlpapi.dll", SetLastError = true)]
		private static extern int GetAdaptersInfo(IntPtr info, ref int size);

		// Token: 0x06003B15 RID: 15125
		[DllImport("iphlpapi.dll", SetLastError = true)]
		private static extern int GetIfEntry(ref Win32_MIB_IFROW row);

		// Token: 0x06003B16 RID: 15126 RVA: 0x000CBAAC File Offset: 0x000C9CAC
		private static Win32_IP_ADAPTER_INFO[] GetAdaptersInfo()
		{
			int num = 0;
			Win32NetworkInterface2.GetAdaptersInfo(IntPtr.Zero, ref num);
			IntPtr intPtr = Marshal.AllocHGlobal(num);
			int adaptersInfo = Win32NetworkInterface2.GetAdaptersInfo(intPtr, ref num);
			if (adaptersInfo != 0)
			{
				throw new NetworkInformationException(adaptersInfo);
			}
			List<Win32_IP_ADAPTER_INFO> list = new List<Win32_IP_ADAPTER_INFO>();
			IntPtr intPtr2 = intPtr;
			while (intPtr2 != IntPtr.Zero)
			{
				Win32_IP_ADAPTER_INFO win32_IP_ADAPTER_INFO = Marshal.PtrToStructure<Win32_IP_ADAPTER_INFO>(intPtr2);
				list.Add(win32_IP_ADAPTER_INFO);
				intPtr2 = win32_IP_ADAPTER_INFO.Next;
			}
			return list.ToArray();
		}

		// Token: 0x06003B17 RID: 15127 RVA: 0x000CBB18 File Offset: 0x000C9D18
		internal Win32NetworkInterface2(Win32_IP_ADAPTER_ADDRESSES addr)
		{
			this.addr = addr;
			this.mib4 = default(Win32_MIB_IFROW);
			this.mib4.Index = addr.Alignment.IfIndex;
			if (Win32NetworkInterface2.GetIfEntry(ref this.mib4) != 0)
			{
				this.mib4.Index = -1;
			}
			this.mib6 = default(Win32_MIB_IFROW);
			this.mib6.Index = addr.Ipv6IfIndex;
			if (Win32NetworkInterface2.GetIfEntry(ref this.mib6) != 0)
			{
				this.mib6.Index = -1;
			}
			this.ip4stats = new Win32IPv4InterfaceStatistics(this.mib4);
			this.ip_if_props = new Win32IPInterfaceProperties2(addr, this.mib4, this.mib6);
		}

		// Token: 0x06003B18 RID: 15128 RVA: 0x000CBBCC File Offset: 0x000C9DCC
		public override IPInterfaceProperties GetIPProperties()
		{
			return this.ip_if_props;
		}

		// Token: 0x06003B19 RID: 15129 RVA: 0x000CBBD4 File Offset: 0x000C9DD4
		public override IPv4InterfaceStatistics GetIPv4Statistics()
		{
			return this.ip4stats;
		}

		// Token: 0x06003B1A RID: 15130 RVA: 0x000CBBDC File Offset: 0x000C9DDC
		public override PhysicalAddress GetPhysicalAddress()
		{
			byte[] array = new byte[this.addr.PhysicalAddressLength];
			Array.Copy(this.addr.PhysicalAddress, 0, array, 0, array.Length);
			return new PhysicalAddress(array);
		}

		// Token: 0x06003B1B RID: 15131 RVA: 0x000CBC16 File Offset: 0x000C9E16
		public override bool Supports(NetworkInterfaceComponent networkInterfaceComponent)
		{
			if (networkInterfaceComponent != NetworkInterfaceComponent.IPv4)
			{
				return networkInterfaceComponent == NetworkInterfaceComponent.IPv6 && this.mib6.Index >= 0;
			}
			return this.mib4.Index >= 0;
		}

		// Token: 0x17000D36 RID: 3382
		// (get) Token: 0x06003B1C RID: 15132 RVA: 0x000CBC46 File Offset: 0x000C9E46
		public override string Description
		{
			get
			{
				return this.addr.Description;
			}
		}

		// Token: 0x17000D37 RID: 3383
		// (get) Token: 0x06003B1D RID: 15133 RVA: 0x000CBC53 File Offset: 0x000C9E53
		public override string Id
		{
			get
			{
				return this.addr.AdapterName;
			}
		}

		// Token: 0x17000D38 RID: 3384
		// (get) Token: 0x06003B1E RID: 15134 RVA: 0x000CBC60 File Offset: 0x000C9E60
		public override bool IsReceiveOnly
		{
			get
			{
				return this.addr.IsReceiveOnly;
			}
		}

		// Token: 0x17000D39 RID: 3385
		// (get) Token: 0x06003B1F RID: 15135 RVA: 0x000CBC6D File Offset: 0x000C9E6D
		public override string Name
		{
			get
			{
				return this.addr.FriendlyName;
			}
		}

		// Token: 0x17000D3A RID: 3386
		// (get) Token: 0x06003B20 RID: 15136 RVA: 0x000CBC7A File Offset: 0x000C9E7A
		public override NetworkInterfaceType NetworkInterfaceType
		{
			get
			{
				return this.addr.IfType;
			}
		}

		// Token: 0x17000D3B RID: 3387
		// (get) Token: 0x06003B21 RID: 15137 RVA: 0x000CBC87 File Offset: 0x000C9E87
		public override OperationalStatus OperationalStatus
		{
			get
			{
				return this.addr.OperStatus;
			}
		}

		// Token: 0x17000D3C RID: 3388
		// (get) Token: 0x06003B22 RID: 15138 RVA: 0x000CBC94 File Offset: 0x000C9E94
		public override long Speed
		{
			get
			{
				return (long)((ulong)((this.mib6.Index >= 0) ? this.mib6.Speed : this.mib4.Speed));
			}
		}

		// Token: 0x17000D3D RID: 3389
		// (get) Token: 0x06003B23 RID: 15139 RVA: 0x000CBCBD File Offset: 0x000C9EBD
		public override bool SupportsMulticast
		{
			get
			{
				return !this.addr.NoMulticast;
			}
		}

		// Token: 0x040022C9 RID: 8905
		private Win32_IP_ADAPTER_ADDRESSES addr;

		// Token: 0x040022CA RID: 8906
		private Win32_MIB_IFROW mib4;

		// Token: 0x040022CB RID: 8907
		private Win32_MIB_IFROW mib6;

		// Token: 0x040022CC RID: 8908
		private Win32IPv4InterfaceStatistics ip4stats;

		// Token: 0x040022CD RID: 8909
		private IPInterfaceProperties ip_if_props;
	}
}
