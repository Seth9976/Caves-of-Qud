using System;

namespace AiUnity.Common.Log
{
	// Token: 0x020002D2 RID: 722
	public interface IGameConsoleController
	{
		// Token: 0x060018E8 RID: 6376
		void AddMessage(int logLevels, string message, string loggerName = null, DateTime dateTime = default(DateTime));

		// Token: 0x060018E9 RID: 6377
		void SetConsoleActive(bool consoleActive);

		// Token: 0x060018EA RID: 6378
		void SetFontSize(int fontSize, bool updateControl = true, bool updateMessage = true);

		// Token: 0x060018EB RID: 6379
		void SetIconEnable(bool iconEnable);

		// Token: 0x060018EC RID: 6380
		void SetLogLevelFilter(LogLevels level, bool updateControl = true, bool updateMessage = true);
	}
}
