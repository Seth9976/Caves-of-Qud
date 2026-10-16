using System;
using AiUnity.Common.Log;

namespace AiUnity.Common.InternalLog
{
	// Token: 0x020002D9 RID: 729
	public interface IInternalLogger
	{
		// Token: 0x17000367 RID: 871
		// (get) Token: 0x060018F7 RID: 6391
		// (set) Token: 0x060018F8 RID: 6392
		LogLevels InternalLogLevel { get; set; }

		// Token: 0x17000368 RID: 872
		// (get) Token: 0x060018F9 RID: 6393
		bool IsAssertEnabled { get; }

		// Token: 0x17000369 RID: 873
		// (get) Token: 0x060018FA RID: 6394
		bool IsDebugEnabled { get; }

		// Token: 0x1700036A RID: 874
		// (get) Token: 0x060018FB RID: 6395
		bool IsErrorEnabled { get; }

		// Token: 0x1700036B RID: 875
		// (get) Token: 0x060018FC RID: 6396
		bool IsFatalEnabled { get; }

		// Token: 0x1700036C RID: 876
		// (get) Token: 0x060018FD RID: 6397
		bool IsInfoEnabled { get; }

		// Token: 0x1700036D RID: 877
		// (get) Token: 0x060018FE RID: 6398
		bool IsTraceEnabled { get; }

		// Token: 0x1700036E RID: 878
		// (get) Token: 0x060018FF RID: 6399
		bool IsWarnEnabled { get; }

		// Token: 0x06001900 RID: 6400
		void Assert(string message, params object[] args);

		// Token: 0x06001901 RID: 6401
		void Assert(Exception e, string message, params object[] args);

		// Token: 0x06001902 RID: 6402
		void Assert(bool condition, string message, params object[] args);

		// Token: 0x06001903 RID: 6403
		void Debug(string message, params object[] args);

		// Token: 0x06001904 RID: 6404
		void Error(string message, params object[] args);

		// Token: 0x06001905 RID: 6405
		void Error(Exception e, string message, params object[] args);

		// Token: 0x06001906 RID: 6406
		void Fatal(string message, params object[] args);

		// Token: 0x06001907 RID: 6407
		void Fatal(Exception e, string message, params object[] args);

		// Token: 0x06001908 RID: 6408
		void Info(string message, params object[] args);

		// Token: 0x06001909 RID: 6409
		void Log(LogLevels levels, string message, params object[] args);

		// Token: 0x0600190A RID: 6410
		void Trace(string message, params object[] args);

		// Token: 0x0600190B RID: 6411
		void Warn(string message, params object[] args);

		// Token: 0x0600190C RID: 6412
		void Warn(Exception e, string message, params object[] args);
	}
}
