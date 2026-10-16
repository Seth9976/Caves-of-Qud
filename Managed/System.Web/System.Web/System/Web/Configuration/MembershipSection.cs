using System;
using System.ComponentModel;
using System.Configuration;

namespace System.Web.Configuration
{
	/// <summary>Defines configuration settings to support the infrastructure for configuring and managing membership details. This class cannot be inherited.</summary>
	// Token: 0x020005B6 RID: 1462
	public sealed class MembershipSection : ConfigurationSection
	{
		// Token: 0x06003EA6 RID: 16038 RVA: 0x000A408C File Offset: 0x000A228C
		static MembershipSection()
		{
			MembershipSection.properties.Add(MembershipSection.defaultProviderProp);
			MembershipSection.properties.Add(MembershipSection.hashAlgorithmTypeProp);
			MembershipSection.properties.Add(MembershipSection.providersProp);
			MembershipSection.properties.Add(MembershipSection.userIsOnlineTimeWindowProp);
		}

		/// <summary>Gets or sets the name of the default provider that is used to manage roles. </summary>
		/// <returns>The name of a provider in <see cref="P:System.Web.Configuration.MembershipSection.Providers" />. The default is AspNetSqlRoleProvider.</returns>
		// Token: 0x170013BD RID: 5053
		// (get) Token: 0x06003EA7 RID: 16039 RVA: 0x000A4195 File Offset: 0x000A2395
		// (set) Token: 0x06003EA8 RID: 16040 RVA: 0x000A41A7 File Offset: 0x000A23A7
		[StringValidator(MinLength = 1)]
		[ConfigurationProperty("defaultProvider", DefaultValue = "AspNetSqlMembershipProvider")]
		public string DefaultProvider
		{
			get
			{
				return (string)base[MembershipSection.defaultProviderProp];
			}
			set
			{
				base[MembershipSection.defaultProviderProp] = value;
			}
		}

		/// <summary>Gets or sets the type of encryption that is used for sensitive membership information.</summary>
		/// <returns>The type of encryption used to encrypt sensitive membership information.</returns>
		// Token: 0x170013BE RID: 5054
		// (get) Token: 0x06003EA9 RID: 16041 RVA: 0x000A41B5 File Offset: 0x000A23B5
		// (set) Token: 0x06003EAA RID: 16042 RVA: 0x000A41C7 File Offset: 0x000A23C7
		[ConfigurationProperty("hashAlgorithmType", DefaultValue = "")]
		public string HashAlgorithmType
		{
			get
			{
				return (string)base[MembershipSection.hashAlgorithmTypeProp];
			}
			set
			{
				base[MembershipSection.hashAlgorithmTypeProp] = value;
			}
		}

		/// <summary>Gets a <see cref="T:System.Configuration.ProviderSettingsCollection" /> object of <see cref="T:System.Configuration.ProviderSettings" /> objects.</summary>
		/// <returns>A <see cref="T:System.Configuration.ProviderSettingsCollection" /> that contains the provider's settings, defined within the providers subsection of the membership section of the configuration file.</returns>
		// Token: 0x170013BF RID: 5055
		// (get) Token: 0x06003EAB RID: 16043 RVA: 0x000A41D5 File Offset: 0x000A23D5
		[ConfigurationProperty("providers")]
		public ProviderSettingsCollection Providers
		{
			get
			{
				return (ProviderSettingsCollection)base[MembershipSection.providersProp];
			}
		}

		/// <summary>Gets or sets the length of time, in minutes, before a user is no longer considered to be online.</summary>
		/// <returns>A length of time in minutes.</returns>
		// Token: 0x170013C0 RID: 5056
		// (get) Token: 0x06003EAC RID: 16044 RVA: 0x000A41E7 File Offset: 0x000A23E7
		// (set) Token: 0x06003EAD RID: 16045 RVA: 0x000A41F9 File Offset: 0x000A23F9
		[TypeConverter(typeof(TimeSpanMinutesConverter))]
		[TimeSpanValidator(MinValueString = "00:01:00")]
		[ConfigurationProperty("userIsOnlineTimeWindow", DefaultValue = "00:15:00")]
		public TimeSpan UserIsOnlineTimeWindow
		{
			get
			{
				return (TimeSpan)base[MembershipSection.userIsOnlineTimeWindowProp];
			}
			set
			{
				base[MembershipSection.userIsOnlineTimeWindowProp] = value;
			}
		}

		// Token: 0x170013C1 RID: 5057
		// (get) Token: 0x06003EAE RID: 16046 RVA: 0x000A420C File Offset: 0x000A240C
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return MembershipSection.properties;
			}
		}

		// Token: 0x0400224C RID: 8780
		private static ConfigurationProperty defaultProviderProp = new ConfigurationProperty("defaultProvider", typeof(string), "AspNetSqlMembershipProvider", TypeDescriptor.GetConverter(typeof(string)), PropertyHelper.NonEmptyStringValidator, ConfigurationPropertyOptions.None);

		// Token: 0x0400224D RID: 8781
		private static ConfigurationProperty hashAlgorithmTypeProp = new ConfigurationProperty("hashAlgorithmType", typeof(string), "");

		// Token: 0x0400224E RID: 8782
		private static ConfigurationProperty providersProp = new ConfigurationProperty("providers", typeof(ProviderSettingsCollection), null, null, PropertyHelper.DefaultValidator, ConfigurationPropertyOptions.None);

		// Token: 0x0400224F RID: 8783
		private static ConfigurationProperty userIsOnlineTimeWindowProp = new ConfigurationProperty("userIsOnlineTimeWindow", typeof(TimeSpan), TimeSpan.FromMinutes(15.0), PropertyHelper.TimeSpanMinutesConverter, new TimeSpanValidator(new TimeSpan(0, 1, 0), TimeSpan.MaxValue), ConfigurationPropertyOptions.None);

		// Token: 0x04002250 RID: 8784
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();
	}
}
