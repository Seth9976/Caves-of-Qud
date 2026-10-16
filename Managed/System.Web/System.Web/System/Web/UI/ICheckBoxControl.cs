using System;

namespace System.Web.UI
{
	/// <summary>Defines the property and event a control implements to act as a check box. </summary>
	// Token: 0x02000197 RID: 407
	public interface ICheckBoxControl
	{
		/// <summary>Gets or sets the value of an <see cref="T:System.Web.UI.ICheckBoxControl" /> control that indicates whether the control is selected.</summary>
		/// <returns>true if the check box is selected; otherwise, false.</returns>
		// Token: 0x17000529 RID: 1321
		// (get) Token: 0x06000F93 RID: 3987
		// (set) Token: 0x06000F94 RID: 3988
		bool Checked { get; set; }

		/// <summary>Occurs when the value of the <see cref="P:System.Web.UI.ICheckBoxControl.Checked" /> property changes between posts to the server.</summary>
		// Token: 0x14000020 RID: 32
		// (add) Token: 0x06000F95 RID: 3989
		// (remove) Token: 0x06000F96 RID: 3990
		event EventHandler CheckedChanged;
	}
}
