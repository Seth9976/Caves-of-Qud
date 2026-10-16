using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using AiUnity.Common.Extensions;
using AiUnity.Common.InternalLog;
using AiUnity.Common.Log;
using AiUnity.Common.Patterns;
using AiUnity.NLog.Core.Common;
using AiUnity.NLog.Core.Config;
using AiUnity.NLog.Core.Internal;
using AiUnity.NLog.Core.Targets;
using UnityEngine;

namespace AiUnity.NLog.Core
{
	// Token: 0x0200020B RID: 523
	public class LogFactory
	{
		// Token: 0x17000202 RID: 514
		// (get) Token: 0x060012EC RID: 4844 RVA: 0x0006EF70 File Offset: 0x0006D170
		// (set) Token: 0x060012ED RID: 4845 RVA: 0x0006F05C File Offset: 0x0006D25C
		public LoggingConfiguration Configuration
		{
			get
			{
				LoggingConfiguration loggingConfiguration;
				lock (this)
				{
					if (this.configLoaded && this.config != null)
					{
						loggingConfiguration = this.config;
					}
					else
					{
						this.configLoaded = true;
						if (this.config == null)
						{
							string configText = Singleton<NLogConfigFile>.Instance.GetConfigText();
							if (!string.IsNullOrEmpty(configText))
							{
								this.config = new XmlLoggingConfiguration(configText, Singleton<NLogConfigFile>.Instance.FileInfo.FullName);
							}
							else
							{
								LogFactory.Logger.Info("Using default configuration because unable to locate {0}.", new object[] { Singleton<NLogConfigFile>.Instance.RelativeName });
								this.config = new XmlLoggingConfiguration("<?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n  <nlog buildLevels=\"\">\r\n\t<targets>\r\n\t\t<target name=\"UnityConsole\" type=\"UnityConsole\"/>\r\n\t</targets>\r\n\t<rules>\r\n\t\t<logger name=\"*\" namespace=\"*\" target=\"UnityConsole\" levels=\"Fatal, Error, Warn\"/>\r\n\t</rules>\r\n  </nlog>", Singleton<NLogConfigFile>.Instance.FileInfo.FullName);
							}
						}
						if (this.config != null)
						{
							this.config.InitializeAll();
						}
						loggingConfiguration = this.config;
					}
				}
				return loggingConfiguration;
			}
			set
			{
				lock (this)
				{
					LoggingConfiguration loggingConfiguration = this.config;
					if (loggingConfiguration != null)
					{
						LogFactory.Logger.Debug("Closing old configuration.", Array.Empty<object>());
						this.Flush();
						loggingConfiguration.Close();
					}
					this.config = value;
					if (this.config != null)
					{
						LogFactory.Logger.Debug("Establish new logging configuration.", Array.Empty<object>());
						this.configLoaded = true;
						LogFactory.Dump(this.config);
						this.config.InitializeAll();
						this.ReconfigExistingLoggers(this.config);
					}
					else
					{
						LogFactory.Logger.Debug("Logging Configuration deleted.", Array.Empty<object>());
						this.configLoaded = false;
					}
					EventHandler<LoggingConfigurationChangedEventArgs> configurationChanged = this.ConfigurationChanged;
					if (configurationChanged != null)
					{
						configurationChanged(this, new LoggingConfigurationChangedEventArgs(loggingConfiguration, value));
					}
				}
			}
		}

		// Token: 0x17000203 RID: 515
		// (get) Token: 0x060012EE RID: 4846 RVA: 0x0006F13C File Offset: 0x0006D33C
		// (set) Token: 0x060012EF RID: 4847 RVA: 0x0006F144 File Offset: 0x0006D344
		public LogLevels GlobalThreshold
		{
			get
			{
				return this.globalThreshold;
			}
			set
			{
				if (this.globalThreshold != value)
				{
					lock (this)
					{
						this.globalThreshold = value;
						this.ReconfigExistingLoggers();
					}
				}
			}
		}

