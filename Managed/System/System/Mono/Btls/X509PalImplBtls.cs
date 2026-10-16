using System;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Win32.SafeHandles;
using Mono.Security.Interface;

namespace Mono.Btls
{
	// Token: 0x02000117 RID: 279
	internal class X509PalImplBtls : X509PalImpl
	{
		// Token: 0x060006AD RID: 1709 RVA: 0x00012538 File Offset: 0x00010738
		public X509PalImplBtls(MonoTlsProvider provider)
		{
			this.Provider = (MonoBtlsProvider)provider;
		}

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x060006AE RID: 1710 RVA: 0x0001254C File Offset: 0x0001074C
		private MonoBtlsProvider Provider { get; }

		// Token: 0x060006AF RID: 1711 RVA: 0x00012554 File Offset: 0x00010754
		public override X509CertificateImpl Import(byte[] data)
		{
			return this.Provider.GetNativeCertificate(data, null, X509KeyStorageFlags.DefaultKeySet);
		}

		// Token: 0x060006B0 RID: 1712 RVA: 0x00012564 File Offset: 0x00010764
		public override X509Certificate2Impl Import(byte[] data, SafePasswordHandle password, X509KeyStorageFlags keyStorageFlags)
		{
			return this.Provider.GetNativeCertificate(data, password, keyStorageFlags);
		}

		// Token: 0x060006B1 RID: 1713 RVA: 0x00012574 File Offset: 0x00010774
		public override X509Certificate2Impl Import(X509Certificate cert)
		{
			return this.Provider.GetNativeCertificate(cert);
		}
	}
}
