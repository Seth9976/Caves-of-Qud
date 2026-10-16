using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using AiUnity.Common.InternalLog;
using AiUnity.Common.Patterns;
using AiUnity.NLog.Core.Common;
using AiUnity.NLog.Core.Internal;
using AiUnity.NLog.Core.Targets;

namespace AiUnity.NLog.Core.Config
{
	// Token: 0x02000290 RID: 656
	public class LoggingConfiguration
	{
		// Token: 0x17000320 RID: 800
		// (get) Token: 0x06001744 RID: 5956 RVA: 0x0007A7C2 File Offset: 0x000789C2
		private static IInternalLogger Logger
		{
			get
			{
				return Singleton<NLogInternalLogger>.Instance;
			}
		}

		// Token: 0x06001745 RID: 5957 RVA: 0x0007A7C9 File Offset: 0x000789C9
		public LoggingConfiguration()
		{
			this.LoggingRules = new List<LoggingRule>();
		}

		// Token: 0x17000321 RID: 801
		// (get) Token: 0x06001746 RID: 5958 RVA: 0x0007A7EC File Offset: 0x000789EC
		public ReadOnlyCollection<Target> ConfiguredNamedTargets
		{
			get
			{
				return new List<Target>(this.targets.Values).AsReadOnly();
			}
		}

		// Token: 0x17000322 RID: 802
		// (get) Token: 0x06001747 RID: 5959 RVA: 0x0007A803 File Offset: 0x00078A03
		public virtual IEnumerable<string> FileNamesToWatch
		{
			get
			{
				return new string[0];
			}
		}

		// Token: 0x17000323 RID: 803
		// (get) Token: 0x06001748 RID: 5960 RVA: 0x0007A80B File Offset: 0x00078A0B
		// (set) Token: 0x06001749 RID: 5961 RVA: 0x0007A813 File Offset: 0x00078A13
		public IList<LoggingRule> LoggingRules { get; private set; }

		// Token: 0x17000324 RID: 804
		// (get) Token: 0x0600174A RID: 5962 RVA: 0x0007A81C File Offset: 0x00078A1C
		// (set) Token: 0x0600174B RID: 5963 RVA: 0x0007A830 File Offset: 0x00078A30
		public CultureInfo DefaultCultureInfo
		{
			get
			{
				return Singleton<NLogManager>.Instance.DefaultCultureInfo();
			}
			set
			{
				Singleton<NLogManager>.Instance.DefaultCultureInfo = () => value;
			}
		}

		// Token: 0x17000325 RID: 805
		// (get) Token: 0x0600174C RID: 5964 RVA: 0x0007A860 File Offset: 0x00078A60
		public ReadOnlyCollection<Target> AllTargets
		{
			get
			{
				return this.configItems.OfType<Target>().ToList<Target>().AsReadOnly();
			}
		}

		// Token: 0x0600174D RID: 5965 RVA: 0x0007A878 File Offset: 0x00078A78
		public void AddTarget(string name, Target target)
		{
			if (name == null)
			{
				throw new ArgumentException("Target name cannot be null", "name");
			}
			LoggingConfiguration.Logger.Debug("Registering target {0}: {1}", new object[]
			{
				name,
				target.GetType().FullName
			});
			this.targets[name] = target;
		}

		// Token: 0x0600174E RID: 5966 RVA: 0x0007A8CC File Offset: 0x00078ACC
		public Target FindTargetByName(string name)
		{
			Target target;
			if (!this.targets.TryGetValue(name, out target))
			{
				return null;
			}
			return target;
		}

		// Token: 0x0600174F RID: 5967 RVA: 0x0007A8EC File Offset: 0x00078AEC
		public virtual LoggingConfiguration Reload()
		{
			return this;
		}

		// Token: 0x06001750 RID: 5968 RVA: 0x0007A8EF File Offset: 0x00078AEF
		public void RemoveTarget(string name)
		{
			this.targets.Remove(name);
		}

