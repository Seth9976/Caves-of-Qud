using System;

namespace System.Net.Security
{
	/// <summary>Enumerates Secure Socket Layer (SSL) policy errors.</summary>
	// Token: 0x02000861 RID: 2145
	[Flags]
	public enum SslPolicyErrors
	{
		/// <summary>No SSL policy errors.</summary>
		// Token: 0x04002948 RID: 10568
		None = 0,
		/// <summary>Certificate not available.</summary>
		// Token: 0x04002949 RID: 10569
		RemoteCertificateNotAvailable = 1,
		/// <summary>Certificate name mismatch.</summary>
		// Token: 0x0400294A RID: 10570
		RemoteCertificateNameMismatch = 2,
		/// <summary>
		///   <see cref="P:System.Security.Cryptography.X509Certificates.X509Chain.ChainStatus" /> has returned a non empty array.</summary>
		// Token: 0x0400294B RID: 10571
		RemoteCertificateChainErrors = 4
	}
}
