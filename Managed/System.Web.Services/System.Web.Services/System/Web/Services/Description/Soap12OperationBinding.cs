using System;
using System.ComponentModel;
using System.Web.Services.Configuration;
using System.Web.Services.Protocols;
using System.Xml.Serialization;

namespace System.Web.Services.Description
{
	/// <summary>Represents a service description format extension applied to an <see cref="T:System.Web.Services.Description.OperationBinding" /> when an XML Web service supports the SOAP protocol version 1.2. This class cannot be inherited.</summary>
	// Token: 0x02000115 RID: 277
	[XmlFormatExtension("operation", "http://schemas.xmlsoap.org/wsdl/soap12/", typeof(OperationBinding))]
	public sealed class Soap12OperationBinding : SoapOperationBinding
	{
		/// <summary>Gets or sets a value indicating whether an XML Web service anticipates requiring the SOAPAction HTTP header.</summary>
		/// <returns>true if an XML Web service anticipates requiring the SOAPAction HTTP header; otherwise, false. The default is false.</returns>
		// Token: 0x17000227 RID: 551
		// (get) Token: 0x06000878 RID: 2168 RVA: 0x0003BE1F File Offset: 0x0003A01F
		// (set) Token: 0x06000879 RID: 2169 RVA: 0x0003BE27 File Offset: 0x0003A027
		[DefaultValue(false)]
		[XmlAttribute("soapActionRequired")]
		public bool SoapActionRequired
		{
			get
			{
				return this.soapActionRequired;
			}
			set
			{
				this.soapActionRequired = value;
			}
		}

		// Token: 0x17000228 RID: 552
		// (get) Token: 0x0600087A RID: 2170 RVA: 0x0003BE30 File Offset: 0x0003A030
		// (set) Token: 0x0600087B RID: 2171 RVA: 0x0003BE38 File Offset: 0x0003A038
		internal SoapReflectedMethod Method
		{
			get
			{
				return this.method;
			}
			set
			{
				this.method = value;
			}
		}

		// Token: 0x17000229 RID: 553
		// (get) Token: 0x0600087C RID: 2172 RVA: 0x0003BE41 File Offset: 0x0003A041
		// (set) Token: 0x0600087D RID: 2173 RVA: 0x0003BE49 File Offset: 0x0003A049
		internal Soap12OperationBinding DuplicateBySoapAction
		{
			get
			{
				return this.duplicateBySoapAction;
			}
			set
			{
				this.duplicateBySoapAction = value;
			}
		}

		// Token: 0x1700022A RID: 554
		// (get) Token: 0x0600087E RID: 2174 RVA: 0x0003BE52 File Offset: 0x0003A052
		// (set) Token: 0x0600087F RID: 2175 RVA: 0x0003BE5A File Offset: 0x0003A05A
		internal Soap12OperationBinding DuplicateByRequestElement
		{
			get
			{
				return this.duplicateByRequestElement;
			}
			set
			{
				this.duplicateByRequestElement = value;
			}
		}

		// Token: 0x0400051F RID: 1311
		private bool soapActionRequired;

		// Token: 0x04000520 RID: 1312
		private Soap12OperationBinding duplicateBySoapAction;

		// Token: 0x04000521 RID: 1313
		private Soap12OperationBinding duplicateByRequestElement;

		// Token: 0x04000522 RID: 1314
		private SoapReflectedMethod method;
	}
}
