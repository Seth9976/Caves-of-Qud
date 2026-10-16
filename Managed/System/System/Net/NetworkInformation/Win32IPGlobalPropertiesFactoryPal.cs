using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x0200072D RID: 1837
	internal static class Win32IPGlobalPropertiesFactoryPal
	{
		// Token: 0x06003A8D RID: 14989 RVA: 0x000CAF93 File Offset: 0x000C9193
		public static IPGlobalProperties Create()
		{
			return new Win32IPGlobalProperties();
		}
	}
}
