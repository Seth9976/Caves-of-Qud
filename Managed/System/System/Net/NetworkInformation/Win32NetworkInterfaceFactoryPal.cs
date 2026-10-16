using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x0200073E RID: 1854
	internal static class Win32NetworkInterfaceFactoryPal
	{
		// Token: 0x06003B24 RID: 15140 RVA: 0x000CBCD0 File Offset: 0x000C9ED0
		public static NetworkInterfaceFactory Create()
		{
			Version version = new Version(5, 1);
			if (Environment.OSVersion.Version >= version)
			{
				return new Win32NetworkInterfaceAPI();
			}
			return null;
		}
	}
}
