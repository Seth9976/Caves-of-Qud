using System;
using System.Configuration;

namespace System.CodeDom.Compiler
{
	// Token: 0x0200035D RID: 861
	internal sealed class CompilerProviderOption : ConfigurationElement
	{
		// Token: 0x06001C80 RID: 7296 RVA: 0x00067310 File Offset: 0x00065510
		static CompilerProviderOption()
		{
			CompilerProviderOption.properties.Add(CompilerProviderOption.nameProp);
			CompilerProviderOption.properties.Add(CompilerProviderOption.valueProp);
		}

		// Token: 0x170005BD RID: 1469
		// (get) Token: 0x06001C81 RID: 7297 RVA: 0x00067383 File Offset: 0x00065583
		// (set) Token: 0x06001C82 RID: 7298 RVA: 0x00067395 File Offset: 0x00065595
		[ConfigurationProperty("name", DefaultValue = "", Options = ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey)]
		public string Name
		{
			get
			{
				return (string)base[CompilerProviderOption.nameProp];
			}
			set
			{
				base[CompilerProviderOption.nameProp] = value;
			}
		}

		// Token: 0x170005BE RID: 1470
		// (get) Token: 0x06001C83 RID: 7299 RVA: 0x000673A3 File Offset: 0x000655A3
		// (set) Token: 0x06001C84 RID: 7300 RVA: 0x000673B5 File Offset: 0x000655B5
		[ConfigurationProperty("value", DefaultValue = "", Options = ConfigurationPropertyOptions.IsRequired)]
		public string Value
		{
			get
			{
				return (string)base[CompilerProviderOption.valueProp];
			}
			set
			{
				base[CompilerProviderOption.valueProp] = value;
			}
		}

		// Token: 0x170005BF RID: 1471
		// (get) Token: 0x06001C85 RID: 7301 RVA: 0x000673C3 File Offset: 0x000655C3
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return CompilerProviderOption.properties;
			}
		}

		// Token: 0x04000EA6 RID: 3750
		private static ConfigurationProperty nameProp = new ConfigurationProperty("name", typeof(string), "", ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey);

		// Token: 0x04000EA7 RID: 3751
		private static ConfigurationProperty valueProp = new ConfigurationProperty("value", typeof(string), "", ConfigurationPropertyOptions.IsRequired);

		// Token: 0x04000EA8 RID: 3752
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();
	}
}
