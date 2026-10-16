using System;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace System.Net.Security
{
	// Token: 0x02000846 RID: 2118
	internal abstract class SafeFreeContextBuffer : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x06004374 RID: 17268 RVA: 0x00013B50 File Offset: 0x00011D50
		protected SafeFreeContextBuffer()
			: base(true)
		{
		}

		// Token: 0x06004375 RID: 17269 RVA: 0x00013B79 File Offset: 0x00011D79
		internal void Set(IntPtr value)
		{
			this.handle = value;
		}

		// Token: 0x06004376 RID: 17270 RVA: 0x000EB630 File Offset: 0x000E9830
		internal static int EnumeratePackages(out int pkgnum, out SafeFreeContextBuffer pkgArray)
		{
			SafeFreeContextBuffer_SECURITY safeFreeContextBuffer_SECURITY = null;
			int num = global::Interop.SspiCli.EnumerateSecurityPackagesW(out pkgnum, out safeFreeContextBuffer_SECURITY);
			pkgArray = safeFreeContextBuffer_SECURITY;
			if (num != 0 && pkgArray != null)
			{
				pkgArray.SetHandleAsInvalid();
			}
			return num;
		}

		// Token: 0x06004377 RID: 17271 RVA: 0x000EB658 File Offset: 0x000E9858
		internal static SafeFreeContextBuffer CreateEmptyHandle()
		{
			return new SafeFreeContextBuffer_SECURITY();
		}

		// Token: 0x06004378 RID: 17272 RVA: 0x000EB660 File Offset: 0x000E9860
		public unsafe static int QueryContextAttributes(SafeDeleteContext phContext, global::Interop.SspiCli.ContextAttribute contextAttribute, byte* buffer, SafeHandle refHandle)
		{
			int num = -2146893055;
			try
			{
				bool flag = false;
				phContext.DangerousAddRef(ref flag);
				num = global::Interop.SspiCli.QueryContextAttributesW(ref phContext._handle, contextAttribute, (void*)buffer);
			}
			finally
			{
				phContext.DangerousRelease();
			}
			if (num == 0 && refHandle != null)
			{
				if (refHandle is SafeFreeContextBuffer)
				{
					((SafeFreeContextBuffer)refHandle).Set(*(IntPtr*)buffer);
				}
				else
				{
					((SafeFreeCertContext)refHandle).Set(*(IntPtr*)buffer);
				}
			}
			if (num != 0 && refHandle != null)
			{
				refHandle.SetHandleAsInvalid();
			}
			return num;
		}

		// Token: 0x06004379 RID: 17273 RVA: 0x000EB6DC File Offset: 0x000E98DC
		public static int SetContextAttributes(SafeDeleteContext phContext, global::Interop.SspiCli.ContextAttribute contextAttribute, byte[] buffer)
		{
			int num;
			try
			{
				bool flag = false;
				phContext.DangerousAddRef(ref flag);
				num = global::Interop.SspiCli.SetContextAttributesW(ref phContext._handle, contextAttribute, buffer, buffer.Length);
			}
			finally
			{
				phContext.DangerousRelease();
			}
			return num;
		}
	}
}
