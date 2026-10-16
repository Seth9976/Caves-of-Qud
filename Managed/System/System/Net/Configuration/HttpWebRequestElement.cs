using System;
using System.Configuration;

namespace System.Net.Configuration
{
	/// <summary>Represents the maximum length for response headers. This class cannot be inherited.</summary>
	// Token: 0x0200076D RID: 1901
	public sealed class HttpWebRequestElement : ConfigurationElement
	{
		// Token: 0x06003BE2 RID: 15330 RVA: 0x000CD1E4 File Offset: 0x000CB3E4
		static HttpWebRequestElement()
		{
			HttpWebRequestElement.properties.Add(HttpWebRequestElement.maximumErrorResponseLengthProp);
			HttpWebRequestElement.properties.Add(HttpWebRequestElement.maximumResponseHeadersLengthProp);
			HttpWebRequestElement.properties.Add(HttpWebRequestElement.maximumUnauthorizedUploadLengthProp);
			HttpWebRequestElement.properties.Add(HttpWebRequestElement.useUnsafeHeaderParsingProp);
		}

		/// <summary>Gets or sets the maximum allowed length of an error response.</summary>
		/// <returns>A 32-bit signed integer containing the maximum length in kilobytes (1024 bytes) of the error response. The default value is 64.</returns>
		// Token: 0x17000D8D RID: 3469
		// (get) Token: 0x06003BE4 RID: 15332 RVA: 0x000CD2B5 File Offset: 0x000CB4B5
		// (set) Token: 0x06003BE5 RID: 15333 RVA: 0x000CD2C7 File Offset: 0x000CB4C7
		[ConfigurationProperty("maximumErrorResponseLength", DefaultValue = "64")]
		public int MaximumErrorResponseLength
		{
			get
			{
				return (int)base[HttpWebRequestElement.maximumErrorResponseLengthProp];
			}
			set
			{
				base[HttpWebRequestElement.maximumErrorResponseLengthProp] = value;
			}
		}

		/// <summary>Gets or sets the maximum allowed length of the response headers.</summary>
		/// <returns>A 32-bit signed integer containing the maximum length in kilobytes (1024 bytes) of the response headers. The default value is 64.</returns>
		// Token: 0x17000D8E RID: 3470
		// (get) Token: 0x06003BE6 RID: 15334 RVA: 0x000CD2DA File Offset: 0x000CB4DA
		// (set) Token: 0x06003BE7 RID: 15335 RVA: 0x000CD2EC File Offset: 0x000CB4EC
		[ConfigurationProperty("maximumResponseHeadersLength", DefaultValue = "64")]
		public int MaximumResponseHeadersLength
		{
			get
			{
				return (int)base[HttpWebRequestElement.maximumResponseHeadersLengthProp];
			}
			set
			{
				base[HttpWebRequestElement.maximumResponseHeadersLengthProp] = value;
			}
		}

		/// <summary>Gets or sets the maximum length of an upload in response to an unauthorized error code.</summary>
		/// <returns>A 32-bit signed integer containing the maximum length (in multiple of 1,024 byte units) of an upload in response to an unauthorized error code. A value of -1 indicates that no size limit will be imposed on the upload. Setting the <see cref="P:System.Net.Configuration.HttpWebRequestElement.MaximumUnauthorizedUploadLength" /> property to any other value will only send the request body if it is smaller than the number of bytes specified. So a value of 1 would indicate to only send the request body if it is smaller than 1,024 bytes. The default value for this property is -1.</returns>
		// Token: 0x17000D8F RID: 3471
		// (get) Token: 0x06003BE8 RID: 15336 RVA: 0x000CD2FF File Offset: 0x000CB4FF
		// (set) Token: 0x06003BE9 RID: 15337 RVA: 0x000CD311 File Offset: 0x000CB511
		[ConfigurationProperty("maximumUnauthorizedUploadLength", DefaultValue = "-1")]
		public int MaximumUnauthorizedUploadLength
		{
			get
			{
				return (int)base[HttpWebRequestElement.maximumUnauthorizedUploadLengthProp];
			}
			set
			{
				base[HttpWebRequestElement.maximumUnauthorizedUploadLengthProp] = value;
			}
		}

		/// <summary>Setting this property ignores validation errors that occur during HTTP parsing.</summary>
		/// <returns>Boolean that indicates whether this property has been set. </returns>
		// Token: 0x17000D90 RID: 3472
		// (get) Token: 0x06003BEA RID: 15338 RVA: 0x000CD324 File Offset: 0x000CB524
		// (set) Token: 0x06003BEB RID: 15339 RVA: 0x000CD336 File Offset: 0x000CB536
		[ConfigurationProperty("useUnsafeHeaderParsing", DefaultValue = "False")]
		public bool UseUnsafeHeaderParsing
		{
			get
			{
				return (bool)base[HttpWebRequestElement.useUnsafeHeaderParsingProp];
			}
			set
			{
				base[HttpWebRequestElement.useUnsafeHeaderParsingProp] = value;
			}
		}

		// Token: 0x17000D91 RID: 3473
		// (get) Token: 0x06003BEC RID: 15340 RVA: 0x000CD349 File Offset: 0x000CB549
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return HttpWebRequestElement.properties;
			}
		}

		// Token: 0x06003BED RID: 15341 RVA: 0x00066D5E File Offset: 0x00064F5E
		[MonoTODO]
		protected override void PostDeserialize()
		{
			base.PostDeserialize();
		}

		// Token: 0x040023B6 RID: 9142
		private static ConfigurationProperty maximumErrorResponseLengthProp = new ConfigurationProperty("maximumErrorResponseLength", typeof(int), 64);

		// Token: 0x040023B7 RID: 9143
		private static ConfigurationProperty maximumResponseHeadersLengthProp = new ConfigurationProperty("maximumResponseHeadersLength", typeof(int), 64);

		// Token: 0x040023B8 RID: 9144
		private static ConfigurationProperty maximumUnauthorizedUploadLengthProp = new ConfigurationProperty("maximumUnauthorizedUploadLength", typeof(int), -1);

		// Token: 0x040023B9 RID: 9145
		private static ConfigurationProperty useUnsafeHeaderParsingProp = new ConfigurationProperty("useUnsafeHeaderParsing", typeof(bool), false);

		// Token: 0x040023BA RID: 9146
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();
	}
}
