using System;
using System.Configuration;

namespace System.Net.Configuration
{
	/// <summary>Represents the maximum number of connections to a remote computer. This class cannot be inherited.</summary>
	// Token: 0x02000763 RID: 1891
	public sealed class ConnectionManagementElement : ConfigurationElement
	{
		// Token: 0x06003B99 RID: 15257 RVA: 0x000CC578 File Offset: 0x000CA778
		static ConnectionManagementElement()
		{
			ConnectionManagementElement.properties.Add(ConnectionManagementElement.addressProp);
			ConnectionManagementElement.properties.Add(ConnectionManagementElement.maxConnectionProp);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Configuration.ConnectionManagementElement" /> class. </summary>
		// Token: 0x06003B9A RID: 15258 RVA: 0x0003121C File Offset: 0x0002F41C
		public ConnectionManagementElement()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Configuration.ConnectionManagementElement" /> class with the specified address and connection limit information.</summary>
		/// <param name="address">A string that identifies the address of a remote computer.</param>
		/// <param name="maxConnection">An integer that identifies the maximum number of connections allowed to <paramref name="address" /> from the local computer.</param>
		// Token: 0x06003B9B RID: 15259 RVA: 0x000CC5E8 File Offset: 0x000CA7E8
		public ConnectionManagementElement(string address, int maxConnection)
		{
			this.Address = address;
			this.MaxConnection = maxConnection;
		}

		/// <summary>Gets or sets the address for remote computers.</summary>
		/// <returns>A string that contains a regular expression describing an IP address or DNS name.</returns>
		// Token: 0x17000D78 RID: 3448
		// (get) Token: 0x06003B9C RID: 15260 RVA: 0x000CC5FE File Offset: 0x000CA7FE
		// (set) Token: 0x06003B9D RID: 15261 RVA: 0x000CC610 File Offset: 0x000CA810
		[ConfigurationProperty("address", Options = ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey)]
		public string Address
		{
			get
			{
				return (string)base[ConnectionManagementElement.addressProp];
			}
			set
			{
				base[ConnectionManagementElement.addressProp] = value;
			}
		}

		/// <summary>Gets or sets the maximum number of connections that can be made to a remote computer.</summary>
		/// <returns>An integer that specifies the maximum number of connections.</returns>
		// Token: 0x17000D79 RID: 3449
		// (get) Token: 0x06003B9E RID: 15262 RVA: 0x000CC61E File Offset: 0x000CA81E
		// (set) Token: 0x06003B9F RID: 15263 RVA: 0x000CC630 File Offset: 0x000CA830
		[ConfigurationProperty("maxconnection", DefaultValue = "6", Options = ConfigurationPropertyOptions.IsRequired)]
		public int MaxConnection
		{
			get
			{
				return (int)base[ConnectionManagementElement.maxConnectionProp];
			}
			set
			{
				base[ConnectionManagementElement.maxConnectionProp] = value;
			}
		}

		// Token: 0x17000D7A RID: 3450
		// (get) Token: 0x06003BA0 RID: 15264 RVA: 0x000CC643 File Offset: 0x000CA843
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return ConnectionManagementElement.properties;
			}
		}

		// Token: 0x040023A2 RID: 9122
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x040023A3 RID: 9123
		private static ConfigurationProperty addressProp = new ConfigurationProperty("address", typeof(string), null, ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey);

		// Token: 0x040023A4 RID: 9124
		private static ConfigurationProperty maxConnectionProp = new ConfigurationProperty("maxconnection", typeof(int), 1, ConfigurationPropertyOptions.IsRequired);
	}
}
