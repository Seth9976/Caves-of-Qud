using System;
using System.Configuration;
using System.Configuration.Provider;
using System.Reflection;
using System.Web.Configuration;
using System.Web.Security;

namespace System.Web.Profile
{
	/// <summary>Provides untyped access to profile property values and information.</summary>
	// Token: 0x02000504 RID: 1284
	public class ProfileBase : SettingsBase
	{
		// Token: 0x0600390D RID: 14605 RVA: 0x000976B0 File Offset: 0x000958B0
		private static void InitProperties()
		{
			SettingsPropertyCollection settingsPropertyCollection = new SettingsPropertyCollection();
			ProfileSection profileSection = (ProfileSection)WebConfigurationManager.GetSection("system.web/profile");
			RootProfilePropertySettingsCollection propertySettings = profileSection.PropertySettings;
			for (int i = 0; i < propertySettings.GroupSettings.Count; i++)
			{
				ProfileGroupSettings profileGroupSettings = propertySettings.GroupSettings[i];
				ProfilePropertySettingsCollection propertySettings2 = profileGroupSettings.PropertySettings;
				for (int j = 0; j < propertySettings2.Count; j++)
				{
					SettingsProperty settingsProperty = ProfileBase.CreateSettingsProperty(profileGroupSettings, propertySettings2[j]);
					ProfileBase.ValidateProperty(settingsProperty, propertySettings2[j].ElementInformation);
					settingsPropertyCollection.Add(settingsProperty);
				}
			}
			for (int k = 0; k < propertySettings.Count; k++)
			{
				SettingsProperty settingsProperty2 = ProfileBase.CreateSettingsProperty(null, propertySettings[k]);
				ProfileBase.ValidateProperty(settingsProperty2, propertySettings[k].ElementInformation);
				settingsPropertyCollection.Add(settingsProperty2);
			}
			if (profileSection.Inherits.Length > 0)
			{
				Type profileCommonType = ProfileParser.GetProfileCommonType(HttpContext.Current);
				if (profileCommonType != null)
				{
					Type type = profileCommonType.BaseType;
					for (;;)
					{
						PropertyInfo[] properties = type.GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public);
						if (properties.Length != 0)
						{
							for (int l = 0; l < properties.Length; l++)
							{
								settingsPropertyCollection.Add(ProfileBase.CreateSettingsProperty(properties[l]));
							}
						}
						if (type.BaseType == null || type.BaseType == typeof(ProfileBase))
						{
							break;
						}
						type = type.BaseType;
					}
				}
			}
			settingsPropertyCollection.SetReadOnly();
			string text = "Profiles.SettingsPropertyCollection";
			lock (text)
			{
				if (ProfileBase._properties == null)
				{
					ProfileBase._properties = settingsPropertyCollection;
				}
			}
		}

		/// <summary>Used by ASP.NET to create an instance of a profile for the specified user name.</summary>
		/// <returns>An <see cref="T:System.Web.Profile.ProfileBase" /> that represents the profile for the specified user.</returns>
		/// <param name="username">The name of the user to create a profile for.</param>
		/// <exception cref="T:System.Configuration.Provider.ProviderException">The enabled attribute of the profile section of the Web.config file is false.</exception>
		/// <exception cref="T:System.Web.HttpException">The current hosting permission level is less than <see cref="F:System.Web.AspNetHostingPermissionLevel.Medium" />.</exception>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">A property type specified in the profile section of the Web.config file could not be created.-or-The allowAnonymous attribute for a property in the profile section of the Web.config file is set to true and the enabled attribute of the &lt;anonymousIdentification&gt; element is set to false.-or-The serializeAs attribute for a property in the profile section of the Web.config file is set to <see cref="F:System.Configuration.SettingsSerializeAs.Binary" /> and the <see cref="P:System.Type.IsSerializable" /> property of the specified type returns false.-or-The name of a provider specified using the provider attribute of a profile property could not be found in the <see cref="P:System.Web.Profile.ProfileManager.Providers" /> collection.-or-The type specified for a profile property could not be found.-or-A profile property was specified with a name that matches a property name on the base class specified in the inherits attribute of the profile section.</exception>
		// Token: 0x0600390F RID: 14607 RVA: 0x00097886 File Offset: 0x00095A86
		public static ProfileBase Create(string username)
		{
			return ProfileBase.Create(username, true);
		}

