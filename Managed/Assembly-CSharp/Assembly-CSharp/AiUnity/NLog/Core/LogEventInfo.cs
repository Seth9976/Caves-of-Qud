using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using AiUnity.Common.InternalLog;
using AiUnity.Common.Log;
using AiUnity.Common.Patterns;
using AiUnity.NLog.Core.Common;
using AiUnity.NLog.Core.Internal;
using AiUnity.NLog.Core.Layouts;
using AiUnity.NLog.Core.Time;
using UnityEngine;

namespace AiUnity.NLog.Core
{
	// Token: 0x0200020A RID: 522
	public class LogEventInfo
	{
		// Token: 0x060012BF RID: 4799 RVA: 0x0006EB0B File Offset: 0x0006CD0B
		public LogEventInfo()
		{
		}

		// Token: 0x060012C0 RID: 4800 RVA: 0x0006EB20 File Offset: 0x0006CD20
		public LogEventInfo(LogLevels level, string loggerName, global::UnityEngine.Object context, IFormatProvider formatProvider, string message, object[] parameters = null, Exception exception = null)
		{
			this.TimeStamp = TimeSource.Current.Time;
			this.Level = level;
			this.LoggerName = loggerName;
			this.Message = message;
			this.Parameters = parameters;
			this.FormatProvider = formatProvider;
			this.Exception = exception;
			this.SequenceID = Interlocked.Increment(ref LogEventInfo.globalSequenceId);
			this.Context = context;
			if (LogEventInfo.NeedToPreformatMessage(parameters))
			{
				this.CalcFormattedMessage();
			}
		}

		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x060012C1 RID: 4801 RVA: 0x0006EBA2 File Offset: 0x0006CDA2
		private static IInternalLogger Logger
		{
			get
			{
				return Singleton<NLogInternalLogger>.Instance;
			}
		}

		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x060012C2 RID: 4802 RVA: 0x0006EBA9 File Offset: 0x0006CDA9
		// (set) Token: 0x060012C3 RID: 4803 RVA: 0x0006EBB1 File Offset: 0x0006CDB1
		public int SequenceID { get; private set; }

		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x060012C4 RID: 4804 RVA: 0x0006EBBA File Offset: 0x0006CDBA
		// (set) Token: 0x060012C5 RID: 4805 RVA: 0x0006EBC2 File Offset: 0x0006CDC2
		public DateTime TimeStamp { get; set; }

		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x060012C6 RID: 4806 RVA: 0x0006EBCB File Offset: 0x0006CDCB
		// (set) Token: 0x060012C7 RID: 4807 RVA: 0x0006EBD3 File Offset: 0x0006CDD3
		public LogLevels Level { get; set; }

		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x060012C8 RID: 4808 RVA: 0x0006EBDC File Offset: 0x0006CDDC
		public bool HasStackTrace
		{
			get
			{
				return this.StackTrace != null;
			}
		}

		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x060012C9 RID: 4809 RVA: 0x0006EBE7 File Offset: 0x0006CDE7
		public StackFrame UserStackFrame
		{
			get
			{
				if (this.StackTrace == null)
				{
					return null;
				}
				return this.StackTrace.GetFrame(this.UserStackFrameNumber);
			}
		}

		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x060012CA RID: 4810 RVA: 0x0006EC04 File Offset: 0x0006CE04
		// (set) Token: 0x060012CB RID: 4811 RVA: 0x0006EC0C File Offset: 0x0006CE0C
		public int UserStackFrameNumber { get; private set; }

		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x060012CC RID: 4812 RVA: 0x0006EC15 File Offset: 0x0006CE15
		// (set) Token: 0x060012CD RID: 4813 RVA: 0x0006EC1D File Offset: 0x0006CE1D
		public StackTrace StackTrace { get; private set; }

		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x060012CE RID: 4814 RVA: 0x0006EC26 File Offset: 0x0006CE26
		// (set) Token: 0x060012CF RID: 4815 RVA: 0x0006EC2E File Offset: 0x0006CE2E
		public Exception Exception { get; set; }

		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x060012D0 RID: 4816 RVA: 0x0006EC37 File Offset: 0x0006CE37
		// (set) Token: 0x060012D1 RID: 4817 RVA: 0x0006EC3F File Offset: 0x0006CE3F
		public string LoggerName { get; set; }

