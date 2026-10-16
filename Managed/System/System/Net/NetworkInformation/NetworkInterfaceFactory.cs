using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x0200071C RID: 1820
	internal abstract class NetworkInterfaceFactory
	{
		// Token: 0x06003A1A RID: 14874
		public abstract NetworkInterface[] GetAllNetworkInterfaces();

		// Token: 0x06003A1B RID: 14875
		public abstract int GetLoopbackInterfaceIndex();

		// Token: 0x06003A1C RID: 14876
		public abstract IPAddress GetNetMask(IPAddress address);

		// Token: 0x06003A1D RID: 14877 RVA: 0x000C99C5 File Offset: 0x000C7BC5
		public static NetworkInterfaceFactory Create()
		{
			return NetworkInterfaceFactoryPal.Create();
		}
	}
}
