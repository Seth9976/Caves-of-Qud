using System;

namespace System.Web.UI.WebControls
{
	/// <summary>Defines a method that enables controls to obtain a callback script.</summary>
	// Token: 0x020002CB RID: 715
	public interface ICallbackContainer
	{
		/// <summary>Creates a script for initiating a client callback to a Web server.</summary>
		/// <returns>A script that, when run on a client, will initiate a callback to the Web server.</returns>
		/// <param name="buttonControl">The control initiating the callback request.</param>
		/// <param name="argument">The arguments used to build the callback script.</param>
		// Token: 0x06001B21 RID: 6945
		string GetCallbackScript(IButtonControl buttonControl, string argument);
	}
}
