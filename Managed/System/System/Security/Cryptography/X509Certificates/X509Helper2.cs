using System;
using System.IO;
using Mono.Btls;
using Mono.Security.X509;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x020002E8 RID: 744
	internal static class X509Helper2
	{
		// Token: 0x06001799 RID: 6041 RVA: 0x0005D558 File Offset: 0x0005B758
		[MonoTODO("Investigate replacement; see comments in source.")]
		internal static X509Certificate GetMonoCertificate(X509Certificate2 certificate)
		{
			X509Certificate2ImplMono x509Certificate2ImplMono = certificate.Impl as X509Certificate2ImplMono;
			if (x509Certificate2ImplMono != null)
			{
				return x509Certificate2ImplMono.MonoCertificate;
			}
			return new X509Certificate(certificate.RawData);
		}

		// Token: 0x0600179A RID: 6042 RVA: 0x0005D586 File Offset: 0x0005B786
		internal static X509ChainImpl CreateChainImpl(bool useMachineContext)
		{
			return new X509ChainImplMono(useMachineContext);
		}

		// Token: 0x0600179B RID: 6043 RVA: 0x0005D58E File Offset: 0x0005B78E
		public static bool IsValid(X509ChainImpl impl)
		{
			return impl != null && impl.IsValid;
		}

		// Token: 0x0600179C RID: 6044 RVA: 0x0005D59B File Offset: 0x0005B79B
		internal static void ThrowIfContextInvalid(X509ChainImpl impl)
		{
			if (!X509Helper2.IsValid(impl))
			{
				throw X509Helper2.GetInvalidChainContextException();
			}
		}

		// Token: 0x0600179D RID: 6045 RVA: 0x0005D5AB File Offset: 0x0005B7AB
		internal static Exception GetInvalidChainContextException()
		{
			return new CryptographicException(global::Locale.GetText("Chain instance is empty."));
		}

		// Token: 0x0600179E RID: 6046 RVA: 0x0005D5BC File Offset: 0x0005B7BC
		[Obsolete("This is only used by Mono.Security's X509Store and will be replaced shortly.")]
		internal static long GetSubjectNameHash(X509Certificate certificate)
		{
			X509Helper.ThrowIfContextInvalid(certificate.Impl);
			long hash;
			using (MonoBtlsX509 nativeInstance = X509Helper2.GetNativeInstance(certificate.Impl))
			{
				using (MonoBtlsX509Name subjectName = nativeInstance.GetSubjectName())
				{
					hash = subjectName.GetHash();
				}
			}
			return hash;
		}

		// Token: 0x0600179F RID: 6047 RVA: 0x0005D624 File Offset: 0x0005B824
		[Obsolete("This is only used by Mono.Security's X509Store and will be replaced shortly.")]
		internal static void ExportAsPEM(X509Certificate certificate, Stream stream, bool includeHumanReadableForm)
		{
			X509Helper.ThrowIfContextInvalid(certificate.Impl);
			using (MonoBtlsX509 nativeInstance = X509Helper2.GetNativeInstance(certificate.Impl))
			{
				using (MonoBtlsBio monoBtlsBio = MonoBtlsBio.CreateMonoStream(stream))
				{
					nativeInstance.ExportAsPEM(monoBtlsBio, includeHumanReadableForm);
				}
			}
		}

		// Token: 0x060017A0 RID: 6048 RVA: 0x0005D68C File Offset: 0x0005B88C
		private static MonoBtlsX509 GetNativeInstance(X509CertificateImpl impl)
		{
			X509Helper.ThrowIfContextInvalid(impl);
			X509CertificateImplBtls x509CertificateImplBtls = impl as X509CertificateImplBtls;
			if (x509CertificateImplBtls != null)
			{
				return x509CertificateImplBtls.X509.Copy();
			}
			return MonoBtlsX509.LoadFromData(impl.RawData, MonoBtlsX509Format.DER);
		}
	}
}
