using System;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace System.Net.Security
{
	// Token: 0x02000843 RID: 2115
	internal static class CertificateHelper
	{
		// Token: 0x06004362 RID: 17250 RVA: 0x000EA560 File Offset: 0x000E8760
		internal static X509Certificate2 GetEligibleClientCertificate(X509CertificateCollection candidateCerts)
		{
			if (candidateCerts.Count == 0)
			{
				return null;
			}
			X509Certificate2Collection x509Certificate2Collection = new X509Certificate2Collection();
			x509Certificate2Collection.AddRange(candidateCerts);
			return CertificateHelper.GetEligibleClientCertificate(x509Certificate2Collection);
		}

		// Token: 0x06004363 RID: 17251 RVA: 0x000EA580 File Offset: 0x000E8780
		internal static X509Certificate2 GetEligibleClientCertificate(X509Certificate2Collection candidateCerts)
		{
			if (candidateCerts.Count == 0)
			{
				return null;
			}
			foreach (X509Certificate2 x509Certificate in candidateCerts)
			{
				if (x509Certificate.HasPrivateKey && CertificateHelper.IsValidClientCertificate(x509Certificate))
				{
					return x509Certificate;
				}
			}
			return null;
		}

		// Token: 0x06004364 RID: 17252 RVA: 0x000EA5C4 File Offset: 0x000E87C4
		private static bool IsValidClientCertificate(X509Certificate2 cert)
		{
			foreach (X509Extension x509Extension in cert.Extensions)
			{
				X509EnhancedKeyUsageExtension x509EnhancedKeyUsageExtension = x509Extension as X509EnhancedKeyUsageExtension;
				if (x509EnhancedKeyUsageExtension != null && !CertificateHelper.IsValidForClientAuthenticationEKU(x509EnhancedKeyUsageExtension))
				{
					return false;
				}
				X509KeyUsageExtension x509KeyUsageExtension = x509Extension as X509KeyUsageExtension;
				if (x509KeyUsageExtension != null && !CertificateHelper.IsValidForDigitalSignatureUsage(x509KeyUsageExtension))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06004365 RID: 17253 RVA: 0x000EA618 File Offset: 0x000E8818
		private static bool IsValidForClientAuthenticationEKU(X509EnhancedKeyUsageExtension eku)
		{
			OidEnumerator enumerator = eku.EnhancedKeyUsages.GetEnumerator();
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.Value == "1.3.6.1.5.5.7.3.2")
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06004366 RID: 17254 RVA: 0x000EA655 File Offset: 0x000E8855
		private static bool IsValidForDigitalSignatureUsage(X509KeyUsageExtension ku)
		{
			return (ku.KeyUsages & X509KeyUsageFlags.DigitalSignature) == X509KeyUsageFlags.DigitalSignature;
		}

		// Token: 0x06004367 RID: 17255 RVA: 0x000EA66C File Offset: 0x000E886C
		internal static X509Certificate2 GetEligibleClientCertificate()
		{
			X509Certificate2Collection certificates;
			using (X509Store x509Store = new X509Store(StoreName.My, StoreLocation.CurrentUser))
			{
				x509Store.Open(OpenFlags.OpenExistingOnly);
				certificates = x509Store.Certificates;
			}
			return CertificateHelper.GetEligibleClientCertificate(certificates);
		}

		// Token: 0x040028E8 RID: 10472
		private const string ClientAuthenticationOID = "1.3.6.1.5.5.7.3.2";
	}
}
