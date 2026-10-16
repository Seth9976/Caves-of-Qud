using System;
using System.Diagnostics;
using AiUnity.Common.Log;
using AiUnity.NLog.Core.Internal;
using UnityEngine;

namespace AiUnity.NLog.Core
{
	// Token: 0x02000210 RID: 528
	public class NLogger : AiUnity.Common.Log.ILogger
	{
		// Token: 0x1700020F RID: 527
		// (get) Token: 0x06001335 RID: 4917 RVA: 0x0006FFB8 File Offset: 0x0006E1B8
		// (set) Token: 0x06001336 RID: 4918 RVA: 0x0006FFC0 File Offset: 0x0006E1C0
		public global::UnityEngine.Object Context { get; set; }

		// Token: 0x17000210 RID: 528
		// (get) Token: 0x06001337 RID: 4919 RVA: 0x0006FFC9 File Offset: 0x0006E1C9
		// (set) Token: 0x06001338 RID: 4920 RVA: 0x0006FFD1 File Offset: 0x0006E1D1
		public LogFactory Factory { get; private set; }

		// Token: 0x17000211 RID: 529
		// (get) Token: 0x06001339 RID: 4921 RVA: 0x0006FFDA File Offset: 0x0006E1DA
		// (set) Token: 0x0600133A RID: 4922 RVA: 0x0006FFE2 File Offset: 0x0006E1E2
		public IFormatProvider FormatProvider { get; protected set; }

		// Token: 0x17000212 RID: 530
		// (get) Token: 0x0600133B RID: 4923 RVA: 0x0006FFEB File Offset: 0x0006E1EB
		// (set) Token: 0x0600133C RID: 4924 RVA: 0x0006FFF3 File Offset: 0x0006E1F3
		public string Name { get; private set; }

		// Token: 0x14000006 RID: 6
		// (add) Token: 0x0600133D RID: 4925 RVA: 0x0006FFFC File Offset: 0x0006E1FC
		// (remove) Token: 0x0600133E RID: 4926 RVA: 0x00070034 File Offset: 0x0006E234
		public event EventHandler<EventArgs> LoggerReconfigured;

		// Token: 0x0600133F RID: 4927 RVA: 0x00070069 File Offset: 0x0006E269
		protected internal NLogger(global::UnityEngine.Object context = null, IFormatProvider formatProvider = null)
		{
			this.Context = context;
			this.FormatProvider = formatProvider;
		}

		// Token: 0x06001340 RID: 4928 RVA: 0x0007008F File Offset: 0x0006E28F
		internal void Initialize(string name, LoggerConfiguration loggerConfiguration, LogFactory factory)
		{
			this.Name = name;
			this.Factory = factory;
			this.SetConfiguration(loggerConfiguration);
		}

		// Token: 0x06001341 RID: 4929 RVA: 0x000700A8 File Offset: 0x0006E2A8
		internal void SetConfiguration(LoggerConfiguration newConfiguration)
		{
			this.configuration = newConfiguration;
			this.isTraceEnabled = newConfiguration.IsEnabled(LogLevels.Trace);
			this.isDebugEnabled = newConfiguration.IsEnabled(LogLevels.Debug);
			this.isInfoEnabled = newConfiguration.IsEnabled(LogLevels.Info);
			this.isWarnEnabled = newConfiguration.IsEnabled(LogLevels.Warn);
			this.isErrorEnabled = newConfiguration.IsEnabled(LogLevels.Error);
			this.isFatalEnabled = newConfiguration.IsEnabled(LogLevels.Fatal);
			EventHandler<EventArgs> loggerReconfigured = this.LoggerReconfigured;
			if (loggerReconfigured != null)
			{
				loggerReconfigured(this, new EventArgs());
			}
		}

		// Token: 0x06001342 RID: 4930 RVA: 0x00070134 File Offset: 0x0006E334
		internal void WriteToTargets(LogLevels level, global::UnityEngine.Object context, IFormatProvider formatProvider, string message, object[] args, Exception exception = null)
		{
			LogEventInfo logEventInfo = LogEventInfo.Create(level, this.Name, context, formatProvider, message, args, exception);
			this.WriteToTargets(logEventInfo);
		}

