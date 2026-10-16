using System;
using System.ComponentModel;
using System.Configuration;

namespace System.Web.Configuration
{
	/// <summary>The <see cref="T:System.Web.Configuration.ProfilePropertySettings" /> class provides a way to programmatically access and modify the profiles section of a configuration file. This class cannot be inherited.</summary>
	// Token: 0x020005C5 RID: 1477
	public sealed class ProfilePropertySettings : ConfigurationElement
	{
		// Token: 0x06003FA6 RID: 16294 RVA: 0x000A6180 File Offset: 0x000A4380
		static ProfilePropertySettings()
		{
			ProfilePropertySettings.properties.Add(ProfilePropertySettings.allowAnonymousProp);
			ProfilePropertySettings.properties.Add(ProfilePropertySettings.customProviderDataProp);
			ProfilePropertySettings.properties.Add(ProfilePropertySettings.defaultValueProp);
			ProfilePropertySettings.properties.Add(ProfilePropertySettings.nameProp);
			ProfilePropertySettings.properties.Add(ProfilePropertySettings.providerProp);
			ProfilePropertySettings.properties.Add(ProfilePropertySettings.readOnlyProp);
			ProfilePropertySettings.properties.Add(ProfilePropertySettings.serializeAsProp);
			ProfilePropertySettings.properties.Add(ProfilePropertySettings.typeProp);
		}

