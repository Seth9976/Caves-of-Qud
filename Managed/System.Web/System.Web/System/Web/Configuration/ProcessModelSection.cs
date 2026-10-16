using System;
using System.ComponentModel;
using System.Configuration;

namespace System.Web.Configuration
{
	/// <summary>Configures the ASP.NET process model settings on an Internet Information Services (IIS) Web server. This class cannot be inherited.</summary>
	// Token: 0x020005C1 RID: 1473
	public sealed class ProcessModelSection : ConfigurationSection
	{
		// Token: 0x06003F49 RID: 16201 RVA: 0x000A54BC File Offset: 0x000A36BC
		static ProcessModelSection()
		{
			ProcessModelSection.properties.Add(ProcessModelSection.autoConfigProp);
			ProcessModelSection.properties.Add(ProcessModelSection.clientConnectedCheckProp);
			ProcessModelSection.properties.Add(ProcessModelSection.comAuthenticationLevelProp);
			ProcessModelSection.properties.Add(ProcessModelSection.comImpersonationLevelProp);
			ProcessModelSection.properties.Add(ProcessModelSection.cpuMaskProp);
			ProcessModelSection.properties.Add(ProcessModelSection.enableProp);
			ProcessModelSection.properties.Add(ProcessModelSection.idleTimeoutProp);
			ProcessModelSection.properties.Add(ProcessModelSection.logLevelProp);
			ProcessModelSection.properties.Add(ProcessModelSection.maxAppDomainsProp);
			ProcessModelSection.properties.Add(ProcessModelSection.maxIoThreadsProp);
			ProcessModelSection.properties.Add(ProcessModelSection.maxWorkerThreadsProp);
			ProcessModelSection.properties.Add(ProcessModelSection.memoryLimitProp);
			ProcessModelSection.properties.Add(ProcessModelSection.minIoThreadsProp);
			ProcessModelSection.properties.Add(ProcessModelSection.minWorkerThreadsProp);
			ProcessModelSection.properties.Add(ProcessModelSection.passwordProp);
			ProcessModelSection.properties.Add(ProcessModelSection.pingFrequencyProp);
			ProcessModelSection.properties.Add(ProcessModelSection.pingTimeoutProp);
			ProcessModelSection.properties.Add(ProcessModelSection.requestLimitProp);
			ProcessModelSection.properties.Add(ProcessModelSection.requestQueueLimitProp);
			ProcessModelSection.properties.Add(ProcessModelSection.responseDeadlockIntervalProp);
			ProcessModelSection.properties.Add(ProcessModelSection.responseRestartDeadlockIntervalProp);
			ProcessModelSection.properties.Add(ProcessModelSection.restartQueueLimitProp);
			ProcessModelSection.properties.Add(ProcessModelSection.serverErrorMessageFileProp);
			ProcessModelSection.properties.Add(ProcessModelSection.shutdownTimeoutProp);
			ProcessModelSection.properties.Add(ProcessModelSection.timeoutProp);
			ProcessModelSection.properties.Add(ProcessModelSection.userNameProp);
			ProcessModelSection.properties.Add(ProcessModelSection.webGardenProp);
			ProcessModelSection.elementProperty = new ConfigurationElementProperty(new CallbackValidator(typeof(ProcessModelSection), new ValidatorCallback(ProcessModelSection.ValidateElement)));
		}

		// Token: 0x06003F4A RID: 16202 RVA: 0x00003946 File Offset: 0x00001B46
		private static void ValidateElement(object o)
		{
		}

		// Token: 0x17001403 RID: 5123
		// (get) Token: 0x06003F4B RID: 16203 RVA: 0x000A5B48 File Offset: 0x000A3D48
		protected internal override ConfigurationElementProperty ElementProperty
		{
			get
			{
				return ProcessModelSection.elementProperty;
			}
		}

		/// <summary>Gets or sets a value indicating whether ASP.NET performance settings are automatically configured for ASP.NET applications. </summary>
		/// <returns>true if performance settings are automatically configured for ASP.NET applications; otherwise, false. The default value is false.</returns>
		// Token: 0x17001404 RID: 5124
		// (get) Token: 0x06003F4C RID: 16204 RVA: 0x000A5B4F File Offset: 0x000A3D4F
		// (set) Token: 0x06003F4D RID: 16205 RVA: 0x000A5B61 File Offset: 0x000A3D61
		[ConfigurationProperty("autoConfig", DefaultValue = "False")]
		public bool AutoConfig
		{
			get
			{
				return (bool)base[ProcessModelSection.autoConfigProp];
			}
			set
			{
				base[ProcessModelSection.autoConfigProp] = value;
			}
		}

