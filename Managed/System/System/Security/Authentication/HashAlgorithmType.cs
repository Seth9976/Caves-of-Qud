using System;

namespace System.Security.Authentication
{
	/// <summary>Specifies the algorithm used for generating message authentication codes (MACs).</summary>
	// Token: 0x020002A1 RID: 673
	public enum HashAlgorithmType
	{
		/// <summary>No hashing algorithm is used.</summary>
		// Token: 0x04000C03 RID: 3075
		None,
		/// <summary>The Message Digest 5 (MD5) hashing algorithm.</summary>
		// Token: 0x04000C04 RID: 3076
		Md5 = 32771,
		/// <summary>The Secure Hashing Algorithm (SHA1).</summary>
		// Token: 0x04000C05 RID: 3077
		Sha1,
		// Token: 0x04000C06 RID: 3078
		Sha256 = 32780,
		// Token: 0x04000C07 RID: 3079
		Sha384,
		// Token: 0x04000C08 RID: 3080
		Sha512
	}
}
