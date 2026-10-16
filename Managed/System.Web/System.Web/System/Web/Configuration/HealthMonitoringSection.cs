using System;
using System.ComponentModel;
using System.Configuration;

namespace System.Web.Configuration
{
	/// <summary>Configures ASP.NET profiles that determine how health-monitoring events are sent to event providers. This class cannot be inherited.</summary>
	// Token: 0x0200059F RID: 1439
	public sealed class HealthMonitoringSection : ConfigurationSection
	{
		// Token: 0x06003D1A RID: 15642 RVA: 0x000A02B4 File Offset: 0x0009E4B4
		static HealthMonitoringSection()
		{
			HealthMonitoringSection.properties.Add(HealthMonitoringSection.bufferModesProp);
			HealthMonitoringSection.properties.Add(HealthMonitoringSection.enabledProp);
			HealthMonitoringSection.properties.Add(HealthMonitoringSection.eventMappingsProp);
			HealthMonitoringSection.properties.Add(HealthMonitoringSection.heartbeatIntervalProp);
			HealthMonitoringSection.properties.Add(HealthMonitoringSection.profilesProp);
			HealthMonitoringSection.properties.Add(HealthMonitoringSection.providersProp);
			HealthMonitoringSection.properties.Add(HealthMonitoringSection.rulesProp);
		}

		/// <summary>Gets a collection of objects that specify the settings for the buffer modes.</summary>
		/// <returns>A <see cref="T:System.Web.Configuration.BufferModesCollection" /> collection of <see cref="T:System.Web.Configuration.BufferModeSettings" /> objects.</returns>
		// Token: 0x170012EF RID: 4847
		// (get) Token: 0x06003D1B RID: 15643 RVA: 0x000A0441 File Offset: 0x0009E641
		[ConfigurationProperty("bufferModes")]
		public BufferModesCollection BufferModes
		{
			get
			{
				return (BufferModesCollection)base[HealthMonitoringSection.bufferModesProp];
			}
		}

		/// <summary>Gets or sets a value indicating whether health monitoring is enabled.</summary>
		/// <returns>true if health monitoring is enabled; otherwise, false. The default is true.</returns>
		// Token: 0x170012F0 RID: 4848
		// (get) Token: 0x06003D1C RID: 15644 RVA: 0x000A0453 File Offset: 0x0009E653
		// (set) Token: 0x06003D1D RID: 15645 RVA: 0x000A0465 File Offset: 0x0009E665
		[ConfigurationProperty("enabled", DefaultValue = "True")]
		public bool Enabled
		{
			get
			{
				return (bool)base[HealthMonitoringSection.enabledProp];
			}
			set
			{
				base[HealthMonitoringSection.enabledProp] = value;
			}
		}

		/// <summary>Gets a <see cref="T:System.Web.Configuration.EventMappingSettingsCollection" /> collection of <see cref="T:System.Web.Configuration.EventMappingSettings" /> objects.</summary>
		/// <returns>A <see cref="T:System.Web.Configuration.EventMappingSettingsCollection" /> collection of <see cref="T:System.Web.Configuration.EventMappingSettings" /> objects. The default is an empty <see cref="T:System.Web.Configuration.EventMappingSettingsCollection" /> collection.</returns>
		// Token: 0x170012F1 RID: 4849
		// (get) Token: 0x06003D1E RID: 15646 RVA: 0x000A0478 File Offset: 0x0009E678
		[ConfigurationProperty("eventMappings")]
		public EventMappingSettingsCollection EventMappings
		{
			get
			{
				return (EventMappingSettingsCollection)base[HealthMonitoringSection.eventMappingsProp];
			}
		}

		/// <summary>Gets or sets the interval used by the application domain when it raises the <see cref="T:System.Web.Management.WebHeartbeatEvent" /> event.</summary>
		/// <returns>The interval used by the application domain when it raises the <see cref="T:System.Web.Management.WebHeartbeatEvent" /> event.</returns>
		// Token: 0x170012F2 RID: 4850
		// (get) Token: 0x06003D1F RID: 15647 RVA: 0x000A048A File Offset: 0x0009E68A
		// (set) Token: 0x06003D20 RID: 15648 RVA: 0x000A049C File Offset: 0x0009E69C
		[TimeSpanValidator(MinValueString = "00:00:00", MaxValueString = "24.20:31:23")]
		[TypeConverter(typeof(TimeSpanSecondsConverter))]
		[ConfigurationProperty("heartbeatInterval", DefaultValue = "00:00:00")]
		public TimeSpan HeartbeatInterval
		{
			get
			{
				return (TimeSpan)base[HealthMonitoringSection.heartbeatIntervalProp];
			}
			set
			{
				base[HealthMonitoringSection.heartbeatIntervalProp] = value;
			}
		}

