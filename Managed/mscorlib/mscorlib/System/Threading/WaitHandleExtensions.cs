using System;
using System.Security;
using Microsoft.Win32.SafeHandles;

namespace System.Threading
{
	// Token: 0x020002EC RID: 748
	public static class WaitHandleExtensions
	{
		// Token: 0x0600209B RID: 8347 RVA: 0x000768B7 File Offset: 0x00074AB7
		[SecurityCritical]
		public static SafeWaitHandle GetSafeWaitHandle(this WaitHandle waitHandle)
		{
			if (waitHandle == null)
			{
				throw new ArgumentNullException("waitHandle");
			}
			return waitHandle.SafeWaitHandle;
		}

		// Token: 0x0600209C RID: 8348 RVA: 0x000768CD File Offset: 0x00074ACD
		[SecurityCritical]
		public static void SetSafeWaitHandle(this WaitHandle waitHandle, SafeWaitHandle value)
		{
			if (waitHandle == null)
			{
				throw new ArgumentNullException("waitHandle");
			}
			waitHandle.SafeWaitHandle = value;
		}
	}
}
