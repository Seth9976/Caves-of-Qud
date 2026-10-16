using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x02000704 RID: 1796
	internal class SystemGatewayIPAddressInformation : GatewayIPAddressInformation
	{
		// Token: 0x06003989 RID: 14729 RVA: 0x000C8D10 File Offset: 0x000C6F10
		internal SystemGatewayIPAddressInformation(IPAddress address)
		{
			this.address = address;
		}

		// Token: 0x17000C7B RID: 3195
		// (get) Token: 0x0600398A RID: 14730 RVA: 0x000C8D1F File Offset: 0x000C6F1F
		public override IPAddress Address
		{
			get
			{
				return this.address;
			}
		}

		// Token: 0x0600398B RID: 14731 RVA: 0x000C8D28 File Offset: 0x000C6F28
		internal static GatewayIPAddressInformationCollection ToGatewayIpAddressInformationCollection(IPAddressCollection addresses)
		{
			GatewayIPAddressInformationCollection gatewayIPAddressInformationCollection = new GatewayIPAddressInformationCollection();
			foreach (IPAddress ipaddress in addresses)
			{
				gatewayIPAddressInformationCollection.InternalAdd(new SystemGatewayIPAddressInformation(ipaddress));
			}
			return gatewayIPAddressInformationCollection;
		}

		// Token: 0x040021E8 RID: 8680
		private IPAddress address;
	}
}
