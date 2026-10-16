using System;
using System.Collections.Generic;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;

namespace System.Net.Security
{
	// Token: 0x02000857 RID: 2135
	public class SslClientAuthenticationOptions
	{
		// Token: 0x17000F39 RID: 3897
		// (get) Token: 0x060043D7 RID: 17367 RVA: 0x000EC5B7 File Offset: 0x000EA7B7
		// (set) Token: 0x060043D8 RID: 17368 RVA: 0x000EC5BF File Offset: 0x000EA7BF
		public bool AllowRenegotiation
		{
			get
			{
				return this._allowRenegotiation;
			}
			set
			{
				this._allowRenegotiation = value;
			}
		}

		// Token: 0x17000F3A RID: 3898
		// (get) Token: 0x060043D9 RID: 17369 RVA: 0x000EC5C8 File Offset: 0x000EA7C8
		// (set) Token: 0x060043DA RID: 17370 RVA: 0x000EC5D0 File Offset: 0x000EA7D0
		public LocalCertificateSelectionCallback LocalCertificateSelectionCallback { get; set; }

		// Token: 0x17000F3B RID: 3899
		// (get) Token: 0x060043DB RID: 17371 RVA: 0x000EC5D9 File Offset: 0x000EA7D9
		// (set) Token: 0x060043DC RID: 17372 RVA: 0x000EC5E1 File Offset: 0x000EA7E1
		public RemoteCertificateValidationCallback RemoteCertificateValidationCallback { get; set; }

		// Token: 0x17000F3C RID: 3900
		// (get) Token: 0x060043DD RID: 17373 RVA: 0x000EC5EA File Offset: 0x000EA7EA
		// (set) Token: 0x060043DE RID: 17374 RVA: 0x000EC5F2 File Offset: 0x000EA7F2
		public List<SslApplicationProtocol> ApplicationProtocols { get; set; }

		// Token: 0x17000F3D RID: 3901
		// (get) Token: 0x060043DF RID: 17375 RVA: 0x000EC5FB File Offset: 0x000EA7FB
		// (set) Token: 0x060043E0 RID: 17376 RVA: 0x000EC603 File Offset: 0x000EA803
		public string TargetHost { get; set; }

		// Token: 0x17000F3E RID: 3902
		// (get) Token: 0x060043E1 RID: 17377 RVA: 0x000EC60C File Offset: 0x000EA80C
		// (set) Token: 0x060043E2 RID: 17378 RVA: 0x000EC614 File Offset: 0x000EA814
		public X509CertificateCollection ClientCertificates { get; set; }

		// Token: 0x17000F3F RID: 3903
		// (get) Token: 0x060043E3 RID: 17379 RVA: 0x000EC61D File Offset: 0x000EA81D
		// (set) Token: 0x060043E4 RID: 17380 RVA: 0x000EC625 File Offset: 0x000EA825
		public X509RevocationMode CertificateRevocationCheckMode
		{
			get
			{
				return this._checkCertificateRevocation;
			}
			set
			{
				if (value != X509RevocationMode.NoCheck && value != X509RevocationMode.Offline && value != X509RevocationMode.Online)
				{
					throw new ArgumentException(SR.Format("The specified value is not valid in the '{0}' enumeration.", "X509RevocationMode"), "value");
				}
				this._checkCertificateRevocation = value;
			}
		}

		// Token: 0x17000F40 RID: 3904
		// (get) Token: 0x060043E5 RID: 17381 RVA: 0x000EC653 File Offset: 0x000EA853
		// (set) Token: 0x060043E6 RID: 17382 RVA: 0x000EC65B File Offset: 0x000EA85B
		public EncryptionPolicy EncryptionPolicy
		{
			get
			{
				return this._encryptionPolicy;
			}
			set
			{
				if (value != EncryptionPolicy.RequireEncryption && value != EncryptionPolicy.AllowNoEncryption && value != EncryptionPolicy.NoEncryption)
				{
					throw new ArgumentException(SR.Format("The specified value is not valid in the '{0}' enumeration.", "EncryptionPolicy"), "value");
				}
				this._encryptionPolicy = value;
			}
		}

		// Token: 0x17000F41 RID: 3905
		// (get) Token: 0x060043E7 RID: 17383 RVA: 0x000EC689 File Offset: 0x000EA889
		// (set) Token: 0x060043E8 RID: 17384 RVA: 0x000EC691 File Offset: 0x000EA891
		public SslProtocols EnabledSslProtocols
		{
			get
			{
				return this._enabledSslProtocols;
			}
			set
			{
				this._enabledSslProtocols = value;
			}
		}

		// Token: 0x04002920 RID: 10528
		private EncryptionPolicy _encryptionPolicy;

		// Token: 0x04002921 RID: 10529
		private X509RevocationMode _checkCertificateRevocation;

		// Token: 0x04002922 RID: 10530
		private SslProtocols _enabledSslProtocols;

		// Token: 0x04002923 RID: 10531
		private bool _allowRenegotiation = true;
	}
}
