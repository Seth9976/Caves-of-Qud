using System;

namespace UnityEngine.Android
{
	// Token: 0x02000018 RID: 24
	public static class ApplicationExitInfoProvider
	{
		// Token: 0x06000261 RID: 609 RVA: 0x0000A27C File Offset: 0x0000847C
		public static IApplicationExitInfo[] GetHistoricalProcessExitInfo(string packageName = null, int pid = 0, int maxNum = 0)
		{
			IApplicationExitInfo[] array = null;
			bool flag = array == null;
			if (flag)
			{
				array = new IApplicationExitInfo[0];
			}
			return array;
		}

		// Token: 0x06000262 RID: 610 RVA: 0x0000A2A0 File Offset: 0x000084A0
		public static void SetProcessStateSummary(sbyte[] buffer)
		{
		}
	}
}