		// Token: 0x17000204 RID: 516
		// (get) Token: 0x060012F0 RID: 4848 RVA: 0x0006F190 File Offset: 0x0006D390
		// (set) Token: 0x060012F1 RID: 4849 RVA: 0x0006F198 File Offset: 0x0006D398
		public bool ThrowExceptions { get; set; }

		// Token: 0x17000205 RID: 517
		// (get) Token: 0x060012F2 RID: 4850 RVA: 0x0006F1A1 File Offset: 0x0006D3A1
		private static IInternalLogger Logger
		{
			get
			{
				return Singleton<NLogInternalLogger>.Instance;
			}
		}

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x060012F3 RID: 4851 RVA: 0x0006F1A8 File Offset: 0x0006D3A8
		// (remove) Token: 0x060012F4 RID: 4852 RVA: 0x0006F1E0 File Offset: 0x0006D3E0
		public event EventHandler<LoggingConfigurationChangedEventArgs> ConfigurationChanged;

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x060012F5 RID: 4853 RVA: 0x0006F218 File Offset: 0x0006D418
		// (remove) Token: 0x060012F6 RID: 4854 RVA: 0x0006F250 File Offset: 0x0006D450
		public event EventHandler<LoggingConfigurationReloadedEventArgs> ConfigurationReloaded;

		// Token: 0x060012F7 RID: 4855 RVA: 0x0006F285 File Offset: 0x0006D485
		public LogFactory()
		{
		}

		// Token: 0x060012F8 RID: 4856 RVA: 0x0006F29F File Offset: 0x0006D49F
		public LogFactory(LoggingConfiguration config)
			: this()
		{
			this.Configuration = config;
		}

		// Token: 0x060012F9 RID: 4857 RVA: 0x0006F2B0 File Offset: 0x0006D4B0
		public NLogger CreateNullLogger()
		{
			Dictionary<LogLevels, TargetWithFilterChain> dictionary = new Dictionary<LogLevels, TargetWithFilterChain>();
			NLogger nlogger = new NLogger(null, null);
			nlogger.Initialize(string.Empty, new LoggerConfiguration(dictionary), this);
			return nlogger;
		}

		// Token: 0x060012FA RID: 4858 RVA: 0x0006F2DC File Offset: 0x0006D4DC
		public void EnableLogging()
		{
			lock (this)
			{
				this.logsEnabled++;
				if (this.logsEnabled == 0)
				{
					this.ReconfigExistingLoggers();
				}
			}
		}

		// Token: 0x060012FB RID: 4859 RVA: 0x0006F330 File Offset: 0x0006D530
		public void Flush()
		{
			this.Flush(LogFactory.defaultFlushTimeout);
		}

		// Token: 0x060012FC RID: 4860 RVA: 0x0006F340 File Offset: 0x0006D540
		public void Flush(TimeSpan timeout)
		{
			try
			{
				AsyncHelpers.RunSynchronously(delegate(AsyncContinuation cb)
				{
					this.Flush(cb, timeout);
				});
			}
			catch (Exception ex)
			{
				if (this.ThrowExceptions)
				{
					throw;
				}
				LogFactory.Logger.Error(ex.ToString(), Array.Empty<object>());
			}
		}

		// Token: 0x060012FD RID: 4861 RVA: 0x0006F3A8 File Offset: 0x0006D5A8
		public void Flush(int timeoutMilliseconds)
		{
			this.Flush(TimeSpan.FromMilliseconds((double)timeoutMilliseconds));
		}

		// Token: 0x060012FE RID: 4862 RVA: 0x0006F3B7 File Offset: 0x0006D5B7
		public void Flush(AsyncContinuation asyncContinuation)
		{
			this.Flush(asyncContinuation, TimeSpan.MaxValue);
		}

		// Token: 0x060012FF RID: 4863 RVA: 0x0006F3C5 File Offset: 0x0006D5C5
		public void Flush(AsyncContinuation asyncContinuation, int timeoutMilliseconds)
		{
			this.Flush(asyncContinuation, TimeSpan.FromMilliseconds((double)timeoutMilliseconds));
		}

