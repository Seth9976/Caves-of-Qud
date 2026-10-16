using System;

namespace System.Net
{
	// Token: 0x02000562 RID: 1378
	internal static class ContextFlagsAdapterPal
	{
		// Token: 0x06002CA1 RID: 11425 RVA: 0x000980DC File Offset: 0x000962DC
		internal static ContextFlagsPal GetContextFlagsPalFromInterop(global::Interop.SspiCli.ContextFlags win32Flags)
		{
			ContextFlagsPal contextFlagsPal = ContextFlagsPal.None;
			foreach (ContextFlagsAdapterPal.ContextFlagMapping contextFlagMapping in ContextFlagsAdapterPal.s_contextFlagMapping)
			{
				if ((win32Flags & contextFlagMapping.Win32Flag) == contextFlagMapping.Win32Flag)
				{
					contextFlagsPal |= contextFlagMapping.ContextFlag;
				}
			}
			return contextFlagsPal;
		}

		// Token: 0x06002CA2 RID: 11426 RVA: 0x00098124 File Offset: 0x00096324
		internal static global::Interop.SspiCli.ContextFlags GetInteropFromContextFlagsPal(ContextFlagsPal flags)
		{
			global::Interop.SspiCli.ContextFlags contextFlags = global::Interop.SspiCli.ContextFlags.Zero;
			foreach (ContextFlagsAdapterPal.ContextFlagMapping contextFlagMapping in ContextFlagsAdapterPal.s_contextFlagMapping)
			{
				if ((flags & contextFlagMapping.ContextFlag) == contextFlagMapping.ContextFlag)
				{
					contextFlags |= contextFlagMapping.Win32Flag;
				}
			}
			return contextFlags;
		}

		// Token: 0x0400181B RID: 6171
		private static readonly ContextFlagsAdapterPal.ContextFlagMapping[] s_contextFlagMapping = new ContextFlagsAdapterPal.ContextFlagMapping[]
		{
			new ContextFlagsAdapterPal.ContextFlagMapping(global::Interop.SspiCli.ContextFlags.AcceptExtendedError, ContextFlagsPal.AcceptExtendedError),
			new ContextFlagsAdapterPal.ContextFlagMapping(global::Interop.SspiCli.ContextFlags.InitManualCredValidation, ContextFlagsPal.InitManualCredValidation),
			new ContextFlagsAdapterPal.ContextFlagMapping(global::Interop.SspiCli.ContextFlags.AcceptIntegrity, ContextFlagsPal.AcceptIntegrity),
			new ContextFlagsAdapterPal.ContextFlagMapping(global::Interop.SspiCli.ContextFlags.AcceptStream, ContextFlagsPal.AcceptStream),
			new ContextFlagsAdapterPal.ContextFlagMapping(global::Interop.SspiCli.ContextFlags.AllocateMemory, ContextFlagsPal.AllocateMemory),
			new ContextFlagsAdapterPal.ContextFlagMapping(global::Interop.SspiCli.ContextFlags.AllowMissingBindings, ContextFlagsPal.AllowMissingBindings),
			new ContextFlagsAdapterPal.ContextFlagMapping(global::Interop.SspiCli.ContextFlags.Confidentiality, ContextFlagsPal.Confidentiality),
			new ContextFlagsAdapterPal.ContextFlagMapping(global::Interop.SspiCli.ContextFlags.Connection, ContextFlagsPal.Connection),
			new ContextFlagsAdapterPal.ContextFlagMapping(global::Interop.SspiCli.ContextFlags.Delegate, ContextFlagsPal.Delegate),
			new ContextFlagsAdapterPal.ContextFlagMapping(global::Interop.SspiCli.ContextFlags.InitExtendedError, ContextFlagsPal.InitExtendedError),
			new ContextFlagsAdapterPal.ContextFlagMapping(global::Interop.SspiCli.ContextFlags.AcceptIntegrity, ContextFlagsPal.AcceptIntegrity),
			new ContextFlagsAdapterPal.ContextFlagMapping(global::Interop.SspiCli.ContextFlags.InitManualCredValidation, ContextFlagsPal.InitManualCredValidation),
			new ContextFlagsAdapterPal.ContextFlagMapping(global::Interop.SspiCli.ContextFlags.AcceptStream, ContextFlagsPal.AcceptStream),
			new ContextFlagsAdapterPal.ContextFlagMapping(global::Interop.SspiCli.ContextFlags.AcceptExtendedError, ContextFlagsPal.AcceptExtendedError),
			new ContextFlagsAdapterPal.ContextFlagMapping(global::Interop.SspiCli.ContextFlags.InitUseSuppliedCreds, ContextFlagsPal.InitUseSuppliedCreds),
			new ContextFlagsAdapterPal.ContextFlagMapping(global::Interop.SspiCli.ContextFlags.MutualAuth, ContextFlagsPal.MutualAuth),
			new ContextFlagsAdapterPal.ContextFlagMapping(global::Interop.SspiCli.ContextFlags.ProxyBindings, ContextFlagsPal.ProxyBindings),
			new ContextFlagsAdapterPal.ContextFlagMapping(global::Interop.SspiCli.ContextFlags.ReplayDetect, ContextFlagsPal.ReplayDetect),
			new ContextFlagsAdapterPal.ContextFlagMapping(global::Interop.SspiCli.ContextFlags.SequenceDetect, ContextFlagsPal.SequenceDetect),
			new ContextFlagsAdapterPal.ContextFlagMapping(global::Interop.SspiCli.ContextFlags.UnverifiedTargetName, ContextFlagsPal.UnverifiedTargetName),
			new ContextFlagsAdapterPal.ContextFlagMapping(global::Interop.SspiCli.ContextFlags.UseSessionKey, ContextFlagsPal.UseSessionKey),
			new ContextFlagsAdapterPal.ContextFlagMapping(global::Interop.SspiCli.ContextFlags.Zero, ContextFlagsPal.None)
		};

		// Token: 0x02000563 RID: 1379
		private readonly struct ContextFlagMapping
		{
			// Token: 0x06002CA4 RID: 11428 RVA: 0x00098342 File Offset: 0x00096542
			public ContextFlagMapping(global::Interop.SspiCli.ContextFlags win32Flag, ContextFlagsPal contextFlag)
			{
				this.Win32Flag = win32Flag;
				this.ContextFlag = contextFlag;
			}

			// Token: 0x0400181C RID: 6172
			public readonly global::Interop.SspiCli.ContextFlags Win32Flag;

			// Token: 0x0400181D RID: 6173
			public readonly ContextFlagsPal ContextFlag;
		}
	}
}
