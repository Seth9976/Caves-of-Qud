using System;
using Unity;

namespace System.Web.UI.WebControls.WebParts
{
	/// <summary>Enables developers to override the rendering for only the selected sections of <see cref="T:System.Web.UI.WebControls.WebParts.CatalogPart" /> controls in a <see cref="T:System.Web.UI.WebControls.WebParts.CatalogZoneBase" /> zone.</summary>
	// Token: 0x0200079E RID: 1950
	public class CatalogPartChrome
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.WebParts.CatalogPartChrome" /> class. </summary>
		/// <param name="zone">The associated <see cref="T:System.Web.UI.WebControls.WebParts.CatalogZoneBase" />. </param>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="T:System.Web.UI.WebControls.WebParts.CatalogZoneBase" /> that <paramref name="zone" /> refers to is null.</exception>
		// Token: 0x06004EA0 RID: 20128 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public CatalogPartChrome(CatalogZoneBase zone)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets a reference to the associated <see cref="T:System.Web.UI.WebControls.WebParts.CatalogZoneBase" /> zone.</summary>
		/// <returns>A reference to a <see cref="T:System.Web.UI.WebControls.WebParts.CatalogZoneBase" /> that is associated with the <see cref="T:System.Web.UI.WebControls.WebParts.CatalogPartChrome" />.</returns>
		// Token: 0x170017F6 RID: 6134
		// (get) Token: 0x06004EA1 RID: 20129 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		protected CatalogZoneBase Zone
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Creates the style object that supplies style attributes for each <see cref="T:System.Web.UI.WebControls.WebParts.CatalogPart" /> control rendered by the <see cref="T:System.Web.UI.WebControls.WebParts.CatalogPartChrome" /> object.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.Style" /> that contains style attributes for <paramref name="catalogPart" />.</returns>
		/// <param name="catalogPart">The control that is currently being rendered. </param>
		/// <param name="chromeType">The type of chrome for a particular control; one of the <see cref="T:System.Web.UI.WebControls.WebParts.PartChromeType" /> enumeration values. </param>
		/// <exception cref="T:System.ArgumentNullException">The control that <paramref name="catalogPart" /> refers to is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="chromeType" /> is not a <see cref="T:System.Web.UI.WebControls.WebParts.PartChromeType" />.</exception>
		// Token: 0x06004EA2 RID: 20130 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		protected virtual Style CreateCatalogPartChromeStyle(CatalogPart catalogPart, PartChromeType chromeType)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Performs tasks that must be done prior to rendering <see cref="T:System.Web.UI.WebControls.WebParts.CatalogPart" /> controls.</summary>
		// Token: 0x06004EA3 RID: 20131 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public virtual void PerformPreRender()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Renders a complete <see cref="T:System.Web.UI.WebControls.WebParts.CatalogPart" /> control with all its sections.</summary>
		/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> that receives the <paramref name="catalogPart" /> content. </param>
		/// <param name="catalogPart">The control currently being rendered. </param>
		/// <exception cref="T:System.ArgumentNullException">The control that <paramref name="catalogPart" /> refers to is null.</exception>
		// Token: 0x06004EA4 RID: 20132 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public virtual void RenderCatalogPart(HtmlTextWriter writer, CatalogPart catalogPart)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Renders the main content area of a <see cref="T:System.Web.UI.WebControls.WebParts.CatalogPart" /> control, excluding the header and footer.</summary>
		/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> that receives the <paramref name="catalogPart" /> content. </param>
		/// <param name="catalogPart">The control currently being rendered. </param>
		/// <exception cref="T:System.ArgumentNullException">The control that <paramref name="catalogPart" /> refers to is null.</exception>
		// Token: 0x06004EA5 RID: 20133 RVA: 0x0000B3D8 File Offset: 0x000095D8
		protected virtual void RenderPartContents(HtmlTextWriter writer, CatalogPart catalogPart)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
}
