using System;

namespace System.Web.UI.WebControls.WebParts
{
	/// <summary>Defines common user interface (UI) properties used by ASP.NET <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> controls. </summary>
	// Token: 0x02000461 RID: 1121
	public interface IWebPart
	{
		/// <summary>Gets or sets the URL to an image that represents a <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control in a catalog of controls.</summary>
		/// <returns>A string that represents the URL to an image used to represent the control in a catalog. The default value is an empty string ("").</returns>
		// Token: 0x17001057 RID: 4183
		// (get) Token: 0x060033AE RID: 13230
		// (set) Token: 0x060033AF RID: 13231
		string CatalogIconImageUrl { get; set; }

		/// <summary>Gets or sets a brief phrase that summarizes what a control does, for use in ToolTips and catalogs of <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> controls.</summary>
		/// <returns>A string that briefly summarizes the control's functionality. The default value is an empty string ("").</returns>
		// Token: 0x17001058 RID: 4184
		// (get) Token: 0x060033B0 RID: 13232
		// (set) Token: 0x060033B1 RID: 13233
		string Description { get; set; }

		/// <summary>Gets a string that is concatenated with the <see cref="P:System.Web.UI.WebControls.WebParts.IWebPart.Title" /> property value to form a complete title for a <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control.</summary>
		/// <returns>A string that serves as a subtitle for the control. The default value is an empty string ("").</returns>
		// Token: 0x17001059 RID: 4185
		// (get) Token: 0x060033B2 RID: 13234
		string Subtitle { get; }

		/// <summary>Gets or sets the title of a <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control.</summary>
		/// <returns>A string that contains the title of the control. The default value is an empty string ("").</returns>
		// Token: 0x1700105A RID: 4186
		// (get) Token: 0x060033B3 RID: 13235
		// (set) Token: 0x060033B4 RID: 13236
		string Title { get; set; }

		/// <summary>Gets or sets the URL to an image used to represent a Web Parts control in the control's own title bar.</summary>
		/// <returns>A string that represents the URL to an image. The default value is an empty string ("").</returns>
		// Token: 0x1700105B RID: 4187
		// (get) Token: 0x060033B5 RID: 13237
		// (set) Token: 0x060033B6 RID: 13238
		string TitleIconImageUrl { get; set; }

		/// <summary>Gets or sets a URL to supplemental information about a <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control.</summary>
		/// <returns>A string that represents a URL to more information about a <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control. The default value is an empty string ("").</returns>
		// Token: 0x1700105C RID: 4188
		// (get) Token: 0x060033B7 RID: 13239
		// (set) Token: 0x060033B8 RID: 13240
		string TitleUrl { get; set; }
	}
}
