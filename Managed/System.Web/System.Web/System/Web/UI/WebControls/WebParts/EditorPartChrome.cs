using System;
using Unity;

namespace System.Web.UI.WebControls.WebParts
{
	/// <summary>Enables developers to override the rendering for only the selected sections of <see cref="T:System.Web.UI.WebControls.WebParts.EditorPart" /> controls in an <see cref="T:System.Web.UI.WebControls.WebParts.EditorZoneBase" /> zone.</summary>
	// Token: 0x020006DA RID: 1754
	public class EditorPartChrome
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.WebParts.EditorPartChrome" /> class. </summary>
		/// <param name="zone">The associated <see cref="T:System.Web.UI.WebControls.WebParts.EditorZoneBase" />.</param>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="T:System.Web.UI.WebControls.WebParts.EditorZoneBase" /> that <paramref name="zone" /> refers to is null.</exception>
		// Token: 0x06004A6F RID: 19055 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public EditorPartChrome(EditorZoneBase zone)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets a reference to the associated <see cref="T:System.Web.UI.WebControls.WebParts.EditorZoneBase" /> zone.</summary>
		/// <returns>A reference to an <see cref="T:System.Web.UI.WebControls.WebParts.EditorZoneBase" /> that is associated with the <see cref="T:System.Web.UI.WebControls.WebParts.EditorPartChrome" />.</returns>
		// Token: 0x17001713 RID: 5907
		// (get) Token: 0x06004A70 RID: 19056 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		protected EditorZoneBase Zone
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Creates the style object that supplies style attributes for each <see cref="T:System.Web.UI.WebControls.WebParts.EditorPart" /> control rendered by the <see cref="T:System.Web.UI.WebControls.WebParts.EditorPartChrome" /> object.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.Style" /> that contains style attributes for <paramref name="editorPart" />.</returns>
		/// <param name="editorPart">The control that is currently being rendered.</param>
		/// <param name="chromeType">The type of chrome for a particular control; one of the <see cref="T:System.Web.UI.WebControls.WebParts.PartChromeType" /> enumeration values.</param>
		/// <exception cref="T:System.ArgumentNullException">The control that <paramref name="editorPart" /> refers to is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="chromeType" /> is not a <see cref="T:System.Web.UI.WebControls.WebParts.PartChromeType" />.</exception>
		// Token: 0x06004A71 RID: 19057 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		protected virtual Style CreateEditorPartChromeStyle(EditorPart editorPart, PartChromeType chromeType)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Performs tasks that must be done prior to rendering <see cref="T:System.Web.UI.WebControls.WebParts.EditorPart" /> controls.</summary>
		// Token: 0x06004A72 RID: 19058 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public virtual void PerformPreRender()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Renders a complete <see cref="T:System.Web.UI.WebControls.WebParts.EditorPart" /> control with all its sections.</summary>
		/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> that receives the <paramref name="editorPart" /> content.</param>
		/// <param name="editorPart">The control currently being rendered.</param>
		/// <exception cref="T:System.ArgumentNullException">The control that <paramref name="editorPart" /> refers to is null.</exception>
		// Token: 0x06004A73 RID: 19059 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public virtual void RenderEditorPart(HtmlTextWriter writer, EditorPart editorPart)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Renders the main content area of an <see cref="T:System.Web.UI.WebControls.WebParts.EditorPart" /> control, excluding the header and footer.</summary>
		/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> that receives the <paramref name="editorPart" /> content.</param>
		/// <param name="editorPart">The control currently being rendered.</param>
		// Token: 0x06004A74 RID: 19060 RVA: 0x0000B3D8 File Offset: 0x000095D8
		protected virtual void RenderPartContents(HtmlTextWriter writer, EditorPart editorPart)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
}
