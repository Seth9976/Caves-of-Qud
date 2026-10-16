using System;

namespace System.Net
{
	// Token: 0x020005E6 RID: 1510
	internal enum SecurityStatus
	{
		// Token: 0x04001B3A RID: 6970
		OK,
		// Token: 0x04001B3B RID: 6971
		ContinueNeeded = 590610,
		// Token: 0x04001B3C RID: 6972
		CompleteNeeded,
		// Token: 0x04001B3D RID: 6973
		CompAndContinue,
		// Token: 0x04001B3E RID: 6974
		ContextExpired = 590615,
		// Token: 0x04001B3F RID: 6975
		CredentialsNeeded = 590624,
		// Token: 0x04001B40 RID: 6976
		Renegotiate,
		// Token: 0x04001B41 RID: 6977
		OutOfMemory = -2146893056,
		// Token: 0x04001B42 RID: 6978
		InvalidHandle,
		// Token: 0x04001B43 RID: 6979
		Unsupported,
		// Token: 0x04001B44 RID: 6980
		TargetUnknown,
		// Token: 0x04001B45 RID: 6981
		InternalError,
		// Token: 0x04001B46 RID: 6982
		PackageNotFound,
		// Token: 0x04001B47 RID: 6983
		NotOwner,
		// Token: 0x04001B48 RID: 6984
		CannotInstall,
		// Token: 0x04001B49 RID: 6985
		InvalidToken,
		// Token: 0x04001B4A RID: 6986
		CannotPack,
		// Token: 0x04001B4B RID: 6987
		QopNotSupported,
		// Token: 0x04001B4C RID: 6988
		NoImpersonation,
		// Token: 0x04001B4D RID: 6989
		LogonDenied,
		// Token: 0x04001B4E RID: 6990
		UnknownCredentials,
		// Token: 0x04001B4F RID: 6991
		NoCredentials,
		// Token: 0x04001B50 RID: 6992
		MessageAltered,
		// Token: 0x04001B51 RID: 6993
		OutOfSequence,
		// Token: 0x04001B52 RID: 6994
		NoAuthenticatingAuthority,
		// Token: 0x04001B53 RID: 6995
		IncompleteMessage = -2146893032,
		// Token: 0x04001B54 RID: 6996
		IncompleteCredentials = -2146893024,
		// Token: 0x04001B55 RID: 6997
		BufferNotEnough,
		// Token: 0x04001B56 RID: 6998
		WrongPrincipal,
		// Token: 0x04001B57 RID: 6999
		TimeSkew = -2146893020,
		// Token: 0x04001B58 RID: 7000
		UntrustedRoot,
		// Token: 0x04001B59 RID: 7001
		IllegalMessage,
		// Token: 0x04001B5A RID: 7002
		CertUnknown,
		// Token: 0x04001B5B RID: 7003
		CertExpired,
		// Token: 0x04001B5C RID: 7004
		AlgorithmMismatch = -2146893007,
		// Token: 0x04001B5D RID: 7005
		SecurityQosFailed,
		// Token: 0x04001B5E RID: 7006
		SmartcardLogonRequired = -2146892994,
		// Token: 0x04001B5F RID: 7007
		UnsupportedPreauth = -2146892989,
		// Token: 0x04001B60 RID: 7008
		BadBinding = -2146892986
	}
}
