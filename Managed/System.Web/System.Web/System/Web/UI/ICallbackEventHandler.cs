using System;

namespace System.Web.UI
{
	/// <summary>Used to indicate that a control can be the target of a callback event on the server.</summary>
	// Token: 0x02000165 RID: 357
	public interface ICallbackEventHandler
	{
		/// <summary>Processes a callback event that targets a control.</summary>
		/// <param name="eventArgument">A string that represents an event argument to pass to the event handler.</param>
		// Token: 0x06000F17 RID: 3863
		void RaiseCallbackEvent(string eventArgument);

		/// <summary>Returns the results of a callback event that targets a control.</summary>
		/// <returns>The result of the callback.</returns>
		// Token: 0x06000F18 RID: 3864
		string GetCallbackResult();
	}
}
