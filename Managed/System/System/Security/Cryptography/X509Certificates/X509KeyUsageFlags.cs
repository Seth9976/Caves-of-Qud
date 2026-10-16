using System;

namespace System.Security.Cryptography.X509Certificates
{
	/// <summary>Defines how the certificate key can be used. If this value is not defined, the key can be used for any purpose.</summary>
	// Token: 0x020002C8 RID: 712
	[Flags]
	public enum X509KeyUsageFlags
	{
		/// <summary>No key usage parameters.</summary>
		// Token: 0x04000CCA RID: 3274
		None = 0,
		/// <summary>The key can be used for encryption only.</summary>
		// Token: 0x04000CCB RID: 3275
		EncipherOnly = 1,
		/// <summary>The key can be used to sign a certificate revocation list (CRL).</summary>
		// Token: 0x04000CCC RID: 3276
		CrlSign = 2,
		/// <summary>The key can be used to sign certificates.</summary>
		// Token: 0x04000CCD RID: 3277
		KeyCertSign = 4,
		/// <summary>The key can be used to determine key agreement, such as a key created using the Diffie-Hellman key agreement algorithm.</summary>
		// Token: 0x04000CCE RID: 3278
		KeyAgreement = 8,
		/// <summary>The key can be used for data encryption.</summary>
		// Token: 0x04000CCF RID: 3279
		DataEncipherment = 16,
		/// <summary>The key can be used for key encryption.</summary>
		// Token: 0x04000CD0 RID: 3280
		KeyEncipherment = 32,
		/// <summary>The key can be used for authentication.</summary>
		// Token: 0x04000CD1 RID: 3281
		NonRepudiation = 64,
		/// <summary>The key can be used as a digital signature.</summary>
		// Token: 0x04000CD2 RID: 3282
		DigitalSignature = 128,
		/// <summary>The key can be used for decryption only.</summary>
		// Token: 0x04000CD3 RID: 3283
		DecipherOnly = 32768
	}
}
