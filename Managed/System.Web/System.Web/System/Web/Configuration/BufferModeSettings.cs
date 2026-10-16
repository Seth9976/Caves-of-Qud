using System;
using System.ComponentModel;
using System.Configuration;

namespace System.Web.Configuration
{
	/// <summary>Configures the ASP.NET event-buffering settings for event providers. This class cannot be inherited.</summary>
	// Token: 0x02000582 RID: 1410
	public sealed class BufferModeSettings : ConfigurationElement
	{
		// Token: 0x06003B89 RID: 15241 RVA: 0x0009D58C File Offset: 0x0009B78C
		static BufferModeSettings()
		{
			IntegerValidator integerValidator = new IntegerValidator(1, int.MaxValue);
			BufferModeSettings.maxBufferSizeProp = new ConfigurationProperty("maxBufferSize", typeof(int), int.MaxValue, PropertyHelper.InfiniteIntConverter, integerValidator, ConfigurationPropertyOptions.IsRequired);
			BufferModeSettings.maxBufferThreadsProp = new ConfigurationProperty("maxBufferThreads", typeof(int), 1, PropertyHelper.InfiniteIntConverter, integerValidator, ConfigurationPropertyOptions.None);
			BufferModeSettings.maxFlushSizeProp = new ConfigurationProperty("maxFlushSize", typeof(int), int.MaxValue, PropertyHelper.InfiniteIntConverter, integerValidator, ConfigurationPropertyOptions.IsRequired);
			BufferModeSettings.nameProp = new ConfigurationProperty("name", typeof(string), "", TypeDescriptor.GetConverter(typeof(string)), PropertyHelper.NonEmptyStringValidator, ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey);
			BufferModeSettings.regularFlushIntervalProp = new ConfigurationProperty("regularFlushInterval", typeof(TimeSpan), TimeSpan.FromSeconds(1.0), PropertyHelper.InfiniteTimeSpanConverter, PropertyHelper.PositiveTimeSpanValidator, ConfigurationPropertyOptions.IsRequired);
			BufferModeSettings.urgentFlushIntervalProp = new ConfigurationProperty("urgentFlushInterval", typeof(TimeSpan), TimeSpan.FromSeconds(0.0), PropertyHelper.InfiniteTimeSpanConverter, null, ConfigurationPropertyOptions.IsRequired);
			BufferModeSettings.urgentFlushThresholdProp = new ConfigurationProperty("urgentFlushThreshold", typeof(int), int.MaxValue, PropertyHelper.InfiniteIntConverter, integerValidator, ConfigurationPropertyOptions.IsRequired);
			BufferModeSettings.properties = new ConfigurationPropertyCollection();
			BufferModeSettings.properties.Add(BufferModeSettings.nameProp);
			BufferModeSettings.properties.Add(BufferModeSettings.maxBufferSizeProp);
			BufferModeSettings.properties.Add(BufferModeSettings.maxBufferThreadsProp);
			BufferModeSettings.properties.Add(BufferModeSettings.maxFlushSizeProp);
			BufferModeSettings.properties.Add(BufferModeSettings.regularFlushIntervalProp);
			BufferModeSettings.properties.Add(BufferModeSettings.urgentFlushIntervalProp);
			BufferModeSettings.properties.Add(BufferModeSettings.urgentFlushThresholdProp);
			BufferModeSettings.elementProperty = new ConfigurationElementProperty(new CallbackValidator(typeof(BufferModeSettings), new ValidatorCallback(BufferModeSettings.ValidateElement)));
		}

		// Token: 0x06003B8A RID: 15242 RVA: 0x0009CC6D File Offset: 0x0009AE6D
		internal BufferModeSettings()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Configuration.BufferModeSettings" /> class using specified settings.</summary>
		/// <param name="name">The name of the <see cref="T:System.Web.Configuration.BufferModeSettings" /> object being created.</param>
		/// <param name="maxBufferSize">The maximum number of events buffered at one time. The value must be greater than zero.</param>
		/// <param name="maxFlushSize">The maximum number of events per buffer flush. Must be greater than zero.</param>
		/// <param name="urgentFlushThreshold">The number of events buffered before a buffer flush is triggered. The value must be greater than zero and less than or equal to <paramref name="maxBufferSize" />.</param>
		/// <param name="regularFlushInterval">The standard amount of time between buffer flushes. The value can be made infinite by setting it to <see cref="F:System.Int32.MaxValue" /> ticks.</param>
		/// <param name="urgentFlushInterval">The minimum length of time that can pass between buffer flushes. The value must be less than or equal to <paramref name="regularFlushInterval" />.</param>
		/// <param name="maxBufferThreads">The maximum number of buffer-flushing threads that can be active at one time.</param>
		// Token: 0x06003B8B RID: 15243 RVA: 0x0009D77E File Offset: 0x0009B97E
		public BufferModeSettings(string name, int maxBufferSize, int maxFlushSize, int urgentFlushThreshold, TimeSpan regularFlushInterval, TimeSpan urgentFlushInterval, int maxBufferThreads)
		{
			this.Name = name;
			this.MaxBufferSize = maxBufferSize;
			this.MaxFlushSize = maxFlushSize;
			this.UrgentFlushThreshold = urgentFlushThreshold;
			this.RegularFlushInterval = regularFlushInterval;
			this.UrgentFlushInterval = urgentFlushInterval;
			this.MaxBufferThreads = maxBufferThreads;
		}

