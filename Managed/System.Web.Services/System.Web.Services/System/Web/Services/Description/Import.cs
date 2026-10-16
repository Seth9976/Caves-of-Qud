using System;
using System.Web.Services.Configuration;
using System.Xml.Serialization;

namespace System.Web.Services.Description
{
	/// <summary>Associates an XML namespace with a document location. This class cannot be inherited.</summary>
	// Token: 0x020000E8 RID: 232
	[XmlFormatExtensionPoint("Extensions")]
	public sealed class Import : DocumentableItem
	{
		// Token: 0x06000641 RID: 1601 RVA: 0x0001C21D File Offset: 0x0001A41D
		internal void SetParent(ServiceDescription parent)
		{
			this.parent = parent;
		}

		/// <summary>Gets the <see cref="T:System.Web.Services.Description.ServiceDescriptionFormatExtensionCollection" /> associated with this <see cref="T:System.Web.Services.Description.Import" /> class.</summary>
		/// <returns>The <see cref="T:System.Web.Services.Description.ServiceDescriptionFormatExtensionCollection" /> associated with this <see cref="T:System.Web.Services.Description.Import" /> class.</returns>
		// Token: 0x170001BA RID: 442
		// (get) Token: 0x06000642 RID: 1602 RVA: 0x0001C226 File Offset: 0x0001A426
		[XmlIgnore]
		public override ServiceDescriptionFormatExtensionCollection Extensions
		{
			get
			{
				if (this.extensions == null)
				{
					this.extensions = new ServiceDescriptionFormatExtensionCollection(this);
				}
				return this.extensions;
			}
		}

		/// <summary>Gets a reference to the <see cref="T:System.Web.Services.Description.ServiceDescription" /> of which the <see cref="T:System.Web.Services.Description.Import" /> is a member.</summary>
		/// <returns>The <see cref="T:System.Web.Services.Description.ServiceDescription" /> of which the <see cref="T:System.Web.Services.Description.Import" /> is a member.</returns>
		// Token: 0x170001BB RID: 443
		// (get) Token: 0x06000643 RID: 1603 RVA: 0x0001C242 File Offset: 0x0001A442
		public ServiceDescription ServiceDescription
		{
			get
			{
				return this.parent;
			}
		}

		/// <summary>Gets or sets the value of the XML namespace attribute of the import element.</summary>
		/// <returns>The value of the XML namespace attribute of the import element.</returns>
		// Token: 0x170001BC RID: 444
		// (get) Token: 0x06000644 RID: 1604 RVA: 0x0001C24A File Offset: 0x0001A44A
		// (set) Token: 0x06000645 RID: 1605 RVA: 0x0001C260 File Offset: 0x0001A460
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

		/// <summary>Gets or sets the value of the XML location attribute of the import element.</summary>
		/// <returns>The value of the XML location attribute of the import element. This value also specifies the URL of the imported document.</returns>
		// Token: 0x170001BD RID: 445
		// (get) Token: 0x06000646 RID: 1606 RVA: 0x0001C269 File Offset: 0x0001A469
		// (set) Token: 0x06000647 RID: 1607 RVA: 0x0001C27F File Offset: 0x0001A47F
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

		// Token: 0x040003DF RID: 991
		private string ns;

		// Token: 0x040003E0 RID: 992
		private string location;

		// Token: 0x040003E1 RID: 993
		private ServiceDescription parent;

		// Token: 0x040003E2 RID: 994
		private ServiceDescriptionFormatExtensionCollection extensions;
	}
}
