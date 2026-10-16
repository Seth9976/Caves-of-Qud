using System;
using System.Globalization;

namespace UnityEngine
{
	// Token: 0x0200020B RID: 523
	public class Logger : ILogger, ILogHandler
	{
		// Token: 0x06001B67 RID: 7015 RVA: 0x00030B93 File Offset: 0x0002ED93
		private Logger()
		{
		}

		// Token: 0x06001B68 RID: 7016 RVA: 0x00030B9D File Offset: 0x0002ED9D
		public Logger(ILogHandler logHandler)
		{
			this.logHandler = logHandler;
			this.logEnabled = true;
			this.filterLogType = LogType.Log;
		}

		// Token: 0x1700047B RID: 1147
		// (get) Token: 0x06001B69 RID: 7017 RVA: 0x00030BBF File Offset: 0x0002EDBF
		// (set) Token: 0x06001B6A RID: 7018 RVA: 0x00030BC7 File Offset: 0x0002EDC7
		public ILogHandler logHandler { get; set; }

		// Token: 0x1700047C RID: 1148
		// (get) Token: 0x06001B6B RID: 7019 RVA: 0x00030BD0 File Offset: 0x0002EDD0
		// (set) Token: 0x06001B6C RID: 7020 RVA: 0x00030BD8 File Offset: 0x0002EDD8
		public bool logEnabled { get; set; }

		// Token: 0x1700047D RID: 1149
		// (get) Token: 0x06001B6D RID: 7021 RVA: 0x00030BE1 File Offset: 0x0002EDE1
		// (set) Token: 0x06001B6E RID: 7022 RVA: 0x00030BE9 File Offset: 0x0002EDE9
		public LogType filterLogType { get; set; }

		// Token: 0x06001B6F RID: 7023 RVA: 0x00030BF4 File Offset: 0x0002EDF4
		public bool IsLogTypeAllowed(LogType logType)
		{
			bool logEnabled = this.logEnabled;
			if (logEnabled)
			{
				bool flag = logType == LogType.Exception;
				if (flag)
				{
					return true;
				}
				bool flag2 = this.filterLogType != LogType.Exception;
				if (flag2)
				{
					return logType <= this.filterLogType;
				}
			}
			return false;
		}

		// Token: 0x06001B70 RID: 7024 RVA: 0x00030C40 File Offset: 0x0002EE40
		private static string GetString(object message)
		{
			bool flag = message == null;
			string text;
			if (flag)
			{
				text = "Null";
			}
			else
			{
				IFormattable formattable = message as IFormattable;
				bool flag2 = formattable != null;
				if (flag2)
				{
					text = formattable.ToString(null, CultureInfo.InvariantCulture);
				}
				else
				{
					text = message.ToString();
				}
			}
			return text;
		}

		// Token: 0x06001B71 RID: 7025 RVA: 0x00030C8C File Offset: 0x0002EE8C
		public void Log(LogType logType, object message)
		{
			bool flag = this.IsLogTypeAllowed(logType);
			if (flag)
			{
				this.logHandler.LogFormat(logType, null, "{0}", new object[] { Logger.GetString(message) });
			}
		}

		// Token: 0x06001B72 RID: 7026 RVA: 0x00030CC8 File Offset: 0x0002EEC8
		public void Log(LogType logType, object message, Object context)
		{
			bool flag = this.IsLogTypeAllowed(logType);
			if (flag)
			{
				this.logHandler.LogFormat(logType, context, "{0}", new object[] { Logger.GetString(message) });
			}
		}

		// Token: 0x06001B73 RID: 7027 RVA: 0x00030D04 File Offset: 0x0002EF04
		public void Log(LogType logType, string tag, object message)
		{
			bool flag = this.IsLogTypeAllowed(logType);
			if (flag)
			{
				this.logHandler.LogFormat(logType, null, "{0}: {1}", new object[]
				{
					tag,
					Logger.GetString(message)
				});
			}
		}

		// Token: 0x06001B74 RID: 7028 RVA: 0x00030D44 File Offset: 0x0002EF44
		public void Log(LogType logType, string tag, object message, Object context)
		{
			bool flag = this.IsLogTypeAllowed(logType);
			if (flag)
			{
				this.logHandler.LogFormat(logType, context, "{0}: {1}", new object[]
				{
					tag,
					Logger.GetString(message)
				});
			}
		}