		/// <summary>Used by ASP.NET to create an instance of a profile for the specified user name. Takes a parameter indicating whether the user is authenticated or anonymous.</summary>
		/// <returns>A <see cref="T:System.Web.Profile.ProfileBase" /> object that represents the profile for the specified user.</returns>
		/// <param name="username">The name of the user to create a profile for.</param>
		/// <param name="isAuthenticated">true to indicate the user is authenticated; false to indicate the user is anonymous.</param>
		/// <exception cref="T:System.Configuration.Provider.ProviderException">The enabled attribute of the profile section of the Web.config file is false.</exception>
		/// <exception cref="T:System.Web.HttpException">The current hosting permission level is less than <see cref="F:System.Web.AspNetHostingPermissionLevel.Medium" />.</exception>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">A property type specified in the profile section of the Web.config file could not be created.-or-The allowAnonymous attribute for a property in the profile section of the Web.config file is set to true and the enabled attribute of the &lt;anonymousIdentification&gt; element is set to false.-or-The serializeAs attribute for a property in the profile section of the Web.config file is set to <see cref="F:System.Configuration.SettingsSerializeAs.Binary" /> and the <see cref="P:System.Type.IsSerializable" /> property of the specified type returns false.-or-The name of a provider specified using the provider attribute of a profile property could not be found in the <see cref="P:System.Web.Profile.ProfileManager.Providers" /> collection.-or-The type specified for a profile property could not be found.-or-A profile property was specified with a name that matches a property name on the base class specified in the inherits attribute of the profile section.</exception>
		// Token: 0x06003910 RID: 14608 RVA: 0x00097890 File Offset: 0x00095A90
		public static ProfileBase Create(string username, bool isAuthenticated)
		{
			Type profileCommonType = ProfileParser.GetProfileCommonType(HttpContext.Current);
			ProfileBase profileBase;
			if (profileCommonType != null)
			{
				profileBase = (ProfileBase)Activator.CreateInstance(profileCommonType);
			}
			else
			{
				profileBase = new DefaultProfile();
			}
			profileBase.Initialize(username, isAuthenticated);
			return profileBase;
		}

		/// <summary>Gets a group of properties identified by a group name.</summary>
		/// <returns>A <see cref="T:System.Web.Profile.ProfileGroupBase" /> object for a group of properties configured with the specified group name.</returns>
		/// <param name="groupName">The name of the group of properties.</param>
		/// <exception cref="T:System.Configuration.Provider.ProviderException">The specified profile property group name was not found in the properties configuration section.</exception>
		// Token: 0x06003911 RID: 14609 RVA: 0x000978D0 File Offset: 0x00095AD0
		public ProfileGroupBase GetProfileGroup(string groupName)
		{
			Type profileGroupType = ProfileParser.GetProfileGroupType(HttpContext.Current, groupName);
			if (profileGroupType != null)
			{
				ProfileGroupBase profileGroupBase = (ProfileGroupBase)Activator.CreateInstance(profileGroupType);
				profileGroupBase.Init(this, groupName);
				return profileGroupBase;
			}
			throw new ProviderException("Group '" + groupName + "' not found");
		}

