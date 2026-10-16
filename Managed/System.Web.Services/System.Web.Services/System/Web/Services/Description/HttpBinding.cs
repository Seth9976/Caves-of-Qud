using System;
using System.Web.Services.Configuration;
using System.Xml.Serialization;

namespace System.Web.Services.Description
{
	/// <summary>Represents an extensibility element added to a <see cref="T:System.Web.Services.Description.Binding" /> within an XML Web service. This class cannot be inherited.</summary>
	// Token: 0x020000BE RID: 190
	[XmlFormatExtension("binding", "http://schemas.xmlsoap.org/wsdl/http/", typeof(Binding))]
	[XmlFormatExtensionPrefix("http", "http://schemas.xmlsoap.org/wsdl/http/")]
	public sealed class HttpBinding : ServiceDescriptionFormatExtension
	{
		/// <summary>Gets or sets a value indicating whether the HTTP request will be made using the "GET" or "POST" method.</summary>
		/// <returns>A string containing one of two possible values, "GET" or "POST". The default value is an empty string ("").</returns>
		// Token: 0x1700014C RID: 332
		// (get) Token: 0x06000501 RID: 1281 RVA: 0x00017370 File Offset: 0x00015570
		// (set) Token: 0x06000502 RID: 1282 RVA: 0x00017378 File Offset: 0x00015578
		[XmlAttribute("verb")]
		public string Verb
		{
			get
			{
				return this.verb;
			}
			set
			{
				this.verb = value;
			}
		}

		// Token: 0x04000374 RID: 884
		private string verb;

		/// <summary>Specifies the URI for the XML namespace representing the HTTP transport for use with SOAP. This field is constant.</summary>
		// Token: 0x04000375 RID: 885
		public const string Namespace = "http://schemas.xmlsoap.org/wsdl/http/";
	}
}
