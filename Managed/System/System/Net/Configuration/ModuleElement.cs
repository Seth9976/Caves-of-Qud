using System;
using System.Configuration;

namespace System.Net.Configuration
{
	/// <summary>Represents the type information for a custom <see cref="T:System.Net.IWebProxy" /> module. This class cannot be inherited.</summary>
	// Token: 0x02000770 RID: 1904
	public sealed class ModuleElement : ConfigurationElement
	{
		// Token: 0x06003BF5 RID: 15349 RVA: 0x000CD3CD File Offset: 0x000CB5CD
		static ModuleElement()
		{
			ModuleElement.properties.Add(ModuleElement.typeProp);
		}

		// Token: 0x17000D95 RID: 3477
		// (get) Token: 0x06003BF7 RID: 15351 RVA: 0x000CD402 File Offset: 0x000CB602
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return ModuleElement.properties;
			}
		}

		/// <summary>Gets or sets the type and assembly information for the current instance.</summary>
		/// <returns>A string that identifies a type that implements the <see cref="T:System.Net.IWebProxy" /> interface or null if no value has been specified.</returns>
		// Token: 0x17000D96 RID: 3478
		// (get) Token: 0x06003BF8 RID: 15352 RVA: 0x000CD409 File Offset: 0x000CB609
		// (set) Token: 0x06003BF9 RID: 15353 RVA: 0x000CD41B File Offset: 0x000CB61B
		[ConfigurationProperty("type")]
		public string Type
		{
			get
			{
				return (string)base[ModuleElement.typeProp];
			}
			set
			{
				base[ModuleElement.typeProp] = value;
			}
		}

		// Token: 0x040023BD RID: 9149
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x040023BE RID: 9150
		private static ConfigurationProperty typeProp = new ConfigurationProperty("type", typeof(string), null);
	}
}
