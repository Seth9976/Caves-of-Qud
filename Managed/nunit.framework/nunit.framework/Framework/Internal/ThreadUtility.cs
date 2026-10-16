using System;
using System.Threading;

namespace NUnit.Framework.Internal
{
	// Token: 0x0200008E RID: 142
	public static class ThreadUtility
	{
		// Token: 0x06000580 RID: 1408 RVA: 0x0000C780 File Offset: 0x0000A980
		public static void Kill(Thread thread)
		{
			ThreadUtility.Kill(thread, null);
		}

		// Token: 0x06000581 RID: 1409 RVA: 0x0000C78C File Offset: 0x0000A98C
		public static void Kill(Thread thread, object stateInfo)
		{
			try
			{
				if (stateInfo == null)
				{
					thread.Abort();
				}
				else
				{
					thread.Abort(stateInfo);
				}
			}
			catch (ThreadStateException)
			{
				thread.Resume();
			}
			if ((thread.ThreadState & ThreadState.WaitSleepJoin) != ThreadState.Running)
			{
				thread.Interrupt();
			}
		}
	}
}
