using System;
using System.ComponentModel;
using System.Configuration;
using Unity;

namespace System.Web.Configuration
{
	/// <summary>Configures the ASP.NET HTTP runtime. This class cannot be inherited.</summary>
	// Token: 0x020005AD RID: 1453
	public sealed class HttpRuntimeSection : ConfigurationSection
	{
		// Token: 0x06003E23 RID: 15907 RVA: 0x000A295C File Offset: 0x000A0B5C
		static HttpRuntimeSection()
		{
			HttpRuntimeSection.properties.Add(HttpRuntimeSection.apartmentThreadingProp);
			HttpRuntimeSection.properties.Add(HttpRuntimeSection.appRequestQueueLimitProp);
			HttpRuntimeSection.properties.Add(HttpRuntimeSection.delayNotificationTimeoutProp);
			HttpRuntimeSection.properties.Add(HttpRuntimeSection.enableProp);
			HttpRuntimeSection.properties.Add(HttpRuntimeSection.enableHeaderCheckingProp);
			HttpRuntimeSection.properties.Add(HttpRuntimeSection.enableKernelOutputCacheProp);
			HttpRuntimeSection.properties.Add(HttpRuntimeSection.enableVersionHeaderProp);
			HttpRuntimeSection.properties.Add(HttpRuntimeSection.executionTimeoutProp);
			HttpRuntimeSection.properties.Add(HttpRuntimeSection.maxRequestLengthProp);
			HttpRuntimeSection.properties.Add(HttpRuntimeSection.maxWaitChangeNotificationProp);
			HttpRuntimeSection.properties.Add(HttpRuntimeSection.minFreeThreadsProp);
			HttpRuntimeSection.properties.Add(HttpRuntimeSection.minLocalRequestFreeThreadsProp);
			HttpRuntimeSection.properties.Add(HttpRuntimeSection.requestLengthDiskThresholdProp);
			HttpRuntimeSection.properties.Add(HttpRuntimeSection.requireRootedSaveAsPathProp);
			HttpRuntimeSection.properties.Add(HttpRuntimeSection.sendCacheControlHeaderProp);
			HttpRuntimeSection.properties.Add(HttpRuntimeSection.shutdownTimeoutProp);
			HttpRuntimeSection.properties.Add(HttpRuntimeSection.useFullyQualifiedRedirectUrlProp);
			HttpRuntimeSection.properties.Add(HttpRuntimeSection.waitChangeNotificationProp);
			HttpRuntimeSection.properties.Add(HttpRuntimeSection.requestPathInvalidCharactersProp);
			HttpRuntimeSection.properties.Add(HttpRuntimeSection.requestValidationTypeProp);
			HttpRuntimeSection.properties.Add(HttpRuntimeSection.requestValidationModeProp);
			HttpRuntimeSection.properties.Add(HttpRuntimeSection.maxQueryStringLengthProp);
			HttpRuntimeSection.properties.Add(HttpRuntimeSection.maxUrlLengthProp);
			HttpRuntimeSection.properties.Add(HttpRuntimeSection.encoderTypeProp);
			HttpRuntimeSection.properties.Add(HttpRuntimeSection.relaxedUrlToFileSystemMappingProp);
			HttpRuntimeSection.properties.Add(HttpRuntimeSection.targetFrameworkProp);
			HttpRuntimeSection.properties.Add(HttpRuntimeSection.allowDynamicModuleRegistrationProp);
		}

		/// <summary>Gets or sets a value that indicates whether application apartment threading is enabled.</summary>
		/// <returns>true if application apartment threading is enabled; otherwise, false. </returns>
		// Token: 0x1700138D RID: 5005
		// (get) Token: 0x06003E25 RID: 15909 RVA: 0x000A2FAF File Offset: 0x000A11AF
		// (set) Token: 0x06003E26 RID: 15910 RVA: 0x000A2FC1 File Offset: 0x000A11C1
		[ConfigurationProperty("apartmentThreading", DefaultValue = "False")]
		public bool ApartmentThreading
		{
			get
			{
				return (bool)base[HttpRuntimeSection.apartmentThreadingProp];
			}
			set
			{
				base[HttpRuntimeSection.apartmentThreadingProp] = value;
			}
		}