		// Token: 0x06003B8C RID: 15244 RVA: 0x00003946 File Offset: 0x00001B46
		[global::System.MonoTODO("Should do some validation here")]
		private static void ValidateElement(object o)
		{
		}

		// Token: 0x17001255 RID: 4693
		// (get) Token: 0x06003B8D RID: 15245 RVA: 0x0009D7BB File Offset: 0x0009B9BB
		protected internal override ConfigurationElementProperty ElementProperty
		{
			get
			{
				return BufferModeSettings.elementProperty;
			}
		}

		/// <summary>Gets or sets the maximum number of events that can be buffered at one time.</summary>
		/// <returns>The maximum number of events that can be buffered at one time.</returns>
		// Token: 0x17001256 RID: 4694
		// (get) Token: 0x06003B8E RID: 15246 RVA: 0x0009D7C2 File Offset: 0x0009B9C2
		// (set) Token: 0x06003B8F RID: 15247 RVA: 0x0009D7D4 File Offset: 0x0009B9D4
		[ConfigurationProperty("maxBufferSize", DefaultValue = "2147483647", Options = ConfigurationPropertyOptions.IsRequired)]
		[TypeConverter(typeof(InfiniteIntConverter))]
		[IntegerValidator(MinValue = 1, MaxValue = 2147483647)]
		public int MaxBufferSize
		{
			get
			{
				return (int)base[BufferModeSettings.maxBufferSizeProp];
			}
			set
			{
				base[BufferModeSettings.maxBufferSizeProp] = value;
			}
		}

		/// <summary>Gets or sets the maximum number of flushing threads that can be active at one time.</summary>
		/// <returns>The maximum number of flushing threads that can be active at one time. The default is 1.</returns>
		// Token: 0x17001257 RID: 4695
		// (get) Token: 0x06003B90 RID: 15248 RVA: 0x0009D7E7 File Offset: 0x0009B9E7
		// (set) Token: 0x06003B91 RID: 15249 RVA: 0x0009D7F9 File Offset: 0x0009B9F9
		[TypeConverter(typeof(InfiniteIntConverter))]
		[IntegerValidator(MinValue = 1, MaxValue = 2147483647)]
		[ConfigurationProperty("maxBufferThreads", DefaultValue = "1")]
		public int MaxBufferThreads
		{
			get
			{
				return (int)base[BufferModeSettings.maxBufferThreadsProp];
			}
			set
			{
				base[BufferModeSettings.maxBufferThreadsProp] = value;
			}
		}

		/// <summary>Gets or sets the maximum number of events per flush.</summary>
		/// <returns>The maximum number of events per flush.</returns>
		// Token: 0x17001258 RID: 4696
		// (get) Token: 0x06003B92 RID: 15250 RVA: 0x0009D80C File Offset: 0x0009BA0C
		// (set) Token: 0x06003B93 RID: 15251 RVA: 0x0009D81E File Offset: 0x0009BA1E
		[TypeConverter(typeof(InfiniteIntConverter))]
		[ConfigurationProperty("maxFlushSize", DefaultValue = "2147483647", Options = ConfigurationPropertyOptions.IsRequired)]
		[IntegerValidator(MinValue = 1, MaxValue = 2147483647)]
		public int MaxFlushSize
		{
			get
			{
				return (int)base[BufferModeSettings.maxFlushSizeProp];
			}
			set
			{
				base[BufferModeSettings.maxFlushSizeProp] = value;
			}
		}

		/// <summary>Gets or sets the name of the <see cref="T:System.Web.Configuration.BufferModeSettings" /> object.</summary>
		/// <returns>The name of the <see cref="T:System.Web.Configuration.BufferModeSettings" /> object. The default value is an empty string.</returns>
		// Token: 0x17001259 RID: 4697
		// (get) Token: 0x06003B94 RID: 15252 RVA: 0x0009D831 File Offset: 0x0009BA31
		// (set) Token: 0x06003B95 RID: 15253 RVA: 0x0009D843 File Offset: 0x0009BA43
		[ConfigurationProperty("name", DefaultValue = "", Options = ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey)]
		[StringValidator(MinLength = 1)]
		public string Name
		{
			get
			{
				return (string)base[BufferModeSettings.nameProp];
			}
			set
			{
				base[BufferModeSettings.nameProp] = value;
			}
		}

