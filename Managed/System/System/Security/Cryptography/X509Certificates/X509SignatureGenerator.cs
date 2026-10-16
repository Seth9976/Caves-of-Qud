using System;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x020002ED RID: 749
	public abstract class X509SignatureGenerator
	{
		// Token: 0x060017DF RID: 6111 RVA: 0x0005E6E0 File Offset: 0x0005C8E0
		protected X509SignatureGenerator()
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x170004A9 RID: 1193
		// (get) Token: 0x060017E0 RID: 6112 RVA: 0x00011F38 File Offset: 0x00010138
		public PublicKey PublicKey
		{
			get
			{
				throw new PlatformNotSupportedException();
			}
		}

		// Token: 0x060017E1 RID: 6113
		protected abstract PublicKey BuildPublicKey();

		// Token: 0x060017E2 RID: 6114 RVA: 0x00011F38 File Offset: 0x00010138
		public static X509SignatureGenerator CreateForECDsa(ECDsa key)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060017E3 RID: 6115 RVA: 0x00011F38 File Offset: 0x00010138
		public static X509SignatureGenerator CreateForRSA(RSA key, RSASignaturePadding signaturePadding)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060017E4 RID: 6116
		public abstract byte[] GetSignatureAlgorithmIdentifier(HashAlgorithmName hashAlgorithm);

		// Token: 0x060017E5 RID: 6117
		public abstract byte[] SignData(byte[] data, HashAlgorithmName hashAlgorithm);
	}
}
