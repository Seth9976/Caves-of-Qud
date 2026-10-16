using System;
using System.Configuration;

namespace System.Diagnostics
{
	// Token: 0x0200023A RID: 570
	internal class TypedElement : ConfigurationElement
	{
		// Token: 0x0600110F RID: 4367 RVA: 0x0004A7E2 File Offset: 0x000489E2
		public TypedElement(Type baseType)
		{
			this._properties = new ConfigurationPropertyCollection();
			this._properties.Add(TypedElement._propTypeName);
			this._properties.Add(TypedElement._propInitData);
			this._baseType = baseType;
		}

		// Token: 0x170002E1 RID: 737
		// (get) Token: 0x06001110 RID: 4368 RVA: 0x0004A81C File Offset: 0x00048A1C
		// (set) Token: 0x06001111 RID: 4369 RVA: 0x0004A82E File Offset: 0x00048A2E
		[ConfigurationProperty("initializeData", DefaultValue = "")]
		public string InitData
		{
			get
			{
				return (string)base[TypedElement._propInitData];
			}
			set
			{
				base[TypedElement._propInitData] = value;
			}
		}

		// Token: 0x170002E2 RID: 738
		// (get) Token: 0x06001112 RID: 4370 RVA: 0x0004A83C File Offset: 0x00048A3C
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return this._properties;
			}
		}

		// Token: 0x170002E3 RID: 739
		// (get) Token: 0x06001113 RID: 4371 RVA: 0x0004A844 File Offset: 0x00048A44
		// (set) Token: 0x06001114 RID: 4372 RVA: 0x0004A856 File Offset: 0x00048A56
		[ConfigurationProperty("type", IsRequired = true, DefaultValue = "")]
		public virtual string TypeName
		{
			get
			{
				return (string)base[TypedElement._propTypeName];
			}
			set
			{
				base[TypedElement._propTypeName] = value;
			}
		}

		// Token: 0x06001115 RID: 4373 RVA: 0x0004A864 File Offset: 0x00048A64
		protected object BaseGetRuntimeObject()
		{
			if (this._runtimeObject == null)
			{
				this._runtimeObject = TraceUtils.GetRuntimeObject(this.TypeName, this._baseType, this.InitData);
			}
			return this._runtimeObject;
		}

		// Token: 0x04000A33 RID: 2611
		protected static readonly ConfigurationProperty _propTypeName = new ConfigurationProperty("type", typeof(string), string.Empty, ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsTypeStringTransformationRequired);

		// Token: 0x04000A34 RID: 2612
		protected static readonly ConfigurationProperty _propInitData = new ConfigurationProperty("initializeData", typeof(string), string.Empty, ConfigurationPropertyOptions.None);

		// Token: 0x04000A35 RID: 2613
		protected ConfigurationPropertyCollection _properties;

		// Token: 0x04000A36 RID: 2614
		protected object _runtimeObject;

		// Token: 0x04000A37 RID: 2615
		private Type _baseType;
	}
}
