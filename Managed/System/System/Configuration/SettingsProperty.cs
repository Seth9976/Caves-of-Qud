using System;

namespace System.Configuration
{
	/// <summary>Used internally as the class that represents metadata about an individual configuration property.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001CF RID: 463
	public class SettingsProperty
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.SettingsProperty" /> class, based on the supplied parameter.</summary>
		/// <param name="propertyToCopy">Specifies a copy of an existing <see cref="T:System.Configuration.SettingsProperty" /> object.</param>
		// Token: 0x06000C01 RID: 3073 RVA: 0x00031E54 File Offset: 0x00030054
		public SettingsProperty(SettingsProperty propertyToCopy)
			: this(propertyToCopy.Name, propertyToCopy.PropertyType, propertyToCopy.Provider, propertyToCopy.IsReadOnly, propertyToCopy.DefaultValue, propertyToCopy.SerializeAs, new SettingsAttributeDictionary(propertyToCopy.Attributes), propertyToCopy.ThrowOnErrorDeserializing, propertyToCopy.ThrowOnErrorSerializing)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.SettingsProperty" /> class. based on the supplied parameter.</summary>
		/// <param name="name">Specifies the name of an existing <see cref="T:System.Configuration.SettingsProperty" /> object.</param>
		// Token: 0x06000C02 RID: 3074 RVA: 0x00031EA4 File Offset: 0x000300A4
		public SettingsProperty(string name)
			: this(name, null, null, false, null, SettingsSerializeAs.String, new SettingsAttributeDictionary(), false, false)
		{
		}

		/// <summary>Creates a new instance of the <see cref="T:System.Configuration.SettingsProperty" /> class based on the supplied parameters.</summary>
		/// <param name="name">The name of the <see cref="T:System.Configuration.SettingsProperty" /> object.</param>
		/// <param name="propertyType">The type of <see cref="T:System.Configuration.SettingsProperty" /> object.</param>
		/// <param name="provider">A <see cref="T:System.Configuration.SettingsProvider" /> object to use for persistence.</param>
		/// <param name="isReadOnly">A <see cref="T:System.Boolean" /> value specifying whether the <see cref="T:System.Configuration.SettingsProperty" /> object is read-only.</param>
		/// <param name="defaultValue">The default value of the <see cref="T:System.Configuration.SettingsProperty" /> object.</param>
		/// <param name="serializeAs">A <see cref="T:System.Configuration.SettingsSerializeAs" /> object. This object is an enumeration used to set the serialization scheme for storing application settings.</param>
		/// <param name="attributes">A <see cref="T:System.Configuration.SettingsAttributeDictionary" /> object.</param>
		/// <param name="throwOnErrorDeserializing">A Boolean value specifying whether an error will be thrown when the property is unsuccessfully deserialized.</param>
		/// <param name="throwOnErrorSerializing">A Boolean value specifying whether an error will be thrown when the property is unsuccessfully serialized.</param>
		// Token: 0x06000C03 RID: 3075 RVA: 0x00031EC4 File Offset: 0x000300C4
		public SettingsProperty(string name, Type propertyType, SettingsProvider provider, bool isReadOnly, object defaultValue, SettingsSerializeAs serializeAs, SettingsAttributeDictionary attributes, bool throwOnErrorDeserializing, bool throwOnErrorSerializing)
		{
			this.name = name;
			this.propertyType = propertyType;
			this.provider = provider;
			this.isReadOnly = isReadOnly;
			this.defaultValue = defaultValue;
			this.serializeAs = serializeAs;
			this.attributes = attributes;
			this.throwOnErrorDeserializing = throwOnErrorDeserializing;
			this.throwOnErrorSerializing = throwOnErrorSerializing;
		}

		/// <summary>Gets a <see cref="T:System.Configuration.SettingsAttributeDictionary" /> object containing the attributes of the <see cref="T:System.Configuration.SettingsProperty" /> object.</summary>
		/// <returns>A <see cref="T:System.Configuration.SettingsAttributeDictionary" /> object.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000205 RID: 517
		// (get) Token: 0x06000C04 RID: 3076 RVA: 0x00031F1C File Offset: 0x0003011C
		public virtual SettingsAttributeDictionary Attributes
		{
			get
			{
				return this.attributes;
			}
		}

		/// <summary>Gets or sets the default value of the <see cref="T:System.Configuration.SettingsProperty" /> object.</summary>
		/// <returns>An object containing the default value of the <see cref="T:System.Configuration.SettingsProperty" /> object.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000206 RID: 518
		// (get) Token: 0x06000C05 RID: 3077 RVA: 0x00031F24 File Offset: 0x00030124
		// (set) Token: 0x06000C06 RID: 3078 RVA: 0x00031F2C File Offset: 0x0003012C
		public virtual object DefaultValue
		{
			get
			{
				return this.defaultValue;
			}
			set
			{
				this.defaultValue = value;
			}
		}