		/// <summary>Gets the value of a profile property.</summary>
		/// <returns>The value of the specified profile property, typed as object.</returns>
		/// <param name="propertyName">The name of the profile property.</param>
		/// <exception cref="T:System.Configuration.Provider.ProviderException">An attempt was made to set a property value on an anonymous profile where the property's allowAnonymous attribute is false.</exception>
		/// <exception cref="T:System.Configuration.SettingsPropertyNotFoundException">There are no properties defined for the current profile.-or-The specified profile property name does not exist in the current profile.-or-The provider for the specified profile property did not recognize the specified property.</exception>
		// Token: 0x06003912 RID: 14610 RVA: 0x00097921 File Offset: 0x00095B21
		public object GetPropertyValue(string propertyName)
		{
			if (!this._propertiyValuesLoaded)
			{
				this.InitPropertiesValues();
			}
			this._lastActivityDate = DateTime.UtcNow;
			return this._propertiyValues[propertyName].PropertyValue;
		}

		/// <summary>Sets the value of a profile property.</summary>
		/// <param name="propertyName">The name of the property to set.</param>
		/// <param name="propertyValue">The value to assign to the property.</param>
		/// <exception cref="T:System.Configuration.Provider.ProviderException">An attempt was made to set a property value on an anonymous profile where the property's allowAnonymous attribute is false.</exception>
		/// <exception cref="T:System.Configuration.SettingsPropertyNotFoundException">There are no properties defined for the current profile.-or-The specified profile property name does not exist in the current profile.-or-The provider for the specified profile property did not recognize the specified property.</exception>
		/// <exception cref="T:System.Configuration.SettingsPropertyIsReadOnlyException">An attempt was made to set a value value on a property that was marked as read-only.</exception>
		/// <exception cref="T:System.Configuration.SettingsPropertyWrongTypeException">An attempt was made to assign a value to a property using an incompatible type.</exception>
		// Token: 0x06003913 RID: 14611 RVA: 0x00097950 File Offset: 0x00095B50
		public void SetPropertyValue(string propertyName, object propertyValue)
		{
			if (!this._propertiyValuesLoaded)
			{
				this.InitPropertiesValues();
			}
			if (this._propertiyValues[propertyName] == null)
			{
				throw new SettingsPropertyNotFoundException("The settings property '" + propertyName + "' was not found.");
			}
			if (!(bool)this._propertiyValues[propertyName].Property.Attributes["AllowAnonymous"] && this.IsAnonymous)
			{
				throw new ProviderException("This property cannot be set for anonymous users.");
			}
			this._propertiyValues[propertyName].PropertyValue = propertyValue;
			this._dirty = true;
			this._lastActivityDate = DateTime.UtcNow;
			this._lastUpdatedDate = this._lastActivityDate;
		}

		/// <summary>Gets or sets a profile property value indexed by the property name.</summary>
		/// <returns>The value of the specified profile property, typed as object.</returns>
		/// <param name="propertyName">The name of the profile property.</param>
		/// <exception cref="T:System.Configuration.Provider.ProviderException">An attempt was made to set a property value on an anonymous profile where the property's allowAnonymous attribute is false.</exception>
		/// <exception cref="T:System.Configuration.SettingsPropertyNotFoundException">There are no properties defined for the current profile.-or-The specified profile property name does not exist in the current profile.-or-The provider for the specified profile property did not recognize the specified property.</exception>
		/// <exception cref="T:System.Configuration.SettingsPropertyIsReadOnlyException">An attempt was made to set a property value that was marked as read-only.</exception>
		/// <exception cref="T:System.Configuration.SettingsPropertyWrongTypeException">An attempt was made to assign a value to a property using an incompatible type.</exception>
		// Token: 0x170011CE RID: 4558
		public override object this[string propertyName]
		{
			get
			{
				return this.GetPropertyValue(propertyName);
			}
			set
			{
				this.SetPropertyValue(propertyName, value);
			}
		}

		// Token: 0x06003916 RID: 14614 RVA: 0x00097A0C File Offset: 0x00095C0C
		private void InitPropertiesValues()
		{
			if (!this._propertiyValuesLoaded)
			{
				this._propertiyValues = ProfileManager.Provider.GetPropertyValues(this._settingsContext, ProfileBase.Properties);
				this._propertiyValuesLoaded = true;
			}
		}

