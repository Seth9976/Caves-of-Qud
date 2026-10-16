using System;

namespace System.Configuration
{
	/// <summary>Provides the configuration setting for International Domain Name (IDN) processing in the <see cref="T:System.Uri" /> class.</summary>
	// Token: 0x020001B8 RID: 440
	public sealed class IdnElement : ConfigurationElement
	{
		// Token: 0x06000B90 RID: 2960 RVA: 0x000311E1 File Offset: 0x0002F3E1
		static IdnElement()
		{
			IdnElement.properties.Add(IdnElement.enabled_prop);
		}

		/// <summary>Gets or sets the value of the <see cref="T:System.Configuration.IdnElement" /> configuration setting. </summary>
		/// <returns>A <see cref="T:System.UriIdnScope" /> that contains the current configuration setting for IDN processing.</returns>
		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x06000B92 RID: 2962 RVA: 0x00031224 File Offset: 0x0002F424
		// (set) Token: 0x06000B93 RID: 2963 RVA: 0x00031236 File Offset: 0x0002F436
		[ConfigurationProperty("enabled", DefaultValue = UriIdnScope.None, Options = ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey)]
		public UriIdnScope Enabled
		{
			get
			{
				return (UriIdnScope)base[IdnElement.enabled_prop];
			}
			set
			{
				base[IdnElement.enabled_prop] = value;
			}
		}

		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x06000B94 RID: 2964 RVA: 0x00031249 File Offset: 0x0002F449
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return IdnElement.properties;
			}
		}

		// Token: 0x06000B95 RID: 2965 RVA: 0x00031250 File Offset: 0x0002F450
		public override bool Equals(object o)
		{
			IdnElement idnElement = o as IdnElement;
			return idnElement != null && idnElement.Enabled == this.Enabled;
		}

		// Token: 0x06000B96 RID: 2966 RVA: 0x00031277 File Offset: 0x0002F477
		public override int GetHashCode()
		{
			return (int)(this.Enabled ^ (UriIdnScope)127);
		}

		// Token: 0x040007A2 RID: 1954
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x040007A3 RID: 1955
		private static ConfigurationProperty enabled_prop = new ConfigurationProperty("enabled", typeof(UriIdnScope), UriIdnScope.None, ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey);

		// Token: 0x040007A4 RID: 1956
		internal const UriIdnScope EnabledDefaultValue = UriIdnScope.None;
	}
}
