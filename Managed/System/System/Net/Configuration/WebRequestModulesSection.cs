using System;
using System.Configuration;

namespace System.Net.Configuration
{
	/// <summary>Represents the configuration section for Web request modules. This class cannot be inherited.</summary>
	// Token: 0x02000784 RID: 1924
	public sealed class WebRequestModulesSection : ConfigurationSection
	{
		// Token: 0x06003C92 RID: 15506 RVA: 0x000CE5C0 File Offset: 0x000CC7C0
		static WebRequestModulesSection()
		{
			WebRequestModulesSection.properties.Add(WebRequestModulesSection.webRequestModulesProp);
		}

		// Token: 0x17000DDC RID: 3548
		// (get) Token: 0x06003C93 RID: 15507 RVA: 0x000CE5F6 File Offset: 0x000CC7F6
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return WebRequestModulesSection.properties;
			}
		}

		/// <summary>Gets the collection of Web request modules in the section.</summary>
		/// <returns>A <see cref="T:System.Net.Configuration.WebRequestModuleElementCollection" /> containing the registered Web request modules. </returns>
		// Token: 0x17000DDD RID: 3549
		// (get) Token: 0x06003C94 RID: 15508 RVA: 0x000CE5FD File Offset: 0x000CC7FD
		[ConfigurationProperty("", Options = ConfigurationPropertyOptions.IsDefaultCollection)]
		public WebRequestModuleElementCollection WebRequestModules
		{
			get
			{
				return (WebRequestModuleElementCollection)base[WebRequestModulesSection.webRequestModulesProp];
			}
		}

		// Token: 0x06003C95 RID: 15509 RVA: 0x00003917 File Offset: 0x00001B17
		[MonoTODO]
		protected override void PostDeserialize()
		{
		}

		// Token: 0x06003C96 RID: 15510 RVA: 0x00003917 File Offset: 0x00001B17
		[MonoTODO]
		protected override void InitializeDefault()
		{
		}

		// Token: 0x040023F2 RID: 9202
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x040023F3 RID: 9203
		private static ConfigurationProperty webRequestModulesProp = new ConfigurationProperty("", typeof(WebRequestModuleElementCollection), null, ConfigurationPropertyOptions.IsDefaultCollection);
	}
}
