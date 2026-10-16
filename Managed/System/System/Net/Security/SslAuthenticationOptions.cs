using System;
using System.Collections.Generic;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;

namespace System.Net.Security
{
	// Token: 0x02000856 RID: 2134
	internal class SslAuthenticationOptions
	{
		// Token: 0x060043B6 RID: 17334 RVA: 0x000EC380 File Offset: 0x000EA580
		internal SslAuthenticationOptions(SslClientAuthenticationOptions sslClientAuthenticationOptions, RemoteCertValidationCallback remoteCallback, LocalCertSelectionCallback localCallback)
		{
			this.AllowRenegotiation = sslClientAuthenticationOptions.AllowRenegotiation;
			this.ApplicationProtocols = sslClientAuthenticationOptions.ApplicationProtocols;
			this.CertValidationDelegate = remoteCallback;
			this.CheckCertName = true;
			this.EnabledSslProtocols = sslClientAuthenticationOptions.EnabledSslProtocols;
			this.EncryptionPolicy = sslClientAuthenticationOptions.EncryptionPolicy;
			this.IsServer = false;
			this.RemoteCertRequired = true;
			this.RemoteCertificateValidationCallback = sslClientAuthenticationOptions.RemoteCertificateValidationCallback;
			this.TargetHost = sslClientAuthenticationOptions.TargetHost;
			this.CertSelectionDelegate = localCallback;
			this.CertificateRevocationCheckMode = sslClientAuthenticationOptions.CertificateRevocationCheckMode;
			this.ClientCertificates = sslClientAuthenticationOptions.ClientCertificates;
			this.LocalCertificateSelectionCallback = sslClientAuthenticationOptions.LocalCertificateSelectionCallback;
		}

		// Token: 0x060043B7 RID: 17335 RVA: 0x000EC424 File Offset: 0x000EA624
		internal SslAuthenticationOptions(SslServerAuthenticationOptions sslServerAuthenticationOptions)
		{
			this.AllowRenegotiation = sslServerAuthenticationOptions.AllowRenegotiation;
			this.ApplicationProtocols = sslServerAuthenticationOptions.ApplicationProtocols;
			this.CheckCertName = false;
			this.EnabledSslProtocols = sslServerAuthenticationOptions.EnabledSslProtocols;
			this.EncryptionPolicy = sslServerAuthenticationOptions.EncryptionPolicy;
			this.IsServer = true;
			this.RemoteCertRequired = sslServerAuthenticationOptions.ClientCertificateRequired;
			this.RemoteCertificateValidationCallback = sslServerAuthenticationOptions.RemoteCertificateValidationCallback;
			this.TargetHost = string.Empty;
			this.CertificateRevocationCheckMode = sslServerAuthenticationOptions.CertificateRevocationCheckMode;
			this.ServerCertificate = sslServerAuthenticationOptions.ServerCertificate;
		}

		// Token: 0x17000F29 RID: 3881
		// (get) Token: 0x060043B8 RID: 17336 RVA: 0x000EC4B0 File Offset: 0x000EA6B0
		// (set) Token: 0x060043B9 RID: 17337 RVA: 0x000EC4B8 File Offset: 0x000EA6B8
		internal bool AllowRenegotiation { get; set; }

		// Token: 0x17000F2A RID: 3882
		// (get) Token: 0x060043BA RID: 17338 RVA: 0x000EC4C1 File Offset: 0x000EA6C1
		// (set) Token: 0x060043BB RID: 17339 RVA: 0x000EC4C9 File Offset: 0x000EA6C9
		internal string TargetHost { get; set; }

		// Token: 0x17000F2B RID: 3883
		// (get) Token: 0x060043BC RID: 17340 RVA: 0x000EC4D2 File Offset: 0x000EA6D2
		// (set) Token: 0x060043BD RID: 17341 RVA: 0x000EC4DA File Offset: 0x000EA6DA
		internal X509CertificateCollection ClientCertificates { get; set; }

		// Token: 0x17000F2C RID: 3884
		// (get) Token: 0x060043BE RID: 17342 RVA: 0x000EC4E3 File Offset: 0x000EA6E3
		internal List<SslApplicationProtocol> ApplicationProtocols { get; }

