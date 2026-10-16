using System;
using System.Configuration;

namespace System.Web.Configuration
{
	/// <summary>Configures the clientTarget section. This class cannot be inherited.</summary>
	// Token: 0x0200058B RID: 1419
	public sealed class ClientTargetSection : ConfigurationSection
	{
		// Token: 0x06003BF1 RID: 15345 RVA: 0x0009E0A5 File Offset: 0x0009C2A5
		static ClientTargetSection()
		{
			ClientTargetSection.properties.Add(ClientTargetSection.clientTargetsProp);
		}

		/// <summary>Gets the collection of <see cref="T:System.Web.Configuration.ClientTarget" /> objects.</summary>
		/// <returns>A <see cref="T:System.Web.Configuration.ClientTargetCollection" /> that contains the <see cref="T:System.Web.Configuration.ClientTarget" /> objects.</returns>
		// Token: 0x17001278 RID: 4728
		// (get) Token: 0x06003BF2 RID: 15346 RVA: 0x0009E0D7 File Offset: 0x0009C2D7
		[ConfigurationProperty("", Options = ConfigurationPropertyOptions.IsDefaultCollection | ConfigurationPropertyOptions.IsRequired)]
		public ClientTargetCollection ClientTargets
		{
			get
			{
				return (ClientTargetCollection)base[ClientTargetSection.clientTargetsProp];
			}
		}

		// Token: 0x17001279 RID: 4729
		// (get) Token: 0x06003BF3 RID: 15347 RVA: 0x0009E0E9 File Offset: 0x0009C2E9
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return ClientTargetSection.properties;
			}
		}

		// Token: 0x040020A7 RID: 8359
		private static ConfigurationProperty clientTargetsProp = new ConfigurationProperty(null, typeof(ClientTargetCollection), null, ConfigurationPropertyOptions.IsDefaultCollection | ConfigurationPropertyOptions.IsRequired);

		// Token: 0x040020A8 RID: 8360
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();
	}
}
