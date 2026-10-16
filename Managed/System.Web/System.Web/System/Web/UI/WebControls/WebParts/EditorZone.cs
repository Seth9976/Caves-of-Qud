using System;
using System.ComponentModel;
using Unity;

namespace System.Web.UI.WebControls.WebParts
{
	/// <summary>Serves as the primary control in the Web Parts control set for hosting <see cref="T:System.Web.UI.WebControls.WebParts.EditorPart" /> controls on a Web page.</summary>
	// Token: 0x020007A7 RID: 1959
	[Designer("System.Web.UI.Design.WebControls.WebParts.EditorZoneDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[SupportsEventValidation]
	public class EditorZone : EditorZoneBase
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.WebParts.EditorZone" /> class.</summary>
		// Token: 0x06004F1A RID: 20250 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public EditorZone()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Provides a template to contain child controls within an <see cref="T:System.Web.UI.WebControls.WebParts.EditorZone" /> control in page persistence format.</summary>
		/// <returns>An <see cref="T:System.Web.UI.ITemplate" /> zone template that acts as a container for child controls in the zone.</returns>
		// Token: 0x17001829 RID: 6185
		// (get) Token: 0x06004F1B RID: 20251 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		// (set) Token: 0x06004F1C RID: 20252 RVA: 0x0000B3D8 File Offset: 0x000095D8
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

		/// <summary>Creates all the <see cref="T:System.Web.UI.WebControls.WebParts.EditorPart" /> controls declared within a zone template in page persistence format.</summary>
		/// <returns>An <see cref="T:System.Web.UI.WebControls.WebParts.EditorPartCollection" /> that contains references to all the <see cref="T:System.Web.UI.WebControls.WebParts.EditorPart" /> controls declared within the zone template.</returns>
		/// <exception cref="T:System.InvalidOperationException">The current <see cref="T:System.Web.UI.WebControls.WebParts.EditorPart" /> control does not have an ID.</exception>
		// Token: 0x06004F1D RID: 20253 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		protected override EditorPartCollection CreateEditorParts()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}
	}
}
