using System;
using System.Configuration;

namespace System.Web.Configuration
{
	/// <summary>Provides programmatic access to the webParts configuration file section. This class cannot be inherited.</summary>
	// Token: 0x020005F2 RID: 1522
	public sealed class WebPartsSection : ConfigurationSection
	{
		// Token: 0x0600420E RID: 16910 RVA: 0x000AAB8C File Offset: 0x000A8D8C
		static WebPartsSection()
		{
			WebPartsSection.properties.Add(WebPartsSection.enableExportProp);
			WebPartsSection.properties.Add(WebPartsSection.personalizationProp);
			WebPartsSection.properties.Add(WebPartsSection.transformersProp);
		}

		// Token: 0x0600420F RID: 16911 RVA: 0x00002058 File Offset: 0x00000258
		[global::System.MonoTODO("why override this?")]
		protected internal override object GetRuntimeObject()
		{
			return this;
		}

		/// <summary>Gets or sets a value indicating whether to enable the export of control data to an XML description file.</summary>
		/// <returns>true to enable the export of control data to an XML description file; otherwise, false.</returns>
		// Token: 0x1700150B RID: 5387
		// (get) Token: 0x06004210 RID: 16912 RVA: 0x000AAC31 File Offset: 0x000A8E31
		// (set) Token: 0x06004211 RID: 16913 RVA: 0x000AAC43 File Offset: 0x000A8E43
		[ConfigurationProperty("enableExport", DefaultValue = "False")]
		public bool EnableExport
		{
			get
			{
				return (bool)base[WebPartsSection.enableExportProp];
			}
			set
			{
				base[WebPartsSection.enableExportProp] = value;
			}
		}

		/// <summary>Gets a <see cref="T:System.Web.Configuration.WebPartsPersonalization" /> object that allows you to specify the Web Parts personalization provider and set Web Parts personalization authorizations.</summary>
		/// <returns>A <see cref="T:System.Web.Configuration.WebPartsPersonalization" /> object that allows you to specify the personalization provider and set personalization authorizations.</returns>
		// Token: 0x1700150C RID: 5388
		// (get) Token: 0x06004212 RID: 16914 RVA: 0x000AAC56 File Offset: 0x000A8E56
		[ConfigurationProperty("personalization")]
		public WebPartsPersonalization Personalization
		{
			get
			{
				return (WebPartsPersonalization)base[WebPartsSection.personalizationProp];
			}
		}

		/// <summary>Gets a collection of <see cref="T:System.Web.Configuration.TransformerInfo" /> objects.</summary>
		/// <returns>A <see cref="T:System.Web.Configuration.TransformerInfoCollection" /> collection of <see cref="T:System.Web.Configuration.TransformerInfo" /> objects.</returns>
		// Token: 0x1700150D RID: 5389
		// (get) Token: 0x06004213 RID: 16915 RVA: 0x000AAC68 File Offset: 0x000A8E68
		[ConfigurationProperty("transformers")]
		public TransformerInfoCollection Transformers
		{
			get
			{
				return (TransformerInfoCollection)base[WebPartsSection.transformersProp];
			}
		}

		// Token: 0x1700150E RID: 5390
		// (get) Token: 0x06004214 RID: 16916 RVA: 0x000AAC7A File Offset: 0x000A8E7A
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return WebPartsSection.properties;
			}
		}

		// Token: 0x0400236C RID: 9068
		private static ConfigurationProperty enableExportProp = new ConfigurationProperty("enableExport", typeof(bool), false);

		// Token: 0x0400236D RID: 9069
		private static ConfigurationProperty personalizationProp = new ConfigurationProperty("personalization", typeof(WebPartsPersonalization), null, null, PropertyHelper.DefaultValidator, ConfigurationPropertyOptions.None);

		// Token: 0x0400236E RID: 9070
		private static ConfigurationProperty transformersProp = new ConfigurationProperty("transformers", typeof(TransformerInfoCollection), null, null, PropertyHelper.DefaultValidator, ConfigurationPropertyOptions.None);

		// Token: 0x0400236F RID: 9071
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();
	}
}
