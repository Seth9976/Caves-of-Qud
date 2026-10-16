using System;
using System.Threading;
using Microsoft.Win32.SafeHandles;

namespace System.Net.Security
{
	// Token: 0x02000853 RID: 2131
	internal sealed class SecurityContextTokenHandle : CriticalHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x060043A6 RID: 17318 RVA: 0x000EBFF0 File Offset: 0x000EA1F0
		private SecurityContextTokenHandle()
		{
		}

		// Token: 0x060043A7 RID: 17319 RVA: 0x000EBFF8 File Offset: 0x000EA1F8
		internal IntPtr DangerousGetHandle()
		{
			return this.handle;
		}

		// Token: 0x060043A8 RID: 17320 RVA: 0x000EC000 File Offset: 0x000EA200
		protected override bool ReleaseHandle()
		{
			return this.IsInvalid || Interlocked.Increment(ref this._disposed) != 1 || global::Interop.Kernel32.CloseHandle(this.handle);
		}

		// Token: 0x0400290B RID: 10507
		private int _disposed;
	}
}
