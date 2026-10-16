using System;
using Unity;

namespace System.Configuration
{
	/// <summary>Represents the Uri section within a configuration file.</summary>
	// Token: 0x020001DF RID: 479
	public sealed class UriSection : ConfigurationSection
	{
		// Token: 0x06000C64 RID: 3172 RVA: 0x000327D4 File Offset: 0x000309D4
		static UriSection()
		{
			UriSection.properties.Add(UriSection.idn_prop);
			UriSection.properties.Add(UriSection.iriParsing_prop);
		}

		/// <summary>Gets an <see cref="T:System.Configuration.IdnElement" /> object that contains the configuration setting for International Domain Name (IDN) processing in the <see cref="T:System.Uri" /> class.</summary>
		/// <returns>The configuration setting for International Domain Name (IDN) processing in the <see cref="T:System.Uri" /> class.</returns>
		// Token: 0x17000222 RID: 546
		// (get) Token: 0x06000C66 RID: 3174 RVA: 0x0003283D File Offset: 0x00030A3D
		[ConfigurationProperty("idn")]
		public IdnElement Idn
		{
			get
			{
				return (IdnElement)base[UriSection.idn_prop];
			}
		}

		/// <summary>Gets an <see cref="T:System.Configuration.IriParsingElement" /> object that contains the configuration setting for International Resource Identifiers (IRI) parsing in the <see cref="T:System.Uri" /> class.</summary>
		/// <returns>The configuration setting for International Resource Identifiers (IRI) parsing in the <see cref="T:System.Uri" /> class.</returns>
		// Token: 0x17000223 RID: 547
		// (get) Token: 0x06000C67 RID: 3175 RVA: 0x0003284F File Offset: 0x00030A4F
		[ConfigurationProperty("iriParsing")]
		public IriParsingElement IriParsing
		{
			get
			{
				return (IriParsingElement)base[UriSection.iriParsing_prop];
			}
		}

		// Token: 0x17000224 RID: 548
		// (get) Token: 0x06000C68 RID: 3176 RVA: 0x00032861 File Offset: 0x00030A61
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return UriSection.properties;
			}
		}

		/// <summary>Gets a <see cref="T:System.Configuration.SchemeSettingElementCollection" /> object that contains the configuration settings for scheme parsing in the <see cref="T:System.Uri" /> class.</summary>
		/// <returns>The configuration settings for scheme parsing in the <see cref="T:System.Uri" /> class</returns>
		// Token: 0x17000225 RID: 549
		// (get) Token: 0x06000C69 RID: 3177 RVA: 0x00032868 File Offset: 0x00030A68
		public SchemeSettingElementCollection SchemeSettings
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		// Token: 0x040007DF RID: 2015
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x040007E0 RID: 2016
		private static ConfigurationProperty idn_prop = new ConfigurationProperty("idn", typeof(IdnElement), null);

		// Token: 0x040007E1 RID: 2017
		private static ConfigurationProperty iriParsing_prop = new ConfigurationProperty("iriParsing", typeof(IriParsingElement), null);
	}
}