		// Token: 0x170001FA RID: 506
		// (get) Token: 0x060012D2 RID: 4818 RVA: 0x0006EC48 File Offset: 0x0006CE48
		// (set) Token: 0x060012D3 RID: 4819 RVA: 0x0006EC50 File Offset: 0x0006CE50
		public string Message { get; set; }

		// Token: 0x170001FB RID: 507
		// (get) Token: 0x060012D4 RID: 4820 RVA: 0x0006EC59 File Offset: 0x0006CE59
		// (set) Token: 0x060012D5 RID: 4821 RVA: 0x0006EC61 File Offset: 0x0006CE61
		public object[] Parameters { get; set; }

		// Token: 0x170001FC RID: 508
		// (get) Token: 0x060012D6 RID: 4822 RVA: 0x0006EC6A File Offset: 0x0006CE6A
		// (set) Token: 0x060012D7 RID: 4823 RVA: 0x0006EC72 File Offset: 0x0006CE72
		public IFormatProvider FormatProvider { get; set; }

		// Token: 0x170001FD RID: 509
		// (get) Token: 0x060012D8 RID: 4824 RVA: 0x0006EC7B File Offset: 0x0006CE7B
		public string FormattedMessage
		{
			get
			{
				if (this.formattedMessage == null)
				{
					this.CalcFormattedMessage();
				}
				return this.formattedMessage;
			}
		}

		// Token: 0x170001FE RID: 510
		// (get) Token: 0x060012D9 RID: 4825 RVA: 0x0006EC91 File Offset: 0x0006CE91
		public IDictionary<object, object> Properties
		{
			get
			{
				if (this.properties == null)
				{
					this.InitEventContext();
				}
				return this.properties;
			}
		}

		// Token: 0x170001FF RID: 511
		// (get) Token: 0x060012DA RID: 4826 RVA: 0x0006ECA7 File Offset: 0x0006CEA7
		// (set) Token: 0x060012DB RID: 4827 RVA: 0x0006ECAF File Offset: 0x0006CEAF
		public global::UnityEngine.Object Context { get; private set; }

		// Token: 0x17000200 RID: 512
		// (get) Token: 0x060012DC RID: 4828 RVA: 0x0006ECB8 File Offset: 0x0006CEB8
		// (set) Token: 0x060012DD RID: 4829 RVA: 0x0006ECC0 File Offset: 0x0006CEC0
		public bool FromUnityLogListener { get; set; }

		// Token: 0x17000201 RID: 513
		// (get) Token: 0x060012DE RID: 4830 RVA: 0x0006ECC9 File Offset: 0x0006CEC9
		// (set) Token: 0x060012DF RID: 4831 RVA: 0x0006ECD1 File Offset: 0x0006CED1
		internal ISupportsInitialize UnityLogListener { get; set; }

		// Token: 0x060012E0 RID: 4832 RVA: 0x0006ECDA File Offset: 0x0006CEDA
		public static LogEventInfo CreateNullEvent()
		{
			return new LogEventInfo((LogLevels)0, string.Empty, null, null, string.Empty, null, null);
		}

		// Token: 0x060012E1 RID: 4833 RVA: 0x0006ECF0 File Offset: 0x0006CEF0
		public static LogEventInfo Create(LogLevels logLevel, string loggerName, global::UnityEngine.Object context, IFormatProvider formatProvider, string message, object[] parameters = null, Exception exception = null)
		{
			return new LogEventInfo(logLevel, loggerName, context, formatProvider, message, parameters, exception);
		}

		// Token: 0x060012E2 RID: 4834 RVA: 0x0006ED01 File Offset: 0x0006CF01
		public AsyncLogEventInfo WithContinuation(AsyncContinuation asyncContinuation)
		{
			return new AsyncLogEventInfo(this, asyncContinuation);
		}

