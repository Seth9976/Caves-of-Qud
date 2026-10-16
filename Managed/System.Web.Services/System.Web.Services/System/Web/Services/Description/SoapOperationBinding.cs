using System;
using System.ComponentModel;
using System.Web.Services.Configuration;
using System.Xml.Serialization;

namespace System.Web.Services.Description
{
	/// <summary>Represents an extension added to an <see cref="T:System.Web.Services.Description.OperationBinding" /> within an XML Web service.</summary>
	// Token: 0x02000121 RID: 289
	[XmlFormatExtension("operation", "http://schemas.xmlsoap.org/wsdl/soap/", typeof(OperationBinding))]
	public class SoapOperationBinding : ServiceDescriptionFormatExtension
	{
		/// <summary>Gets or sets the URI for the SOAP header.</summary>
		/// <returns>A string containing the URI for the SOAP header.</returns>
		// Token: 0x17000233 RID: 563
		// (get) Token: 0x060008A8 RID: 2216 RVA: 0x0003C463 File Offset: 0x0003A663
		// (set) Token: 0x060008A9 RID: 2217 RVA: 0x0003C479 File Offset: 0x0003A679
		[XmlAttribute("soapAction")]
		public string SoapAction
		{
			get
			{
				if (this.soapAction != null)
				{
					return this.soapAction;
				}
				return string.Empty;
			}
			set
			{
				this.soapAction = value;
			}
		}

		/// <summary>Gets or sets the type of SOAP binding used by the <see cref="T:System.Web.Services.Description.SoapOperationBinding" />.</summary>
		/// <returns>One of the <see cref="T:System.Web.Services.Description.SoapBindingStyle" /> values. The default is Document.</returns>
		// Token: 0x17000234 RID: 564
		// (get) Token: 0x060008AA RID: 2218 RVA: 0x0003C482 File Offset: 0x0003A682
		// (set) Token: 0x060008AB RID: 2219 RVA: 0x0003C48A File Offset: 0x0003A68A
		[DefaultValue(SoapBindingStyle.Default)]
		[XmlAttribute("style")]
		public SoapBindingStyle Style
		{
			get
			{
				return this.style;
			}
			set
			{
				this.style = value;
			}
		}

		// Token: 0x04000532 RID: 1330
		private string soapAction;

		// Token: 0x04000533 RID: 1331
		private SoapBindingStyle style;
	}
}
