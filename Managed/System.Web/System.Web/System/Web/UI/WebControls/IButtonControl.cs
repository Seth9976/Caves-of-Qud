using System;

namespace System.Web.UI.WebControls
{
	/// <summary>Defines properties and events that must be implemented to allow a control to act like a button on a Web page.</summary>
	// Token: 0x020002CA RID: 714
	public interface IButtonControl
	{
		/// <summary>Gets or sets a value indicating whether clicking the button causes page validation to occur.</summary>
		/// <returns>true if clicking the button causes page validation to occur; otherwise, false.</returns>
		// Token: 0x17000866 RID: 2150
		// (get) Token: 0x06001B11 RID: 6929
		// (set) Token: 0x06001B12 RID: 6930
		bool CausesValidation { get; set; }

		/// <summary>Gets or sets an optional argument that is propagated to the <see cref="E:System.Web.UI.WebControls.IButtonControl.Command" /> event.</summary>
		/// <returns>The argument that is propagated to the <see cref="E:System.Web.UI.WebControls.IButtonControl.Command" /> event.</returns>
		// Token: 0x17000867 RID: 2151
		// (get) Token: 0x06001B13 RID: 6931
		// (set) Token: 0x06001B14 RID: 6932
		string CommandArgument { get; set; }

		/// <summary>Gets or sets the command name that is propagated to the <see cref="E:System.Web.UI.WebControls.IButtonControl.Command" /> event.</summary>
		/// <returns>The name of the command that is propagated to the <see cref="E:System.Web.UI.WebControls.IButtonControl.Command" /> event.</returns>
		// Token: 0x17000868 RID: 2152
		// (get) Token: 0x06001B15 RID: 6933
		// (set) Token: 0x06001B16 RID: 6934
		string CommandName { get; set; }

		/// <summary>Occurs when the button control is clicked.</summary>
		// Token: 0x14000041 RID: 65
		// (add) Token: 0x06001B17 RID: 6935
		// (remove) Token: 0x06001B18 RID: 6936
		event EventHandler Click;

		/// <summary>Occurs when the button control is clicked.</summary>
		// Token: 0x14000042 RID: 66
		// (add) Token: 0x06001B19 RID: 6937
		// (remove) Token: 0x06001B1A RID: 6938
		event CommandEventHandler Command;

		/// <summary>Gets or sets the URL of the Web page to post to from the current page when the button control is clicked.</summary>
		/// <returns>The URL of the Web page to post to from the current page when the button control is clicked.</returns>
		// Token: 0x17000869 RID: 2153
		// (get) Token: 0x06001B1B RID: 6939
		// (set) Token: 0x06001B1C RID: 6940
		string PostBackUrl { get; set; }

		/// <summary>Gets or sets the text caption displayed for the button.</summary>
		/// <returns>The text caption displayed for the button.</returns>
		// Token: 0x1700086A RID: 2154
		// (get) Token: 0x06001B1D RID: 6941
		// (set) Token: 0x06001B1E RID: 6942
		string Text { get; set; }

		/// <summary>Gets or sets the name for the group of controls for which the button control causes validation when it posts back to the server.</summary>
		/// <returns>The name for the group of controls for which the button control causes validation when it posts back to the server.</returns>
		// Token: 0x1700086B RID: 2155
		// (get) Token: 0x06001B1F RID: 6943
		// (set) Token: 0x06001B20 RID: 6944
		string ValidationGroup { get; set; }
	}
}
