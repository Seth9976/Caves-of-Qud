using System;

namespace System.Web.UI
{
	/// <summary>Defines the interface a control implements to get or set its text content.</summary>
	// Token: 0x0200019A RID: 410
	public interface ITextControl
	{
		/// <summary>Gets or sets the text content of a control.</summary>
		/// <returns>The text content of a control.</returns>
		// Token: 0x1700052C RID: 1324
		// (get) Token: 0x06000F9C RID: 3996
		// (set) Token: 0x06000F9D RID: 3997
		string Text { get; set; }
	}
}