		/// <summary>Gets a <see cref="T:System.Web.Configuration.ProfileSettingsCollection" /> collection of <see cref="T:System.Web.Configuration.ProfileSettings" /> objects.</summary>
		/// <returns>A <see cref="T:System.Web.Configuration.ProfileSettingsCollection" /> collection of <see cref="T:System.Web.Configuration.ProfileSettings" /> objects. The default is an empty <see cref="T:System.Web.Configuration.ProfileSettingsCollection" /> collection.</returns>
		// Token: 0x170012F3 RID: 4851
		// (get) Token: 0x06003D21 RID: 15649 RVA: 0x000A04AF File Offset: 0x0009E6AF
		[ConfigurationProperty("profiles")]
		public ProfileSettingsCollection Profiles
		{
			get
			{
				return (ProfileSettingsCollection)base[HealthMonitoringSection.profilesProp];
			}
		}

		/// <summary>Gets a <see cref="T:System.Configuration.ProviderSettingsCollection" /> collection of <see cref="T:System.Configuration.ProviderSettings" /> objects.</summary>
		/// <returns>A <see cref="T:System.Configuration.ProviderSettingsCollection" /> collection. The default is an empty <see cref="T:System.Configuration.ProviderSettingsCollection" /> collection.</returns>
		// Token: 0x170012F4 RID: 4852
		// (get) Token: 0x06003D22 RID: 15650 RVA: 0x000A04C1 File Offset: 0x0009E6C1
		[ConfigurationProperty("providers")]
		public ProviderSettingsCollection Providers
		{
			get
			{
				return (ProviderSettingsCollection)base[HealthMonitoringSection.providersProp];
			}
		}

		/// <summary>Gets a <see cref="T:System.Web.Configuration.RuleSettingsCollection" /> collection of <see cref="T:System.Web.Configuration.RuleSettings" /> objects.</summary>
		/// <returns>A <see cref="T:System.Web.Configuration.RuleSettingsCollection" /> collection. The default is an empty <see cref="T:System.Web.Configuration.RuleSettingsCollection" /> collection</returns>
		// Token: 0x170012F5 RID: 4853
		// (get) Token: 0x06003D23 RID: 15651 RVA: 0x000A04D3 File Offset: 0x0009E6D3
		[ConfigurationProperty("rules")]
		public RuleSettingsCollection Rules
		{
			get
			{
				return (RuleSettingsCollection)base[HealthMonitoringSection.rulesProp];
			}
		}

		// Token: 0x170012F6 RID: 4854
		// (get) Token: 0x06003D24 RID: 15652 RVA: 0x000A04E5 File Offset: 0x0009E6E5
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return HealthMonitoringSection.properties;
			}
		}

		// Token: 0x04002108 RID: 8456
		private static ConfigurationProperty bufferModesProp = new ConfigurationProperty("bufferModes", typeof(BufferModesCollection), null, null, PropertyHelper.DefaultValidator, ConfigurationPropertyOptions.None);

		// Token: 0x04002109 RID: 8457
		private static ConfigurationProperty enabledProp = new ConfigurationProperty("enabled", typeof(bool), true);

		// Token: 0x0400210A RID: 8458
		private static ConfigurationProperty eventMappingsProp = new ConfigurationProperty("eventMappings", typeof(EventMappingSettingsCollection), null, null, PropertyHelper.DefaultValidator, ConfigurationPropertyOptions.None);

		// Token: 0x0400210B RID: 8459
		private static ConfigurationProperty heartbeatIntervalProp = new ConfigurationProperty("heartbeatInterval", typeof(TimeSpan), TimeSpan.FromSeconds(0.0), PropertyHelper.TimeSpanSecondsConverter, new TimeSpanValidator(TimeSpan.Zero, new TimeSpan(24, 30, 31, 23)), ConfigurationPropertyOptions.None);

		// Token: 0x0400210C RID: 8460
		private static ConfigurationProperty profilesProp = new ConfigurationProperty("profiles", typeof(ProfileSettingsCollection), null, null, PropertyHelper.DefaultValidator, ConfigurationPropertyOptions.None);

		// Token: 0x0400210D RID: 8461
		private static ConfigurationProperty providersProp = new ConfigurationProperty("providers", typeof(ProviderSettingsCollection), null, null, PropertyHelper.DefaultValidator, ConfigurationPropertyOptions.None);

		// Token: 0x0400210E RID: 8462
		private static ConfigurationProperty rulesProp = new ConfigurationProperty("rules", typeof(RuleSettingsCollection), null, null, PropertyHelper.DefaultValidator, ConfigurationPropertyOptions.None);

		// Token: 0x0400210F RID: 8463
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();
	}
}
