using System;
using System.Configuration;

namespace System.Net.Configuration
{
	/// <summary>Represents the configuration section for connection management. This class cannot be inherited.</summary>
	// Token: 0x02000768 RID: 1896
	public sealed class ConnectionManagementSection : ConfigurationSection
	{
		// Token: 0x06003BBB RID: 15291 RVA: 0x000CCA16 File Offset: 0x000CAC16
		static ConnectionManagementSection()
		{
			ConnectionManagementSection.properties.Add(ConnectionManagementSection.connectionManagementProp);
		}

		/// <summary>Gets the collection of connection management objects in the section.</summary>
		/// <returns>A <see cref="T:System.Net.Configuration.ConnectionManagementElementCollection" /> that contains the connection management information for the local computer. </returns>
		// Token: 0x17000D7E RID: 3454
		// (get) Token: 0x06003BBD RID: 15293 RVA: 0x000CCA4C File Offset: 0x000CAC4C
		[ConfigurationProperty("", Options = ConfigurationPropertyOptions.IsDefaultCollection)]
		public ConnectionManagementElementCollection ConnectionManagement
		{
			get
			{
				return (ConnectionManagementElementCollection)base[ConnectionManagementSection.connectionManagementProp];
			}
		}

		// Token: 0x17000D7F RID: 3455
		// (get) Token: 0x06003BBE RID: 15294 RVA: 0x000CCA5E File Offset: 0x000CAC5E
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return ConnectionManagementSection.properties;
			}
		}

		// Token: 0x040023A7 RID: 9127
		private static ConfigurationProperty connectionManagementProp = new ConfigurationProperty("ConnectionManagement", typeof(ConnectionManagementElementCollection), null, ConfigurationPropertyOptions.IsDefaultCollection);

		// Token: 0x040023A8 RID: 9128
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();
	}
}
