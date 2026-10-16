using System;

namespace System.Security.Cryptography.X509Certificates
{
	/// <summary>Specifies the location of the X.509 certificate store.</summary>
	// Token: 0x020002C1 RID: 705
	public enum StoreLocation
	{
		/// <summary>The X.509 certificate store used by the current user.</summary>
		// Token: 0x04000C81 RID: 3201
		CurrentUser = 1,
		/// <summary>The X.509 certificate store assigned to the local machine.</summary>
		// Token: 0x04000C82 RID: 3202
		LocalMachine
	}
}
