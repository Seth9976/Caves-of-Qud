using System;
using System.ComponentModel;
using System.Web.Services.Configuration;
using System.Xml.Serialization;

namespace System.Web.Services.Description
{
	/// <summary>Represents an extensibility element added to a <see cref="T:System.Web.Services.Description.FaultBinding" /> within an XML Web service.</summary>
	// Token: 0x02000124 RID: 292
	[XmlFormatExtension("fault", "http://schemas.xmlsoap.org/wsdl/soap/", typeof(FaultBinding))]
	public class SoapFaultBinding : ServiceDescriptionFormatExtension
	{
		/// <summary>Specifies whether the fault message is encoded using rules specified by the <see cref="P:System.Web.Services.Description.SoapFaultBinding.Encoding" /> property, or is encapsulated within a concrete XML schema.</summary>
		/// <returns>One of the <see cref="T:System.Web.Services.Description.SoapBindingUse" /> values. The default is Default.</returns>
		// Token: 0x1700023A RID: 570
		// (get) Token: 0x060008B8 RID: 2232 RVA: 0x0003C56B File Offset: 0x0003A76B
		// (set) Token: 0x060008B9 RID: 2233 RVA: 0x0003C573 File Offset: 0x0003A773
		[DefaultValue(SoapBindingUse.Default)]
		[XmlAttribute("use")]
		public SoapBindingUse Use
		{
			get
			{
				return this.use;
			}
			set
			{
				this.use = value;
			}
		}

		/// <summary>Gets or sets the value of the name attribute that relates the soap fault to the wsdl fault defined for the specified operation.</summary>
		/// <returns>A <see cref="T:System.String" /> object that contains the name attribute that relates the soap fault to the wsdl fault defined for the operation.</returns>
		// Token: 0x1700023B RID: 571
		// (get) Token: 0x060008BA RID: 2234 RVA: 0x0003C57C File Offset: 0x0003A77C
		// (set) Token: 0x060008BB RID: 2235 RVA: 0x0003C584 File Offset: 0x0003A784
		[XmlAttribute("name")]
		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		/// <summary>Get or sets the URI representing the location of the specification for encoding of content not specifically defined by the <see cref="P:System.Web.Services.Description.SoapFaultBinding.Encoding" /> property.</summary>
		/// <returns>A string representing a URI.</returns>
		// Token: 0x1700023C RID: 572
		// (get) Token: 0x060008BC RID: 2236 RVA: 0x0003C58D File Offset: 0x0003A78D
		// (set) Token: 0x060008BD RID: 2237 RVA: 0x0003C5A3 File Offset: 0x0003A7A3
		[XmlAttribute("namespace")]
		public string Namespace
		{
			get
			{
				if (this.ns != null)
				{
					return this.ns;
				}
				return string.Empty;
			}
			set
			{
				this.ns = value;
			}
		}

		/// <summary>Gets or sets a URI representing the encoding style used to encode the SOAP fault message.</summary>
		/// <returns>A string containing a URI. The default value is an empty string ("").</returns>
		// Token: 0x1700023D RID: 573
		// (get) Token: 0x060008BE RID: 2238 RVA: 0x0003C5AC File Offset: 0x0003A7AC
		// (set) Token: 0x060008BF RID: 2239 RVA: 0x0003C5C2 File Offset: 0x0003A7C2
		[XmlAttribute("encodingStyle")]
		[DefaultValue("")]
		public string Encoding
		{
			get
			{
				if (this.encoding != null)
				{
					return this.encoding;
				}
				return string.Empty;
			}
			set
			{
				this.encoding = value;
			}
		}

		// Token: 0x0400053C RID: 1340
		private SoapBindingUse use;

		// Token: 0x0400053D RID: 1341
		private string ns;

		// Token: 0x0400053E RID: 1342
		private string encoding;

		// Token: 0x0400053F RID: 1343
		private string name;
	}
}