		// Token: 0x060012E3 RID: 4835 RVA: 0x0006ED0C File Offset: 0x0006CF0C
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				"Log Event: Logger='",
				this.LoggerName,
				"' Level=",
				this.Level.ToString(),
				" Message='",
				this.FormattedMessage,
				"' SequenceID=",
				this.SequenceID.ToString()
			});
		}

		// Token: 0x060012E4 RID: 4836 RVA: 0x0006ED7E File Offset: 0x0006CF7E
		public void SetStackTrace(StackTrace stackTrace, int userStackFrame)
		{
			this.StackTrace = stackTrace;
			this.UserStackFrameNumber = userStackFrame;
		}

		// Token: 0x060012E5 RID: 4837 RVA: 0x0006ED90 File Offset: 0x0006CF90
		internal string AddCachedLayoutValue(Layout layout, string value)
		{
			object obj = this.layoutCacheLock;
			lock (obj)
			{
				if (this.layoutCache == null)
				{
					this.layoutCache = new Dictionary<Layout, string>();
				}
				this.layoutCache[layout] = value;
			}
			return value;
		}

		// Token: 0x060012E6 RID: 4838 RVA: 0x0006EDEC File Offset: 0x0006CFEC
		internal bool TryGetCachedLayoutValue(Layout layout, out string value)
		{
			object obj = this.layoutCacheLock;
			bool flag2;
			lock (obj)
			{
				if (this.layoutCache == null)
				{
					value = null;
					flag2 = false;
				}
				else
				{
					flag2 = this.layoutCache.TryGetValue(layout, out value);
				}
			}
			return flag2;
		}

		// Token: 0x060012E7 RID: 4839 RVA: 0x0006EE44 File Offset: 0x0006D044
		private static bool NeedToPreformatMessage(object[] parameters)
		{
			return parameters != null && parameters.Length != 0 && (parameters.Length > 3 || !LogEventInfo.IsSafeToDeferFormatting(parameters[0]) || (parameters.Length >= 2 && !LogEventInfo.IsSafeToDeferFormatting(parameters[1])) || (parameters.Length >= 3 && !LogEventInfo.IsSafeToDeferFormatting(parameters[2])));
		}

		// Token: 0x060012E8 RID: 4840 RVA: 0x0006EE93 File Offset: 0x0006D093
		private static bool IsSafeToDeferFormatting(object value)
		{
			return value == null || value.GetType().IsPrimitive || value is string;
		}

		// Token: 0x060012E9 RID: 4841 RVA: 0x0006EEB4 File Offset: 0x0006D0B4
		private void CalcFormattedMessage()
		{
			if (this.Parameters == null || this.Parameters.Length == 0)
			{
				this.formattedMessage = this.Message;
				return;
			}
			try
			{
				this.formattedMessage = string.Format(this.FormatProvider ?? Singleton<NLogManager>.Instance.DefaultCultureInfo(), this.Message, this.Parameters);
			}
			catch (Exception ex)
			{
				this.formattedMessage = this.Message;
				if (ex.MustBeRethrown())
				{
					throw;
				}
				LogEventInfo.Logger.Warn("Error when formatting a message: {0}", new object[] { ex });
			}
		}

		// Token: 0x060012EA RID: 4842 RVA: 0x0006EF54 File Offset: 0x0006D154
		private void InitEventContext()
		{
			this.properties = new Dictionary<object, object>();
		}

		// Token: 0x04000C8B RID: 3211
		public static readonly DateTime ZeroDate = DateTime.UtcNow;

		// Token: 0x04000C8C RID: 3212
		private static int globalSequenceId;

		// Token: 0x04000C8D RID: 3213
		private readonly object layoutCacheLock = new object();

		// Token: 0x04000C8E RID: 3214
		private string formattedMessage;

		// Token: 0x04000C8F RID: 3215
		private IDictionary<Layout, string> layoutCache;

		// Token: 0x04000C90 RID: 3216
		private IDictionary<object, object> properties;
	}
}
