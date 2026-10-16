using System;
using System.Collections.Generic;

namespace Trivial.CodeSecurity.LoopDetection
{
	// Token: 0x0200000F RID: 15
	public class ExecutionTimingServices
	{
		// Token: 0x06000083 RID: 131 RVA: 0x000039C8 File Offset: 0x00001BC8
		public static void EnterTimedExecutionContext(int executionHash)
		{
			DateTime dateTime;
			if (ExecutionTimingServices.timeLookups.TryGetValue(executionHash, ref dateTime))
			{
				if ((DateTime.Now - dateTime).TotalSeconds > 5.0)
				{
					throw new TimeoutException("Execution was aborted to because an infinite loop was detected");
				}
			}
			else
			{
				ExecutionTimingServices.timeLookups.Add(executionHash, DateTime.Now);
			}
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00003A1D File Offset: 0x00001C1D
		public static void ExitTimedExecutionContext(int executionHash)
		{
			if (ExecutionTimingServices.timeLookups.ContainsKey(executionHash))
			{
				ExecutionTimingServices.timeLookups.Remove(executionHash);
			}
		}

		// Token: 0x04000037 RID: 55
		private static Dictionary<int, DateTime> timeLookups = new Dictionary<int, DateTime>();
	}
}
