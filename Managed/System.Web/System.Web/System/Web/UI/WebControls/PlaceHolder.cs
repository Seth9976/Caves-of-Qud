using System;
using System.ComponentModel;

namespace System.Web.UI.WebControls
{
	/// <summary>Stores dynamically added server controls on the Web page.</summary>
	// Token: 0x020003EE RID: 1006
	[ControlBuilder(typeof(PlaceHolderControlBuilder))]
	public class PlaceHolder : Control
	{
		/// <summary>Gets or sets a value indicating whether themes apply to this control.</summary>
		/// <returns>true to use themes; otherwise, false. The default is false.</returns>
		// Token: 0x17000E40 RID: 3648
		// (get) Token: 0x06002C76 RID: 11382 RVA: 0x000743A0 File Offset: 0x000725A0
		// (set) Token: 0x06002C77 RID: 11383 RVA: 0x000743A8 File Offset: 0x000725A8
		[Browsable(true)]
		public override bool EnableTheming { get; set; }
	}
}
