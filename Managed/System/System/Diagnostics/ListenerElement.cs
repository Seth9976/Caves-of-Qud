using System;
using System.Collections;
using System.Configuration;
using System.Xml;

namespace System.Diagnostics
{
	// Token: 0x0200021F RID: 543
	internal class ListenerElement : TypedElement
	{
		// Token: 0x06000FB5 RID: 4021 RVA: 0x0004598C File Offset: 0x00043B8C
		public ListenerElement(bool allowReferences)
			: base(typeof(TraceListener))
		{
			this._allowReferences = allowReferences;
			ConfigurationPropertyOptions configurationPropertyOptions = ConfigurationPropertyOptions.None;
			if (!this._allowReferences)
			{
				configurationPropertyOptions |= ConfigurationPropertyOptions.IsRequired;
			}
			this._propListenerTypeName = new ConfigurationProperty("type", typeof(string), null, configurationPropertyOptions);
			this._properties.Remove("type");
			this._properties.Add(this._propListenerTypeName);
			this._properties.Add(ListenerElement._propFilter);
			this._properties.Add(ListenerElement._propName);
			this._properties.Add(ListenerElement._propOutputOpts);
		}

		// Token: 0x17000284 RID: 644
		// (get) Token: 0x06000FB6 RID: 4022 RVA: 0x00045A2C File Offset: 0x00043C2C
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

		// Token: 0x17000285 RID: 645
		// (get) Token: 0x06000FB7 RID: 4023 RVA: 0x00045A4C File Offset: 0x00043C4C
		[ConfigurationProperty("filter")]
		public FilterElement Filter
		{
			get
			{
				return (FilterElement)base[ListenerElement._propFilter];
			}
		}

		// Token: 0x17000286 RID: 646
		// (get) Token: 0x06000FB8 RID: 4024 RVA: 0x00045A5E File Offset: 0x00043C5E
		// (set) Token: 0x06000FB9 RID: 4025 RVA: 0x00045A70 File Offset: 0x00043C70
		[ConfigurationProperty("name", IsRequired = true, IsKey = true)]
		public string Name
		{
			get
			{
				return (string)base[ListenerElement._propName];
			}
			set
			{
				base[ListenerElement._propName] = value;
			}
		}

		// Token: 0x17000287 RID: 647
		// (get) Token: 0x06000FBA RID: 4026 RVA: 0x00045A7E File Offset: 0x00043C7E
		// (set) Token: 0x06000FBB RID: 4027 RVA: 0x00045A90 File Offset: 0x00043C90
		[ConfigurationProperty("traceOutputOptions", DefaultValue = TraceOptions.None)]
		public TraceOptions TraceOutputOptions
		{
			get
			{
				return (TraceOptions)base[ListenerElement._propOutputOpts];
			}
			set
			{
				base[ListenerElement._propOutputOpts] = value;
			}
		}

		// Token: 0x17000288 RID: 648
		// (get) Token: 0x06000FBC RID: 4028 RVA: 0x00045AA3 File Offset: 0x00043CA3
		// (set) Token: 0x06000FBD RID: 4029 RVA: 0x00045AB6 File Offset: 0x00043CB6
		[ConfigurationProperty("type")]
		public override string TypeName
		{
			get
			{
				return (string)base[this._propListenerTypeName];
			}
			set
			{
				base[this._propListenerTypeName] = value;
			}
		}

		// Token: 0x06000FBE RID: 4030 RVA: 0x00045AC8 File Offset: 0x00043CC8
		public override bool Equals(object compareTo)
		{
			if (this.Name.Equals("Default") && this.TypeName.Equals(typeof(DefaultTraceListener).FullName))
			{
				ListenerElement listenerElement = compareTo as ListenerElement;
				return listenerElement != null && listenerElement.Name.Equals("Default") && listenerElement.TypeName.Equals(typeof(DefaultTraceListener).FullName);
			}
			return base.Equals(compareTo);
		}

