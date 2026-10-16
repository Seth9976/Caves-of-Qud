using System;
using System.Reflection;
using System.Xml;

namespace System.Configuration
{
	/// <summary>Contains the XML representing the serialized value of the setting. This class cannot be inherited.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001C4 RID: 452
	public sealed class SettingValueElement : ConfigurationElement
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.SettingValueElement" /> class. </summary>
		// Token: 0x06000BCF RID: 3023 RVA: 0x0003121C File Offset: 0x0002F41C
		[MonoTODO]
		public SettingValueElement()
		{
		}

		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x06000BD0 RID: 3024 RVA: 0x0003176B File Offset: 0x0002F96B
		[MonoTODO]
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return base.Properties;
			}
		}

		/// <summary>Gets or sets the value of a <see cref="T:System.Configuration.SettingValueElement" /> object by using an <see cref="T:System.Xml.XmlNode" /> object.</summary>
		/// <returns>An <see cref="T:System.Xml.XmlNode" /> object containing the value of a <see cref="T:System.Configuration.SettingElement" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x06000BD1 RID: 3025 RVA: 0x00031773 File Offset: 0x0002F973
		// (set) Token: 0x06000BD2 RID: 3026 RVA: 0x0003177B File Offset: 0x0002F97B
		public XmlNode ValueXml
		{
			get
			{
				return this.node;
			}
			set
			{
				this.node = value;
			}
		}

		// Token: 0x06000BD3 RID: 3027 RVA: 0x00031784 File Offset: 0x0002F984
		[MonoTODO]
		protected override void DeserializeElement(XmlReader reader, bool serializeCollectionKey)
		{
			this.original = new XmlDocument().ReadNode(reader);
			this.node = this.original.CloneNode(true);
		}

		/// <summary>Compares the current <see cref="T:System.Configuration.SettingValueElement" /> instance to the specified object.</summary>
		/// <returns>true if the <see cref="T:System.Configuration.SettingValueElement" /> instance is equal to the specified object; otherwise, false.</returns>
		/// <param name="settingValue">The object to compare.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000BD4 RID: 3028 RVA: 0x0000829A File Offset: 0x0000649A
		public override bool Equals(object settingValue)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets a unique value representing the <see cref="T:System.Configuration.SettingValueElement" /> current instance.</summary>
		/// <returns>A unique value representing the <see cref="T:System.Configuration.SettingValueElement" /> current instance.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000BD5 RID: 3029 RVA: 0x0000829A File Offset: 0x0000649A
		public override int GetHashCode()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000BD6 RID: 3030 RVA: 0x000317A9 File Offset: 0x0002F9A9
		protected override bool IsModified()
		{
			return this.original != this.node;
		}

		// Token: 0x06000BD7 RID: 3031 RVA: 0x000317BC File Offset: 0x0002F9BC
		protected override void Reset(ConfigurationElement parentElement)
		{
			this.node = null;
		}

		// Token: 0x06000BD8 RID: 3032 RVA: 0x000317C5 File Offset: 0x0002F9C5
		protected override void ResetModified()
		{
			this.node = this.original;
		}

		// Token: 0x06000BD9 RID: 3033 RVA: 0x000317D3 File Offset: 0x0002F9D3
		protected override bool SerializeToXmlElement(XmlWriter writer, string elementName)
		{
			if (this.node == null)
			{
				return false;
			}
			this.node.WriteTo(writer);
			return true;
		}

		// Token: 0x06000BDA RID: 3034 RVA: 0x000317EC File Offset: 0x0002F9EC
		protected override void Unmerge(ConfigurationElement sourceElement, ConfigurationElement parentElement, ConfigurationSaveMode saveMode)
		{
			if (parentElement != null && sourceElement.GetType() != parentElement.GetType())
			{
				throw new ConfigurationErrorsException("Can't unmerge two elements of different type");
			}
			bool flag = saveMode == ConfigurationSaveMode.Minimal || saveMode == ConfigurationSaveMode.Modified;
			foreach (object obj in sourceElement.ElementInformation.Properties)
			{
				PropertyInformation propertyInformation = (PropertyInformation)obj;
				if (propertyInformation.ValueOrigin != PropertyValueOrigin.Default)
				{
					PropertyInformation propertyInformation2 = base.ElementInformation.Properties[propertyInformation.Name];
					object value = propertyInformation.Value;
					if (parentElement == null || !this.HasValue(parentElement, propertyInformation.Name))
					{
						propertyInformation2.Value = value;
					}
					else if (value != null)
					{
						object item = this.GetItem(parentElement, propertyInformation.Name);
						if (!this.PropertyIsElement(propertyInformation))
						{
							if (!object.Equals(value, item) || saveMode == ConfigurationSaveMode.Full || (saveMode == ConfigurationSaveMode.Modified && propertyInformation.ValueOrigin == PropertyValueOrigin.SetHere))
							{
								propertyInformation2.Value = value;
							}
						}
						else
						{
							ConfigurationElement configurationElement = (ConfigurationElement)value;
							if (!flag || this.ElementIsModified(configurationElement))
							{
								if (item == null)
								{
									propertyInformation2.Value = value;
								}
								else
								{
									ConfigurationElement configurationElement2 = (ConfigurationElement)item;
									ConfigurationElement configurationElement3 = (ConfigurationElement)propertyInformation2.Value;
									this.ElementUnmerge(configurationElement3, configurationElement, configurationElement2, saveMode);
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x06000BDB RID: 3035 RVA: 0x00031950 File Offset: 0x0002FB50
		private bool HasValue(ConfigurationElement element, string propName)
		{
			PropertyInformation propertyInformation = element.ElementInformation.Properties[propName];
			return propertyInformation != null && propertyInformation.ValueOrigin > PropertyValueOrigin.Default;
		}

		// Token: 0x06000BDC RID: 3036 RVA: 0x0003197D File Offset: 0x0002FB7D
		private object GetItem(ConfigurationElement element, string property)
		{
			PropertyInformation propertyInformation = base.ElementInformation.Properties[property];
			if (propertyInformation == null)
			{
				throw new InvalidOperationException("Property '" + property + "' not found in configuration element");
			}
			return propertyInformation.Value;
		}

		// Token: 0x06000BDD RID: 3037 RVA: 0x000319AE File Offset: 0x0002FBAE
		private bool PropertyIsElement(PropertyInformation prop)
		{
			return typeof(ConfigurationElement).IsAssignableFrom(prop.Type);
		}

		// Token: 0x06000BDE RID: 3038 RVA: 0x000319C5 File Offset: 0x0002FBC5
		private bool ElementIsModified(ConfigurationElement element)
		{
			return (bool)element.GetType().GetMethod("IsModified", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(element, new object[0]);
		}

		// Token: 0x06000BDF RID: 3039 RVA: 0x000319EA File Offset: 0x0002FBEA
		private void ElementUnmerge(ConfigurationElement target, ConfigurationElement sourceElement, ConfigurationElement parentElement, ConfigurationSaveMode saveMode)
		{
			target.GetType().GetMethod("Unmerge", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(target, new object[] { sourceElement, parentElement, saveMode });
		}

		// Token: 0x040007B0 RID: 1968
		private XmlNode node;

		// Token: 0x040007B1 RID: 1969
		private XmlNode original;
	}
}
