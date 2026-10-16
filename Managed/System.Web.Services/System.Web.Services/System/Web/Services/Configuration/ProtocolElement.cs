using System;
using System.Configuration;

namespace System.Web.Services.Configuration
{
	/// <summary>Represents a protocol element in the Web Services configuration file. The class cannot be inherited.</summary>
	// Token: 0x0200013F RID: 319
	public sealed class ProtocolElement : ConfigurationElement
	{
		/// <summary>Creates an instance of this class.</summary>
		// Token: 0x060009B5 RID: 2485 RVA: 0x000435E4 File Offset: 0x000417E4
		public ProtocolElement()
		{
			this.properties.Add(this.name);
		}

		/// <summary>Creates an instance of this class, and initializes the <see cref="P:System.Web.Services.Configuration.ProtocolElement.Name" /> property.</summary>
		/// <param name="protocol">The value to initialize <see cref="P:System.Web.Services.Configuration.ProtocolElement.Name" />.</param>
		// Token: 0x060009B6 RID: 2486 RVA: 0x00043634 File Offset: 0x00041834
		public ProtocolElement(WebServiceProtocols protocol)
			: this()
		{
			this.Name = protocol;
		}

		/// <summary>Gets or sets the protocol name.</summary>
		/// <returns>A <see cref="T:System.Web.Services.Configuration.WebServiceProtocols" /> object that represents the protocol name.</returns>
		// Token: 0x1700027B RID: 635
		// (get) Token: 0x060009B7 RID: 2487 RVA: 0x00043643 File Offset: 0x00041843
		// (set) Token: 0x060009B8 RID: 2488 RVA: 0x00043656 File Offset: 0x00041856
		[ConfigurationProperty("name", IsKey = true, DefaultValue = WebServiceProtocols.Unknown)]
		public WebServiceProtocols Name
		{
			get
			{
				return (WebServiceProtocols)base[this.name];
			}
			set
			{
				if (!this.IsValidProtocolsValue(value))
				{
					value = WebServiceProtocols.Unknown;
				}
				base[this.name] = value;
			}
		}

		// Token: 0x1700027C RID: 636
		// (get) Token: 0x060009B9 RID: 2489 RVA: 0x00043676 File Offset: 0x00041876
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return this.properties;
			}
		}

		// Token: 0x060009BA RID: 2490 RVA: 0x0004367E File Offset: 0x0004187E
		private bool IsValidProtocolsValue(WebServiceProtocols value)
		{
			return Enum.IsDefined(typeof(WebServiceProtocols), value);
		}

		// Token: 0x0400059D RID: 1437
		private ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x0400059E RID: 1438
		private readonly ConfigurationProperty name = new ConfigurationProperty("name", typeof(WebServiceProtocols), WebServiceProtocols.Unknown, ConfigurationPropertyOptions.IsKey);
	}
}