		/// <summary>Gets or sets a value indicating how long a request is left in the queue. </summary>
		/// <returns>A <see cref="T:System.TimeSpan" /> value indicating the queuing time. The default value is 5 seconds.</returns>
		// Token: 0x17001405 RID: 5125
		// (get) Token: 0x06003F4E RID: 16206 RVA: 0x000A5B74 File Offset: 0x000A3D74
		// (set) Token: 0x06003F4F RID: 16207 RVA: 0x000A5B86 File Offset: 0x000A3D86
		[TypeConverter(typeof(InfiniteTimeSpanConverter))]
		[ConfigurationProperty("clientConnectedCheck", DefaultValue = "00:00:05")]
		public TimeSpan ClientConnectedCheck
		{
			get
			{
				return (TimeSpan)base[ProcessModelSection.clientConnectedCheckProp];
			}
			set
			{
				base[ProcessModelSection.clientConnectedCheckProp] = value;
			}
		}

		/// <summary>Gets or sets a value indicating the level of authentication for DCOM security.</summary>
		/// <returns>One of the <see cref="T:System.Web.Configuration.ProcessModelComAuthenticationLevel" /> values. The default value is <see cref="F:System.Web.Configuration.ProcessModelComAuthenticationLevel.Connect" />.</returns>
		// Token: 0x17001406 RID: 5126
		// (get) Token: 0x06003F50 RID: 16208 RVA: 0x000A5B99 File Offset: 0x000A3D99
		// (set) Token: 0x06003F51 RID: 16209 RVA: 0x000A5BAB File Offset: 0x000A3DAB
		[ConfigurationProperty("comAuthenticationLevel", DefaultValue = "Connect")]
		public ProcessModelComAuthenticationLevel ComAuthenticationLevel
		{
			get
			{
				return (ProcessModelComAuthenticationLevel)base[ProcessModelSection.comAuthenticationLevelProp];
			}
			set
			{
				base[ProcessModelSection.comAuthenticationLevelProp] = value;
			}
		}

		/// <summary>Gets or sets a value indicating the level of authentication for COM security.</summary>
		/// <returns>One of the <see cref="T:System.Web.Configuration.ProcessModelComImpersonationLevel" /> values. The default value is <see cref="F:System.Web.Configuration.ProcessModelComImpersonationLevel.Impersonate" />. </returns>
		// Token: 0x17001407 RID: 5127
		// (get) Token: 0x06003F52 RID: 16210 RVA: 0x000A5BBE File Offset: 0x000A3DBE
		// (set) Token: 0x06003F53 RID: 16211 RVA: 0x000A5BD0 File Offset: 0x000A3DD0
		[ConfigurationProperty("comImpersonationLevel", DefaultValue = "Impersonate")]
		public ProcessModelComImpersonationLevel ComImpersonationLevel
		{
			get
			{
				return (ProcessModelComImpersonationLevel)base[ProcessModelSection.comImpersonationLevelProp];
			}
			set
			{
				base[ProcessModelSection.comImpersonationLevelProp] = value;
			}
		}

