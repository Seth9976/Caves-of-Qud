using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Internal.Cryptography.Pal;
using Microsoft.Win32.SafeHandles;
using Mono.Security.X509;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x020002D8 RID: 728
	internal abstract class X509Certificate2ImplUnix : X509Certificate2Impl
	{
		// Token: 0x060016BA RID: 5818 RVA: 0x0005AB8E File Offset: 0x00058D8E
		private void EnsureCertData()
		{
			if (this.readCertData)
			{
				return;
			}
			base.ThrowIfContextInvalid();
			this.certData = new CertificateData(this.GetRawCertData());
			this.readCertData = true;
		}

		// Token: 0x060016BB RID: 5819
		protected abstract byte[] GetRawCertData();

		// Token: 0x1700044E RID: 1102
		// (get) Token: 0x060016BC RID: 5820 RVA: 0x00003062 File Offset: 0x00001262
		// (set) Token: 0x060016BD RID: 5821 RVA: 0x0005ABB7 File Offset: 0x00058DB7
		public sealed override bool Archived
		{
			get
			{
				return false;
			}
			set
			{
				throw new PlatformNotSupportedException(SR.Format("The {0} value cannot be set on Unix.", "Archived"));
			}
		}

		// Token: 0x1700044F RID: 1103
		// (get) Token: 0x060016BE RID: 5822 RVA: 0x0005ABCD File Offset: 0x00058DCD
		public sealed override string KeyAlgorithm
		{
			get
			{
				this.EnsureCertData();
				return this.certData.PublicKeyAlgorithm.AlgorithmId;
			}
		}

		// Token: 0x17000450 RID: 1104
		// (get) Token: 0x060016BF RID: 5823 RVA: 0x0005ABE5 File Offset: 0x00058DE5
		public sealed override byte[] KeyAlgorithmParameters
		{
			get
			{
				this.EnsureCertData();
				return this.certData.PublicKeyAlgorithm.Parameters;
			}
		}

		// Token: 0x17000451 RID: 1105
		// (get) Token: 0x060016C0 RID: 5824 RVA: 0x0005ABFD File Offset: 0x00058DFD
		public sealed override byte[] PublicKeyValue
		{
			get
			{
				this.EnsureCertData();
				return this.certData.PublicKey;
			}
		}

		// Token: 0x17000452 RID: 1106
		// (get) Token: 0x060016C1 RID: 5825 RVA: 0x0005AC10 File Offset: 0x00058E10
		public sealed override byte[] SerialNumber
		{
			get
			{
				this.EnsureCertData();
				return this.certData.SerialNumber;
			}
		}

		// Token: 0x17000453 RID: 1107
		// (get) Token: 0x060016C2 RID: 5826 RVA: 0x0005AC23 File Offset: 0x00058E23
		public sealed override string SignatureAlgorithm
		{
			get
			{
				this.EnsureCertData();
				return this.certData.SignatureAlgorithm.AlgorithmId;
			}
		}

		// Token: 0x17000454 RID: 1108
		// (get) Token: 0x060016C3 RID: 5827 RVA: 0x00052517 File Offset: 0x00050717
		// (set) Token: 0x060016C4 RID: 5828 RVA: 0x0005AC3B File Offset: 0x00058E3B
		public sealed override string FriendlyName
		{
			get
			{
				return "";
			}
			set
			{
				throw new PlatformNotSupportedException(SR.Format("The {0} value cannot be set on Unix.", "FriendlyName"));
			}
		}

		// Token: 0x17000455 RID: 1109
		// (get) Token: 0x060016C5 RID: 5829 RVA: 0x0005AC51 File Offset: 0x00058E51
		public sealed override int Version
		{
			get
			{
				this.EnsureCertData();
				return this.certData.Version + 1;
			}
		}

		// Token: 0x17000456 RID: 1110
		// (get) Token: 0x060016C6 RID: 5830 RVA: 0x0005AC66 File Offset: 0x00058E66
		public sealed override X500DistinguishedName SubjectName
		{
			get
			{
				this.EnsureCertData();
				return this.certData.Subject;
			}
		}

		// Token: 0x17000457 RID: 1111
		// (get) Token: 0x060016C7 RID: 5831 RVA: 0x0005AC79 File Offset: 0x00058E79
		public sealed override X500DistinguishedName IssuerName
		{
			get
			{
				this.EnsureCertData();
				return this.certData.Issuer;
			}
		}

		// Token: 0x17000458 RID: 1112
		// (get) Token: 0x060016C8 RID: 5832 RVA: 0x0005AC8C File Offset: 0x00058E8C
		public sealed override string Subject
		{
			get
			{
				return this.SubjectName.Name;
			}
		}

		// Token: 0x17000459 RID: 1113
		// (get) Token: 0x060016C9 RID: 5833 RVA: 0x0005AC99 File Offset: 0x00058E99
		public sealed override string Issuer
		{
			get
			{
				return this.IssuerName.Name;
			}
		}

		// Token: 0x1700045A RID: 1114
		// (get) Token: 0x060016CA RID: 5834 RVA: 0x0005ACA6 File Offset: 0x00058EA6
		public sealed override string LegacySubject
		{
			get
			{
				return this.SubjectName.Decode(X500DistinguishedNameFlags.None);
			}
		}

		// Token: 0x1700045B RID: 1115
		// (get) Token: 0x060016CB RID: 5835 RVA: 0x0005ACB4 File Offset: 0x00058EB4
		public sealed override string LegacyIssuer
		{
			get
			{
				return this.IssuerName.Decode(X500DistinguishedNameFlags.None);
			}
		}

		// Token: 0x1700045C RID: 1116
		// (get) Token: 0x060016CC RID: 5836 RVA: 0x0005ACC2 File Offset: 0x00058EC2
		public sealed override byte[] RawData
		{
			get
			{
				this.EnsureCertData();
				return this.certData.RawData;
			}
		}

		// Token: 0x1700045D RID: 1117
		// (get) Token: 0x060016CD RID: 5837 RVA: 0x0005ACD8 File Offset: 0x00058ED8
		public sealed override byte[] Thumbprint
		{
			get
			{
				this.EnsureCertData();
				byte[] array;
				using (SHA1 sha = SHA1.Create())
				{
					array = sha.ComputeHash(this.certData.RawData);
				}
				return array;
			}
		}

		// Token: 0x060016CE RID: 5838 RVA: 0x0005AD20 File Offset: 0x00058F20
		public sealed override string GetNameInfo(X509NameType nameType, bool forIssuer)
		{
			this.EnsureCertData();
			return this.certData.GetNameInfo(nameType, forIssuer);
		}

		// Token: 0x1700045E RID: 1118
		// (get) Token: 0x060016CF RID: 5839 RVA: 0x0005AD35 File Offset: 0x00058F35
		public sealed override IEnumerable<X509Extension> Extensions
		{
			get
			{
				this.EnsureCertData();
				return this.certData.Extensions;
			}
		}

		// Token: 0x1700045F RID: 1119
		// (get) Token: 0x060016D0 RID: 5840 RVA: 0x0005AD48 File Offset: 0x00058F48
		public sealed override DateTime NotAfter
		{
			get
			{
				this.EnsureCertData();
				return this.certData.NotAfter.ToLocalTime();
			}
		}

		// Token: 0x17000460 RID: 1120
		// (get) Token: 0x060016D1 RID: 5841 RVA: 0x0005AD60 File Offset: 0x00058F60
		public sealed override DateTime NotBefore
		{
			get
			{
				this.EnsureCertData();
				return this.certData.NotBefore.ToLocalTime();
			}
		}

		// Token: 0x060016D2 RID: 5842 RVA: 0x0005AD78 File Offset: 0x00058F78
		public sealed override void AppendPrivateKeyInfo(StringBuilder sb)
		{
			if (!this.HasPrivateKey)
			{
				return;
			}
			sb.AppendLine();
			sb.AppendLine();
			sb.AppendLine("[Private Key]");
		}

		// Token: 0x060016D3 RID: 5843 RVA: 0x0005AD9D File Offset: 0x00058F9D
		public override void Reset()
		{
			this.readCertData = false;
		}

		// Token: 0x060016D4 RID: 5844 RVA: 0x0005ADA8 File Offset: 0x00058FA8
		public sealed override byte[] Export(X509ContentType contentType, SafePasswordHandle password)
		{
			base.ThrowIfContextInvalid();
			switch (contentType)
			{
			case X509ContentType.Cert:
				return this.RawData;
			case X509ContentType.SerializedCert:
			case X509ContentType.SerializedStore:
				throw new PlatformNotSupportedException("X509ContentType.SerializedCert and X509ContentType.SerializedStore are not supported on Unix.");
			case X509ContentType.Pfx:
				return this.ExportPkcs12(password);
			case X509ContentType.Pkcs7:
				return this.ExportPkcs12(null);
			default:
				throw new CryptographicException("Invalid content type.");
			}
		}

		// Token: 0x060016D5 RID: 5845 RVA: 0x0005AE08 File Offset: 0x00059008
		private byte[] ExportPkcs12(SafePasswordHandle password)
		{
			if (password == null || password.IsInvalid)
			{
				return this.ExportPkcs12(null);
			}
			string text = password.Mono_DangerousGetString();
			return this.ExportPkcs12(text);
		}

		// Token: 0x060016D6 RID: 5846 RVA: 0x0005AE38 File Offset: 0x00059038
		private byte[] ExportPkcs12(string password)
		{
			PKCS12 pkcs = new PKCS12();
			byte[] bytes;
			try
			{
				Hashtable hashtable = new Hashtable();
				ArrayList arrayList = new ArrayList();
				ArrayList arrayList2 = arrayList;
				byte[] array = new byte[4];
				array[0] = 1;
				arrayList2.Add(array);
				hashtable.Add("1.2.840.113549.1.9.21", arrayList);
				if (password != null)
				{
					pkcs.Password = password;
				}
				pkcs.AddCertificate(new X509Certificate(this.RawData), hashtable);
				if (this.IntermediateCertificates != null)
				{
					for (int i = 0; i < this.IntermediateCertificates.Count; i++)
					{
						pkcs.AddCertificate(new X509Certificate(this.IntermediateCertificates[i].RawData));
					}
				}
				AsymmetricAlgorithm privateKey = this.PrivateKey;
				if (privateKey != null)
				{
					pkcs.AddPkcs8ShroudedKeyBag(privateKey, hashtable);
				}
				bytes = pkcs.GetBytes();
			}
			finally
			{
				pkcs.Password = null;
			}
			return bytes;
		}

		// Token: 0x04000D20 RID: 3360
		private bool readCertData;

		// Token: 0x04000D21 RID: 3361
		private CertificateData certData;
	}
}
