using System;

namespace System.Security.Cryptography.X509Certificates
{
	/// <summary>Specifies which X509 certificates in the chain should be checked for revocation.</summary>
	// Token: 0x020002CA RID: 714
	public enum X509RevocationFlag
	{
		/// <summary>Only the end certificate is checked for revocation.</summary>
		// Token: 0x04000CDC RID: 3292
		EndCertificateOnly,
		/// <summary>The entire chain of certificates is checked for revocation.</summary>
		// Token: 0x04000CDD RID: 3293
		EntireChain,
		/// <summary>The entire chain, except the root certificate, is checked for revocation.</summary>
		// Token: 0x04000CDE RID: 3294
		ExcludeRoot
	}
}
