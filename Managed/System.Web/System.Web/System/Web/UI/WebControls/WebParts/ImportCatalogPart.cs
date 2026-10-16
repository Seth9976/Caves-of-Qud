using System;
using Unity;

namespace System.Web.UI.WebControls.WebParts
{
	/// <summary>Imports a description file for a <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control (or other ASP.NET server control used as a <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control), so that users can add the control to a Web page with pre-defined settings. This class cannot be inherited.</summary>
	// Token: 0x020007A8 RID: 1960
	public sealed class ImportCatalogPart : CatalogPart
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.WebParts.ImportCatalogPart" /> class. </summary>
		// Token: 0x06004F1E RID: 20254 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public ImportCatalogPart()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets or sets a text message that instructs users to browse to the location of a description file.</summary>
		/// <returns>A string that contains the text of the message. The default value is a culture-specific message supplied by the .NET Framework.</returns>
		// Token: 0x1700182A RID: 6186
		// (get) Token: 0x06004F1F RID: 20255 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		// (set) Token: 0x06004F20 RID: 20256 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public string BrowseHelpText
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

		/// <summary>Gets or sets text displayed after a user imports a description file to represent or describe the imported control within the catalog of imported controls.</summary>
		/// <returns>A string that contains the text of the label. The default value is a culture-specific message supplied by the .NET Framework.</returns>
		// Token: 0x1700182B RID: 6187
		// (get) Token: 0x06004F21 RID: 20257 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		// (set) Token: 0x06004F22 RID: 20258 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public string ImportedPartLabelText
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

		/// <summary>Gets or sets an error message that is displayed if an error occurs during the import process.</summary>
		/// <returns>A string that contains the text of the label. The default value is a culture-specific message supplied by the .NET Framework.</returns>
		// Token: 0x1700182C RID: 6188
		// (get) Token: 0x06004F23 RID: 20259 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		// (set) Token: 0x06004F24 RID: 20260 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public string PartImportErrorLabelText
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

		/// <summary>Gets or sets the text for the <see cref="T:System.Web.UI.WebControls.Button" /> control that initiates the upload of a description file.</summary>
		/// <returns>A string that is used as the text for a <see cref="T:System.Web.UI.WebControls.Button" />. The default value is a culture-specific string supplied by the Web Parts control set.</returns>
		// Token: 0x1700182D RID: 6189
		// (get) Token: 0x06004F25 RID: 20261 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		// (set) Token: 0x06004F26 RID: 20262 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public string UploadButtonText
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

		/// <summary>Gets or sets the text of the message that tells the user how to upload a description file.</summary>
		/// <returns>A string that is used as instructions for the user to upload a description file. The default value is a culture-specific string supplied by the Web Parts control set.</returns>
		// Token: 0x1700182E RID: 6190
		// (get) Token: 0x06004F27 RID: 20263 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		// (set) Token: 0x06004F28 RID: 20264 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public string UploadHelpText
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
		// Token: 0x06004F29 RID: 20265 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public override WebPartDescriptionCollection GetAvailableWebPartDescriptions()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Returns a reference to a <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control based on the values in the description passed into the method.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control whose description matches the values in <paramref name="description" />.</returns>
		/// <param name="description">A <see cref="T:System.Web.UI.WebControls.WebParts.WebPartDescription" /> that contains details about the control. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="description" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="description" /> is not an available <see cref="T:System.Web.UI.WebControls.WebParts.WebPartDescription" /> value.</exception>
		// Token: 0x06004F2A RID: 20266 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public override WebPart GetWebPart(WebPartDescription description)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}
	}
}
