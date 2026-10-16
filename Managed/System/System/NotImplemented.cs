using System;

namespace System
{
	// Token: 0x02000146 RID: 326
	internal static class NotImplemented
	{
		// Token: 0x1700014C RID: 332
		// (get) Token: 0x060008A9 RID: 2217 RVA: 0x0001FD13 File Offset: 0x0001DF13
		internal static Exception ByDesign
		{
			get
			{
				return new NotImplementedException();
			}
		}

		// Token: 0x060008AA RID: 2218 RVA: 0x0001FD1A File Offset: 0x0001DF1A
		internal static Exception ByDesignWithMessage(string message)
		{
			return new NotImplementedException(message);
		}

		// Token: 0x060008AB RID: 2219 RVA: 0x0001FD13 File Offset: 0x0001DF13
		internal static Exception ActiveIssue(string issue)
		{
			return new NotImplementedException();
		}
	}
}
