using System;

namespace System.Security.Cryptography.X509Certificates
{
	/// <summary>Specifies conditions under which verification of certificates in the X509 chain should be conducted.</summary>
	// Token: 0x020002CD RID: 717
	[Flags]
	public enum X509VerificationFlags
	{
		/// <summary>No flags pertaining to verification are included.</summary>
		// Token: 0x04000CE8 RID: 3304
		NoFlag = 0,
		/// <summary>Ignore certificates in the chain that are not valid either because they have expired or they are not yet in effect when determining certificate validity.</summary>
		// Token: 0x04000CE9 RID: 3305
		IgnoreNotTimeValid = 1,
		/// <summary>Ignore that the certificate trust list (CTL) is not valid, for reasons such as the CTL has expired, when determining certificate verification.</summary>
		// Token: 0x04000CEA RID: 3306
		IgnoreCtlNotTimeValid = 2,
		/// <summary>Ignore that the CA (certificate authority) certificate and the issued certificate have validity periods that are not nested when verifying the certificate. For example, the CA cert can be valid from January 1 to December 1 and the issued certificate from January 2 to December 2, which would mean the validity periods are not nested.</summary>
		// Token: 0x04000CEB RID: 3307
		IgnoreNotTimeNested = 4,
		/// <summary>Ignore that the basic constraints are not valid when determining certificate verification.</summary>
		// Token: 0x04000CEC RID: 3308
		IgnoreInvalidBasicConstraints = 8,
		/// <summary>Ignore that the chain cannot be verified due to an unknown certificate authority (CA).</summary>
		// Token: 0x04000CED RID: 3309
		AllowUnknownCertificateAuthority = 16,
		/// <summary>Ignore that the certificate was not issued for the current use when determining certificate verification.</summary>
		// Token: 0x04000CEE RID: 3310
		IgnoreWrongUsage = 32,
		/// <summary>Ignore that the certificate has an invalid name when determining certificate verification.</summary>
		// Token: 0x04000CEF RID: 3311
		IgnoreInvalidName = 64,
		/// <summary>Ignore that the certificate has invalid policy when determining certificate verification.</summary>
		// Token: 0x04000CF0 RID: 3312
		IgnoreInvalidPolicy = 128,
		/// <summary>Ignore that the end certificate (the user certificate) revocation is unknown when determining certificate verification.</summary>
		// Token: 0x04000CF1 RID: 3313
		IgnoreEndRevocationUnknown = 256,
		/// <summary>Ignore that the certificate trust list (CTL) signer revocation is unknown when determining certificate verification.</summary>
		// Token: 0x04000CF2 RID: 3314
		IgnoreCtlSignerRevocationUnknown = 512,
		/// <summary>Ignore that the certificate authority revocation is unknown when determining certificate verification.</summary>
		// Token: 0x04000CF3 RID: 3315
		IgnoreCertificateAuthorityRevocationUnknown = 1024,
		/// <summary>Ignore that the root revocation is unknown when determining certificate verification.</summary>
		// Token: 0x04000CF4 RID: 3316
		IgnoreRootRevocationUnknown = 2048,
		/// <summary>All flags pertaining to verification are included.</summary>
		// Token: 0x04000CF5 RID: 3317
		AllFlags = 4095
	}
}