		// Token: 0x06001B75 RID: 7029 RVA: 0x00030D84 File Offset: 0x0002EF84
		public void Log(object message)
		{
			bool flag = this.IsLogTypeAllowed(LogType.Log);
			if (flag)
			{
				this.logHandler.LogFormat(LogType.Log, null, "{0}", new object[] { Logger.GetString(message) });
			}
		}

		// Token: 0x06001B76 RID: 7030 RVA: 0x00030DC0 File Offset: 0x0002EFC0
		public void Log(string tag, object message)
		{
			bool flag = this.IsLogTypeAllowed(LogType.Log);
			if (flag)
			{
				this.logHandler.LogFormat(LogType.Log, null, "{0}: {1}", new object[]
				{
					tag,
					Logger.GetString(message)
				});
			}
		}

		// Token: 0x06001B77 RID: 7031 RVA: 0x00030E00 File Offset: 0x0002F000
		public void Log(string tag, object message, Object context)
		{
			bool flag = this.IsLogTypeAllowed(LogType.Log);
			if (flag)
			{
				this.logHandler.LogFormat(LogType.Log, context, "{0}: {1}", new object[]
				{
					tag,
					Logger.GetString(message)
				});
			}
		}

		// Token: 0x06001B78 RID: 7032 RVA: 0x00030E40 File Offset: 0x0002F040
		public void LogWarning(string tag, object message)
		{
			bool flag = this.IsLogTypeAllowed(LogType.Warning);
			if (flag)
			{
				this.logHandler.LogFormat(LogType.Warning, null, "{0}: {1}", new object[]
				{
					tag,
					Logger.GetString(message)
				});
			}
		}

		// Token: 0x06001B79 RID: 7033 RVA: 0x00030E80 File Offset: 0x0002F080
		public void LogWarning(string tag, object message, Object context)
		{
			bool flag = this.IsLogTypeAllowed(LogType.Warning);
			if (flag)
			{
				this.logHandler.LogFormat(LogType.Warning, context, "{0}: {1}", new object[]
				{
					tag,
					Logger.GetString(message)
				});
			}
		}

		// Token: 0x06001B7A RID: 7034 RVA: 0x00030EC0 File Offset: 0x0002F0C0
		public void LogError(string tag, object message)
		{
			bool flag = this.IsLogTypeAllowed(LogType.Error);
			if (flag)
			{
				this.logHandler.LogFormat(LogType.Error, null, "{0}: {1}", new object[]
				{
					tag,
					Logger.GetString(message)
				});
			}
		}

		// Token: 0x06001B7B RID: 7035 RVA: 0x00030F00 File Offset: 0x0002F100
		public void LogError(string tag, object message, Object context)
		{
			bool flag = this.IsLogTypeAllowed(LogType.Error);
			if (flag)
			{
				this.logHandler.LogFormat(LogType.Error, context, "{0}: {1}", new object[]
				{
					tag,
					Logger.GetString(message)
				});
			}
		}

		// Token: 0x06001B7C RID: 7036 RVA: 0x00030F40 File Offset: 0x0002F140
		public void LogException(Exception exception)
		{
			bool logEnabled = this.logEnabled;
			if (logEnabled)
			{
				this.logHandler.LogException(exception, null);
			}
		}

		// Token: 0x06001B7D RID: 7037 RVA: 0x00030F68 File Offset: 0x0002F168
		public void LogException(Exception exception, Object context)
		{
			bool logEnabled = this.logEnabled;
			if (logEnabled)
			{
				this.logHandler.LogException(exception, context);
			}
		}

		// Token: 0x06001B7E RID: 7038 RVA: 0x00030F90 File Offset: 0x0002F190
		public void LogFormat(LogType logType, string format, params object[] args)
		{
			bool flag = this.IsLogTypeAllowed(logType);
			if (flag)
			{
				this.logHandler.LogFormat(logType, null, format, args);
			}
		}

		// Token: 0x06001B7F RID: 7039 RVA: 0x00030FBC File Offset: 0x0002F1BC
		public void LogFormat(LogType logType, Object context, string format, params object[] args)
		{
			bool flag = this.IsLogTypeAllowed(logType);
			if (flag)
			{
				this.logHandler.LogFormat(logType, context, format, args);
			}
		}

		// Token: 0x04000861 RID: 2145
		private const string kNoTagFormat = "{0}";

		// Token: 0x04000862 RID: 2146
		private const string kTagFormat = "{0}: {1}";
	}
}
