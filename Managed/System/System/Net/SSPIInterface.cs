using System;
using System.Net.Security;
using System.Runtime.InteropServices;

namespace System.Net
{
	// Token: 0x02000554 RID: 1364
	internal interface SSPIInterface
	{
		// Token: 0x170008FD RID: 2301
		// (get) Token: 0x06002C42 RID: 11330
		// (set) Token: 0x06002C43 RID: 11331
		SecurityPackageInfoClass[] SecurityPackages { get; set; }

		// Token: 0x06002C44 RID: 11332
		int EnumerateSecurityPackages(out int pkgnum, out SafeFreeContextBuffer pkgArray);

		// Token: 0x06002C45 RID: 11333
		int AcquireCredentialsHandle(string moduleName, global::Interop.SspiCli.CredentialUse usage, ref global::Interop.SspiCli.SEC_WINNT_AUTH_IDENTITY_W authdata, out SafeFreeCredentials outCredential);

		// Token: 0x06002C46 RID: 11334
		int AcquireCredentialsHandle(string moduleName, global::Interop.SspiCli.CredentialUse usage, ref SafeSspiAuthDataHandle authdata, out SafeFreeCredentials outCredential);

		// Token: 0x06002C47 RID: 11335
		int AcquireDefaultCredential(string moduleName, global::Interop.SspiCli.CredentialUse usage, out SafeFreeCredentials outCredential);

		// Token: 0x06002C48 RID: 11336
		int AcquireCredentialsHandle(string moduleName, global::Interop.SspiCli.CredentialUse usage, ref global::Interop.SspiCli.SCHANNEL_CRED authdata, out SafeFreeCredentials outCredential);

		// Token: 0x06002C49 RID: 11337
		int AcceptSecurityContext(ref SafeFreeCredentials credential, ref SafeDeleteContext context, SecurityBuffer inputBuffer, global::Interop.SspiCli.ContextFlags inFlags, global::Interop.SspiCli.Endianness endianness, SecurityBuffer outputBuffer, ref global::Interop.SspiCli.ContextFlags outFlags);

		// Token: 0x06002C4A RID: 11338
		int AcceptSecurityContext(SafeFreeCredentials credential, ref SafeDeleteContext context, SecurityBuffer[] inputBuffers, global::Interop.SspiCli.ContextFlags inFlags, global::Interop.SspiCli.Endianness endianness, SecurityBuffer outputBuffer, ref global::Interop.SspiCli.ContextFlags outFlags);

		// Token: 0x06002C4B RID: 11339
		int InitializeSecurityContext(ref SafeFreeCredentials credential, ref SafeDeleteContext context, string targetName, global::Interop.SspiCli.ContextFlags inFlags, global::Interop.SspiCli.Endianness endianness, SecurityBuffer inputBuffer, SecurityBuffer outputBuffer, ref global::Interop.SspiCli.ContextFlags outFlags);

		// Token: 0x06002C4C RID: 11340
		int InitializeSecurityContext(SafeFreeCredentials credential, ref SafeDeleteContext context, string targetName, global::Interop.SspiCli.ContextFlags inFlags, global::Interop.SspiCli.Endianness endianness, SecurityBuffer[] inputBuffers, SecurityBuffer outputBuffer, ref global::Interop.SspiCli.ContextFlags outFlags);

		// Token: 0x06002C4D RID: 11341
		int EncryptMessage(SafeDeleteContext context, ref global::Interop.SspiCli.SecBufferDesc inputOutput, uint sequenceNumber);

		// Token: 0x06002C4E RID: 11342
		int DecryptMessage(SafeDeleteContext context, ref global::Interop.SspiCli.SecBufferDesc inputOutput, uint sequenceNumber);

		// Token: 0x06002C4F RID: 11343
		int MakeSignature(SafeDeleteContext context, ref global::Interop.SspiCli.SecBufferDesc inputOutput, uint sequenceNumber);

		// Token: 0x06002C50 RID: 11344
		int VerifySignature(SafeDeleteContext context, ref global::Interop.SspiCli.SecBufferDesc inputOutput, uint sequenceNumber);

		// Token: 0x06002C51 RID: 11345
		int QueryContextChannelBinding(SafeDeleteContext phContext, global::Interop.SspiCli.ContextAttribute attribute, out SafeFreeContextBufferChannelBinding refHandle);

		// Token: 0x06002C52 RID: 11346
		int QueryContextAttributes(SafeDeleteContext phContext, global::Interop.SspiCli.ContextAttribute attribute, byte[] buffer, Type handleType, out SafeHandle refHandle);

		// Token: 0x06002C53 RID: 11347
		int SetContextAttributes(SafeDeleteContext phContext, global::Interop.SspiCli.ContextAttribute attribute, byte[] buffer);

		// Token: 0x06002C54 RID: 11348
		int QuerySecurityContextToken(SafeDeleteContext phContext, out SecurityContextTokenHandle phToken);

		// Token: 0x06002C55 RID: 11349
		int CompleteAuthToken(ref SafeDeleteContext refContext, SecurityBuffer[] inputBuffers);

		// Token: 0x06002C56 RID: 11350
		int ApplyControlToken(ref SafeDeleteContext refContext, SecurityBuffer[] inputBuffers);
	}
}
