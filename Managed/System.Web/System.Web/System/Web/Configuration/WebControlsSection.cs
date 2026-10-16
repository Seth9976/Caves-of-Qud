using System;
using System.Collections;
using System.ComponentModel;
using System.Configuration;

namespace System.Web.Configuration
{
	/// <summary>Configures the webControls section. This class cannot be inherited. </summary>
	// Token: 0x020005EF RID: 1519
	public sealed class WebControlsSection : ConfigurationSection
	{
		// Token: 0x060041FE RID: 16894 RVA: 0x000AA9A4 File Offset: 0x000A8BA4
		static WebControlsSection()
		{
			WebControlsSection.properties.Add(WebControlsSection.clientScriptsLocationProp);
		}

		// Token: 0x060041FF RID: 16895 RVA: 0x000AA9FD File Offset: 0x000A8BFD
		protected internal override object GetRuntimeObject()
		{
			return new Hashtable { { "clientScriptsLocation", this.ClientScriptsLocation } };
		}

		/// <summary>Gets the client scripts location.</summary>
		/// <returns>The location of the client scripts.</returns>
		// Token: 0x17001503 RID: 5379
		// (get) Token: 0x06004200 RID: 16896 RVA: 0x000AAA15 File Offset: 0x000A8C15
		[ConfigurationProperty("clientScriptsLocation", DefaultValue = "/aspnet_client/{0}/{1}/", Options = ConfigurationPropertyOptions.IsRequired)]
		[StringValidator(MinLength = 1)]
		public string ClientScriptsLocation
		{
			get
			{
				return (string)base[WebControlsSection.clientScriptsLocationProp];
			}
		}

		// Token: 0x17001504 RID: 5380
		// (get) Token: 0x06004201 RID: 16897 RVA: 0x000AAA27 File Offset: 0x000A8C27
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return WebControlsSection.properties;
			}
		}

		// Token: 0x04002364 RID: 9060
		private static ConfigurationProperty clientScriptsLocationProp = new ConfigurationProperty("clientScriptsLocation", typeof(string), "/aspnet_client/{0}/{1}/", TypeDescriptor.GetConverter(typeof(string)), PropertyHelper.NonEmptyStringValidator, ConfigurationPropertyOptions.IsRequired);

		// Token: 0x04002365 RID: 9061
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();
	}
}
