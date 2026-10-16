using System;
using System.Runtime.InteropServices;
using System.Threading;
using Microsoft.Win32;
using Microsoft.Win32.SafeHandles;

namespace System.Diagnostics
{
	// Token: 0x0200024A RID: 586
	internal class ProcessWaitHandle : WaitHandle
	{
		// Token: 0x06001216 RID: 4630 RVA: 0x0004E214 File Offset: 0x0004C414
		internal ProcessWaitHandle(SafeProcessHandle processHandle)
		{
			SafeWaitHandle safeWaitHandle = null;
			if (!Microsoft.Win32.NativeMethods.DuplicateHandle(new HandleRef(this, Microsoft.Win32.NativeMethods.GetCurrentProcess()), processHandle, new HandleRef(this, Microsoft.Win32.NativeMethods.GetCurrentProcess()), out safeWaitHandle, 0, false, 2))
			{
				throw new SystemException("Unknown error in DuplicateHandle");
			}
			base.SafeWaitHandle = safeWaitHandle;
		}
	}
}
