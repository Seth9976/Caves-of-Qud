using System;

namespace System.Web.Configuration
{
	/// <summary>Specifies the hashing algorithm that ASP.NET uses for forms authentication and for validating view state data, and for out-of-process session state identification.</summary>
	// Token: 0x02000568 RID: 1384
	public enum MachineKeyValidation
	{
		/// <summary>Specifies that ASP.NET uses the Message Digest 5 (MD5) hashing algorithm. </summary>
		// Token: 0x04002020 RID: 8224
		MD5,
		/// <summary>Specifies that ASP.NET uses the HMACSHA1 hash algorithm.</summary>
		// Token: 0x04002021 RID: 8225
		SHA1,
		/// <summary>Specifies that ASP.NET uses the TripleDES (3DES) encryption algorithm. </summary>
		// Token: 0x04002022 RID: 8226
		TripleDES,
		/// <summary>Specifies that ASP.NET uses the AES (Rijndael) encryption algorithm.</summary>
		// Token: 0x04002023 RID: 8227
		AES,
		/// <summary>Specifies that ASP.NET uses the HMACSHA256 hashing algorithm.  This is the default value.</summary>
		// Token: 0x04002024 RID: 8228
		HMACSHA256,
		/// <summary>Specifies that ASP.NET uses the HMACSHA384 hashing algorithm.</summary>
		// Token: 0x04002025 RID: 8229
		HMACSHA384,
		/// <summary>Specifies that ASP.NET uses the HMACSHA512 hashing algorithm.</summary>
		// Token: 0x04002026 RID: 8230
		HMACSHA512,
		/// <summary>Specifies that ASP.NET uses a custom hashing algorithm. </summary>
		// Token: 0x04002027 RID: 8231
		Custom
	}
}