		/// <summary>Gets or sets a value indicating which processors on a multiprocessor server are eligible to run ASP.NET processes. </summary>
		/// <returns>The number representing the bit pattern to apply. The default value is 0xFFFFFFFF.</returns>
		// Token: 0x17001408 RID: 5128
		// (get) Token: 0x06003F54 RID: 16212 RVA: 0x000A5BE3 File Offset: 0x000A3DE3
		// (set) Token: 0x06003F55 RID: 16213 RVA: 0x000A5BF5 File Offset: 0x000A3DF5
		[ConfigurationProperty("cpuMask", DefaultValue = "0xffffffff")]
		public int CpuMask
		{
			get
			{
				return (int)base[ProcessModelSection.cpuMaskProp];
			}
			set
			{
				base[ProcessModelSection.cpuMaskProp] = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the process model is enabled.</summary>
		/// <returns>true if the process model is enabled; otherwise, false. The default value is true.</returns>
		// Token: 0x17001409 RID: 5129
		// (get) Token: 0x06003F56 RID: 16214 RVA: 0x000A5C08 File Offset: 0x000A3E08
		// (set) Token: 0x06003F57 RID: 16215 RVA: 0x000A5C1A File Offset: 0x000A3E1A
		[ConfigurationProperty("enable", DefaultValue = "True")]
		public bool Enable
		{
			get
			{
				return (bool)base[ProcessModelSection.enableProp];
			}
			set
			{
				base[ProcessModelSection.enableProp] = value;
			}
		}

		/// <summary>Gets or sets a value indicating the period of inactivity after which ASP.NET automatically ends the worker process.</summary>
		/// <returns>A <see cref="T:System.TimeSpan" /> value indicating the idle time. The default value is Infinite, which corresponds to <see cref="F:System.TimeSpan.MaxValue" />. </returns>
		// Token: 0x1700140A RID: 5130
		// (get) Token: 0x06003F58 RID: 16216 RVA: 0x000A5C2D File Offset: 0x000A3E2D
		// (set) Token: 0x06003F59 RID: 16217 RVA: 0x000A5C3F File Offset: 0x000A3E3F
		[ConfigurationProperty("idleTimeout", DefaultValue = "10675199.02:48:05.4775807")]
		[TypeConverter(typeof(InfiniteTimeSpanConverter))]
		public TimeSpan IdleTimeout
		{
			get
			{
				return (TimeSpan)base[ProcessModelSection.idleTimeoutProp];
			}
			set
			{
				base[ProcessModelSection.idleTimeoutProp] = value;
			}
		}

		/// <summary>Gets or sets a value indicating the event types to be logged to the event log.</summary>
		/// <returns>One of the <see cref="T:System.Web.Configuration.ProcessModelLogLevel" /> values. The default value is <see cref="F:System.Web.Configuration.ProcessModelLogLevel.Errors" />. </returns>
		// Token: 0x1700140B RID: 5131
		// (get) Token: 0x06003F5A RID: 16218 RVA: 0x000A5C52 File Offset: 0x000A3E52
		// (set) Token: 0x06003F5B RID: 16219 RVA: 0x000A5C64 File Offset: 0x000A3E64
		[ConfigurationProperty("logLevel", DefaultValue = "Errors")]
		public ProcessModelLogLevel LogLevel
		{
			get
			{
				return (ProcessModelLogLevel)base[ProcessModelSection.logLevelProp];
			}
			set
			{
				base[ProcessModelSection.logLevelProp] = value;
			}
		}

		/// <summary>Gets or sets the maximum allowed number of application domains in one process.</summary>
		/// <returns>The maximum allowed number of application domains in one process.</returns>
		// Token: 0x1700140C RID: 5132
		// (get) Token: 0x06003F5C RID: 16220 RVA: 0x000A5C77 File Offset: 0x000A3E77
		// (set) Token: 0x06003F5D RID: 16221 RVA: 0x000A5C89 File Offset: 0x000A3E89
		[IntegerValidator(MinValue = 1, MaxValue = 2147483646)]
		[ConfigurationProperty("maxAppDomains", DefaultValue = "2000")]
		public int MaxAppDomains
		{
			get
			{
				return (int)base[ProcessModelSection.maxAppDomainsProp];
			}
			set
			{
				base[ProcessModelSection.maxAppDomainsProp] = value;
			}
		}

		/// <summary>Gets or sets a value indicating the maximum number of I/O threads per CPU in the CLR thread pool. </summary>
		/// <returns>The maximum number of threads. The default is 20.</returns>
		// Token: 0x1700140D RID: 5133
		// (get) Token: 0x06003F5E RID: 16222 RVA: 0x000A5C9C File Offset: 0x000A3E9C
		// (set) Token: 0x06003F5F RID: 16223 RVA: 0x000A5CAE File Offset: 0x000A3EAE
		[IntegerValidator(MinValue = 1, MaxValue = 2147483646)]
		[ConfigurationProperty("maxIoThreads", DefaultValue = "20")]
		public int MaxIOThreads
		{
			get
			{
				return (int)base[ProcessModelSection.maxIoThreadsProp];
			}
			set
			{
				base[ProcessModelSection.maxIoThreadsProp] = value;
			}
		}

		/// <summary>Gets or sets a value indicating the maximum amount of worker threads per CPU in the CLR thread pool. </summary>
		/// <returns>The maximum number of threads. The default is 20.</returns>
		// Token: 0x1700140E RID: 5134
		// (get) Token: 0x06003F60 RID: 16224 RVA: 0x000A5CC1 File Offset: 0x000A3EC1
		// (set) Token: 0x06003F61 RID: 16225 RVA: 0x000A5CD3 File Offset: 0x000A3ED3
		[IntegerValidator(MinValue = 1, MaxValue = 2147483646)]
		[ConfigurationProperty("maxWorkerThreads", DefaultValue = "20")]
		public int MaxWorkerThreads
		{
			get
			{
				return (int)base[ProcessModelSection.maxWorkerThreadsProp];
			}
			set
			{
				base[ProcessModelSection.maxWorkerThreadsProp] = value;
			}
		}

		/// <summary>Gets or sets a value indicating the maximum allowed memory size.</summary>
		/// <returns>The percentage of the total system memory. The default is 60 percent. </returns>
		// Token: 0x1700140F RID: 5135
		// (get) Token: 0x06003F62 RID: 16226 RVA: 0x000A5CE6 File Offset: 0x000A3EE6
		// (set) Token: 0x06003F63 RID: 16227 RVA: 0x000A5CF8 File Offset: 0x000A3EF8
		[ConfigurationProperty("memoryLimit", DefaultValue = "60")]
		public int MemoryLimit
		{
			get
			{
				return (int)base[ProcessModelSection.memoryLimitProp];
			}
			set
			{
				base[ProcessModelSection.memoryLimitProp] = value;
			}
		}

		/// <summary>Gets or sets the minimum number of I/O threads per CPU in the CLR thread pool.</summary>
		/// <returns>The minimum number of I/O threads per CPU in the CLR thread pool.</returns>
		// Token: 0x17001410 RID: 5136
		// (get) Token: 0x06003F64 RID: 16228 RVA: 0x000A5D0B File Offset: 0x000A3F0B
		// (set) Token: 0x06003F65 RID: 16229 RVA: 0x000A5D1D File Offset: 0x000A3F1D
		[ConfigurationProperty("minIoThreads", DefaultValue = "1")]
		[IntegerValidator(MinValue = 1, MaxValue = 2147483646)]
		public int MinIOThreads
		{
			get
			{
				return (int)base[ProcessModelSection.minIoThreadsProp];
			}
			set
			{
				base[ProcessModelSection.minIoThreadsProp] = value;
			}
		}

		/// <summary>Gets or sets the minimum number of worker threads per CPU in the CLR thread pool.</summary>
		/// <returns>The minimum number of worker threads per CPU in the CLR thread pool</returns>
		// Token: 0x17001411 RID: 5137
		// (get) Token: 0x06003F66 RID: 16230 RVA: 0x000A5D30 File Offset: 0x000A3F30
		// (set) Token: 0x06003F67 RID: 16231 RVA: 0x000A5D42 File Offset: 0x000A3F42
		[IntegerValidator(MinValue = 1, MaxValue = 2147483646)]
		[ConfigurationProperty("minWorkerThreads", DefaultValue = "1")]
		public int MinWorkerThreads
		{
			get
			{
				return (int)base[ProcessModelSection.minWorkerThreadsProp];
			}
			set
			{
				base[ProcessModelSection.minWorkerThreadsProp] = value;
			}
		}

		/// <summary>Gets or sets a value indicating the password to use for the Windows identity.</summary>
		/// <returns>The password to use. The default value is AutoGenerate.</returns>
		// Token: 0x17001412 RID: 5138
		// (get) Token: 0x06003F68 RID: 16232 RVA: 0x000A5D55 File Offset: 0x000A3F55
		// (set) Token: 0x06003F69 RID: 16233 RVA: 0x000A5D67 File Offset: 0x000A3F67
		[ConfigurationProperty("password", DefaultValue = "AutoGenerate")]
		public string Password
		{
			get
			{
				return (string)base[ProcessModelSection.passwordProp];
			}
			set
			{
				base[ProcessModelSection.passwordProp] = value;
			}
		}

		/// <summary>Gets or sets a value indicating the time interval at which the ISAPI extension pings the worker process to determine whether it is running.</summary>
		/// <returns>The <see cref="T:System.TimeSpan" /> defining the time interval. The default is 30 seconds.</returns>
		// Token: 0x17001413 RID: 5139
		// (get) Token: 0x06003F6A RID: 16234 RVA: 0x000A5D75 File Offset: 0x000A3F75
		// (set) Token: 0x06003F6B RID: 16235 RVA: 0x000A5D87 File Offset: 0x000A3F87
		[TypeConverter(typeof(InfiniteTimeSpanConverter))]
		[ConfigurationProperty("pingFrequency", DefaultValue = "10675199.02:48:05.4775807")]
		public TimeSpan PingFrequency
		{
			get
			{
				return (TimeSpan)base[ProcessModelSection.pingFrequencyProp];
			}
			set
			{
				base[ProcessModelSection.pingFrequencyProp] = value;
			}
		}

		/// <summary>Gets or sets a value indicating the time interval after which a non-responsive worker process is restarted.</summary>
		/// <returns>The <see cref="T:System.TimeSpan" /> defining the time interval. The default is 5 seconds.</returns>
		// Token: 0x17001414 RID: 5140
		// (get) Token: 0x06003F6C RID: 16236 RVA: 0x000A5D9A File Offset: 0x000A3F9A
		// (set) Token: 0x06003F6D RID: 16237 RVA: 0x000A5DAC File Offset: 0x000A3FAC
		[TypeConverter(typeof(InfiniteTimeSpanConverter))]
		[ConfigurationProperty("pingTimeout", DefaultValue = "10675199.02:48:05.4775807")]
		public TimeSpan PingTimeout
		{
			get
			{
				return (TimeSpan)base[ProcessModelSection.pingTimeoutProp];
			}
			set
			{
				base[ProcessModelSection.pingTimeoutProp] = value;
			}
		}

		/// <summary>Gets or sets a value indicating the number of requests allowed before a worker process is recycled.</summary>
		/// <returns>The number of allowed requests. The default is Infinite.</returns>
		// Token: 0x17001415 RID: 5141
		// (get) Token: 0x06003F6E RID: 16238 RVA: 0x000A5DBF File Offset: 0x000A3FBF
		// (set) Token: 0x06003F6F RID: 16239 RVA: 0x000A5DD1 File Offset: 0x000A3FD1
		[TypeConverter(typeof(InfiniteIntConverter))]
		[IntegerValidator(MinValue = 0, MaxValue = 2147483647)]
		[ConfigurationProperty("requestLimit", DefaultValue = "2147483647")]
		public int RequestLimit
		{
			get
			{
				return (int)base[ProcessModelSection.requestLimitProp];
			}
			set
			{
				base[ProcessModelSection.requestLimitProp] = value;
			}
		}

		/// <summary>Gets or sets a value indicating the number of requests allowed in the queue.</summary>
		/// <returns>The number of requests allowed to be queued. The default is 5000.</returns>
		// Token: 0x17001416 RID: 5142
		// (get) Token: 0x06003F70 RID: 16240 RVA: 0x000A5DE4 File Offset: 0x000A3FE4
		// (set) Token: 0x06003F71 RID: 16241 RVA: 0x000A5DF6 File Offset: 0x000A3FF6
		[TypeConverter(typeof(InfiniteIntConverter))]
		[IntegerValidator(MinValue = 0, MaxValue = 2147483647)]
		[ConfigurationProperty("requestQueueLimit", DefaultValue = "5000")]
		public int RequestQueueLimit
		{
			get
			{
				return (int)base[ProcessModelSection.requestQueueLimitProp];
			}
			set
			{
				base[ProcessModelSection.requestQueueLimitProp] = value;
			}
		}

		/// <summary>Gets or sets a value indicating the time interval for the worker process to respond.</summary>
		/// <returns>The <see cref="T:System.TimeSpan" /> defining the interval. The default is 3 minutes.</returns>
		// Token: 0x17001417 RID: 5143
		// (get) Token: 0x06003F72 RID: 16242 RVA: 0x000A5E09 File Offset: 0x000A4009
		// (set) Token: 0x06003F73 RID: 16243 RVA: 0x000A5E1B File Offset: 0x000A401B
		[ConfigurationProperty("responseDeadlockInterval", DefaultValue = "00:03:00")]
		[TimeSpanValidator(MinValueString = "00:00:00", MaxValueString = "10675199.02:48:05.4775807")]
		[TypeConverter(typeof(InfiniteTimeSpanConverter))]
		public TimeSpan ResponseDeadlockInterval
		{
			get
			{
				return (TimeSpan)base[ProcessModelSection.responseDeadlockIntervalProp];
			}
			set
			{
				base[ProcessModelSection.responseDeadlockIntervalProp] = value;
			}
		}

		/// <summary>No longer used.</summary>
		/// <returns>Not applicable.</returns>
		// Token: 0x17001418 RID: 5144
		// (get) Token: 0x06003F74 RID: 16244 RVA: 0x000A5E2E File Offset: 0x000A402E
		// (set) Token: 0x06003F75 RID: 16245 RVA: 0x000A5E40 File Offset: 0x000A4040
		[TypeConverter(typeof(InfiniteTimeSpanConverter))]
		[ConfigurationProperty("responseRestartDeadlockInterval", DefaultValue = "00:03:00")]
		public TimeSpan ResponseRestartDeadlockInterval
		{
			get
			{
				return (TimeSpan)base[ProcessModelSection.responseRestartDeadlockIntervalProp];
			}
			set
			{
				base[ProcessModelSection.responseRestartDeadlockIntervalProp] = value;
			}
		}

		/// <summary>Gets or sets a value indicating the maximum number of requests queued by the ISAPI while waiting for a new worker process to start handling the requests.</summary>
		/// <returns>The number of requests queued. The default is 10. </returns>
		// Token: 0x17001419 RID: 5145
		// (get) Token: 0x06003F76 RID: 16246 RVA: 0x000A5E53 File Offset: 0x000A4053
		// (set) Token: 0x06003F77 RID: 16247 RVA: 0x000A5E65 File Offset: 0x000A4065
		[TypeConverter(typeof(InfiniteIntConverter))]
		[IntegerValidator(MinValue = 0, MaxValue = 2147483647)]
		[ConfigurationProperty("restartQueueLimit", DefaultValue = "10")]
		public int RestartQueueLimit
		{
			get
			{
				return (int)base[ProcessModelSection.restartQueueLimitProp];
			}
			set
			{
				base[ProcessModelSection.restartQueueLimitProp] = value;
			}
		}

		/// <summary>Gets or sets a value indicating the file whose content must be used when a fatal error occurs.</summary>
		/// <returns>The path of the file used when a fatal error occurs.</returns>
		// Token: 0x1700141A RID: 5146
		// (get) Token: 0x06003F78 RID: 16248 RVA: 0x000A5E78 File Offset: 0x000A4078
		// (set) Token: 0x06003F79 RID: 16249 RVA: 0x000A5E8A File Offset: 0x000A408A
		[ConfigurationProperty("serverErrorMessageFile", DefaultValue = "")]
		public string ServerErrorMessageFile
		{
			get
			{
				return (string)base[ProcessModelSection.serverErrorMessageFileProp];
			}
			set
			{
				base[ProcessModelSection.serverErrorMessageFileProp] = value;
			}
		}

		/// <summary>Gets or sets a value indicating the time allowed for the worker process to shut down. </summary>
		/// <returns>The <see cref="T:System.TimeSpan" /> defining the interval. The default is 5 seconds.</returns>
		// Token: 0x1700141B RID: 5147
		// (get) Token: 0x06003F7A RID: 16250 RVA: 0x000A5E98 File Offset: 0x000A4098
		// (set) Token: 0x06003F7B RID: 16251 RVA: 0x000A5EAA File Offset: 0x000A40AA
		[ConfigurationProperty("shutdownTimeout", DefaultValue = "00:00:05")]
		[TypeConverter(typeof(InfiniteTimeSpanConverter))]
		[TimeSpanValidator(MinValueString = "00:00:00", MaxValueString = "10675199.02:48:05.4775807")]
		public TimeSpan ShutdownTimeout
		{
			get
			{
				return (TimeSpan)base[ProcessModelSection.shutdownTimeoutProp];
			}
			set
			{
				base[ProcessModelSection.shutdownTimeoutProp] = value;
			}
		}

		/// <summary>Gets or sets a value indicating the number of minutes until ASP.NET launches a new worker process.</summary>
		/// <returns>The <see cref="T:System.TimeSpan" /> defining the interval. The default is Infinite.</returns>
		// Token: 0x1700141C RID: 5148
		// (get) Token: 0x06003F7C RID: 16252 RVA: 0x000A5EBD File Offset: 0x000A40BD
		// (set) Token: 0x06003F7D RID: 16253 RVA: 0x000A5ECF File Offset: 0x000A40CF
		[TypeConverter(typeof(InfiniteTimeSpanConverter))]
		[ConfigurationProperty("timeout", DefaultValue = "10675199.02:48:05.4775807")]
		public TimeSpan Timeout
		{
			get
			{
				return (TimeSpan)base[ProcessModelSection.timeoutProp];
			}
			set
			{
				base[ProcessModelSection.timeoutProp] = value;
			}
		}

		/// <summary>Gets or sets a value indicating the user name for a Windows identity.</summary>
		/// <returns>The user name. The default value is Machine. </returns>
		// Token: 0x1700141D RID: 5149
		// (get) Token: 0x06003F7E RID: 16254 RVA: 0x000A5EE2 File Offset: 0x000A40E2
		// (set) Token: 0x06003F7F RID: 16255 RVA: 0x000A5EF4 File Offset: 0x000A40F4
		[ConfigurationProperty("userName", DefaultValue = "machine")]
		public string UserName
		{
			get
			{
				return (string)base[ProcessModelSection.userNameProp];
			}
			set
			{
				base[ProcessModelSection.userNameProp] = value;
			}
		}

		/// <summary>Gets or sets a value enabling the available CPUs to run the worker processes.</summary>
		/// <returns>true, if <see cref="P:System.Web.Configuration.ProcessModelSection.CpuMask" /> is used to map the worker processes to the number of eligible CPUs; false if <see cref="P:System.Web.Configuration.ProcessModelSection.CpuMask" /> is ignored.</returns>
		// Token: 0x1700141E RID: 5150
		// (get) Token: 0x06003F80 RID: 16256 RVA: 0x000A5F02 File Offset: 0x000A4102
		// (set) Token: 0x06003F81 RID: 16257 RVA: 0x000A5F14 File Offset: 0x000A4114
		[ConfigurationProperty("webGarden", DefaultValue = "False")]
		public bool WebGarden
		{
			get
			{
				return (bool)base[ProcessModelSection.webGardenProp];
			}
			set
			{
				base[ProcessModelSection.webGardenProp] = value;
			}
		}

		// Token: 0x1700141F RID: 5151
		// (get) Token: 0x06003F82 RID: 16258 RVA: 0x000A5F27 File Offset: 0x000A4127
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return ProcessModelSection.properties;
			}
		}

