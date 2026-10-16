using System;

namespace UnityEngine
{
	// Token: 0x02000209 RID: 521
	public interface ILogger : ILogHandler
	{
		// Token: 0x17000478 RID: 1144
		// (get) Token: 0x06001B51 RID: 6993
		// (set) Token: 0x06001B52 RID: 6994
		ILogHandler logHandler { get; set; }

		// Token: 0x17000479 RID: 1145
		// (get) Token: 0x06001B53 RID: 6995
		// (set) Token: 0x06001B54 RID: 6996
		bool logEnabled { get; set; }

		// Token: 0x1700047A RID: 1146
		// (get) Token: 0x06001B55 RID: 6997
		// (set) Token: 0x06001B56 RID: 6998
		LogType filterLogType { get; set; }

		// Token: 0x06001B57 RID: 6999
		bool IsLogTypeAllowed(LogType logType);

		// Token: 0x06001B58 RID: 7000
		void Log(LogType logType, object message);

		// Token: 0x06001B59 RID: 7001
		void Log(LogType logType, object message, Object context);

		// Token: 0x06001B5A RID: 7002
		void Log(LogType logType, string tag, object message);

		// Token: 0x06001B5B RID: 7003
		void Log(LogType logType, string tag, object message, Object context);

		// Token: 0x06001B5C RID: 7004
		void Log(object message);

		// Token: 0x06001B5D RID: 7005
		void Log(string tag, object message);

		// Token: 0x06001B5E RID: 7006
		void Log(string tag, object message, Object context);

		// Token: 0x06001B5F RID: 7007
		void LogWarning(string tag, object message);

		// Token: 0x06001B60 RID: 7008
		void LogWarning(string tag, object message, Object context);

		// Token: 0x06001B61 RID: 7009
		void LogError(string tag, object message);

		// Token: 0x06001B62 RID: 7010
		void LogError(string tag, object message, Object context);

		// Token: 0x06001B63 RID: 7011
		void LogFormat(LogType logType, string format, params object[] args);

		// Token: 0x06001B64 RID: 7012
		void LogException(Exception exception);
	}
}