		// Token: 0x06000FBF RID: 4031 RVA: 0x00045B43 File Offset: 0x00043D43
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06000FC0 RID: 4032 RVA: 0x00045B4C File Offset: 0x00043D4C
		public TraceListener GetRuntimeObject()
		{
			if (this._runtimeObject != null)
			{
				return (TraceListener)this._runtimeObject;
			}
			TraceListener traceListener;
			try
			{
				if (string.IsNullOrEmpty(this.TypeName))
				{
					if (this._attributes != null || base.ElementInformation.Properties[ListenerElement._propFilter.Name].ValueOrigin == PropertyValueOrigin.SetHere || this.TraceOutputOptions != TraceOptions.None || !string.IsNullOrEmpty(base.InitData))
					{
						throw new ConfigurationErrorsException(SR.GetString("A listener with no type name specified references the sharedListeners section and cannot have any attributes other than 'Name'.  Listener: '{0}'.", new object[] { this.Name }));
					}
					if (DiagnosticsConfiguration.SharedListeners == null)
					{
						throw new ConfigurationErrorsException(SR.GetString("Listener '{0}' does not exist in the sharedListeners section.", new object[] { this.Name }));
					}
					ListenerElement listenerElement = DiagnosticsConfiguration.SharedListeners[this.Name];
					if (listenerElement == null)
					{
						throw new ConfigurationErrorsException(SR.GetString("Listener '{0}' does not exist in the sharedListeners section.", new object[] { this.Name }));
					}
					this._runtimeObject = listenerElement.GetRuntimeObject();
					traceListener = (TraceListener)this._runtimeObject;
				}
				else
				{
					TraceListener traceListener2 = (TraceListener)base.BaseGetRuntimeObject();
					traceListener2.initializeData = base.InitData;
					traceListener2.Name = this.Name;
					traceListener2.SetAttributes(this.Attributes);
					traceListener2.TraceOutputOptions = this.TraceOutputOptions;
					if (this.Filter != null && this.Filter.TypeName != null && this.Filter.TypeName.Length != 0)
					{
						traceListener2.Filter = this.Filter.GetRuntimeObject();
						XmlWriterTraceListener xmlWriterTraceListener = traceListener2 as XmlWriterTraceListener;
						if (xmlWriterTraceListener != null)
						{
							xmlWriterTraceListener.shouldRespectFilterOnTraceTransfer = true;
						}
					}
					this._runtimeObject = traceListener2;
					traceListener = traceListener2;
				}
			}
			catch (ArgumentException ex)
			{
				throw new ConfigurationErrorsException(SR.GetString("Couldn't create listener '{0}'.", new object[] { this.Name }), ex);
			}
			return traceListener;
		}

		// Token: 0x06000FC1 RID: 4033 RVA: 0x00045D24 File Offset: 0x00043F24
		protected override bool OnDeserializeUnrecognizedAttribute(string name, string value)
		{
			this.Attributes.Add(name, value);
			return true;
		}

		// Token: 0x06000FC2 RID: 4034 RVA: 0x00045D34 File Offset: 0x00043F34
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

		// Token: 0x06000FC3 RID: 4035 RVA: 0x00045D85 File Offset: 0x00043F85
		protected override bool SerializeElement(XmlWriter writer, bool serializeCollectionKey)
		{
			return base.SerializeElement(writer, serializeCollectionKey) || (this._attributes != null && this._attributes.Count > 0);
		}

		// Token: 0x06000FC4 RID: 4036 RVA: 0x00045DAC File Offset: 0x00043FAC
		protected override void Unmerge(ConfigurationElement sourceElement, ConfigurationElement parentElement, ConfigurationSaveMode saveMode)
		{
			base.Unmerge(sourceElement, parentElement, saveMode);
			ListenerElement listenerElement = sourceElement as ListenerElement;
			if (listenerElement != null && listenerElement._attributes != null)
			{
				this._attributes = listenerElement._attributes;
			}
		}

