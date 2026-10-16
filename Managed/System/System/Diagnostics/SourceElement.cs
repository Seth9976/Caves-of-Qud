using System;
using System.Collections;
using System.Configuration;
using System.Xml;

namespace System.Diagnostics
{
	// Token: 0x02000223 RID: 547
	internal class SourceElement : ConfigurationElement
	{
		// Token: 0x06000FD5 RID: 4053 RVA: 0x00046104 File Offset: 0x00044304
		static SourceElement()
		{
			SourceElement._properties.Add(SourceElement._propName);
			SourceElement._properties.Add(SourceElement._propSwitchName);
			SourceElement._properties.Add(SourceElement._propSwitchValue);
			SourceElement._properties.Add(SourceElement._propSwitchType);
			SourceElement._properties.Add(SourceElement._propListeners);
		}

		// Token: 0x1700028F RID: 655
		// (get) Token: 0x06000FD6 RID: 4054 RVA: 0x000461F5 File Offset: 0x000443F5
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

		// Token: 0x17000290 RID: 656
		// (get) Token: 0x06000FD7 RID: 4055 RVA: 0x00046215 File Offset: 0x00044415
		[ConfigurationProperty("listeners")]
		public ListenerElementsCollection Listeners
		{
			get
			{
				return (ListenerElementsCollection)base[SourceElement._propListeners];
			}
		}

		// Token: 0x17000291 RID: 657
		// (get) Token: 0x06000FD8 RID: 4056 RVA: 0x00046227 File Offset: 0x00044427
		[ConfigurationProperty("name", IsRequired = true, DefaultValue = "")]
		public string Name
		{
			get
			{
				return (string)base[SourceElement._propName];
			}
		}

		// Token: 0x17000292 RID: 658
		// (get) Token: 0x06000FD9 RID: 4057 RVA: 0x00046239 File Offset: 0x00044439
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return SourceElement._properties;
			}
		}

		// Token: 0x17000293 RID: 659
		// (get) Token: 0x06000FDA RID: 4058 RVA: 0x00046240 File Offset: 0x00044440
		[ConfigurationProperty("switchName")]
		public string SwitchName
		{
			get
			{
				return (string)base[SourceElement._propSwitchName];
			}
		}

		// Token: 0x17000294 RID: 660
		// (get) Token: 0x06000FDB RID: 4059 RVA: 0x00046252 File Offset: 0x00044452
		[ConfigurationProperty("switchValue")]
		public string SwitchValue
		{
			get
			{
				return (string)base[SourceElement._propSwitchValue];
			}
		}

		// Token: 0x17000295 RID: 661
		// (get) Token: 0x06000FDC RID: 4060 RVA: 0x00046264 File Offset: 0x00044464
		[ConfigurationProperty("switchType")]
		public string SwitchType
		{
			get
			{
				return (string)base[SourceElement._propSwitchType];
			}
		}

		// Token: 0x06000FDD RID: 4061 RVA: 0x00046278 File Offset: 0x00044478
		protected override void DeserializeElement(XmlReader reader, bool serializeCollectionKey)
		{
			base.DeserializeElement(reader, serializeCollectionKey);
			if (!string.IsNullOrEmpty(this.SwitchName) && !string.IsNullOrEmpty(this.SwitchValue))
			{
				throw new ConfigurationErrorsException(SR.GetString("'switchValue' and 'switchName' cannot both be specified on source '{0}'.", new object[] { this.Name }));
			}
		}

		// Token: 0x06000FDE RID: 4062 RVA: 0x000462C6 File Offset: 0x000444C6
		protected override bool OnDeserializeUnrecognizedAttribute(string name, string value)
		{
			this.Attributes.Add(name, value);
			return true;
		}

		// Token: 0x06000FDF RID: 4063 RVA: 0x000462D8 File Offset: 0x000444D8
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

		// Token: 0x06000FE0 RID: 4064 RVA: 0x00046329 File Offset: 0x00044529
		protected override bool SerializeElement(XmlWriter writer, bool serializeCollectionKey)
		{
			return base.SerializeElement(writer, serializeCollectionKey) || (this._attributes != null && this._attributes.Count > 0);
		}

		// Token: 0x06000FE1 RID: 4065 RVA: 0x00046350 File Offset: 0x00044550
		protected override void Unmerge(ConfigurationElement sourceElement, ConfigurationElement parentElement, ConfigurationSaveMode saveMode)
		{
			base.Unmerge(sourceElement, parentElement, saveMode);
			SourceElement sourceElement2 = sourceElement as SourceElement;
			if (sourceElement2 != null && sourceElement2._attributes != null)
			{
				this._attributes = sourceElement2._attributes;
			}
		}

		// Token: 0x06000FE2 RID: 4066 RVA: 0x00046384 File Offset: 0x00044584
		internal void ResetProperties()
		{
			if (this._attributes != null)
			{
				this._attributes.Clear();
				SourceElement._properties.Clear();
				SourceElement._properties.Add(SourceElement._propName);
				SourceElement._properties.Add(SourceElement._propSwitchName);
				SourceElement._properties.Add(SourceElement._propSwitchValue);
				SourceElement._properties.Add(SourceElement._propSwitchType);
				SourceElement._properties.Add(SourceElement._propListeners);
			}
		}

		// Token: 0x040009C6 RID: 2502
		private static readonly ConfigurationPropertyCollection _properties = new ConfigurationPropertyCollection();

		// Token: 0x040009C7 RID: 2503
		private static readonly ConfigurationProperty _propName = new ConfigurationProperty("name", typeof(string), "", ConfigurationPropertyOptions.IsRequired);

		// Token: 0x040009C8 RID: 2504
		private static readonly ConfigurationProperty _propSwitchName = new ConfigurationProperty("switchName", typeof(string), null, ConfigurationPropertyOptions.None);

		// Token: 0x040009C9 RID: 2505
		private static readonly ConfigurationProperty _propSwitchValue = new ConfigurationProperty("switchValue", typeof(string), null, ConfigurationPropertyOptions.None);

		// Token: 0x040009CA RID: 2506
		private static readonly ConfigurationProperty _propSwitchType = new ConfigurationProperty("switchType", typeof(string), null, ConfigurationPropertyOptions.None);

		// Token: 0x040009CB RID: 2507
		private static readonly ConfigurationProperty _propListeners = new ConfigurationProperty("listeners", typeof(ListenerElementsCollection), new ListenerElementsCollection(), ConfigurationPropertyOptions.None);

		// Token: 0x040009CC RID: 2508
		private Hashtable _attributes;
	}
}
