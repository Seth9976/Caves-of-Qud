using System;
using System.Configuration;

namespace System.Net.Configuration
{
	/// <summary>Represents the address information for resources that are not retrieved using a proxy server. This class cannot be inherited.</summary>
	// Token: 0x02000761 RID: 1889
	public sealed class BypassElement : ConfigurationElement
	{
		// Token: 0x06003B85 RID: 15237 RVA: 0x000CC4D7 File Offset: 0x000CA6D7
		static BypassElement()
		{
			BypassElement.properties.Add(BypassElement.addressProp);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Configuration.BypassElement" /> class. </summary>
		// Token: 0x06003B86 RID: 15238 RVA: 0x0003121C File Offset: 0x0002F41C
		public BypassElement()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Configuration.BypassElement" /> class with the specified type information.</summary>
		/// <param name="address">A string that identifies the address of a resource.</param>
		// Token: 0x06003B87 RID: 15239 RVA: 0x000CC50D File Offset: 0x000CA70D
		public BypassElement(string address)
		{
			this.Address = address;
		}

		/// <summary>Gets or sets the addresses of resources that bypass the proxy server.</summary>
		/// <returns>A string that identifies a resource.</returns>
		// Token: 0x17000D73 RID: 3443
		// (get) Token: 0x06003B88 RID: 15240 RVA: 0x000CC51C File Offset: 0x000CA71C
		// (set) Token: 0x06003B89 RID: 15241 RVA: 0x000CC52E File Offset: 0x000CA72E
		[ConfigurationProperty("address", Options = ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey)]
		public string Address
		{
			get
			{
				return (string)base[BypassElement.addressProp];
			}
			set
			{
				base[BypassElement.addressProp] = value;
			}
		}

		// Token: 0x17000D74 RID: 3444
		// (get) Token: 0x06003B8A RID: 15242 RVA: 0x000CC53C File Offset: 0x000CA73C
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return BypassElement.properties;
			}
		}

		// Token: 0x040023A0 RID: 9120
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x040023A1 RID: 9121
		private static ConfigurationProperty addressProp = new ConfigurationProperty("address", typeof(string), null, ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey);
	}
}
