using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x0200071D RID: 1821
	internal static class NetworkInterfaceFactoryPal
	{
		// Token: 0x06003A1F RID: 14879 RVA: 0x000C99CC File Offset: 0x000C7BCC
		public static NetworkInterfaceFactory Create()
		{
			NetworkInterfaceFactory networkInterfaceFactory = UnixNetworkInterfaceFactoryPal.Create();
			if (networkInterfaceFactory == null)
			{
				networkInterfaceFactory = Win32NetworkInterfaceFactoryPal.Create();
			}
			if (networkInterfaceFactory == null)
			{
				throw new NotImplementedException();
			}
			return networkInterfaceFactory;
		}
	}
}