		// Token: 0x17000F2D RID: 3885
		// (get) Token: 0x060043BF RID: 17343 RVA: 0x000EC4EB File Offset: 0x000EA6EB
		// (set) Token: 0x060043C0 RID: 17344 RVA: 0x000EC4F3 File Offset: 0x000EA6F3
		internal bool IsServer { get; set; }

		// Token: 0x17000F2E RID: 3886
		// (get) Token: 0x060043C1 RID: 17345 RVA: 0x000EC4FC File Offset: 0x000EA6FC
		// (set) Token: 0x060043C2 RID: 17346 RVA: 0x000EC504 File Offset: 0x000EA704
		internal RemoteCertificateValidationCallback RemoteCertificateValidationCallback { get; set; }

		// Token: 0x17000F2F RID: 3887
		// (get) Token: 0x060043C3 RID: 17347 RVA: 0x000EC50D File Offset: 0x000EA70D
		// (set) Token: 0x060043C4 RID: 17348 RVA: 0x000EC515 File Offset: 0x000EA715
		internal LocalCertificateSelectionCallback LocalCertificateSelectionCallback { get; set; }

		// Token: 0x17000F30 RID: 3888
		// (get) Token: 0x060043C5 RID: 17349 RVA: 0x000EC51E File Offset: 0x000EA71E
		// (set) Token: 0x060043C6 RID: 17350 RVA: 0x000EC526 File Offset: 0x000EA726
		internal X509Certificate ServerCertificate { get; set; }

		// Token: 0x17000F31 RID: 3889
		// (get) Token: 0x060043C7 RID: 17351 RVA: 0x000EC52F File Offset: 0x000EA72F
		// (set) Token: 0x060043C8 RID: 17352 RVA: 0x000EC537 File Offset: 0x000EA737
		internal SslProtocols EnabledSslProtocols { get; set; }

		// Token: 0x17000F32 RID: 3890
		// (get) Token: 0x060043C9 RID: 17353 RVA: 0x000EC540 File Offset: 0x000EA740
		// (set) Token: 0x060043CA RID: 17354 RVA: 0x000EC548 File Offset: 0x000EA748
		internal X509RevocationMode CertificateRevocationCheckMode { get; set; }

		// Token: 0x17000F33 RID: 3891
		// (get) Token: 0x060043CB RID: 17355 RVA: 0x000EC551 File Offset: 0x000EA751
		// (set) Token: 0x060043CC RID: 17356 RVA: 0x000EC559 File Offset: 0x000EA759
		internal EncryptionPolicy EncryptionPolicy { get; set; }

		// Token: 0x17000F34 RID: 3892
		// (get) Token: 0x060043CD RID: 17357 RVA: 0x000EC562 File Offset: 0x000EA762
		// (set) Token: 0x060043CE RID: 17358 RVA: 0x000EC56A File Offset: 0x000EA76A
		internal bool RemoteCertRequired { get; set; }

		// Token: 0x17000F35 RID: 3893
		// (get) Token: 0x060043CF RID: 17359 RVA: 0x000EC573 File Offset: 0x000EA773
		// (set) Token: 0x060043D0 RID: 17360 RVA: 0x000EC57B File Offset: 0x000EA77B
		internal bool CheckCertName { get; set; }

		// Token: 0x17000F36 RID: 3894
		// (get) Token: 0x060043D1 RID: 17361 RVA: 0x000EC584 File Offset: 0x000EA784
		// (set) Token: 0x060043D2 RID: 17362 RVA: 0x000EC58C File Offset: 0x000EA78C
		internal RemoteCertValidationCallback CertValidationDelegate { get; set; }

		// Token: 0x17000F37 RID: 3895
		// (get) Token: 0x060043D3 RID: 17363 RVA: 0x000EC595 File Offset: 0x000EA795
		// (set) Token: 0x060043D4 RID: 17364 RVA: 0x000EC59D File Offset: 0x000EA79D
		internal LocalCertSelectionCallback CertSelectionDelegate { get; set; }

		// Token: 0x17000F38 RID: 3896
		// (get) Token: 0x060043D5 RID: 17365 RVA: 0x000EC5A6 File Offset: 0x000EA7A6
		// (set) Token: 0x060043D6 RID: 17366 RVA: 0x000EC5AE File Offset: 0x000EA7AE
		internal ServerCertSelectionCallback ServerCertSelectionDelegate { get; set; }
	}
}
