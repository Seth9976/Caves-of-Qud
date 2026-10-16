using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;

namespace System.CodeDom.Compiler
{
	// Token: 0x0200035B RID: 859
	internal sealed class Compiler : ConfigurationElement
	{
		// Token: 0x06001C5A RID: 7258 RVA: 0x00066D9C File Offset: 0x00064F9C
		static Compiler()
		{
			Compiler.properties.Add(Compiler.compilerOptionsProp);
			Compiler.properties.Add(Compiler.extensionProp);
			Compiler.properties.Add(Compiler.languageProp);
			Compiler.properties.Add(Compiler.typeProp);
			Compiler.properties.Add(Compiler.warningLevelProp);
			Compiler.properties.Add(Compiler.providerOptionsProp);
		}

		// Token: 0x06001C5B RID: 7259 RVA: 0x0003121C File Offset: 0x0002F41C
		internal Compiler()
		{
		}

		// Token: 0x06001C5C RID: 7260 RVA: 0x00066EDA File Offset: 0x000650DA
		public Compiler(string compilerOptions, string extension, string language, string type, int warningLevel)
		{
			this.CompilerOptions = compilerOptions;
			this.Extension = extension;
			this.Language = language;
			this.Type = type;
			this.WarningLevel = warningLevel;
		}

		// Token: 0x170005AD RID: 1453
		// (get) Token: 0x06001C5D RID: 7261 RVA: 0x00066F07 File Offset: 0x00065107
		// (set) Token: 0x06001C5E RID: 7262 RVA: 0x00066F19 File Offset: 0x00065119
		[ConfigurationProperty("compilerOptions", DefaultValue = "")]
		public string CompilerOptions
		{
			get
			{
				return (string)base[Compiler.compilerOptionsProp];
			}
			internal set
			{
				base[Compiler.compilerOptionsProp] = value;
			}
		}

		// Token: 0x170005AE RID: 1454
		// (get) Token: 0x06001C5F RID: 7263 RVA: 0x00066F27 File Offset: 0x00065127
		// (set) Token: 0x06001C60 RID: 7264 RVA: 0x00066F39 File Offset: 0x00065139
		[ConfigurationProperty("extension", DefaultValue = "")]
		public string Extension
		{
			get
			{
				return (string)base[Compiler.extensionProp];
			}
			internal set
			{
				base[Compiler.extensionProp] = value;
			}
		}

		// Token: 0x170005AF RID: 1455
		// (get) Token: 0x06001C61 RID: 7265 RVA: 0x00066F47 File Offset: 0x00065147
		// (set) Token: 0x06001C62 RID: 7266 RVA: 0x00066F59 File Offset: 0x00065159
		[ConfigurationProperty("language", DefaultValue = "", Options = ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey)]
		public string Language
		{
			get
			{
				return (string)base[Compiler.languageProp];
			}
			internal set
			{
				base[Compiler.languageProp] = value;
			}
		}

		// Token: 0x170005B0 RID: 1456
		// (get) Token: 0x06001C63 RID: 7267 RVA: 0x00066F67 File Offset: 0x00065167
		// (set) Token: 0x06001C64 RID: 7268 RVA: 0x00066F79 File Offset: 0x00065179
		[ConfigurationProperty("type", DefaultValue = "", Options = ConfigurationPropertyOptions.IsRequired)]
		public string Type
		{
			get
			{
				return (string)base[Compiler.typeProp];
			}
			internal set
			{
				base[Compiler.typeProp] = value;
			}
		}

		// Token: 0x170005B1 RID: 1457
		// (get) Token: 0x06001C65 RID: 7269 RVA: 0x00066F87 File Offset: 0x00065187
		// (set) Token: 0x06001C66 RID: 7270 RVA: 0x00066F99 File Offset: 0x00065199
		[IntegerValidator(MinValue = 0, MaxValue = 4)]
		[ConfigurationProperty("warningLevel", DefaultValue = "0")]
		public int WarningLevel
		{
			get
			{
				return (int)base[Compiler.warningLevelProp];
			}
			internal set
			{
				base[Compiler.warningLevelProp] = value;
			}
		}

		// Token: 0x170005B2 RID: 1458
		// (get) Token: 0x06001C67 RID: 7271 RVA: 0x00066FAC File Offset: 0x000651AC
		// (set) Token: 0x06001C68 RID: 7272 RVA: 0x00066FBE File Offset: 0x000651BE
		[ConfigurationProperty("", Options = ConfigurationPropertyOptions.IsDefaultCollection)]
		public CompilerProviderOptionsCollection ProviderOptions
		{
			get
			{
				return (CompilerProviderOptionsCollection)base[Compiler.providerOptionsProp];
			}
			internal set
			{
				base[Compiler.providerOptionsProp] = value;
			}
		}

		// Token: 0x170005B3 RID: 1459
		// (get) Token: 0x06001C69 RID: 7273 RVA: 0x00066FCC File Offset: 0x000651CC
		public Dictionary<string, string> ProviderOptionsDictionary
		{
			get
			{
				return this.ProviderOptions.ProviderOptions;
			}
		}

		// Token: 0x170005B4 RID: 1460
		// (get) Token: 0x06001C6A RID: 7274 RVA: 0x00066FD9 File Offset: 0x000651D9
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return Compiler.properties;
			}
		}

		// Token: 0x04000E9A RID: 3738
		private static ConfigurationProperty compilerOptionsProp = new ConfigurationProperty("compilerOptions", typeof(string), "");

		// Token: 0x04000E9B RID: 3739
		private static ConfigurationProperty extensionProp = new ConfigurationProperty("extension", typeof(string), "");

		// Token: 0x04000E9C RID: 3740
		private static ConfigurationProperty languageProp = new ConfigurationProperty("language", typeof(string), "", ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey);

		// Token: 0x04000E9D RID: 3741
		private static ConfigurationProperty typeProp = new ConfigurationProperty("type", typeof(string), "", ConfigurationPropertyOptions.IsRequired);

		// Token: 0x04000E9E RID: 3742
		private static ConfigurationProperty warningLevelProp = new ConfigurationProperty("warningLevel", typeof(int), 0, TypeDescriptor.GetConverter(typeof(int)), new IntegerValidator(0, 4), ConfigurationPropertyOptions.None);

		// Token: 0x04000E9F RID: 3743
		private static ConfigurationProperty providerOptionsProp = new ConfigurationProperty("", typeof(CompilerProviderOptionsCollection), null, null, null, ConfigurationPropertyOptions.IsDefaultCollection);

		// Token: 0x04000EA0 RID: 3744
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();
	}
}
