using System;
using System.Web.Services.Configuration;
using System.Xml.Serialization;

namespace System.Web.Services.Description
{
	/// <summary>Defines the specifications for input messages received by the XML Web service. This class cannot be inherited.</summary>
	// Token: 0x020000F5 RID: 245
	[XmlFormatExtensionPoint("Extensions")]
	public sealed class OperationInput : OperationMessage
	{
		/// <summary>Gets the <see cref="T:System.Web.Services.Description.ServiceDescriptionFormatExtensionCollection" /> associated with this <see cref="T:System.Web.Services.Description.OperationInput" />.</summary>
		/// <returns>The <see cref="T:System.Web.Services.Description.ServiceDescriptionFormatExtensionCollection" /> associated with this <see cref="T:System.Web.Services.Description.OperationInput" />.</returns>
		// Token: 0x170001DB RID: 475
		// (get) Token: 0x06000683 RID: 1667 RVA: 0x0001C67D File Offset: 0x0001A87D
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

		// Token: 0x040003FF RID: 1023
		private ServiceDescriptionFormatExtensionCollection extensions;
	}
}
