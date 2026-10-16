using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using AiUnity.Common.InternalLog;
using AiUnity.Common.Log;
using AiUnity.Common.Patterns;
using AiUnity.NLog.Core.Common;
using AiUnity.NLog.Core.Internal;
using AiUnity.NLog.Core.Time;
using UnityEngine;

namespace AiUnity.NLog.Core.Config
{
	// Token: 0x0200029C RID: 668
	[NLogConfigurationItem]
	public class UnityLogListener : ISupportsInitialize
	{
		// Token: 0x17000337 RID: 823
		// (get) Token: 0x0600179C RID: 6044 RVA: 0x0007B6F3 File Offset: 0x000798F3
		private static IInternalLogger Logger
		{
			get
			{
				return Singleton<NLogInternalLogger>.Instance;
			}
		}

		// Token: 0x0600179D RID: 6045 RVA: 0x0007B6FA File Offset: 0x000798FA
		void ISupportsInitialize.Close()
		{
			UnityLogListener.Logger.Info("Removing UnityLogListener", Array.Empty<object>());
			Application.logMessageReceived -= this.HandleLog;
		}

		// Token: 0x0600179E RID: 6046 RVA: 0x0007B721 File Offset: 0x00079921
		void ISupportsInitialize.Initialize(LoggingConfiguration configuration)
		{
			UnityLogListener.Logger.Info("Adding UnityLogListener", Array.Empty<object>());
			Application.logMessageReceived += this.HandleLog;
		}

		// Token: 0x0600179F RID: 6047 RVA: 0x0007B748 File Offset: 0x00079948
		private void HandleLog(string message, string stackTrace, LogType type)
		{
			LogLevels logLevels = LogLevels.Debug;
			switch (type)
			{
			case LogType.Error:
				logLevels = LogLevels.Error;
				break;
			case LogType.Assert:
				logLevels = LogLevels.Assert;
				break;
			case LogType.Warning:
				logLevels = LogLevels.Warn;
				break;
			case LogType.Log:
				logLevels = LogLevels.Debug;
				break;
			case LogType.Exception:
				logLevels = LogLevels.Assert;
				break;
			}
			LogEventInfo logEventInfo = this.CreateLogEventInfo(logLevels, message, null);
			Singleton<NLogManager>.Instance.GetLogger(logEventInfo.LoggerName, logEventInfo.Context, logEventInfo.FormatProvider).Log(logEventInfo);
		}

		// Token: 0x060017A0 RID: 6048 RVA: 0x0007B7B4 File Offset: 0x000799B4
		protected virtual LogEventInfo CreateLogEventInfo(LogLevels logLevel, string message, object[] arguments)
		{
			string text = "Unity";
			try
			{
				StackFrame stackFrame = new StackTrace(true).GetFrames().SkipWhile<StackFrame>((StackFrame s) => s.GetMethod().DeclaringType.Namespace.StartsWith("UnityEngine") || s.GetMethod().DeclaringType.Name.Equals("UnityLogListener")).FirstOrDefault<StackFrame>();
				if (stackFrame != null)
				{
					Type declaringType = stackFrame.GetMethod().DeclaringType;
					if (declaringType.Namespace.StartsWith("AiUnity.NLog.Core") || declaringType.Namespace.StartsWith("AiUnity.CLog.Core") || declaringType.Namespace.StartsWith("AiUnity.Common"))
					{
						return null;
					}
					text = declaringType.FullName;
				}
			}
			catch
			{
				text = "Unity";
			}
			return new LogEventInfo(logLevel, text, null, null, message, arguments, null)
			{
				TimeStamp = TimeSource.Current.Time,
				UnityLogListener = this,
				FromUnityLogListener = true
			};
		}

		// Token: 0x04000E33 RID: 3635
		public List<StackFrame> stackFrames;
	}
}