		// Token: 0x06001751 RID: 5969 RVA: 0x0007A900 File Offset: 0x00078B00
		internal void Close()
		{
			LoggingConfiguration.Logger.Debug("Closing logging configuration...", Array.Empty<object>());
			foreach (ISupportsInitialize supportsInitialize in this.configItems.OfType<ISupportsInitialize>())
			{
				LoggingConfiguration.Logger.Trace("Closing {0}", new object[] { supportsInitialize });
				try
				{
					supportsInitialize.Close();
				}
				catch (Exception ex)
				{
					if (ex.MustBeRethrown())
					{
						throw;
					}
					LoggingConfiguration.Logger.Warn("Exception while closing {0}", new object[] { ex });
				}
			}
			LoggingConfiguration.Logger.Debug("Finished closing logging configuration.", Array.Empty<object>());
		}

		// Token: 0x06001752 RID: 5970 RVA: 0x0007A9C8 File Offset: 0x00078BC8
		internal void Dump()
		{
			List<string> list = new List<string>();
			list.Add("----- NLog configuration dump. -----");
			list.Add("Targets:");
			foreach (Target target in this.targets.Values)
			{
				list.Add(target.ToString());
			}
			list.Add("Rules:");
			foreach (LoggingRule loggingRule in this.LoggingRules)
			{
				list.Add(loggingRule.ToString());
			}
			list.Add("--- End of NLog configuration dump ---");
			LoggingConfiguration.Logger.Debug(string.Join(Environment.NewLine, list.ToArray()), Array.Empty<object>());
		}

		// Token: 0x06001753 RID: 5971 RVA: 0x0007AAB4 File Offset: 0x00078CB4
		internal void FlushAllTargets(AsyncContinuation asyncContinuation)
		{
			List<Target> list = new List<Target>();
			foreach (LoggingRule loggingRule in this.LoggingRules)
			{
				foreach (Target target2 in loggingRule.Targets)
				{
					if (!list.Contains(target2))
					{
						list.Add(target2);
					}
				}
			}
			AsyncHelpers.ForEachItemInParallel<Target>(list, asyncContinuation, delegate(Target target, AsyncContinuation cont)
			{
				target.Flush(cont);
			});
		}

		// Token: 0x06001754 RID: 5972 RVA: 0x0007AB6C File Offset: 0x00078D6C
		internal void ValidateConfig()
		{
			List<object> list = new List<object>();
			foreach (LoggingRule loggingRule in this.LoggingRules)
			{
				list.Add(loggingRule);
			}
			foreach (Target target in this.targets.Values)
			{
				list.Add(target);
			}
			if (Singleton<NLogManager>.Instance.ShowUnityLog)
			{
				list.Add(new UnityLogListener());
			}
			this.configItems = ObjectGraphScanner.FindReachableObjects<object>(list.ToArray());
			LoggingConfiguration.Logger.Trace("Found {0} configuration items", new object[] { this.configItems.Length });
			object[] array = this.configItems;
			for (int i = 0; i < array.Length; i++)
			{
				PropertyHelper.CheckRequiredParameters(array[i]);
			}
		}

		// Token: 0x06001755 RID: 5973 RVA: 0x0007AC74 File Offset: 0x00078E74
		internal void InitializeAll()
		{
			this.ValidateConfig();
			foreach (ISupportsInitialize supportsInitialize in this.configItems.OfType<ISupportsInitialize>().Reverse<ISupportsInitialize>())
			{
				LoggingConfiguration.Logger.Trace("Initializing {0}", new object[] { supportsInitialize });
				try
				{
					supportsInitialize.Initialize(this);
				}
				catch (Exception ex)
				{
					if (ex.MustBeRethrown())
					{
						throw;
					}
					if (Singleton<NLogManager>.Instance.ThrowExceptions)
					{
						string text = "Error during initialization of ";
						ISupportsInitialize supportsInitialize2 = supportsInitialize;
						throw new NLogConfigurationException(text + ((supportsInitialize2 != null) ? supportsInitialize2.ToString() : null), ex);
					}
				}
			}
		}

		// Token: 0x06001756 RID: 5974 RVA: 0x0007AD30 File Offset: 0x00078F30
		internal void EnsureInitialized()
		{
		}

		// Token: 0x04000E17 RID: 3607
		private readonly IDictionary<string, Target> targets = new Dictionary<string, Target>(StringComparer.OrdinalIgnoreCase);

		// Token: 0x04000E18 RID: 3608
		private object[] configItems;
	}
}
