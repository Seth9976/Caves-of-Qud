using System;
using System.Net.Security;
using System.Runtime.InteropServices;

namespace System.Net
{
	// Token: 0x02000555 RID: 1365
	internal class SSPISecureChannelType : SSPIInterface
	{
		// Token: 0x170008FE RID: 2302
		// (get) Token: 0x06002C57 RID: 11351 RVA: 0x00096744 File Offset: 0x00094944
		// (set) Token: 0x06002C58 RID: 11352 RVA: 0x0009674D File Offset: 0x0009494D
		public SecurityPackageInfoClass[] SecurityPackages
		{
			get
			{
				return SSPISecureChannelType.s_securityPackages;
			}
			set
			{
				SSPISecureChannelType.s_securityPackages = value;
			}
		}

		// Token: 0x06002C59 RID: 11353 RVA: 0x0009643F File Offset: 0x0009463F
		public int EnumerateSecurityPackages(out int pkgnum, out SafeFreeContextBuffer pkgArray)
		{
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Info(this, null, "EnumerateSecurityPackages");
			}
			return SafeFreeContextBuffer.EnumeratePackages(out pkgnum, out pkgArray);
		}

		// Token: 0x06002C5A RID: 11354 RVA: 0x0009645B File Offset: 0x0009465B
		public int AcquireCredentialsHandle(string moduleName, global::Interop.SspiCli.CredentialUse usage, ref global::Interop.SspiCli.SEC_WINNT_AUTH_IDENTITY_W authdata, out SafeFreeCredentials outCredential)
		{
			return SafeFreeCredentials.AcquireCredentialsHandle(moduleName, usage, ref authdata, out outCredential);
		}

		// Token: 0x06002C5B RID: 11355 RVA: 0x00096467 File Offset: 0x00094667
		public int AcquireCredentialsHandle(string moduleName, global::Interop.SspiCli.CredentialUse usage, ref SafeSspiAuthDataHandle authdata, out SafeFreeCredentials outCredential)
		{
			return SafeFreeCredentials.AcquireCredentialsHandle(moduleName, usage, ref authdata, out outCredential);
		}

		// Token: 0x06002C5C RID: 11356 RVA: 0x00096473 File Offset: 0x00094673
		public int AcquireDefaultCredential(string moduleName, global::Interop.SspiCli.CredentialUse usage, out SafeFreeCredentials outCredential)
		{
			return SafeFreeCredentials.AcquireDefaultCredential(moduleName, usage, out outCredential);
		}

		// Token: 0x06002C5D RID: 11357 RVA: 0x0009647D File Offset: 0x0009467D
		public int AcquireCredentialsHandle(string moduleName, global::Interop.SspiCli.CredentialUse usage, ref global::Interop.SspiCli.SCHANNEL_CRED authdata, out SafeFreeCredentials outCredential)
		{
			return SafeFreeCredentials.AcquireCredentialsHandle(moduleName, usage, ref authdata, out outCredential);
		}

		// Token: 0x06002C5E RID: 11358 RVA: 0x00096489 File Offset: 0x00094689
		public int AcceptSecurityContext(ref SafeFreeCredentials credential, ref SafeDeleteContext context, SecurityBuffer inputBuffer, global::Interop.SspiCli.ContextFlags inFlags, global::Interop.SspiCli.Endianness endianness, SecurityBuffer outputBuffer, ref global::Interop.SspiCli.ContextFlags outFlags)
		{
			return SafeDeleteContext.AcceptSecurityContext(ref credential, ref context, inFlags, endianness, inputBuffer, null, outputBuffer, ref outFlags);
		}

		// Token: 0x06002C5F RID: 11359 RVA: 0x0009649C File Offset: 0x0009469C
		public int AcceptSecurityContext(SafeFreeCredentials credential, ref SafeDeleteContext context, SecurityBuffer[] inputBuffers, global::Interop.SspiCli.ContextFlags inFlags, global::Interop.SspiCli.Endianness endianness, SecurityBuffer outputBuffer, ref global::Interop.SspiCli.ContextFlags outFlags)
		{
			return SafeDeleteContext.AcceptSecurityContext(ref credential, ref context, inFlags, endianness, null, inputBuffers, outputBuffer, ref outFlags);
		}

		// Token: 0x06002C60 RID: 11360 RVA: 0x00096758 File Offset: 0x00094958
		public int InitializeSecurityContext(ref SafeFreeCredentials credential, ref SafeDeleteContext context, string targetName, global::Interop.SspiCli.ContextFlags inFlags, global::Interop.SspiCli.Endianness endianness, SecurityBuffer inputBuffer, SecurityBuffer outputBuffer, ref global::Interop.SspiCli.ContextFlags outFlags)
		{
			return SafeDeleteContext.InitializeSecurityContext(ref credential, ref context, targetName, inFlags, endianness, inputBuffer, null, outputBuffer, ref outFlags);
		}