		// Token: 0x06003FA7 RID: 16295 RVA: 0x0009CC6D File Offset: 0x0009AE6D
		internal ProfilePropertySettings()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Configuration.ProfilePropertySettings" /> class with the specified name.</summary>
		/// <param name="name">A unique name for the <see cref="T:System.Web.Configuration.ProfilePropertySettings" /> object.</param>
		// Token: 0x06003FA8 RID: 16296 RVA: 0x000A6328 File Offset: 0x000A4528
		public ProfilePropertySettings(string name)
		{
			this.Name = name;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Configuration.ProfilePropertySettings" /> class with the specified name and settings.</summary>
		/// <param name="name">A unique name for the <see cref="T:System.Web.Configuration.ProfilePropertySettings" /> object.</param>
		/// <param name="readOnly">true to indicate that the associated property in the dynamically generated ProfileCommon class should be read-only; otherwise, false.</param>
		/// <param name="serializeAs">One of the <see cref="T:System.Web.Configuration.SerializationMode" /> values.</param>
		/// <param name="providerName">The name of a provider from the <see cref="P:System.Web.Configuration.ProfileSection.Providers" /> property, or an empty string ("").</param>
		/// <param name="defaultValue">A string containing the default value used for the named property in the generated page Profile class.</param>
		/// <param name="profileType">A valid type reference or an empty string.</param>
		/// <param name="allowAnonymous">true to indicate associated property in the dynamically generated ProfileCommon class should support anonymous users; otherwise, false, to indicate that anonymous users cannot change the named property.</param>
		/// <param name="customProviderData">A string containing provider-specific information used by the provider associated with the property.</param>
		// Token: 0x06003FA9 RID: 16297 RVA: 0x000A6338 File Offset: 0x000A4538
		public ProfilePropertySettings(string name, bool readOnly, SerializationMode serializeAs, string providerName, string defaultValue, string profileType, bool allowAnonymous, string customProviderData)
		{
			this.Name = name;
			this.ReadOnly = readOnly;
			this.SerializeAs = serializeAs;
			this.Provider = providerName;
			this.DefaultValue = defaultValue;
			this.Type = profileType;
			this.AllowAnonymous = allowAnonymous;
			this.CustomProviderData = customProviderData;
		}

		/// <summary>Gets or sets a value indicating whether the associated property in the dynamically generated ProfileCommon class can be set by anonymous users.</summary>
		/// <returns>true if the associated property in the ProfileCommon class can be set by anonymous users; otherwise, false, indicating that anonymous users cannot change the property value. The default is false.</returns>
		// Token: 0x17001427 RID: 5159
		// (get) Token: 0x06003FAA RID: 16298 RVA: 0x000A6388 File Offset: 0x000A4588
		// (set) Token: 0x06003FAB RID: 16299 RVA: 0x000A639A File Offset: 0x000A459A
		[ConfigurationProperty("allowAnonymous", DefaultValue = false)]
		public bool AllowAnonymous
		{
			get
			{
				return (bool)base[ProfilePropertySettings.allowAnonymousProp];
			}
			set
			{
				base[ProfilePropertySettings.allowAnonymousProp] = value;
			}
		}

		/// <summary>Gets or sets a string of custom data for the profile property provider.</summary>
		/// <returns>A string of custom data for the profile property provider. The default is null.</returns>
		// Token: 0x17001428 RID: 5160
		// (get) Token: 0x06003FAC RID: 16300 RVA: 0x000A63AD File Offset: 0x000A45AD
		// (set) Token: 0x06003FAD RID: 16301 RVA: 0x000A63BF File Offset: 0x000A45BF
		[ConfigurationProperty("customProviderData", DefaultValue = "")]
		public string CustomProviderData
		{
			get
			{
				return (string)base[ProfilePropertySettings.customProviderDataProp];
			}
			set
			{
				base[ProfilePropertySettings.customProviderDataProp] = value;
			}
		}

		/// <summary>Gets or sets the default value used for the associated property in the dynamically generated ProfileCommon class. </summary>
		/// <returns>A string containing the default value used for the associated property in the dynamically generated ProfileCommon class. The default is an empty string ("").</returns>
		// Token: 0x17001429 RID: 5161
		// (get) Token: 0x06003FAE RID: 16302 RVA: 0x000A63CD File Offset: 0x000A45CD
		// (set) Token: 0x06003FAF RID: 16303 RVA: 0x000A63DF File Offset: 0x000A45DF
		[ConfigurationProperty("defaultValue", DefaultValue = "")]
		public string DefaultValue
		{
			get
			{
				return (string)base[ProfilePropertySettings.defaultValueProp];
			}
			set
			{
				base[ProfilePropertySettings.defaultValueProp] = value;
			}
		}

		/// <summary>Gets or sets the name of the <see cref="T:System.Web.Configuration.ProfilePropertySettings" /> object and the associated property in the dynamically generated ProfileCommon class.</summary>
		/// <returns>A string containing the name of the <see cref="T:System.Web.Configuration.ProfilePropertySettings" /> object. The default is null.</returns>
		// Token: 0x1700142A RID: 5162
		// (get) Token: 0x06003FB0 RID: 16304 RVA: 0x000A63ED File Offset: 0x000A45ED
		// (set) Token: 0x06003FB1 RID: 16305 RVA: 0x000A63FF File Offset: 0x000A45FF
		[ConfigurationProperty("name", Options = ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey)]
		public string Name
		{
			get
			{
				return (string)base[ProfilePropertySettings.nameProp];
			}
			set
			{
				base[ProfilePropertySettings.nameProp] = value;
			}
		}

		/// <summary>Gets or sets the name of a provider to use when serializing the named property.</summary>
		/// <returns>The name of a provider from the <see cref="P:System.Web.Configuration.ProfileSection.Providers" /> property, or an empty string (""). The default is an empty string.</returns>
		// Token: 0x1700142B RID: 5163
		// (get) Token: 0x06003FB2 RID: 16306 RVA: 0x000A640D File Offset: 0x000A460D
		// (set) Token: 0x06003FB3 RID: 16307 RVA: 0x000A641F File Offset: 0x000A461F
		[ConfigurationProperty("provider", DefaultValue = "")]
		public string Provider
		{
			get
			{
				return (string)base[ProfilePropertySettings.providerProp];
			}
			set
			{
				base[ProfilePropertySettings.providerProp] = value;
			}
		}

		/// <summary>Gets or sets a value that determines whether the associated property in the dynamically generated ProfileCommon class is read-only.</summary>
		/// <returns>true if the associated property in the ProfileCommon class is read-only; otherwise, false. The default is false.</returns>
		// Token: 0x1700142C RID: 5164
		// (get) Token: 0x06003FB4 RID: 16308 RVA: 0x000A642D File Offset: 0x000A462D
		// (set) Token: 0x06003FB5 RID: 16309 RVA: 0x000A643F File Offset: 0x000A463F
		[ConfigurationProperty("readOnly", DefaultValue = false)]
		public bool ReadOnly
		{
			get
			{
				return (bool)base[ProfilePropertySettings.readOnlyProp];
			}
			set
			{
				base[ProfilePropertySettings.readOnlyProp] = value;
			}
		}

		/// <summary>Gets or sets the serialization method used for the associated property in the dynamically generated ProfileCommon class.</summary>
		/// <returns>One of the <see cref="T:System.Web.Configuration.SerializationMode" /> values. The default is <see cref="F:System.Web.Configuration.SerializationMode.ProviderSpecific" />.</returns>
		// Token: 0x1700142D RID: 5165
		// (get) Token: 0x06003FB6 RID: 16310 RVA: 0x000A6452 File Offset: 0x000A4652
		// (set) Token: 0x06003FB7 RID: 16311 RVA: 0x000A6464 File Offset: 0x000A4664
		[ConfigurationProperty("serializeAs", DefaultValue = "ProviderSpecific")]
		public SerializationMode SerializeAs
		{
			get
			{
				return (SerializationMode)base[ProfilePropertySettings.serializeAsProp];
			}
			set
			{
				base[ProfilePropertySettings.serializeAsProp] = value;
			}
		}

		/// <summary>Gets or sets the name of the type of the associated property in the dynamically generated ProfileCommon class.</summary>
		/// <returns>A valid, fully qualified type reference, or an empty string (""). The default is an empty string.</returns>
		// Token: 0x1700142E RID: 5166
		// (get) Token: 0x06003FB8 RID: 16312 RVA: 0x000A6477 File Offset: 0x000A4677
		// (set) Token: 0x06003FB9 RID: 16313 RVA: 0x000A6489 File Offset: 0x000A4689
		[ConfigurationProperty("type", DefaultValue = "string")]
		public string Type
		{
			get
			{
				return (string)base[ProfilePropertySettings.typeProp];
			}
			set
			{
				base[ProfilePropertySettings.typeProp] = value;
			}
		}

		// Token: 0x1700142F RID: 5167
		// (get) Token: 0x06003FBA RID: 16314 RVA: 0x000A6497 File Offset: 0x000A4697
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return ProfilePropertySettings.properties;
			}
		}