		// Token: 0x06001343 RID: 4931 RVA: 0x0007015D File Offset: 0x0006E35D
		internal void WriteToTargets(LogEventInfo logEventInfo)
		{
			LoggerEngine.Write(this.loggerType, this.GetTargetsForLevel(logEventInfo.Level), logEventInfo, this.Factory);
		}

		// Token: 0x06001344 RID: 4932 RVA: 0x0007017D File Offset: 0x0006E37D
		private TargetWithFilterChain GetTargetsForLevel(LogLevels level)
		{
			return this.configuration.GetTargetsForLevel(level);
		}

		// Token: 0x17000213 RID: 531
		// (get) Token: 0x06001345 RID: 4933 RVA: 0x0007018D File Offset: 0x0006E38D
		public bool IsTraceEnabled
		{
			get
			{
				return this.isTraceEnabled;
			}
		}

		// Token: 0x17000214 RID: 532
		// (get) Token: 0x06001346 RID: 4934 RVA: 0x00070197 File Offset: 0x0006E397
		public bool IsDebugEnabled
		{
			get
			{
				return this.isDebugEnabled;
			}
		}

		// Token: 0x17000215 RID: 533
		// (get) Token: 0x06001347 RID: 4935 RVA: 0x000701A1 File Offset: 0x0006E3A1
		public bool IsInfoEnabled
		{
			get
			{
				return this.isInfoEnabled;
			}
		}

		// Token: 0x17000216 RID: 534
		// (get) Token: 0x06001348 RID: 4936 RVA: 0x000701AB File Offset: 0x0006E3AB
		public bool IsWarnEnabled
		{
			get
			{
				return this.isWarnEnabled;
			}
		}

		// Token: 0x17000217 RID: 535
		// (get) Token: 0x06001349 RID: 4937 RVA: 0x000701B5 File Offset: 0x0006E3B5
		public bool IsErrorEnabled
		{
			get
			{
				return this.isErrorEnabled;
			}
		}

		// Token: 0x17000218 RID: 536
		// (get) Token: 0x0600134A RID: 4938 RVA: 0x000701BF File Offset: 0x0006E3BF
		public bool IsFatalEnabled
		{
			get
			{
				return this.isFatalEnabled;
			}
		}

		// Token: 0x17000219 RID: 537
		// (get) Token: 0x0600134B RID: 4939 RVA: 0x000701C9 File Offset: 0x0006E3C9
		public bool IsAssertEnabled
		{
			get
			{
				return this.IsEnabled(LogLevels.Assert);
			}
		}

		// Token: 0x0600134C RID: 4940 RVA: 0x000701D2 File Offset: 0x0006E3D2
		public bool IsEnabled(LogLevels level)
		{
			return this.GetTargetsForLevel(level) != null;
		}

		// Token: 0x0600134D RID: 4941 RVA: 0x000701DE File Offset: 0x0006E3DE
		public void Log(LogEventInfo logEvent)
		{
			if (this.IsEnabled(logEvent.Level))
			{
				this.WriteToTargets(logEvent);
			}
		}

		// Token: 0x0600134E RID: 4942 RVA: 0x000701F5 File Offset: 0x0006E3F5
		public void Log(LogLevels level, string message, params object[] args)
		{
			if (this.IsEnabled(level))
			{
				this.WriteToTargets(level, this.Context, this.FormatProvider, message, args, null);
			}
		}

		// Token: 0x0600134F RID: 4943 RVA: 0x00070216 File Offset: 0x0006E416
		public void Log(LogLevels level, global::UnityEngine.Object context, string message, params object[] args)
		{
			if (this.IsEnabled(level))
			{
				this.WriteToTargets(level, context, this.FormatProvider, message, args, null);
			}
		}

