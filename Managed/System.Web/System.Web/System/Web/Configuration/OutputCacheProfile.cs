using System;
using System.ComponentModel;
using System.Configuration;
using System.Web.UI;

namespace System.Web.Configuration
{
	/// <summary>Configures the output cache profile that can be used by the application pages. This class cannot be inherited.</summary>
	// Token: 0x020005BA RID: 1466
	public sealed class OutputCacheProfile : ConfigurationElement
	{
		// Token: 0x06003ECC RID: 16076 RVA: 0x000A4468 File Offset: 0x000A2668
		static OutputCacheProfile()
		{
			OutputCacheProfile.properties.Add(OutputCacheProfile.durationProp);
			OutputCacheProfile.properties.Add(OutputCacheProfile.enabledProp);
			OutputCacheProfile.properties.Add(OutputCacheProfile.locationProp);
			OutputCacheProfile.properties.Add(OutputCacheProfile.nameProp);
			OutputCacheProfile.properties.Add(OutputCacheProfile.noStoreProp);
			OutputCacheProfile.properties.Add(OutputCacheProfile.sqlDependencyProp);
			OutputCacheProfile.properties.Add(OutputCacheProfile.varyByContentEncodingProp);
			OutputCacheProfile.properties.Add(OutputCacheProfile.varyByControlProp);
			OutputCacheProfile.properties.Add(OutputCacheProfile.varyByCustomProp);
			OutputCacheProfile.properties.Add(OutputCacheProfile.varyByHeaderProp);
			OutputCacheProfile.properties.Add(OutputCacheProfile.varyByParamProp);
		}

		// Token: 0x06003ECD RID: 16077 RVA: 0x0009CC6D File Offset: 0x0009AE6D
		internal OutputCacheProfile()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Configuration.OutputCacheProfile" /> class.</summary>
		/// <param name="name">The name value to use.</param>
		// Token: 0x06003ECE RID: 16078 RVA: 0x000A466F File Offset: 0x000A286F
		public OutputCacheProfile(string name)
		{
			this.Name = name;
		}

