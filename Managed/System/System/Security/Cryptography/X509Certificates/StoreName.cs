using System;

namespace System.Security.Cryptography.X509Certificates
{
	/// <summary>Specifies the name of the X.509 certificate store to open.</summary>
	// Token: 0x020002C2 RID: 706
	public enum StoreName
	{
		/// <summary>The X.509 certificate store for other users.</summary>
		// Token: 0x04000C84 RID: 3204
		AddressBook = 1,
		/// <summary>The X.509 certificate store for third-party certificate authorities (CAs).</summary>
		// Token: 0x04000C85 RID: 3205
		AuthRoot,
		/// <summary>The X.509 certificate store for intermediate certificate authorities (CAs). </summary>
		// Token: 0x04000C86 RID: 3206
		CertificateAuthority,
		/// <summary>The X.509 certificate store for revoked certificates.</summary>
		// Token: 0x04000C87 RID: 3207
		Disallowed,
		/// <summary>The X.509 certificate store for personal certificates.</summary>
		// Token: 0x04000C88 RID: 3208
		My,
		/// <summary>The X.509 certificate store for trusted root certificate authorities (CAs).</summary>
		// Token: 0x04000C89 RID: 3209
		Root,
		/// <summary>The X.509 certificate store for directly trusted people and resources.</summary>
		// Token: 0x04000C8A RID: 3210
		TrustedPeople,
		/// <summary>The X.509 certificate store for directly trusted publishers.</summary>
		// Token: 0x04000C8B RID: 3211
		TrustedPublisher
	}
}
