using System;

namespace System.Net
{
	// Token: 0x02000570 RID: 1392
	internal enum SecurityStatusPalErrorCode
	{
		// Token: 0x04001867 RID: 6247
		NotSet,
		// Token: 0x04001868 RID: 6248
		OK,
		// Token: 0x04001869 RID: 6249
		ContinueNeeded,
		// Token: 0x0400186A RID: 6250
		CompleteNeeded,
		// Token: 0x0400186B RID: 6251
		CompAndContinue,
		// Token: 0x0400186C RID: 6252
		ContextExpired,
		// Token: 0x0400186D RID: 6253
		CredentialsNeeded,
		// Token: 0x0400186E RID: 6254
		Renegotiate,
		// Token: 0x0400186F RID: 6255
		OutOfMemory,
		// Token: 0x04001870 RID: 6256
		InvalidHandle,
		// Token: 0x04001871 RID: 6257
		Unsupported,
		// Token: 0x04001872 RID: 6258
		TargetUnknown,
		// Token: 0x04001873 RID: 6259
		InternalError,
		// Token: 0x04001874 RID: 6260
		PackageNotFound,
		// Token: 0x04001875 RID: 6261
		NotOwner,
		// Token: 0x04001876 RID: 6262
		CannotInstall,
		// Token: 0x04001877 RID: 6263
		InvalidToken,
		// Token: 0x04001878 RID: 6264
		CannotPack,
		// Token: 0x04001879 RID: 6265
		QopNotSupported,
		// Token: 0x0400187A RID: 6266
		NoImpersonation,
		// Token: 0x0400187B RID: 6267
		LogonDenied,
		// Token: 0x0400187C RID: 6268
		UnknownCredentials,
		// Token: 0x0400187D RID: 6269
		NoCredentials,
		// Token: 0x0400187E RID: 6270
		MessageAltered,
		// Token: 0x0400187F RID: 6271
		OutOfSequence,
		// Token: 0x04001880 RID: 6272
		NoAuthenticatingAuthority,
		// Token: 0x04001881 RID: 6273
		IncompleteMessage,
		// Token: 0x04001882 RID: 6274
		IncompleteCredentials,
		// Token: 0x04001883 RID: 6275
		BufferNotEnough,
		// Token: 0x04001884 RID: 6276
		WrongPrincipal,
		// Token: 0x04001885 RID: 6277
		TimeSkew,
		// Token: 0x04001886 RID: 6278
		UntrustedRoot,
		// Token: 0x04001887 RID: 6279
		IllegalMessage,
		// Token: 0x04001888 RID: 6280
		CertUnknown,
		// Token: 0x04001889 RID: 6281
		CertExpired,
		// Token: 0x0400188A RID: 6282
		AlgorithmMismatch,
		// Token: 0x0400188B RID: 6283
		SecurityQosFailed,
		// Token: 0x0400188C RID: 6284
		SmartcardLogonRequired,
		// Token: 0x0400188D RID: 6285
		UnsupportedPreauth,
		// Token: 0x0400188E RID: 6286
		BadBinding,
		// Token: 0x0400188F RID: 6287
		DowngradeDetected,
		// Token: 0x04001890 RID: 6288
		ApplicationProtocolMismatch
	}
}