		/// <summary>Gets or sets the time duration during which the page or control is cached.</summary>
		/// <returns>The time duration in seconds.</returns>
		// Token: 0x170013CB RID: 5067
		// (get) Token: 0x06003ECF RID: 16079 RVA: 0x000A467E File Offset: 0x000A287E
		// (set) Token: 0x06003ED0 RID: 16080 RVA: 0x000A4690 File Offset: 0x000A2890
		[ConfigurationProperty("duration", DefaultValue = "-1")]
		public int Duration
		{
			get
			{
				return (int)base[OutputCacheProfile.durationProp];
			}
			set
			{
				base[OutputCacheProfile.durationProp] = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether caching is enabled.</summary>
		/// <returns>true if caching is enabled; otherwise, false. The default value is false. </returns>
		// Token: 0x170013CC RID: 5068
		// (get) Token: 0x06003ED1 RID: 16081 RVA: 0x000A46A3 File Offset: 0x000A28A3
		// (set) Token: 0x06003ED2 RID: 16082 RVA: 0x000A46B5 File Offset: 0x000A28B5
		[ConfigurationProperty("enabled", DefaultValue = "True")]
		public bool Enabled
		{
			get
			{
				return (bool)base[OutputCacheProfile.enabledProp];
			}
			set
			{
				base[OutputCacheProfile.enabledProp] = value;
			}
		}

		/// <summary>Gets or sets the output cache location.</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.OutputCacheLocation" /> enumeration values. The default is Any.</returns>
		// Token: 0x170013CD RID: 5069
		// (get) Token: 0x06003ED3 RID: 16083 RVA: 0x000A46C8 File Offset: 0x000A28C8
		// (set) Token: 0x06003ED4 RID: 16084 RVA: 0x000A46DA File Offset: 0x000A28DA
		[ConfigurationProperty("location")]
		public OutputCacheLocation Location
		{
			get
			{
				return (OutputCacheLocation)base[OutputCacheProfile.locationProp];
			}
			set
			{
				base[OutputCacheProfile.locationProp] = value;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Web.Configuration.OutputCacheProfile" /> name.</summary>
		/// <returns>The <see cref="T:System.Web.Configuration.OutputCacheProfile" /> name.</returns>
		// Token: 0x170013CE RID: 5070
		// (get) Token: 0x06003ED5 RID: 16085 RVA: 0x000A46ED File Offset: 0x000A28ED
		// (set) Token: 0x06003ED6 RID: 16086 RVA: 0x000A46FF File Offset: 0x000A28FF
		[StringValidator(MinLength = 1)]
		[TypeConverter(typeof(WhiteSpaceTrimStringConverter))]
		[ConfigurationProperty("name", DefaultValue = "", Options = ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey)]
		public string Name
		{
			get
			{
				return (string)base[OutputCacheProfile.nameProp];
			}
			set
			{
				base[OutputCacheProfile.nameProp] = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether secondary storage is enabled. </summary>
		/// <returns>true if secondary storage is enabled; otherwise, false. The default value is false. </returns>
		// Token: 0x170013CF RID: 5071
		// (get) Token: 0x06003ED7 RID: 16087 RVA: 0x000A470D File Offset: 0x000A290D
		// (set) Token: 0x06003ED8 RID: 16088 RVA: 0x000A471F File Offset: 0x000A291F
		[ConfigurationProperty("noStore", DefaultValue = "False")]
		public bool NoStore
		{
			get
			{
				return (bool)base[OutputCacheProfile.noStoreProp];
			}
			set
			{
				base[OutputCacheProfile.noStoreProp] = value;
			}
		}

		/// <summary>Gets or sets the <see cref="P:System.Web.Configuration.OutputCacheProfile.SqlDependency" /> property. </summary>
		/// <returns>The <see cref="P:System.Web.Configuration.OutputCacheProfile.SqlDependency" /> value.</returns>
		// Token: 0x170013D0 RID: 5072
		// (get) Token: 0x06003ED9 RID: 16089 RVA: 0x000A4732 File Offset: 0x000A2932
		// (set) Token: 0x06003EDA RID: 16090 RVA: 0x000A4744 File Offset: 0x000A2944
		[ConfigurationProperty("sqlDependency")]
		public string SqlDependency
		{
			get
			{
				return (string)base[OutputCacheProfile.sqlDependencyProp];
			}
			set
			{
				base[OutputCacheProfile.sqlDependencyProp] = value;
			}
		}

		/// <summary>Gets or sets the semicolon-delimited set of content encodings to be cached.</summary>
		/// <returns>The list of content encodings.</returns>
		// Token: 0x170013D1 RID: 5073
		// (get) Token: 0x06003EDB RID: 16091 RVA: 0x000A4752 File Offset: 0x000A2952
		// (set) Token: 0x06003EDC RID: 16092 RVA: 0x000A4764 File Offset: 0x000A2964
		[ConfigurationProperty("varyByContentEncoding")]
		public string VaryByContentEncoding
		{
			get
			{
				return (string)base[OutputCacheProfile.varyByContentEncodingProp];
			}
			set
			{
				base[OutputCacheProfile.varyByContentEncodingProp] = value;
			}
		}

		/// <summary>Gets or sets the <see cref="P:System.Web.Configuration.OutputCacheProfile.VaryByControl" /> property.</summary>
		/// <returns>The <see cref="P:System.Web.Configuration.OutputCacheProfile.VaryByControl" /> value.</returns>
		// Token: 0x170013D2 RID: 5074
		// (get) Token: 0x06003EDD RID: 16093 RVA: 0x000A4772 File Offset: 0x000A2972
		// (set) Token: 0x06003EDE RID: 16094 RVA: 0x000A4784 File Offset: 0x000A2984
		[ConfigurationProperty("varyByControl")]
		public string VaryByControl
		{
			get
			{
				return (string)base[OutputCacheProfile.varyByControlProp];
			}
			set
			{
				base[OutputCacheProfile.varyByControlProp] = value;
			}
		}

		/// <summary>Gets or sets the <see cref="P:System.Web.Configuration.OutputCacheProfile.VaryByCustom" /> property.</summary>
		/// <returns>The <see cref="P:System.Web.Configuration.OutputCacheProfile.VaryByCustom" /> value.</returns>
		// Token: 0x170013D3 RID: 5075
		// (get) Token: 0x06003EDF RID: 16095 RVA: 0x000A4792 File Offset: 0x000A2992
		// (set) Token: 0x06003EE0 RID: 16096 RVA: 0x000A47A4 File Offset: 0x000A29A4
		[ConfigurationProperty("varyByCustom")]
		public string VaryByCustom
		{
			get
			{
				return (string)base[OutputCacheProfile.varyByCustomProp];
			}
			set
			{
				base[OutputCacheProfile.varyByCustomProp] = value;
			}
		}

		/// <summary>Gets or sets the <see cref="P:System.Web.Configuration.OutputCacheProfile.VaryByHeader" /> property.</summary>
		/// <returns>The <see cref="P:System.Web.Configuration.OutputCacheProfile.VaryByHeader" /> value.</returns>
		// Token: 0x170013D4 RID: 5076
		// (get) Token: 0x06003EE1 RID: 16097 RVA: 0x000A47B2 File Offset: 0x000A29B2
		// (set) Token: 0x06003EE2 RID: 16098 RVA: 0x000A47C4 File Offset: 0x000A29C4
		[ConfigurationProperty("varyByHeader")]
		public string VaryByHeader
		{
			get
			{
				return (string)base[OutputCacheProfile.varyByHeaderProp];
			}
			set
			{
				base[OutputCacheProfile.varyByHeaderProp] = value;
			}
		}

		/// <summary>Gets or sets the <see cref="P:System.Web.Configuration.OutputCacheProfile.VaryByParam" /> property.</summary>
		/// <returns>The <see cref="P:System.Web.Configuration.OutputCacheProfile.VaryByParam" /> value.</returns>
		// Token: 0x170013D5 RID: 5077
		// (get) Token: 0x06003EE3 RID: 16099 RVA: 0x000A47D2 File Offset: 0x000A29D2
		// (set) Token: 0x06003EE4 RID: 16100 RVA: 0x000A47E4 File Offset: 0x000A29E4
		[ConfigurationProperty("varyByParam")]
		public string VaryByParam
		{
			get
			{
				return (string)base[OutputCacheProfile.varyByParamProp];
			}
			set
			{
				base[OutputCacheProfile.varyByParamProp] = value;
			}
		}

		// Token: 0x170013D6 RID: 5078
		// (get) Token: 0x06003EE5 RID: 16101 RVA: 0x000A47F2 File Offset: 0x000A29F2
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return OutputCacheProfile.properties;
			}
		}

		// Token: 0x04002259 RID: 8793
		private static ConfigurationProperty durationProp = new ConfigurationProperty("duration", typeof(int), -1);

		// Token: 0x0400225A RID: 8794
		private static ConfigurationProperty enabledProp = new ConfigurationProperty("enabled", typeof(bool), true);

		// Token: 0x0400225B RID: 8795
		private static ConfigurationProperty locationProp = new ConfigurationProperty("location", typeof(OutputCacheLocation), null, new GenericEnumConverter(typeof(OutputCacheLocation)), PropertyHelper.DefaultValidator, ConfigurationPropertyOptions.None);

		// Token: 0x0400225C RID: 8796
		private static ConfigurationProperty nameProp = new ConfigurationProperty("name", typeof(string), "", PropertyHelper.WhiteSpaceTrimStringConverter, PropertyHelper.NonEmptyStringValidator, ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey);

		// Token: 0x0400225D RID: 8797
		private static ConfigurationProperty noStoreProp = new ConfigurationProperty("noStore", typeof(bool), false);

		// Token: 0x0400225E RID: 8798
		private static ConfigurationProperty sqlDependencyProp = new ConfigurationProperty("sqlDependency", typeof(string));

		// Token: 0x0400225F RID: 8799
		private static ConfigurationProperty varyByContentEncodingProp = new ConfigurationProperty("varyByContentEncoding", typeof(string));

		// Token: 0x04002260 RID: 8800
		private static ConfigurationProperty varyByControlProp = new ConfigurationProperty("varyByControl", typeof(string));

		// Token: 0x04002261 RID: 8801
		private static ConfigurationProperty varyByCustomProp = new ConfigurationProperty("varyByCustom", typeof(string));

		// Token: 0x04002262 RID: 8802
		private static ConfigurationProperty varyByHeaderProp = new ConfigurationProperty("varyByHeader", typeof(string));

		// Token: 0x04002263 RID: 8803
		private static ConfigurationProperty varyByParamProp = new ConfigurationProperty("varyByParam", typeof(string));

		// Token: 0x04002264 RID: 8804
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();
	}
}
