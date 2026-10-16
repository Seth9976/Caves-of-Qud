using System;

namespace System.Security.Cryptography.X509Certificates
{
	/// <summary>Specifies the type of name the X509 certificate contains.</summary>
	// Token: 0x020002C9 RID: 713
	public enum X509NameType
	{
		/// <summary>The simple name of a subject or issuer of an X509 certificate.</summary>
		// Token: 0x04000CD5 RID: 3285
		SimpleName,
		/// <summary>The email address of the subject or issuer associated of an X509 certificate.</summary>
		// Token: 0x04000CD6 RID: 3286
		EmailName,
		/// <summary>The UPN name of the subject or issuer of an X509 certificate.</summary>
		// Token: 0x04000CD7 RID: 3287
		UpnName,
		/// <summary>The DNS name associated with the alternative name of either the subject or issuer of an X509 certificate.</summary>
		// Token: 0x04000CD8 RID: 3288
		DnsName,
		/// <summary>The DNS name associated with the alternative name of either the subject or the issuer of an X.509 certificate.  This value is equivalent to the <see cref="F:System.Security.Cryptography.X509Certificates.X509NameType.DnsName" /> value.</summary>
		// Token: 0x04000CD9 RID: 3289
		DnsFromAlternativeName,
		/// <summary>The URL address associated with the alternative name of either the subject or issuer of an X509 certificate.</summary>
		// Token: 0x04000CDA RID: 3290
		UrlName
	}
}
