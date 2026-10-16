using System;
using System.Configuration;

namespace System.Web.Configuration
{
	/// <summary>Maps a URL that is displayed to users to the URL of a page in your Web application. This class cannot be inherited.</summary>
	// Token: 0x020005E3 RID: 1507
	public sealed class UrlMapping : ConfigurationElement
	{
		// Token: 0x0600415E RID: 16734 RVA: 0x000A90D0 File Offset: 0x000A72D0
		private static void ValidateUrl(object value)
		{
			string text = value as string;
			if (string.IsNullOrEmpty(text))
			{
				return;
			}
			if (!VirtualPathUtility.IsAppRelative(text))
			{
				throw new ConfigurationException("Only app-relative (~/) URLs are allowed");
			}
		}

		// Token: 0x0600415F RID: 16735 RVA: 0x000A9100 File Offset: 0x000A7300
		static UrlMapping()
		{
			UrlMapping.properties.Add(UrlMapping.mappedUrlProp);
			UrlMapping.properties.Add(UrlMapping.urlProp);
		}

		// Token: 0x06004160 RID: 16736 RVA: 0x0009CC6D File Offset: 0x0009AE6D
		internal UrlMapping()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Configuration.UrlMapping" /> class.</summary>
		/// <param name="url">The URL to be displayed to the user.</param>
		/// <param name="mappedUrl">A URL that exists in your Web application.</param>
		// Token: 0x06004161 RID: 16737 RVA: 0x000A9195 File Offset: 0x000A7395
		public UrlMapping(string url, string mappedUrl)
		{
			this.Url = url;
			this.MappedUrl = mappedUrl;
		}

		/// <summary>A URL in your Web application.</summary>
		/// <returns>The URL in your Web application that has been mapped to the value specified by the <see cref="P:System.Web.Configuration.UrlMapping.Url" /> property. </returns>
		// Token: 0x170014E3 RID: 5347
		// (get) Token: 0x06004162 RID: 16738 RVA: 0x000A91AB File Offset: 0x000A73AB
		// (set) Token: 0x06004163 RID: 16739 RVA: 0x000A91BD File Offset: 0x000A73BD
		[ConfigurationProperty("mappedUrl", Options = ConfigurationPropertyOptions.IsRequired)]
		public string MappedUrl
		{
			get
			{
				return (string)base[UrlMapping.mappedUrlProp];
			}
			internal set
			{
				base[UrlMapping.mappedUrlProp] = value;
			}
		}

		/// <summary>Gets the URL that is displayed to the user.</summary>
		/// <returns>The URL that is displayed to the user.</returns>
		// Token: 0x170014E4 RID: 5348
		// (get) Token: 0x06004164 RID: 16740 RVA: 0x000A91CB File Offset: 0x000A73CB
		// (set) Token: 0x06004165 RID: 16741 RVA: 0x000A91DD File Offset: 0x000A73DD
		[ConfigurationProperty("url", Options = ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey)]
		public string Url
		{
			get
			{
				return (string)base[UrlMapping.urlProp];
			}
			internal set
			{
				base[UrlMapping.urlProp] = value;
			}
		}

		// Token: 0x170014E5 RID: 5349
		// (get) Token: 0x06004166 RID: 16742 RVA: 0x000A91EB File Offset: 0x000A73EB
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return UrlMapping.properties;
			}
		}

		// Token: 0x04002335 RID: 9013
		private static ConfigurationProperty mappedUrlProp = new ConfigurationProperty("mappedUrl", typeof(string), null, PropertyHelper.WhiteSpaceTrimStringConverter, PropertyHelper.NonEmptyStringValidator, ConfigurationPropertyOptions.IsRequired);

		// Token: 0x04002336 RID: 9014
		private static ConfigurationProperty urlProp = new ConfigurationProperty("url", typeof(string), null, PropertyHelper.WhiteSpaceTrimStringConverter, new CallbackValidator(typeof(string), new ValidatorCallback(UrlMapping.ValidateUrl)), ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey);

		// Token: 0x04002337 RID: 9015
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();
	}
}