		// Token: 0x0400228C RID: 8844
		private static ConfigurationProperty autoConfigProp = new ConfigurationProperty("autoConfig", typeof(bool), false);

		// Token: 0x0400228D RID: 8845
		private static ConfigurationProperty clientConnectedCheckProp = new ConfigurationProperty("clientConnectedCheck", typeof(TimeSpan), TimeSpan.FromSeconds(5.0), PropertyHelper.InfiniteTimeSpanConverter, PropertyHelper.DefaultValidator, ConfigurationPropertyOptions.None);

		// Token: 0x0400228E RID: 8846
		private static ConfigurationProperty comAuthenticationLevelProp = new ConfigurationProperty("comAuthenticationLevel", typeof(ProcessModelComAuthenticationLevel), ProcessModelComAuthenticationLevel.Connect, new GenericEnumConverter(typeof(ProcessModelComAuthenticationLevel)), PropertyHelper.DefaultValidator, ConfigurationPropertyOptions.None);

		// Token: 0x0400228F RID: 8847
		private static ConfigurationProperty comImpersonationLevelProp = new ConfigurationProperty("comImpersonationLevel", typeof(ProcessModelComImpersonationLevel), ProcessModelComImpersonationLevel.Impersonate, new GenericEnumConverter(typeof(ProcessModelComImpersonationLevel)), PropertyHelper.DefaultValidator, ConfigurationPropertyOptions.None);

