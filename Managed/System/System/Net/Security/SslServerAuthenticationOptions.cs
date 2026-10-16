using System;
using System.Collections.Generic;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;

namespace System.Net.Security
{
	// Token: 0x02000858 RID: 2136
	public class SslServerAuthenticationOptions
	{
		// Token: 0x17000F42 RID: 3906
		// (get) Token: 0x060043EA RID: 17386 RVA: 0x000EC6A9 File Offset: 0x000EA8A9
		// (set) Token: 0x060043EB RID: 17387 RVA: 0x000EC6B1 File Offset: 0x000EA8B1
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

		// Token: 0x17000F43 RID: 3907
		// (get) Token: 0x060043EC RID: 17388 RVA: 0x000EC6BA File Offset: 0x000EA8BA
		// (set) Token: 0x060043ED RID: 17389 RVA: 0x000EC6C2 File Offset: 0x000EA8C2
		public bool ClientCertificateRequired { get; set; }

		// Token: 0x17000F44 RID: 3908
		// (get) Token: 0x060043EE RID: 17390 RVA: 0x000EC6CB File Offset: 0x000EA8CB
		// (set) Token: 0x060043EF RID: 17391 RVA: 0x000EC6D3 File Offset: 0x000EA8D3
		public List<SslApplicationProtocol> ApplicationProtocols { get; set; }

		// Token: 0x17000F45 RID: 3909
		// (get) Token: 0x060043F0 RID: 17392 RVA: 0x000EC6DC File Offset: 0x000EA8DC
		// (set) Token: 0x060043F1 RID: 17393 RVA: 0x000EC6E4 File Offset: 0x000EA8E4
		public RemoteCertificateValidationCallback RemoteCertificateValidationCallback { get; set; }

		// Token: 0x17000F46 RID: 3910
		// (get) Token: 0x060043F2 RID: 17394 RVA: 0x000EC6ED File Offset: 0x000EA8ED
		// (set) Token: 0x060043F3 RID: 17395 RVA: 0x000EC6F5 File Offset: 0x000EA8F5
		public ServerCertificateSelectionCallback ServerCertificateSelectionCallback { get; set; }

		// Token: 0x17000F47 RID: 3911
		// (get) Token: 0x060043F4 RID: 17396 RVA: 0x000EC6FE File Offset: 0x000EA8FE
		// (set) Token: 0x060043F5 RID: 17397 RVA: 0x000EC706 File Offset: 0x000EA906
		public X509Certificate ServerCertificate { get; set; }

		// Token: 0x17000F48 RID: 3912
		// (get) Token: 0x060043F6 RID: 17398 RVA: 0x000EC70F File Offset: 0x000EA90F
		// (set) Token: 0x060043F7 RID: 17399 RVA: 0x000EC717 File Offset: 0x000EA917
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

		// Token: 0x17000F49 RID: 3913
		// (get) Token: 0x060043F8 RID: 17400 RVA: 0x000EC720 File Offset: 0x000EA920
		// (set) Token: 0x060043F9 RID: 17401 RVA: 0x000EC728 File Offset: 0x000EA928
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

		// Token: 0x17000F4A RID: 3914
		// (get) Token: 0x060043FA RID: 17402 RVA: 0x000EC756 File Offset: 0x000EA956
		// (set) Token: 0x060043FB RID: 17403 RVA: 0x000EC75E File Offset: 0x000EA95E
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

		// Token: 0x04002929 RID: 10537
		private X509RevocationMode _checkCertificateRevocation;

		// Token: 0x0400292A RID: 10538
		private SslProtocols _enabledSslProtocols;

		// Token: 0x0400292B RID: 10539
		private EncryptionPolicy _encryptionPolicy;

		// Token: 0x0400292C RID: 10540
		private bool _allowRenegotiation = true;
	}
}