		// Token: 0x06002C61 RID: 11361 RVA: 0x00096778 File Offset: 0x00094978
		public int InitializeSecurityContext(SafeFreeCredentials credential, ref SafeDeleteContext context, string targetName, global::Interop.SspiCli.ContextFlags inFlags, global::Interop.SspiCli.Endianness endianness, SecurityBuffer[] inputBuffers, SecurityBuffer outputBuffer, ref global::Interop.SspiCli.ContextFlags outFlags)
		{
			return SafeDeleteContext.InitializeSecurityContext(ref credential, ref context, targetName, inFlags, endianness, null, inputBuffers, outputBuffer, ref outFlags);
		}

		// Token: 0x06002C62 RID: 11362 RVA: 0x0009679C File Offset: 0x0009499C
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

		// Token: 0x06002C63 RID: 11363 RVA: 0x000967DC File Offset: 0x000949DC
		public int DecryptMessage(SafeDeleteContext context, ref global::Interop.SspiCli.SecBufferDesc inputOutput, uint sequenceNumber)
		{
			int num;
			try
			{
				bool flag = false;
				context.DangerousAddRef(ref flag);
				num = global::Interop.SspiCli.DecryptMessage(ref context._handle, ref inputOutput, sequenceNumber, null);
			}
			finally
			{
				context.DangerousRelease();
			}
			return num;
		}

		// Token: 0x06002C64 RID: 11364 RVA: 0x000966E0 File Offset: 0x000948E0
		public int MakeSignature(SafeDeleteContext context, ref global::Interop.SspiCli.SecBufferDesc inputOutput, uint sequenceNumber)
		{
			throw global::System.NotImplemented.ByDesignWithMessage("This method is not implemented by this class.");
		}

		// Token: 0x06002C65 RID: 11365 RVA: 0x000966E0 File Offset: 0x000948E0
		public int VerifySignature(SafeDeleteContext context, ref global::Interop.SspiCli.SecBufferDesc inputOutput, uint sequenceNumber)
		{
			throw global::System.NotImplemented.ByDesignWithMessage("This method is not implemented by this class.");
		}

		// Token: 0x06002C66 RID: 11366 RVA: 0x00096820 File Offset: 0x00094A20
		public unsafe int QueryContextChannelBinding(SafeDeleteContext phContext, global::Interop.SspiCli.ContextAttribute attribute, out SafeFreeContextBufferChannelBinding refHandle)
		{
			refHandle = SafeFreeContextBufferChannelBinding.CreateEmptyHandle();
			SecPkgContext_Bindings secPkgContext_Bindings = default(SecPkgContext_Bindings);
			return SafeFreeContextBufferChannelBinding.QueryContextChannelBinding(phContext, attribute, &secPkgContext_Bindings, refHandle);
		}

		// Token: 0x06002C67 RID: 11367 RVA: 0x00096848 File Offset: 0x00094A48
		public unsafe int QueryContextAttributes(SafeDeleteContext phContext, global::Interop.SspiCli.ContextAttribute attribute, byte[] buffer, Type handleType, out SafeHandle refHandle)
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
			return SafeFreeContextBuffer.QueryContextAttributes(phContext, attribute, ptr, refHandle);
		}

		// Token: 0x06002C68 RID: 11368 RVA: 0x000968DC File Offset: 0x00094ADC
		public int SetContextAttributes(SafeDeleteContext phContext, global::Interop.SspiCli.ContextAttribute attribute, byte[] buffer)
		{
			return SafeFreeContextBuffer.SetContextAttributes(phContext, attribute, buffer);
		}

		// Token: 0x06002C69 RID: 11369 RVA: 0x000044FA File Offset: 0x000026FA
		public int QuerySecurityContextToken(SafeDeleteContext phContext, out SecurityContextTokenHandle phToken)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06002C6A RID: 11370 RVA: 0x000044FA File Offset: 0x000026FA
		public int CompleteAuthToken(ref SafeDeleteContext refContext, SecurityBuffer[] inputBuffers)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06002C6B RID: 11371 RVA: 0x000968E6 File Offset: 0x00094AE6
		public int ApplyControlToken(ref SafeDeleteContext refContext, SecurityBuffer[] inputBuffers)
		{
			return SafeDeleteContext.ApplyControlToken(ref refContext, inputBuffers);
		}

		// Token: 0x040017EB RID: 6123
		private static volatile SecurityPackageInfoClass[] s_securityPackages;
	}
}
