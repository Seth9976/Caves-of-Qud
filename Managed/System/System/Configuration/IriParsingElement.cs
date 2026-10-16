using System;

namespace System.Configuration
{
	/// <summary>Provides the configuration setting for International Resource Identifier (IRI) processing in the <see cref="T:System.Uri" /> class.</summary>
	// Token: 0x020001BA RID: 442
	public sealed class IriParsingElement : ConfigurationElement
	{
		// Token: 0x06000B99 RID: 2969 RVA: 0x00031282 File Offset: 0x0002F482
		static IriParsingElement()
		{
			IriParsingElement.properties.Add(IriParsingElement.enabled_prop);
		}

		/// <summary>Gets or sets the value of the <see cref="T:System.Configuration.IriParsingElement" /> configuration setting.</summary>
		/// <returns>A Boolean that indicates if International Resource Identifier (IRI) processing is enabled. </returns>
		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x06000B9B RID: 2971 RVA: 0x000312BD File Offset: 0x0002F4BD
		// (set) Token: 0x06000B9C RID: 2972 RVA: 0x000312CF File Offset: 0x0002F4CF
		[ConfigurationProperty("enabled", DefaultValue = false, Options = ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey)]
		public bool Enabled
		{
			get
			{
				return (bool)base[IriParsingElement.enabled_prop];
			}
			set
			{
				base[IriParsingElement.enabled_prop] = value;
			}
		}

		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x06000B9D RID: 2973 RVA: 0x000312E2 File Offset: 0x0002F4E2
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return IriParsingElement.properties;
			}
		}

		// Token: 0x06000B9E RID: 2974 RVA: 0x000312EC File Offset: 0x0002F4EC
		public override bool Equals(object o)
		{
			IriParsingElement iriParsingElement = o as IriParsingElement;
			return iriParsingElement != null && iriParsingElement.Enabled == this.Enabled;
		}

		// Token: 0x06000B9F RID: 2975 RVA: 0x00031313 File Offset: 0x0002F513
		public override int GetHashCode()
		{
			return Convert.ToInt32(this.Enabled) ^ 127;
		}

		// Token: 0x040007A5 RID: 1957
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x040007A6 RID: 1958
		private static ConfigurationProperty enabled_prop = new ConfigurationProperty("enabled", typeof(bool), false, ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey);
	}
}
