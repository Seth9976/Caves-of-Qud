using System;

namespace System.Security.Cryptography.X509Certificates
{
	/// <summary>Specifies how much of the X.509 certificate chain should be included in the X.509 data.</summary>
	// Token: 0x020002C7 RID: 711
	public enum X509IncludeOption
	{
		/// <summary>No X.509 chain information is included.</summary>
		// Token: 0x04000CC5 RID: 3269
		None,
		/// <summary>The entire X.509 chain is included except for the root certificate.</summary>
		// Token: 0x04000CC6 RID: 3270
		ExcludeRoot,
		/// <summary>Only the end certificate is included in the X.509 chain information.</summary>
		// Token: 0x04000CC7 RID: 3271
		EndCertOnly,
		/// <summary>The entire X.509 chain is included.</summary>
		// Token: 0x04000CC8 RID: 3272
		WholeChain
	}
}