		// Token: 0x06000FC5 RID: 4037 RVA: 0x00045DE0 File Offset: 0x00043FE0
		internal void ResetProperties()
		{
			if (this._attributes != null)
			{
				this._attributes.Clear();
				this._properties.Clear();
				this._properties.Add(this._propListenerTypeName);
				this._properties.Add(ListenerElement._propFilter);
				this._properties.Add(ListenerElement._propName);
				this._properties.Add(ListenerElement._propOutputOpts);
			}
		}

		// Token: 0x06000FC6 RID: 4038 RVA: 0x00045E4C File Offset: 0x0004404C
		internal TraceListener RefreshRuntimeObject(TraceListener listener)
		{
			this._runtimeObject = null;
			TraceListener traceListener;
			try
			{
				string typeName = this.TypeName;
				if (string.IsNullOrEmpty(typeName))
				{
					if (this._attributes != null || base.ElementInformation.Properties[ListenerElement._propFilter.Name].ValueOrigin == PropertyValueOrigin.SetHere || this.TraceOutputOptions != TraceOptions.None || !string.IsNullOrEmpty(base.InitData))
					{
						throw new ConfigurationErrorsException(SR.GetString("A listener with no type name specified references the sharedListeners section and cannot have any attributes other than 'Name'.  Listener: '{0}'.", new object[] { this.Name }));
					}
					if (DiagnosticsConfiguration.SharedListeners == null)
					{
						throw new ConfigurationErrorsException(SR.GetString("Listener '{0}' does not exist in the sharedListeners section.", new object[] { this.Name }));
					}
					ListenerElement listenerElement = DiagnosticsConfiguration.SharedListeners[this.Name];
					if (listenerElement == null)
					{
						throw new ConfigurationErrorsException(SR.GetString("Listener '{0}' does not exist in the sharedListeners section.", new object[] { this.Name }));
					}
					this._runtimeObject = listenerElement.RefreshRuntimeObject(listener);
					traceListener = (TraceListener)this._runtimeObject;
				}
				else if (Type.GetType(typeName) != listener.GetType() || base.InitData != listener.initializeData)
				{
					traceListener = this.GetRuntimeObject();
				}
				else
				{
					listener.SetAttributes(this.Attributes);
					listener.TraceOutputOptions = this.TraceOutputOptions;
					if (listener.Filter != null)
					{
						if (base.ElementInformation.Properties[ListenerElement._propFilter.Name].ValueOrigin == PropertyValueOrigin.SetHere || base.ElementInformation.Properties[ListenerElement._propFilter.Name].ValueOrigin == PropertyValueOrigin.Inherited)
						{
							listener.Filter = this.Filter.RefreshRuntimeObject(listener.Filter);
						}
						else
						{
							listener.Filter = null;
						}
					}
					this._runtimeObject = listener;
					traceListener = listener;
				}
			}
			catch (ArgumentException ex)
			{
				throw new ConfigurationErrorsException(SR.GetString("Couldn't create listener '{0}'.", new object[] { this.Name }), ex);
			}
			return traceListener;
		}

		// Token: 0x040009BC RID: 2492
		private static readonly ConfigurationProperty _propFilter = new ConfigurationProperty("filter", typeof(FilterElement), null, ConfigurationPropertyOptions.None);

		// Token: 0x040009BD RID: 2493
		private static readonly ConfigurationProperty _propName = new ConfigurationProperty("name", typeof(string), null, ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey);

		// Token: 0x040009BE RID: 2494
		private static readonly ConfigurationProperty _propOutputOpts = new ConfigurationProperty("traceOutputOptions", typeof(TraceOptions), TraceOptions.None, ConfigurationPropertyOptions.None);

		// Token: 0x040009BF RID: 2495
		private ConfigurationProperty _propListenerTypeName;

		// Token: 0x040009C0 RID: 2496
		private bool _allowReferences;

		// Token: 0x040009C1 RID: 2497
		private Hashtable _attributes;

		// Token: 0x040009C2 RID: 2498
		internal bool _isAddedByDefault;
	}
}
