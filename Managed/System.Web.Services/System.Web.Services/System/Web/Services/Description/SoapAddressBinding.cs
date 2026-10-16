using System;
using System.Web.Services.Configuration;
using System.Xml.Serialization;

namespace System.Web.Services.Description
{
	/// <summary>Represents an extensibility element added to a <see cref="T:System.Web.Services.Description.Port" /> within an XML Web service.</summary>
	// Token: 0x02000127 RID: 295
	[XmlFormatExtension("address", "http://schemas.xmlsoap.org/wsdl/soap/", typeof(Port))]
	public class SoapAddressBinding : ServiceDescriptionFormatExtension
	{
		/// <summary>Gets or sets a value representing the URI for the <see cref="T:System.Web.Services.Description.Port" /> to which the <see cref="T:System.Web.Services.Description.SoapAddressBinding" /> applies.</summary>
		/// <returns>A string containing a URI. The default value is an empty string ("").</returns>
		// Token: 0x1700024A RID: 586
		// (get) Token: 0x060008DB RID: 2267 RVA: 0x0003C6F5 File Offset: 0x0003A8F5
		// (set) Token: 0x060008DC RID: 2268 RVA: 0x0003C70B File Offset: 0x0003A90B
		[XmlAttribute("location")]
		public string Location
		{
			get
			{
				if (this.location != null)
				{
					return this.location;
				}
				return string.Empty;
			}
			set
			{
				this.location = value;
			}
		}

		// Token: 0x0400054C RID: 1356
		private string location;
	}
}
