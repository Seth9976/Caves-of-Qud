using System;
using System.IO;
using System.Threading;

namespace NUnit.Framework.Internal
{
	// Token: 0x0200006D RID: 109
	public class Logger : ILogger
	{
		// Token: 0x060003A3 RID: 931 RVA: 0x00008250 File Offset: 0x00006450
		public Logger(string name, InternalTraceLevel level, TextWriter writer)
		{
			this.maxLevel = level;
			this.writer = writer;
			this.name = name;
			this.fullname = name;
			int num = this.fullname.LastIndexOf('.');
			if (num >= 0)
			{
				this.name = this.fullname.Substring(num + 1);
			}
		}

		// Token: 0x060003A4 RID: 932 RVA: 0x000082A7 File Offset: 0x000064A7
		public void Error(string message)
		{
			this.Log(InternalTraceLevel.Error, message);
		}

		// Token: 0x060003A5 RID: 933 RVA: 0x000082B1 File Offset: 0x000064B1
		public void Error(string message, params object[] args)
		{
			this.Log(InternalTraceLevel.Error, message, args);
		}

		// Token: 0x060003A6 RID: 934 RVA: 0x000082BC File Offset: 0x000064BC
		public void Warning(string message)
		{
			this.Log(InternalTraceLevel.Warning, message);
		}

		// Token: 0x060003A7 RID: 935 RVA: 0x000082C6 File Offset: 0x000064C6
		public void Warning(string message, params object[] args)
		{
			this.Log(InternalTraceLevel.Warning, message, args);
		}

		// Token: 0x060003A8 RID: 936 RVA: 0x000082D1 File Offset: 0x000064D1
		public void Info(string message)
		{
			this.Log(InternalTraceLevel.Info, message);
		}

		// Token: 0x060003A9 RID: 937 RVA: 0x000082DB File Offset: 0x000064DB
		public void Info(string message, params object[] args)
		{
			this.Log(InternalTraceLevel.Info, message, args);
		}

		// Token: 0x060003AA RID: 938 RVA: 0x000082E6 File Offset: 0x000064E6
		public void Debug(string message)
		{
			this.Log(InternalTraceLevel.Debug, message);
		}

		// Token: 0x060003AB RID: 939 RVA: 0x000082F0 File Offset: 0x000064F0
		public void Debug(string message, params object[] args)
		{
			this.Log(InternalTraceLevel.Debug, message, args);
		}

		// Token: 0x060003AC RID: 940 RVA: 0x000082FB File Offset: 0x000064FB
		private void Log(InternalTraceLevel level, string message)
		{
			if (this.writer != null && this.maxLevel >= level)
			{
				this.WriteLog(level, message);
			}
		}

		// Token: 0x060003AD RID: 941 RVA: 0x00008316 File Offset: 0x00006516
		private void Log(InternalTraceLevel level, string format, params object[] args)
		{
			if (this.maxLevel >= level)
			{
				this.WriteLog(level, string.Format(format, args));
			}
		}

		// Token: 0x060003AE RID: 942 RVA: 0x00008330 File Offset: 0x00006530
		private void WriteLog(InternalTraceLevel level, string message)
		{
			this.writer.WriteLine(Logger.TRACE_FMT, new object[]
			{
				DateTime.Now.ToString(Logger.TIME_FMT),
				(level == InternalTraceLevel.Debug) ? "Debug" : level.ToString(),
				Thread.CurrentThread.ManagedThreadId,
				this.name,
				message
			});
		}

		// Token: 0x04000083 RID: 131
		private static readonly string TIME_FMT = "HH:mm:ss.fff";

		// Token: 0x04000084 RID: 132
		private static readonly string TRACE_FMT = "{0} {1,-5} [{2,2}] {3}: {4}";

		// Token: 0x04000085 RID: 133
		private string name;

		// Token: 0x04000086 RID: 134
		private string fullname;

		// Token: 0x04000087 RID: 135
		private InternalTraceLevel maxLevel;

		// Token: 0x04000088 RID: 136
		private TextWriter writer;
	}
}