		// Token: 0x06001300 RID: 4864 RVA: 0x0006F3D8 File Offset: 0x0006D5D8
		public void Flush(AsyncContinuation asyncContinuation, TimeSpan timeout)
		{
			try
			{
				LogFactory.Logger.Trace("LogFactory.Flush({0})", new object[] { timeout });
				LoggingConfiguration configuration = this.Configuration;
				if (configuration != null)
				{
					LogFactory.Logger.Trace("Flushing all targets...", Array.Empty<object>());
					configuration.FlushAllTargets(AsyncHelpers.WithTimeout(asyncContinuation, timeout));
				}
				else
				{
					asyncContinuation(null);
				}
			}
			catch (Exception ex)
			{
				if (this.ThrowExceptions)
				{
					throw;
				}
				LogFactory.Logger.Error(ex.ToString(), Array.Empty<object>());
			}
		}

		// Token: 0x06001301 RID: 4865 RVA: 0x0006F46C File Offset: 0x0006D66C
		public NLogger GetLogger(string name, global::UnityEngine.Object context = null, IFormatProvider formatProvider = null)
		{
			return this.GetLogger(name, typeof(NLogger), context, formatProvider);
		}

		// Token: 0x06001302 RID: 4866 RVA: 0x0006F481 File Offset: 0x0006D681
		public NLogger GetLogger(string name, Type loggerType, global::UnityEngine.Object context = null, IFormatProvider formatProvider = null)
		{
			return this.GetLogger(new LogFactory.LoggerCacheKey(loggerType, name), context, formatProvider);
		}

		// Token: 0x06001303 RID: 4867 RVA: 0x0006F493 File Offset: 0x0006D693
		public bool IsLoggingEnabled()
		{
			return this.logsEnabled >= 0;
		}

		// Token: 0x06001304 RID: 4868 RVA: 0x0006F4A1 File Offset: 0x0006D6A1
		public void ReconfigExistingLoggers()
		{
			this.ReconfigExistingLoggers(this.config);
		}

		// Token: 0x06001305 RID: 4869 RVA: 0x0006F4B0 File Offset: 0x0006D6B0
		internal LoggerConfiguration GetConfigurationForLogger(string name, LoggingConfiguration configuration)
		{
			Dictionary<LogLevels, TargetWithFilterChain> dictionary = new Dictionary<LogLevels, TargetWithFilterChain>();
			Dictionary<LogLevels, TargetWithFilterChain> dictionary2 = new Dictionary<LogLevels, TargetWithFilterChain>();
			LogFactory.Logger.Debug("Getting targets for {0} by level.", new object[] { name });
			if (configuration != null && this.IsLoggingEnabled())
			{
				this.GetTargetsByLevelForLogger(name, configuration.LoggingRules, dictionary, dictionary2);
			}
			foreach (TargetWithFilterChain targetWithFilterChain in dictionary.Values)
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.AppendFormat(CultureInfo.InvariantCulture, "{0} =>", targetWithFilterChain);
				for (TargetWithFilterChain targetWithFilterChain2 = targetWithFilterChain; targetWithFilterChain2 != null; targetWithFilterChain2 = targetWithFilterChain2.NextInChain)
				{
					stringBuilder.AppendFormat(CultureInfo.InvariantCulture, " {0}", targetWithFilterChain2.Target.Name);
					if (targetWithFilterChain2.FilterChain.Count > 0)
					{
						stringBuilder.AppendFormat(CultureInfo.InvariantCulture, " ({0} filters)", targetWithFilterChain2.FilterChain.Count);
					}
				}
				LogFactory.Logger.Trace("GetConfigurationForLogger: Chain = {0}", new object[] { stringBuilder.ToString() });
			}
			return new LoggerConfiguration(dictionary);
		}