		// Token: 0x04002290 RID: 8848
		private static ConfigurationProperty cpuMaskProp = new ConfigurationProperty("cpuMask", typeof(int), 268435455);

		// Token: 0x04002291 RID: 8849
		private static ConfigurationProperty enableProp = new ConfigurationProperty("enable", typeof(bool), true);

		// Token: 0x04002292 RID: 8850
		private static ConfigurationProperty idleTimeoutProp = new ConfigurationProperty("idleTimeout", typeof(TimeSpan), TimeSpan.MaxValue, PropertyHelper.InfiniteTimeSpanConverter, PropertyHelper.DefaultValidator, ConfigurationPropertyOptions.None);

		// Token: 0x04002293 RID: 8851
		private static ConfigurationProperty logLevelProp = new ConfigurationProperty("logLevel", typeof(ProcessModelLogLevel), ProcessModelLogLevel.Errors, new GenericEnumConverter(typeof(ProcessModelLogLevel)), PropertyHelper.DefaultValidator, ConfigurationPropertyOptions.None);

		// Token: 0x04002294 RID: 8852
		private static ConfigurationProperty maxAppDomainsProp = new ConfigurationProperty("maxAppDomains", typeof(int), 2000, TypeDescriptor.GetConverter(typeof(int)), PropertyHelper.IntFromOneToMax_1Validator, ConfigurationPropertyOptions.None);

