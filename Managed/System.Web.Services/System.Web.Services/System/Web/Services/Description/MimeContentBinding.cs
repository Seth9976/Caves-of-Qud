using System;
using System.Web.Services.Configuration;
using System.Xml.Serialization;

namespace System.Web.Services.Description
{
	/// <summary>Represents an extensibility element added to an <see cref="T:System.Web.Services.Description.InputBinding" /> or an <see cref="T:System.Web.Services.Description.OutputBinding" /> within an XML Web service, specifying the MIME format for the body of the HTTP transmission. This class cannot be inherited.</summary>
	// Token: 0x020000CE RID: 206
	[XmlFormatExtensionPrefix("mime", "http://schemas.xmlsoap.org/wsdl/mime/")]
	[XmlFormatExtension("content", "http://schemas.xmlsoap.org/wsdl/mime/", typeof(MimePart), typeof(InputBinding), typeof(OutputBinding))]
	public sealed class MimeContentBinding : ServiceDescriptionFormatExtension
	{
		/// <summary>Gets or sets the name of the <see cref="T:System.Web.Services.Description.MessagePart" /> to which the <see cref="T:System.Web.Services.Description.MimeContentBinding" /> applies.</summary>
		/// <returns>A string representing the name of the <see cref="T:System.Web.Services.Description.MessagePart" /> with which the current <see cref="T:System.Web.Services.Description.MimeContentBinding" /> is associated. The default value is an empty string ("").</returns>
		// Token: 0x17000156 RID: 342
		// (get) Token: 0x0600053C RID: 1340 RVA: 0x00018A7A File Offset: 0x00016C7A
		// (set) Token: 0x0600053D RID: 1341 RVA: 0x00018A82 File Offset: 0x00016C82
		[XmlAttribute("part")]
		public string Part
		{
			get
			{
				return this.part;
			}
			set
			{
				this.part = value;
			}
		}

		/// <summary>Gets or sets a value indicating the format of the body of the HTTP transmission.</summary>
		/// <returns>A string indicating the format of the body of the HTTP transmission. The default value is an empty string ("").</returns>
		// Token: 0x17000157 RID: 343
		// (get) Token: 0x0600053E RID: 1342 RVA: 0x00018A8B File Offset: 0x00016C8B
		// (set) Token: 0x0600053F RID: 1343 RVA: 0x00018AA1 File Offset: 0x00016CA1
		[XmlAttribute("type")]
		public string Type
		{
			get
			{
				if (this.type != null)
				{
					return this.type;
				}
				return string.Empty;
			}
			set
			{
				this.type = value;
			}
		}

		// Token: 0x04000384 RID: 900
		private string type;

		// Token: 0x04000385 RID: 901
		private string part;

		/// <summary>Specifies the URI for the XML namespace of the <see cref="T:System.Web.Services.Description.MimeContentBinding" /> class. This field is constant.</summary>
		// Token: 0x04000386 RID: 902
		public const string Namespace = "http://schemas.xmlsoap.org/wsdl/mime/";
	}
}
