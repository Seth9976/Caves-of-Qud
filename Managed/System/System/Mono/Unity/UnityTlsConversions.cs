using System;
using System.Net.Security;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using Mono.Security.Interface;

namespace Mono.Unity
{
	// Token: 0x0200007B RID: 123
	internal static class UnityTlsConversions
	{
		// Token: 0x060001BD RID: 445 RVA: 0x00004CAC File Offset: 0x00002EAC
		public static UnityTls.unitytls_protocol GetMinProtocol(SslProtocols protocols)
		{
			if (protocols.HasFlag(SslProtocols.Tls))
			{
				return UnityTls.unitytls_protocol.UNITYTLS_PROTOCOL_TLS_1_0;
			}
			if (protocols.HasFlag(SslProtocols.Tls11))
			{
				return UnityTls.unitytls_protocol.UNITYTLS_PROTOCOL_TLS_1_1;
			}
			if (protocols.HasFlag(SslProtocols.Tls12))
			{
				return UnityTls.unitytls_protocol.UNITYTLS_PROTOCOL_TLS_1_2;
			}
			return UnityTls.unitytls_protocol.UNITYTLS_PROTOCOL_TLS_1_0;
		}

		// Token: 0x060001BE RID: 446 RVA: 0x00004D08 File Offset: 0x00002F08
		public static UnityTls.unitytls_protocol GetMaxProtocol(SslProtocols protocols)
		{
			if (protocols.HasFlag(SslProtocols.Tls12))
			{
				return UnityTls.unitytls_protocol.UNITYTLS_PROTOCOL_TLS_1_2;
			}
			if (protocols.HasFlag(SslProtocols.Tls11))
			{
				return UnityTls.unitytls_protocol.UNITYTLS_PROTOCOL_TLS_1_1;
			}
			if (protocols.HasFlag(SslProtocols.Tls))
			{
				return UnityTls.unitytls_protocol.UNITYTLS_PROTOCOL_TLS_1_0;
			}
			return UnityTls.unitytls_protocol.UNITYTLS_PROTOCOL_TLS_1_2;
		}