		// Token: 0x06001306 RID: 4870 RVA: 0x0006F5E4 File Offset: 0x0006D7E4
		internal void GetTargetsByLevelForLogger(string name, IList<LoggingRule> rules, Dictionary<LogLevels, TargetWithFilterChain> targetsByLevel, Dictionary<LogLevels, TargetWithFilterChain> lastTargetsByLevel)
		{
			foreach (LoggingRule loggingRule in rules)
			{
				if (loggingRule.NameMatches(name) && loggingRule.isPlatformMatch())
				{
					foreach (LogLevels logLevels in loggingRule.logLevels.GetFlags(false, false))
					{
						foreach (Target target in loggingRule.Targets)
						{
							TargetWithFilterChain targetWithFilterChain = new TargetWithFilterChain(target, loggingRule.Filters);
							TargetWithFilterChain targetWithFilterChain2;
							if (lastTargetsByLevel.TryGetValue(logLevels, out targetWithFilterChain2))
							{
								targetWithFilterChain2.NextInChain = targetWithFilterChain;
							}
							else
							{
								targetsByLevel[logLevels] = targetWithFilterChain;
							}
							lastTargetsByLevel[logLevels] = targetWithFilterChain;
						}
					}
					this.GetTargetsByLevelForLogger(name, loggingRule.ChildRules, targetsByLevel, lastTargetsByLevel);
					if (loggingRule.Final)
					{
						break;
					}
				}
			}
			foreach (TargetWithFilterChain targetWithFilterChain3 in targetsByLevel.Values)
			{
				if (targetWithFilterChain3 != null)
				{
					targetWithFilterChain3.PrecalculateStackTraceUsage();
				}
			}
		}

		// Token: 0x06001307 RID: 4871 RVA: 0x0006F750 File Offset: 0x0006D950
		internal void ReconfigExistingLoggers(LoggingConfiguration configuration)
		{
			if (configuration != null)
			{
				configuration.EnsureInitialized();
			}
			foreach (WeakReference weakReference in this.loggerCache.Values.ToList<WeakReference>())
			{
				NLogger nlogger = weakReference.Target as NLogger;
				if (nlogger != null)
				{
					nlogger.SetConfiguration(this.GetConfigurationForLogger(nlogger.Name, configuration));
				}
			}
		}

		// Token: 0x06001308 RID: 4872 RVA: 0x0006F7D0 File Offset: 0x0006D9D0
		internal void ReloadConfig()
		{
			if (this.Configuration != null)
			{
				this.Configuration = this.Configuration.Reload();
				if (this.ConfigurationReloaded != null)
				{
					this.ConfigurationReloaded(this, new LoggingConfigurationReloadedEventArgs(true, null));
					return;
				}
			}
			else
			{
				LogFactory.Logger.Error("Unable to reload configuration {0}.", new object[] { Singleton<NLogConfigFile>.Instance.FileInfo.FullName });
			}
		}

		// Token: 0x06001309 RID: 4873 RVA: 0x0006F839 File Offset: 0x0006DA39
		private static void Dump(LoggingConfiguration config)
		{
			if (LogFactory.Logger.IsDebugEnabled)
			{
				config.Dump();
			}
		}