		/// <summary>Gets or sets a value specifying whether a <see cref="T:System.Configuration.SettingsProperty" /> object is read-only. </summary>
		/// <returns>true if the <see cref="T:System.Configuration.SettingsProperty" /> is read-only; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000207 RID: 519
		// (get) Token: 0x06000C07 RID: 3079 RVA: 0x00031F35 File Offset: 0x00030135
		// (set) Token: 0x06000C08 RID: 3080 RVA: 0x00031F3D File Offset: 0x0003013D
		public virtual bool IsReadOnly
		{
			get
			{
				return this.isReadOnly;
			}
			set
			{
				this.isReadOnly = value;
			}
		}

		/// <summary>Gets or sets the name of the <see cref="T:System.Configuration.SettingsProperty" />.</summary>
		/// <returns>The name of the <see cref="T:System.Configuration.SettingsProperty" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000208 RID: 520
		// (get) Token: 0x06000C09 RID: 3081 RVA: 0x00031F46 File Offset: 0x00030146
		// (set) Token: 0x06000C0A RID: 3082 RVA: 0x00031F4E File Offset: 0x0003014E
		public virtual string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		/// <summary>Gets or sets the type for the <see cref="T:System.Configuration.SettingsProperty" />.</summary>
		/// <returns>The type for the <see cref="T:System.Configuration.SettingsProperty" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000209 RID: 521
		// (get) Token: 0x06000C0B RID: 3083 RVA: 0x00031F57 File Offset: 0x00030157
		// (set) Token: 0x06000C0C RID: 3084 RVA: 0x00031F5F File Offset: 0x0003015F
		public virtual Type PropertyType
		{
			get
			{
				return this.propertyType;
			}
			set
			{
				this.propertyType = value;
			}
		}

		/// <summary>Gets or sets the provider for the <see cref="T:System.Configuration.SettingsProperty" />.</summary>
		/// <returns>A <see cref="T:System.Configuration.SettingsProvider" /> object.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700020A RID: 522
		// (get) Token: 0x06000C0D RID: 3085 RVA: 0x00031F68 File Offset: 0x00030168
		// (set) Token: 0x06000C0E RID: 3086 RVA: 0x00031F70 File Offset: 0x00030170
		public virtual SettingsProvider Provider
		{
			get
			{
				return this.provider;
			}
			set
			{
				this.provider = value;
			}
		}

		/// <summary>Gets or sets a <see cref="T:System.Configuration.SettingsSerializeAs" /> object for the <see cref="T:System.Configuration.SettingsProperty" />.</summary>
		/// <returns>A <see cref="T:System.Configuration.SettingsSerializeAs" /> object.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700020B RID: 523
		// (get) Token: 0x06000C0F RID: 3087 RVA: 0x00031F79 File Offset: 0x00030179
		// (set) Token: 0x06000C10 RID: 3088 RVA: 0x00031F81 File Offset: 0x00030181
		public virtual SettingsSerializeAs SerializeAs
		{
			get
			{
				return this.serializeAs;
			}
			set
			{
				this.serializeAs = value;
			}
		}

		/// <summary>Gets or sets a value specifying whether an error will be thrown when the property is unsuccessfully deserialized.</summary>
		/// <returns>true if the error will be thrown when the property is unsuccessfully deserialized; otherwise, false.</returns>
		// Token: 0x1700020C RID: 524
		// (get) Token: 0x06000C11 RID: 3089 RVA: 0x00031F8A File Offset: 0x0003018A
		// (set) Token: 0x06000C12 RID: 3090 RVA: 0x00031F92 File Offset: 0x00030192
		public bool ThrowOnErrorDeserializing
		{
			get
			{
				return this.throwOnErrorDeserializing;
			}
			set
			{
				this.throwOnErrorDeserializing = value;
			}
		}

		/// <summary>Gets or sets a value specifying whether an error will be thrown when the property is unsuccessfully serialized.</summary>
		/// <returns>true if the error will be thrown when the property is unsuccessfully serialized; otherwise, false.</returns>
		// Token: 0x1700020D RID: 525
		// (get) Token: 0x06000C13 RID: 3091 RVA: 0x00031F9B File Offset: 0x0003019B
		// (set) Token: 0x06000C14 RID: 3092 RVA: 0x00031FA3 File Offset: 0x000301A3
		public bool ThrowOnErrorSerializing
		{
			get
			{
				return this.throwOnErrorSerializing;
			}
			set
			{
				this.throwOnErrorSerializing = value;
			}
		}

		// Token: 0x040007BF RID: 1983
		private string name;

		// Token: 0x040007C0 RID: 1984
		private Type propertyType;

		// Token: 0x040007C1 RID: 1985
		private SettingsProvider provider;

		// Token: 0x040007C2 RID: 1986
		private bool isReadOnly;

		// Token: 0x040007C3 RID: 1987
		private object defaultValue;

		// Token: 0x040007C4 RID: 1988
		private SettingsSerializeAs serializeAs;

		// Token: 0x040007C5 RID: 1989
		private SettingsAttributeDictionary attributes;

		// Token: 0x040007C6 RID: 1990
		private bool throwOnErrorDeserializing;

		// Token: 0x040007C7 RID: 1991
		private bool throwOnErrorSerializing;
	}
}
