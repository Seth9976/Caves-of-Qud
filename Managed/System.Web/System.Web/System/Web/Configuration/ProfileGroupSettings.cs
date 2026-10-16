using System;
using System.Configuration;
using System.Xml;

namespace System.Web.Configuration
{
	/// <summary>Provides programmatic access to the group subsection of the profiles configuration file section.</summary>
	// Token: 0x020005C2 RID: 1474
	public sealed class ProfileGroupSettings : ConfigurationElement
	{
		// Token: 0x06003F84 RID: 16260 RVA: 0x000A5F30 File Offset: 0x000A4130
		static ProfileGroupSettings()
		{
			ProfileGroupSettings.properties.Add(ProfileGroupSettings.propertySettingsProp);
			ProfileGroupSettings.properties.Add(ProfileGroupSettings.nameProp);
		}

		// Token: 0x06003F85 RID: 16261 RVA: 0x0009CC6D File Offset: 0x0009AE6D
		internal ProfileGroupSettings()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Configuration.ProfileGroupSettings" /> class using default settings.</summary>
		/// <param name="name">The name of the new group.</param>
		// Token: 0x06003F86 RID: 16262 RVA: 0x000A5F9F File Offset: 0x000A419F
		public ProfileGroupSettings(string name)
		{
			this.Name = name;
		}

		/// <param name="obj">A <see cref="T:System.Web.Configuration.ProfileGroupSettings" /> object to compare to the current object.</param>
		// Token: 0x06003F87 RID: 16263 RVA: 0x000A5FB0 File Offset: 0x000A41B0
		public override bool Equals(object obj)
		{
			ProfileGroupSettings profileGroupSettings = obj as ProfileGroupSettings;
			return profileGroupSettings != null && !(base.GetType() != profileGroupSettings.GetType()) && this.Name.Equals(profileGroupSettings.Name);
		}

		// Token: 0x06003F88 RID: 16264 RVA: 0x000A5FEF File Offset: 0x000A41EF
		public override int GetHashCode()
		{
			return this.Name.GetHashCode();
		}

		// Token: 0x06003F89 RID: 16265 RVA: 0x000A5FFC File Offset: 0x000A41FC
		internal void DoDeserialize(XmlReader reader)
		{
			this.DeserializeElement(reader, false);
		}

		/// <summary>Gets or sets name of the group of <see cref="T:System.Web.Configuration.ProfilePropertySettings" /> objects this object contains.</summary>
		/// <returns>A string containing the name of the group of <see cref="T:System.Web.Configuration.ProfilePropertySettings" /> objects this object contains. The default value is an empty string ("").</returns>
		// Token: 0x17001420 RID: 5152
		// (get) Token: 0x06003F8A RID: 16266 RVA: 0x000A6006 File Offset: 0x000A4206
		// (set) Token: 0x06003F8B RID: 16267 RVA: 0x000A6018 File Offset: 0x000A4218
		[ConfigurationProperty("name", IsRequired = true, IsKey = true)]
		public string Name
		{
			get
			{
				return (string)base[ProfileGroupSettings.nameProp];
			}
			internal set
			{
				base[ProfileGroupSettings.nameProp] = value;
			}
		}

		/// <summary>Gets the collection of profile property settings this object contains.</summary>
		/// <returns>A <see cref="T:System.Web.Configuration.ProfilePropertySettingsCollection" /> collection that contains all the <see cref="T:System.Web.Configuration.ProfilePropertySettings" /> objects contained in this group.</returns>
		// Token: 0x17001421 RID: 5153
		// (get) Token: 0x06003F8C RID: 16268 RVA: 0x000A6026 File Offset: 0x000A4226
		[ConfigurationProperty("", Options = ConfigurationPropertyOptions.IsDefaultCollection)]
		public ProfilePropertySettingsCollection PropertySettings
		{
			get
			{
				return (ProfilePropertySettingsCollection)base[ProfileGroupSettings.propertySettingsProp];
			}
		}

		// Token: 0x17001422 RID: 5154
		// (get) Token: 0x06003F8D RID: 16269 RVA: 0x000A6038 File Offset: 0x000A4238
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return ProfileGroupSettings.properties;
			}
		}

		// Token: 0x040022A9 RID: 8873
		private static ConfigurationProperty propertySettingsProp = new ConfigurationProperty(null, typeof(ProfilePropertySettingsCollection), null, null, null, ConfigurationPropertyOptions.IsDefaultCollection);

		// Token: 0x040022AA RID: 8874
		private static ConfigurationProperty nameProp = new ConfigurationProperty("name", typeof(string), null, null, PropertyHelper.NonEmptyStringValidator, ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey);

		// Token: 0x040022AB RID: 8875
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();
	}
}
