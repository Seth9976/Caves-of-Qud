using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using AiUnity.Common.Log;
using AiUnity.Common.Patterns;
using AiUnity.NLog.Core.Config;
using UnityEngine;

namespace AiUnity.NLog.Core
{
	// Token: 0x0200020C RID: 524
	public sealed class NLogManager : Singleton<NLogManager>, ILogManager
	{
		// Token: 0x17000206 RID: 518
		// (get) Token: 0x0600130C RID: 4876 RVA: 0x0006F9C1 File Offset: 0x0006DBC1
		// (set) Token: 0x0600130D RID: 4877 RVA: 0x0006F9C9 File Offset: 0x0006DBC9
		public bool AssertException { get; set; }

		// Token: 0x17000207 RID: 519
		// (get) Token: 0x0600130E RID: 4878 RVA: 0x0006F9D2 File Offset: 0x0006DBD2
		// (set) Token: 0x0600130F RID: 4879 RVA: 0x0006F9DA File Offset: 0x0006DBDA
		public NLogManager.GetCultureInfo DefaultCultureInfo { get; set; }

		// Token: 0x17000208 RID: 520
		// (get) Token: 0x06001310 RID: 4880 RVA: 0x0006F9E3 File Offset: 0x0006DBE3
		// (set) Token: 0x06001311 RID: 4881 RVA: 0x0006F9F0 File Offset: 0x0006DBF0
		public LogLevels GlobalLogLevel
		{
			get
			{
				return this.globalFactory.GlobalThreshold;
			}
			set
			{
				this.globalFactory.GlobalThreshold = value;
			}
		}

		// Token: 0x17000209 RID: 521
		// (get) Token: 0x06001312 RID: 4882 RVA: 0x0006F9FE File Offset: 0x0006DBFE
		// (set) Token: 0x06001313 RID: 4883 RVA: 0x0006FA06 File Offset: 0x0006DC06
		public HashSet<Assembly> HiddenAssemblies { get; set; }

		// Token: 0x1700020A RID: 522
		// (get) Token: 0x06001314 RID: 4884 RVA: 0x0006FA0F File Offset: 0x0006DC0F
		// (set) Token: 0x06001315 RID: 4885 RVA: 0x0006FA17 File Offset: 0x0006DC17
		public HashSet<string> HiddenNameSpaces { get; set; }

		// Token: 0x1700020B RID: 523
		// (get) Token: 0x06001316 RID: 4886 RVA: 0x0006FA20 File Offset: 0x0006DC20
		// (set) Token: 0x06001317 RID: 4887 RVA: 0x0006FA28 File Offset: 0x0006DC28
		public bool ShowUnityLog { get; set; }

		// Token: 0x1700020C RID: 524
		// (get) Token: 0x06001318 RID: 4888 RVA: 0x0006FA31 File Offset: 0x0006DC31
		// (set) Token: 0x06001319 RID: 4889 RVA: 0x0006FA3E File Offset: 0x0006DC3E
		public bool ThrowExceptions
		{
			get
			{
				return this.globalFactory.ThrowExceptions;
			}
			set
			{
				this.globalFactory.ThrowExceptions = value;
			}
		}

		// Token: 0x1700020D RID: 525
		// (get) Token: 0x0600131A RID: 4890 RVA: 0x0006FA4C File Offset: 0x0006DC4C
		// (set) Token: 0x0600131B RID: 4891 RVA: 0x0006FA59 File Offset: 0x0006DC59
		internal LoggingConfiguration Configuration
		{
			get
			{
				return this.globalFactory.Configuration;
			}
			set
			{
				this.globalFactory.Configuration = value;
			}
		}

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x0600131C RID: 4892 RVA: 0x0006FA67 File Offset: 0x0006DC67
		// (remove) Token: 0x0600131D RID: 4893 RVA: 0x0006FA75 File Offset: 0x0006DC75
		public event EventHandler<LoggingConfigurationChangedEventArgs> ConfigurationChanged
		{
			add
			{
				this.globalFactory.ConfigurationChanged += value;
			}
			remove
			{
				this.globalFactory.ConfigurationChanged -= value;
			}
		}

		// Token: 0x14000005 RID: 5
		// (add) Token: 0x0600131E RID: 4894 RVA: 0x0006FA83 File Offset: 0x0006DC83
		// (remove) Token: 0x0600131F RID: 4895 RVA: 0x0006FA91 File Offset: 0x0006DC91
		public event EventHandler<LoggingConfigurationReloadedEventArgs> ConfigurationReloaded
		{
			add
			{
				this.globalFactory.ConfigurationReloaded += value;
			}
			remove
			{
				this.globalFactory.ConfigurationReloaded -= value;
			}
		}

		// Token: 0x06001320 RID: 4896 RVA: 0x0006FAA0 File Offset: 0x0006DCA0
		public NLogManager()
		{
			this.HiddenAssemblies = new HashSet<Assembly>();
			this.HiddenNameSpaces = new HashSet<string>();
			this.DefaultCultureInfo = () => CultureInfo.CurrentCulture;
			this.AssertException = false;
		}

		// Token: 0x06001321 RID: 4897 RVA: 0x0006FB00 File Offset: 0x0006DD00
		public NLogger GetLogger(object context, IFormatProvider formatProvider = null)
		{
			global::UnityEngine.Object @object = context as global::UnityEngine.Object;
			return this.globalFactory.GetLogger(context.GetType().FullName, @object, formatProvider);
		}

		// Token: 0x06001322 RID: 4898 RVA: 0x0006FB2C File Offset: 0x0006DD2C
		public NLogger GetLogger(string name, global::UnityEngine.Object context, IFormatProvider formatProvider = null)
		{
			return this.globalFactory.GetLogger(name, context, formatProvider);
		}

		// Token: 0x06001323 RID: 4899 RVA: 0x0006FB3C File Offset: 0x0006DD3C
		public NLogger GetLogger(string name, Type loggerType, global::UnityEngine.Object context = null, IFormatProvider formatProvider = null)
		{
			return this.globalFactory.GetLogger(name, loggerType, context, formatProvider);
		}

		// Token: 0x06001324 RID: 4900 RVA: 0x0006FB4E File Offset: 0x0006DD4E
		public void ReconfigExistingLoggers()
		{
			this.globalFactory.ReconfigExistingLoggers();
		}

		// Token: 0x06001325 RID: 4901 RVA: 0x0006FB5B File Offset: 0x0006DD5B
		public void ReloadConfig()
		{
			this.globalFactory.ReloadConfig();
		}

		// Token: 0x06001326 RID: 4902 RVA: 0x0006FB68 File Offset: 0x0006DD68
		AiUnity.Common.Log.ILogger ILogManager.GetLogger(string name, global::UnityEngine.Object context, IFormatProvider formatProvider)
		{
			return this.GetLogger(name, context, formatProvider);
		}

		// Token: 0x04000CA8 RID: 3240
		public const LogLevels GlobalLogLevelsDefault = LogLevels.Everything;

		// Token: 0x04000CA9 RID: 3241
		private readonly LogFactory globalFactory = new LogFactory();

		// Token: 0x02001659 RID: 5721
		// (Invoke) Token: 0x0600D7D6 RID: 55254
		public delegate CultureInfo GetCultureInfo();
	}
}
