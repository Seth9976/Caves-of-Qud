using System;
using System.Configuration;

namespace System.Security.Authentication.ExtendedProtection.Configuration
{
	/// <summary>The <see cref="T:System.Security.Authentication.ExtendedProtection.Configuration.ServiceNameElement" /> class represents a configuration element for a service name used in a <see cref="T:System.Security.Authentication.ExtendedProtection.Configuration.ServiceNameElementCollection" />.</summary>
	// Token: 0x020002AF RID: 687
	public sealed class ServiceNameElement : ConfigurationElement
	{
		// Token: 0x06001552 RID: 5458 RVA: 0x00055C53 File Offset: 0x00053E53
		static ServiceNameElement()
		{
			ServiceNameElement.properties.Add(ServiceNameElement.name);
		}

		/// <summary>Gets or sets the Service Provider Name (SPN) for this <see cref="T:System.Security.Authentication.ExtendedProtection.Configuration.ServiceNameElement" /> instance.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the representation of SPN for this <see cref="T:System.Security.Authentication.ExtendedProtection.Configuration.ServiceNameElement" /> instance.</returns>
		// Token: 0x17000405 RID: 1029
		// (get) Token: 0x06001553 RID: 5459 RVA: 0x00055C87 File Offset: 0x00053E87
		// (set) Token: 0x06001554 RID: 5460 RVA: 0x00055C99 File Offset: 0x00053E99
		[ConfigurationProperty("name")]
		public string Name
		{
			get
			{
				return (string)base[ServiceNameElement.name];
			}
			set
			{
				base[ServiceNameElement.name] = value;
			}
		}

		// Token: 0x17000406 RID: 1030
		// (get) Token: 0x06001555 RID: 5461 RVA: 0x00055CA7 File Offset: 0x00053EA7
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return ServiceNameElement.properties;
			}
		}

		// Token: 0x04000C26 RID: 3110
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x04000C27 RID: 3111
		private static ConfigurationProperty name = ConfigUtil.BuildProperty(typeof(ServiceNameElement), "Name");
	}
}
