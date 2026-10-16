using System;
using System.Runtime.InteropServices;

namespace System.Net.NetworkInformation
{
	// Token: 0x02000731 RID: 1841
	internal sealed class Win32IPv4InterfaceProperties : IPv4InterfaceProperties
	{
		// Token: 0x06003AB5 RID: 15029
		[DllImport("iphlpapi.dll")]
		private static extern int GetPerAdapterInfo(int IfIndex, Win32_IP_PER_ADAPTER_INFO pPerAdapterInfo, ref int pOutBufLen);

		// Token: 0x06003AB6 RID: 15030 RVA: 0x000CB3F4 File Offset: 0x000C95F4
		public Win32IPv4InterfaceProperties(Win32_IP_ADAPTER_ADDRESSES addr, Win32_MIB_IFROW mib)
		{
			this.addr = addr;
			this.mib = mib;
			int num = 0;
			Win32IPv4InterfaceProperties.GetPerAdapterInfo(mib.Index, null, ref num);
			this.painfo = new Win32_IP_PER_ADAPTER_INFO();
			int perAdapterInfo = Win32IPv4InterfaceProperties.GetPerAdapterInfo(mib.Index, this.painfo, ref num);
			if (perAdapterInfo != 0)
			{
				throw new NetworkInformationException(perAdapterInfo);
			}
		}

		// Token: 0x17000CE7 RID: 3303
		// (get) Token: 0x06003AB7 RID: 15031 RVA: 0x000CB44F File Offset: 0x000C964F
		public override int Index
		{
			get
			{
				return this.mib.Index;
			}
		}

		// Token: 0x17000CE8 RID: 3304
		// (get) Token: 0x06003AB8 RID: 15032 RVA: 0x000CB45C File Offset: 0x000C965C
		public override bool IsAutomaticPrivateAddressingActive
		{
			get
			{
				return this.painfo.AutoconfigActive > 0U;
			}
		}

		// Token: 0x17000CE9 RID: 3305
		// (get) Token: 0x06003AB9 RID: 15033 RVA: 0x000CB46C File Offset: 0x000C966C
		public override bool IsAutomaticPrivateAddressingEnabled
		{
			get
			{
				return this.painfo.AutoconfigEnabled > 0U;
			}
		}

		// Token: 0x17000CEA RID: 3306
		// (get) Token: 0x06003ABA RID: 15034 RVA: 0x000CB47C File Offset: 0x000C967C
		public override bool IsDhcpEnabled
		{
			get
			{
				return this.addr.DhcpEnabled;
			}
		}

		// Token: 0x17000CEB RID: 3307
		// (get) Token: 0x06003ABB RID: 15035 RVA: 0x000CB489 File Offset: 0x000C9689
		public override bool IsForwardingEnabled
		{
			get
			{
				return Win32NetworkInterface.FixedInfo.EnableRouting > 0U;
			}
		}

		// Token: 0x17000CEC RID: 3308
		// (get) Token: 0x06003ABC RID: 15036 RVA: 0x000CB498 File Offset: 0x000C9698
		public override int Mtu
		{
			get
			{
				return this.mib.Mtu;
			}
		}

		// Token: 0x17000CED RID: 3309
		// (get) Token: 0x06003ABD RID: 15037 RVA: 0x000CB4A5 File Offset: 0x000C96A5
		public override bool UsesWins
		{
			get
			{
				return this.addr.FirstWinsServerAddress != IntPtr.Zero;
			}
		}

		// Token: 0x04002298 RID: 8856
		private Win32_IP_ADAPTER_ADDRESSES addr;

		// Token: 0x04002299 RID: 8857
		private Win32_IP_PER_ADAPTER_INFO painfo;

		// Token: 0x0400229A RID: 8858
		private Win32_MIB_IFROW mib;
	}
}