		// Token: 0x06003917 RID: 14615 RVA: 0x00097A38 File Offset: 0x00095C38
		private static Type GetPropertyType(ProfileGroupSettings pgs, ProfilePropertySettings pps)
		{
			Type type = HttpApplication.LoadType(pps.Type);
			if (type != null)
			{
				return type;
			}
			Type type2;
			if (pgs == null)
			{
				type2 = ProfileParser.GetProfileCommonType(HttpContext.Current);
			}
			else
			{
				type2 = ProfileParser.GetProfileGroupType(HttpContext.Current, pgs.Name);
			}
			if (type2 == null)
			{
				return null;
			}
			PropertyInfo property = type2.GetProperty(pps.Name);
			if (property != null)
			{
				return property.PropertyType;
			}
			return null;
		}

		// Token: 0x06003918 RID: 14616 RVA: 0x00097AA8 File Offset: 0x00095CA8
		private static void ValidateProperty(SettingsProperty settingsProperty, ElementInformation elementInfo)
		{
			string text = string.Empty;
			if (!AnonymousIdentificationModule.Enabled && (bool)settingsProperty.Attributes["AllowAnonymous"])
			{
				text = "Profile property '{0}' allows anonymous users to store data. This requires that the AnonymousIdentification feature be enabled.";
			}
			if (settingsProperty.PropertyType == null)
			{
				text = "The type specified for a profile property '{0}' could not be found.";
			}
			if (settingsProperty.SerializeAs == SettingsSerializeAs.Binary && !settingsProperty.PropertyType.IsSerializable)
			{
				text = "The type for the property '{0}' cannot be serialized using the binary serializer, since the type is not marked as serializable.";
			}
			if (text.Length > 0)
			{
				throw new ConfigurationErrorsException(string.Format(text, settingsProperty.Name), elementInfo.Source, elementInfo.LineNumber);
			}
		}

