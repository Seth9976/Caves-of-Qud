using System;

namespace System.Security.Cryptography.X509Certificates
{
	/// <summary>Defines the status of an X509 chain.</summary>
	// Token: 0x020002C5 RID: 709
	[Flags]
	public enum X509ChainStatusFlags
	{
		/// <summary>Specifies that the X509 chain has no errors.</summary>
		// Token: 0x04000C9A RID: 3226
		NoError = 0,
		/// <summary>Specifies that the X509 chain is not valid due to an invalid time value, such as a value that indicates an expired certificate.</summary>
		// Token: 0x04000C9B RID: 3227
		NotTimeValid = 1,
		/// <summary>Deprecated. Specifies that the CA (certificate authority) certificate and the issued certificate have validity periods that are not nested. For example, the CA cert can be valid from January 1 to December 1 and the issued certificate from January 2 to December 2, which would mean the validity periods are not nested.</summary>
		// Token: 0x04000C9C RID: 3228
		NotTimeNested = 2,
		/// <summary>Specifies that the X509 chain is invalid due to a revoked certificate.</summary>
		// Token: 0x04000C9D RID: 3229
		Revoked = 4,
		/// <summary>Specifies that the X509 chain is invalid due to an invalid certificate signature.</summary>
		// Token: 0x04000C9E RID: 3230
		NotSignatureValid = 8,
		/// <summary>Specifies that the key usage is not valid.</summary>
		// Token: 0x04000C9F RID: 3231
		NotValidForUsage = 16,
		/// <summary>Specifies that the X509 chain is invalid due to an untrusted root certificate.</summary>
		// Token: 0x04000CA0 RID: 3232
		UntrustedRoot = 32,
		/// <summary>Specifies that it is not possible to determine whether the certificate has been revoked. This can be due to the certificate revocation list (CRL) being offline or unavailable.</summary>
		// Token: 0x04000CA1 RID: 3233
		RevocationStatusUnknown = 64,
		/// <summary>Specifies that the X509 chain could not be built.</summary>
		// Token: 0x04000CA2 RID: 3234
		Cyclic = 128,
		/// <summary>Specifies that the X509 chain is invalid due to an invalid extension.</summary>
		// Token: 0x04000CA3 RID: 3235
		InvalidExtension = 256,
		/// <summary>Specifies that the X509 chain is invalid due to invalid policy constraints.</summary>
		// Token: 0x04000CA4 RID: 3236
		InvalidPolicyConstraints = 512,
		/// <summary>Specifies that the X509 chain is invalid due to invalid basic constraints.</summary>
		// Token: 0x04000CA5 RID: 3237
		InvalidBasicConstraints = 1024,
		/// <summary>Specifies that the X509 chain is invalid due to invalid name constraints.</summary>
		// Token: 0x04000CA6 RID: 3238
		InvalidNameConstraints = 2048,
		/// <summary>Specifies that the certificate does not have a supported name constraint or has a name constraint that is unsupported.</summary>
		// Token: 0x04000CA7 RID: 3239
		HasNotSupportedNameConstraint = 4096,
		/// <summary>Specifies that the certificate has an undefined name constraint.</summary>
		// Token: 0x04000CA8 RID: 3240
		HasNotDefinedNameConstraint = 8192,
		/// <summary>Specifies that the certificate has an impermissible name constraint.</summary>
		// Token: 0x04000CA9 RID: 3241
		HasNotPermittedNameConstraint = 16384,
		/// <summary>Specifies that the X509 chain is invalid because a certificate has excluded a name constraint.</summary>
		// Token: 0x04000CAA RID: 3242
		HasExcludedNameConstraint = 32768,
		/// <summary>Specifies that the X509 chain could not be built up to the root certificate.</summary>
		// Token: 0x04000CAB RID: 3243
		PartialChain = 65536,
		/// <summary>Specifies that the certificate trust list (CTL) is not valid because of an invalid time value, such as one that indicates that the CTL has expired.</summary>
		// Token: 0x04000CAC RID: 3244
		CtlNotTimeValid = 131072,
		/// <summary>Specifies that the certificate trust list (CTL) contains an invalid signature.</summary>
		// Token: 0x04000CAD RID: 3245
		CtlNotSignatureValid = 262144,
		/// <summary>Specifies that the certificate trust list (CTL) is not valid for this use.</summary>
		// Token: 0x04000CAE RID: 3246
		CtlNotValidForUsage = 524288,
		/// <summary>Specifies that the online certificate revocation list (CRL) the X509 chain relies on is currently offline.</summary>
		// Token: 0x04000CAF RID: 3247
		OfflineRevocation = 16777216,
		/// <summary>Specifies that there is no certificate policy extension in the certificate. This error would occur if a group policy has specified that all certificates must have a certificate policy.</summary>
		// Token: 0x04000CB0 RID: 3248
		NoIssuanceChainPolicy = 33554432,
		// Token: 0x04000CB1 RID: 3249
		ExplicitDistrust = 67108864,
		// Token: 0x04000CB2 RID: 3250
		HasNotSupportedCriticalExtension = 134217728,
		// Token: 0x04000CB3 RID: 3251
		HasWeakSignature = 1048576
	}
}
