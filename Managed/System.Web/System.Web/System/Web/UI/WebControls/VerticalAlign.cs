using System;
using System.ComponentModel;

namespace System.Web.UI.WebControls
{
	/// <summary>Specifies the vertical alignment of an object or text in a control.</summary>
	// Token: 0x02000323 RID: 803
	[TypeConverter(typeof(VerticalAlignConverter))]
	public enum VerticalAlign
	{
		/// <summary>Vertical alignment is not set.</summary>
		// Token: 0x040017CB RID: 6091
		NotSet,
		/// <summary>Text or object is aligned with the top of the enclosing control.</summary>
		// Token: 0x040017CC RID: 6092
		Top,
		/// <summary>Text or object is aligned with the center of the enclosing control.</summary>
		// Token: 0x040017CD RID: 6093
		Middle,
		/// <summary>Text or object is aligned with the bottom of the enclosing control.</summary>
		// Token: 0x040017CE RID: 6094
		Bottom
	}
}
