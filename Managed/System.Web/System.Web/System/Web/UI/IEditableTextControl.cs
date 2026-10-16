using System;

namespace System.Web.UI
{
	/// <summary>Represents a control that renders text that can be changed by the user.</summary>
	// Token: 0x02000198 RID: 408
	public interface IEditableTextControl : ITextControl
	{
		/// <summary>Occurs when the content of the text changes between posts to the server. </summary>
		// Token: 0x14000021 RID: 33
		// (add) Token: 0x06000F97 RID: 3991
		// (remove) Token: 0x06000F98 RID: 3992
		event EventHandler TextChanged;
	}
}
