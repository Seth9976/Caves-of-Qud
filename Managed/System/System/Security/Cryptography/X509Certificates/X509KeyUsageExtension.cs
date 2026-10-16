using System;
using System.Text;
using Mono.Security;

namespace System.Security.Cryptography.X509Certificates
{
	/// <summary>Defines the usage of a key contained within an X.509 certificate.  This class cannot be inherited.</summary>
	// Token: 0x020002E9 RID: 745
	public sealed class X509KeyUsageExtension : X509Extension
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509KeyUsageExtension" /> class.</summary>
		// Token: 0x060017A1 RID: 6049 RVA: 0x0005D6C1 File Offset: 0x0005B8C1
		public X509KeyUsageExtension()
		{
			this._oid = new Oid("2.5.29.15", "Key Usage");
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509KeyUsageExtension" /> class using an <see cref="T:System.Security.Cryptography.AsnEncodedData" /> object and a value that identifies whether the extension is critical. </summary>
		/// <param name="encodedKeyUsage">The encoded data to use to create the extension.</param>
		/// <param name="critical">true if the extension is critical; otherwise, false.</param>
		// Token: 0x060017A2 RID: 6050 RVA: 0x0005D6E0 File Offset: 0x0005B8E0
		public X509KeyUsageExtension(AsnEncodedData encodedKeyUsage, bool critical)
		{
			this._oid = new Oid("2.5.29.15", "Key Usage");
			this._raw = encodedKeyUsage.RawData;
			base.Critical = critical;
			this._status = this.Decode(base.RawData);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509KeyUsageExtension" /> class using the specified <see cref="T:System.Security.Cryptography.X509Certificates.X509KeyUsageFlags" /> value and a value that identifies whether the extension is critical. </summary>
		/// <param name="keyUsages">One of the <see cref="T:System.Security.Cryptography.X509Certificates.X509KeyUsageFlags" /> values that describes how to use the key.</param>
		/// <param name="critical">true if the extension is critical; otherwise, false.</param>
		// Token: 0x060017A3 RID: 6051 RVA: 0x0005D72D File Offset: 0x0005B92D
		public X509KeyUsageExtension(X509KeyUsageFlags keyUsages, bool critical)
		{
			this._oid = new Oid("2.5.29.15", "Key Usage");
			base.Critical = critical;
			this._keyUsages = this.GetValidFlags(keyUsages);
			base.RawData = this.Encode();
		}

		/// <summary>Gets the key usage flag associated with the certificate.</summary>
		/// <returns>One of the <see cref="P:System.Security.Cryptography.X509Certificates.X509KeyUsageExtension.KeyUsages" /> values.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The extension cannot be decoded. </exception>
		// Token: 0x1700049B RID: 1179
		// (get) Token: 0x060017A4 RID: 6052 RVA: 0x0005D76C File Offset: 0x0005B96C
		public X509KeyUsageFlags KeyUsages
		{
			get
			{
				AsnDecodeStatus status = this._status;
				if (status == AsnDecodeStatus.Ok || status == AsnDecodeStatus.InformationNotAvailable)
				{
					return this._keyUsages;
				}
				throw new CryptographicException("Badly encoded extension.");
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509KeyUsageExtension" /> class using an <see cref="T:System.Security.Cryptography.AsnEncodedData" /> object. </summary>
		/// <param name="asnEncodedData">The encoded data to use to create the extension.</param>
		// Token: 0x060017A5 RID: 6053 RVA: 0x0005D798 File Offset: 0x0005B998
		public override void CopyFrom(AsnEncodedData asnEncodedData)
		{
			if (asnEncodedData == null)
			{
				throw new ArgumentNullException("asnEncodedData");
			}
			X509Extension x509Extension = asnEncodedData as X509Extension;
			if (x509Extension == null)
			{
				throw new ArgumentException(global::Locale.GetText("Wrong type."), "asnEncodedData");
			}
			if (x509Extension._oid == null)
			{
				this._oid = new Oid("2.5.29.15", "Key Usage");
			}
			else
			{
				this._oid = new Oid(x509Extension._oid);
			}
			base.RawData = x509Extension.RawData;
			base.Critical = x509Extension.Critical;
			this._status = this.Decode(base.RawData);
		}

		// Token: 0x060017A6 RID: 6054 RVA: 0x0005D82C File Offset: 0x0005BA2C
		internal X509KeyUsageFlags GetValidFlags(X509KeyUsageFlags flags)
		{
			if ((flags & (X509KeyUsageFlags.EncipherOnly | X509KeyUsageFlags.CrlSign | X509KeyUsageFlags.KeyCertSign | X509KeyUsageFlags.KeyAgreement | X509KeyUsageFlags.DataEncipherment | X509KeyUsageFlags.KeyEncipherment | X509KeyUsageFlags.NonRepudiation | X509KeyUsageFlags.DigitalSignature | X509KeyUsageFlags.DecipherOnly)) != flags)
			{
				return X509KeyUsageFlags.None;
			}
			return flags;
		}

		// Token: 0x060017A7 RID: 6055 RVA: 0x0005D83C File Offset: 0x0005BA3C
		internal AsnDecodeStatus Decode(byte[] extension)
		{
			if (extension == null || extension.Length == 0)
			{
				return AsnDecodeStatus.BadAsn;
			}
			if (extension[0] != 3)
			{
				return AsnDecodeStatus.BadTag;
			}
			if (extension.Length < 3)
			{
				return AsnDecodeStatus.BadLength;
			}
			if (extension.Length < 4)
			{
				return AsnDecodeStatus.InformationNotAvailable;
			}
			try
			{
				Mono.Security.ASN1 asn = new Mono.Security.ASN1(extension);
				int num = 0;
				int i = 1;
				while (i < asn.Value.Length)
				{
					num = (num << 8) + (int)asn.Value[i++];
				}
				this._keyUsages = this.GetValidFlags((X509KeyUsageFlags)num);
			}
			catch
			{
				return AsnDecodeStatus.BadAsn;
			}
			return AsnDecodeStatus.Ok;
		}

		// Token: 0x060017A8 RID: 6056 RVA: 0x0005D8BC File Offset: 0x0005BABC
		internal byte[] Encode()
		{
			int keyUsages = (int)this._keyUsages;
			byte b = 0;
			Mono.Security.ASN1 asn;
			if (keyUsages == 0)
			{
				asn = new Mono.Security.ASN1(3, new byte[] { b });
			}
			else
			{
				int num = ((keyUsages < 255) ? keyUsages : (keyUsages >> 8));
				while ((num & 1) == 0 && b < 8)
				{
					b += 1;
					num >>= 1;
				}
				if (keyUsages <= 255)
				{
					asn = new Mono.Security.ASN1(3, new byte[]
					{
						b,
						(byte)keyUsages
					});
				}
				else
				{
					asn = new Mono.Security.ASN1(3, new byte[]
					{
						b,
						(byte)keyUsages,
						(byte)(keyUsages >> 8)
					});
				}
			}
			return asn.GetBytes();
		}

		// Token: 0x060017A9 RID: 6057 RVA: 0x0005D950 File Offset: 0x0005BB50
		internal override string ToString(bool multiLine)
		{
			switch (this._status)
			{
			case AsnDecodeStatus.BadAsn:
				return string.Empty;
			case AsnDecodeStatus.BadTag:
			case AsnDecodeStatus.BadLength:
				return base.FormatUnkownData(this._raw);
			case AsnDecodeStatus.InformationNotAvailable:
				return "Information Not Available";
			default:
			{
				if (this._oid.Value != "2.5.29.15")
				{
					return string.Format("Unknown Key Usage ({0})", this._oid.Value);
				}
				if (this._keyUsages == X509KeyUsageFlags.None)
				{
					return "Information Not Available";
				}
				StringBuilder stringBuilder = new StringBuilder();
				if ((this._keyUsages & X509KeyUsageFlags.DigitalSignature) != X509KeyUsageFlags.None)
				{
					stringBuilder.Append("Digital Signature");
				}
				if ((this._keyUsages & X509KeyUsageFlags.NonRepudiation) != X509KeyUsageFlags.None)
				{
					if (stringBuilder.Length > 0)
					{
						stringBuilder.Append(", ");
					}
					stringBuilder.Append("Non-Repudiation");
				}
				if ((this._keyUsages & X509KeyUsageFlags.KeyEncipherment) != X509KeyUsageFlags.None)
				{
					if (stringBuilder.Length > 0)
					{
						stringBuilder.Append(", ");
					}
					stringBuilder.Append("Key Encipherment");
				}
				if ((this._keyUsages & X509KeyUsageFlags.DataEncipherment) != X509KeyUsageFlags.None)
				{
					if (stringBuilder.Length > 0)
					{
						stringBuilder.Append(", ");
					}
					stringBuilder.Append("Data Encipherment");
				}
				if ((this._keyUsages & X509KeyUsageFlags.KeyAgreement) != X509KeyUsageFlags.None)
				{
					if (stringBuilder.Length > 0)
					{
						stringBuilder.Append(", ");
					}
					stringBuilder.Append("Key Agreement");
				}
				if ((this._keyUsages & X509KeyUsageFlags.KeyCertSign) != X509KeyUsageFlags.None)
				{
					if (stringBuilder.Length > 0)
					{
						stringBuilder.Append(", ");
					}
					stringBuilder.Append("Certificate Signing");
				}
				if ((this._keyUsages & X509KeyUsageFlags.CrlSign) != X509KeyUsageFlags.None)
				{
					if (stringBuilder.Length > 0)
					{
						stringBuilder.Append(", ");
					}
					stringBuilder.Append("Off-line CRL Signing, CRL Signing");
				}
				if ((this._keyUsages & X509KeyUsageFlags.EncipherOnly) != X509KeyUsageFlags.None)
				{
					if (stringBuilder.Length > 0)
					{
						stringBuilder.Append(", ");
					}
					stringBuilder.Append("Encipher Only");
				}
				if ((this._keyUsages & X509KeyUsageFlags.DecipherOnly) != X509KeyUsageFlags.None)
				{
					if (stringBuilder.Length > 0)
					{
						stringBuilder.Append(", ");
					}
					stringBuilder.Append("Decipher Only");
				}
				int keyUsages = (int)this._keyUsages;
				stringBuilder.Append(" (");
				stringBuilder.Append(((byte)keyUsages).ToString("x2"));
				if (keyUsages > 255)
				{
					stringBuilder.Append(" ");
					stringBuilder.Append(((byte)(keyUsages >> 8)).ToString("x2"));
				}
				stringBuilder.Append(")");
				if (multiLine)
				{
					stringBuilder.Append(Environment.NewLine);
				}
				return stringBuilder.ToString();
			}
			}
		}

		// Token: 0x04000D4E RID: 3406
		internal const string oid = "2.5.29.15";

		// Token: 0x04000D4F RID: 3407
		internal const string friendlyName = "Key Usage";

		// Token: 0x04000D50 RID: 3408
		internal const X509KeyUsageFlags all = X509KeyUsageFlags.EncipherOnly | X509KeyUsageFlags.CrlSign | X509KeyUsageFlags.KeyCertSign | X509KeyUsageFlags.KeyAgreement | X509KeyUsageFlags.DataEncipherment | X509KeyUsageFlags.KeyEncipherment | X509KeyUsageFlags.NonRepudiation | X509KeyUsageFlags.DigitalSignature | X509KeyUsageFlags.DecipherOnly;

		// Token: 0x04000D51 RID: 3409
		private X509KeyUsageFlags _keyUsages;

		// Token: 0x04000D52 RID: 3410
		private AsnDecodeStatus _status;
	}
}
