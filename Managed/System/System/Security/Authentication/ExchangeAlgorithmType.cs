using System;

namespace System.Security.Authentication
{
	/// <summary>Specifies the algorithm used to create keys shared by the client and server.</summary>
	// Token: 0x020002A0 RID: 672
	public enum ExchangeAlgorithmType
	{
		/// <summary>No key exchange algorithm is used.</summary>
		// Token: 0x04000BFE RID: 3070
		None,
		/// <summary>The Diffie Hellman ephemeral key exchange algorithm.</summary>
		// Token: 0x04000BFF RID: 3071
		DiffieHellman = 43522,
		/// <summary>The RSA public-key exchange algorithm.</summary>
		// Token: 0x04000C00 RID: 3072
		RsaKeyX = 41984,
		/// <summary>The RSA public-key signature algorithm.</summary>
		// Token: 0x04000C01 RID: 3073
		RsaSign = 9216
	}
}
