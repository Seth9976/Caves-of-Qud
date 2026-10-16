using System;
using System.IO;
using Microsoft.Win32.SafeHandles;
using Mono.Security;
using Mono.Security.Authenticode;
using Mono.Security.Cryptography;
using Mono.Security.X509;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x020002D7 RID: 727
	internal class X509Certificate2ImplMono : X509Certificate2ImplUnix
	{
		// Token: 0x17000445 RID: 1093
		// (get) Token: 0x0600169F RID: 5791 RVA: 0x0005A4E0 File Offset: 0x000586E0
		public override bool IsValid
		{
			get
			{
				return this._cert != null;
			}
		}

		// Token: 0x17000446 RID: 1094
		// (get) Token: 0x060016A0 RID: 5792 RVA: 0x00011E15 File Offset: 0x00010015
		public override IntPtr Handle
		{
			get
			{
				return IntPtr.Zero;
			}
		}

		// Token: 0x060016A1 RID: 5793 RVA: 0x00011E15 File Offset: 0x00010015
		public override IntPtr GetNativeAppleCertificate()
		{
			return IntPtr.Zero;
		}

		// Token: 0x060016A2 RID: 5794 RVA: 0x0005A4EB File Offset: 0x000586EB
		public X509Certificate2ImplMono(X509Certificate cert)
		{
			this._cert = cert;
		}

		// Token: 0x060016A3 RID: 5795 RVA: 0x0005A4FA File Offset: 0x000586FA
		private X509Certificate2ImplMono(X509Certificate2ImplMono other)
		{
			this._cert = other._cert;
			if (other.intermediateCerts != null)
			{
				this.intermediateCerts = other.intermediateCerts.Clone();
			}
		}

		// Token: 0x060016A4 RID: 5796 RVA: 0x0005A528 File Offset: 0x00058728
		public X509Certificate2ImplMono(byte[] rawData, SafePasswordHandle password, X509KeyStorageFlags keyStorageFlags)
		{
			switch (X509Certificate2.GetCertContentType(rawData))
			{
			case X509ContentType.Cert:
			case X509ContentType.Pkcs7:
				this._cert = new X509Certificate(rawData);
				return;
			case X509ContentType.Pfx:
				this._cert = this.ImportPkcs12(rawData, password);
				return;
			case X509ContentType.Authenticode:
			{
				AuthenticodeDeformatter authenticodeDeformatter = new AuthenticodeDeformatter(rawData);
				this._cert = authenticodeDeformatter.SigningCertificate;
				if (this._cert != null)
				{
					return;
				}
				break;
			}
			}
			throw new CryptographicException(global::Locale.GetText("Unable to decode certificate."));
		}

		// Token: 0x060016A5 RID: 5797 RVA: 0x0005A5AB File Offset: 0x000587AB
		public override X509CertificateImpl Clone()
		{
			base.ThrowIfContextInvalid();
			return new X509Certificate2ImplMono(this);
		}

		// Token: 0x17000447 RID: 1095
		// (get) Token: 0x060016A6 RID: 5798 RVA: 0x0005A5B9 File Offset: 0x000587B9
		private X509Certificate Cert
		{
			get
			{
				base.ThrowIfContextInvalid();
				return this._cert;
			}
		}

		// Token: 0x060016A7 RID: 5799 RVA: 0x0005A5C7 File Offset: 0x000587C7
		protected override byte[] GetRawCertData()
		{
			base.ThrowIfContextInvalid();
			return this.Cert.RawData;
		}

		// Token: 0x060016A8 RID: 5800 RVA: 0x0005A5DA File Offset: 0x000587DA
		public override bool Equals(X509CertificateImpl other, out bool result)
		{
			result = false;
			return false;
		}

		// Token: 0x060016A9 RID: 5801 RVA: 0x0005A5E0 File Offset: 0x000587E0
		public X509Certificate2ImplMono()
		{
			this._cert = null;
		}

		// Token: 0x17000448 RID: 1096
		// (get) Token: 0x060016AA RID: 5802 RVA: 0x0005A5EF File Offset: 0x000587EF
		public override bool HasPrivateKey
		{
			get
			{
				return this.PrivateKey != null;
			}
		}

		// Token: 0x17000449 RID: 1097
		// (get) Token: 0x060016AB RID: 5803 RVA: 0x0005A5FC File Offset: 0x000587FC
		// (set) Token: 0x060016AC RID: 5804 RVA: 0x0005A6F8 File Offset: 0x000588F8
		public override AsymmetricAlgorithm PrivateKey
		{
			get
			{
				if (this._cert == null)
				{
					throw new CryptographicException(X509Certificate2ImplMono.empty_error);
				}
				try
				{
					RSACryptoServiceProvider rsacryptoServiceProvider = this._cert.RSA as RSACryptoServiceProvider;
					if (rsacryptoServiceProvider != null)
					{
						if (rsacryptoServiceProvider.PublicOnly)
						{
							return null;
						}
						RSACryptoServiceProvider rsacryptoServiceProvider2 = new RSACryptoServiceProvider();
						rsacryptoServiceProvider2.ImportParameters(this._cert.RSA.ExportParameters(true));
						return rsacryptoServiceProvider2;
					}
					else
					{
						Mono.Security.Cryptography.RSAManaged rsamanaged = this._cert.RSA as Mono.Security.Cryptography.RSAManaged;
						if (rsamanaged != null)
						{
							if (rsamanaged.PublicOnly)
							{
								return null;
							}
							Mono.Security.Cryptography.RSAManaged rsamanaged2 = new Mono.Security.Cryptography.RSAManaged();
							rsamanaged2.ImportParameters(this._cert.RSA.ExportParameters(true));
							return rsamanaged2;
						}
						else
						{
							DSACryptoServiceProvider dsacryptoServiceProvider = this._cert.DSA as DSACryptoServiceProvider;
							if (dsacryptoServiceProvider != null)
							{
								if (dsacryptoServiceProvider.PublicOnly)
								{
									return null;
								}
								DSACryptoServiceProvider dsacryptoServiceProvider2 = new DSACryptoServiceProvider();
								dsacryptoServiceProvider2.ImportParameters(this._cert.DSA.ExportParameters(true));
								return dsacryptoServiceProvider2;
							}
						}
					}
				}
				catch
				{
				}
				return null;
			}
			set
			{
				if (this._cert == null)
				{
					throw new CryptographicException(X509Certificate2ImplMono.empty_error);
				}
				if (value == null)
				{
					this._cert.RSA = null;
					this._cert.DSA = null;
					return;
				}
				if (value is RSA)
				{
					this._cert.RSA = (RSA)value;
					return;
				}
				if (value is DSA)
				{
					this._cert.DSA = (DSA)value;
					return;
				}
				throw new NotSupportedException();
			}
		}

		// Token: 0x060016AD RID: 5805 RVA: 0x0005A76D File Offset: 0x0005896D
		public override RSA GetRSAPrivateKey()
		{
			return this.PrivateKey as RSA;
		}

		// Token: 0x060016AE RID: 5806 RVA: 0x0005A77A File Offset: 0x0005897A
		public override DSA GetDSAPrivateKey()
		{
			return this.PrivateKey as DSA;
		}

		// Token: 0x1700044A RID: 1098
		// (get) Token: 0x060016AF RID: 5807 RVA: 0x0005A788 File Offset: 0x00058988
		public override PublicKey PublicKey
		{
			get
			{
				if (this._cert == null)
				{
					throw new CryptographicException(X509Certificate2ImplMono.empty_error);
				}
				if (this._publicKey == null)
				{
					try
					{
						this._publicKey = new PublicKey(this._cert);
					}
					catch (Exception ex)
					{
						throw new CryptographicException(global::Locale.GetText("Unable to decode public key."), ex);
					}
				}
				return this._publicKey;
			}
		}

		// Token: 0x060016B0 RID: 5808 RVA: 0x0005A7EC File Offset: 0x000589EC
		private X509Certificate ImportPkcs12(byte[] rawData, SafePasswordHandle password)
		{
			if (password == null || password.IsInvalid)
			{
				return this.ImportPkcs12(rawData, null);
			}
			string text = password.Mono_DangerousGetString();
			return this.ImportPkcs12(rawData, text);
		}

		// Token: 0x060016B1 RID: 5809 RVA: 0x0005A81C File Offset: 0x00058A1C
		private X509Certificate ImportPkcs12(byte[] rawData, string password)
		{
			PKCS12 pkcs = null;
			if (string.IsNullOrEmpty(password))
			{
				try
				{
					pkcs = new PKCS12(rawData, null);
					goto IL_002B;
				}
				catch
				{
					pkcs = new PKCS12(rawData, string.Empty);
					goto IL_002B;
				}
			}
			pkcs = new PKCS12(rawData, password);
			IL_002B:
			if (pkcs.Certificates.Count == 0)
			{
				return null;
			}
			if (pkcs.Keys.Count == 0)
			{
				return pkcs.Certificates[0];
			}
			X509Certificate x509Certificate = null;
			AsymmetricAlgorithm asymmetricAlgorithm = pkcs.Keys[0] as AsymmetricAlgorithm;
			string text = asymmetricAlgorithm.ToXmlString(false);
			foreach (X509Certificate x509Certificate2 in pkcs.Certificates)
			{
				if ((x509Certificate2.RSA != null && text == x509Certificate2.RSA.ToXmlString(false)) || (x509Certificate2.DSA != null && text == x509Certificate2.DSA.ToXmlString(false)))
				{
					x509Certificate = x509Certificate2;
					break;
				}
			}
			if (x509Certificate == null)
			{
				x509Certificate = pkcs.Certificates[0];
			}
			else
			{
				x509Certificate.RSA = asymmetricAlgorithm as RSA;
				x509Certificate.DSA = asymmetricAlgorithm as DSA;
			}
			if (pkcs.Certificates.Count > 1)
			{
				this.intermediateCerts = new X509CertificateImplCollection();
				foreach (X509Certificate x509Certificate3 in pkcs.Certificates)
				{
					if (x509Certificate3 != x509Certificate)
					{
						X509Certificate2ImplMono x509Certificate2ImplMono = new X509Certificate2ImplMono(x509Certificate3);
						this.intermediateCerts.Add(x509Certificate2ImplMono, true);
					}
				}
			}
			return x509Certificate;
		}

		// Token: 0x060016B2 RID: 5810 RVA: 0x0005A9D4 File Offset: 0x00058BD4
		public override void Reset()
		{
			this._cert = null;
			this._publicKey = null;
			if (this.intermediateCerts != null)
			{
				this.intermediateCerts.Dispose();
				this.intermediateCerts = null;
			}
		}

		// Token: 0x060016B3 RID: 5811 RVA: 0x0005A9FE File Offset: 0x00058BFE
		[MonoTODO("by default this depends on the incomplete X509Chain")]
		public override bool Verify(X509Certificate2 thisCertificate)
		{
			if (this._cert == null)
			{
				throw new CryptographicException(X509Certificate2ImplMono.empty_error);
			}
			return X509Chain.Create().Build(thisCertificate);
		}

		// Token: 0x060016B4 RID: 5812 RVA: 0x0005AA24 File Offset: 0x00058C24
		[MonoTODO("Detection limited to Cert, Pfx, Pkcs12, Pkcs7 and Unknown")]
		public static X509ContentType GetCertContentType(byte[] rawData)
		{
			if (rawData == null || rawData.Length == 0)
			{
				throw new ArgumentException("rawData");
			}
			X509ContentType x509ContentType = X509ContentType.Unknown;
			try
			{
				Mono.Security.ASN1 asn = new Mono.Security.ASN1(rawData);
				if (asn.Tag != 48)
				{
					throw new CryptographicException(global::Locale.GetText("Unable to decode certificate."));
				}
				if (asn.Count == 0)
				{
					return x509ContentType;
				}
				if (asn.Count == 3)
				{
					byte tag = asn[0].Tag;
					if (tag != 2)
					{
						if (tag == 48 && asn[1].Tag == 48 && asn[2].Tag == 3)
						{
							x509ContentType = X509ContentType.Cert;
						}
					}
					else if (asn[1].Tag == 48 && asn[2].Tag == 48)
					{
						x509ContentType = X509ContentType.Pfx;
					}
				}
				if (asn[0].Tag == 6 && asn[0].CompareValue(X509Certificate2ImplMono.signedData))
				{
					x509ContentType = X509ContentType.Pkcs7;
				}
			}
			catch (Exception ex)
			{
				throw new CryptographicException(global::Locale.GetText("Unable to decode certificate."), ex);
			}
			return x509ContentType;
		}

		// Token: 0x060016B5 RID: 5813 RVA: 0x0005AB28 File Offset: 0x00058D28
		[MonoTODO("Detection limited to Cert, Pfx, Pkcs12 and Unknown")]
		public static X509ContentType GetCertContentType(string fileName)
		{
			if (fileName == null)
			{
				throw new ArgumentNullException("fileName");
			}
			if (fileName.Length == 0)
			{
				throw new ArgumentException("fileName");
			}
			return X509Certificate2ImplMono.GetCertContentType(File.ReadAllBytes(fileName));
		}

		// Token: 0x1700044B RID: 1099
		// (get) Token: 0x060016B6 RID: 5814 RVA: 0x0005AB56 File Offset: 0x00058D56
		internal override X509CertificateImplCollection IntermediateCertificates
		{
			get
			{
				return this.intermediateCerts;
			}
		}

		// Token: 0x1700044C RID: 1100
		// (get) Token: 0x060016B7 RID: 5815 RVA: 0x0005AB5E File Offset: 0x00058D5E
		internal X509Certificate MonoCertificate
		{
			get
			{
				return this._cert;
			}
		}

		// Token: 0x1700044D RID: 1101
		// (get) Token: 0x060016B8 RID: 5816 RVA: 0x000075E1 File Offset: 0x000057E1
		internal override X509Certificate2Impl FallbackImpl
		{
			get
			{
				return this;
			}
		}

		// Token: 0x04000D1B RID: 3355
		private PublicKey _publicKey;

		// Token: 0x04000D1C RID: 3356
		private X509CertificateImplCollection intermediateCerts;

		// Token: 0x04000D1D RID: 3357
		private X509Certificate _cert;

		// Token: 0x04000D1E RID: 3358
		private static string empty_error = global::Locale.GetText("Certificate instance is empty.");

		// Token: 0x04000D1F RID: 3359
		private static byte[] signedData = new byte[] { 42, 134, 72, 134, 247, 13, 1, 7, 2 };
	}
}
