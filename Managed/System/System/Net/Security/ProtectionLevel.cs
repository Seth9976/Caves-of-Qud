using System;

namespace System.Net.Security
{
	/// <summary>Indicates the security services requested for an authenticated stream.</summary>
	// Token: 0x0200085B RID: 2139
	public enum ProtectionLevel
	{
		/// <summary>Authentication only.</summary>
		// Token: 0x04002939 RID: 10553
		None,
		/// <summary>Sign data to help ensure the integrity of transmitted data.</summary>
		// Token: 0x0400293A RID: 10554
		Sign,
		/// <summary>Encrypt and sign data to help ensure the confidentiality and integrity of transmitted data.</summary>
		// Token: 0x0400293B RID: 10555
		EncryptAndSign
	}
}