		// Token: 0x06001350 RID: 4944 RVA: 0x00070233 File Offset: 0x0006E433
		public void Log(LogLevels level, Exception exception, string message, params object[] args)
		{
			if (this.IsEnabled(level))
			{
				this.WriteToTargets(level, this.Context, this.FormatProvider, message, args, exception);
			}
		}

		// Token: 0x06001351 RID: 4945 RVA: 0x00070255 File Offset: 0x0006E455
		public void Log(LogLevels level, global::UnityEngine.Object context, Exception exception, string message, params object[] args)
		{
			if (this.IsEnabled(level))
			{
				this.WriteToTargets(level, context, this.FormatProvider, message, args, exception);
			}
		}

		// Token: 0x06001352 RID: 4946 RVA: 0x00070273 File Offset: 0x0006E473
		[Conditional("NLOG_ALL")]
		[Conditional("NLOG_TRACE")]
		[Conditional("UNITY_EDITOR")]
		public void Trace(string message, params object[] args)
		{
			if (this.IsTraceEnabled)
			{
				this.WriteToTargets(LogLevels.Trace, this.Context, this.FormatProvider, message, args, null);
			}
		}

		// Token: 0x06001353 RID: 4947 RVA: 0x00070294 File Offset: 0x0006E494
		[Conditional("NLOG_ALL")]
		[Conditional("NLOG_TRACE")]
		[Conditional("UNITY_EDITOR")]
		public void Trace(global::UnityEngine.Object context, string message, params object[] args)
		{
			if (this.IsTraceEnabled)
			{
				this.WriteToTargets(LogLevels.Trace, context, this.FormatProvider, message, args, null);
			}
		}

		// Token: 0x06001354 RID: 4948 RVA: 0x000702B0 File Offset: 0x0006E4B0
		[Conditional("NLOG_ALL")]
		[Conditional("NLOG_TRACE")]
		[Conditional("UNITY_EDITOR")]
		public void Trace(Exception exception, string message, params object[] args)
		{
			if (this.IsTraceEnabled)
			{
				this.WriteToTargets(LogLevels.Trace, this.Context, this.FormatProvider, message, args, exception);
			}
		}

		// Token: 0x06001355 RID: 4949 RVA: 0x000702D1 File Offset: 0x0006E4D1
		[Conditional("NLOG_ALL")]
		[Conditional("NLOG_TRACE")]
		[Conditional("UNITY_EDITOR")]
		public void Trace(Exception exception, global::UnityEngine.Object context, string message, params object[] args)
		{
			if (this.IsTraceEnabled)
			{
				this.WriteToTargets(LogLevels.Trace, context, this.FormatProvider, message, args, exception);
			}
		}

		// Token: 0x06001356 RID: 4950 RVA: 0x000702EE File Offset: 0x0006E4EE
		[Conditional("NLOG_ALL")]
		[Conditional("NLOG_DEBUG")]
		[Conditional("UNITY_EDITOR")]
		public void Debug(string message, params object[] args)
		{
			if (this.IsDebugEnabled)
			{
				this.WriteToTargets(LogLevels.Debug, this.Context, this.FormatProvider, message, args, null);
			}
		}

		// Token: 0x06001357 RID: 4951 RVA: 0x0007030F File Offset: 0x0006E50F
		[Conditional("NLOG_ALL")]
		[Conditional("NLOG_DEBUG")]
		[Conditional("UNITY_EDITOR")]
		public void Debug(global::UnityEngine.Object context, string message, params object[] args)
		{
			if (this.IsDebugEnabled)
			{
				this.WriteToTargets(LogLevels.Debug, context, this.FormatProvider, message, args, null);
			}
		}

		// Token: 0x06001358 RID: 4952 RVA: 0x0007032B File Offset: 0x0006E52B
		[Conditional("NLOG_ALL")]
		[Conditional("NLOG_DEBUG")]
		[Conditional("UNITY_EDITOR")]
		public void Debug(Exception exception, string message, params object[] args)
		{
			if (this.IsDebugEnabled)
			{
				this.WriteToTargets(LogLevels.Debug, this.Context, this.FormatProvider, message, args, exception);
			}
		}