		/// <summary>Gets or sets the amount of time between buffer flushes.</summary>
		/// <returns>The regular amount of time between buffer flushes.</returns>
		// Token: 0x1700125A RID: 4698
		// (get) Token: 0x06003B96 RID: 15254 RVA: 0x0009D851 File Offset: 0x0009BA51
		// (set) Token: 0x06003B97 RID: 15255 RVA: 0x0009D863 File Offset: 0x0009BA63
		[TimeSpanValidator(MinValueString = "00:00:00", MaxValueString = "10675199.02:48:05.4775807")]
		[ConfigurationProperty("regularFlushInterval", DefaultValue = "00:00:01", Options = ConfigurationPropertyOptions.IsRequired)]
		[TypeConverter(typeof(InfiniteTimeSpanConverter))]
		public TimeSpan RegularFlushInterval
		{
			get
			{
				return (TimeSpan)base[BufferModeSettings.regularFlushIntervalProp];
			}
			set
			{
				base[BufferModeSettings.regularFlushIntervalProp] = value;
			}
		}

		/// <summary>Gets or sets the minimum amount of time that can pass between buffer flushes. </summary>
		/// <returns>The minimum amount of time that can pass between buffer flushes.</returns>
		// Token: 0x1700125B RID: 4699
		// (get) Token: 0x06003B98 RID: 15256 RVA: 0x0009D876 File Offset: 0x0009BA76
		// (set) Token: 0x06003B99 RID: 15257 RVA: 0x0009D888 File Offset: 0x0009BA88
		[TypeConverter(typeof(InfiniteTimeSpanConverter))]
		[ConfigurationProperty("urgentFlushInterval", DefaultValue = "00:00:00", Options = ConfigurationPropertyOptions.IsRequired)]
		public TimeSpan UrgentFlushInterval
		{
			get
			{
				return (TimeSpan)base[BufferModeSettings.urgentFlushIntervalProp];
			}
			set
			{
				base[BufferModeSettings.urgentFlushIntervalProp] = value;
			}
		}

		/// <summary>Gets or sets the number of events that can be buffered before a flush is triggered.</summary>
		/// <returns>The number of events that can be buffered before a flush is triggered.</returns>
		// Token: 0x1700125C RID: 4700
		// (get) Token: 0x06003B9A RID: 15258 RVA: 0x0009D89B File Offset: 0x0009BA9B
		// (set) Token: 0x06003B9B RID: 15259 RVA: 0x0009D8AD File Offset: 0x0009BAAD
		[TypeConverter(typeof(InfiniteIntConverter))]
		[IntegerValidator(MinValue = 1, MaxValue = 2147483647)]
		[ConfigurationProperty("urgentFlushThreshold", DefaultValue = "2147483647", Options = ConfigurationPropertyOptions.IsRequired)]
		public int UrgentFlushThreshold
		{
			get
			{
				return (int)base[BufferModeSettings.urgentFlushThresholdProp];
			}
			set
			{
				base[BufferModeSettings.urgentFlushThresholdProp] = value;
			}
		}

		// Token: 0x1700125D RID: 4701
		// (get) Token: 0x06003B9C RID: 15260 RVA: 0x0009D8C0 File Offset: 0x0009BAC0
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return BufferModeSettings.properties;
			}
		}

		// Token: 0x0400208F RID: 8335
		private static ConfigurationProperty maxBufferSizeProp;

		// Token: 0x04002090 RID: 8336
		private static ConfigurationProperty maxBufferThreadsProp;

		// Token: 0x04002091 RID: 8337
		private static ConfigurationProperty maxFlushSizeProp;

		// Token: 0x04002092 RID: 8338
		private static ConfigurationProperty nameProp;

		// Token: 0x04002093 RID: 8339
		private static ConfigurationProperty regularFlushIntervalProp;

		// Token: 0x04002094 RID: 8340
		private static ConfigurationProperty urgentFlushIntervalProp;

		// Token: 0x04002095 RID: 8341
		private static ConfigurationProperty urgentFlushThresholdProp;

		// Token: 0x04002096 RID: 8342
		private static ConfigurationPropertyCollection properties;

		// Token: 0x04002097 RID: 8343
		private static ConfigurationElementProperty elementProperty;
	}
}
