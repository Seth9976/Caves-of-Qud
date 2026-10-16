using System;
using System.ComponentModel;
using Unity;

namespace System.Web.UI.WebControls.WebParts
{
	/// <summary>Serves as the primary control in the Web Parts control set for hosting <see cref="T:System.Web.UI.WebControls.WebParts.CatalogPart" /> controls on a Web page.</summary>
	// Token: 0x020007A2 RID: 1954
	[Designer("System.Web.UI.Design.WebControls.WebParts.CatalogZoneDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[SupportsEventValidation]
	public class CatalogZone : CatalogZoneBase
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.WebParts.CatalogZone" /> class.</summary>
		// Token: 0x06004EBB RID: 20155 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public CatalogZone()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets or sets a template to contain child controls within a <see cref="T:System.Web.UI.WebControls.WebParts.CatalogZone" /> control in page persistence format.</summary>
		/// <returns>An <see cref="T:System.Web.UI.ITemplate" /> that acts as a container for child controls in the zone.</returns>
		// Token: 0x170017FD RID: 6141
		// (get) Token: 0x06004EBC RID: 20156 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		// (set) Token: 0x06004EBD RID: 20157 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public virtual ITemplate ZoneTemplate
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Creates an instance of each <see cref="T:System.Web.UI.WebControls.WebParts.CatalogPart" /> type that is declared in the zone.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.WebParts.CatalogPartCollection" /> with references to all the <see cref="T:System.Web.UI.WebControls.WebParts.CatalogPart" /> controls in the zone.</returns>
		/// <exception cref="T:System.InvalidOperationException">The current <see cref="T:System.Web.UI.WebControls.WebParts.CatalogPart" /> control does not have an ID.</exception>
		// Token: 0x06004EBE RID: 20158 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		protected override CatalogPartCollection CreateCatalogParts()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}
	}
}