		// Token: 0x06003919 RID: 14617 RVA: 0x00097B38 File Offset: 0x00095D38
		private static SettingsProperty CreateSettingsProperty(PropertyInfo property)
		{
			SettingsProperty settingsProperty = new SettingsProperty(property.Name);
			Attribute[] array = (Attribute[])property.GetCustomAttributes(false);
			SettingsAttributeDictionary settingsAttributeDictionary = new SettingsAttributeDictionary();
			bool flag = false;
			settingsProperty.SerializeAs = SettingsSerializeAs.ProviderSpecific;
			settingsProperty.PropertyType = property.PropertyType;
			settingsProperty.IsReadOnly = false;
			settingsProperty.ThrowOnErrorDeserializing = false;
			settingsProperty.ThrowOnErrorSerializing = true;
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i] is DefaultSettingValueAttribute)
				{
					settingsProperty.DefaultValue = ((DefaultSettingValueAttribute)array[i]).Value;
					flag = true;
				}
				else if (array[i] is SettingsProviderAttribute)
				{
					Type type = HttpApplication.LoadType(((SettingsProviderAttribute)array[i]).ProviderTypeName);
					settingsProperty.Provider = (SettingsProvider)Activator.CreateInstance(type);
					settingsProperty.Provider.Initialize(null, null);
				}
				else if (array[i] is SettingsSerializeAsAttribute)
				{
					settingsProperty.SerializeAs = ((SettingsSerializeAsAttribute)array[i]).SerializeAs;
				}
				else if (array[i] is SettingsAllowAnonymousAttribute)
				{
					settingsProperty.Attributes["AllowAnonymous"] = ((SettingsAllowAnonymousAttribute)array[i]).Allow;
				}
				else if (array[i] is CustomProviderDataAttribute)
				{
					settingsProperty.Attributes["CustomProviderData"] = ((CustomProviderDataAttribute)array[i]).CustomProviderData;
				}
				else if (array[i] is ApplicationScopedSettingAttribute || array[i] is UserScopedSettingAttribute || array[i] is SettingsDescriptionAttribute || array[i] is SettingAttribute)
				{
					settingsAttributeDictionary.Add(array[i].GetType(), array[i]);
				}
			}
			if (settingsProperty.Provider == null)
			{
				settingsProperty.Provider = ProfileManager.Provider;
			}
			if (settingsProperty.Attributes["AllowAnonymous"] == null)
			{
				settingsProperty.Attributes["AllowAnonymous"] = false;
			}
			if (!flag && settingsProperty.PropertyType == typeof(string) && settingsProperty.DefaultValue == null)
			{
				settingsProperty.DefaultValue = string.Empty;
			}
			return settingsProperty;
		}

		// Token: 0x0600391A RID: 14618 RVA: 0x00097D38 File Offset: 0x00095F38
		private static SettingsProperty CreateSettingsProperty(ProfileGroupSettings pgs, ProfilePropertySettings pps)
		{
			SettingsProperty settingsProperty = new SettingsProperty(((pgs == null) ? string.Empty : (pgs.Name + ".")) + pps.Name);
			settingsProperty.Attributes.Add("AllowAnonymous", pps.AllowAnonymous);
			settingsProperty.DefaultValue = pps.DefaultValue;
			settingsProperty.IsReadOnly = pps.ReadOnly;
			settingsProperty.Provider = ProfileManager.Provider;
			settingsProperty.ThrowOnErrorDeserializing = false;
			settingsProperty.ThrowOnErrorSerializing = true;
			if (pps.Type.Length == 0 || pps.Type == "string")
			{
				settingsProperty.PropertyType = typeof(string);
			}
			else
			{
				settingsProperty.PropertyType = ProfileBase.GetPropertyType(pgs, pps);
			}
			switch (pps.SerializeAs)
			{
			case SerializationMode.String:
				settingsProperty.SerializeAs = SettingsSerializeAs.String;
				break;
			case SerializationMode.Xml:
				settingsProperty.SerializeAs = SettingsSerializeAs.Xml;
				break;
			case SerializationMode.Binary:
				settingsProperty.SerializeAs = SettingsSerializeAs.Binary;
				break;
			case SerializationMode.ProviderSpecific:
				settingsProperty.SerializeAs = SettingsSerializeAs.ProviderSpecific;
				break;
			}
			return settingsProperty;
		}

		/// <summary>Initializes the profile property values and information for the current user.</summary>
		/// <param name="username">The name of the user to initialize the profile for.</param>
		/// <param name="isAuthenticated">true to indicate the user is authenticated; false to indicate the user is anonymous.</param>
		// Token: 0x0600391B RID: 14619 RVA: 0x00097E3C File Offset: 0x0009603C
		public void Initialize(string username, bool isAuthenticated)
		{
			this._settingsContext = new SettingsContext();
			this._settingsContext.Add("UserName", username);
			this._settingsContext.Add("IsAuthenticated", isAuthenticated);
			SettingsProviderCollection settingsProviderCollection = new SettingsProviderCollection();
			settingsProviderCollection.Add(ProfileManager.Provider);
			base.Initialize(this.Context, ProfileBase.Properties, settingsProviderCollection);
		}

		/// <summary>Updates the profile data source with changed profile property values.</summary>
		// Token: 0x0600391C RID: 14620 RVA: 0x00097E9E File Offset: 0x0009609E
		public override void Save()
		{
			if (this.IsDirty)
			{
				ProfileManager.Provider.SetPropertyValues(this._settingsContext, this._propertiyValues);
			}
		}

		/// <summary>Gets a value indicating whether the user profile is for an anonymous user.</summary>
		/// <returns>true if the user profile is for an anonymous user; otherwise, false.</returns>
		// Token: 0x170011CF RID: 4559
		// (get) Token: 0x0600391D RID: 14621 RVA: 0x00097EBE File Offset: 0x000960BE
		public bool IsAnonymous
		{
			get
			{
				return !(bool)this._settingsContext["IsAuthenticated"];
			}
		}

		/// <summary>Gets a value indicating whether any of the profile properties have been modified.</summary>
		/// <returns>true if any of the profile properties have been modified; otherwise, false.</returns>
		// Token: 0x170011D0 RID: 4560
		// (get) Token: 0x0600391E RID: 14622 RVA: 0x00097ED8 File Offset: 0x000960D8
		public bool IsDirty
		{
			get
			{
				return this._dirty;
			}
		}

		/// <summary>Gets the most recent date and time that the profile was read or modified.</summary>
		/// <returns>The most recent date and time that the profile was read or modified by the default provider.</returns>
		// Token: 0x170011D1 RID: 4561
		// (get) Token: 0x0600391F RID: 14623 RVA: 0x00097EE0 File Offset: 0x000960E0
		public DateTime LastActivityDate
		{
			get
			{
				return this._lastActivityDate;
			}
		}

		/// <summary>Gets the most recent date and time that the profile was modified.</summary>
		/// <returns>The most recent date and time that the profile was modified by the default provider.</returns>
		// Token: 0x170011D2 RID: 4562
		// (get) Token: 0x06003920 RID: 14624 RVA: 0x00097EE8 File Offset: 0x000960E8
		public DateTime LastUpdatedDate
		{
			get
			{
				return this._lastUpdatedDate;
			}
		}

		/// <summary>Gets a collection of <see cref="T:System.Configuration.SettingsProperty" /> objects for each property in the profile.</summary>
		/// <returns>A <see cref="T:System.Configuration.SettingsPropertyCollection" /> of <see cref="T:System.Configuration.SettingsProperty" /> objects for each property in the profile for the application.</returns>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">A property type specified in the profile section of the Web.config file could not be created.-or-The allowAnonymous attribute for a property in the profile section of the Web.config file is set to true and the enabled attribute of the anonymousIdentification element is set to false.-or-The serializeAs attribute for a property in the profile section of the Web.config file is set to <see cref="F:System.Configuration.SettingsSerializeAs.Binary" /> and the <see cref="P:System.Type.IsSerializable" /> property of the specified type returns false.-or-The name of a provider specified using the provider attribute of a profile property could not be found in the <see cref="P:System.Web.Profile.ProfileManager.Providers" /> collection.-or-The type specified for a profile property could not be found.-or-A profile property was specified with a name that matches a property name on the base class specified in the inherits attribute of the profile section.</exception>
		// Token: 0x170011D3 RID: 4563
		// (get) Token: 0x06003921 RID: 14625 RVA: 0x00097EF0 File Offset: 0x000960F0
		public new static SettingsPropertyCollection Properties
		{
			get
			{
				if (ProfileBase._properties == null)
				{
					ProfileBase.InitProperties();
				}
				return ProfileBase._properties;
			}
		}

		/// <summary>Gets the user name for the profile.</summary>
		/// <returns>The user name for the profile or the anonymous-user identifier assigned to the profile.</returns>
		// Token: 0x170011D4 RID: 4564
		// (get) Token: 0x06003922 RID: 14626 RVA: 0x00097F03 File Offset: 0x00096103
		public string UserName
		{
			get
			{
				return (string)this._settingsContext["UserName"];
			}
		}

		// Token: 0x04001F1A RID: 7962
		private bool _propertiyValuesLoaded;

		// Token: 0x04001F1B RID: 7963
		private bool _dirty;

		// Token: 0x04001F1C RID: 7964
		private DateTime _lastActivityDate = DateTime.MinValue;

		// Token: 0x04001F1D RID: 7965
		private DateTime _lastUpdatedDate = DateTime.MinValue;

		// Token: 0x04001F1E RID: 7966
		private SettingsContext _settingsContext;

		// Token: 0x04001F1F RID: 7967
		private SettingsPropertyValueCollection _propertiyValues;

		// Token: 0x04001F20 RID: 7968
		private const string Profiles_SettingsPropertyCollection = "Profiles.SettingsPropertyCollection";

		// Token: 0x04001F21 RID: 7969
		private static SettingsPropertyCollection _properties;
	}
}
