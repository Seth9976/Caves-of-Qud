using System;
using System.Diagnostics;
using AiUnity.Common.Log;

namespace AiUnity.NLog.Core.Fluent
{
	// Token: 0x02000281 RID: 641
	public class LogBuilder
	{
		// Token: 0x060016EC RID: 5868 RVA: 0x00079DAE File Offset: 0x00077FAE
		public LogBuilder(NLogger logger)
			: this(logger, LogLevels.Debug)
		{
		}

		// Token: 0x060016ED RID: 5869 RVA: 0x00079DBC File Offset: 0x00077FBC
		public LogBuilder(NLogger logger, LogLevels logLevel)
		{
			if (logger == null)
			{
				throw new ArgumentNullException("logger");
			}
			this._logger = logger;
			this._logEvent = new LogEventInfo
			{
				Level = logLevel,
				LoggerName = logger.Name,
				TimeStamp = DateTime.Now
			};
		}

		// Token: 0x1700030F RID: 783
		// (get) Token: 0x060016EE RID: 5870 RVA: 0x00079E0D File Offset: 0x0007800D
		public LogEventInfo LogEventInfo
		{
			get
			{
				return this._logEvent;
			}
		}

		// Token: 0x060016EF RID: 5871 RVA: 0x00079E15 File Offset: 0x00078015
		public LogBuilder Exception(Exception exception)
		{
			this._logEvent.Exception = exception;
			return this;
		}

		// Token: 0x060016F0 RID: 5872 RVA: 0x00079E24 File Offset: 0x00078024
		public LogBuilder Level(LogLevels logLevel)
		{
			this._logEvent.Level = logLevel;
			return this;
		}

		// Token: 0x060016F1 RID: 5873 RVA: 0x00079E33 File Offset: 0x00078033
		public LogBuilder LoggerName(string loggerName)
		{
			this._logEvent.LoggerName = loggerName;
			return this;
		}

		// Token: 0x060016F2 RID: 5874 RVA: 0x00079E42 File Offset: 0x00078042
		public LogBuilder Message(string message)
		{
			this._logEvent.Message = message;
			return this;
		}

		// Token: 0x060016F3 RID: 5875 RVA: 0x00079E51 File Offset: 0x00078051
		public LogBuilder Message(string format, object arg0)
		{
			this._logEvent.Message = format;
			this._logEvent.Parameters = new object[] { arg0 };
			return this;
		}

		// Token: 0x060016F4 RID: 5876 RVA: 0x00079E75 File Offset: 0x00078075
		public LogBuilder Message(string format, object arg0, object arg1)
		{
			this._logEvent.Message = format;
			this._logEvent.Parameters = new object[] { arg0, arg1 };
			return this;
		}

		// Token: 0x060016F5 RID: 5877 RVA: 0x00079E9D File Offset: 0x0007809D
		public LogBuilder Message(string format, object arg0, object arg1, object arg2)
		{
			this._logEvent.Message = format;
			this._logEvent.Parameters = new object[] { arg0, arg1, arg2 };
			return this;
		}

		// Token: 0x060016F6 RID: 5878 RVA: 0x00079ECA File Offset: 0x000780CA
		public LogBuilder Message(string format, object arg0, object arg1, object arg2, object arg3)
		{
			this._logEvent.Message = format;
			this._logEvent.Parameters = new object[] { arg0, arg1, arg2, arg3 };
			return this;
		}

		// Token: 0x060016F7 RID: 5879 RVA: 0x00079EFC File Offset: 0x000780FC
		public LogBuilder Message(string format, params object[] args)
		{
			this._logEvent.Message = format;
			this._logEvent.Parameters = args;
			return this;
		}

		// Token: 0x060016F8 RID: 5880 RVA: 0x00079F17 File Offset: 0x00078117
		public LogBuilder Message(IFormatProvider provider, string format, params object[] args)
		{
			this._logEvent.FormatProvider = provider;
			this._logEvent.Message = format;
			this._logEvent.Parameters = args;
			return this;
		}

		// Token: 0x060016F9 RID: 5881 RVA: 0x00079F3E File Offset: 0x0007813E
		public LogBuilder Property(object name, object value)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			this._logEvent.Properties[name] = value;
			return this;
		}

		// Token: 0x060016FA RID: 5882 RVA: 0x00079F61 File Offset: 0x00078161
		public LogBuilder TimeStamp(DateTime timeStamp)
		{
			this._logEvent.TimeStamp = timeStamp;
			return this;
		}

		// Token: 0x060016FB RID: 5883 RVA: 0x00079F70 File Offset: 0x00078170
		public LogBuilder StackTrace(StackTrace stackTrace, int userStackFrame)
		{
			this._logEvent.SetStackTrace(stackTrace, userStackFrame);
			return this;
		}

		// Token: 0x060016FC RID: 5884 RVA: 0x00079F80 File Offset: 0x00078180
		public void Write()
		{
			this._logger.Log(this._logEvent);
		}

		// Token: 0x060016FD RID: 5885 RVA: 0x00079F93 File Offset: 0x00078193
		public void WriteIf(Func<bool> condition)
		{
			if (condition == null || !condition())
			{
				return;
			}
			this._logger.Log(this._logEvent);
		}

		// Token: 0x060016FE RID: 5886 RVA: 0x00079FB2 File Offset: 0x000781B2
		public void WriteIf(bool condition)
		{
			if (!condition)
			{
				return;
			}
			this._logger.Log(this._logEvent);
		}

		// Token: 0x04000DFD RID: 3581
		private readonly LogEventInfo _logEvent;

		// Token: 0x04000DFE RID: 3582
		private readonly NLogger _logger;
	}
}
