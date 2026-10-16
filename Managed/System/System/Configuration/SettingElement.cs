using System;

namespace System.Configuration
{
	/// <summary>Represents a simplified configuration element used for updating elements in the configuration. This class cannot be inherited.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001C2 RID: 450
	public sealed class SettingElement : ConfigurationElement
	{
		// Token: 0x06000BBA RID: 3002 RVA: 0x000314FC File Offset: 0x0002F6FC
		static SettingElement()
		{
			SettingElement.properties.Add(SettingElement.name_prop);
			SettingElement.properties.Add(SettingElement.serialize_as_prop);
			SettingElement.properties.Add(SettingElement.value_prop);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.SettingElement" /> class.</summary>
		// Token: 0x06000BBB RID: 3003 RVA: 0x0003121C File Offset: 0x0002F41C
		public SettingElement()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.SettingElement" /> class based on supplied parameters.</summary>
		/// <param name="name">The name of the <see cref="T:System.Configuration.SettingElement" /> object.</param>
		/// <param name="serializeAs">A <see cref="T:System.Configuration.SettingsSerializeAs" /> object. This object is an enumeration used as the serialization scheme to store configuration settings.</param>
		// Token: 0x06000BBC RID: 3004 RVA: 0x00031595 File Offset: 0x0002F795
		public SettingElement(string name, SettingsSerializeAs serializeAs)
		{
			this.Name = name;
			this.SerializeAs = serializeAs;
		}

		/// <summary>Gets or sets the name of the <see cref="T:System.Configuration.SettingElement" /> object.</summary>
		/// <returns>The name of the <see cref="T:System.Configuration.SettingElement" /> object.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x06000BBD RID: 3005 RVA: 0x000315AB File Offset: 0x0002F7AB
		// (set) Token: 0x06000BBE RID: 3006 RVA: 0x000315BD File Offset: 0x0002F7BD
		[ConfigurationProperty("name", DefaultValue = "", Options = ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey)]
		public string Name
		{
			get
			{
				return (string)base[SettingElement.name_prop];
			}
			set
			{
				base[SettingElement.name_prop] = value;
			}
		}

		/// <summary>Gets or sets the value of a <see cref="T:System.Configuration.SettingElement" /> object by using a <see cref="T:System.Configuration.SettingValueElement" /> object.</summary>
		/// <returns>A <see cref="T:System.Configuration.SettingValueElement" /> object containing the value of the <see cref="T:System.Configuration.SettingElement" /> object.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x06000BBF RID: 3007 RVA: 0x000315CB File Offset: 0x0002F7CB
		// (set) Token: 0x06000BC0 RID: 3008 RVA: 0x000315DD File Offset: 0x0002F7DD
		[ConfigurationProperty("value", DefaultValue = null, Options = ConfigurationPropertyOptions.IsRequired)]
		public SettingValueElement Value
		{
			get
			{
				return (SettingValueElement)base[SettingElement.value_prop];
			}
			set
			{
				base[SettingElement.value_prop] = value;
			}
		}

		/// <summary>Gets or sets the serialization mechanism used to persist the values of the <see cref="T:System.Configuration.SettingElement" /> object.</summary>
		/// <returns>A <see cref="T:System.Configuration.SettingsSerializeAs" /> object.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x06000BC1 RID: 3009 RVA: 0x000315EB File Offset: 0x0002F7EB
		// (set) Token: 0x06000BC2 RID: 3010 RVA: 0x0003160C File Offset: 0x0002F80C
		[ConfigurationProperty("serializeAs", DefaultValue = SettingsSerializeAs.String, Options = ConfigurationPropertyOptions.IsRequired)]
		public SettingsSerializeAs SerializeAs
		{
			get
			{
				if (base[SettingElement.serialize_as_prop] == null)
				{
					return SettingsSerializeAs.String;
				}
				return (SettingsSerializeAs)base[SettingElement.serialize_as_prop];
			}
			set
			{
				base[SettingElement.serialize_as_prop] = value;
			}
		}

		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x06000BC3 RID: 3011 RVA: 0x0003161F File Offset: 0x0002F81F
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return SettingElement.properties;
			}
		}

		/// <summary>Compares the current <see cref="T:System.Configuration.SettingElement" /> instance to the specified object.</summary>
		/// <returns>true if the <see cref="T:System.Configuration.SettingElement" /> instance is equal to the specified object; otherwise, false.</returns>
		/// <param name="settings">The object to compare with.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000BC4 RID: 3012 RVA: 0x00031628 File Offset: 0x0002F828
		public override bool Equals(object settings)
		{
			SettingElement settingElement = settings as SettingElement;
			return settingElement != null && (settingElement.SerializeAs == this.SerializeAs && settingElement.Value == this.Value) && settingElement.Name == this.Name;
		}

		/// <summary>Gets a unique value representing the <see cref="T:System.Configuration.SettingElement" /> current instance.</summary>
		/// <returns>A unique value representing the <see cref="T:System.Configuration.SettingElement" /> current instance.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000BC5 RID: 3013 RVA: 0x00031670 File Offset: 0x0002F870
		public override int GetHashCode()
		{
			int num = (int)(this.SerializeAs ^ (SettingsSerializeAs)127);
			if (this.Name != null)
			{
				num += this.Name.GetHashCode() ^ 127;
			}
			if (this.Value != null)
			{
				num += this.Value.GetHashCode();
			}
			return num;
		}

		// Token: 0x040007AC RID: 1964
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x040007AD RID: 1965
		private static ConfigurationProperty name_prop = new ConfigurationProperty("name", typeof(string), string.Empty, ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey);

		// Token: 0x040007AE RID: 1966
		private static ConfigurationProperty serialize_as_prop = new ConfigurationProperty("serializeAs", typeof(SettingsSerializeAs), null, ConfigurationPropertyOptions.IsRequired);

		// Token: 0x040007AF RID: 1967
		private static ConfigurationProperty value_prop = new ConfigurationProperty("value", typeof(SettingValueElement), null, ConfigurationPropertyOptions.IsRequired);
	}
}