		// Token: 0x04002295 RID: 8853
		private static ConfigurationProperty maxIoThreadsProp = new ConfigurationProperty("maxIoThreads", typeof(int), 20, TypeDescriptor.GetConverter(typeof(int)), PropertyHelper.IntFromOneToMax_1Validator, ConfigurationPropertyOptions.None);

		// Token: 0x04002296 RID: 8854
		private static ConfigurationProperty maxWorkerThreadsProp = new ConfigurationProperty("maxWorkerThreads", typeof(int), 20, TypeDescriptor.GetConverter(typeof(int)), PropertyHelper.IntFromOneToMax_1Validator, ConfigurationPropertyOptions.None);

		// Token: 0x04002297 RID: 8855
		private static ConfigurationProperty memoryLimitProp = new ConfigurationProperty("memoryLimit", typeof(int), 60);

		// Token: 0x04002298 RID: 8856
		private static ConfigurationProperty minIoThreadsProp = new ConfigurationProperty("minIoThreads", typeof(int), 1, TypeDescriptor.GetConverter(typeof(int)), PropertyHelper.IntFromOneToMax_1Validator, ConfigurationPropertyOptions.None);

		// Token: 0x04002299 RID: 8857
		private static ConfigurationProperty minWorkerThreadsProp = new ConfigurationProperty("minWorkerThreads", typeof(int), 1, TypeDescriptor.GetConverter(typeof(int)), PropertyHelper.IntFromOneToMax_1Validator, ConfigurationPropertyOptions.None);

