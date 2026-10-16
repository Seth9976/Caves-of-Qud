using System;

namespace System.Web.UI
{
	/// <summary>Defines the properties and methods any class must implement to support view state management for a server control.</summary>
	// Token: 0x0200017D RID: 381
	public interface IStateManager
	{
		/// <summary>When implemented by a class, gets a value indicating whether a server control is tracking its view state changes.</summary>
		/// <returns>true if a server control is tracking its view state changes; otherwise, false.</returns>
		// Token: 0x1700051D RID: 1309
		// (get) Token: 0x06000F53 RID: 3923
		bool IsTrackingViewState { get; }

		/// <summary>When implemented by a class, loads the server control's previously saved view state to the control.</summary>
		/// <param name="state">An <see cref="T:System.Object" /> that contains the saved view state values for the control. </param>
		// Token: 0x06000F54 RID: 3924
		void LoadViewState(object state);

		/// <summary>When implemented by a class, saves the changes to a server control's view state to an <see cref="T:System.Object" />.</summary>
		/// <returns>The <see cref="T:System.Object" /> that contains the view state changes.</returns>
		// Token: 0x06000F55 RID: 3925
		object SaveViewState();

		/// <summary>When implemented by a class, instructs the server control to track changes to its view state.</summary>
		// Token: 0x06000F56 RID: 3926
		void TrackViewState();
	}
}
