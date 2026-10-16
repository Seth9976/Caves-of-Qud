using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x0200071B RID: 1819
	internal static class SystemNetworkInterface
	{
		// Token: 0x06003A14 RID: 14868 RVA: 0x000C9968 File Offset: 0x000C7B68
		public static NetworkInterface[] GetNetworkInterfaces()
		{
			NetworkInterface[] array;
			try
			{
				array = SystemNetworkInterface.nif.GetAllNetworkInterfaces();
			}
			catch
			{
				array = new NetworkInterface[0];
			}
			return array;
		}

		// Token: 0x06003A15 RID: 14869 RVA: 0x0000390E File Offset: 0x00001B0E
		public static bool InternalGetIsNetworkAvailable()
		{
			return true;
		}

		// Token: 0x17000CB1 RID: 3249
		// (get) Token: 0x06003A16 RID: 14870 RVA: 0x000C99A0 File Offset: 0x000C7BA0
		public static int InternalLoopbackInterfaceIndex
		{
			get
			{
				return SystemNetworkInterface.nif.GetLoopbackInterfaceIndex();
			}
		}

		// Token: 0x17000CB2 RID: 3250
		// (get) Token: 0x06003A17 RID: 14871 RVA: 0x0000829A File Offset: 0x0000649A
		public static int InternalIPv6LoopbackInterfaceIndex
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x06003A18 RID: 14872 RVA: 0x000C99AC File Offset: 0x000C7BAC
		public static IPAddress GetNetMask(IPAddress address)
		{
			return SystemNetworkInterface.nif.GetNetMask(address);
		}

		// Token: 0x0400224A RID: 8778
		private static readonly NetworkInterfaceFactory nif = NetworkInterfaceFactory.Create();
	}
}