		// Token: 0x0400229A RID: 8858
		private static ConfigurationProperty passwordProp = new ConfigurationProperty("password", typeof(string), "AutoGenerate");

		// Token: 0x0400229B RID: 8859
		private static ConfigurationProperty pingFrequencyProp = new ConfigurationProperty("pingFrequency", typeof(TimeSpan), TimeSpan.MaxValue, PropertyHelper.InfiniteTimeSpanConverter, PropertyHelper.DefaultValidator, ConfigurationPropertyOptions.None);

		// Token: 0x0400229C RID: 8860
		private static ConfigurationProperty pingTimeoutProp = new ConfigurationProperty("pingTimeout", typeof(TimeSpan), TimeSpan.MaxValue, PropertyHelper.InfiniteTimeSpanConverter, PropertyHelper.DefaultValidator, ConfigurationPropertyOptions.None);

		// Token: 0x0400229D RID: 8861
		private static ConfigurationProperty requestLimitProp = new ConfigurationProperty("requestLimit", typeof(int), int.MaxValue, PropertyHelper.InfiniteIntConverter, PropertyHelper.IntFromZeroToMaxValidator, ConfigurationPropertyOptions.None);

		// Token: 0x0400229E RID: 8862
		private static ConfigurationProperty requestQueueLimitProp = new ConfigurationProperty("requestQueueLimit", typeof(int), 5000, PropertyHelper.InfiniteIntConverter, PropertyHelper.IntFromZeroToMaxValidator, ConfigurationPropertyOptions.None);

