using System;

namespace UnityEngine
{
	// Token: 0x0200020A RID: 522
	public interface ILogHandler
	{
		// Token: 0x06001B65 RID: 7013
		void LogFormat(LogType logType, Object context, string format, params object[] args);

		// Token: 0x06001B66 RID: 7014
		void LogException(Exception exception, Object context);
	}
}
