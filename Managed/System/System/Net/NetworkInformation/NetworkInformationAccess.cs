using System;

namespace System.Net.NetworkInformation
{
	/// <summary>Specifies permission to access information about network interfaces and traffic statistics.</summary>
	// Token: 0x020006F8 RID: 1784
	[Flags]
	public enum NetworkInformationAccess
	{
		/// <summary>No access to network information.</summary>
		// Token: 0x040021B4 RID: 8628
		None = 0,
		/// <summary>Read access to network information.</summary>
		// Token: 0x040021B5 RID: 8629
		Read = 1,
		/// <summary>Ping access to network information.</summary>
		// Token: 0x040021B6 RID: 8630
		Ping = 4
	}
}