		// Token: 0x06001359 RID: 4953 RVA: 0x0007034C File Offset: 0x0006E54C
		[Conditional("NLOG_ALL")]
		[Conditional("NLOG_DEBUG")]
		[Conditional("UNITY_EDITOR")]
		public void Debug(Exception exception, global::UnityEngine.Object context, string message, params object[] args)
		{
			if (this.IsDebugEnabled)
			{
				this.WriteToTargets(LogLevels.Debug, context, this.FormatProvider, message, args, exception);
			}
		}

		// Token: 0x0600135A RID: 4954 RVA: 0x00070369 File Offset: 0x0006E569
		[Conditional("NLOG_ALL")]
		[Conditional("NLOG_INFO")]
		[Conditional("UNITY_EDITOR")]
		public void Info(string message, params object[] args)
		{
			if (this.IsInfoEnabled)
			{
				this.WriteToTargets(LogLevels.Info, this.Context, this.FormatProvider, message, args, null);
			}
		}

		// Token: 0x0600135B RID: 4955 RVA: 0x0007038A File Offset: 0x0006E58A
		[Conditional("NLOG_ALL")]
		[Conditional("NLOG_INFO")]
		[Conditional("UNITY_EDITOR")]
		public void Info(global::UnityEngine.Object context, string message, params object[] args)
		{
			if (this.IsInfoEnabled)
			{
				this.WriteToTargets(LogLevels.Info, context, this.FormatProvider, message, args, null);
			}
		}

		// Token: 0x0600135C RID: 4956 RVA: 0x000703A6 File Offset: 0x0006E5A6
		[Conditional("NLOG_ALL")]
		[Conditional("NLOG_INFO")]
		[Conditional("UNITY_EDITOR")]
		public void Info(Exception exception, string message, params object[] args)
		{
			if (this.IsInfoEnabled)
			{
				this.WriteToTargets(LogLevels.Info, this.Context, this.FormatProvider, message, args, exception);
			}
		}

		// Token: 0x0600135D RID: 4957 RVA: 0x000703C7 File Offset: 0x0006E5C7
		[Conditional("NLOG_ALL")]
		[Conditional("NLOG_INFO")]
		[Conditional("UNITY_EDITOR")]
		public void Info(Exception exception, global::UnityEngine.Object context, string message, params object[] args)
		{
			if (this.IsInfoEnabled)
			{
				this.WriteToTargets(LogLevels.Info, context, this.FormatProvider, message, args, exception);
			}
		}

		// Token: 0x0600135E RID: 4958 RVA: 0x000703E4 File Offset: 0x0006E5E4
		[Conditional("NLOG_ALL")]
		[Conditional("NLOG_WARN")]
		[Conditional("UNITY_EDITOR")]
		public void Warn(string message, params object[] args)
		{
			if (this.IsWarnEnabled)
			{
				this.WriteToTargets(LogLevels.Warn, this.Context, this.FormatProvider, message, args, null);
			}
		}

		// Token: 0x0600135F RID: 4959 RVA: 0x00070404 File Offset: 0x0006E604
		[Conditional("NLOG_ALL")]
		[Conditional("NLOG_WARN")]
		[Conditional("UNITY_EDITOR")]
		public void Warn(global::UnityEngine.Object context, string message, params object[] args)
		{
			if (this.IsWarnEnabled)
			{
				this.WriteToTargets(LogLevels.Warn, context, this.FormatProvider, message, args, null);
			}
		}

		// Token: 0x06001360 RID: 4960 RVA: 0x0007041F File Offset: 0x0006E61F
		[Conditional("NLOG_ALL")]
		[Conditional("NLOG_WARN")]
		[Conditional("UNITY_EDITOR")]
		public void Warn(Exception exception, string message, params object[] args)
		{
			if (this.IsWarnEnabled)
			{
				this.WriteToTargets(LogLevels.Warn, this.Context, this.FormatProvider, message, args, exception);
			}
		}

