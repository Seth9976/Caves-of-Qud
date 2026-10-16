using System;
using System.Configuration;

namespace System.Net.Configuration
{
	/// <summary>Represents an SMTP pickup directory configuration element.</summary>
	// Token: 0x0200077E RID: 1918
	public sealed class SmtpSpecifiedPickupDirectoryElement : ConfigurationElement
	{
		// Token: 0x06003C64 RID: 15460 RVA: 0x000CE167 File Offset: 0x000CC367
		static SmtpSpecifiedPickupDirectoryElement()
		{
			SmtpSpecifiedPickupDirectoryElement.properties.Add(SmtpSpecifiedPickupDirectoryElement.pickupDirectoryLocationProp);
		}

		/// <summary>Gets or sets the folder where applications save mail messages to be processed by the SMTP server.</summary>
		/// <returns>A string that specifies the pickup directory for e-mail messages.</returns>
		// Token: 0x17000DCE RID: 3534
		// (get) Token: 0x06003C65 RID: 15461 RVA: 0x000CE19B File Offset: 0x000CC39B
		// (set) Token: 0x06003C66 RID: 15462 RVA: 0x000CE1AD File Offset: 0x000CC3AD
		[ConfigurationProperty("pickupDirectoryLocation")]
		public string PickupDirectoryLocation
		{
			get
			{
				return (string)base[SmtpSpecifiedPickupDirectoryElement.pickupDirectoryLocationProp];
			}
			set
			{
				base[SmtpSpecifiedPickupDirectoryElement.pickupDirectoryLocationProp] = value;
			}
		}

		// Token: 0x17000DCF RID: 3535
		// (get) Token: 0x06003C67 RID: 15463 RVA: 0x000CE1BB File Offset: 0x000CC3BB
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return SmtpSpecifiedPickupDirectoryElement.properties;
			}
		}

		// Token: 0x040023E8 RID: 9192
		private static ConfigurationProperty pickupDirectoryLocationProp = new ConfigurationProperty("pickupDirectoryLocation", typeof(string));

		// Token: 0x040023E9 RID: 9193
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();
	}
}
