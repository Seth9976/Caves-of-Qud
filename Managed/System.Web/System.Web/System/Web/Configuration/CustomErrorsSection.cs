using System;
using System.Configuration;
using System.Xml;
using Unity;

namespace System.Web.Configuration
{
	/// <summary>Configures the ASP.NET custom errors. This class cannot be inherited.</summary>
	// Token: 0x02000593 RID: 1427
	public sealed class CustomErrorsSection : ConfigurationSection
	{
		// Token: 0x06003C70 RID: 15472 RVA: 0x0009ED3C File Offset: 0x0009CF3C
		static CustomErrorsSection()
		{
			CustomErrorsSection.properties.Add(CustomErrorsSection.defaultRedirectProp);
			CustomErrorsSection.properties.Add(CustomErrorsSection.errorsProp);
			CustomErrorsSection.properties.Add(CustomErrorsSection.modeProp);
			CustomErrorsSection.properties.Add(CustomErrorsSection.redirectModeProp);
		}

		// Token: 0x06003C71 RID: 15473 RVA: 0x0009EE32 File Offset: 0x0009D032
		protected internal override void DeserializeSection(XmlReader reader)
		{
			base.DeserializeSection(reader);
		}

		// Token: 0x06003C72 RID: 15474 RVA: 0x0009CD66 File Offset: 0x0009AF66
		protected internal override void Reset(ConfigurationElement parentElement)
		{
			base.Reset(parentElement);
		}

		/// <summary>Gets or sets the default URL for redirection.</summary>
		/// <returns>The default URL to which the application is redirected when an error occurs.</returns>
		/// <exception cref="T:System.NullReferenceException">The <see cref="P:System.Web.Configuration.CustomErrorsSection.DefaultRedirect" /> property is null. This is the default.</exception>
		// Token: 0x170012AF RID: 4783
		// (get) Token: 0x06003C73 RID: 15475 RVA: 0x0009EE3B File Offset: 0x0009D03B
		// (set) Token: 0x06003C74 RID: 15476 RVA: 0x0009EE4D File Offset: 0x0009D04D
		[ConfigurationProperty("defaultRedirect")]
		public string DefaultRedirect
		{
			get
			{
				return (string)base[CustomErrorsSection.defaultRedirectProp];
			}
			set
			{
				base[CustomErrorsSection.defaultRedirectProp] = value;
			}
		}

		/// <summary>Gets the collection of the <see cref="T:System.Web.Configuration.CustomError" /> objects.</summary>
		/// <returns>A <see cref="T:System.Web.Configuration.CustomErrorCollection" /> that contains the custom errors.</returns>
		// Token: 0x170012B0 RID: 4784
		// (get) Token: 0x06003C75 RID: 15477 RVA: 0x0009EE5B File Offset: 0x0009D05B
		[ConfigurationProperty("", Options = ConfigurationPropertyOptions.IsDefaultCollection)]
		public CustomErrorCollection Errors
		{
			get
			{
				return (CustomErrorCollection)base[CustomErrorsSection.errorsProp];
			}
		}

		/// <summary>Gets or sets the error display modality.</summary>
		/// <returns>One of the <see cref="T:System.Web.Configuration.CustomErrorsMode" /> values. The default is <see cref="F:System.Web.Configuration.CustomErrorsMode.RemoteOnly" />.</returns>
		// Token: 0x170012B1 RID: 4785
		// (get) Token: 0x06003C76 RID: 15478 RVA: 0x0009EE6D File Offset: 0x0009D06D
		// (set) Token: 0x06003C77 RID: 15479 RVA: 0x0009EE7F File Offset: 0x0009D07F
		[ConfigurationProperty("mode", DefaultValue = "RemoteOnly")]
		public CustomErrorsMode Mode
		{
			get
			{
				return (CustomErrorsMode)base[CustomErrorsSection.modeProp];
			}
			set
			{
				base[CustomErrorsSection.modeProp] = value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether the URL of the request should be changed when the user is redirected to a custom error page.</summary>
		/// <returns>A value that indicates whether the URL is changed when the user is redirected to the custom error page. The default value is <see cref="F:System.Web.Configuration.CustomErrorsRedirectMode.ResponseRedirect" />.</returns>
		// Token: 0x170012B2 RID: 4786
		// (get) Token: 0x06003C78 RID: 15480 RVA: 0x0009EE92 File Offset: 0x0009D092
		// (set) Token: 0x06003C79 RID: 15481 RVA: 0x0009EEA4 File Offset: 0x0009D0A4
		[ConfigurationProperty("redirectMode", DefaultValue = CustomErrorsRedirectMode.ResponseRedirect)]
		public CustomErrorsRedirectMode RedirectMode
		{
			get
			{
				return (CustomErrorsRedirectMode)base[CustomErrorsSection.redirectModeProp];
			}
			set
			{
				base[CustomErrorsSection.redirectModeProp] = value;
			}
		}

		// Token: 0x170012B3 RID: 4787
		// (get) Token: 0x06003C7A RID: 15482 RVA: 0x0009EEB7 File Offset: 0x0009D0B7
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return CustomErrorsSection.properties;
			}
		}

		/// <summary>Gets or sets a value that indicates whether ASP.NET should display a generic error message when the custom error page cannot be displayed.</summary>
		/// <returns>true if a generic error message should be displayed when the custom error page cannot be displayed; otherwise, false. The default is false.</returns>
		// Token: 0x170012B4 RID: 4788
		// (get) Token: 0x06003C7C RID: 15484 RVA: 0x0009EEC0 File Offset: 0x0009D0C0
		// (set) Token: 0x06003C7D RID: 15485 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public bool AllowNestedErrors
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		// Token: 0x040020CB RID: 8395
		private static ConfigurationProperty defaultRedirectProp = new ConfigurationProperty("defaultRedirect", typeof(string), null);

		// Token: 0x040020CC RID: 8396
		private static ConfigurationProperty errorsProp = new ConfigurationProperty(string.Empty, typeof(CustomErrorCollection), null, null, PropertyHelper.DefaultValidator, ConfigurationPropertyOptions.IsDefaultCollection);

		// Token: 0x040020CD RID: 8397
		private static ConfigurationProperty modeProp = new ConfigurationProperty("mode", typeof(CustomErrorsMode), CustomErrorsMode.RemoteOnly, new GenericEnumConverter(typeof(CustomErrorsMode)), PropertyHelper.DefaultValidator, ConfigurationPropertyOptions.None);

		// Token: 0x040020CE RID: 8398
		private static ConfigurationProperty redirectModeProp = new ConfigurationProperty("redirectMode", typeof(CustomErrorsRedirectMode), CustomErrorsRedirectMode.ResponseRedirect, new GenericEnumConverter(typeof(CustomErrorsRedirectMode)), PropertyHelper.DefaultValidator, ConfigurationPropertyOptions.None);

		// Token: 0x040020CF RID: 8399
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();
	}
}
