using System;
using System.Configuration;

namespace System.Net.Configuration
{
	/// <summary>Determines whether Internet Protocol version 6 is enabled on the local computer. This class cannot be inherited.</summary>
	// Token: 0x0200076E RID: 1902
	public sealed class Ipv6Element : ConfigurationElement
	{
		// Token: 0x06003BEE RID: 15342 RVA: 0x000CD350 File Offset: 0x000CB550
		static Ipv6Element()
		{
			Ipv6Element.properties.Add(Ipv6Element.enabledProp);
		}

		/// <summary>Gets or sets a Boolean value that indicates whether Internet Protocol version 6 is enabled on the local computer.</summary>
		/// <returns>true if IPv6 is enabled; otherwise, false.</returns>
		// Token: 0x17000D92 RID: 3474
		// (get) Token: 0x06003BF0 RID: 15344 RVA: 0x000CD38A File Offset: 0x000CB58A
		// (set) Token: 0x06003BF1 RID: 15345 RVA: 0x000CD39C File Offset: 0x000CB59C
		[ConfigurationProperty("enabled", DefaultValue = "False")]
		public bool Enabled
		{
			get
			{
				return (bool)base[Ipv6Element.enabledProp];
			}
			set
			{
				base[Ipv6Element.enabledProp] = value;
			}
		}

		// Token: 0x17000D93 RID: 3475
		// (get) Token: 0x06003BF2 RID: 15346 RVA: 0x000CD3AF File Offset: 0x000CB5AF
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return Ipv6Element.properties;
			}
		}

		// Token: 0x040023BB RID: 9147
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x040023BC RID: 9148
		private static ConfigurationProperty enabledProp = new ConfigurationProperty("enabled", typeof(bool), false);
	}
}
