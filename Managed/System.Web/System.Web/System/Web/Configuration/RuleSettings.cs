using System;
using System.ComponentModel;
using System.Configuration;

namespace System.Web.Configuration
{
	/// <summary>Configures the ASP.NET event rules. This class cannot be inherited.</summary>
	// Token: 0x020005CF RID: 1487
	public sealed class RuleSettings : ConfigurationElement
	{
		// Token: 0x0600402C RID: 16428 RVA: 0x000A71C4 File Offset: 0x000A53C4
		static RuleSettings()
		{
			RuleSettings.properties.Add(RuleSettings.customProp);
			RuleSettings.properties.Add(RuleSettings.eventNameProp);
			RuleSettings.properties.Add(RuleSettings.maxLimitProp);
			RuleSettings.properties.Add(RuleSettings.minInstancesProp);
			RuleSettings.properties.Add(RuleSettings.minIntervalProp);
			RuleSettings.properties.Add(RuleSettings.nameProp);
			RuleSettings.properties.Add(RuleSettings.profileProp);
			RuleSettings.properties.Add(RuleSettings.providerProp);
		}

		// Token: 0x0600402D RID: 16429 RVA: 0x0009CC6D File Offset: 0x0009AE6D
		internal RuleSettings()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Configuration.BufferModeSettings" /> class where all values are specified.</summary>
		/// <param name="name">The name of the <see cref="T:System.Web.Configuration.RuleSettings" /> object to create.</param>
		/// <param name="eventName">The name of the <see cref="T:System.Web.Configuration.EventMappingSettings" /> object this rule applies to.</param>
		/// <param name="provider">The name of the <see cref="T:System.Configuration.ProviderSettings" /> object this rule applies to.</param>
		/// <param name="profile">The name of the <see cref="T:System.Web.Configuration.ProfileSettings" /> object this rule applies to.</param>
		/// <param name="minInstances">The minimum number of occurrences of an event of the same type before the event is fired to the provider. </param>
		/// <param name="maxLimit">The maximum number of times events of the same type are fired.</param>
		/// <param name="minInterval">The minimum time interval between two events of the same type.</param>
		/// <param name="custom">The fully qualified type of a custom class that implements <see cref="T:System.Web.Management.IWebEventCustomEvaluator" />.</param>
		// Token: 0x0600402E RID: 16430 RVA: 0x000A739C File Offset: 0x000A559C
		public RuleSettings(string name, string eventName, string provider, string profile, int minInstances, int maxLimit, TimeSpan minInterval, string custom)
		{
			this.Name = name;
			this.EventName = eventName;
			this.Provider = provider;
			this.Profile = profile;
			this.MinInstances = minInstances;
			this.MaxLimit = maxLimit;
			this.MinInterval = minInterval;
			this.Custom = custom;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Configuration.RuleSettings" /> class where all values except those of the <see cref="P:System.Web.Configuration.RuleSettings.Custom" /> class are specified.</summary>
		/// <param name="name">The name of the <see cref="T:System.Web.Configuration.RuleSettings" /> object to create.</param>
		/// <param name="eventName">The name of the <see cref="T:System.Web.Configuration.EventMappingSettings" /> object this rule applies to.</param>
		/// <param name="provider">The name of the <see cref="T:System.Configuration.ProviderSettings" /> object this rule applies to.</param>
		/// <param name="profile">The name of the <see cref="T:System.Web.Configuration.ProfileSettings" /> object this rule applies to.</param>
		/// <param name="minInstances">The minimum number of occurrences of the same type of event that can occur before the event is raised to the provider. </param>
		/// <param name="maxLimit">The maximum number of times events of the same type can be raised.</param>
		/// <param name="minInterval">The minimum time interval between two events of the same type.</param>
		// Token: 0x0600402F RID: 16431 RVA: 0x000A73EC File Offset: 0x000A55EC
		public RuleSettings(string name, string eventName, string provider, string profile, int minInstances, int maxLimit, TimeSpan minInterval)
		{
			this.Name = name;
			this.EventName = eventName;
			this.Provider = provider;
			this.Profile = profile;
			this.MinInstances = minInstances;
			this.MaxLimit = maxLimit;
			this.MinInterval = minInterval;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Configuration.RuleSettings" /> class using default settings; however, the name, event name, and provider are specified.</summary>
		/// <param name="name">The name of the <see cref="T:System.Web.Configuration.RuleSettings" /> object to create.</param>
		/// <param name="eventName">The name of the <see cref="T:System.Web.Configuration.EventMappingSettings" /> object this rule applies to.</param>
		/// <param name="provider">The name of the <see cref="T:System.Configuration.ProviderSettings" /> object this rule applies to.</param>
		// Token: 0x06004030 RID: 16432 RVA: 0x000A7429 File Offset: 0x000A5629
		public RuleSettings(string name, string eventName, string provider)
		{
			this.Name = name;
			this.EventName = eventName;
			this.Provider = provider;
		}

		/// <summary>Gets or sets the fully qualified type of a custom class that implements <see cref="T:System.Web.Management.IWebEventCustomEvaluator" />.</summary>
		/// <returns>The fully qualified type of a custom class that implements <see cref="T:System.Web.Management.IWebEventCustomEvaluator" />.</returns>
		// Token: 0x17001459 RID: 5209
		// (get) Token: 0x06004031 RID: 16433 RVA: 0x000A7446 File Offset: 0x000A5646
		// (set) Token: 0x06004032 RID: 16434 RVA: 0x000A7458 File Offset: 0x000A5658
		[ConfigurationProperty("custom", DefaultValue = "")]
		public string Custom
		{
			get
			{
				return (string)base[RuleSettings.customProp];
			}
			set
			{
				base[RuleSettings.customProp] = value;
			}
		}

		/// <summary>Gets or sets the name of the <see cref="T:System.Web.Configuration.EventMappingSettings" /> object this rule applies to.</summary>
		/// <returns>The name of the <see cref="T:System.Web.Configuration.EventMappingSettings" /> object this rule applies to.</returns>
		// Token: 0x1700145A RID: 5210
		// (get) Token: 0x06004033 RID: 16435 RVA: 0x000A7466 File Offset: 0x000A5666
		// (set) Token: 0x06004034 RID: 16436 RVA: 0x000A7478 File Offset: 0x000A5678
		[ConfigurationProperty("eventName", DefaultValue = "", Options = ConfigurationPropertyOptions.IsRequired)]
		public string EventName
		{
			get
			{
				return (string)base[RuleSettings.eventNameProp];
			}
			set
			{
				base[RuleSettings.eventNameProp] = value;
			}
		}

		/// <summary>Gets or sets the maximum number of times events of the same type are raised.</summary>
		/// <returns>The maximum number of times events of the same type are raised. The default value is <see cref="F:System.Int32.MaxValue" />.</returns>
		// Token: 0x1700145B RID: 5211
		// (get) Token: 0x06004035 RID: 16437 RVA: 0x000A7486 File Offset: 0x000A5686
		// (set) Token: 0x06004036 RID: 16438 RVA: 0x000A7498 File Offset: 0x000A5698
		[IntegerValidator(MinValue = 0, MaxValue = 2147483647)]
		[TypeConverter(typeof(InfiniteIntConverter))]
		[ConfigurationProperty("maxLimit", DefaultValue = "2147483647")]
		public int MaxLimit
		{
			get
			{
				return (int)base[RuleSettings.maxLimitProp];
			}
			set
			{
				base[RuleSettings.maxLimitProp] = value;
			}
		}

		/// <summary>Gets or sets the minimum number of occurrences of the same type of event before the event is raised to the provider.</summary>
		/// <returns>The minimum number of occurrences of the same type of event before the event is raised to the provider. The default value is 1.</returns>
		// Token: 0x1700145C RID: 5212
		// (get) Token: 0x06004037 RID: 16439 RVA: 0x000A74AB File Offset: 0x000A56AB
		// (set) Token: 0x06004038 RID: 16440 RVA: 0x000A74BD File Offset: 0x000A56BD
		[ConfigurationProperty("minInstances", DefaultValue = "1")]
		[IntegerValidator(MinValue = 1, MaxValue = 2147483647)]
		public int MinInstances
		{
			get
			{
				return (int)base[RuleSettings.minInstancesProp];
			}
			set
			{
				base[RuleSettings.minInstancesProp] = value;
			}
		}

		/// <summary>Gets or sets the minimum time interval between two events of the same type.</summary>
		/// <returns>The minimum time interval between two events of the same type. The default value is 0 ticks.</returns>
		// Token: 0x1700145D RID: 5213
		// (get) Token: 0x06004039 RID: 16441 RVA: 0x000A74D0 File Offset: 0x000A56D0
		// (set) Token: 0x0600403A RID: 16442 RVA: 0x000A74E2 File Offset: 0x000A56E2
		[ConfigurationProperty("minInterval", DefaultValue = "00:00:00")]
		[TypeConverter(typeof(InfiniteTimeSpanConverter))]
		public TimeSpan MinInterval
		{
			get
			{
				return (TimeSpan)base[RuleSettings.minIntervalProp];
			}
			set
			{
				base[RuleSettings.minIntervalProp] = value;
			}
		}

		/// <summary>Gets or sets the name of the <see cref="T:System.Web.Configuration.RuleSettings" /> object.</summary>
		/// <returns>The name of the <see cref="T:System.Web.Configuration.RuleSettings" /> object. The default value is an empty string ("").</returns>
		// Token: 0x1700145E RID: 5214
		// (get) Token: 0x0600403B RID: 16443 RVA: 0x000A74F5 File Offset: 0x000A56F5
		// (set) Token: 0x0600403C RID: 16444 RVA: 0x000A7507 File Offset: 0x000A5707
		[ConfigurationProperty("name", DefaultValue = "", Options = ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey)]
		[StringValidator(MinLength = 1)]
		public string Name
		{
			get
			{
				return (string)base[RuleSettings.nameProp];
			}
			set
			{
				base[RuleSettings.nameProp] = value;
			}
		}

		/// <summary>Gets or sets the name of the <see cref="T:System.Web.Configuration.ProfileSettings" /> object this rule applies to.</summary>
		/// <returns>The name of the <see cref="T:System.Web.Configuration.ProfileSettings" /> object this rule applies to.</returns>
		// Token: 0x1700145F RID: 5215
		// (get) Token: 0x0600403D RID: 16445 RVA: 0x000A7515 File Offset: 0x000A5715
		// (set) Token: 0x0600403E RID: 16446 RVA: 0x000A7527 File Offset: 0x000A5727
		[ConfigurationProperty("profile", DefaultValue = "")]
		public string Profile
		{
			get
			{
				return (string)base[RuleSettings.profileProp];
			}
			set
			{
				base[RuleSettings.profileProp] = value;
			}
		}

		/// <summary>Gets or sets the name of the <see cref="T:System.Configuration.ProviderSettings" /> object this rule applies to.</summary>
		/// <returns>The name of the <see cref="T:System.Configuration.ProviderSettings" /> object this rule applies to.</returns>
		// Token: 0x17001460 RID: 5216
		// (get) Token: 0x0600403F RID: 16447 RVA: 0x000A7535 File Offset: 0x000A5735
		// (set) Token: 0x06004040 RID: 16448 RVA: 0x000A7547 File Offset: 0x000A5747
		[ConfigurationProperty("provider", DefaultValue = "")]
		public string Provider
		{
			get
			{
				return (string)base[RuleSettings.providerProp];
			}
			set
			{
				base[RuleSettings.providerProp] = value;
			}
		}

		// Token: 0x17001461 RID: 5217
		// (get) Token: 0x06004041 RID: 16449 RVA: 0x000A7555 File Offset: 0x000A5755
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return RuleSettings.properties;
			}
		}

