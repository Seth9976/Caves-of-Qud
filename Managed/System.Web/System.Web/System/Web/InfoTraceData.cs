using System;

namespace System.Web
{
	// Token: 0x020000DE RID: 222
	internal sealed class InfoTraceData
	{
		// Token: 0x06000BF1 RID: 3057 RVA: 0x0001F769 File Offset: 0x0001D969
		public InfoTraceData(string category, string message, string exception, double timeSinceFirst, double timeSinceLast, bool isWarning)
		{
			this.Category = category;
			this.Message = message;
			this.Exception = exception;
			this.TimeSinceFirst = timeSinceFirst;
			this.TimeSinceLast = timeSinceLast;
			this.IsWarning = isWarning;
		}

		// Token: 0x040010CC RID: 4300
		public string Category;

		// Token: 0x040010CD RID: 4301
		public string Message;

		// Token: 0x040010CE RID: 4302
		public string Exception;

		// Token: 0x040010CF RID: 4303
		public double TimeSinceFirst;

		// Token: 0x040010D0 RID: 4304
		public double TimeSinceLast;

		// Token: 0x040010D1 RID: 4305
		public bool IsWarning;
	}
}