		// Token: 0x0600130A RID: 4874 RVA: 0x0006F850 File Offset: 0x0006DA50
		private NLogger GetLogger(LogFactory.LoggerCacheKey cacheKey, global::UnityEngine.Object context, IFormatProvider formatProvider)
		{
			NLogger nlogger3;
			lock (this)
			{
				WeakReference weakReference;
				if (this.loggerCache.TryGetValue(cacheKey, out weakReference))
				{
					NLogger nlogger = weakReference.Target as NLogger;
					if (nlogger != null)
					{
						return nlogger;
					}
				}
				NLogger nlogger2;
				if (cacheKey.ConcreteType != null && cacheKey.ConcreteType != typeof(NLogger))
				{
					try
					{
						nlogger2 = (NLogger)FactoryHelper.CreateInstance(cacheKey.ConcreteType);
						goto IL_00C8;
					}
					catch (Exception ex)
					{
						if (ex.MustBeRethrown())
						{
							throw;
						}
						if (this.ThrowExceptions)
						{
							throw;
						}
						LogFactory.Logger.Error("Cannot create instance of specified type. Proceeding with default type instance. Exception : {0}", new object[] { ex });
						cacheKey = new LogFactory.LoggerCacheKey(typeof(NLogger), cacheKey.Name);
						nlogger2 = new NLogger(context, formatProvider);
						goto IL_00C8;
					}
				}
				nlogger2 = new NLogger(context, formatProvider);
				IL_00C8:
				if (cacheKey.ConcreteType != null)
				{
					nlogger2.Initialize(cacheKey.Name, this.GetConfigurationForLogger(cacheKey.Name, this.Configuration), this);
				}
				this.loggerCache[cacheKey] = new WeakReference(nlogger2);
				nlogger3 = nlogger2;
			}
			return nlogger3;
		}

		// Token: 0x04000C9E RID: 3230
		private static TimeSpan defaultFlushTimeout = TimeSpan.FromSeconds(15.0);

		// Token: 0x04000C9F RID: 3231
		private readonly Dictionary<LogFactory.LoggerCacheKey, WeakReference> loggerCache = new Dictionary<LogFactory.LoggerCacheKey, WeakReference>();

		// Token: 0x04000CA0 RID: 3232
		private LoggingConfiguration config;

		// Token: 0x04000CA1 RID: 3233
		private bool configLoaded;

		// Token: 0x04000CA2 RID: 3234
		private LogLevels globalThreshold = LogLevels.Everything;

		// Token: 0x04000CA3 RID: 3235
		private int logsEnabled;

		// Token: 0x04000CA4 RID: 3236
		private const string DefaultConfig = "<?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n  <nlog buildLevels=\"\">\r\n\t<targets>\r\n\t\t<target name=\"UnityConsole\" type=\"UnityConsole\"/>\r\n\t</targets>\r\n\t<rules>\r\n\t\t<logger name=\"*\" namespace=\"*\" target=\"UnityConsole\" levels=\"Fatal, Error, Warn\"/>\r\n\t</rules>\r\n  </nlog>";

		// Token: 0x02001657 RID: 5719
		internal class LoggerCacheKey
		{
			// Token: 0x17000D3A RID: 3386
			// (get) Token: 0x0600D7CC RID: 55244 RVA: 0x004FCAB6 File Offset: 0x004FACB6
			// (set) Token: 0x0600D7CD RID: 55245 RVA: 0x004FCABE File Offset: 0x004FACBE
			internal Type ConcreteType { get; private set; }

			// Token: 0x17000D3B RID: 3387
			// (get) Token: 0x0600D7CE RID: 55246 RVA: 0x004FCAC7 File Offset: 0x004FACC7
			// (set) Token: 0x0600D7CF RID: 55247 RVA: 0x004FCACF File Offset: 0x004FACCF
			internal string Name { get; private set; }

			// Token: 0x0600D7D0 RID: 55248 RVA: 0x004FCAD8 File Offset: 0x004FACD8
			internal LoggerCacheKey(Type loggerConcreteType, string name)
			{
				this.ConcreteType = loggerConcreteType;
				this.Name = name;
			}

			// Token: 0x0600D7D1 RID: 55249 RVA: 0x004FCAF0 File Offset: 0x004FACF0
			public override bool Equals(object o)
			{
				LogFactory.LoggerCacheKey loggerCacheKey = o as LogFactory.LoggerCacheKey;
				return loggerCacheKey != null && this.ConcreteType == loggerCacheKey.ConcreteType && loggerCacheKey.Name == this.Name;
			}

			// Token: 0x0600D7D2 RID: 55250 RVA: 0x004FCB2F File Offset: 0x004FAD2F
			public override int GetHashCode()
			{
				return this.ConcreteType.GetHashCode() ^ this.Name.GetHashCode();
			}
		}
	}
}