		/// <summary>Gets or sets a value that indicates the maximum number of requests that ASP.NET queues for the application.</summary>
		/// <returns>The maximum number of requests that can be queued.</returns>
		// Token: 0x1700138E RID: 5006
		// (get) Token: 0x06003E27 RID: 15911 RVA: 0x000A2FD4 File Offset: 0x000A11D4
		// (set) Token: 0x06003E28 RID: 15912 RVA: 0x000A2FE6 File Offset: 0x000A11E6
		[IntegerValidator(MinValue = 1, MaxValue = 2147483647)]
		[ConfigurationProperty("appRequestQueueLimit", DefaultValue = "5000")]
		public int AppRequestQueueLimit
		{
			get
			{
				return (int)base[HttpRuntimeSection.appRequestQueueLimitProp];
			}
			set
			{
				base[HttpRuntimeSection.appRequestQueueLimitProp] = value;
			}
		}

		/// <summary>Gets or sets the change notification delay.</summary>
		/// <returns>The time, in seconds, that specifies the change notification delay.</returns>
		// Token: 0x1700138F RID: 5007
		// (get) Token: 0x06003E29 RID: 15913 RVA: 0x000A2FF9 File Offset: 0x000A11F9
		// (set) Token: 0x06003E2A RID: 15914 RVA: 0x000A300B File Offset: 0x000A120B
		[ConfigurationProperty("delayNotificationTimeout", DefaultValue = "00:00:05")]
		[TypeConverter(typeof(TimeSpanSecondsConverter))]
		public TimeSpan DelayNotificationTimeout
		{
			get
			{
				return (TimeSpan)base[HttpRuntimeSection.delayNotificationTimeoutProp];
			}
			set
			{
				base[HttpRuntimeSection.delayNotificationTimeoutProp] = value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether the application domain is enabled.</summary>
		/// <returns>true if the application domain is enabled; otherwise, false. The default value is true.</returns>
		// Token: 0x17001390 RID: 5008
		// (get) Token: 0x06003E2B RID: 15915 RVA: 0x000A301E File Offset: 0x000A121E
		// (set) Token: 0x06003E2C RID: 15916 RVA: 0x000A3030 File Offset: 0x000A1230
		[ConfigurationProperty("enable", DefaultValue = "True")]
		public bool Enable
		{
			get
			{
				return (bool)base[HttpRuntimeSection.enableProp];
			}
			set
			{
				base[HttpRuntimeSection.enableProp] = value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether the header checking is enabled.</summary>
		/// <returns>true if the header checking is enabled; otherwise, false. The default value is true. </returns>
		// Token: 0x17001391 RID: 5009
		// (get) Token: 0x06003E2D RID: 15917 RVA: 0x000A3043 File Offset: 0x000A1243
		// (set) Token: 0x06003E2E RID: 15918 RVA: 0x000A3055 File Offset: 0x000A1255
		[ConfigurationProperty("enableHeaderChecking", DefaultValue = "True")]
		public bool EnableHeaderChecking
		{
			get
			{
				return (bool)base[HttpRuntimeSection.enableHeaderCheckingProp];
			}
			set
			{
				base[HttpRuntimeSection.enableHeaderCheckingProp] = value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether output caching is enabled.</summary>
		/// <returns>true if output caching is enabled; otherwise, false. The default value is true. </returns>
		// Token: 0x17001392 RID: 5010
		// (get) Token: 0x06003E2F RID: 15919 RVA: 0x000A3068 File Offset: 0x000A1268
		// (set) Token: 0x06003E30 RID: 15920 RVA: 0x000A307A File Offset: 0x000A127A
		[ConfigurationProperty("enableKernelOutputCache", DefaultValue = "True")]
		public bool EnableKernelOutputCache
		{
			get
			{
				return (bool)base[HttpRuntimeSection.enableKernelOutputCacheProp];
			}
			set
			{
				base[HttpRuntimeSection.enableKernelOutputCacheProp] = value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether ASP.NET should output a version header.</summary>
		/// <returns>true if the output of the version header is enabled; otherwise, false. The default value is true.</returns>
		// Token: 0x17001393 RID: 5011
		// (get) Token: 0x06003E31 RID: 15921 RVA: 0x000A308D File Offset: 0x000A128D
		// (set) Token: 0x06003E32 RID: 15922 RVA: 0x000A309F File Offset: 0x000A129F
		[ConfigurationProperty("enableVersionHeader", DefaultValue = "True")]
		public bool EnableVersionHeader
		{
			get
			{
				return (bool)base[HttpRuntimeSection.enableVersionHeaderProp];
			}
			set
			{
				base[HttpRuntimeSection.enableVersionHeaderProp] = value;
			}
		}

		/// <summary>Gets or sets the allowed execution time for the request.</summary>
		/// <returns>A <see cref="T:System.TimeSpan" /> value that indicates the allowed execution time for the request.</returns>
		/// <exception cref="T:System.Web.HttpException">The request execution time exceeded the limit set by the execution time-out.</exception>
		// Token: 0x17001394 RID: 5012
		// (get) Token: 0x06003E33 RID: 15923 RVA: 0x000A30B2 File Offset: 0x000A12B2
		// (set) Token: 0x06003E34 RID: 15924 RVA: 0x000A30C4 File Offset: 0x000A12C4
		[ConfigurationProperty("executionTimeout", DefaultValue = "00:01:50")]
		[TimeSpanValidator(MinValueString = "00:00:00")]
		[TypeConverter(typeof(TimeSpanSecondsConverter))]
		public TimeSpan ExecutionTimeout
		{
			get
			{
				return (TimeSpan)base[HttpRuntimeSection.executionTimeoutProp];
			}
			set
			{
				base[HttpRuntimeSection.executionTimeoutProp] = value;
			}
		}

		/// <summary>Gets or sets the maximum request size.</summary>
		/// <returns>The maximum request size in kilobytes. The default size is 4096 KB (4 MB).</returns>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">The selected value is less than <see cref="P:System.Web.Configuration.HttpRuntimeSection.RequestLengthDiskThreshold" />.</exception>
		// Token: 0x17001395 RID: 5013
		// (get) Token: 0x06003E35 RID: 15925 RVA: 0x000A30D7 File Offset: 0x000A12D7
		// (set) Token: 0x06003E36 RID: 15926 RVA: 0x000A30E9 File Offset: 0x000A12E9
		[ConfigurationProperty("maxRequestLength", DefaultValue = "4096")]
		[IntegerValidator(MinValue = 0, MaxValue = 2147483647)]
		public int MaxRequestLength
		{
			get
			{
				return (int)base[HttpRuntimeSection.maxRequestLengthProp];
			}
			set
			{
				base[HttpRuntimeSection.maxRequestLengthProp] = value;
			}
		}

		/// <summary>Gets or sets the time interval between the first change notification and the time at which the application domain is restarted.</summary>
		/// <returns>The maximum time interval, in seconds, from the first change notification and the time when the application domain is restarted.</returns>
		// Token: 0x17001396 RID: 5014
		// (get) Token: 0x06003E37 RID: 15927 RVA: 0x000A30FC File Offset: 0x000A12FC
		// (set) Token: 0x06003E38 RID: 15928 RVA: 0x000A310E File Offset: 0x000A130E
		[ConfigurationProperty("maxWaitChangeNotification", DefaultValue = "0")]
		[IntegerValidator(MinValue = 0, MaxValue = 2147483647)]
		public int MaxWaitChangeNotification
		{
			get
			{
				return (int)base[HttpRuntimeSection.maxWaitChangeNotificationProp];
			}
			set
			{
				base[HttpRuntimeSection.maxWaitChangeNotificationProp] = value;
			}
		}

		/// <summary>Gets or sets the minimum number of threads that must be free before a request for resources in this configuration scope can be serviced.</summary>
		/// <returns>The minimum number of free threads in the common language runtime (CLR) thread pool before a request in this configuration scope will be serviced. The default value is 8.</returns>
		// Token: 0x17001397 RID: 5015
		// (get) Token: 0x06003E39 RID: 15929 RVA: 0x000A3121 File Offset: 0x000A1321
		// (set) Token: 0x06003E3A RID: 15930 RVA: 0x000A3133 File Offset: 0x000A1333
		[ConfigurationProperty("minFreeThreads", DefaultValue = "8")]
		[IntegerValidator(MinValue = 0, MaxValue = 2147483647)]
		public int MinFreeThreads
		{
			get
			{
				return (int)base[HttpRuntimeSection.minFreeThreadsProp];
			}
			set
			{
				base[HttpRuntimeSection.minFreeThreadsProp] = value;
			}
		}

		/// <summary>Gets or sets the minimum number of free threads required to service a local request.</summary>
		/// <returns>The minimum number of free threads assigned to local requests. The default value is 4.</returns>
		// Token: 0x17001398 RID: 5016
		// (get) Token: 0x06003E3B RID: 15931 RVA: 0x000A3146 File Offset: 0x000A1346
		// (set) Token: 0x06003E3C RID: 15932 RVA: 0x000A3158 File Offset: 0x000A1358
		[IntegerValidator(MinValue = 0, MaxValue = 2147483647)]
		[ConfigurationProperty("minLocalRequestFreeThreads", DefaultValue = "4")]
		public int MinLocalRequestFreeThreads
		{
			get
			{
				return (int)base[HttpRuntimeSection.minLocalRequestFreeThreadsProp];
			}
			set
			{
				base[HttpRuntimeSection.minLocalRequestFreeThreadsProp] = value;
			}
		}

		/// <summary>Gets or sets the input-stream buffering threshold.</summary>
		/// <returns>The number of bytes that indicate the input-stream buffering threshold. The default is 80 kilobytes.</returns>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">The selected value is greater than <see cref="P:System.Web.Configuration.HttpRuntimeSection.MaxRequestLength" />.</exception>
		// Token: 0x17001399 RID: 5017
		// (get) Token: 0x06003E3D RID: 15933 RVA: 0x000A316B File Offset: 0x000A136B
		// (set) Token: 0x06003E3E RID: 15934 RVA: 0x000A317D File Offset: 0x000A137D
		[IntegerValidator(MinValue = 1, MaxValue = 2147483647)]
		[ConfigurationProperty("requestLengthDiskThreshold", DefaultValue = "80")]
		public int RequestLengthDiskThreshold
		{
			get
			{
				return (int)base[HttpRuntimeSection.requestLengthDiskThresholdProp];
			}
			set
			{
				base[HttpRuntimeSection.requestLengthDiskThresholdProp] = value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether the file name must be a fully qualified physical file path.</summary>
		/// <returns>true if the file name must be a fully qualified physical file path; otherwise, false. The default value is true.</returns>
		// Token: 0x1700139A RID: 5018
		// (get) Token: 0x06003E3F RID: 15935 RVA: 0x000A3190 File Offset: 0x000A1390
		// (set) Token: 0x06003E40 RID: 15936 RVA: 0x000A31A2 File Offset: 0x000A13A2
		[ConfigurationProperty("requireRootedSaveAsPath", DefaultValue = "True")]
		public bool RequireRootedSaveAsPath
		{
			get
			{
				return (bool)base[HttpRuntimeSection.requireRootedSaveAsPathProp];
			}
			set
			{
				base[HttpRuntimeSection.requireRootedSaveAsPathProp] = value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether the cache-control:private header is sent as part of the HTTP response.</summary>
		/// <returns>true if the cache-control:private header is to be sent; otherwise, false. The default value is false.</returns>
		// Token: 0x1700139B RID: 5019
		// (get) Token: 0x06003E41 RID: 15937 RVA: 0x000A31B5 File Offset: 0x000A13B5
		// (set) Token: 0x06003E42 RID: 15938 RVA: 0x000A31C7 File Offset: 0x000A13C7
		[ConfigurationProperty("sendCacheControlHeader", DefaultValue = "True")]
		public bool SendCacheControlHeader
		{
			get
			{
				return (bool)base[HttpRuntimeSection.sendCacheControlHeaderProp];
			}
			set
			{
				base[HttpRuntimeSection.sendCacheControlHeaderProp] = value;
			}
		}

		/// <summary>Gets or sets the length of time the application is allowed to idle before it is terminated.</summary>
		/// <returns>A <see cref="T:System.TimeSpan" /> value that indicates the length of time the application is allowed to idle before it is terminated.</returns>
		// Token: 0x1700139C RID: 5020
		// (get) Token: 0x06003E43 RID: 15939 RVA: 0x000A31DA File Offset: 0x000A13DA
		// (set) Token: 0x06003E44 RID: 15940 RVA: 0x000A31EC File Offset: 0x000A13EC
		[TypeConverter(typeof(TimeSpanSecondsConverter))]
		[ConfigurationProperty("shutdownTimeout", DefaultValue = "00:01:30")]
		public TimeSpan ShutdownTimeout
		{
			get
			{
				return (TimeSpan)base[HttpRuntimeSection.shutdownTimeoutProp];
			}
			set
			{
				base[HttpRuntimeSection.shutdownTimeoutProp] = value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether the client-side redirects are fully qualified.</summary>
		/// <returns>true if client-side redirects are fully qualified; otherwise, false. The default value is false.</returns>
		// Token: 0x1700139D RID: 5021
		// (get) Token: 0x06003E45 RID: 15941 RVA: 0x000A31FF File Offset: 0x000A13FF
		// (set) Token: 0x06003E46 RID: 15942 RVA: 0x000A3211 File Offset: 0x000A1411
		[ConfigurationProperty("useFullyQualifiedRedirectUrl", DefaultValue = "False")]
		public bool UseFullyQualifiedRedirectUrl
		{
			get
			{
				return (bool)base[HttpRuntimeSection.useFullyQualifiedRedirectUrlProp];
			}
			set
			{
				base[HttpRuntimeSection.useFullyQualifiedRedirectUrlProp] = value;
			}
		}

		/// <summary>Gets or sets the waiting time before the next change notification.</summary>
		/// <returns>The waiting time, in seconds, before the next change notification that triggers an application domain to restart. The default value is 0.</returns>
		// Token: 0x1700139E RID: 5022
		// (get) Token: 0x06003E47 RID: 15943 RVA: 0x000A3224 File Offset: 0x000A1424
		// (set) Token: 0x06003E48 RID: 15944 RVA: 0x000A3236 File Offset: 0x000A1436
		[IntegerValidator(MinValue = 0, MaxValue = 2147483647)]
		[ConfigurationProperty("waitChangeNotification", DefaultValue = "0")]
		public int WaitChangeNotification
		{
			get
			{
				return (int)base[HttpRuntimeSection.waitChangeNotificationProp];
			}
			set
			{
				base[HttpRuntimeSection.waitChangeNotificationProp] = value;
			}
		}

		/// <summary>Gets or sets a list of characters that are specified as invalid in a path that is part of an HTTP request.</summary>
		/// <returns>A comma-separated list of invalid characters. The following list contains the default set of invalid characters: &lt;,&gt;,*,%,&amp;,:,\\</returns>
		// Token: 0x1700139F RID: 5023
		// (get) Token: 0x06003E49 RID: 15945 RVA: 0x000A3249 File Offset: 0x000A1449
		// (set) Token: 0x06003E4A RID: 15946 RVA: 0x000A325B File Offset: 0x000A145B
		[ConfigurationProperty("requestPathInvalidCharacters", DefaultValue = ",*,%,&,:,\\,?")]
		public string RequestPathInvalidCharacters
		{
			get
			{
				return (string)base[HttpRuntimeSection.requestPathInvalidCharactersProp];
			}
			set
			{
				base[HttpRuntimeSection.requestPathInvalidCharactersProp] = value;
			}
		}

		/// <summary>Gets or sets the name of a type that is used to validate HTTP requests.</summary>
		/// <returns>The name of a type that handles request validation tasks. The default is the fully qualified name of the <see cref="T:System.Web.Util.RequestValidator" /> type that ASP.NET uses for validation.</returns>
		// Token: 0x170013A0 RID: 5024
		// (get) Token: 0x06003E4B RID: 15947 RVA: 0x000A3269 File Offset: 0x000A1469
		// (set) Token: 0x06003E4C RID: 15948 RVA: 0x000A327B File Offset: 0x000A147B
		[ConfigurationProperty("requestValidationType", DefaultValue = "System.Web.Util.RequestValidator")]
		[StringValidator(MinLength = 1)]
		public string RequestValidationType
		{
			get
			{
				return (string)base[HttpRuntimeSection.requestValidationTypeProp];
			}
			set
			{
				base[HttpRuntimeSection.requestValidationTypeProp] = value;
			}
		}

		/// <summary>Gets or sets a version number that indicates which ASP.NET version-specific approach to validation will be used.</summary>
		/// <returns>A value that indicates which ASP.NET version-specific approach to validation will be used. The default is 4.0. </returns>
		// Token: 0x170013A1 RID: 5025
		// (get) Token: 0x06003E4D RID: 15949 RVA: 0x000A3289 File Offset: 0x000A1489
		// (set) Token: 0x06003E4E RID: 15950 RVA: 0x000A329B File Offset: 0x000A149B
		[ConfigurationProperty("requestValidationMode", DefaultValue = "4.0")]
		[TypeConverter("System.Web.Configuration.VersionConverter")]
		public Version RequestValidationMode
		{
			get
			{
				return (Version)base[HttpRuntimeSection.requestValidationModeProp];
			}
			set
			{
				base[HttpRuntimeSection.requestValidationModeProp] = value;
			}
		}

		/// <summary>Gets or sets the maximum possible length, in number of characters, of a query string in an HTTP request.</summary>
		/// <returns>The maximum length of the query string, in number of characters. The default is 2048. </returns>
		// Token: 0x170013A2 RID: 5026
		// (get) Token: 0x06003E4F RID: 15951 RVA: 0x000A32A9 File Offset: 0x000A14A9
		// (set) Token: 0x06003E50 RID: 15952 RVA: 0x000A32BB File Offset: 0x000A14BB
		[ConfigurationProperty("maxQueryStringLength", DefaultValue = "2048")]
		[IntegerValidator(MinValue = 0)]
		public int MaxQueryStringLength
		{
			get
			{
				return (int)base[HttpRuntimeSection.maxQueryStringLengthProp];
			}
			set
			{
				base[HttpRuntimeSection.maxQueryStringLengthProp] = value;
			}
		}

		/// <summary>Gets or sets the maximum possible length, in number of characters, of the URL in an HTTP request.</summary>
		/// <returns>The length of the URL, in number of characters. The default is 260.</returns>
		// Token: 0x170013A3 RID: 5027
		// (get) Token: 0x06003E51 RID: 15953 RVA: 0x000A32CE File Offset: 0x000A14CE
		// (set) Token: 0x06003E52 RID: 15954 RVA: 0x000A32E0 File Offset: 0x000A14E0
		[IntegerValidator(MinValue = 0)]
		[ConfigurationProperty("maxUrlLength", DefaultValue = "260")]
		public int MaxUrlLength
		{
			get
			{
				return (int)base[HttpRuntimeSection.maxUrlLengthProp];
			}
			set
			{
				base[HttpRuntimeSection.maxUrlLengthProp] = value;
			}
		}

		/// <summary>Gets or sets the name of a custom type that can be used to handle HTML and URL encoding. </summary>
		/// <returns>The name of a type that can be used to handle HTML and URL encoding. </returns>
		// Token: 0x170013A4 RID: 5028
		// (get) Token: 0x06003E53 RID: 15955 RVA: 0x000A32F3 File Offset: 0x000A14F3
		// (set) Token: 0x06003E54 RID: 15956 RVA: 0x000A3305 File Offset: 0x000A1505
		[StringValidator(MinLength = 1)]
		[ConfigurationProperty("encoderType", DefaultValue = "System.Web.Util.HttpEncoder")]
		public string EncoderType
		{
			get
			{
				return (string)base[HttpRuntimeSection.encoderTypeProp];
			}
			set
			{
				base[HttpRuntimeSection.encoderTypeProp] = value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether the URL in an HTTP request is required to be a valid Windows file path.</summary>
		/// <returns>true if the URL does not have to comply with Windows path rules; otherwise false. The default is false. </returns>
		// Token: 0x170013A5 RID: 5029
		// (get) Token: 0x06003E55 RID: 15957 RVA: 0x000A3313 File Offset: 0x000A1513
		// (set) Token: 0x06003E56 RID: 15958 RVA: 0x000A3325 File Offset: 0x000A1525
		[ConfigurationProperty("relaxedUrlToFileSystemMapping", DefaultValue = "False")]
		public bool RelaxedUrlToFileSystemMapping
		{
			get
			{
				return (bool)base[HttpRuntimeSection.relaxedUrlToFileSystemMappingProp];
			}
			set
			{
				base[HttpRuntimeSection.relaxedUrlToFileSystemMappingProp] = value;
			}
		}

		/// <summary>Gets or sets the target .NET framework.</summary>
		/// <returns>The target .NET framework.</returns>
		// Token: 0x170013A6 RID: 5030
		// (get) Token: 0x06003E57 RID: 15959 RVA: 0x000A3338 File Offset: 0x000A1538
		// (set) Token: 0x06003E58 RID: 15960 RVA: 0x000A334A File Offset: 0x000A154A
		[TypeConverter("System.Web.Configuration.VersionConverter")]
		[ConfigurationProperty("targetFramework", DefaultValue = "4.0")]
		public Version TargetFramework
		{
			get
			{
				return (Version)base[HttpRuntimeSection.targetFrameworkProp];
			}
			set
			{
				base[HttpRuntimeSection.targetFrameworkProp] = value;
			}
		}

		// Token: 0x170013A7 RID: 5031
		// (get) Token: 0x06003E59 RID: 15961 RVA: 0x000A3358 File Offset: 0x000A1558
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return HttpRuntimeSection.properties;
			}
		}

		/// <summary>Gets or sets a value that indicates whether <see cref="M:System.Web.HttpApplication.RegisterModule(System.Type)" /> method calls are allowed. The default is true.</summary>
		/// <returns>true if <see cref="M:System.Web.HttpApplication.RegisterModule(System.Type)" /> method calls are allowed; otherwise, false.</returns>
		// Token: 0x170013A8 RID: 5032
		// (get) Token: 0x06003E5A RID: 15962 RVA: 0x000A335F File Offset: 0x000A155F
		// (set) Token: 0x06003E5B RID: 15963 RVA: 0x000A3371 File Offset: 0x000A1571
		[ConfigurationProperty("allowDynamicModuleRegistration", DefaultValue = "True")]
		public bool AllowDynamicModuleRegistration
		{
			get
			{
				return (bool)base[HttpRuntimeSection.allowDynamicModuleRegistrationProp];
			}
			set
			{
				base[HttpRuntimeSection.allowDynamicModuleRegistrationProp] = value;
			}
		}

		/// <summary>Gets or sets the mode of the request entity that is asynchronously preloaded.</summary>
		/// <returns>The mode of the request entity.</returns>
		// Token: 0x170013A9 RID: 5033
		// (get) Token: 0x06003E5C RID: 15964 RVA: 0x000A3384 File Offset: 0x000A1584
		// (set) Token: 0x06003E5D RID: 15965 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public AsyncPreloadModeFlags AsyncPreloadMode
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return AsyncPreloadModeFlags.None;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets or sets the amount of time that a regular expression is allowed in order to perform a match.</summary>
		/// <returns>The time that is allowed for regular-expression matching.</returns>
		// Token: 0x170013AA RID: 5034
		// (get) Token: 0x06003E5E RID: 15966 RVA: 0x000A33A0 File Offset: 0x000A15A0
		// (set) Token: 0x06003E5F RID: 15967 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public TimeSpan DefaultRegexMatchTimeout
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(TimeSpan);
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets or sets an enumeration value specifying the file change notifications mode.</summary>
		/// <returns>An <see cref="T:System.Web.Configuration.FcnMode" /> enumeration value.</returns>
		// Token: 0x170013AB RID: 5035
		// (get) Token: 0x06003E60 RID: 15968 RVA: 0x000A33BC File Offset: 0x000A15BC
		// (set) Token: 0x06003E61 RID: 15969 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public FcnMode FcnMode
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return FcnMode.NotSet;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		// Token: 0x06003E62 RID: 15970 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public void set_TargetFramework(string value)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04002213 RID: 8723
		private static ConfigurationProperty apartmentThreadingProp = new ConfigurationProperty("apartmentThreading", typeof(bool), false);

		// Token: 0x04002214 RID: 8724
		private static ConfigurationProperty appRequestQueueLimitProp = new ConfigurationProperty("appRequestQueueLimit", typeof(int), 5000, TypeDescriptor.GetConverter(typeof(int)), new IntegerValidator(1, int.MaxValue), ConfigurationPropertyOptions.None);

		// Token: 0x04002215 RID: 8725
		private static ConfigurationProperty delayNotificationTimeoutProp = new ConfigurationProperty("delayNotificationTimeout", typeof(TimeSpan), TimeSpan.FromSeconds(5.0), PropertyHelper.TimeSpanSecondsConverter, PropertyHelper.DefaultValidator, ConfigurationPropertyOptions.None);

		// Token: 0x04002216 RID: 8726
		private static ConfigurationProperty enableProp = new ConfigurationProperty("enable", typeof(bool), true);

		// Token: 0x04002217 RID: 8727
		private static ConfigurationProperty enableHeaderCheckingProp = new ConfigurationProperty("enableHeaderChecking", typeof(bool), true);

		// Token: 0x04002218 RID: 8728
		private static ConfigurationProperty enableKernelOutputCacheProp = new ConfigurationProperty("enableKernelOutputCache", typeof(bool), true);

		// Token: 0x04002219 RID: 8729
		private static ConfigurationProperty enableVersionHeaderProp = new ConfigurationProperty("enableVersionHeader", typeof(bool), true);

		// Token: 0x0400221A RID: 8730
		private static ConfigurationProperty executionTimeoutProp = new ConfigurationProperty("executionTimeout", typeof(TimeSpan), TimeSpan.FromSeconds(110.0), PropertyHelper.TimeSpanSecondsConverter, PropertyHelper.PositiveTimeSpanValidator, ConfigurationPropertyOptions.None);

		// Token: 0x0400221B RID: 8731
		private static ConfigurationProperty maxRequestLengthProp = new ConfigurationProperty("maxRequestLength", typeof(int), 4096, TypeDescriptor.GetConverter(typeof(int)), PropertyHelper.IntFromZeroToMaxValidator, ConfigurationPropertyOptions.None);

		// Token: 0x0400221C RID: 8732
		private static ConfigurationProperty maxWaitChangeNotificationProp = new ConfigurationProperty("maxWaitChangeNotification", typeof(int), 0, TypeDescriptor.GetConverter(typeof(int)), PropertyHelper.IntFromZeroToMaxValidator, ConfigurationPropertyOptions.None);

		// Token: 0x0400221D RID: 8733
		private static ConfigurationProperty minFreeThreadsProp = new ConfigurationProperty("minFreeThreads", typeof(int), 8, TypeDescriptor.GetConverter(typeof(int)), PropertyHelper.IntFromZeroToMaxValidator, ConfigurationPropertyOptions.None);

		// Token: 0x0400221E RID: 8734
		private static ConfigurationProperty minLocalRequestFreeThreadsProp = new ConfigurationProperty("minLocalRequestFreeThreads", typeof(int), 4, TypeDescriptor.GetConverter(typeof(int)), PropertyHelper.IntFromZeroToMaxValidator, ConfigurationPropertyOptions.None);

		// Token: 0x0400221F RID: 8735
		private static ConfigurationProperty requestLengthDiskThresholdProp = new ConfigurationProperty("requestLengthDiskThreshold", typeof(int), 80, TypeDescriptor.GetConverter(typeof(int)), new IntegerValidator(1, int.MaxValue), ConfigurationPropertyOptions.None);

		// Token: 0x04002220 RID: 8736
		private static ConfigurationProperty requireRootedSaveAsPathProp = new ConfigurationProperty("requireRootedSaveAsPath", typeof(bool), true);

		// Token: 0x04002221 RID: 8737
		private static ConfigurationProperty sendCacheControlHeaderProp = new ConfigurationProperty("sendCacheControlHeader", typeof(bool), true);

		// Token: 0x04002222 RID: 8738
		private static ConfigurationProperty shutdownTimeoutProp = new ConfigurationProperty("shutdownTimeout", typeof(TimeSpan), TimeSpan.FromSeconds(90.0), PropertyHelper.TimeSpanSecondsConverter, PropertyHelper.DefaultValidator, ConfigurationPropertyOptions.None);

		// Token: 0x04002223 RID: 8739
		private static ConfigurationProperty useFullyQualifiedRedirectUrlProp = new ConfigurationProperty("useFullyQualifiedRedirectUrl", typeof(bool), false);

		// Token: 0x04002224 RID: 8740
		private static ConfigurationProperty waitChangeNotificationProp = new ConfigurationProperty("waitChangeNotification", typeof(int), 0, TypeDescriptor.GetConverter(typeof(int)), PropertyHelper.IntFromZeroToMaxValidator, ConfigurationPropertyOptions.None);

		// Token: 0x04002225 RID: 8741
		private static ConfigurationProperty requestPathInvalidCharactersProp = new ConfigurationProperty("requestPathInvalidCharacters", typeof(string), "<,>,*,%,&,:,\\,?");

		// Token: 0x04002226 RID: 8742
		private static ConfigurationProperty requestValidationTypeProp = new ConfigurationProperty("requestValidationType", typeof(string), "System.Web.Util.RequestValidator", TypeDescriptor.GetConverter(typeof(string)), PropertyHelper.NonEmptyStringValidator, ConfigurationPropertyOptions.None);

		// Token: 0x04002227 RID: 8743
		private static ConfigurationProperty requestValidationModeProp = new ConfigurationProperty("requestValidationMode", typeof(Version), new Version(4, 0), PropertyHelper.VersionConverter, PropertyHelper.DefaultValidator, ConfigurationPropertyOptions.None);

		// Token: 0x04002228 RID: 8744
		private static ConfigurationProperty maxQueryStringLengthProp = new ConfigurationProperty("maxQueryStringLength", typeof(int), 2048, TypeDescriptor.GetConverter(typeof(int)), PropertyHelper.IntFromZeroToMaxValidator, ConfigurationPropertyOptions.None);

		// Token: 0x04002229 RID: 8745
		private static ConfigurationProperty maxUrlLengthProp = new ConfigurationProperty("maxUrlLength", typeof(int), 260, TypeDescriptor.GetConverter(typeof(int)), PropertyHelper.IntFromZeroToMaxValidator, ConfigurationPropertyOptions.None);

		// Token: 0x0400222A RID: 8746
		private static ConfigurationProperty encoderTypeProp = new ConfigurationProperty("encoderType", typeof(string), "System.Web.Util.HttpEncoder", TypeDescriptor.GetConverter(typeof(string)), PropertyHelper.NonEmptyStringValidator, ConfigurationPropertyOptions.None);

		// Token: 0x0400222B RID: 8747
		private static ConfigurationProperty relaxedUrlToFileSystemMappingProp = new ConfigurationProperty("relaxedUrlToFileSystemMapping", typeof(bool), false);

		// Token: 0x0400222C RID: 8748
		private static ConfigurationProperty targetFrameworkProp = new ConfigurationProperty("targetFramework", typeof(Version), new Version(4, 0), PropertyHelper.VersionConverter, PropertyHelper.DefaultValidator, ConfigurationPropertyOptions.None);

		// Token: 0x0400222D RID: 8749
		private static ConfigurationProperty allowDynamicModuleRegistrationProp = new ConfigurationProperty("allowDynamicModuleRegistration", typeof(bool), true, ConfigurationPropertyOptions.None);

		// Token: 0x0400222E RID: 8750
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();
	}
}