		// Token: 0x060001BF RID: 447 RVA: 0x00004D61 File Offset: 0x00002F61
		public static TlsProtocols ConvertProtocolVersion(UnityTls.unitytls_protocol protocol)
		{
			switch (protocol)
			{
			case UnityTls.unitytls_protocol.UNITYTLS_PROTOCOL_TLS_1_0:
				return TlsProtocols.Tls10;
			case UnityTls.unitytls_protocol.UNITYTLS_PROTOCOL_TLS_1_1:
				return TlsProtocols.Tls11;
			case UnityTls.unitytls_protocol.UNITYTLS_PROTOCOL_TLS_1_2:
				return TlsProtocols.Tls12;
			case UnityTls.unitytls_protocol.UNITYTLS_PROTOCOL_INVALID:
				return TlsProtocols.Zero;
			default:
				return TlsProtocols.Zero;
			}
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x00004D90 File Offset: 0x00002F90
		public static AlertDescription VerifyResultToAlertDescription(UnityTls.unitytls_x509verify_result verifyResult, AlertDescription defaultAlert = AlertDescription.InternalError)
		{
			if (verifyResult == (UnityTls.unitytls_x509verify_result)4294967295U)
			{
				return AlertDescription.CertificateUnknown;
			}
			if (verifyResult.HasFlag(UnityTls.unitytls_x509verify_result.UNITYTLS_X509VERIFY_FLAG_EXPIRED))
			{
				return AlertDescription.CertificateExpired;
			}
			if (verifyResult.HasFlag(UnityTls.unitytls_x509verify_result.UNITYTLS_X509VERIFY_FLAG_REVOKED))
			{
				return AlertDescription.CertificateRevoked;
			}
			if (verifyResult.HasFlag(UnityTls.unitytls_x509verify_result.UNITYTLS_X509VERIFY_FLAG_CN_MISMATCH))
			{
				return AlertDescription.UnknownCA;
			}
			if (verifyResult.HasFlag(UnityTls.unitytls_x509verify_result.UNITYTLS_X509VERIFY_FLAG_NOT_TRUSTED))
			{
				return AlertDescription.CertificateUnknown;
			}
			if (verifyResult.HasFlag(UnityTls.unitytls_x509verify_result.UNITYTLS_X509VERIFY_FLAG_BADCERT_BAD_KEY))
			{
				return AlertDescription.UserCancelled;
			}
			if (verifyResult.HasFlag(UnityTls.unitytls_x509verify_result.UNITYTLS_X509VERIFY_FLAG_BADCRL_BAD_MD))
			{
				return AlertDescription.UserCancelled;
			}
			if (verifyResult.HasFlag(UnityTls.unitytls_x509verify_result.UNITYTLS_X509VERIFY_FLAG_BADCRL_BAD_MD))
			{
				return AlertDescription.UserCancelled;
			}
			if (verifyResult.HasFlag(UnityTls.unitytls_x509verify_result.UNITYTLS_X509VERIFY_FLAG_BADCRL_BAD_PK))
			{
				return AlertDescription.UserCancelled;
			}
			if (verifyResult.HasFlag(UnityTls.unitytls_x509verify_result.UNITYTLS_X509VERIFY_FLAG_BADCRL_BAD_KEY))
			{
				return AlertDescription.UserCancelled;
			}
			if (verifyResult.HasFlag(UnityTls.unitytls_x509verify_result.UNITYTLS_X509VERIFY_FLAG_USER_ERROR5))
			{
				return AlertDescription.UserCancelled;
			}
			if (verifyResult.HasFlag(UnityTls.unitytls_x509verify_result.UNITYTLS_X509VERIFY_FLAG_USER_ERROR6))
			{
				return AlertDescription.UserCancelled;
			}
			if (verifyResult.HasFlag(UnityTls.unitytls_x509verify_result.UNITYTLS_X509VERIFY_FLAG_USER_ERROR7))
			{
				return AlertDescription.UserCancelled;
			}
			if (verifyResult.HasFlag(UnityTls.unitytls_x509verify_result.UNITYTLS_X509VERIFY_FLAG_USER_ERROR8))
			{
				return AlertDescription.UserCancelled;
			}
			return defaultAlert;
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x00004EE8 File Offset: 0x000030E8
		public static SslPolicyErrors VerifyResultToPolicyErrror(UnityTls.unitytls_x509verify_result verifyResult)
		{
			if (verifyResult == UnityTls.unitytls_x509verify_result.UNITYTLS_X509VERIFY_SUCCESS)
			{
				return SslPolicyErrors.None;
			}
			if (verifyResult == (UnityTls.unitytls_x509verify_result)4294967295U)
			{
				return SslPolicyErrors.RemoteCertificateChainErrors;
			}
			SslPolicyErrors sslPolicyErrors = SslPolicyErrors.None;
			if (verifyResult.HasFlag(UnityTls.unitytls_x509verify_result.UNITYTLS_X509VERIFY_FLAG_CN_MISMATCH))
			{
				sslPolicyErrors |= SslPolicyErrors.RemoteCertificateNameMismatch;
			}
			if (verifyResult != UnityTls.unitytls_x509verify_result.UNITYTLS_X509VERIFY_FLAG_CN_MISMATCH)
			{
				sslPolicyErrors |= SslPolicyErrors.RemoteCertificateChainErrors;
			}
			return sslPolicyErrors;
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x00004F24 File Offset: 0x00003124
		public static X509ChainStatusFlags VerifyResultToChainStatus(UnityTls.unitytls_x509verify_result verifyResult)
		{
			if (verifyResult == UnityTls.unitytls_x509verify_result.UNITYTLS_X509VERIFY_SUCCESS)
			{
				return X509ChainStatusFlags.NoError;
			}
			if (verifyResult == (UnityTls.unitytls_x509verify_result)4294967295U)
			{
				return X509ChainStatusFlags.UntrustedRoot;
			}
			X509ChainStatusFlags x509ChainStatusFlags = X509ChainStatusFlags.NoError;
			if (verifyResult.HasFlag(UnityTls.unitytls_x509verify_result.UNITYTLS_X509VERIFY_FLAG_EXPIRED))
			{
				x509ChainStatusFlags |= X509ChainStatusFlags.NotTimeValid;
			}
			if (verifyResult.HasFlag(UnityTls.unitytls_x509verify_result.UNITYTLS_X509VERIFY_FLAG_REVOKED))
			{
				x509ChainStatusFlags |= X509ChainStatusFlags.Revoked;
			}
			if (verifyResult.HasFlag(UnityTls.unitytls_x509verify_result.UNITYTLS_X509VERIFY_FLAG_CN_MISMATCH))
			{
				x509ChainStatusFlags |= X509ChainStatusFlags.UntrustedRoot;
			}
			if (verifyResult.HasFlag(UnityTls.unitytls_x509verify_result.UNITYTLS_X509VERIFY_FLAG_NOT_TRUSTED))
			{
				x509ChainStatusFlags |= X509ChainStatusFlags.UntrustedRoot;
			}
			return x509ChainStatusFlags;
		}
	}
}
