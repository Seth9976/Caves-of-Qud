using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x02000711 RID: 1809
	internal static class IPGlobalPropertiesFactoryPal
	{
		// Token: 0x060039D2 RID: 14802 RVA: 0x000C8F04 File Offset: 0x000C7104
		public static IPGlobalProperties Create()
		{
			IPGlobalProperties ipglobalProperties = UnixIPGlobalPropertiesFactoryPal.Create();
			if (ipglobalProperties == null)
			{
				ipglobalProperties = Win32IPGlobalPropertiesFactoryPal.Create();
			}
			if (ipglobalProperties == null)
			{
				throw new NotImplementedException();
			}
			return ipglobalProperties;
		}
	}
}
