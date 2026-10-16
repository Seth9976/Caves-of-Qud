using System;

namespace System.Configuration
{
	/// <summary>Contains a collection of <see cref="T:System.Configuration.SettingElement" /> objects. This class cannot be inherited.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001C3 RID: 451
	public sealed class SettingElementCollection : ConfigurationElementCollection
	{
		/// <summary>Adds a <see cref="T:System.Configuration.SettingElement" /> object to the collection.</summary>
		/// <param name="element">The <see cref="T:System.Configuration.SettingElement" /> object to add to the collection.</param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000BC7 RID: 3015 RVA: 0x000316BF File Offset: 0x0002F8BF
		public void Add(SettingElement element)
		{
			this.BaseAdd(element);
		}

		/// <summary>Removes all <see cref="T:System.Configuration.SettingElement" /> objects from the collection.</summary>
		// Token: 0x06000BC8 RID: 3016 RVA: 0x000316C8 File Offset: 0x0002F8C8
		public void Clear()
		{
			base.BaseClear();
		}

		/// <summary>Gets a <see cref="T:System.Configuration.SettingElement" /> object from the collection. </summary>
		/// <returns>A <see cref="T:System.Configuration.SettingElement" /> object.</returns>
		/// <param name="elementKey">A string value representing the <see cref="T:System.Configuration.SettingElement" /> object in the collection.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000BC9 RID: 3017 RVA: 0x000316D0 File Offset: 0x0002F8D0
		public SettingElement Get(string elementKey)
		{
			foreach (object obj in this)
			{
				SettingElement settingElement = (SettingElement)obj;
				if (settingElement.Name == elementKey)
				{
					return settingElement;
				}
			}
			return null;
		}

		/// <summary>Removes a <see cref="T:System.Configuration.SettingElement" /> object from the collection.</summary>
		/// <param name="element">A <see cref="T:System.Configuration.SettingElement" /> object.</param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000BCA RID: 3018 RVA: 0x00031734 File Offset: 0x0002F934
		public void Remove(SettingElement element)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			base.BaseRemove(element.Name);
		}

		// Token: 0x06000BCB RID: 3019 RVA: 0x00031750 File Offset: 0x0002F950
		protected override ConfigurationElement CreateNewElement()
		{
			return new SettingElement();
		}

		// Token: 0x06000BCC RID: 3020 RVA: 0x00031757 File Offset: 0x0002F957
		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((SettingElement)element).Name;
		}

		/// <summary>Gets the type of the configuration collection.</summary>
		/// <returns>The <see cref="T:System.Configuration.ConfigurationElementCollectionType" /> object of the collection.</returns>
		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x06000BCD RID: 3021 RVA: 0x00003062 File Offset: 0x00001262
		public override ConfigurationElementCollectionType CollectionType
		{
			get
			{
				return ConfigurationElementCollectionType.BasicMap;
			}
		}

		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x06000BCE RID: 3022 RVA: 0x00031764 File Offset: 0x0002F964
		protected override string ElementName
		{
			get
			{
				return "setting";
			}
		}
	}
}
