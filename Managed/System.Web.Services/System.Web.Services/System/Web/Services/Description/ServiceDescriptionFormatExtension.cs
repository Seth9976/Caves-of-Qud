using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace System.Web.Services.Description
{
	/// <summary>Represents an extensibility element added to an XML Web service.</summary>
	// Token: 0x020000FD RID: 253
	public abstract class ServiceDescriptionFormatExtension
	{
		// Token: 0x060006BF RID: 1727 RVA: 0x0001CDDB File Offset: 0x0001AFDB
		internal void SetParent(object parent)
		{
			this.parent = parent;
		}

		/// <summary>Gets the parent of the <see cref="T:System.Web.Services.Description.ServiceDescriptionFormatExtension" />.</summary>
		/// <returns>The parent of the <see cref="T:System.Web.Services.Description.ServiceDescriptionFormatExtension" />.</returns>
		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x060006C0 RID: 1728 RVA: 0x0001CDE4 File Offset: 0x0001AFE4
		public object Parent
		{
			get
			{
				return this.parent;
			}
		}

		/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Web.Services.Description.ServiceDescriptionFormatExtension" /> is necessary for the action to which it refers.</summary>
		/// <returns>true if the <see cref="T:System.Web.Services.Description.ServiceDescriptionFormatExtension" /> is required; otherwise, false. The default is false.</returns>
		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x060006C1 RID: 1729 RVA: 0x0001CDEC File Offset: 0x0001AFEC
		// (set) Token: 0x060006C2 RID: 1730 RVA: 0x0001CDF4 File Offset: 0x0001AFF4
		[DefaultValue(false)]
		[XmlAttribute("required", Namespace = "http://schemas.xmlsoap.org/wsdl/")]
		public bool Required
		{
			get
			{
				return this.required;
			}
			set
			{
				this.required = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Web.Services.Description.ServiceDescriptionFormatExtension" /> is used by the import process when the extensibility element is imported.</summary>
		/// <returns>true if the <see cref="T:System.Web.Services.Description.ServiceDescriptionFormatExtension" /> is used by the import process; otherwise, false. The default is false.</returns>
		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x060006C3 RID: 1731 RVA: 0x0001CDFD File Offset: 0x0001AFFD
		// (set) Token: 0x060006C4 RID: 1732 RVA: 0x0001CE05 File Offset: 0x0001B005
		[XmlIgnore]
		public bool Handled
		{
			get
			{
				return this.handled;
			}
			set
			{
				this.handled = value;
			}
		}

		// Token: 0x04000413 RID: 1043
		private object parent;

		// Token: 0x04000414 RID: 1044
		private bool required;

		// Token: 0x04000415 RID: 1045
		private bool handled;
	}
}