		// Token: 0x040022AD RID: 8877
		private static ConfigurationProperty allowAnonymousProp = new ConfigurationProperty("allowAnonymous", typeof(bool), false);

		// Token: 0x040022AE RID: 8878
		private static ConfigurationProperty customProviderDataProp = new ConfigurationProperty("customProviderData", typeof(string), "");

		// Token: 0x040022AF RID: 8879
		private static ConfigurationProperty defaultValueProp = new ConfigurationProperty("defaultValue", typeof(string), "");

		// Token: 0x040022B0 RID: 8880
		private static ConfigurationProperty nameProp = new ConfigurationProperty("name", typeof(string), null, TypeDescriptor.GetConverter(typeof(string)), new ProfilePropertyNameValidator(), ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey);

		// Token: 0x040022B1 RID: 8881
		private static ConfigurationProperty providerProp = new ConfigurationProperty("provider", typeof(string), "");

		// Token: 0x040022B2 RID: 8882
		private static ConfigurationProperty readOnlyProp = new ConfigurationProperty("readOnly", typeof(bool), false);

		// Token: 0x040022B3 RID: 8883
		private static ConfigurationProperty serializeAsProp = new ConfigurationProperty("serializeAs", typeof(SerializationMode), SerializationMode.ProviderSpecific, new GenericEnumConverter(typeof(SerializationMode)), PropertyHelper.DefaultValidator, ConfigurationPropertyOptions.None);

		// Token: 0x040022B4 RID: 8884
		private static ConfigurationProperty typeProp = new ConfigurationProperty("type", typeof(string), "string");

		// Token: 0x040022B5 RID: 8885
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();
	}
}
