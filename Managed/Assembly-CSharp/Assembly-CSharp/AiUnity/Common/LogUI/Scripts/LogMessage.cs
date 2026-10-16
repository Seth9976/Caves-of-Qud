using System;
using AiUnity.Common.Log;

namespace AiUnity.Common.LogUI.Scripts
{
	// Token: 0x020002CC RID: 716
	public class LogMessage
	{
		// Token: 0x17000361 RID: 865
		// (get) Token: 0x060018CB RID: 6347 RVA: 0x0007FF7E File Offset: 0x0007E17E
		// (set) Token: 0x060018CC RID: 6348 RVA: 0x0007FF86 File Offset: 0x0007E186
		public string LoggerName { get; set; }

		// Token: 0x17000362 RID: 866
		// (get) Token: 0x060018CD RID: 6349 RVA: 0x0007FF8F File Offset: 0x0007E18F
		// (set) Token: 0x060018CE RID: 6350 RVA: 0x0007FF97 File Offset: 0x0007E197
		public LogLevels LogLevels { get; set; }

		// Token: 0x17000363 RID: 867
		// (get) Token: 0x060018CF RID: 6351 RVA: 0x0007FFA0 File Offset: 0x0007E1A0
		// (set) Token: 0x060018D0 RID: 6352 RVA: 0x0007FFA8 File Offset: 0x0007E1A8
		public string Message { get; set; }

		// Token: 0x17000364 RID: 868
		// (get) Token: 0x060018D1 RID: 6353 RVA: 0x0007FFB1 File Offset: 0x0007E1B1
		// (set) Token: 0x060018D2 RID: 6354 RVA: 0x0007FFB9 File Offset: 0x0007E1B9
		public DateTime TimeStamp { get; set; }

		// Token: 0x060018D3 RID: 6355 RVA: 0x0007FFC2 File Offset: 0x0007E1C2
		public LogMessage(string message, int logLevel, string loggerName, DateTime timeStamp)
		{
			this.Message = message;
			this.LogLevels = (LogLevels)logLevel;
			this.LoggerName = loggerName;
			this.TimeStamp = timeStamp;
		}
	}
}
