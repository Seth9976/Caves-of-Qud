using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading;
using AiUnity.Common.InternalLog;
using AiUnity.Common.Patterns;
using AiUnity.NLog.Core.Common;
using AiUnity.NLog.Core.Config;
using AiUnity.NLog.Core.Filters;
using AiUnity.NLog.Core.Internal;
using AiUnity.NLog.Core.Targets;

namespace AiUnity.NLog.Core
{
	// Token: 0x0200020D RID: 525
	internal static class LoggerEngine
	{
		// Token: 0x1700020E RID: 526
		// (get) Token: 0x06001327 RID: 4903 RVA: 0x0006FB73 File Offset: 0x0006DD73
		private static IInternalLogger Logger
		{
			get
			{
				return Singleton<NLogInternalLogger>.Instance;
			}
		}

		// Token: 0x06001328 RID: 4904 RVA: 0x0006FB7C File Offset: 0x0006DD7C
		internal static void Write(Type loggerType, TargetWithFilterChain targets, LogEventInfo logEvent, LogFactory factory)
		{
			if (targets == null)
			{
				return;
			}
			StackTraceUsage stackTraceUsage = targets.GetStackTraceUsage();
			if (stackTraceUsage != StackTraceUsage.None && !logEvent.HasStackTrace)
			{
				StackTrace stackTrace = new StackTrace(0, stackTraceUsage == StackTraceUsage.WithSource);
				int num = LoggerEngine.FindCallingMethodOnStackTrace(stackTrace, loggerType);
				logEvent.SetStackTrace(stackTrace, num);
			}
			int originalThreadId = Thread.CurrentThread.ManagedThreadId;
			AsyncContinuation asyncContinuation = delegate(Exception ex)
			{
				if (ex != null && factory.ThrowExceptions && Thread.CurrentThread.ManagedThreadId == originalThreadId)
				{
					throw new NLogRuntimeException("Exception occurred in NLog", ex);
				}
			};
			TargetWithFilterChain targetWithFilterChain = targets;
			while (targetWithFilterChain != null && LoggerEngine.WriteToTargetWithFilterChain(targetWithFilterChain, logEvent, asyncContinuation))
			{
				targetWithFilterChain = targetWithFilterChain.NextInChain;
			}
		}

		// Token: 0x06001329 RID: 4905 RVA: 0x0006FC04 File Offset: 0x0006DE04
		private static int FindCallingMethodOnStackTrace(StackTrace stackTrace, Type loggerType)
		{
			int? num = null;
			if (loggerType != null)
			{
				for (int i = 0; i < stackTrace.FrameCount; i++)
				{
					MethodBase method = stackTrace.GetFrame(i).GetMethod();
					if (method.DeclaringType == loggerType || (method.DeclaringType != null && (LoggerEngine.SkipAssembly(method.DeclaringType.Assembly) || LoggerEngine.SkipNameSpace(method.DeclaringType.Namespace))))
					{
						num = new int?(i + 1);
					}
					else if (num != null)
					{
						break;
					}
				}
			}
			int? num2 = num;
			int num3 = stackTrace.FrameCount;
			if ((num2.GetValueOrDefault() == num3) & (num2 != null))
			{
				num = null;
			}
			if (num == null)
			{
				for (int j = 0; j < stackTrace.FrameCount; j++)
				{
					MethodBase method2 = stackTrace.GetFrame(j).GetMethod();
					Assembly assembly = null;
					if (method2.DeclaringType != null)
					{
						assembly = method2.DeclaringType.Assembly;
					}
					if (LoggerEngine.SkipAssembly(assembly))
					{
						num = new int?(j + 1);
					}
					else
					{
						num2 = num;
						num3 = 0;
						if (!((num2.GetValueOrDefault() == num3) & (num2 != null)))
						{
							break;
						}
					}
				}
			}
			return num.GetValueOrDefault();
		}

		// Token: 0x0600132A RID: 4906 RVA: 0x0006FD40 File Offset: 0x0006DF40
		private static bool SkipAssembly(Assembly assembly)
		{
			if (assembly != null)
			{
				if (assembly.GetModules().Any<Module>((Module m) => m.Name.Equals("NLog.dll")))
				{
					return true;
				}
			}
			return assembly == LoggerEngine.mscorlibAssembly || assembly == LoggerEngine.systemAssembly || Singleton<NLogManager>.Instance.HiddenAssemblies.Contains(assembly);
		}

		// Token: 0x0600132B RID: 4907 RVA: 0x0006FDB8 File Offset: 0x0006DFB8
		private static bool SkipNameSpace(string nameSpace)
		{
			return !string.IsNullOrEmpty(nameSpace) && (nameSpace.StartsWith("AiUnity.NLog.Core") || nameSpace.StartsWith("AiUnity.CLog.Core") || nameSpace.StartsWith("AiUnity.Common") || nameSpace.StartsWith("UnityEngine") || Singleton<NLogManager>.Instance.HiddenNameSpaces.Any<string>((string n) => n.StartsWith(nameSpace)));
		}

		// Token: 0x0600132C RID: 4908 RVA: 0x0006FE50 File Offset: 0x0006E050
		private static bool WriteToTargetWithFilterChain(TargetWithFilterChain targetListHead, LogEventInfo logEvent, AsyncContinuation onException)
		{
			Target target = targetListHead.Target;
			FilterResult filterResult = LoggerEngine.GetFilterResult(targetListHead.FilterChain, logEvent);
			if (filterResult == FilterResult.Ignore || filterResult == FilterResult.IgnoreFinal)
			{
				if (LoggerEngine.Logger.IsDebugEnabled)
				{
					LoggerEngine.Logger.Debug("{0}.{1} Rejecting message because of a filter.", new object[] { logEvent.LoggerName, logEvent.Level });
				}
				return filterResult != FilterResult.IgnoreFinal;
			}
			target.WriteAsyncLogEvent(logEvent.WithContinuation(onException));
			return filterResult != FilterResult.LogFinal;
		}

		// Token: 0x0600132D RID: 4909 RVA: 0x0006FED0 File Offset: 0x0006E0D0
		private static FilterResult GetFilterResult(IEnumerable<Filter> filterChain, LogEventInfo logEvent)
		{
			FilterResult filterResult = FilterResult.Neutral;
			FilterResult filterResult2;
			try
			{
				foreach (Filter filter in filterChain)
				{
					filterResult = filter.GetFilterResult(logEvent);
					if (filterResult != FilterResult.Neutral)
					{
						break;
					}
				}
				filterResult2 = filterResult;
			}
			catch (Exception ex)
			{
				if (ex.MustBeRethrown())
				{
					throw;
				}
				LoggerEngine.Logger.Warn("Exception during filter evaluation: {0}", new object[] { ex });
				filterResult2 = FilterResult.Ignore;
			}
			return filterResult2;
		}

		// Token: 0x04000CAF RID: 3247
		private const int StackTraceSkipMethods = 0;

		// Token: 0x04000CB0 RID: 3248
		private static readonly Assembly mscorlibAssembly = typeof(string).Assembly;

		// Token: 0x04000CB1 RID: 3249
		private static readonly Assembly systemAssembly = typeof(Debug).Assembly;
	}
}
