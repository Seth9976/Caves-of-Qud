using System;
using System.Collections.ObjectModel;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x020002EC RID: 748
	public sealed class CertificateRequest
	{
		// Token: 0x060017D1 RID: 6097 RVA: 0x0005E6E0 File Offset: 0x0005C8E0
		public CertificateRequest(X500DistinguishedName subjectName, ECDsa key, HashAlgorithmName hashAlgorithm)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060017D2 RID: 6098 RVA: 0x0005E6E0 File Offset: 0x0005C8E0
		public CertificateRequest(X500DistinguishedName subjectName, RSA key, HashAlgorithmName hashAlgorithm, RSASignaturePadding padding)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060017D3 RID: 6099 RVA: 0x0005E6E0 File Offset: 0x0005C8E0
		public CertificateRequest(X500DistinguishedName subjectName, PublicKey publicKey, HashAlgorithmName hashAlgorithm)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060017D4 RID: 6100 RVA: 0x0005E6E0 File Offset: 0x0005C8E0
		public CertificateRequest(string subjectName, ECDsa key, HashAlgorithmName hashAlgorithm)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060017D5 RID: 6101 RVA: 0x0005E6E0 File Offset: 0x0005C8E0
		public CertificateRequest(string subjectName, RSA key, HashAlgorithmName hashAlgorithm, RSASignaturePadding padding)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x170004A5 RID: 1189
		// (get) Token: 0x060017D6 RID: 6102 RVA: 0x00011F38 File Offset: 0x00010138
		public Collection<X509Extension> CertificateExtensions
		{
			get
			{
				throw new PlatformNotSupportedException();
			}
		}

		// Token: 0x170004A6 RID: 1190
		// (get) Token: 0x060017D7 RID: 6103 RVA: 0x0005E6F0 File Offset: 0x0005C8F0
		public HashAlgorithmName HashAlgorithm
		{
			get
			{
				throw new PlatformNotSupportedException();
			}
		}

		// Token: 0x170004A7 RID: 1191
		// (get) Token: 0x060017D8 RID: 6104 RVA: 0x00011F38 File Offset: 0x00010138
		public PublicKey PublicKey
		{
			get
			{
				throw new PlatformNotSupportedException();
			}
		}

		// Token: 0x170004A8 RID: 1192
		// (get) Token: 0x060017D9 RID: 6105 RVA: 0x00011F38 File Offset: 0x00010138
		public X500DistinguishedName SubjectName
		{
			get
			{
				throw new PlatformNotSupportedException();
			}
		}

		// Token: 0x060017DA RID: 6106 RVA: 0x00011F38 File Offset: 0x00010138
		public X509Certificate2 Create(X500DistinguishedName issuerName, X509SignatureGenerator generator, DateTimeOffset notBefore, DateTimeOffset notAfter, byte[] serialNumber)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060017DB RID: 6107 RVA: 0x00011F38 File Offset: 0x00010138
		public X509Certificate2 Create(X509Certificate2 issuerCertificate, DateTimeOffset notBefore, DateTimeOffset notAfter, byte[] serialNumber)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060017DC RID: 6108 RVA: 0x00011F38 File Offset: 0x00010138
		public X509Certificate2 CreateSelfSigned(DateTimeOffset notBefore, DateTimeOffset notAfter)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060017DD RID: 6109 RVA: 0x00011F38 File Offset: 0x00010138
		public byte[] CreateSigningRequest()
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060017DE RID: 6110 RVA: 0x00011F38 File Offset: 0x00010138
		public byte[] CreateSigningRequest(X509SignatureGenerator signatureGenerator)
		{
			throw new PlatformNotSupportedException();
		}
	}
}
