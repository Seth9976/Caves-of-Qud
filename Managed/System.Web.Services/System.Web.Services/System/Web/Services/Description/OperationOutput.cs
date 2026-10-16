using System;
using System.Web.Services.Configuration;
using System.Xml.Serialization;

namespace System.Web.Services.Description
{
	/// <summary>Defines the specifications for output messages returned by the XML Web service. This class cannot be inherited.</summary>
	// Token: 0x020000F6 RID: 246
	[XmlFormatExtensionPoint("Extensions")]
	public sealed class OperationOutput : OperationMessage
	{
		/// <summary>Gets the <see cref="T:System.Web.Services.Description.ServiceDescriptionFormatExtensionCollection" /> associated with this <see cref="T:System.Web.Services.Description.OperationOutput" />.</summary>
		/// <returns>The <see cref="T:System.Web.Services.Description.ServiceDescriptionFormatExtensionCollection" /> associated with this <see cref="T:System.Web.Services.Description.OperationOutput" />.</returns>
		// Token: 0x170001DC RID: 476
		// (get) Token: 0x06000685 RID: 1669 RVA: 0x0001C699 File Offset: 0x0001A899
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

		// Token: 0x04000400 RID: 1024
		private ServiceDescriptionFormatExtensionCollection extensions;
	}
}
