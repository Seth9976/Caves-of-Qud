using System;
using System.ComponentModel;
using System.Configuration;

namespace System.Web.Configuration
{
	/// <summary>Configures the ASP.NET event profiles. This class cannot be inherited.</summary>
	// Token: 0x020005C8 RID: 1480
	public sealed class ProfileSettings : ConfigurationElement
	{
		// Token: 0x06003FDD RID: 16349 RVA: 0x000A6778 File Offset: 0x000A4978
		static ProfileSettings()
		{
			ProfileSettings.properties.Add(ProfileSettings.customProp);
			ProfileSettings.properties.Add(ProfileSettings.maxLimitProp);
			ProfileSettings.properties.Add(ProfileSettings.minInstancesProp);
			ProfileSettings.properties.Add(ProfileSettings.minIntervalProp);
			ProfileSettings.properties.Add(ProfileSettings.nameProp);
		}

		// Token: 0x06003FDE RID: 16350 RVA: 0x0009CC6D File Offset: 0x0009AE6D
		internal ProfileSettings()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Configuration.ProfileSettings" /> class. using the specified name for the new instance of the class.</summary>
		/// <param name="name">The name of the <see cref="T:System.Web.Configuration.ProfileSettings" /> object to create.</param>
		// Token: 0x06003FDF RID: 16351 RVA: 0x000A68CA File Offset: 0x000A4ACA
		public ProfileSettings(string name)
		{
			this.Name = name;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Configuration.BufferModeSettings" /> class, using the specified settings for the new instance of the class.</summary>
		/// <param name="name">The name of the <see cref="T:System.Web.Configuration.ProfileSettings" /> object to create.</param>
		/// <param name="minInstances">The minimum number of event occurrences before the event is raised to the provider. </param>
		/// <param name="maxLimit">The maximum number of times events of the same type are raised.</param>
		/// <param name="minInterval">A <see cref="T:System.TimeSpan" /> that specifies the minimum interval between two events of the same type.</param>
		/// <param name="custom">The fully qualified type of a custom class that implements <see cref="T:System.Web.Management.IWebEventCustomEvaluator" />.</param>
		// Token: 0x06003FE0 RID: 16352 RVA: 0x000A68D9 File Offset: 0x000A4AD9
		public ProfileSettings(string name, int minInstances, int maxLimit, TimeSpan minInterval, string custom)
		{
			this.Name = name;
			this.MinInstances = minInstances;
			this.MaxLimit = maxLimit;
			this.MinInterval = this.MinInterval;
			this.Custom = custom;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Configuration.ProfileSettings" /> class, using specified settings for the new instance of the class.</summary>
		/// <param name="name">The name of the <see cref="T:System.Web.Configuration.ProfileSettings" /> object to create.</param>
		/// <param name="minInstances">The minimum number of event occurrences before the event is raised to the provider. </param>
		/// <param name="maxLimit">The maximum number of times events of the same type are raised.</param>
		/// <param name="minInterval">A <see cref="T:System.TimeSpan" /> that specifies the minimum length of the interval between the times when two events of the same type are raised.</param>
		// Token: 0x06003FE1 RID: 16353 RVA: 0x000A690A File Offset: 0x000A4B0A
		public ProfileSettings(string name, int minInstances, int maxLimit, TimeSpan minInterval)
		{
			this.Name = name;
			this.MinInstances = minInstances;
			this.MaxLimit = maxLimit;
			this.MinInterval = this.MinInterval;
		}

		/// <summary>Gets or sets the fully qualified type of a custom class that implements the <see cref="T:System.Web.Management.IWebEventCustomEvaluator" /> interface.</summary>
		/// <returns>The fully qualified type of a custom class that implements the <see cref="T:System.Web.Management.IWebEventCustomEvaluator" /> interface. The default is an empty string ("").</returns>
		// Token: 0x1700143D RID: 5181
		// (get) Token: 0x06003FE2 RID: 16354 RVA: 0x000A6933 File Offset: 0x000A4B33
		// (set) Token: 0x06003FE3 RID: 16355 RVA: 0x000A6945 File Offset: 0x000A4B45
		[ConfigurationProperty("custom", DefaultValue = "")]
		public string Custom
		{
			get
			{
				return (string)base[ProfileSettings.customProp];
			}
			set
			{
				base[ProfileSettings.customProp] = value;
			}
		}

		/// <summary>Gets or sets the maximum number of times events of the same type are raised.</summary>
		/// <returns>The maximum number of times events of the same type are raised. The default is <see cref="F:System.Int32.MaxValue" />.</returns>
		// Token: 0x1700143E RID: 5182
		// (get) Token: 0x06003FE4 RID: 16356 RVA: 0x000A6953 File Offset: 0x000A4B53
		// (set) Token: 0x06003FE5 RID: 16357 RVA: 0x000A6965 File Offset: 0x000A4B65
		[TypeConverter(typeof(InfiniteIntConverter))]
		[IntegerValidator(MinValue = 0, MaxValue = 2147483647)]
		[ConfigurationProperty("maxLimit", DefaultValue = 2147483647)]
		public int MaxLimit
		{
			get
			{
				return (int)base[ProfileSettings.maxLimitProp];
			}
			set
			{
				base[ProfileSettings.maxLimitProp] = value;
			}
		}

		/// <summary>Gets or sets the minimum number of event occurrences before the event is raised to the provider.</summary>
		/// <returns>The minimum number of event occurrences before the event is fired to the provider. The default is 1.</returns>
		// Token: 0x1700143F RID: 5183
		// (get) Token: 0x06003FE6 RID: 16358 RVA: 0x000A6978 File Offset: 0x000A4B78
		// (set) Token: 0x06003FE7 RID: 16359 RVA: 0x000A698A File Offset: 0x000A4B8A
		[IntegerValidator(MinValue = 1, MaxValue = 2147483647)]
		[ConfigurationProperty("minInstances", DefaultValue = "1")]
		public int MinInstances
		{
			get
			{
				return (int)base[ProfileSettings.minInstancesProp];
			}
			set
			{
				base[ProfileSettings.minInstancesProp] = value;
			}
		}

		/// <summary>Gets or sets the minimum interval between two events of the same type.</summary>
		/// <returns>A <see cref="T:System.TimeSpan" /> that specifies the minimum interval between two events of the same type. The default is <see cref="F:System.TimeSpan.Zero" />.</returns>
		// Token: 0x17001440 RID: 5184
		// (get) Token: 0x06003FE8 RID: 16360 RVA: 0x000A699D File Offset: 0x000A4B9D
		// (set) Token: 0x06003FE9 RID: 16361 RVA: 0x000A69AF File Offset: 0x000A4BAF
		[TypeConverter(typeof(InfiniteTimeSpanConverter))]
		[ConfigurationProperty("minInterval", DefaultValue = "00:00:00")]
		public TimeSpan MinInterval
		{
			get
			{
				return (TimeSpan)base[ProfileSettings.minIntervalProp];
			}
			set
			{
				base[ProfileSettings.minIntervalProp] = value;
			}
		}

		/// <summary>Gets or sets the name of the <see cref="T:System.Web.Configuration.ProfileSettings" /> object.</summary>
		/// <returns>The name of the <see cref="T:System.Web.Configuration.ProfileSettings" /> object. The default is an empty string("").</returns>
		// Token: 0x17001441 RID: 5185
		// (get) Token: 0x06003FEA RID: 16362 RVA: 0x000A69C2 File Offset: 0x000A4BC2
		// (set) Token: 0x06003FEB RID: 16363 RVA: 0x000A69D4 File Offset: 0x000A4BD4
		[StringValidator(MinLength = 1)]
		[ConfigurationProperty("name", DefaultValue = "", Options = ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey)]
		public string Name
		{
			get
			{
				return (string)base[ProfileSettings.nameProp];
			}
			set
			{
				base[ProfileSettings.nameProp] = value;
			}
		}

		// Token: 0x17001442 RID: 5186
		// (get) Token: 0x06003FEC RID: 16364 RVA: 0x000A69E2 File Offset: 0x000A4BE2
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return ProfileSettings.properties;
			}
		}