		// Token: 0x06001361 RID: 4961 RVA: 0x0007043F File Offset: 0x0006E63F
		[Conditional("NLOG_ALL")]
		[Conditional("NLOG_WARN")]
		[Conditional("UNITY_EDITOR")]
		public void Warn(Exception exception, global::UnityEngine.Object context, string message, params object[] args)
		{
			if (this.IsWarnEnabled)
			{
				this.WriteToTargets(LogLevels.Warn, context, this.FormatProvider, message, args, exception);
			}
		}

		// Token: 0x06001362 RID: 4962 RVA: 0x0007045B File Offset: 0x0006E65B
		[Conditional("NLOG_ALL")]
		[Conditional("NLOG_ERROR")]
		[Conditional("UNITY_EDITOR")]
		public void Error(string message, params object[] args)
		{
			if (this.IsErrorEnabled)
			{
				this.WriteToTargets(LogLevels.Error, this.Context, this.FormatProvider, message, args, null);
			}
		}

		// Token: 0x06001363 RID: 4963 RVA: 0x0007047B File Offset: 0x0006E67B
		[Conditional("NLOG_ALL")]
		[Conditional("NLOG_ERROR")]
		[Conditional("UNITY_EDITOR")]
		public void Error(global::UnityEngine.Object context, string message, params object[] args)
		{
			if (this.IsErrorEnabled)
			{
				this.WriteToTargets(LogLevels.Error, context, this.FormatProvider, message, args, null);
			}
		}

		// Token: 0x06001364 RID: 4964 RVA: 0x00070496 File Offset: 0x0006E696
		[Conditional("NLOG_ALL")]
		[Conditional("NLOG_ERROR")]
		[Conditional("UNITY_EDITOR")]
		public void Error(Exception exception, string message, params object[] args)
		{
			if (this.IsErrorEnabled)
			{
				this.WriteToTargets(LogLevels.Error, this.Context, this.FormatProvider, message, args, exception);
			}
		}

		// Token: 0x06001365 RID: 4965 RVA: 0x000704B6 File Offset: 0x0006E6B6
		[Conditional("NLOG_ALL")]
		[Conditional("NLOG_ERROR")]
		[Conditional("UNITY_EDITOR")]
		public void Error(Exception exception, global::UnityEngine.Object context, string message, params object[] args)
		{
			if (this.IsErrorEnabled)
			{
				this.WriteToTargets(LogLevels.Error, context, this.FormatProvider, message, args, exception);
			}
		}

		// Token: 0x06001366 RID: 4966 RVA: 0x000704D2 File Offset: 0x0006E6D2
		public void Fatal(string message, params object[] args)
		{
			if (this.IsFatalEnabled)
			{
				this.WriteToTargets(LogLevels.Fatal, this.Context, this.FormatProvider, message, args, null);
			}
		}

		// Token: 0x06001367 RID: 4967 RVA: 0x000704F2 File Offset: 0x0006E6F2
		[Conditional("NLOG_ALL")]
		[Conditional("NLOG_FATAL")]
		[Conditional("UNITY_EDITOR")]
		public void Fatal(global::UnityEngine.Object context, string message, params object[] args)
		{
			if (this.IsFatalEnabled)
			{
				this.WriteToTargets(LogLevels.Fatal, context, this.FormatProvider, message, args, null);
			}
		}

		// Token: 0x06001368 RID: 4968 RVA: 0x0007050D File Offset: 0x0006E70D
		[Conditional("NLOG_ALL")]
		[Conditional("NLOG_FATAL")]
		[Conditional("UNITY_EDITOR")]
		public void Fatal(Exception exception, string message, params object[] args)
		{
			if (this.IsFatalEnabled)
			{
				this.WriteToTargets(LogLevels.Fatal, this.Context, this.FormatProvider, message, args, exception);
			}
		}

		// Token: 0x06001369 RID: 4969 RVA: 0x0007052D File Offset: 0x0006E72D
		[Conditional("NLOG_ALL")]
		[Conditional("NLOG_FATAL")]
		[Conditional("UNITY_EDITOR")]
		public void Fatal(Exception exception, global::UnityEngine.Object context, string message, params object[] args)
		{
			if (this.IsFatalEnabled)
			{
				this.WriteToTargets(LogLevels.Fatal, context, this.FormatProvider, message, args, exception);
			}
		}

