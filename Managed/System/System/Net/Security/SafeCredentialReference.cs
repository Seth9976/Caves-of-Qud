using System;
using Microsoft.Win32.SafeHandles;

namespace System.Net.Security
{
	// Token: 0x0200084A RID: 2122
	internal sealed class SafeCredentialReference : CriticalHandleMinusOneIsInvalid
	{
		// Token: 0x06004385 RID: 17285 RVA: 0x000EB900 File Offset: 0x000E9B00
		internal static SafeCredentialReference CreateReference(SafeFreeCredentials target)
		{
			SafeCredentialReference safeCredentialReference = new SafeCredentialReference(target);
			if (safeCredentialReference.IsInvalid)
			{
				return null;
			}
			return safeCredentialReference;
		}

		// Token: 0x06004386 RID: 17286 RVA: 0x000EB920 File Offset: 0x000E9B20
		private SafeCredentialReference(SafeFreeCredentials target)
		{
			bool flag = false;
			target.DangerousAddRef(ref flag);
			this.Target = target;
			base.SetHandle(new IntPtr(0));
		}

		// Token: 0x06004387 RID: 17287 RVA: 0x000EB950 File Offset: 0x000E9B50
		protected override bool ReleaseHandle()
		{
			SafeFreeCredentials target = this.Target;
			if (target != null)
			{
				target.DangerousRelease();
			}
			this.Target = null;
			return true;
		}

		// Token: 0x040028F0 RID: 10480
		internal SafeFreeCredentials Target;
	}
}
