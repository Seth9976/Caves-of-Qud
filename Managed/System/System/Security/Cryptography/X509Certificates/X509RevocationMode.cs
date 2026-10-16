using System;

namespace System.Security.Cryptography.X509Certificates
{
	/// <summary>Specifies the mode used to check for X509 certificate revocation.</summary>
	// Token: 0x020002CB RID: 715
	public enum X509RevocationMode
	{
		/// <summary>No revocation check is performed on the certificate.</summary>
		// Token: 0x04000CE0 RID: 3296
		NoCheck,
		/// <summary>A revocation check is made using an online certificate revocation list (CRL).</summary>
		// Token: 0x04000CE1 RID: 3297
		Online,
		/// <summary>A revocation check is made using a cached certificate revocation list (CRL).</summary>
		// Token: 0x04000CE2 RID: 3298
		Offline
	}
}
