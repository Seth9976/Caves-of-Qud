using System;
using System.Net.Security;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using Mono.Security.Interface;

namespace Mono.Net.Security
{
	// Token: 0x020000A6 RID: 166
	internal sealed class MonoSslClientAuthenticationOptions : MonoSslAuthenticationOptions, IMonoSslClientAuthenticationOptions, IMonoAuthenticationOptions
	{
		// Token: 0x17000099 RID: 153
		// (get) Token: 0x06000302 RID: 770 RVA: 0x000091C4 File Offset: 0x000073C4
		public SslClientAuthenticationOptions Options { get; }

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x06000303 RID: 771 RVA: 0x00003062 File Offset: 0x00001262
		public override bool ServerMode
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000304 RID: 772 RVA: 0x000091CC File Offset: 0x000073CC
		public MonoSslClientAuthenticationOptions(SslClientAuthenticationOptions options)
		{
			this.Options = options;
		}

		// Token: 0x06000305 RID: 773 RVA: 0x000091DB File Offset: 0x000073DB
		public MonoSslClientAuthenticationOptions()
		{
			this.Options = new SslClientAuthenticationOptions();
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x06000306 RID: 774 RVA: 0x000091EE File Offset: 0x000073EE
		// (set) Token: 0x06000307 RID: 775 RVA: 0x000091FB File Offset: 0x000073FB
		public override bool AllowRenegotiation
		{
			get
			{
				return this.Options.AllowRenegotiation;
			}
			set
			{
				this.Options.AllowRenegotiation = value;
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x06000308 RID: 776 RVA: 0x00009209 File Offset: 0x00007409
		// (set) Token: 0x06000309 RID: 777 RVA: 0x00009216 File Offset: 0x00007416
		public override RemoteCertificateValidationCallback RemoteCertificateValidationCallback
		{
			get
			{
				return this.Options.RemoteCertificateValidationCallback;
			}
			set
			{
				this.Options.RemoteCertificateValidationCallback = value;
			}
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x0600030A RID: 778 RVA: 0x00009224 File Offset: 0x00007424
		// (set) Token: 0x0600030B RID: 779 RVA: 0x00009231 File Offset: 0x00007431
		public override X509RevocationMode CertificateRevocationCheckMode
		{
			get
			{
				return this.Options.CertificateRevocationCheckMode;
			}
			set
			{
				this.Options.CertificateRevocationCheckMode = value;
			}
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x0600030C RID: 780 RVA: 0x0000923F File Offset: 0x0000743F
		// (set) Token: 0x0600030D RID: 781 RVA: 0x0000924C File Offset: 0x0000744C
		public override EncryptionPolicy EncryptionPolicy
		{
			get
			{
				return this.Options.EncryptionPolicy;
			}
			set
			{
				this.Options.EncryptionPolicy = value;
			}
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x0600030E RID: 782 RVA: 0x0000925A File Offset: 0x0000745A
		// (set) Token: 0x0600030F RID: 783 RVA: 0x00009267 File Offset: 0x00007467
		public override SslProtocols EnabledSslProtocols
		{
			get
			{
				return this.Options.EnabledSslProtocols;
			}
			set
			{
				this.Options.EnabledSslProtocols = value;
			}
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x06000310 RID: 784 RVA: 0x00009275 File Offset: 0x00007475
		// (set) Token: 0x06000311 RID: 785 RVA: 0x00009282 File Offset: 0x00007482
		public LocalCertificateSelectionCallback LocalCertificateSelectionCallback
		{
			get
			{
				return this.Options.LocalCertificateSelectionCallback;
			}
			set
			{
				this.Options.LocalCertificateSelectionCallback = value;
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x06000312 RID: 786 RVA: 0x00009290 File Offset: 0x00007490
		// (set) Token: 0x06000313 RID: 787 RVA: 0x0000929D File Offset: 0x0000749D
		public override string TargetHost
		{
			get
			{
				return this.Options.TargetHost;
			}
			set
			{
				this.Options.TargetHost = value;
			}
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x06000314 RID: 788 RVA: 0x000044FA File Offset: 0x000026FA
		// (set) Token: 0x06000315 RID: 789 RVA: 0x000044FA File Offset: 0x000026FA
		public override bool ClientCertificateRequired
		{
			get
			{
				throw new NotSupportedException();
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x06000316 RID: 790 RVA: 0x000092AB File Offset: 0x000074AB
		// (set) Token: 0x06000317 RID: 791 RVA: 0x000092B8 File Offset: 0x000074B8
		public override X509CertificateCollection ClientCertificates
		{
			get
			{
				return this.Options.ClientCertificates;
			}
			set
			{
				this.Options.ClientCertificates = value;
			}
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x06000318 RID: 792 RVA: 0x000044FA File Offset: 0x000026FA
		// (set) Token: 0x06000319 RID: 793 RVA: 0x000044FA File Offset: 0x000026FA
		public override X509Certificate ServerCertificate
		{
			get
			{
				throw new NotSupportedException();
			}
			set
			{
				throw new NotSupportedException();
			}
		}
	}
}
