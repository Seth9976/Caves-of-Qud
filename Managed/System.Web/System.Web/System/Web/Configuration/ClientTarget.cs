using System;
using System.ComponentModel;
using System.Configuration;

namespace System.Web.Configuration
{
	/// <summary>Defines the alias associated with the target user agent for which ASP.NET server controls should render content. This class cannot be inherited.</summary>
	// Token: 0x02000589 RID: 1417
	public sealed class ClientTarget : ConfigurationElement
	{
		// Token: 0x06003BDB RID: 15323 RVA: 0x0009DF18 File Offset: 0x0009C118
		static ClientTarget()
		{
			ClientTarget.properties.Add(ClientTarget.aliasProp);
			ClientTarget.properties.Add(ClientTarget.userAgentProp);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Configuration.ClientTarget" /> class.</summary>
		/// <param name="alias">The name used to refer to a specific user agent.</param>
		/// <param name="userAgent">The user agent's identification name.</param>
		// Token: 0x06003BDC RID: 15324 RVA: 0x0009DFAB File Offset: 0x0009C1AB
		public ClientTarget(string alias, string userAgent)
		{
			this.Alias = alias;
			this.UserAgent = userAgent;
		}

		/// <summary>Gets the user agent's alias.</summary>
		/// <returns>The name used to refer to a specific user agent. </returns>
		// Token: 0x17001271 RID: 4721
		// (get) Token: 0x06003BDD RID: 15325 RVA: 0x0009DFC1 File Offset: 0x0009C1C1
		// (set) Token: 0x06003BDE RID: 15326 RVA: 0x0009DFD3 File Offset: 0x0009C1D3
		[StringValidator(MinLength = 1)]
		[ConfigurationProperty("alias", Options = ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey)]
		public string Alias
		{
			get
			{
				return (string)base[ClientTarget.aliasProp];
			}
			internal set
			{
				base[ClientTarget.aliasProp] = value;
			}
		}

		/// <summary>Gets the user agent's identification name.</summary>
		/// <returns>The user agent's identification name.</returns>
		// Token: 0x17001272 RID: 4722
		// (get) Token: 0x06003BDF RID: 15327 RVA: 0x0009DFE1 File Offset: 0x0009C1E1
		// (set) Token: 0x06003BE0 RID: 15328 RVA: 0x0009DFF3 File Offset: 0x0009C1F3
		[StringValidator(MinLength = 1)]
		[ConfigurationProperty("userAgent", Options = ConfigurationPropertyOptions.IsRequired)]
		public string UserAgent
		{
			get
			{
				return (string)base[ClientTarget.userAgentProp];
			}
			internal set
			{
				base[ClientTarget.userAgentProp] = value;
			}
		}

		// Token: 0x17001273 RID: 4723
		// (get) Token: 0x06003BE1 RID: 15329 RVA: 0x0009E001 File Offset: 0x0009C201
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return ClientTarget.properties;
			}
		}

		// Token: 0x040020A3 RID: 8355
		private static ConfigurationProperty aliasProp = new ConfigurationProperty("alias", typeof(string), null, TypeDescriptor.GetConverter(typeof(string)), PropertyHelper.NonEmptyStringValidator, ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey);

		// Token: 0x040020A4 RID: 8356
		private static ConfigurationProperty userAgentProp = new ConfigurationProperty("userAgent", typeof(string), null, TypeDescriptor.GetConverter(typeof(string)), PropertyHelper.NonEmptyStringValidator, ConfigurationPropertyOptions.IsRequired);

		// Token: 0x040020A5 RID: 8357
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();
	}
}
