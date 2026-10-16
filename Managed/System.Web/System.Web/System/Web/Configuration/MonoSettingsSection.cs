using System;
using System.Configuration;

namespace System.Web.Configuration
{
	// Token: 0x020005B7 RID: 1463
	internal sealed class MonoSettingsSection : ConfigurationSection
	{
		// Token: 0x06003EB0 RID: 16048 RVA: 0x000A4214 File Offset: 0x000A2414
		static MonoSettingsSection()
		{
			MonoSettingsSection.properties.Add(MonoSettingsSection.compilersCompatibilityProp);
			MonoSettingsSection.properties.Add(MonoSettingsSection.useCompilersCompatibilityProp);
			MonoSettingsSection.properties.Add(MonoSettingsSection.verificationCompatibilityProp);
		}

		// Token: 0x170013C2 RID: 5058
		// (get) Token: 0x06003EB1 RID: 16049 RVA: 0x000A42B7 File Offset: 0x000A24B7
		[ConfigurationProperty("compilersCompatibility")]
		public CompilerCollection CompilersCompatibility
		{
			get
			{
				return (CompilerCollection)base[MonoSettingsSection.compilersCompatibilityProp];
			}
		}

		// Token: 0x170013C3 RID: 5059
		// (get) Token: 0x06003EB2 RID: 16050 RVA: 0x000A42C9 File Offset: 0x000A24C9
		// (set) Token: 0x06003EB3 RID: 16051 RVA: 0x000A42DB File Offset: 0x000A24DB
		[ConfigurationProperty("useCompilersCompatibility", DefaultValue = "True")]
		public bool UseCompilersCompatibility
		{
			get
			{
				return (bool)base[MonoSettingsSection.useCompilersCompatibilityProp];
			}
			set
			{
				base[MonoSettingsSection.useCompilersCompatibilityProp] = value;
			}
		}

		// Token: 0x170013C4 RID: 5060
		// (get) Token: 0x06003EB4 RID: 16052 RVA: 0x000A42EE File Offset: 0x000A24EE
		// (set) Token: 0x06003EB5 RID: 16053 RVA: 0x000A4300 File Offset: 0x000A2500
		[ConfigurationProperty("verificationCompatibility", DefaultValue = "0")]
		public int VerificationCompatibility
		{
			get
			{
				return (int)base[MonoSettingsSection.verificationCompatibilityProp];
			}
			set
			{
				base[MonoSettingsSection.verificationCompatibilityProp] = value;
			}
		}

		// Token: 0x170013C5 RID: 5061
		// (get) Token: 0x06003EB6 RID: 16054 RVA: 0x000A4313 File Offset: 0x000A2513
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return MonoSettingsSection.properties;
			}
		}

		// Token: 0x04002251 RID: 8785
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x04002252 RID: 8786
		private static ConfigurationProperty compilersCompatibilityProp = new ConfigurationProperty("compilersCompatibility", typeof(CompilerCollection), null, null, PropertyHelper.DefaultValidator, ConfigurationPropertyOptions.None);

		// Token: 0x04002253 RID: 8787
		private static ConfigurationProperty useCompilersCompatibilityProp = new ConfigurationProperty("useCompilersCompatibility", typeof(bool), true);

		// Token: 0x04002254 RID: 8788
		private static ConfigurationProperty verificationCompatibilityProp = new ConfigurationProperty("verificationCompatibility", typeof(int), 0);
	}
}
