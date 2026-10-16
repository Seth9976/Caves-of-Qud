using System;

namespace System.Net.Security
{
	// Token: 0x02000854 RID: 2132
	internal static class SslClientAuthenticationOptionsExtensions
	{
		// Token: 0x060043A9 RID: 17321 RVA: 0x000EC028 File Offset: 0x000EA228
		public static SslClientAuthenticationOptions ShallowClone(this SslClientAuthenticationOptions options)
		{
			return new SslClientAuthenticationOptions
			{
				AllowRenegotiation = options.AllowRenegotiation,
				ApplicationProtocols = options.ApplicationProtocols,
				CertificateRevocationCheckMode = options.CertificateRevocationCheckMode,
				ClientCertificates = options.ClientCertificates,
				EnabledSslProtocols = options.EnabledSslProtocols,
				EncryptionPolicy = options.EncryptionPolicy,
				LocalCertificateSelectionCallback = options.LocalCertificateSelectionCallback,
				RemoteCertificateValidationCallback = options.RemoteCertificateValidationCallback,
				TargetHost = options.TargetHost
			};
		}
	}
}
