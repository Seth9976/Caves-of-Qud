using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x02000734 RID: 1844
	internal class Win32IPv6InterfaceProperties : IPv6InterfaceProperties
	{
		// Token: 0x06003ACC RID: 15052 RVA: 0x000CB573 File Offset: 0x000C9773
		public Win32IPv6InterfaceProperties(Win32_MIB_IFROW mib)
		{
			this.mib = mib;
		}

		// Token: 0x17000CFA RID: 3322
		// (get) Token: 0x06003ACD RID: 15053 RVA: 0x000CB582 File Offset: 0x000C9782
		public override int Index
		{
			get
			{
				return this.mib.Index;
			}
		}

		// Token: 0x17000CFB RID: 3323
		// (get) Token: 0x06003ACE RID: 15054 RVA: 0x000CB58F File Offset: 0x000C978F
		public override int Mtu
		{
			get
			{
				return this.mib.Mtu;
			}
		}

		// Token: 0x040022A0 RID: 8864
		private Win32_MIB_IFROW mib;
	}
}