		// Token: 0x0600136A RID: 4970 RVA: 0x00070549 File Offset: 0x0006E749
		[Conditional("NLOG_ALL")]
		[Conditional("NLOG_ASSERT")]
		[Conditional("UNITY_EDITOR")]
		public void Assert(bool test, string message, params object[] args)
		{
			if (this.IsAssertEnabled && !test)
			{
				this.WriteToTargets(LogLevels.Assert, this.Context, this.FormatProvider, message, args, null);
			}
		}

		// Token: 0x0600136B RID: 4971 RVA: 0x0007056C File Offset: 0x0006E76C
		[Conditional("NLOG_ALL")]
		[Conditional("NLOG_ASSERT")]
		[Conditional("UNITY_EDITOR")]
		public void Assert(bool test, global::UnityEngine.Object context, string message, params object[] args)
		{
			if (this.IsAssertEnabled && !test)
			{
				this.WriteToTargets(LogLevels.Assert, context, this.FormatProvider, message, args, null);
			}
		}

		// Token: 0x0600136C RID: 4972 RVA: 0x0007058B File Offset: 0x0006E78B
		[Conditional("NLOG_ALL")]
		[Conditional("NLOG_ASSERT")]
		[Conditional("UNITY_EDITOR")]
		public void Assert(Exception exception, string message, params object[] args)
		{
			if (this.IsAssertEnabled)
			{
				this.WriteToTargets(LogLevels.Assert, this.Context, this.FormatProvider, message, args, exception);
			}
		}

		// Token: 0x0600136D RID: 4973 RVA: 0x000705AB File Offset: 0x0006E7AB
		[Conditional("NLOG_ALL")]
		[Conditional("NLOG_ASSERT")]
		[Conditional("UNITY_EDITOR")]
		public void Assert(Exception exception, global::UnityEngine.Object context, string message, params object[] args)
		{
			if (this.IsAssertEnabled)
			{
				this.WriteToTargets(LogLevels.Assert, this.Context, this.FormatProvider, message, args, exception);
			}
		}

		// Token: 0x0600136E RID: 4974 RVA: 0x000705CC File Offset: 0x0006E7CC
		[Conditional("NLOG_ALL")]
		[Conditional("NLOG_ASSERT")]
		[Conditional("UNITY_EDITOR")]
		public void Assert(Func<bool> test, string message, params object[] args)
		{
			if (this.IsAssertEnabled && !test())
			{
				this.WriteToTargets(LogLevels.Assert, this.Context, this.FormatProvider, message, args, null);
			}
		}

		// Token: 0x0600136F RID: 4975 RVA: 0x000705F4 File Offset: 0x0006E7F4
		[Conditional("NLOG_ALL")]
		[Conditional("NLOG_ASSERT")]
		[Conditional("UNITY_EDITOR")]
		public void Assert(Func<bool> test, global::UnityEngine.Object context, string message, params object[] args)
		{
			if (this.IsAssertEnabled && !test())
			{
				this.WriteToTargets(LogLevels.Assert, context, this.FormatProvider, message, args, null);
			}
		}

		// Token: 0x04000CB2 RID: 3250
		private readonly Type loggerType = typeof(NLogger);

		// Token: 0x04000CB3 RID: 3251
		private volatile LoggerConfiguration configuration;

		// Token: 0x04000CB4 RID: 3252
		private volatile bool isDebugEnabled;

		// Token: 0x04000CB5 RID: 3253
		private volatile bool isErrorEnabled;

		// Token: 0x04000CB6 RID: 3254
		private volatile bool isFatalEnabled;

		// Token: 0x04000CB7 RID: 3255
		private volatile bool isInfoEnabled;

		// Token: 0x04000CB8 RID: 3256
		private volatile bool isTraceEnabled;

		// Token: 0x04000CB9 RID: 3257
		private volatile bool isWarnEnabled;
	}
}