		// Token: 0x040022E3 RID: 8931
		private static ConfigurationProperty customProp = new ConfigurationProperty("custom", typeof(string), "");

		// Token: 0x040022E4 RID: 8932
		private static ConfigurationProperty eventNameProp = new ConfigurationProperty("eventName", typeof(string), "", ConfigurationPropertyOptions.IsRequired);

		// Token: 0x040022E5 RID: 8933
		private static ConfigurationProperty maxLimitProp = new ConfigurationProperty("maxLimit", typeof(int), int.MaxValue, PropertyHelper.InfiniteIntConverter, PropertyHelper.IntFromZeroToMaxValidator, ConfigurationPropertyOptions.None);

		// Token: 0x040022E6 RID: 8934
		private static ConfigurationProperty minInstancesProp = new ConfigurationProperty("minInstances", typeof(int), 1, TypeDescriptor.GetConverter(typeof(int)), new IntegerValidator(1, int.MaxValue), ConfigurationPropertyOptions.None);

		// Token: 0x040022E7 RID: 8935
		private static ConfigurationProperty minIntervalProp = new ConfigurationProperty("minInterval", typeof(TimeSpan), TimeSpan.FromSeconds(0.0), PropertyHelper.InfiniteTimeSpanConverter, null, ConfigurationPropertyOptions.None);

		// Token: 0x040022E8 RID: 8936
		private static ConfigurationProperty nameProp = new ConfigurationProperty("name", typeof(string), "", TypeDescriptor.GetConverter(typeof(string)), PropertyHelper.NonEmptyStringValidator, ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey);

		// Token: 0x040022E9 RID: 8937
		private static ConfigurationProperty profileProp = new ConfigurationProperty("profile", typeof(string), "");

		// Token: 0x040022EA RID: 8938
		private static ConfigurationProperty providerProp = new ConfigurationProperty("provider", typeof(string), "");

		// Token: 0x040022EB RID: 8939
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();
	}
}
