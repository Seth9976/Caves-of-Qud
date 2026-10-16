using System;
using System.Configuration;
using System.Net.Mail;

namespace System.Net.Configuration
{
	/// <summary>Represents the SMTP section in the System.Net configuration file.</summary>
	// Token: 0x0200077D RID: 1917
	public sealed class SmtpSection : ConfigurationSection
	{
		/// <summary>Gets or sets the Simple Mail Transport Protocol (SMTP) delivery method. The default delivery method is <see cref="F:System.Net.Mail.SmtpDeliveryMethod.Network" />.</summary>
		/// <returns>A string that represents the SMTP delivery method.</returns>
		// Token: 0x17000DC8 RID: 3528
		// (get) Token: 0x06003C5A RID: 15450 RVA: 0x000CE0D9 File Offset: 0x000CC2D9
		// (set) Token: 0x06003C5B RID: 15451 RVA: 0x000CE0EB File Offset: 0x000CC2EB
		[ConfigurationProperty("deliveryMethod", DefaultValue = "Network")]
		public SmtpDeliveryMethod DeliveryMethod
		{
			get
			{
				return (SmtpDeliveryMethod)base["deliveryMethod"];
			}
			set
			{
				base["deliveryMethod"] = value;
			}
		}

		/// <summary>Gets or sets the delivery format to use for sending outgoing e-mail using the Simple Mail Transport Protocol (SMTP).</summary>
		/// <returns>Returns <see cref="T:System.Net.Mail.SmtpDeliveryFormat" />.The delivery format to use for sending outgoing e-mail using SMTP.</returns>
		// Token: 0x17000DC9 RID: 3529
		// (get) Token: 0x06003C5C RID: 15452 RVA: 0x000CE0FE File Offset: 0x000CC2FE
		// (set) Token: 0x06003C5D RID: 15453 RVA: 0x000CE110 File Offset: 0x000CC310
		[ConfigurationProperty("deliveryFormat", DefaultValue = SmtpDeliveryFormat.SevenBit)]
		public SmtpDeliveryFormat DeliveryFormat
		{
			get
			{
				return (SmtpDeliveryFormat)base["deliveryFormat"];
			}
			set
			{
				base["deliveryFormat"] = value;
			}
		}

		/// <summary>Gets or sets the default value that indicates who the email message is from.</summary>
		/// <returns>A string that represents the default value indicating who a mail message is from.</returns>
		// Token: 0x17000DCA RID: 3530
		// (get) Token: 0x06003C5E RID: 15454 RVA: 0x000CE123 File Offset: 0x000CC323
		// (set) Token: 0x06003C5F RID: 15455 RVA: 0x000CE135 File Offset: 0x000CC335
		[ConfigurationProperty("from")]
		public string From
		{
			get
			{
				return (string)base["from"];
			}
			set
			{
				base["from"] = value;
			}
		}

		/// <summary>Gets the configuration element that controls the network settings used by the Simple Mail Transport Protocol (SMTP). file.<see cref="T:System.Net.Configuration.SmtpNetworkElement" />.</summary>
		/// <returns>A <see cref="T:System.Net.Configuration.SmtpNetworkElement" /> object.The configuration element that controls the network settings used by SMTP.</returns>
		// Token: 0x17000DCB RID: 3531
		// (get) Token: 0x06003C60 RID: 15456 RVA: 0x000CE143 File Offset: 0x000CC343
		[ConfigurationProperty("network")]
		public SmtpNetworkElement Network
		{
			get
			{
				return (SmtpNetworkElement)base["network"];
			}
		}

		/// <summary>Gets the pickup directory that will be used by the SMPT client.</summary>
		/// <returns>A <see cref="T:System.Net.Configuration.SmtpSpecifiedPickupDirectoryElement" /> object that specifies the pickup directory folder.</returns>
		// Token: 0x17000DCC RID: 3532
		// (get) Token: 0x06003C61 RID: 15457 RVA: 0x000CE155 File Offset: 0x000CC355
		[ConfigurationProperty("specifiedPickupDirectory")]
		public SmtpSpecifiedPickupDirectoryElement SpecifiedPickupDirectory
		{
			get
			{
				return (SmtpSpecifiedPickupDirectoryElement)base["specifiedPickupDirectory"];
			}
		}

		// Token: 0x17000DCD RID: 3533
		// (get) Token: 0x06003C62 RID: 15458 RVA: 0x0003176B File Offset: 0x0002F96B
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return base.Properties;
			}
		}
	}
}
