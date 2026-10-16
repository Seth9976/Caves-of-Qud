using System;
using System.ComponentModel;
using Unity;

namespace System.Web.UI.WebControls.WebParts
{
	/// <summary>Enables developers to add a catalog of <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> or other server controls to a Web page in the declarative, page persistence format. This class cannot be inherited.</summary>
	// Token: 0x020007A6 RID: 1958
	[Designer("System.Web.UI.Design.WebControls.WebParts.DeclarativeCatalogPartDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public sealed class DeclarativeCatalogPart : CatalogPart
	{
		/// <summary>Initializes a new instance of the class. </summary>
		// Token: 0x06004F13 RID: 20243 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public DeclarativeCatalogPart()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets or sets the path to a user control that contains a list of <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> or other server controls for the catalog.</summary>
		/// <returns>A string with the path to a user control that contains a set of <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> or server controls.</returns>
		// Token: 0x17001827 RID: 6183
		// (get) Token: 0x06004F14 RID: 20244 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		// (set) Token: 0x06004F15 RID: 20245 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public string WebPartsListUserControlPath
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

		/// <summary>Gets or sets a reference to a template that contains the <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> controls declared in a catalog.</summary>
		/// <returns>An <see cref="T:System.Web.UI.ITemplate" /> that contains controls declared in a catalog. </returns>
		// Token: 0x17001828 RID: 6184
		// (get) Token: 0x06004F16 RID: 20246 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		// (set) Token: 0x06004F17 RID: 20247 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public ITemplate WebPartsTemplate
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

		/// <summary>Returns a collection of descriptions of the available <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> controls in a catalog.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.WebParts.WebPartDescriptionCollection" /> that contains a <see cref="T:System.Web.UI.WebControls.WebParts.WebPartDescription" /> for each control in a catalog.</returns>
		// Token: 0x06004F18 RID: 20248 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public override WebPartDescriptionCollection GetAvailableWebPartDescriptions()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Returns a reference to a <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control based on the value of the description passed into the method.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control whose description matches the values in <paramref name="description" />.</returns>
		/// <param name="description">A <see cref="T:System.Web.UI.WebControls.WebParts.WebPartDescription" /> that contains details about the control. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="Description" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="Description" /> is not an available <see cref="T:System.Web.UI.WebControls.WebParts.WebPartDescription" /> instance.</exception>
		// Token: 0x06004F19 RID: 20249 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public override WebPart GetWebPart(WebPartDescription description)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}
	}
}