		// Token: 0x0400229F RID: 8863
		private static ConfigurationProperty responseDeadlockIntervalProp = new ConfigurationProperty("responseDeadlockInterval", typeof(TimeSpan), TimeSpan.FromMinutes(3.0), PropertyHelper.InfiniteTimeSpanConverter, PropertyHelper.PositiveTimeSpanValidator, ConfigurationPropertyOptions.None);

		// Token: 0x040022A0 RID: 8864
		private static ConfigurationProperty responseRestartDeadlockIntervalProp = new ConfigurationProperty("responseRestartDeadlockInterval", typeof(TimeSpan), TimeSpan.FromMinutes(3.0), PropertyHelper.InfiniteTimeSpanConverter, PropertyHelper.DefaultValidator, ConfigurationPropertyOptions.None);

		// Token: 0x040022A1 RID: 8865
		private static ConfigurationProperty restartQueueLimitProp = new ConfigurationProperty("restartQueueLimit", typeof(int), 10, PropertyHelper.InfiniteIntConverter, PropertyHelper.IntFromZeroToMaxValidator, ConfigurationPropertyOptions.None);

		// Token: 0x040022A2 RID: 8866
		private static ConfigurationProperty serverErrorMessageFileProp = new ConfigurationProperty("serverErrorMessageFile", typeof(string), "");

		// Token: 0x040022A3 RID: 8867
		private static ConfigurationProperty shutdownTimeoutProp = new ConfigurationProperty("shutdownTimeout", typeof(TimeSpan), TimeSpan.FromSeconds(5.0), PropertyHelper.InfiniteTimeSpanConverter, PropertyHelper.PositiveTimeSpanValidator, ConfigurationPropertyOptions.None);

		// Token: 0x040022A4 RID: 8868
		private static ConfigurationProperty timeoutProp = new ConfigurationProperty("timeout", typeof(TimeSpan), TimeSpan.MaxValue, PropertyHelper.InfiniteTimeSpanConverter, PropertyHelper.DefaultValidator, ConfigurationPropertyOptions.None);

		// Token: 0x040022A5 RID: 8869
		private static ConfigurationProperty userNameProp = new ConfigurationProperty("userName", typeof(string), "machine");

		// Token: 0x040022A6 RID: 8870
		private static ConfigurationProperty webGardenProp = new ConfigurationProperty("webGarden", typeof(bool), false);

		// Token: 0x040022A7 RID: 8871
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x040022A8 RID: 8872
		private static ConfigurationElementProperty elementProperty;
	}
}
