using System;
using System.Net.Security;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using Mono.Security.Interface;

namespace Mono.Net.Security
{
	// Token: 0x020000A5 RID: 165
	internal abstract class MonoSslAuthenticationOptions : IMonoAuthenticationOptions
	{
		// Token: 0x1700008E RID: 142
		// (get) Token: 0x060002EC RID: 748
		public abstract bool ServerMode { get; }

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x060002ED RID: 749
		// (set) Token: 0x060002EE RID: 750
		public abstract bool AllowRenegotiation { get; set; }

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x060002EF RID: 751
		// (set) Token: 0x060002F0 RID: 752
		public abstract RemoteCertificateValidationCallback RemoteCertificateValidationCallback { get; set; }

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x060002F1 RID: 753
		// (set) Token: 0x060002F2 RID: 754
		public abstract SslProtocols EnabledSslProtocols { get; set; }

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x060002F3 RID: 755
		// (set) Token: 0x060002F4 RID: 756
		public abstract EncryptionPolicy EncryptionPolicy { get; set; }

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x060002F5 RID: 757
		// (set) Token: 0x060002F6 RID: 758
		public abstract X509RevocationMode CertificateRevocationCheckMode { get; set; }

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x060002F7 RID: 759
		// (set) Token: 0x060002F8 RID: 760
		public abstract string TargetHost { get; set; }

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x060002F9 RID: 761
		// (set) Token: 0x060002FA RID: 762
		public abstract X509Certificate ServerCertificate { get; set; }

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x060002FB RID: 763
		// (set) Token: 0x060002FC RID: 764
		public abstract X509CertificateCollection ClientCertificates { get; set; }

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x060002FD RID: 765
		// (set) Token: 0x060002FE RID: 766
		public abstract bool ClientCertificateRequired { get; set; }

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x060002FF RID: 767 RVA: 0x000091B3 File Offset: 0x000073B3
		// (set) Token: 0x06000300 RID: 768 RVA: 0x000091BB File Offset: 0x000073BB
		internal ServerCertSelectionCallback ServerCertSelectionDelegate { get; set; }
	}
}
