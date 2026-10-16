using System;
using System.Net.Security;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Net
{
	// Token: 0x02000553 RID: 1363
	internal class SSPIAuthType : SSPIInterface
	{
		// Token: 0x170008FC RID: 2300
		// (get) Token: 0x06002C2B RID: 11307 RVA: 0x0009642C File Offset: 0x0009462C
		// (set) Token: 0x06002C2C RID: 11308 RVA: 0x00096435 File Offset: 0x00094635
		public SecurityPackageInfoClass[] SecurityPackages
		{
			get
			{
				return SSPIAuthType.s_securityPackages;
			}
			set
			{
				SSPIAuthType.s_securityPackages = value;
			}
		}

		// Token: 0x06002C2D RID: 11309 RVA: 0x0009643F File Offset: 0x0009463F
		public int EnumerateSecurityPackages(out int pkgnum, out SafeFreeContextBuffer pkgArray)
		{
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Info(this, null, "EnumerateSecurityPackages");
			}
			return SafeFreeContextBuffer.EnumeratePackages(out pkgnum, out pkgArray);
		}

		// Token: 0x06002C2E RID: 11310 RVA: 0x0009645B File Offset: 0x0009465B
		public int AcquireCredentialsHandle(string moduleName, global::Interop.SspiCli.CredentialUse usage, ref global::Interop.SspiCli.SEC_WINNT_AUTH_IDENTITY_W authdata, out SafeFreeCredentials outCredential)
		{
			return SafeFreeCredentials.AcquireCredentialsHandle(moduleName, usage, ref authdata, out outCredential);
		}

		// Token: 0x06002C2F RID: 11311 RVA: 0x00096467 File Offset: 0x00094667
		public int AcquireCredentialsHandle(string moduleName, global::Interop.SspiCli.CredentialUse usage, ref SafeSspiAuthDataHandle authdata, out SafeFreeCredentials outCredential)
		{
			return SafeFreeCredentials.AcquireCredentialsHandle(moduleName, usage, ref authdata, out outCredential);
		}

		// Token: 0x06002C30 RID: 11312 RVA: 0x00096473 File Offset: 0x00094673
		public int AcquireDefaultCredential(string moduleName, global::Interop.SspiCli.CredentialUse usage, out SafeFreeCredentials outCredential)
		{
			return SafeFreeCredentials.AcquireDefaultCredential(moduleName, usage, out outCredential);
		}

		// Token: 0x06002C31 RID: 11313 RVA: 0x0009647D File Offset: 0x0009467D
		public int AcquireCredentialsHandle(string moduleName, global::Interop.SspiCli.CredentialUse usage, ref global::Interop.SspiCli.SCHANNEL_CRED authdata, out SafeFreeCredentials outCredential)
		{
			return SafeFreeCredentials.AcquireCredentialsHandle(moduleName, usage, ref authdata, out outCredential);
		}

		// Token: 0x06002C32 RID: 11314 RVA: 0x00096489 File Offset: 0x00094689
		public int AcceptSecurityContext(ref SafeFreeCredentials credential, ref SafeDeleteContext context, SecurityBuffer inputBuffer, global::Interop.SspiCli.ContextFlags inFlags, global::Interop.SspiCli.Endianness endianness, SecurityBuffer outputBuffer, ref global::Interop.SspiCli.ContextFlags outFlags)
		{
			return SafeDeleteContext.AcceptSecurityContext(ref credential, ref context, inFlags, endianness, inputBuffer, null, outputBuffer, ref outFlags);
		}

		// Token: 0x06002C33 RID: 11315 RVA: 0x0009649C File Offset: 0x0009469C
		public int AcceptSecurityContext(SafeFreeCredentials credential, ref SafeDeleteContext context, SecurityBuffer[] inputBuffers, global::Interop.SspiCli.ContextFlags inFlags, global::Interop.SspiCli.Endianness endianness, SecurityBuffer outputBuffer, ref global::Interop.SspiCli.ContextFlags outFlags)
		{
			return SafeDeleteContext.AcceptSecurityContext(ref credential, ref context, inFlags, endianness, null, inputBuffers, outputBuffer, ref outFlags);
		}

		// Token: 0x06002C34 RID: 11316 RVA: 0x000964B0 File Offset: 0x000946B0
		public int InitializeSecurityContext(ref SafeFreeCredentials credential, ref SafeDeleteContext context, string targetName, global::Interop.SspiCli.ContextFlags inFlags, global::Interop.SspiCli.Endianness endianness, SecurityBuffer inputBuffer, SecurityBuffer outputBuffer, ref global::Interop.SspiCli.ContextFlags outFlags)
		{
			return SafeDeleteContext.InitializeSecurityContext(ref credential, ref context, targetName, inFlags, endianness, inputBuffer, null, outputBuffer, ref outFlags);
		}

		// Token: 0x06002C35 RID: 11317 RVA: 0x000964D0 File Offset: 0x000946D0
		public int InitializeSecurityContext(SafeFreeCredentials credential, ref SafeDeleteContext context, string targetName, global::Interop.SspiCli.ContextFlags inFlags, global::Interop.SspiCli.Endianness endianness, SecurityBuffer[] inputBuffers, SecurityBuffer outputBuffer, ref global::Interop.SspiCli.ContextFlags outFlags)
		{
			return SafeDeleteContext.InitializeSecurityContext(ref credential, ref context, targetName, inFlags, endianness, null, inputBuffers, outputBuffer, ref outFlags);
		}

		// Token: 0x06002C36 RID: 11318 RVA: 0x000964F4 File Offset: 0x000946F4
		public int EncryptMessage(SafeDeleteContext context, ref global::Interop.SspiCli.SecBufferDesc inputOutput, uint sequenceNumber)
		{
			int num;
			try
			{
				bool flag = false;
				context.DangerousAddRef(ref flag);
				num = global::Interop.SspiCli.EncryptMessage(ref context._handle, 0U, ref inputOutput, sequenceNumber);
			}
			finally
			{
				context.DangerousRelease();
			}
			return num;
		}

		// Token: 0x06002C37 RID: 11319 RVA: 0x00096534 File Offset: 0x00094734
		public unsafe int DecryptMessage(SafeDeleteContext context, ref global::Interop.SspiCli.SecBufferDesc inputOutput, uint sequenceNumber)
		{
			int num = -2146893055;
			uint num2 = 0U;
			try
			{
				bool flag = false;
				context.DangerousAddRef(ref flag);
				num = global::Interop.SspiCli.DecryptMessage(ref context._handle, ref inputOutput, sequenceNumber, &num2);
			}
			finally
			{
				context.DangerousRelease();
			}
			if (num == 0 && num2 == 2147483649U)
			{
				NetEventSource.Fail(this, FormattableStringFactory.Create("Expected qop = 0, returned value = {0}", new object[] { num2 }), "DecryptMessage");
				throw new InvalidOperationException("Protocol error: A received message contains a valid signature but it was not encrypted as required by the effective Protection Level.");
			}
			return num;
		}

		// Token: 0x06002C38 RID: 11320 RVA: 0x000965B8 File Offset: 0x000947B8
		public int MakeSignature(SafeDeleteContext context, ref global::Interop.SspiCli.SecBufferDesc inputOutput, uint sequenceNumber)
		{
			int num;
			try
			{
				bool flag = false;
				context.DangerousAddRef(ref flag);
				num = global::Interop.SspiCli.EncryptMessage(ref context._handle, 2147483649U, ref inputOutput, sequenceNumber);
			}
			finally
			{
				context.DangerousRelease();
			}
			return num;
		}

		// Token: 0x06002C39 RID: 11321 RVA: 0x000965FC File Offset: 0x000947FC
		public unsafe int VerifySignature(SafeDeleteContext context, ref global::Interop.SspiCli.SecBufferDesc inputOutput, uint sequenceNumber)
		{
			int num2;
			try
			{
				bool flag = false;
				uint num = 0U;
				context.DangerousAddRef(ref flag);
				num2 = global::Interop.SspiCli.DecryptMessage(ref context._handle, ref inputOutput, sequenceNumber, &num);
			}
			finally
			{
				context.DangerousRelease();
			}
			return num2;
		}

		// Token: 0x06002C3A RID: 11322 RVA: 0x00096640 File Offset: 0x00094840
		public int QueryContextChannelBinding(SafeDeleteContext context, global::Interop.SspiCli.ContextAttribute attribute, out SafeFreeContextBufferChannelBinding binding)
		{
			binding = null;
			throw new NotSupportedException();
		}

		// Token: 0x06002C3B RID: 11323 RVA: 0x0009664C File Offset: 0x0009484C
		public unsafe int QueryContextAttributes(SafeDeleteContext context, global::Interop.SspiCli.ContextAttribute attribute, byte[] buffer, Type handleType, out SafeHandle refHandle)
		{
			refHandle = null;
			if (handleType != null)
			{
				if (handleType == typeof(SafeFreeContextBuffer))
				{
					refHandle = SafeFreeContextBuffer.CreateEmptyHandle();
				}
				else
				{
					if (!(handleType == typeof(SafeFreeCertContext)))
					{
						throw new ArgumentException(SR.Format("'{0}' is not a supported handle type.", handleType.FullName), "handleType");
					}
					refHandle = new SafeFreeCertContext();
				}
			}
			byte* ptr;
			if (buffer == null || buffer.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &buffer[0];
			}
			return SafeFreeContextBuffer.QueryContextAttributes(context, attribute, ptr, refHandle);
		}

		// Token: 0x06002C3C RID: 11324 RVA: 0x000966E0 File Offset: 0x000948E0
		public int SetContextAttributes(SafeDeleteContext context, global::Interop.SspiCli.ContextAttribute attribute, byte[] buffer)
		{
			throw global::System.NotImplemented.ByDesignWithMessage("This method is not implemented by this class.");
		}

		// Token: 0x06002C3D RID: 11325 RVA: 0x000966EC File Offset: 0x000948EC
		public int QuerySecurityContextToken(SafeDeleteContext phContext, out SecurityContextTokenHandle phToken)
		{
			return SSPIAuthType.GetSecurityContextToken(phContext, out phToken);
		}

		// Token: 0x06002C3E RID: 11326 RVA: 0x000966F5 File Offset: 0x000948F5
		public int CompleteAuthToken(ref SafeDeleteContext refContext, SecurityBuffer[] inputBuffers)
		{
			return SafeDeleteContext.CompleteAuthToken(ref refContext, inputBuffers);
		}

		// Token: 0x06002C3F RID: 11327 RVA: 0x00096700 File Offset: 0x00094900
		private static int GetSecurityContextToken(SafeDeleteContext phContext, out SecurityContextTokenHandle safeHandle)
		{
			safeHandle = null;
			int num;
			try
			{
				bool flag = false;
				phContext.DangerousAddRef(ref flag);
				num = global::Interop.SspiCli.QuerySecurityContextToken(ref phContext._handle, out safeHandle);
			}
			finally
			{
				phContext.DangerousRelease();
			}
			return num;
		}

		// Token: 0x06002C40 RID: 11328 RVA: 0x000044FA File Offset: 0x000026FA
		public int ApplyControlToken(ref SafeDeleteContext refContext, SecurityBuffer[] inputBuffers)
		{
			throw new NotSupportedException();
		}

		// Token: 0x040017EA RID: 6122
		private static volatile SecurityPackageInfoClass[] s_securityPackages;
	}
}
