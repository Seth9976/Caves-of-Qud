using System;

namespace System.Security.Authentication
{
	/// <summary>Defines the possible cipher algorithms for the <see cref="T:System.Net.Security.SslStream" /> class.</summary>
	// Token: 0x0200029F RID: 671
	public enum CipherAlgorithmType
	{
		/// <summary>No encryption algorithm is used.</summary>
		// Token: 0x04000BF3 RID: 3059
		None,
		/// <summary>No encryption is used with a Null cipher algorithm. </summary>
		// Token: 0x04000BF4 RID: 3060
		Null = 24576,
		/// <summary>The Advanced Encryption Standard (AES) algorithm.</summary>
		// Token: 0x04000BF5 RID: 3061
		Aes = 26129,
		/// <summary>The Advanced Encryption Standard (AES) algorithm with a 128 bit key.</summary>
		// Token: 0x04000BF6 RID: 3062
		Aes128 = 26126,
		/// <summary>The Advanced Encryption Standard (AES) algorithm with a 192 bit key.</summary>
		// Token: 0x04000BF7 RID: 3063
		Aes192,
		/// <summary>The Advanced Encryption Standard (AES) algorithm with a 256 bit key.</summary>
		// Token: 0x04000BF8 RID: 3064
		Aes256,
		/// <summary>The Data Encryption Standard (DES) algorithm.</summary>
		// Token: 0x04000BF9 RID: 3065
		Des = 26113,
		/// <summary>Rivest's Code 2 (RC2) algorithm.</summary>
		// Token: 0x04000BFA RID: 3066
		Rc2,
		/// <summary>Rivest's Code 4 (RC4) algorithm.</summary>
		// Token: 0x04000BFB RID: 3067
		Rc4 = 26625,
		/// <summary>The Triple Data Encryption Standard (3DES) algorithm.</summary>
		// Token: 0x04000BFC RID: 3068
		TripleDes = 26115
	}
}