		// Token: 0x040022BE RID: 8894
		private static ConfigurationProperty customProp = new ConfigurationProperty("custom", typeof(string), "");

		// Token: 0x040022BF RID: 8895
		private static ConfigurationProperty maxLimitProp = new ConfigurationProperty("maxLimit", typeof(int), int.MaxValue, PropertyHelper.InfiniteIntConverter, PropertyHelper.IntFromZeroToMaxValidator, ConfigurationPropertyOptions.None);

		// Token: 0x040022C0 RID: 8896
		private static ConfigurationProperty minInstancesProp = new ConfigurationProperty("minInstances", typeof(int), 1, TypeDescriptor.GetConverter(typeof(int)), new IntegerValidator(1, int.MaxValue), ConfigurationPropertyOptions.None);

		// Token: 0x040022C1 RID: 8897
		private static ConfigurationProperty minIntervalProp = new ConfigurationProperty("minInterval", typeof(TimeSpan), TimeSpan.FromSeconds(0.0), PropertyHelper.InfiniteTimeSpanConverter, PropertyHelper.DefaultValidator, ConfigurationPropertyOptions.None);

		// Token: 0x040022C2 RID: 8898
		private static ConfigurationProperty nameProp = new ConfigurationProperty("name", typeof(string), "", TypeDescriptor.GetConverter(typeof(string)), PropertyHelper.NonEmptyStringValidator, ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey);

		// Token: 0x040022C3 RID: 8899
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();
	}
}
