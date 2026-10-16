using System;
using System.Collections;
using System.Configuration;
using System.Xml;

namespace System.Diagnostics
{
	// Token: 0x0200022A RID: 554
	internal class SwitchElement : ConfigurationElement
	{
		// Token: 0x06001011 RID: 4113 RVA: 0x00046BEC File Offset: 0x00044DEC
		static SwitchElement()
		{
			SwitchElement._properties.Add(SwitchElement._propName);
			SwitchElement._properties.Add(SwitchElement._propValue);
		}

		// Token: 0x170002A3 RID: 675
		// (get) Token: 0x06001012 RID: 4114 RVA: 0x00046C5B File Offset: 0x00044E5B
		public Hashtable Attributes
		{
			get
			{
				if (this._attributes == null)
				{
					this._attributes = new Hashtable(StringComparer.OrdinalIgnoreCase);
				}
				return this._attributes;
			}
		}

		// Token: 0x170002A4 RID: 676
		// (get) Token: 0x06001013 RID: 4115 RVA: 0x00046C7B File Offset: 0x00044E7B
		[ConfigurationProperty("name", DefaultValue = "", IsRequired = true, IsKey = true)]
		public string Name
		{
			get
			{
				return (string)base[SwitchElement._propName];
			}
		}

		// Token: 0x170002A5 RID: 677
		// (get) Token: 0x06001014 RID: 4116 RVA: 0x00046C8D File Offset: 0x00044E8D
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return SwitchElement._properties;
			}
		}

		// Token: 0x170002A6 RID: 678
		// (get) Token: 0x06001015 RID: 4117 RVA: 0x00046C94 File Offset: 0x00044E94
		[ConfigurationProperty("value", IsRequired = true)]
		public string Value
		{
			get
			{
				return (string)base[SwitchElement._propValue];
			}
		}

		// Token: 0x06001016 RID: 4118 RVA: 0x00046CA6 File Offset: 0x00044EA6
		protected override bool OnDeserializeUnrecognizedAttribute(string name, string value)
		{
			this.Attributes.Add(name, value);
			return true;
		}

		// Token: 0x06001017 RID: 4119 RVA: 0x00046CB8 File Offset: 0x00044EB8
		protected override void PreSerialize(XmlWriter writer)
		{
			if (this._attributes != null)
			{
				IDictionaryEnumerator enumerator = this._attributes.GetEnumerator();
				while (enumerator.MoveNext())
				{
					string text = (string)enumerator.Value;
					string text2 = (string)enumerator.Key;
					if (text != null && writer != null)
					{
						writer.WriteAttributeString(text2, text);
					}
				}
			}
		}

		// Token: 0x06001018 RID: 4120 RVA: 0x00046D09 File Offset: 0x00044F09
		protected override bool SerializeElement(XmlWriter writer, bool serializeCollectionKey)
		{
			return base.SerializeElement(writer, serializeCollectionKey) || (this._attributes != null && this._attributes.Count > 0);
		}

		// Token: 0x06001019 RID: 4121 RVA: 0x00046D30 File Offset: 0x00044F30
		protected override void Unmerge(ConfigurationElement sourceElement, ConfigurationElement parentElement, ConfigurationSaveMode saveMode)
		{
			base.Unmerge(sourceElement, parentElement, saveMode);
			SwitchElement switchElement = sourceElement as SwitchElement;
			if (switchElement != null && switchElement._attributes != null)
			{
				this._attributes = switchElement._attributes;
			}
		}

		// Token: 0x0600101A RID: 4122 RVA: 0x00046D64 File Offset: 0x00044F64
		internal void ResetProperties()
		{
			if (this._attributes != null)
			{
				this._attributes.Clear();
				SwitchElement._properties.Clear();
				SwitchElement._properties.Add(SwitchElement._propName);
				SwitchElement._properties.Add(SwitchElement._propValue);
			}
		}

		// Token: 0x040009E6 RID: 2534
		private static readonly ConfigurationPropertyCollection _properties = new ConfigurationPropertyCollection();

		// Token: 0x040009E7 RID: 2535
		private static readonly ConfigurationProperty _propName = new ConfigurationProperty("name", typeof(string), "", ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey);

		// Token: 0x040009E8 RID: 2536
		private static readonly ConfigurationProperty _propValue = new ConfigurationProperty("value", typeof(string), null, ConfigurationPropertyOptions.IsRequired);

		// Token: 0x040009E9 RID: 2537
		private Hashtable _attributes;
	}
}
